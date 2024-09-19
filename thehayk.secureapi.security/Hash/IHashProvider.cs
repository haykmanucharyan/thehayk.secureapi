using System.Security.Authentication;
using System.Text;

namespace thehayk.secureapi.security.Hash
{
    public interface IHashProvider
    {
        string[] GetAlgorithms();

        HashAlgorithmType GetHashAlgorithmType(string name);

        byte[] ComputeHash(HashAlgorithmType algorithmType, byte[] data);

        byte[] ComputeHash(HashAlgorithmType algorithmType, string data, Encoding encoding = null);
    }
}
