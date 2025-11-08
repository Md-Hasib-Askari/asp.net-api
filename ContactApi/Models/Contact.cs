using System.Text.RegularExpressions;

namespace ContactApi.Models;

public class Contact
{
    public int Id { get; set; } // Primary key
    public string Name { get; set; }
    public string Email { get; set; }

    public Contact(string name, string email)
    {
        // input validation
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be null or empty.", nameof(name));

        const string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        if (string.IsNullOrWhiteSpace(email) || !Regex.IsMatch(email, emailPattern))
            throw new ArgumentException("Invalid email format.", nameof(email));

        Name = name;
        Email = email;
    }
}