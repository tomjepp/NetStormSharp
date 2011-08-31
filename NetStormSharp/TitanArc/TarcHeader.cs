using System;
using System.Runtime.InteropServices;

namespace NetStormSharp.TitanArc
{
    [StructLayout(LayoutKind.Explicit, Size=0x2C)]
    public struct TarcHeader
    {
        [FieldOffset(0x00)]
        [MarshalAs(UnmanagedType.ByValArray, SizeConst=0x0A)]
        public Byte[] Magic;

        [FieldOffset(0x0A)]
        public UInt16 Padding;

        [FieldOffset(0x0C)]
        public UInt32 MajorVersion;

        [FieldOffset(0x10)]
        public UInt32 MinorVersion;

        [FieldOffset(0x14)]
        public UInt32 FileCount;

        [FieldOffset(0x18)]
        [MarshalAs(UnmanagedType.U4)]
        public EncryptionType EncryptionType;

        [FieldOffset(0x1C)]
        public UInt32 FileListOffsetListOffset;

        [FieldOffset(0x20)]
        public UInt32 FileListOffset;

        [FieldOffset(0x24)]
        public UInt32 FileListLength;

        [FieldOffset(0x28)]
        public UInt32 FileContentOffset;
    }
}
