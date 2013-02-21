using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Drawing;

using NetStormSharp.TitanArc;
using NetStormSharp.Shapes;

namespace TestApp
{
    class Program
    {
        [STAThread]
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

                Directory.CreateDirectory("out");

                for (int i = 0; i < shapeFile.Sections.Count; i++)
                {
                    Section section = shapeFile.Sections[i];

                    for (int j = 0; j < section.Shapes.Count; j++)
                    {
                        Shape shape = section.Shapes[j];

                        Bitmap bitmap = Render(shape, palette);

                        //string filename = String.Format("out/{2}-{3} {0}-{1}.png", i, j, shape.OriginalOffset, shape.OriginalFinish);
                        string filename = String.Format("out/{0}-{1}.png", i, j);

                        if (File.Exists(filename))
                            File.Delete(filename);

                        if (bitmap != null)
                        {
                            try
                            {
                                bitmap.Save(filename, System.Drawing.Imaging.ImageFormat.Png);
                                bitmap.Dispose();
                            }
                            catch
                            {
                            }
                        }
                    }
                }
                ShapeViewer viewer = new ShapeViewer(shapeFile, palette);
                Application.Run(viewer);
            }

            Console.ReadLine();
        }

        public static Bitmap Render(Shape shape, Palette palette)
        {
            if (shape.Width == 0 || shape.Height == 0)
                return null;

            int drawWindowWidth = shape.MaxX - shape.MinX;
            int drawWindowHeight = shape.MaxY - shape.MinY;
            //int drawWindowWidth = shape.Width;
            //int drawWindowHeight = shape.Height;
            Console.WriteLine("Draw window: {0}x{1} size: {2}x{3}", shape.OriginX, shape.OriginY, drawWindowWidth, drawWindowHeight);

            if (drawWindowHeight == 0 || drawWindowWidth == 0)
            {
                return null;
            }

            Bitmap drawBitmap = new Bitmap(drawWindowWidth, drawWindowHeight, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            using (Bitmap bitmap = new Bitmap(shape.Width, shape.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb))
            {
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.Clear(Color.CornflowerBlue);
                    for (int x = 0; x < shape.Width; x++)
                    {
                        for (int y = 0; y < shape.Height; y++)
                        {
                            byte paletteEntryIndex = shape.Data[y, x];
                            if (paletteEntryIndex == 255)
                            {
                                Color color = Color.FromArgb(0, 255, 255, 255);
                                bitmap.SetPixel(x, y, color);
                            }
                            else
                            {
                                PaletteColor paletteColor = palette.Entries[paletteEntryIndex];
                                Color color = Color.FromArgb(paletteColor.Red, paletteColor.Green, paletteColor.Blue);
                                bitmap.SetPixel(x, y, color);
                            }
                        }
                    }

                    Graphics drawGraphics = Graphics.FromImage(drawBitmap);
                    drawGraphics.DrawImageUnscaledAndClipped(bitmap, new Rectangle(0, 0, drawWindowWidth, drawWindowHeight));

                    return drawBitmap;
                }
            }
        }
    }
}
