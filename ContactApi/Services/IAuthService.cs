using ContactApi.DTOs.Auth;
using ContactApi.Models;

namespace ContactApi.Services;

public interface IAuthService
{
    string GenerateJwtToken(User user);
    string Register(RegisterDto registerDto);
    User Login(LoginDto loginDto);
}