using _4Create.Domain.Interfaces;
using _4Create.Infrastructure.Repositories.MySql;
using _4Create.Infrastructure.Services.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace _4Create.Infrastructure.Bootstrap;

public static class InfrastructureBootstrap
{
    public static IServiceCollection ConfigureMySql(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<MySqlDbContext>(options =>
        {
            options.UseMySQL(connectionString);
        });

        services.AddScoped<ICompaniesWriteRepository, CompaniesMySqlWriteRepository>();
        services.AddScoped<ICompaniesReadRepository, CompaniesMySqlReadRepository>();
        services.AddScoped<IEmployeesWriteRepository, EmployeesMySqlWriteRepository>();
        services.AddScoped<IEmployeesReadRepository, EmployeesMySqlReadRepository>();
        services.AddScoped<IUserReadRepository, UserMySqlReadRepository>();
        
        return services;
    }

    public static IServiceCollection ConfigurePasswordHashing(this IServiceCollection services)
    {
        services.AddSingleton<IPasswordHasher, PasswordHasher>();

        return services;
    }
}
