using System.Globalization;
using System.Text;

namespace Utilities.Handlers
{
    public static class Normalize
    {
        public static string NormalizeString(string str)
        {
            var normalizedString = str.Normalize(NormalizationForm.FormD);

            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().ToLowerInvariant();
        }

        public static string RemoveNonAlphanumeric(string str)
        {
            var stringBuilder = new StringBuilder();

            foreach (var c in str)
            {
                if (char.IsLetterOrDigit(c))
                {
                    stringBuilder.Append(c);
                }
                else if (c == ' ')
                {
                    stringBuilder.Append('-');
                }
            }

            return stringBuilder.ToString();
        }
    }
}
