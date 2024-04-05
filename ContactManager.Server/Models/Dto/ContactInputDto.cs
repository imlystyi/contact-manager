using ContactManager.Server.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace ContactManager.Server.Models.Dto;

public class ContactInputDto
{
    [StringLength(120)]
    public required string Name { get; set; }

    public required DateOnly BirthDate { get; set; }

    public required bool IsMarried { get; set; }

    [StringLength(20)]
    [Phone]
    public required string Phone { get; set; }

    public required decimal Salary { get; set; }

    public static explicit operator Contact(ContactInputDto v) => new()
    {
            Name = v.Name,
            BirthDate = v.BirthDate,
            IsMarried = v.IsMarried,
            Phone = v.Phone,
            Salary = v.Salary
    };
}
