using System;
using System.Runtime.InteropServices;

namespace NetStormSharp.Shapes
{
    [StructLayout(LayoutKind.Explicit, Size = 0x08)]
    public struct TypeHeader
    {
        [FieldOffset(0x00)]
        public uint Version;

        [FieldOffset(0x04)]
        public uint FrameCount;
    }
}
