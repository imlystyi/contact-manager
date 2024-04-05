using System.Text.RegularExpressions;
using ContactManager.Server.Models.Dto;
using FluentValidation;

namespace ContactManager.Server.Validators;

public class ContactCsvValidator : AbstractValidator<ContactCsvInputDto>
{
    public ContactCsvValidator()
    {
        this.RuleFor(x => x.Name).MaximumLength(120);
        this.RuleFor(x => x.Phone).MaximumLength(20);
    }
}
