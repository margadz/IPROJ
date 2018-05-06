using System;
using System.Linq;
using System.Text;

namespace IPROJ.ConnectionBroker.Devices.HS110
{
    public static class HS110Coding
    {
        public static byte[] Encrypt(string payload, bool hasHeader = true)
        {
            byte key = 0xAB;
            byte[] cipherBytes = new byte[payload.Length];
            byte[] header = hasHeader ? BitConverter.GetBytes(ReverseBytes((uint)payload.Length)) : new byte[] { };
            for (var i = 0; i < payload.Length; i++)
            {
                cipherBytes[i] = Convert.ToByte(payload[i] ^ key);
                key = cipherBytes[i];
            }

            return header.Concat(cipherBytes).ToArray();
        }

        public static string Decrypt(byte[] cipher, bool hasHeader = true)
        {
            byte key = 0xAB;
            byte nextKey;
            if (hasHeader)
            {
                cipher = cipher.Skip(4).ToArray();
            }

            byte[] result = new byte[cipher.Length];

            for (int i = 0; i < cipher.Length; i++)
            {
                nextKey = cipher[i];
                result[i] = (byte)(cipher[i] ^ key);
                key = nextKey;
            }

            return Encoding.UTF7.GetString(result);
        }

        private static UInt32 ReverseBytes(uint value)
        {
            return (value & 0x000000FFU) << 24 | (value & 0x0000FF00U) << 8 |
                   (value & 0x00FF0000U) >> 8 | (value & 0xFF000000U) >> 24;
        }
    }
}
