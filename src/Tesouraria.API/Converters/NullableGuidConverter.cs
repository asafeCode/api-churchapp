using System.Text.Json;
using System.Text.Json.Serialization;

namespace Tesouraria.API.Converters;

public class NullableGuidConverter : JsonConverter<Guid?>
{
    public override Guid? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.String) return null;
        var str = reader.GetString()?.Trim();
        
        
        if (Guid.TryParse(str, out var guid)) return guid;
        return null; 
    }

    public override void Write(Utf8JsonWriter writer, Guid? value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value?.ToString());
    }
    
}