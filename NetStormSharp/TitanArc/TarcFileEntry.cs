using System;
using System.Runtime.InteropServices;

namespace NetStormSharp.TitanArc
{
    [StructLayout(LayoutKind.Explicit)]
    public struct TarcFileEntry
    {
        [FieldOffset(0x00)]
        public UInt32 Offset;

        [FieldOffset(0x04)]
        public UInt32 Length;

        [FieldOffset(0x08)]
        [MarshalAs(UnmanagedType.LPStr)]
        public string Filename;
    }
}
