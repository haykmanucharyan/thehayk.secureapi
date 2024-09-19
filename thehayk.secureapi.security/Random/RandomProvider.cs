using System.Text;
using thehayk.secureapi.security.Helpers;

namespace thehayk.secureapi.security.Random
{
    public class RandomProvider : IRandomProvider
    {
        public string GetBytes(int quantity)
        {
            if(quantity < 1 || quantity > 10_000)
                throw new ArgumentOutOfRangeException(nameof(quantity));

            byte[] array = RandomHelper.GetBytes(quantity);

            StringBuilder sb = new StringBuilder(array.Length * 2);

            foreach (byte b in array)
                sb.AppendFormat("{0:x2}", b);

            return sb.ToString();
        }

        public int GetInt32(int startInclusive, int endExclusive)
        {
            return RandomHelper.GetSecureRandomInt32(startInclusive, endExclusive);
        }

        public int[] GetListOfInt32(int startInclusive, int endExclusive, int quantity)
        {
            return RandomHelper.GetSecureRandomInt32s(startInclusive, endExclusive, quantity);
        }

        public Guid Uid()
        {
            return Guid.NewGuid();
        }

        public Guid[] Uids(int quantity)
        {
            if (quantity < 2 || quantity > 1000)
                throw new ArgumentOutOfRangeException(nameof(quantity));

            Guid[] guids = new Guid[quantity];

            for (int i = 0; i < guids.Length; i++)
                guids[i] = Uid();

            return guids;
        }
    }
}
