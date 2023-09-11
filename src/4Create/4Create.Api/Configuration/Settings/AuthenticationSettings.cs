using _4Create.WebApi.Configuration.Options;

namespace _4Create.WebApi.Configuration.Settings;

public class AuthenticationSettings
{
    private AuthenticationSettings(
        string issuer,
        string audience,
        string signingKey,
        int expirationTimeInMinutes)
    {
        if (string.IsNullOrWhiteSpace(issuer))
        {
            throw new ArgumentException("Value cannot be null or whitespace.", nameof(issuer));
        }

        if (string.IsNullOrWhiteSpace(audience))
        {
            throw new ArgumentException("Value cannot be null or whitespace.", nameof(audience));
        }

        if (string.IsNullOrWhiteSpace(signingKey))
        {
            throw new ArgumentException("Value cannot be null or whitespace.", nameof(signingKey));
        }

        Issuer = issuer;
        Audience = audience;
        SigningKey = signingKey;
        ExpirationTimeInMinutes = expirationTimeInMinutes;
    }
    
    public string Issuer { get; }
    public string Audience { get; }
    public string SigningKey { get; }
    public int ExpirationTimeInMinutes { get; }

    public static AuthenticationSettings FromOptions(AuthenticationOptions options)
    {
        return new AuthenticationSettings(
            options.Issuer,
            options.Audience,
            options.SigningKey,
            options.ExpirationTimeInMinutes);
    }
}
