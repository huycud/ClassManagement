using System.Globalization;
using static Utilities.Enums.EnumTypes;

namespace Utilities.Handlers
{
    public static class StringConverter
    {
        public static string Convert(string str)
        {
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;

            string convertedStr = textInfo.ToTitleCase(str);

            return convertedStr;
        }

        public static string Convert(string str, StringMode mode)
        {
            string stringConvert = mode switch
            {
                StringMode.UpperFirstCharInWord => Convert(str),

                StringMode.UpperFirstCharInSentence => char.ToUpper(str[0]) + str[1..],

                _ => str,
            };

            return stringConvert;
        }
    }
}
