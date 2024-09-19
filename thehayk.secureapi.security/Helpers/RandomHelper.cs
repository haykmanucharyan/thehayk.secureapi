using System.Security.Cryptography;
using System.Text;

namespace thehayk.secureapi.security.Helpers
{
    internal static class RandomHelper
    {
        private static string GetDiceNumber(int digitsLength)
        {
            StringBuilder sb = new StringBuilder(digitsLength);

            for (int i = 0; i < digitsLength; i++)
                sb.Append(RandomNumberGenerator.GetInt32(0, 6).ToString());

            return sb.ToString();
        }

        private static string[] GetDiceNumbers(int digitsLength, int quantity)
        {
            string[] nums = new string[quantity];

            for (int i = 0; i < quantity; i++)
                nums[i] = GetDiceNumber(digitsLength);

            return nums;
        }

        private static void Shuffle<T>(T[] array)
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

        public static string[] GetRandomDiceNumbers(int digitsLength, int quantity)
        {
            string[] nums = GetDiceNumbers(digitsLength, quantity);

            Shuffle(nums);

            return nums;
        }
    }
}
