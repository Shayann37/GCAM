using System.Security.Cryptography;

namespace GCAM_Common
{
    internal static class Encryption
    {
        internal static byte[] SHA512SignatureWithSalt(byte[] salt, byte[] data)
        {
            using (SHA512 sha512 = SHA512.Create())
            {
                byte[] saltData = data.Concat(salt).ToArray();
                return sha512.ComputeHash(saltData);
            }
        }
    }
}
