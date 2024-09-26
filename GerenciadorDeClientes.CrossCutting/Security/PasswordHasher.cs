using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorDeClientes.Infra.CrossCutting.Security
{
    public class PasswordCriptor
    {
        private static readonly string EncryptionKey = "G3re|\\|<14|)0r";

        // Método para criptografar o texto
        public static string Encrypt(string plainText)
        {
            using (Aes aesAlg = Aes.Create())
            {
                var key = Encoding.UTF8.GetBytes(EncryptionKey);
                Array.Resize(ref key, 32); // Garante que a chave tenha 256 bits

                aesAlg.Key = key;
                aesAlg.Mode = CipherMode.ECB; // Usando modo ECB, para garantir que o mesmo texto sempre gera o mesmo resultado (atenção à segurança desse modo)
                aesAlg.Padding = PaddingMode.PKCS7;

                using (var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, null))
                {
                    byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
                    byte[] encryptedBytes = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);
                    return Convert.ToBase64String(encryptedBytes);
                }
            }
        }

        // Método para descriptografar o texto
        public static string Decrypt(string cipherText)
        {
            using (Aes aesAlg = Aes.Create())
            {
                var key = Encoding.UTF8.GetBytes(EncryptionKey);
                Array.Resize(ref key, 32); // Garante que a chave tenha 256 bits

                aesAlg.Key = key;
                aesAlg.Mode = CipherMode.ECB;
                aesAlg.Padding = PaddingMode.PKCS7;

                using (var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, null))
                {
                    byte[] cipherBytes = Convert.FromBase64String(cipherText);
                    byte[] decryptedBytes = decryptor.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);
                    return Encoding.UTF8.GetString(decryptedBytes);
                }
            }
        }
    }
}
