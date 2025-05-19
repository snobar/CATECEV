using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.WebUtilities;


namespace CATECEV.FE.Extensions
{
    public static class ShortEncryptor
    {
        private static readonly string Key = "ThisIsA32ByteLongSecretKey123456"; // 32 chars = 32 bytes
        private static readonly string IV = "ThisIsA16ByteIV!";                 // 16 chars = 16 bytes


        public static string Encrypt(int id)
        {
            using var aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(Key);
            aes.IV = Encoding.UTF8.GetBytes(IV);
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            using var encryptor = aes.CreateEncryptor();
            var bytes = Encoding.UTF8.GetBytes(id.ToString());

            var encrypted = encryptor.TransformFinalBlock(bytes, 0, bytes.Length);
            return WebEncoders.Base64UrlEncode(encrypted); // Short + URL-safe
        }

        public static int Decrypt(string encryptedId)
        {
            var encryptedBytes = WebEncoders.Base64UrlDecode(encryptedId);

            using var aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(Key);
            aes.IV = Encoding.UTF8.GetBytes(IV);
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            using var decryptor = aes.CreateDecryptor();
            var decryptedBytes = decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);
            var decrypted = Encoding.UTF8.GetString(decryptedBytes);

            return int.Parse(decrypted);
        }
    }


}
