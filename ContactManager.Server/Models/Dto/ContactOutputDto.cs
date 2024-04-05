using ContactManager.Server.Models.Entities;

namespace ContactManager.Server.Models.Dto;

public class ContactOutputDto
{
    public string Id { get; set; }

    public string Name { get; set; }

    public string BirthDate { get; set; }

    public string IsMarried { get; set; }

    public string Phone { get; set; }

    public string Salary { get; set; }

    public static explicit operator ContactOutputDto(Contact v) => new()
    {
            Id = v.Id.ToString(),
            Name = v.Name,
            BirthDate = v.BirthDate.ToString(),
            IsMarried = v.IsMarried
                    ? "Yes"
                    : "No",
            Phone = v.Phone,
            Salary = v.Salary.ToString("F2")
    };
}
