using System;
using System.Text.Json;
using System.Text.Json.Serialization;

public class DateTimeConverter : JsonConverter<DateTime>
{
    private static readonly TimeZoneInfo BrasiliaTimeZone = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");

    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var dateTimeString = reader.GetString();
        var localDateTime = DateTime.Parse(dateTimeString);
        return TimeZoneInfo.ConvertTime(localDateTime, BrasiliaTimeZone);
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        DateTime brasiliaDateTime;

        if (value.Kind == DateTimeKind.Unspecified || value.Kind == DateTimeKind.Local)
        {
            brasiliaDateTime = TimeZoneInfo.ConvertTime(value, TimeZoneInfo.Local, BrasiliaTimeZone);
        }
        else
        {
            brasiliaDateTime = TimeZoneInfo.ConvertTimeFromUtc(value, BrasiliaTimeZone);
        }

        writer.WriteStringValue(brasiliaDateTime.ToString("yyyy-MM-ddTHH:mm:ss.fffK"));
    }

}
