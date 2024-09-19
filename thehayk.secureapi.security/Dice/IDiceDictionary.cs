namespace thehayk.secureapi.security.Dice
{
    public interface IDiceDictionary
    {
        void Init(string dictFilePath, params char[] separators);

        string GetPassword(int wordsQuantity, string separator);

        string[] GetPasswords(int wordsQuantity, string separator, int quantity);
    }
}
