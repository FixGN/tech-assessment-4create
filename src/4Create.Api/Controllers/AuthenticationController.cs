using _4Create.Contracts.Dtos.Authentication;
using _4Create.WebApi.Mappers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace _4Create.WebApi.Controllers;

[ApiController]
[AllowAnonymous]
[Route("/api/authentication")]
public class AuthenticationController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthenticationController(
        IMediator mediator)
    {
        _mediator = mediator;
    }
    
    /// <summary>
    /// Get access token for user
    /// </summary>
    /// <param name="request">User credentials</param>
    /// <returns></returns>
    [HttpPost("user/authorize")]
    public async Task<ActionResult<UserAuthorizeResponseDto>> UserAuthorize(UserAuthorizeRequestDto request)
    {
        var query = AuthenticationMapper.UserAuthorizeRequestDtoToQuery(request);
        var token = await _mediator.Send(query);

        return Ok(new UserAuthorizeResponseDto(token));
    }
}
