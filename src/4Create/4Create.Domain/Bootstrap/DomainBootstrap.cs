using _4Create.Domain.Services;
using _4Create.Domain.Services.Abstraction;
using Microsoft.Extensions.DependencyInjection;

namespace _4Create.Domain.Bootstrap;

public static class DomainBootstrap
{
    public static IServiceCollection ConfigureDomain(this IServiceCollection services)
    {
        services.AddScoped<ICompanyService, CompanyService>();
        services.AddScoped<IEmployeeService, EmployeesService>();

        return services;
    }
}
