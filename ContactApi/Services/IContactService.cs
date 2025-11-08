using ContactApi.Models;

namespace ContactApi.Services;

public interface IContactService
{
    void Add(Contact contact);
    IEnumerable<Contact> GetAll();
    IEnumerable<Contact> GetPaged(string? query, int pageNumber, int pageSize, string sortBy, string sortOrder);
    IEnumerable<Contact> Search(string? query);
}