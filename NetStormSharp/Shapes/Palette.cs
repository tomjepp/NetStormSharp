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
            Entries = new PaletteColor[(stream.Length - 8) / Marshal.SizeOf(typeof(PaletteColor))];

            stream.Seek(8, SeekOrigin.Begin);

            for (int i = 0; i < Entries.Length; i++)
            {
                PaletteColor color = stream.ReadStruct<PaletteColor>();
                Entries[i] = color;
            }
        }
    }
}
