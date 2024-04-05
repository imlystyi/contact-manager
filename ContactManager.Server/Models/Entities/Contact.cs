using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContactManager.Server.Models.Entities;

[Table("contact")]
public class Contact
{
    [Column("id")]
    [Key]
    public long Id { get; set; }

    [Column("name")]
    [StringLength(120)]
    public string Name { get; set; }

    [Column("birth_date")]
    public DateOnly BirthDate { get; set; }

    [Column("is_married")]
    public bool IsMarried { get; set; }

    [Column("phone")]
    [StringLength(20)]
    public string Phone { get; set; }

    [Column("salary")]
    public decimal Salary { get; set; }
}
