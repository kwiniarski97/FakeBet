using System.Text;

namespace FakeBet.API.Extensions
{
    public static class StringExtension
    {
        public static string RemoveAllSpaces(this string value)
        {
            var sb = new StringBuilder();
            foreach (var ch in value.ToCharArray())
            {
                if (ch != ' ')
                {
                    sb.Append(ch);
                }
            }

            return sb.ToString();
        }
    }
}