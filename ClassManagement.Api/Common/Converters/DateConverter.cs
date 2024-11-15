using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
using Utilities.Common;

namespace ClassManagement.Api.Common.Converter
{
    public class DateConverter : JsonConverter<DateTime>
    {
        private string formatDate = SystemConstants.FORMAT_STRING;

        DateTime dateOfBirth;

        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string jsonString = reader.GetString();

            if (!string.IsNullOrEmpty(jsonString) && DateTime.TryParseExact(jsonString, formatDate, CultureInfo.GetCultureInfo("vi-VN"), DateTimeStyles.None, out dateOfBirth))
            {
                return dateOfBirth;
            }

            DateTime.TryParseExact(jsonString, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateOfBirth);

            return dateOfBirth;
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(formatDate));
        }
    }
}
