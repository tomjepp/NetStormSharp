using System;
using System.Collections.Generic;
using System.IO;

namespace NetStormSharp.Shapes
{
    public class Palette
    {
        private PaletteColor[] m_Entries;

        public PaletteColor[] Entries
        {
            get
            {
                return m_Entries;
            }
        }

        public Palette(Stream stream)
        {
            PaletteHeader header = stream.ReadStruct<PaletteHeader>();

            if (header.ID != 0xB123)
                throw new Exception("Unable to load palette. ID != 0xB123.");

            if (header.Version != 0)
                throw new Exception("Unable to load palette. Version != 0.");

            uint entryCount = header.FileLength / 3;

            m_Entries = new PaletteColor[entryCount];
            for (uint i = 0; i < entryCount; i++)
            {
                PaletteColor color = stream.ReadStruct<PaletteColor>();
                m_Entries[i] = color;
            }

            stream.Dispose();
        }
    }
}
