using ContactApi.Models;

namespace ContactApi.Repositories;

public interface IContactRepository
{
    void Add(Contact contact);
    IEnumerable<Contact> GetAll();
    IEnumerable<Contact> Search(string query);
    IEnumerable<Contact> GetPaged(string query, int pageNumber, int pageSize, string sortBy, string sortOrder);
}