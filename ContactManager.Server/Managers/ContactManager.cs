using ContactManager.Contexts;
using ContactManager.Server.Models.Dto;
using ContactManager.Server.Models.Entities;

namespace ContactManager.Server.Managers;

public class ContactManager(ContactContext contactContext)
{
    public void CreateContact(ContactInputDto inputDto)
    {
        Contact contact = (Contact)inputDto;

        contactContext.Add(contact);
        contactContext.SaveChanges();
    }

    public void CreateFromCsv(string csv)
    {
        List<Contact> contacts = [ ];

        StreamReader sr = new(csv);
        while (!sr.EndOfStream)
        {
            string[] csvData = sr.ReadLine()!.Split(',');
            ContactInputDto contact = new()
            {
                    Name = csvData[0],
                    BirthDate = DateOnly.Parse(csvData[1]),
                    IsMarried = bool.Parse(csvData[2]),
                    Phone = csvData[3],
                    Salary = decimal.Parse(csvData[4])
            };

            contacts.Add((Contact)contact);
        }

        contactContext.AddRange(contacts);
        contactContext.SaveChanges();
    }

    public void EditContact(ContactUpdateDto updateDto)
    {
        Contact contact = contactContext.FindById(updateDto.Id);

        contact.Name = updateDto.Name;
        contact.BirthDate = updateDto.BirthDate;
        contact.IsMarried = updateDto.IsMarried;
        contact.Phone = updateDto.Phone;
        contact.Salary = updateDto.Salary;

        contactContext.SaveChanges();
    }

    public void DeleteContact(long id)
    {
        Contact contact = contactContext.FindById(id);

        contactContext.Remove(contact);
        contactContext.SaveChanges();
    }
}
