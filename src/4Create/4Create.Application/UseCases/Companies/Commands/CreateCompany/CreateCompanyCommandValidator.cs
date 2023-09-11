using _4Create.Domain.Aggregates.Employees.Enum;
using FluentValidation;

namespace _4Create.Application.UseCases.Companies.Commands.CreateCompany;

public class CreateCompanyCommandValidator : AbstractValidator<CreateCompanyCommand>
{
    public CreateCompanyCommandValidator()
    {
        RuleFor(c => c.CreatedById).NotEmpty();
        RuleFor(c => c.Company).NotNull();
        RuleFor(c => c.Company.Name).NotEmpty();
        RuleForEach(c => c.Company.Employees).ChildRules(rules =>
        {
            rules.When(c => c.Id is not null, () =>
            {
                rules.RuleFor(e => e.Email)
                    .Null()
                    .WithMessage("You can't set employee email if you set Id");
                rules.RuleFor(e => e.Title)
                    .Equal(EmployeeTitle.Undefined)
                    .WithMessage("You can't set employee title if you set Id");
            });
            rules.When(c => c.Email is not null && c.Title is not null, () =>
            {
                rules.RuleFor(e => e.Id)
                    .Null()
                    .WithMessage("You can't set employee Id if you set Email and Title");
                
                rules.RuleFor(e => e.Title)
                    .IsInEnum()
                    .NotEqual(EmployeeTitle.Undefined);
            });
        });
    }
}
