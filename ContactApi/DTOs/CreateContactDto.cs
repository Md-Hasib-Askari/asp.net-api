namespace ContactApi.DTOs;

public class CreateContactDto
{
    public string Name { get; set; }
    public string Email { get; set; }

    public CreateContactDto(string name, string email)
    {
        Name = name;
        Email = email;
    }
}