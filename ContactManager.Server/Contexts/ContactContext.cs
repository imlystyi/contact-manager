using ContactManager.Server.Models.Dto;
using ContactManager.Server.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ContactManager.Contexts;

public class ContactContext(DbContextOptions<ContactContext> options) : DbContext(options)
{
    public DbSet<Contact> Contacts { get; set; }

    public Contact FindById(long id) => this.Contacts.Find(id);

    public List<Contact> GetAll() => this.Contacts.ToList();
}
