using System;
using System.Collections.Generic;
using System.IO;

namespace NetStormSharp.Shapes
{
    public class Shape
    {
        private ushort m_Height;
        private ushort m_Width;

        private short m_OriginX;
        private short m_OriginY;

        private int m_MinX;
        private int m_MinY;
        private int m_MaxX;
        private int m_MaxY;

        private byte[] m_Data;

        public ushort Height
        {
            get
            {
                return m_Height;
            }
        }

        public ushort Width
        {
            get
            {
                return m_Width;
            }
        }

        public short OriginX
        {
            get
            {
                return m_OriginX;
            }
        }

        public short OriginY
        {
            get
            {
                return m_OriginY;
            }
        }

        public int MinX
        {
            get
            {
                return m_MinX;
            }
        }

        public int MinY
        {
            get
            {
                return m_MinY;
            }
        }

        public int MaxX
        {
            get
            {
                return m_MaxX;
            }
        }

        public int MaxY
        {
            get
            {
                return m_MaxY;
            }
        }

        public byte[] Data
        {
            get
            {
                return m_Data;
            }
        }

        private void AdvancePixel(ref int currentX, ref int currentY)
        {
            currentX++;
            if (currentX == m_Width)
            {
                currentY++;
                currentX = 0;
            }
        }

        public Shape(Stream stream)
        {
            m_Width = stream.ReadUInt16();
            m_Height = stream.ReadUInt16();
            m_OriginX = stream.ReadInt16();
            m_OriginY = stream.ReadInt16();

            m_MinX = stream.ReadInt32();
            m_MinY = stream.ReadInt32();
            m_MaxX = stream.ReadInt32();
            m_MaxY = stream.ReadInt32();

            Console.WriteLine("Size: {0}x{1}, Origin: {2}x{3}, MinX: {4}, MinY: {5}, MaxX: {6}, MaxY: {7}", m_Width, m_Height, m_OriginX, m_OriginY, m_MinX, m_MinY, m_MaxX, m_MaxY);

            try
            {
                m_Data = new byte[m_Height * m_Width];
                int currentX = 0;
                int currentY = 0;

                while (currentY < m_Width)
                {
                    byte firstByte = stream.ReadUInt8();
                    byte secondByte = stream.ReadUInt8();
                    if (firstByte == 0 && secondByte > 0x02)
                    {
                        // Absolute mode

                        for (int count = 0; count < secondByte; count++)
                        {
                            byte colorIndex = stream.ReadUInt8();
                            m_Data[currentX * currentY] = colorIndex;
                            AdvancePixel(ref currentX, ref currentY);
                        }
                    }
                    else if (firstByte == 0 && secondByte < 0x03)
                    {
                        // Escape mode
                        if (secondByte == 0)
                        {
                            // End of line
                            currentY++;
                            currentX = 0;
                        }
                        else if (secondByte == 1)
                        {
                            // End of bitmap
                            break;
                        }
                        else if (secondByte == 2)
                        {
                            // Delta
                            byte deltaX = stream.ReadUInt8();
                            byte deltaY = stream.ReadUInt8();
                            currentX += deltaX;
                            currentY += deltaY;
                        }
                    }
                    else
                    {
                        // Encoded mode

                        for (int count = 0; count < firstByte; count++)
                        {
                            m_Data[currentX * currentY] = secondByte;
                            AdvancePixel(ref currentX, ref currentY);
                        }
                    }
                }
            }
            catch
            {
            }
        }
    }
}
