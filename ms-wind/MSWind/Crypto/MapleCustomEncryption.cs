﻿namespace MSWind.Crypto
{
    /// <summary>
    /// Class to handle the MapleStory Custom Encryption routines
    /// </summary>
    public class MapleCustomEncryption
    {

        /// <summary>
        /// Encrypt data using MapleStory's Custom Encryption
        /// </summary>
        /// <param name="data">data to encrypt</param>
        /// <returns>Encrypted data</returns>
        public static void Encrypt(byte[] data)
        {
            int size = data.Length;
            int j;
            byte a, c;
            for (int i = 0; i < 3; i++)
            {
                a = 0;
                for (j = size; j > 0; j--)
                {
                    c = data[size - j];
                    c = rol(c, 3);
                    c = (byte)(c + j);
                    c ^= a;
                    a = c;
                    c = ror(a, j);
                    c ^= 0xFF;
                    c += 0x48;
                    data[size - j] = c;
                }
                a = 0;
                for (j = data.Length; j > 0; j--)
                {
                    c = data[j - 1];
                    c = rol(c, 4);
                    c = (byte)(c + j);
                    c ^= a;
                    a = c;
                    c ^= 0x13;
                    c = ror(c, 3);
                    data[j - 1] = c;
                }
            }
        }

        /// <summary>
        /// Decrypt data using MapleStory's Custom Encryption
        /// </summary>
        /// <param name="data">data to decrypt</param>
        /// <returns>Decrypted data</returns>
        public static void Decrypt(byte[] data)
        {
            int size = data.Length;
            int j;
            byte a, b, c;
            for (int i = 0; i < 3; i++)
            {
                a = 0;
                b = 0;
                for (j = size; j > 0; j--)
                {
                    c = data[j - 1];
                    c = rol(c, 3);
                    c ^= 0x13;
                    a = c;
                    c ^= b;
                    c = (byte)(c - j); // Guess this is supposed to be right?
                    c = ror(c, 4);
                    b = a;
                    data[j - 1] = c;
                }
                a = 0;
                b = 0;
                for (j = size; j > 0; j--)
                {
                    c = data[size - j];
                    c -= 0x48;
                    c ^= 0xFF;
                    c = rol(c, j);
                    a = c;
                    c ^= b;
                    c = (byte)(c - j); // Guess this is supposed to be right?
                    c = ror(c, 3);
                    b = a;
                    data[size - j] = c;
                }
            }
        }

        /// <summary>
        /// Rolls a byte left
        /// </summary>
        /// <param name="val">input byte to roll</param>
        /// <param name="num">amount of bits to roll</param>
        /// <returns>The left rolled byte</returns>
        public static byte rol(byte val, int num)
        {
            int highbit;
            for (int i = 0; i < num; i++)
            {
                highbit = ((val & 0x80) != 0 ? 1 : 0);
                val <<= 1;
                val |= (byte)highbit;
            }
            return val;
        }

        /// <summary>
        /// Rolls a byte right
        /// </summary>
        /// <param name="val">input byte to roll</param>
        /// <param name="num">amount of bits to roll</param>
        /// <returns>The right rolled byte</returns>
        public static byte ror(byte val, int num)
        {
            int lowbit;
            for (int i = 0; i < num; i++)
            {
                lowbit = ((val & 1) != 0 ? 1 : 0);
                val >>= 1;
                val |= (byte)(lowbit << 7);
            }
            return val;
        }
    }
}