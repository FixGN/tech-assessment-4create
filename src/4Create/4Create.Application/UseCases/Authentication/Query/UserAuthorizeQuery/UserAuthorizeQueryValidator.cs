using FluentValidation;

namespace _4Create.Application.UseCases.Authentication.Query.UserAuthorizeQuery;

public class UserAuthorizeQueryValidator : AbstractValidator<UserAuthorizeQuery>
{
    public UserAuthorizeQueryValidator()
    {
        RuleFor(u => u.Username).NotEmpty();
        RuleFor(u => u.Password).NotEmpty();
    }
}
