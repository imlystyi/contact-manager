using System.ComponentModel.DataAnnotations;

namespace ContactManager.Server.Models.Dto;

public class ContactUpdateDto
{
    public required long Id { get; set; }

    [StringLength(120)]
    public required string Name { get; set; }

    public required DateOnly BirthDate { get; set; }

    public required bool IsMarried { get; set; }

    [StringLength(20)]
    [Phone]
    public required string Phone { get; set; }

    public required decimal Salary { get; set; }
}
