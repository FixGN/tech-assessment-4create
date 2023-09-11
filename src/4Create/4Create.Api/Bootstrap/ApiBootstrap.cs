using System.Text;
using _4Create.Application.Bootstrap;
using _4Create.Application.Exceptions;
using _4Create.Domain.Exceptions;
using _4Create.WebApi.ExceptionHandlers;
using _4Create.WebApi.ExceptionHandlers.Application;
using _4Create.WebApi.ExceptionHandlers.Domain;
using _4Create.WebApi.ExceptionHandlers.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace _4Create.WebApi.Bootstrap;

public static class ApiBootstrap
{
    public static IServiceCollection ConfigureSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(opt =>
        {
            opt.SwaggerDoc(
                "v1",
                new OpenApiInfo
                {
                    Title = "4Create tech assessment API",
                    Version = "v1"
                });
            opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter access token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "bearer"
            });
            opt.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type=ReferenceType.SecurityScheme,
                            Id="Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });

        return services;
    }

    public static IServiceCollection ConfigureAuthentication(
        this IServiceCollection services,
        string validAudience,
        string validIssuer,
        string signingKey,
        int expirationTimeInMinutes)
    {
        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = validAudience,
                    ValidIssuer = validIssuer,
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey))
                };
            });

        services.ConfigureJwtService(
            validAudience,
            validIssuer,
            signingKey,
            expirationTimeInMinutes);
        
        return services;
    }

    public static IServiceCollection ConfigureExceptionHandlers(this IServiceCollection services)
    {
        services.AddSingleton<IExceptionHandler, DefaultExceptionHandler>();

        services.AddSingleton<IExceptionHandler<CompaniesNotFoundException>, CompaniesNotFoundExceptionHandler>();
        services.AddSingleton<IExceptionHandler<EmployeesNotFoundException>, EmployeesNotFoundExceptionHandler>();
        services.AddSingleton<IExceptionHandler<UserNotFoundException>, UserNotFoundExceptionHandler>();
        services.AddSingleton<IExceptionHandler<UserPasswordIsInvalidException>, UserPasswordIsInvalidExceptionHandler>();
        
        services.AddSingleton<IExceptionHandler<CompaniesDoesntExistException>, CompanyDoesntExistExceptionHandler>();
        services.AddSingleton<IExceptionHandler<CompanyNameExistsException>, CompanyNameExistsExceptionHandler>();
        services.AddSingleton<IExceptionHandler<DuplicateEmployeeTitleException>, DuplicateEmployeeTitleExceptionHandler>();
        services.AddSingleton<IExceptionHandler<DuplicateEmployeeTitleInCompaniesException>, DuplicateEmployeeTitleInCompaniesExceptionHandler>();
        services.AddSingleton<IExceptionHandler<EmailAlreadyExistsException>, EmailAlreadyExistsExceptionHandler>();
        
        return services;
    }
}
