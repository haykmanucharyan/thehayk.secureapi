using System.Security.Authentication;
using System.Security.Cryptography;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace thehayk.secureapi.security.Hash
{
    public class HashProvider : IHashProvider
    {
        public string[] GetAlgorithms()
        {
            HashAlgorithmType[] enums = Enum.GetValues<HashAlgorithmType>();

            string[] names = new string[enums.Length - 1];

            for(int i = 1; i < enums.Length; i++)
                names[i - 1] = enums[i].ToString();

            return names;
        }

        private HashAlgorithm GetAlgorithm(HashAlgorithmType algorithmType)
        {
            switch (algorithmType)
            {
                case HashAlgorithmType.Md5:
                    return MD5.Create();

                case HashAlgorithmType.Sha1:
                    return SHA1.Create();

                case HashAlgorithmType.Sha256:
                    return SHA256.Create();

                case HashAlgorithmType.Sha384:
                    return SHA384.Create();

                default:
                    return SHA512.Create();
            }
        }

        public HashAlgorithmType GetHashAlgorithmType(string algorithm)
        {
            if (!Enum.TryParse(algorithm, out HashAlgorithmType hashAlgorithmType))
                throw new ArgumentException(nameof(algorithm));

            return hashAlgorithmType;
        }

        public byte[] ComputeHash(HashAlgorithmType algorithmType, byte[] data)
        {
            if (data == null || data.Length < 1 || data.Length > 8192)
                throw new ArgumentNullException(nameof(data));

            using (HashAlgorithm algorithm = GetAlgorithm(algorithmType))
                return algorithm.ComputeHash(data);
        }

        public byte[] ComputeHash(HashAlgorithmType algorithmType, string data, Encoding encoding = null)
        {
            if (encoding == null)
                encoding = Encoding.ASCII;

            return ComputeHash(algorithmType, encoding.GetBytes(data));
        }
    }
}
