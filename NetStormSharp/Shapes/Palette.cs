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
            //Entries = new PaletteColor[stream.Length/Marshal.SizeOf(typeof(PaletteColor))];
            Entries = new PaletteColor[(stream.Length - 8) / Marshal.SizeOf(typeof(PaletteColor))];

            stream.Seek(8, SeekOrigin.Begin);

            for (int i = 0; i < Entries.Length; i++)
            {
                PaletteColor color = stream.ReadStruct<PaletteColor>();
                //PaletteColor color = new PaletteColor();
                Entries[i] = color;
            }

            for (int i = 0; i < Entries.Length; i++)
            {
            }

            //Entries[1].Alpha = 0xFF;
            //Entries[1].Red = 0xFF;
            //Entries[1].Green = 0xFF;
            //Entries[1].Blue = 0xFF;
        }
    }
}
