using System.Text;

namespace FakeBet.API.Extensions
{
    public static class StringExtension
    {
        public static string RemoveAllSpaces(this string value)
        {
            if (value == null)
            {
                return null;
            }

            var sb = new StringBuilder();
            foreach (var ch in value.ToCharArray())
            {
                if (ch == ' ') continue;
                sb.Append(ch);
            }

            return sb.ToString();
        }
    }
}