using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using NetStormSharp.TitanArc;
using NetStormSharp.Shapes;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.BufferHeight = Int16.MaxValue-1;
            using (FileStream fs = new FileStream(@"C:\NetStorm\d\_shapes - Copy.shp", FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                ShapeFile shapes = new ShapeFile(fs);
            }

            Console.ReadLine();
        }
    }
}
