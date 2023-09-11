using System.IdentityModel.Tokens.Jwt;
using _4Create.Application.Behaviours;
using _4Create.Infrastructure.Services;
using _4Create.Infrastructure.Services.Authentication;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace _4Create.Application.Bootstrap;

public static class ApplicationBootstrap
{
    public static IServiceCollection ConfigureJwtService(
        this IServiceCollection services,
        string validAudience,
        string validIssuer,
        string signingKey,
        int expirationTimeInMinutes)
    {
        services.AddSingleton<JwtSecurityTokenHandler>();
        services.AddSingleton<IJwtTokenService>(sp =>
        {
            var jwtSecurityTokenHandler = sp.GetRequiredService<JwtSecurityTokenHandler>();
            return new JwtTokenService(
                jwtSecurityTokenHandler,
                validAudience,
                validIssuer,
                signingKey,
                expirationTimeInMinutes);
        });
        
        return services;
    }
    
    public static IServiceCollection ConfigureMediatr(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(ApplicationBootstrap).Assembly));
        
        services.AddValidatorsFromAssembly(typeof(ApplicationBootstrap).Assembly, includeInternalTypes: true);
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehaviour<,>));

        return services;
    }
}
