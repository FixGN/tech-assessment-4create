using System.Net.Mail;

namespace _4Create.Domain.Services.Abstraction;

public interface IEmployeeService
{
    Task ValidateEmailUniquenessAsync(MailAddress email, CancellationToken cancellationToken = default);
}
