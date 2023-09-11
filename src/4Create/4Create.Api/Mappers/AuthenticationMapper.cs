using _4Create.Application.UseCases.Authentication.Query.UserAuthorizeQuery;
using _4Create.Contracts.Dtos.Authentication;
using Riok.Mapperly.Abstractions;

namespace _4Create.WebApi.Mappers;

[Mapper]
public static partial class AuthenticationMapper
{
    public static partial UserAuthorizeQuery UserAuthorizeRequestDtoToQuery(UserAuthorizeRequestDto request);
}
