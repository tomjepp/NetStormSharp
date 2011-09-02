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
            Console.BufferHeight = Int16.MaxValue - 1;
            ShapeFile shapeFile = null;
            using (FileStream fs = new FileStream(@"C:\NetStorm\d\_shapes.shp", FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                shapeFile = new ShapeFile(fs);
            }

            Dictionary<string, Palette> palettes = new Dictionary<string, Palette>();
            using (FileStream fs = new FileStream(@"C:\NetStorm\netstorm.tarc", FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                TarcFile tarcFile = new TarcFile(fs);
                foreach (TarcFileEntry file in tarcFile.Files)
                {
                    if (file.Filename.ToLower().EndsWith(".col"))
                    {
                        string fileName = Path.GetFileName(file.Filename);

                        Palette palette = new Palette(tarcFile.GetStream(file.Filename), fileName);
                        palettes.Add(fileName, palette);
                    }
                }
            }
            Application.SetCompatibleTextRenderingDefault(false);
            Application.EnableVisualStyles();
            ShapeViewer viewer = new ShapeViewer(shapeFile, palettes);
            Application.Run(viewer);

            Console.ReadLine();
        }
    }
}
