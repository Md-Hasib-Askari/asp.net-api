namespace ContactApi.Utils;

public static class PasswordHasher
{
    // Simple hash function for demonstration purposes only.
    public static string HashPassword(string password)
    {
        if (string.IsNullOrWhiteSpace(password))
            throw new ArgumentException("Password cannot be null or empty.", nameof(password));

        using (var sha256 = System.Security.Cryptography.SHA256.Create())
        {
            var bytes = System.Text.Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }

    public static bool VerifyPassword(string password, string storedHash)
    {
        var hashOfInput = HashPassword(password);
        return StringComparer.Ordinal.Compare(hashOfInput, storedHash) == 0;
    }
}