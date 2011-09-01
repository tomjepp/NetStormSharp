using System;
using System.Collections.Generic;
using System.IO;

namespace NetStormSharp.Shapes
{
    public class ShapeFile
    {
        public ShapeFile(Stream stream)
        {
            uint highestOffset = 0;
            uint sectionCount = 0;
            uint elementCount = 0;
            List<uint> offsets = new List<uint>();
            while (true)
            {
                SectionHeader header = stream.ReadStruct<SectionHeader>();

                if (header.Version != 0x30312e31)
                    break;

                sectionCount++;

                uint sectionStartCount = elementCount;

                for (int i = 0; i < header.ElementCount; i++)
                {
                    SectionElement element = stream.ReadStruct<SectionElement>();
                    Console.WriteLine("{0:X8} {1:X8}", element.Offset, element.Unknown1);

                    if (element.Offset > highestOffset)
                        highestOffset = element.Offset;

                    elementCount++;

                    offsets.Add(element.Offset);

                    if (element.Unknown1 != 0)
                        System.Diagnostics.Debugger.Break();
                }
                uint sectionTotal = elementCount - sectionStartCount;
                Console.WriteLine("Section {0} count: {1}", sectionCount, sectionTotal);
            }

            Console.WriteLine("{0:X}", stream.Position);

            Console.WriteLine("Section count: {0}", sectionCount);
            Console.WriteLine("Element count: {0}", elementCount);
            Console.WriteLine("Highest offset: {0:X}", highestOffset);

            offsets.Sort();

            for (int i = 0; i < offsets.Count; i++)
            {
                uint lastOffset = 0;
                uint offset = offsets[i];

                if (i > 1)
                {
                    lastOffset = offsets[i - 1];
                    Console.WriteLine("Size: {0:X}", offset - lastOffset);
                }
                Console.Write("Offset: {0:X} ", offset);
            }

            stream.Dispose();
        }
    }
}
