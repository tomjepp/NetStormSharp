using System;
using System.Runtime.InteropServices;

namespace NetStormSharp.Shapes
{
    [StructLayout(LayoutKind.Explicit, Size = 0x08)]
    public struct SectionElement
    {
        [FieldOffset(0x00)]
        public uint Offset;

        [FieldOffset(0x04)]
        public uint Unknown1;
    }
}
