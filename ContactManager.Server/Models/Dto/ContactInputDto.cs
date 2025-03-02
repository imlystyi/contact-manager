﻿using System.ComponentModel.DataAnnotations;
using ContactManager.Server.Models.Entities;

namespace ContactManager.Server.Models.Dto;

public class ContactInputDto
{
    [StringLength(120)]
    public required string Name { get; set; }

    public required string BirthDate { get; set; }

    public required bool IsMarried { get; set; }

    [StringLength(20)]
    [Phone]
    public required string Phone { get; set; }

    public required decimal Salary { get; set; }

    public static explicit operator Contact(ContactInputDto v) => new()
    {
            Name = v.Name,
            BirthDate = DateOnly.Parse(v.BirthDate),
            IsMarried = v.IsMarried,
            Phone = v.Phone,
            Salary = v.Salary
    };
}
