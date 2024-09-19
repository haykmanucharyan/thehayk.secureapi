using System.Text;

namespace thehayk.secureapi.security.Helpers
{
    public static class ByteArrayHelper
    {
        public static string ToHexString(byte[] array)
        {
            StringBuilder sb = new StringBuilder(array.Length * 2);

            foreach (byte b in array)
                sb.AppendFormat("{0:x2}", b);

            return sb.ToString();
        }

        public static byte[] FromHexString(string data)
        {
            byte[] bytes = new byte[data.Length / 2];

            for (int i = 0; i < data.Length; i += 2)
                bytes[i / 2] = Convert.ToByte(data.Substring(i, 2), 16);

            return bytes;
        }
    }
}
