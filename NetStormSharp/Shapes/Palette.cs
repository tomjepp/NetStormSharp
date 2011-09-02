using System;
using System.Collections.Generic;
using System.IO;

namespace NetStormSharp.Shapes
{
    public class Palette
    {
        public PaletteColor[] Entries;

        public Palette(Stream stream)
        {
            Entries = new PaletteColor[stream.Length/4];

            for (int i = 0; i < Entries.Length; i++)
            {
                PaletteColor color = stream.ReadStruct<PaletteColor>();
                Entries[i] = color;
            }
        }
    }
}
