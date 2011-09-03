using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;

namespace NetStormSharp.Shapes
{
    public class Palette
    {
        public PaletteColor[] Entries;

        public Palette(Stream stream)
        {
            Entries = new PaletteColor[stream.Length/Marshal.SizeOf(typeof(PaletteColor))];

            for (int i = 0; i < Entries.Length; i++)
            {
                PaletteColor color = stream.ReadStruct<PaletteColor>();
                Entries[i] = color;
            }
        }
    }
}
