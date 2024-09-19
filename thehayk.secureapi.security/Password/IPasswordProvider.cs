namespace thehayk.secureapi.security.Password
{
    public interface IPasswordProvider
    {
        char[] GetSpecialChars();

        string GetPassword(int length, bool uppercase, bool numbers, bool specials);

        string[] GetPasswords(int length, bool uppercase, bool numbers, bool specials, int quantity);
    }
}
