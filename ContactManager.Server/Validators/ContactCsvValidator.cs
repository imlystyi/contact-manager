using ContactManager.Server.Models.Dto;
using FluentValidation;

namespace ContactManager.Server.Validators;

public class ContactCsvValidator : AbstractValidator<ContactCsvInputDto>
{
    public ContactCsvValidator()
    {
        this.RuleFor(x => x.Name).NotEmpty().MaximumLength(120);
        this.RuleFor(x => x.BirthDate).NotEmpty();
        this.RuleFor(x => x.IsMarried).NotEmpty();
        this.RuleFor(x => x.Phone).NotEmpty().MaximumLength(20)
            .Matches(@"((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}");
        this.RuleFor(x => x.Salary).NotEmpty();
    }
}
