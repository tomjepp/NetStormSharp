using System;
using System.Collections.Generic;
using System.IO;

namespace NetStormSharp.Shapes
{
    public class Section
    {
        private List<Shape> m_Shapes;
        public List<Shape> Shapes
        {
            get
            {
                return m_Shapes;
            }
        }

        public Section(SectionHeader header, long sectionHeaderOffset, Stream stream)
        {
            m_Shapes = new List<Shape>();
            for (int i = 0; i < header.ElementCount; i++)
            {
                long elementOffset = stream.Position;
                SectionElement element = stream.ReadStruct<SectionElement>();

                long oldPos = stream.Position;

                stream.Seek(sectionHeaderOffset + element.Offset, SeekOrigin.Begin);
                Shape shape = new Shape(stream);
                m_Shapes.Add(shape);
                stream.Seek(oldPos, SeekOrigin.Begin);

                if (element.Unknown1 != 0)
                    System.Diagnostics.Debugger.Break();
            }
        }
    }
}
