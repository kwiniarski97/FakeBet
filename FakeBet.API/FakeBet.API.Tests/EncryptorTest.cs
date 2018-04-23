using FakeBet.API.Helpers;
using Xunit;

namespace FakeBet.API.Tests
{
    //decoded strings taken from https://www.base64encode.org/
    public class EncryptorTest
    {
        [Fact]
        public void Can_Encrypt()
        {
            var value = "helloworld";
            string encryptedvalue;
            var expected = "aGVsbG93b3JsZA==";

            encryptedvalue = Encryptor.ToBase64(value);

            Assert.Equal(encryptedvalue, encryptedvalue);
        }

        [Fact]
        public void Can_Decrypt()
        {
            var value = "aGVsbG93b3JsZA==";
            string decryptedvalue;
            var expected = "helloworld";

            decryptedvalue = Encryptor.FromBase64(value);
            
            Assert.Equal(expected, decryptedvalue);
        }
    }
}