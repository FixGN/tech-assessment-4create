using System.ComponentModel.DataAnnotations;

namespace _4Create.WebApi.Configuration.Options;

public record AuthenticationOptions
{
    public const string OptionsName = "Authentication";
    [Required]
    public required string Issuer { get; set; }
    [Required]
    public required string Audience { get; set; }
    [Required]
    public required string SigningKey { get; set; }
    [Required]
    public required int ExpirationTimeInMinutes { get; set; }
}
