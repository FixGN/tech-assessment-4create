using MediatR;

namespace _4Create.Application.UseCases.Authentication.Query.UserAuthorizeQuery;

public record UserAuthorizeQuery(
    string Username,
    string Password) : IRequest<string>;
