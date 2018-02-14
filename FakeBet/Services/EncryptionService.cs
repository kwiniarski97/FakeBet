namespace FakeBet.Services
{
    using System.Security.Cryptography;

    public static class EncryptionService
    {
        public static byte[] GetSalt()
        {
            var salt = new byte[64];
            using (var random = new RNGCryptoServiceProvider())
            {
                random.GetNonZeroBytes(salt);
            }

            return salt;
        }
    }
}
