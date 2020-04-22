using System;
using System.Security.Cryptography;
using System.Text;

namespace Utilities
{
    /// <summary>
    /// Class help for encrypting/decrypting.
    /// </summary>
    public class CryptographyHelper
    {
        public const string DEFAULT_CRYPTOGRAPHY_HELPER_KEY = "{A3B47417-3869-41E7-84E0-CC65027ABF6D}";

        private static Random random = new Random((int)DateTime.Now.Ticks);

        #region Encrypt
        public static string EncryptUsingSymmetricAlgorithm(string plainText)
        {
            return EncryptUsingSymmetricAlgorithm(plainText, DEFAULT_CRYPTOGRAPHY_HELPER_KEY);
        }

        public static string EncryptUsingSymmetricAlgorithm(string plainText, string key)
        {
            var des = CreateTripleDes(key);
            var ct = des.CreateEncryptor();
            var input = Encoding.UTF8.GetBytes(plainText);

            return Convert.ToBase64String(ct.TransformFinalBlock(input, 0, input.Length));
        }

        public static string EncryptDatabasePasswordUsingSymmetricAlgorithm(string plainText)
        {
            return EncryptDatabasePasswordUsingSymmetricAlgorithm(plainText,
                                                                  DEFAULT_CRYPTOGRAPHY_HELPER_KEY);
        }

        public static string EncryptDatabasePasswordUsingSymmetricAlgorithm(string plainText, string key)
        {
            var des = CreateTripleDes(key);
            var ct = des.CreateEncryptor();
            var input = Encoding.UTF32.GetBytes(plainText);

            var byteValues = ct.TransformFinalBlock(input, 0, input.Length);
            StringBuilder returnValue = new StringBuilder();

            foreach (var b in byteValues)
            {
                returnValue.Append(b.ToString());
                returnValue.Append(" ");
            }

            return returnValue.ToString();
        }
        #endregion

        #region Decrypt
        public static string DecryptUsingSymmetricAlgorithm(string encryptedText)
        {
            return DecryptUsingSymmetricAlgorithm(encryptedText, DEFAULT_CRYPTOGRAPHY_HELPER_KEY);
        }

        public static string DecryptUsingSymmetricAlgorithm(string encryptedText, string key)
        {
            var des = CreateTripleDes(key);
            var bytes = Convert.FromBase64String(encryptedText);
            var ct = des.CreateDecryptor();
            var output = ct.TransformFinalBlock(bytes, 0, bytes.Length);

            return Encoding.UTF8.GetString(output);
        }

        public static string DecryptDatabasePasswordUsingSymmetricAlgorithm(string encryptedText)
        {
            return DecryptDatabasePasswordUsingSymmetricAlgorithm(encryptedText,
                                                                  DEFAULT_CRYPTOGRAPHY_HELPER_KEY);
        }

        public static string DecryptDatabasePasswordUsingSymmetricAlgorithm(string encryptedText, string key)
        {
            var des = CreateTripleDes(key);
            string[] stringByte = encryptedText.Split(' ');
            byte[] bytes = new byte[stringByte.Length];

            for (int i = 0; i < stringByte.Length; i++)
            {
                bytes[i] = Convert.ToByte(stringByte[i], 10);
            }

            var ct = des.CreateDecryptor();
            var output = ct.TransformFinalBlock(bytes, 0, bytes.Length);

            return Encoding.UTF32.GetString(output);
        }
        #endregion

        #region Others
        public static string GenerateHash(string userName, string password)
        {
            var passwordByte = Encoding.UTF8.GetBytes(userName.ToLower() + password);
            var sha1 = new SHA1CryptoServiceProvider();
            var passwordHash = sha1.ComputeHash(passwordByte);

            //Convert byte[] to hexa string format
            var hex = BitConverter.ToString(passwordHash);
            hex = hex.Replace("-", "");

            return hex;
        }

        public static string GenerateFileHash(byte[] fileBinaries)
        {
            var sha1 = new SHA1CryptoServiceProvider();
            var fileHash = sha1.ComputeHash(fileBinaries);

            //Convert byte[] to hexa string format
            var hex = BitConverter.ToString(fileHash);
            hex = hex.Replace("-", "");

            return hex;
        }

        public static string RandomString(int size)
        {
            StringBuilder builder = new StringBuilder();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }

            return builder.ToString();
        }

        public static byte[] StringToByteArray(String hex)
        {
            int NumberChars = hex.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return bytes;
        }
        #endregion

        #region Private methods
        private static TripleDES CreateTripleDes(string key)
        {
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }

            PasswordDeriveBytes pwdGenerator = new PasswordDeriveBytes(key, null);
            byte[] keyForAlgorithm = pwdGenerator.GetBytes(16);
            byte[] initVec = pwdGenerator.GetBytes(8);
            TripleDES des = new TripleDESCryptoServiceProvider();
            des.Key = keyForAlgorithm;
            des.IV = initVec;

            return des;
        }

        private static byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        private static string GetString(byte[] bytes)
        {
            char[] chars = new char[bytes.Length / sizeof(char)];
            System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }
        #endregion
    }
}
