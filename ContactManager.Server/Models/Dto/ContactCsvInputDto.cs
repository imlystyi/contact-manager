using ContactManager.Server.Models.Entities;

namespace ContactManager.Server.Models.Dto;

public class ContactCsvInputDto
{
    public required string Name { get; set; }

    public required DateOnly BirthDate { get; set; }

    public required bool IsMarried { get; set; }

    public required string Phone { get; set; }

    public required decimal Salary { get; set; }

    public static explicit operator Contact(ContactCsvInputDto v) => new()
    {
            Name = v.Name,
            BirthDate = v.BirthDate,
            IsMarried = v.IsMarried,
            Phone = v.Phone,
            Salary = v.Salary
    };
}
