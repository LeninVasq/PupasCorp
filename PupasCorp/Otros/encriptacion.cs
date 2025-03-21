using System.Security.Cryptography;
using System.Text;

namespace PupasCorp.Otros
{
    public class encriptacion
    {
        private static readonly string claveSecreta = "PUPASCORPS157PUPAS";

        public string Encrypt(string plainText)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(claveSecreta.Substring(0, 16)); // Aseguramos que la clave tenga 16 bytes
                aesAlg.IV = new byte[16]; // El vector de inicialización (IV) se puede usar con 0s para simplificar

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
                    return Convert.ToBase64String(msEncrypt.ToArray()); // Devuelve el texto encriptado en Base64
                }
            }
        }

        // Método para desencriptar el texto
        public string Decrypt(string cipherText)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(claveSecreta.Substring(0, 16)); // Aseguramos que la clave tenga 16 bytes
                aesAlg.IV = new byte[16]; // El vector de inicialización (IV) se puede usar con 0s para simplificar

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(cipherText)))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            return srDecrypt.ReadToEnd(); // Devuelve el texto desencriptado
                        }
                    }
                }
            }
        }
    }
}
