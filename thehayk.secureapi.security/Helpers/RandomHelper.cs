using System.Security.Cryptography;

namespace thehayk.secureapi.security.Helpers
{
    internal static class RandomHelper
    {
        public static int GetSecureRandomInt32(int startInclusive, int endExclusive)
        {
            return RandomNumberGenerator.GetInt32(startInclusive, endExclusive);
        }

        public static int[] GetSecureRandomInt32s(int startInclusive, int endExclusive, int quantity)
        {
            if(quantity < 2 || quantity > 5_000)
                throw new ArgumentOutOfRangeException(nameof(quantity));

            int[] nums = new int[quantity];

            for (int i = 0; i < nums.Length; i++)
                nums[i] = GetSecureRandomInt32(startInclusive, endExclusive);

            return nums;
        }

        public static byte[] GetBytes(int quantity)
        {
            if (quantity < 1 || quantity > 500_000)
                throw new ArgumentOutOfRangeException(nameof(quantity));

            return RandomNumberGenerator.GetBytes(quantity);
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
