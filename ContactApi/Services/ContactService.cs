using ContactApi.Models;
using ContactApi.Repositories;

namespace ContactApi.Services;

public class ContactService : IContactService
{
    private readonly IContactRepository contactRepository;

    public ContactService(IContactRepository contactRepository)
    {
        this.contactRepository = contactRepository;
    }

    public void Add(Contact contact)
    {
        contactRepository.Add(contact);
    }

    public IEnumerable<Contact> GetAll()
    {
        return contactRepository.GetAll();
    }

    public IEnumerable<Contact> GetPaged(string query, int pageNumber, int pageSize, string sortBy, string sortOrder)
    {
        return contactRepository.GetPaged(query, pageNumber, pageSize, sortBy, sortOrder);
    }

    public IEnumerable<Contact> Search(string query)
    {
        return contactRepository.Search(query);
    }
}
