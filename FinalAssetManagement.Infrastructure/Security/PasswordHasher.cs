using FinalAssetManagement.Application.Common.Interfaces;
using System.Security.Cryptography;

namespace FinalAssetManagement.Infrastructure.Security
{
    public class PasswordHasher : IPasswordHasher
    {
        private const int SaltSize = 16; // 128-bit
        private const int KeySize = 32;  // 256-bit
        private const int Iterations = 100_000;

        //---------------------------------------------------------------------------------------

        // خروجی: "Base64Hash:Base64Salt"
        public string Hash(string password)
        {
            if (password is null)
                throw new ArgumentNullException(nameof(password));

            byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);

            byte[] key = Rfc2898DeriveBytes.Pbkdf2(
                password: password,
                salt: salt,
                iterations: Iterations,
                hashAlgorithm: HashAlgorithmName.SHA256,
                outputLength: KeySize);

            string hash = Convert.ToBase64String(key);
            string saltText = Convert.ToBase64String(salt);

            return $"{hash}:{saltText}";
        }

        //---------------------------------------------------------------------------------------
        public bool Verify(string password, string storedPasswordHash)
        {
            if (password is null)
                throw new ArgumentNullException(nameof(password));

            if (storedPasswordHash is null)
                throw new ArgumentNullException(nameof(storedPasswordHash));

            string[] parts = storedPasswordHash.Split(':', 2);

            if (parts.Length != 2)
                return false;

            string storedHash = parts[0];
            string storedSalt = parts[1];

            byte[] salt = Convert.FromBase64String(storedSalt);
            byte[] expected = Convert.FromBase64String(storedHash);

            byte[] actual = Rfc2898DeriveBytes.Pbkdf2(
                password: password,
                salt: salt,
                iterations: Iterations,
                hashAlgorithm: HashAlgorithmName.SHA256,
                outputLength: expected.Length);

            return CryptographicOperations.FixedTimeEquals(actual, expected);
        }

        //---------------------------------------------------------------------------------------
    }
}
