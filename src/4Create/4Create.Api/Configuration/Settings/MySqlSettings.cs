using _4Create.WebApi.Configuration.Options;

namespace _4Create.WebApi.Configuration.Settings;

public class MySqlSettings
{
    private MySqlSettings(string connectionString)
    {
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new ArgumentException("Value cannot be null or whitespace.", nameof(connectionString));
        }

        ConnectionString = connectionString;
    }
    
    public string ConnectionString { get; }

    public static MySqlSettings FromOptions(MySqlOptions options)
    {
        return new MySqlSettings(options.ConnectionString);
    }
}
