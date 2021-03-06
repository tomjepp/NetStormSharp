﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

using NetStormSharp;
using NetStormSharp.Shapes;

namespace ShapeViewer
{
    public partial class ShapeViewer : Form
    {
        private ShapeFile ShapeFile;
        private Palette Palette;

        public ShapeViewer(ShapeFile shapeFile, Palette palette)
        {
            InitializeComponent();

            Palette = palette;
            ShapeFile = shapeFile;

            for (int i = 0; i < shapeFile.Sections.Count; i++)
            {
                Section section = shapeFile.Sections[i];

                UnitType type = (UnitType)i;

                TreeNode sectionNode = new TreeNode(String.Format("Type {0} ({1})", type, i));
                ShapeTree.Nodes.Add(sectionNode);

                for (int j = 0; j < section.Shapes.Count; j++)
                {
                    Shape shape = section.Shapes[j];

                    TreeNode shapeNode = new TreeNode(String.Format("Frame {0} ({1}x{2})", j, shape.Width, shape.Height));
                    shapeNode.Tag = shape;
                    sectionNode.Nodes.Add(shapeNode);
                }
            }

            VisualisePalette();
        }

        public void VisualisePalette()
        {
            Bitmap b = new Bitmap((256 * 4) + 32, (4 * 3) + 32, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
            Graphics g = Graphics.FromImage(b);
            g.Clear(Color.CornflowerBlue);
            for (int i = 0; i < Palette.Entries.Length; i++)
            {
                PaletteColor paletteColor = Palette.Entries[i];
                Color color = Color.FromArgb(paletteColor.Red, paletteColor.Green, paletteColor.Blue);
                //Color color = Color.FromArgb(paletteColor.Alpha, paletteColor.Red, paletteColor.Green, paletteColor.Blue);

                int col = i;
                int row = 0;
                if (i >= 256)
                {
                    col = i - 256;
                    row = 1;
                }
                if (i >= 512)
                {
                    col = i - 512;
                    row = 2;
                }

                int x1 = (col * 4) + 16;
                int y1 = (row * 4) + 16;

                for (int x = x1; x < x1 + 4; x++)
                {
                    for (int y = y1; y < y1 + 4; y++)
                    {
                        b.SetPixel(x, y, color);
                    }
                }
            }

            ImageOutput.Image = b;
        }

        public void Render()
        {
            if (ShapeTree.SelectedNode == null || ShapeTree.SelectedNode.Tag == null || !(ShapeTree.SelectedNode.Tag is Shape))
                return;

            Shape shape = (Shape)ShapeTree.SelectedNode.Tag;

            if (shape.Width == 0 || shape.Height == 0)
                return;

            int drawWindowWidth = shape.MaxX - shape.MinX;
            int drawWindowHeight = shape.MaxY - shape.MinY;
            //int drawWindowWidth = shape.Width;
            //int drawWindowHeight = shape.Height;
            Console.WriteLine("Draw window: {0}x{1} size: {2}x{3}", shape.OriginX, shape.OriginY, drawWindowWidth, drawWindowHeight);

            if (drawWindowHeight == 0 || drawWindowWidth == 0)
            {
                drawWindowHeight = shape.Height;
                drawWindowWidth = shape.Width;
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
                                PaletteColor paletteColor = Palette.Entries[paletteEntryIndex];
                                Color color = Color.FromArgb(paletteColor.Red, paletteColor.Green, paletteColor.Blue);
                                bitmap.SetPixel(x, y, color);
                            }
                        }
                    }

                    Graphics drawGraphics = Graphics.FromImage(drawBitmap);
                    drawGraphics.DrawImageUnscaledAndClipped(bitmap, new Rectangle(0, 0, drawWindowWidth, drawWindowHeight));

                    ImageOutput.Image = drawBitmap;
                }
            }
        }

        private void ShapeTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            Render();
        }

        private void ExportButton_Click(object sender, EventArgs e)
        {
            DialogResult dr = ExportSaveDialog.ShowDialog();
            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                if (File.Exists(ExportSaveDialog.FileName))
                    File.Delete(ExportSaveDialog.FileName);

                ImageOutput.Image.Save(ExportSaveDialog.FileName);
            }
        }
    }
}
