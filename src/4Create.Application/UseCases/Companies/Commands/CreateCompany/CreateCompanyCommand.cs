using _4Create.Application.Dtos.Companies.CreateCompany;
using MediatR;

namespace _4Create.Application.UseCases.Companies.Commands.CreateCompany;

public record CreateCompanyCommand(
    Guid CreatedById,
    CreateCompanyDto Company) : IRequest<Guid>;
