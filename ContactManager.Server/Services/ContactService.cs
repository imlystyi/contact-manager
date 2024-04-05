using ContactManager.Server.Contexts;
using ContactManager.Server.Exceptions;
using ContactManager.Server.Models.Dto;
using ContactManager.Server.Models.Entities;
using ContactManager.Server.Validators;

namespace ContactManager.Server.Services;

public class ContactService(ContactContext contactContext)
{
    public IEnumerable<ContactOutputDto> GetAllContacts()
    {
        return contactContext.GetAll().Select(c => (ContactOutputDto)c);
    }

    public void CreateContact(ContactInputDto inputDto)
    {
        Contact contact = (Contact)inputDto;

        contactContext.Add(contact);
        contactContext.SaveChanges();
    }

    public void CreateFromCsv(string csvString)
    {
        List<Contact> contacts = [ ];

        using StringReader sr = new(csvString);
        while (sr.Peek() != -1)
        {
            string[] csvData = sr.ReadLine()!.Split(',');

            if (csvData.Length != 5 ||
                !DateOnly.TryParse(csvData[1], out DateOnly birthDate) ||
                !bool.TryParse(csvData[2], out bool isMarried) ||
                !decimal.TryParse(csvData[4], out decimal salary))
                throw new ArgumentException("Bad CSV");

            ContactCsvInputDto contact = new()
            {
                    Name = csvData[0],
                    BirthDate = birthDate,
                    IsMarried = isMarried,
                    Phone = csvData[3],
                    Salary = salary
            };

            ContactCsvValidator validator = new();

            if (!validator.Validate(contact).IsValid)
                throw new ArgumentException("Bad CSV");

            contacts.Add((Contact)contact);
        }

        contactContext.AddRange(contacts);
        contactContext.SaveChanges();
    }

    public void EditContact(ContactUpdateDto updateDto)
    {
        Contact contact = contactContext.FindById(updateDto.Id) ?? throw new ContactNotFound();

        contact.Name = updateDto.Name;
        contact.BirthDate = updateDto.BirthDate;
        contact.IsMarried = updateDto.IsMarried;
        contact.Phone = updateDto.Phone;
        contact.Salary = updateDto.Salary;

        contactContext.SaveChanges();
    }

    public void DeleteContact(long id)
    {
        Contact contact = contactContext.FindById(id) ?? throw new ContactNotFound();

        contactContext.Remove(contact);
        contactContext.SaveChanges();
    }
}
