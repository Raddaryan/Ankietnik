using System;
using System.Linq;
using System.Security.Cryptography;

namespace Ankietnik
{
    /// <summary>
    /// Klasa odpowiadające za szyfrowanie i weryfikację haseł oraz podpisów.
    /// </summary>
    internal static class CryptoService
    {

        /// <summary>
        /// Tworzy zaszyfrowany ciąg na podstawie wprowadzonego ciągu znaków.
        /// </summary>
        /// <returns>
        /// <c>EncryptedPassword<c> - obiekt zawierający zaszyfrowany ciąg główny oraz pośredni w postaci byte[]
        /// </returns>
        /// <param name="saltSizeInBytes">Opcjonalny. Domyślna wartość: 20</param>
        internal static EncryptedPassword EncryptPassword(string password, int saltSizeInBytes = Constants.DEFAULT_SALT_SIZE)
        {
            using (var deriveBytes = new Rfc2898DeriveBytes(password, saltSizeInBytes))
            {
                return new EncryptedPassword
                {
                    Salt = deriveBytes.Salt,
                    Key = deriveBytes.GetBytes(saltSizeInBytes)
                };
            }
        }

        /// <summary>
        /// Weryfikuje czy wprowadzony ciąg znaków po zaszyfrowaniu odpowiada szyfrowi przechowywanemu w bazie danych.
        /// </summary>
        /// <returns>
        /// Prawda lub fałsz.
        /// </returns>
        internal static bool VerifyPassword(string password, EncryptedPassword encryptedPassword)
        {
            using (var deriveBytes = new Rfc2898DeriveBytes(password, encryptedPassword.Salt))
            {
                byte[] newKey = deriveBytes.GetBytes(encryptedPassword.Salt.Length);
                return newKey.SequenceEqual(encryptedPassword.Key);
            }
        }


        /// <summary>
        /// Tworzy zaszyfrowany podpis na podstawie nazwy użytkownika i hasła.
        /// </summary>
        /// <returns>
        /// Ciąg znaków [Base64String] odpowiadający zaszyfrowanemu ciągowi głównemu.
        /// </returns>
        internal static string GenerateSignature(string username, string passcode)
        {
            var salt = AccountService.GetUser(username).Password.Salt;
            using (var deriveBytes = new Rfc2898DeriveBytes(username + passcode, salt))
            {
                return Convert.ToBase64String(deriveBytes.GetBytes(salt.Length));
            }
        }
    }
}