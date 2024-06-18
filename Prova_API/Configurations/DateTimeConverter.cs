using System.Text.Json;
using System.Text.Json.Serialization;

public class DateTimeConverter : JsonConverter<DateTime>
{
    private static readonly TimeZoneInfo BrasiliaTimeZone = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");

    public static DateTime ConvertToBrasiliaTimeZone(DateTime dateTime)
    {
        if (dateTime.Kind == DateTimeKind.Unspecified || dateTime.Kind == DateTimeKind.Local)
        {
            return TimeZoneInfo.ConvertTime(dateTime, TimeZoneInfo.Local, BrasiliaTimeZone);
        }
        else
        {
            return TimeZoneInfo.ConvertTimeFromUtc(dateTime, BrasiliaTimeZone);
        }
    }

    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var dateTimeString = reader.GetString();
        var localDateTime = DateTime.Parse(dateTimeString);
        return ConvertToBrasiliaTimeZone(localDateTime);
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        var brasiliaDateTime = ConvertToBrasiliaTimeZone(value);
        writer.WriteStringValue(brasiliaDateTime.ToString("yyyy-MM-ddTHH:mm:ss.fffK"));
    }
}

