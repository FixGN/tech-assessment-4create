using System.Text.Json;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace _4Create.Infrastructure.Converters;

public class ObjectToJsonValueConverter : ValueConverter<object, string>
{
    public ObjectToJsonValueConverter()
        : base(
            v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null!),
            v => JsonSerializer.Deserialize<object>(v, (JsonSerializerOptions)null!)!)
    {
    }
}
