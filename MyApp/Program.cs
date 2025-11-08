namespace MyApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IContactService manager = new ContactService();
            while (true)
            {
                Console.WriteLine("1. Add Contact");
                Console.WriteLine("2. View Contacts");
                Console.WriteLine("3. Search Contacts");
                Console.WriteLine("4. Exit");
                Console.Write("Choose an option: ");
                string choice = Console.ReadLine() ?? string.Empty;

                switch (choice)
                {
                    case "1":
                        Console.Write("Enter Name: ");
                        string name = Console.ReadLine() ?? string.Empty;
                        Console.Write("Enter Email: ");
                        string email = Console.ReadLine() ?? string.Empty;

                        try
                        {
                            var contact = new Contact(name, email);
                            manager.Add(contact);
                            Console.WriteLine("Contact added successfully!\n");
                        }
                        catch (ArgumentException ex)
                        {
                            Console.WriteLine($"Error: {ex.Message}\n");
                            break;
                        }
                        break;
                    case "2":
                        var contacts = manager.GetAll();
                        Console.WriteLine("\nContacts:");
                        foreach (var contact in contacts)
                        {
                            Console.WriteLine($"Name: {contact.Name}, Email: {contact.Email}");
                        }
                        Console.WriteLine();
                        break;
                    case "3":
                        Console.Write("Enter search query: ");
                        string query = Console.ReadLine() ?? string.Empty;
                        var results = manager.Search(query);
                        Console.WriteLine("\nSearch Results:");
                        foreach (var contact in results)
                        {
                            Console.WriteLine($"Name: {contact.Name}, Email: {contact.Email}");
                        }
                        Console.WriteLine();
                        break;
                    case "4":
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please try again.\n");
                        break;
                }
            }
        }
    }
}