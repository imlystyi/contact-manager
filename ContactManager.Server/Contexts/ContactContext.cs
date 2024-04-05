using ContactManager.Server.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ContactManager.Server.Contexts;

public class ContactContext(DbContextOptions<ContactContext> options) : DbContext(options)
{
    public DbSet<Contact> Contacts { get; set; }

#nullable enable

    public Contact? FindById(long id) => this.Contacts.Find(id);

#nullable restore

    public List<Contact> GetAll() => this.Contacts.ToList();
}
