using System;
using System.Text;

namespace FakeBet.API.Helpers
{
    public static class Encryptor
    {
        public static string ToBase64(string unencodedValue)
        {
            return unencodedValue == null ? null : Convert.ToBase64String(Encoding.UTF8.GetBytes(unencodedValue));
        }

        public static string FromBase64(string encodedValue)
        {
            return encodedValue == null ? null : Encoding.UTF8.GetString(Convert.FromBase64String(encodedValue));
        }
    }
}