using System;
using System.Runtime.InteropServices;

namespace NetStormSharp.Shapes
{
    [StructLayout(LayoutKind.Explicit, Size = 0x08)]
    public struct PaletteHeader
    {
        [FieldOffset(0x00)]
        public uint FileLength;

        [FieldOffset(0x04)]
        public ushort ID;

        [FieldOffset(0x06)]
        public ushort Version;
    }
}
