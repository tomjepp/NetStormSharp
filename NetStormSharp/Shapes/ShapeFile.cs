using System;
using System.Collections.Generic;
using System.IO;

namespace NetStormSharp.Shapes
{
    public class ShapeFile
    {
        private List<Section> m_Sections;
        public List<Section> Sections
        {
            get
            {
                return m_Sections;
            }
        }

        public ShapeFile(Stream stream)
        {
            m_Sections = new List<Section>();
        
            while (true)
            {
                long headerOffset = stream.Position;
                TypeHeader header = stream.ReadStruct<TypeHeader>();

                if (header.Version != 0x30312e31)
                   break;
                Section section = new Section(header, headerOffset, stream);
                m_Sections.Add(section);
            }

            stream.Dispose();
        }
    }
}
