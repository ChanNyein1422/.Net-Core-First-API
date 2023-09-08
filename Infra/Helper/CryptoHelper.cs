using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public class CryptoHelper
{
    // Encryption Key (must be the same as in your Flutter code)
    private static readonly string EncryptionKey = "f2gG0Aumgs5DH2l3rsmkl7qy4no9N7o1";

    // IV (Initialization Vector) - same as in Flutter code
    private static readonly string IV = "dEhzWnNBOTViWjFWc2JHOQ==";

    // Encrypts a plaintext string and returns the Base64-encoded ciphertext
    public static string Encrypt(string plainText)
    {
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = Encoding.UTF8.GetBytes(EncryptionKey);
            aesAlg.IV = Convert.FromBase64String(IV);
            aesAlg.Mode = CipherMode.CBC;

            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(plainText);
                    }
                }
                //return Convert.ToBase64String(msEncrypt.ToArray());
                return UrlTokenEncode(msEncrypt.ToArray()); 
            }
        }
    }

    // Decrypts a Base64-encoded ciphertext and returns the plaintext string
    public static string Decrypt(string cipherText)
    {
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = Encoding.UTF8.GetBytes(EncryptionKey);
            aesAlg.IV = Convert.FromBase64String(IV);
            aesAlg.Mode = CipherMode.CBC;

            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

            using (MemoryStream msDecrypt = new MemoryStream(UrlTokenDecode(cipherText)))
            {
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                    {
                        return srDecrypt.ReadToEnd();
                    }
                }
            }
        }
    }
    public static byte[] UrlTokenDecode(string input)
    {
        var padding = 0;
        if ((input.Length % 4) == 2) padding = 2;
        else if ((input.Length % 4) == 3) padding = 1;
        var base64 = input.Replace("_", "/").Replace("-", "+") + new string('=', padding);
        return Convert.FromBase64String(base64);
    }

    public static string UrlTokenEncode(byte[] input)
    {
        var base64 = Convert.ToBase64String(input);
        return base64.Replace("/", "_").Replace("+", "-").Replace("=", "");
    }
}