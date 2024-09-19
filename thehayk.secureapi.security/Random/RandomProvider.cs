using thehayk.secureapi.security.Helpers;

namespace thehayk.secureapi.security.Random
{
    public class RandomProvider : IRandomProvider
    {
        public byte[] GetBytes(int quantity)
        {
            if(quantity < 1 || quantity > 10_000)
                throw new ArgumentOutOfRangeException(nameof(quantity));

            return RandomHelper.GetBytes(quantity);
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
