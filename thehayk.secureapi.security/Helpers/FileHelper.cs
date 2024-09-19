using System.Text;

namespace thehayk.secureapi.security.Helpers
{
    internal static class FileHelper
    {
        public static List<string> ReadWords(string filePath, params char[] separators)
        {
            if (string.IsNullOrEmpty(filePath))
                throw new ArgumentNullException(filePath);

            if (!File.Exists(filePath))
                throw new FileNotFoundException(filePath);

            if (separators == null || separators.Length == 0)
                throw new ArgumentNullException(nameof(separators));

            HashSet<char> set = new HashSet<char>(separators);

            List<string> words = new List<string>();

            using (StreamReader sr = new StreamReader(filePath))
            {
                StringBuilder sb = new StringBuilder();

                while (!sr.EndOfStream)
                {
                    char ch = (char)sr.Read();

                    if (set.Contains(ch))
                    {
                        string word = sb.ToString();

                        if (!string.IsNullOrEmpty(word))
                            words.Add(sb.ToString().ToLower());

                        sb.Clear();
                    }
                    else
                        sb.Append(ch);
                }
            }

            return words;
        }
    }
}
