using System.ComponentModel.DataAnnotations;

namespace _4Create.WebApi.Configuration.Options;

public record MySqlOptions
{
    public const string OptionsName = "MySql";
    [Required]
    public required string ConnectionString { get; set; }
}
