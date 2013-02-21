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

        public Section(TypeHeader header, long sectionHeaderOffset, Stream stream)
        {
            m_Shapes = new List<Shape>();
            for (int i = 0; i < header.FrameCount; i++)
            {
                long elementOffset = stream.Position;
                FrameHeader element = stream.ReadStruct<FrameHeader>();

                long oldPos = stream.Position;

                stream.Seek(sectionHeaderOffset + element.Offset, SeekOrigin.Begin);
                Shape shape = new Shape(stream);
                m_Shapes.Add(shape);
                stream.Seek(oldPos, SeekOrigin.Begin);

                if (element.ColorTable != 0)
                    throw new Exception("Unknown color table: " + element.ColorTable.ToString());
            }
        }
    }
}
