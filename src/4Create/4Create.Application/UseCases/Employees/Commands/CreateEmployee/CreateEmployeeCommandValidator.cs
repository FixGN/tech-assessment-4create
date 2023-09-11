using _4Create.Domain.Aggregates.Employees.Enum;
using FluentValidation;

namespace _4Create.Application.UseCases.Employees.Commands.CreateEmployee;

public class CreateEmployeeCommandValidator : AbstractValidator<CreateEmployeeCommand>
{
    public CreateEmployeeCommandValidator()
    {
        RuleFor(c => c.CreatedById)
            .NotEmpty();
        RuleFor(c => c.Employee)
            .NotNull();

        RuleFor(c => c.Employee.Email)
            .NotEmpty()
            .EmailAddress();
        RuleFor(c => c.Employee.Title)
            .IsInEnum()
            .NotEqual(EmployeeTitle.Undefined);
    }
}
