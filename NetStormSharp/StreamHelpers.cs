using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace NetStormSharp
{
    public static class StreamHelpers
    {
        #region Struct helpers
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
        #endregion

        #region Signed integer helpers
        public static SByte ReadInt8(this Stream stream)
        {
            return (SByte)stream.ReadByte();
        }

        public static Int16 ReadInt16(this Stream stream)
        {
            byte[] data = new byte[2];
            stream.Read(data, 0, 2);
            return BitConverter.ToInt16(data, 0);
        }

        public static Int32 ReadInt32(this Stream stream)
        {
            byte[] data = new byte[4];
            stream.Read(data, 0, 4);
            return BitConverter.ToInt32(data, 0);
        }
        #endregion

        #region Unsigned integer helpers
        public static Byte ReadUInt8(this Stream stream)
        {
            return (byte)stream.ReadByte();
        }

        public static UInt16 ReadUInt16(this Stream stream)
        {
            byte[] data = new byte[2];
            stream.Read(data, 0, 2);
            return BitConverter.ToUInt16(data, 0);
        }

        public static UInt32 ReadUInt32(this Stream stream)
        {
            byte[] data = new byte[4];
            stream.Read(data, 0, 4);
            return BitConverter.ToUInt32(data, 0);
        }
        #endregion

        #region String helpers
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
        #endregion
    }
}
