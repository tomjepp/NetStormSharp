using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using NetStormSharp.TitanArc;
using NetStormSharp.Shapes;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
//            Console.BufferHeight = Int16.MaxValue - 1;

            Palette palette = null;
            using (FileStream fs = new FileStream(@"C:\NetStorm\netstorm.tarc", FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                TarcFile tarcFile = new TarcFile(fs);
                using (Stream stream = tarcFile.GetStream(@"\d\gifcloud.col"))
                {
                    palette = new Palette(stream);
                }
            }
            Application.SetCompatibleTextRenderingDefault(false);
            Application.EnableVisualStyles();
            using (FileStream fs = new FileStream(@"C:\NetStorm\d\_shapes.shp", FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                ShapeFile shapeFile = new ShapeFile(fs);
                ShapeViewer viewer = new ShapeViewer(shapeFile, palette);
                Application.Run(viewer);
            }

            Console.ReadLine();
        }
    }
}
