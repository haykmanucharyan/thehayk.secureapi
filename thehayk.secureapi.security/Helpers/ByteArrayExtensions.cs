using System.Text;

namespace thehayk.secureapi.security.Helpers
{
    public static class ByteArrayExtensions
    {
        #region Hex

        public static string ToHexString(this byte[] array)
        {
            StringBuilder sb = new StringBuilder(array.Length * 2);

            foreach (byte b in array)
                sb.AppendFormat("{0:x2}", b);

            return sb.ToString();
        }

        public static byte[] FromHexString(this string data)
        {
            byte[] bytes = new byte[data.Length / 2];

            for (int i = 0; i < data.Length; i += 2)
                bytes[i / 2] = Convert.ToByte(data.Substring(i, 2), 16);

            return bytes;
        }

        #endregion

        #region Int16

        public static byte[] ToByteArray(this short num)
        {
            byte[] array = new byte[2];

            for (int i = 0; i < array.Length; i++)
                array[i] = (byte)(num >> i * 8);

            return array;
        }

        public static short ToInt16(this byte[] array)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));

            if (array.Length != 2)
                throw new ArgumentOutOfRangeException(nameof(array));

            short num = 0;
            
            for (int i = 0; i < 2; i++)
                num |= (short)(array[i] << i * 8);

            return num;
        }

        #endregion

        #region UInt16

        public static byte[] ToByteArray(this ushort num)
        {
            byte[] array = new byte[2];

            for (int i = 0; i < array.Length; i++)
                array[i] = (byte)(num >> i * 8);

            return array;
        }

        public static ushort ToUInt16(this byte[] array)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));

            if (array.Length != 2)
                throw new ArgumentOutOfRangeException(nameof(array));

            ushort num = 0;

            for (int i = 0; i < 2; i++)
                num |= (ushort)(array[i] << i * 8);

            return num;
        }

        #endregion

        #region Int32

        public static byte[] ToByteArray(this int num)
        {
            byte[] array = new byte[4];

            for (int i = 0; i < array.Length; i++)
                array[i] = (byte)(num >> i * 8);

            return array;
        }

        public static int ToInt32(this byte[] array)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));

            if(array.Length != 4)
                throw new ArgumentOutOfRangeException(nameof(array));

            int num = 0;

            for (int i = 0; i < 4; i++)
                num |= array[i] << i * 8;

            return num;
        }

        #endregion

        #region UInt32

        public static byte[] ToByteArray(this uint num)
        {
            byte[] array = new byte[4];

            for (int i = 0; i < array.Length; i++)
                array[i] = (byte)(num >> i * 8);

            return array;
        }

        public static uint ToUInt32(this byte[] array)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));

            if (array.Length != 4)
                throw new ArgumentOutOfRangeException(nameof(array));

            uint num = 0;

            for (int i = 0; i < 4; i++)
                num |= (uint)array[i] << i * 8;

            return num;
        }

        #endregion

        #region Int64

        public static byte[] ToByteArray(this long num)
        {
            byte[] array = new byte[8];

            for (int i = 0; i < array.Length; i++)
                array[i] = (byte)(num >> i * 8);

            return array;
        }

        public static long ToInt64(this byte[] array)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));

            if (array.Length != 8)
                throw new ArgumentOutOfRangeException(nameof(array));

            long num = 0;

            for (int i = 0; i < 8; i++)
                num |= (long)array[i] << i * 8;

            return num;
        }

        #endregion

        #region UInt64

        public static byte[] ToByteArray(this ulong num)
        {
            byte[] array = new byte[8];

            for (int i = 0; i < array.Length; i++)
                array[i] = (byte)(num >> i * 8);

            return array;
        }

        public static ulong ToUInt64(this byte[] array)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));

            if (array.Length != 8)
                throw new ArgumentOutOfRangeException(nameof(array));

            ulong num = 0;

            for (int i = 0; i < 8; i++)
                num |= (ulong)array[i] << i * 8;

            return num;
        }

        #endregion
    }
}