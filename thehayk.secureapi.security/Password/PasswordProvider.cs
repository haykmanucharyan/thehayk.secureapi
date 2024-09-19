using thehayk.secureapi.security.Helpers;

namespace thehayk.secureapi.security.Password
{
    public class PasswordProvider : IPasswordProvider
    {
        public char[] GetSpecialChars()
        {
            char[] specials = new char[15];

            for (int i = 0; i < specials.Length; i++)
                specials[i] = (char)(i + 33);

            return specials;
        }

        private char[] FormAlphabet(bool uppercase, bool numbers, bool specials)
        {
            int l = 26;

            if (uppercase)
                l += 26;

            if (numbers)
                l += 10;

            if (specials)
                l += 15;

            char[] alphabet = new char[l];

            int idx = 0;

            for (idx = 0; idx < 26; idx++)
                alphabet[idx] = (char)(idx + 97);

            if (uppercase)
            {
                for (int i = 0; i < 26; i++)
                    alphabet[idx + i] = (char)(i + 65);

                idx += 26;
            }

            if (numbers)
            {
                for (int i = 0; i < 10; i++)
                    alphabet[idx + i] = (char)(i + 48);

                idx += 10;
            }

            if (specials)
            {
                for (int i = 0; i < 15; i++)
                    alphabet[idx + i] = (char)(i + 33);

                idx += 15;
            }

            return alphabet;
        }

        private string GetPassword(char[] alphabet, int length)
        {
            char[] pass = new char[length];

            for (int i = 0; i < pass.Length; i++)
                pass[i] = alphabet[RandomHelper.GetSecureRandomInt32(0, alphabet.Length)];

            return new string(pass);
        }

        public string GetPassword(int length, bool uppercase, bool numbers, bool specials)
        {
            if (length < 6 || length > 256)
                throw new ArgumentOutOfRangeException(nameof(length));

            char[] alphabet = FormAlphabet(uppercase, numbers, specials);
            RandomHelper.Shuffle(alphabet);

            return GetPassword(alphabet, length);
        }

        public string[] GetPasswords(int length, bool uppercase, bool numbers, bool specials, int quantity)
        {
            if (quantity < 2 || quantity > 1_000)
                throw new ArgumentOutOfRangeException(nameof(quantity));

            if (length < 6 || length > 256)
                throw new ArgumentOutOfRangeException(nameof(length));

            string[] passwords = new string[quantity];
            char[] alphabet = FormAlphabet(uppercase, numbers, specials);

            for (int i = 0; i < passwords.Length; i++)
            {
                RandomHelper.Shuffle(alphabet);
                passwords[i] = GetPassword(alphabet, length);
            }

            return passwords;
        }
    }
}
