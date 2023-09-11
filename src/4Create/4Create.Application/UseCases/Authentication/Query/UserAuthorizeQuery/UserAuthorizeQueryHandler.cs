using _4Create.Application.Exceptions;
using _4Create.Domain.Interfaces;
using _4Create.Infrastructure.Services.Authentication;
using MediatR;
using System.Security.Authentication;

namespace _4Create.Application.UseCases.Authentication.Query.UserAuthorizeQuery;

public class UserAuthorizeQueryHandler : IRequestHandler<UserAuthorizeQuery, string>
{
    private readonly IUserReadRepository _userReadRepository;
    private readonly IJwtTokenService _jwtTokenService;
    private readonly IPasswordHasher _passwordHasher;

    public UserAuthorizeQueryHandler(
        IUserReadRepository userReadRepository,
        IJwtTokenService jwtTokenService,
        IPasswordHasher passwordHasher)
    {
        _userReadRepository = userReadRepository;
        _jwtTokenService = jwtTokenService;
        _passwordHasher = passwordHasher;
    }
    
    public async Task<string> Handle(UserAuthorizeQuery request, CancellationToken cancellationToken)
    {
        var user = await _userReadRepository.GetByName(request.Username, cancellationToken);

        if (user is null)
        {
            throw new UserNotFoundException(request.Username);
        }

        if (!_passwordHasher.Verify(user.HashedPassword, request.Password))
        {
            throw new UserPasswordIsInvalidException();
        }

        return _jwtTokenService.GenerateJwtToken(user.Id, user.Username, user.Role);
    }
}
