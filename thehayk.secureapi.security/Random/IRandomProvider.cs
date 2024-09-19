namespace thehayk.secureapi.security.Random
{
    public interface IRandomProvider
    {
        int GetInt32(int startInclusive, int endExclusive);

        int[] GetListOfInt32(int startInclusive, int endExclusive, int quantity);

        string GetBytes(int quantity);

        Guid Uid();

        Guid[] Uids(int quantity);
    }
}
