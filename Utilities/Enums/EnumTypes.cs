using System.Text.Json.Serialization;

namespace Utilities.Enums
{
    public static class EnumTypes
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum SortOrder
        {
            AscendingId,

            DescendingId,

            AscendingName,

            DescendingName
        }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum ClassPeriod
        {
            First,

            Second,

            Third,

            Fourth,

            Fifth,

            Sixth,

            Seventh,

            Eighth,

            Ninth,

            Tenth
        }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum Gender
        {
            Male,

            Female
        }

        public enum StringMode
        {
            UpperFirstCharInWord,

            UpperFirstCharInSentence
        }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum Status
        {
            Opening,

            Closed
        }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum ClassType
        {
            Theory,

            Practice,
        }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum NotifyType
        {
            System,

            Class,

            ClassRoom
        }

        public enum FileExtension
        {
            None,

            Jpg,

            Jpeg,

            Png,

            Zip,

            Rar
        }

        public enum FileType
        {
            Image,

            Compression
        }
    }
}