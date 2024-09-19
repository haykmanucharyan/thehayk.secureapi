using System.Text;
using thehayk.secureapi.security.Helpers;

namespace thehayk.secureapi.security.Dice
{
    public class DiceDictionary : IDiceDictionary
    {
        #region Fields

        Dictionary<string, string> _wordsMap;
        const string _base6 = "012345";
        int _length;

        #endregion

        #region Internal

        private string Int32ToBase6String(int value)
        {
            string result = string.Empty;
            do
            {
                result = _base6[value % 6] + result;
                value /= 6;
            }
            while (value > 0);

            return result;
        }

        private void InitEnumeratedMap(List<string> words)
        {
            // get the length of dice numbers
            _length = (int)Math.Floor(Math.Log(words.Count, 6));

            _wordsMap = new Dictionary<string, string>();

            for (int i = 0; i < words.Count; i++)
            {
                string n = Int32ToBase6String(i);

                if (n.Length < _length)
                {
                    int q = _length - n.Length;
                    for (int j = 0; j < q; j++)
                        n = "0" + n;
                }

                _wordsMap.Add(n, words[i]);
            }
        }

        private static string GetDiceNumber(int digitsLength)
        {
            StringBuilder sb = new StringBuilder(digitsLength);

            for (int i = 0; i < digitsLength; i++)
                sb.Append(RandomHelper.GetSecureRandomInt32(0, 6).ToString());

            return sb.ToString();
        }

        private static string[] GetDiceNumbers(int digitsLength, int quantity)
        {
            string[] nums = new string[quantity];

            for (int i = 0; i < quantity; i++)
                nums[i] = GetDiceNumber(digitsLength);

            return nums;
        }

        public static string[] GetRandomDiceNumbers(int digitsLength, int quantity)
        {
            string[] nums = GetDiceNumbers(digitsLength, quantity);

            RandomHelper.Shuffle(nums);

            return nums;
        }

        #endregion

        #region Interface implementation

        public void Init(string dictFilePath, params char[] separators)
        {
            List<string> words = FileHelper.ReadWords(dictFilePath, separators);

            InitEnumeratedMap(words);
        }

        public string GetPassword(int wordsQuantity, string separator)
        {
            if (wordsQuantity < 1 || wordsQuantity > 50)
                throw new ArgumentOutOfRangeException(nameof(wordsQuantity));

            if (string.IsNullOrEmpty(separator))
                separator = " ";

            string[] nums = GetRandomDiceNumbers(_length, wordsQuantity);

            for (int i = 0; i < nums.Length; i++)
                nums[i] = _wordsMap[nums[i]];

            return string.Join(separator, nums);
        }

        public string[] GetPasswords(int wordsQuantity, string separator, int quantity)
        {
            if (quantity < 2 || quantity > 100)
                throw new ArgumentOutOfRangeException(nameof(quantity));

            if (wordsQuantity < 1 || wordsQuantity > 50)
                throw new ArgumentOutOfRangeException(nameof(wordsQuantity));

            if (string.IsNullOrEmpty(separator))
                separator = " ";

            string[] passes = new string[quantity];

            for (int i = 0; i < quantity; i++)
                passes[i] = GetPassword(wordsQuantity, separator);

            return passes;
        }

        #endregion
    }
}
