using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using NetStormSharp.TitanArc;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            using (FileStream fs = new FileStream(@"D:\Gaming\NetStorm\NetStorm\tarcExtract\netstorm.tarc", FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                TarcFile tarc = new TarcFile(fs);
            }

            Console.ReadLine();
        }
    }
}
