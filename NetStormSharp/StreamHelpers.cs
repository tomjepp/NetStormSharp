using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace NetStormSharp
{
    public static class StreamHelpers
    {
        public static T ReadStruct<T>(this Stream stream)
        {
            byte[] data = new byte[Marshal.SizeOf(typeof(T))];
            stream.Read(data, 0, data.Length);
            return Utility.ReadStruct<T>(data, data.Length);
        }

        public static T ReadStruct<T>(this Stream stream, int length)
        {
            byte[] data = new byte[length];
            stream.Read(data, 0, data.Length);
            return Utility.ReadStruct<T>(data, length);
        }

        public static UInt32 ReadUInt32(this Stream stream)
        {
            byte[] data = new byte[4];
            stream.Read(data, 0, 4);
            return BitConverter.ToUInt32(data, 0);
        }

        public static string ReadAsciiNullTerminatedString(this Stream stream)
        {
            StringBuilder sb = new StringBuilder();
            while (true)
            {
                char c = (char)stream.ReadByte();
                if (c == 0)
                    return sb.ToString();
                else
                    sb.Append(c);
            }
        }
    }
}
