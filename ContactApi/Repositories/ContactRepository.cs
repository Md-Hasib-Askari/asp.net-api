using ContactApi.Models;
using ContactApi.Data;
using Microsoft.EntityFrameworkCore;

namespace ContactApi.Repositories;

public class ContactRepository : IContactRepository
{
    private readonly AppDbContext db;

    public ContactRepository(AppDbContext db)
    {
        this.db = db;
    }

    public void Add(Contact contact)
    {
        db.Contacts.Add(contact);
        db.SaveChanges();
    }

    public IEnumerable<Contact> GetAll()
    {
        return db.Contacts.ToList();
    }

    public IEnumerable<Contact> GetPaged(string keyword, int pageNumber, int pageSize, string sortBy, string sortOrder)
    {
        var query = db.Contacts.AsQueryable();

        if (!string.IsNullOrWhiteSpace(keyword))
            query = query.Where(c => EF.Functions.ILike(c.Name, $"%{keyword}%") || EF.Functions.Like(c.Email, $"%{keyword}%"));

        // Sorting
        sortBy = sortBy?.ToLower() ?? "name";
        sortOrder = sortOrder?.ToLower() ?? "asc";

        query = (sortBy, sortOrder) switch
        {
            ("name", "asc") => query.OrderBy(c => c.Name),
            ("name", "desc") => query.OrderByDescending(c => c.Name),
            ("email", "asc") => query.OrderBy(c => c.Email),
            ("email", "desc") => query.OrderByDescending(c => c.Email),
            _ => query.OrderBy(c => c.Name)
        };

        // Pagination
        return query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
    }

    public IEnumerable<Contact> Search(string keyword)
    {
        return db.Contacts
            .Where(c => c.Name.Contains(keyword) || c.Email.Contains(keyword)).ToList();
    }
}