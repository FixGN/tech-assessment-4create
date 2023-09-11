using System.Security.Claims;

namespace _4Create.WebApi.Authentication.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static Guid GetUserId(this ClaimsPrincipal? user)
    {
        var userId = user?.Claims
            .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

        if (userId is not null)
        {
            return new Guid(userId.Value);
        }

        throw new InvalidOperationException(
            $"Can't get {nameof(ClaimTypes.Name)} from ClaimPrincipal");
    }
}
