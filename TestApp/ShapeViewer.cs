using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using NetStormSharp.Shapes;

namespace TestApp
{
    public partial class ShapeViewer : Form
    {
        public ShapeViewer(ShapeFile shapeFile, Dictionary<string, Palette> palettes)
        {
            InitializeComponent();

            foreach (string filename in palettes.Keys)
            {
                PaletteList.Items.Add(palettes[filename]);
            }

            int headerId = 0;
            foreach (SectionHeader header in shapeFile.Shapes.Keys)
            {
                TreeNode sectionNode = new TreeNode(String.Format("Section {0}", headerId));
                ShapeTree.Nodes.Add(sectionNode);

                int elementId = 0;
                foreach (SectionElement element in shapeFile.Shapes[header].Keys)
                {
                    Shape shape = shapeFile.Shapes[header][element];
                    TreeNode elementNode = new TreeNode(String.Format("Element {0} ({1}x{2})", elementId, shape.Width, shape.Height));
                    elementNode.Tag = shape;
                    sectionNode.Nodes.Add(elementNode);
                    elementId++;
                }

                headerId++;
            }
        }

        public void Render()
        {
            if (PaletteList.SelectedItem != null && ShapeTree.SelectedNode != null)
            {
                if (!(PaletteList.SelectedItem is Palette) || !(ShapeTree.SelectedNode.Tag is Shape))
                {
                    return;
                }

                Palette palette = (Palette)PaletteList.SelectedItem;
                Shape shape = (Shape)ShapeTree.SelectedNode.Tag;

                try
                {
                    Bitmap bitmap = new Bitmap(shape.Width, shape.Height, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
                    for (int x = 0; x < shape.Width; x++)
                    {
                        for (int y = 0; y < shape.Height; y++)
                        {
                            int pixel = x * y;
                            byte paletteEntryIndex = shape.Data[pixel];
                            PaletteColor paletteColor = palette.Entries[paletteEntryIndex];
                            Color color = Color.FromArgb(paletteColor.Red, paletteColor.Green, paletteColor.Blue);

                            bitmap.SetPixel(x, y, color);
                        }
                    }

                    ImageOutput.Image = bitmap;
                }
                catch
                {
                }
            }
        }

        private void ShapeTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            Render();
        }

        private void PaletteList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Render();
        }
    }
}
