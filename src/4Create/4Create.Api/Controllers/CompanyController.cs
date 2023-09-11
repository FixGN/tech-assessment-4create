using _4Create.Application.UseCases.Companies.Commands.CreateCompany;
using _4Create.Contracts.Dtos.Companies.CreateCompany.Request;
using _4Create.Contracts.Dtos.Companies.CreateCompany.Response;
using _4Create.WebApi.Authentication.Extensions;
using _4Create.WebApi.Mappers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace _4Create.WebApi.Controllers;

[ApiController]
[Authorize]
[Route("/api/companies")]
public class CompanyController : ControllerBase
{
    private readonly IMediator _mediator;

    public CompanyController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    /// <summary>
    /// Create new company
    /// </summary>
    /// <param name="request">Company model with name and number of employees</param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<CreateCompanyResponse>> Get([FromBody] CreateCompanyRequest request)
    {
        var userId = User.GetUserId();

        var command = new CreateCompanyCommand(
            userId,
            CompaniesMapper.CreateCompanyRequestToDto(request));

        var companyId = await _mediator.Send(command);
        
        return Ok(new CreateCompanyResponse(companyId));
    }
}
