using System;
using System.Linq;
using System.Security.Cryptography;

namespace Ankietnik
{
    internal static class CryptoService
    {
        
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

        internal static bool VerifyPassword(string password, EncryptedPassword encryptedPassword)
        {
            using (var deriveBytes = new Rfc2898DeriveBytes(password, encryptedPassword.Salt))
            {
                byte[] newKey = deriveBytes.GetBytes(encryptedPassword.Salt.Length);
                return newKey.SequenceEqual(encryptedPassword.Key);
            }
        }

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