using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using ContactApi.Data;
using ContactApi.DTOs.Auth;
using ContactApi.Models;
using ContactApi.Utils;

namespace ContactApi.Services;

public class AuthService : IAuthService
{
    private readonly AppDbContext db;
    private readonly IConfiguration config;

    public AuthService(AppDbContext db, IConfiguration config)
    {
        this.db = db;
        this.config = config;
    }

    public string GenerateJwtToken(User user)
    {
        var jwt = config.GetSection("Jwt");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt["Key"] ?? throw new Exception("JWT Key not found")));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        };

        var token = new JwtSecurityToken(
            issuer: jwt["Issuer"],
            audience: jwt["Audience"],
            claims: claims,
            expires: DateTime.Now.AddHours(2),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public User Login(LoginDto loginDto)
    {
        // print
        Console.WriteLine("================================");
        Console.WriteLine("JWT Configuration:");
        var jwt = config.GetSection("Jwt");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt["Key"] ?? throw new Exception("JWT Key not found")));
        Console.WriteLine(Encoding.UTF8.GetBytes(jwt["Key"]));
        Console.WriteLine("================================");

        var hash = PasswordHasher.HashPassword(loginDto.Password);
        var user = db.Users.FirstOrDefault(u => u.Username == loginDto.Username && u.PasswordHash == hash);
        if (user == null)
        {
            throw new Exception("Invalid username or password");
        }
        return user;
    }

    public string Register(RegisterDto registerDto)
    {
        var existingUser = db.Users.FirstOrDefault(u => u.Username == registerDto.Username);
        if (existingUser != null)
        {
            throw new Exception("Username already exists");
        }

        var passwordHash = PasswordHasher.HashPassword(registerDto.Password);
        var newUser = new User(registerDto.Username, passwordHash);
        db.Users.Add(newUser);
        db.SaveChanges();

        return "User registered successfully";
    }
}