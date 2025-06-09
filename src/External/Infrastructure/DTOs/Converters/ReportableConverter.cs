namespace Infrastructure.DTOs.Converters;

using System.Text.Json;
using System.Text.Json.Serialization;

public class ReportableConverter : JsonConverter<ReportableDto>
{
    public override ReportableDto Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using var jsonDoc = JsonDocument.ParseValue(ref reader);
        var jsonObject = jsonDoc.RootElement;

        // Trzeba znaleźć reportable_type z wyższego poziomu -> obejście:
        // W twoim kodzie: masz w ReportDto obok to pole, więc w deserializacji ReportDto będzie custom logika

        throw new NotSupportedException("Nie używaj tego konwertera bezpośrednio - potrzebny jest dostęp do reportable_type z wyższego poziomu");
    }

    public override void Write(Utf8JsonWriter writer, ReportableDto value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, (object)value, value.GetType(), options);
    }
}