﻿using System;
using System.Runtime.InteropServices;

namespace NetStormSharp.Shapes
{
    [StructLayout(LayoutKind.Explicit, Size = 0x03)]
    public struct PaletteColor
    {
        [FieldOffset(0x00)]
        public byte Red;
        [FieldOffset(0x01)]
        public byte Green;
        [FieldOffset(0x02)]
        public byte Blue;        
    }
}
