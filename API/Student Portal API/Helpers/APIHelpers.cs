using System.Security.Cryptography;
using System.Text;

namespace Student_Portal_API.Helpers
{
    public class APIHelper
    {

        public static string EncryptSHA256(string plainText)
        {

            byte[] data = Encoding.ASCII.GetBytes(plainText);
            data = new SHA256Managed().ComputeHash(data);
            string hash = Encoding.ASCII.GetString(data);
            return hash;
        }


        public static string EncryptAES(string plainText, string passPhrase)
        {
            byte[] iv = new byte[16];
            byte[] array;

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(passPhrase);
                aes.IV = iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using MemoryStream memoryStream = new();
                using CryptoStream cryptoStream = new(memoryStream, encryptor, CryptoStreamMode.Write);
                using (StreamWriter streamWriter = new(cryptoStream))
                {
                    streamWriter.Write(plainText);
                }

                array = memoryStream.ToArray();
            }

            return Convert.ToBase64String(array);
        }

        public static string DecryptAES(string cipherText, string passPhrase)
        {
            byte[] iv = new byte[16];
            byte[] buffer = Convert.FromBase64String(cipherText);

            using Aes aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(passPhrase);
            aes.IV = iv;
            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

            using MemoryStream memoryStream = new(buffer);
            using CryptoStream cryptoStream = new(memoryStream, decryptor, CryptoStreamMode.Read);
            using StreamReader streamReader = new(cryptoStream);
            return streamReader.ReadToEnd();
        }

        public static string ToBase64(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return null;
            }

            var bytes = Encoding.UTF8.GetBytes(text);
            return Convert.ToBase64String(bytes);
        }

        public static string FromBase64(string text64)
        {
            if (string.IsNullOrEmpty(text64))
            {
                return null;
            }

            var bytes = Convert.FromBase64String(text64);
            return Encoding.UTF8.GetString(bytes);
        }

        public static string GetComplexMessage(int passwordLen)
        {
            return $"<div style='text-align: left; list-style-position: inside'><ul>" +
                    $"<p>Password provided is not complex enough</p>" +
                    $"<p>Requirements:<p>" +
                    $"<li>Do not have blank space;</li>" +
                    $"<li>Minimum size: {passwordLen} characters;</li>" +
                    $"<li>have number;</li>" +
                    $"<li>have handwriting;</li>" +
                    $"<li>have special character;</li>" +
                    $"</ul></div>";
        }

    }
}
