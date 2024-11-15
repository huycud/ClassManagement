namespace ClassManagement.Api.Common.Converter
{
    internal class DateHandler
    {
        public static DateTime UTCConverter(DateTime dateTime)
        {
            TimeZoneInfo cstZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");

            DateTime dateTimeWithTimeZone = DateTime.SpecifyKind(dateTime, DateTimeKind.Local).ToUniversalTime();

            dateTimeWithTimeZone = TimeZoneInfo.ConvertTime(dateTimeWithTimeZone, cstZone);

            return dateTimeWithTimeZone;
        }
    }
}
