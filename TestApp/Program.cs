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
            using (FileStream fs = new FileStream(@"D:\Gaming\NetStorm\NetStorm\tarcExtract\netstorm.tarc", FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (TarcFile tarc = new TarcFile(fs))
                {
                    Palette palette = new Palette(tarc.GetStream(@"\d\raincannon.col"));
                    
                }
            }

            Console.ReadLine();
        }
    }
}
