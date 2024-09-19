using System.Security.Cryptography;

namespace thehayk.secureapi.security.Helpers
{
    internal static class RandomHelper
    {
        public static int GetSecureRandomInt32(int startInclusive, int endExclusive)
        {
            return RandomNumberGenerator.GetInt32(startInclusive, endExclusive);
        }

        public static void Shuffle<T>(T[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                // get random idx which is not equal to current one
                // to swap current element with idx one
                int idx = i;
                while (idx == i)
                    idx = RandomNumberGenerator.GetInt32(0, array.Length);

                T tmp = array[i];
                array[i] = array[idx];
                array[idx] = tmp;
            }
        }
    }
}
