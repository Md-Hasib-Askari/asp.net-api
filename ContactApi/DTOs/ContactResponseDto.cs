namespace ContactApi.DTOs;

public class ContactResponseDto
{
    public string Name { get; set; }
    public string Email { get; set; }

    public ContactResponseDto(string name, string email)
    {
        Name = name;
        Email = email;
    }
}