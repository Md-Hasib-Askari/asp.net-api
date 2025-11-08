using System.Text.RegularExpressions;

namespace MyApp;

// Data modesl
class Contact
{
    public string Name { get; set; }
    public string Email { get; set; }

    public Contact(string name, string email)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be empty.", nameof(name));

        var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        if (String.IsNullOrWhiteSpace(email) || !emailRegex.IsMatch(email))
            throw new ArgumentException("Invalid email address.", nameof(email));

        Name = name;
        Email = email;
    }
}

// Repository interfaces
interface IContactRepository
{
    void Add(Contact contact);
    List<Contact> GetAll();
    List<Contact> Search(string query);
}

// Repository implementations
class InMemoryContactRepository : IContactRepository
{
    private List<Contact> contacts = new List<Contact>();

    public void Add(Contact contact) => contacts.Add(contact);

    public List<Contact> GetAll() => contacts;

    public List<Contact> Search(string query) => contacts.Where(c => c.Name.Contains(query, StringComparison.OrdinalIgnoreCase) || c.Email.Contains(query, StringComparison.OrdinalIgnoreCase)).ToList();
}

// Service interfaces
interface IContactService
{
    void Add(Contact contact);
    List<Contact> GetAll();
    List<Contact> Search(string query);
}

// Service implementations
class ContactService : IContactService
{
    private readonly IContactRepository repository;

    public ContactService(IContactRepository? repo = null)
    {
        repository = repo ?? new InMemoryContactRepository();
    }

    public void Add(Contact contact) => repository.Add(contact);

    public List<Contact> GetAll() => repository.GetAll();

    public List<Contact> Search(string query) => repository.Search(query);
}