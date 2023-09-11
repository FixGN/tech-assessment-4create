using System.Net.Mail;
using _4Create.Domain.Exceptions;
using _4Create.Domain.Interfaces;
using _4Create.Domain.Services.Abstraction;

namespace _4Create.Domain.Services;

public class EmployeesService : IEmployeeService
{
    private readonly IEmployeesReadRepository _employeesRepository;

    public EmployeesService(IEmployeesReadRepository employeesRepository)
    {
        _employeesRepository = employeesRepository;
    }

    public async Task ValidateEmailUniquenessAsync(
        MailAddress email,
        CancellationToken cancellationToken = default)
    {
        if (await _employeesRepository.IsUserWithEmailExistAsync(email.Address, cancellationToken))
        {
            throw new EmailAlreadyExistsException(email.Address);
        }
    }
}
