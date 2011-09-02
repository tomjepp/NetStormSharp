using System;
using System.Collections.Generic;
using System.IO;

namespace NetStormSharp.Shapes
{
    public class ShapeFile
    {
        private Dictionary<SectionHeader, Dictionary<SectionElement, Shape>> m_Shapes;
        public Dictionary<SectionHeader, Dictionary<SectionElement, Shape>> Shapes
        {
            get
            {
                return m_Shapes;
            }
        }

        public ShapeFile(Stream stream)
        {
            m_Shapes = new Dictionary<SectionHeader, Dictionary<SectionElement, Shape>>();
            while (true)
            {
                SectionHeader header = stream.ReadStruct<SectionHeader>();

                if (header.Version != 0x30312e31)
                   break;

                if (!m_Shapes.ContainsKey(header))
                    m_Shapes.Add(header, new Dictionary<SectionElement, Shape>());

                for (int i = 0; i < header.ElementCount; i++)
                {
                    SectionElement element = stream.ReadStruct<SectionElement>();

                    if (!m_Shapes[header].ContainsKey(element))
                        m_Shapes[header].Add(element, null);

                    if (element.Unknown1 != 0)
                        System.Diagnostics.Debugger.Break();
                }
            }

            SectionHeader[] headers = new SectionHeader[m_Shapes.Count];
            m_Shapes.Keys.CopyTo(headers, 0);
            for (int i = 0; i < m_Shapes.Count; i++)
            {
                SectionHeader header = headers[i];

                SectionElement[] elements = new SectionElement[m_Shapes[header].Count];
                m_Shapes[header].Keys.CopyTo(elements, 0);

                for (int j = 0; j < m_Shapes[header].Count; j++)
                {
                    SectionElement element = elements[j];

                    Console.WriteLine("Seeking to: {0}", element.Offset);
                    stream.Seek(element.Offset, SeekOrigin.Begin);
                    m_Shapes[header][element] = new Shape(stream);
                }
            }

            stream.Dispose();
        }
    }
}
