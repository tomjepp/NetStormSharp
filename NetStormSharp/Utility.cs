using System;
using System.IO;
using System.Runtime.InteropServices;

namespace NetStormSharp
{
    public static class Utility
    {
        public static void XorCryptBytes(ref byte[] data, byte[] key)
        {
            int keyIndex = 0;
            for (int i = 0; i < data.Length; i++)
            {
                if (keyIndex == key.Length)
                    keyIndex = 0;

                data[i] = (byte)(data[i] ^ key[keyIndex]);

                keyIndex++;
            }
        }

        public static bool CompareBytes(byte[] array1, byte[] array2)
        {
            if (array1 == null || array2 == null)
                return false;

            if (array1.Length != array2.Length)
                return false;

            for (int i = 0; i < array1.Length; i++)
            {
                if (array1[i] != array2[i])
                    return false;
            }

            return true;
        }

        public static T ReadStruct<T>(byte[] data, int length)
        {
            IntPtr ptr = Marshal.AllocHGlobal(length);
            Marshal.Copy(data, 0, ptr, length);
            T structInstance = (T)Marshal.PtrToStructure(ptr, typeof(T));
            Marshal.FreeHGlobal(ptr);

            return structInstance;
        }
    }
}
