using System;
using System.Collections.Generic;
using System.IO;

namespace NetStormSharp.Shapes
{
    public class Shape
    {
        private ushort m_Width;
        private ushort m_Height;

        private short m_OriginX;
        private short m_OriginY;

        private int m_MinX;
        private int m_MinY;
        private int m_MaxX;
        private int m_MaxY;

        private byte[] m_Data;

        public ushort Width
        {
            get
            {
                return m_Width;
            }
        }

        public ushort Height
        {
            get
            {
                return m_Height;
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

        public Shape(Stream stream)
        {
            m_Height = stream.ReadUInt16();
            m_Width = stream.ReadUInt16();

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
                for (int i = 0; i < m_Data.Length; i++)
                {
                    m_Data[i] = 0;
                }
            }
            catch
            {
                return;
            }

            if (m_Data.Length == 0)
                return;

            try
            {
                uint linesCount = 0;
                uint lineChar = 0;

                while (linesCount < m_Height)
                {
                    byte packetType = stream.ReadUInt8();

                    if (packetType == 0)
                    {
                        if (lineChar == 0)
                            continue;

                        // End line
                        ++linesCount;
                        lineChar = 0;
                    }
                    else if (packetType == 1)
                    {
                        // Skip token
                        byte nSkip = stream.ReadUInt8();
                        lineChar += nSkip;
                    }
                    else if ((packetType & 1) != 0)
                    {
                        // String token
                        byte stringLen = (byte)((packetType - 1) / 2);
                        if (stringLen < (m_Width - lineChar))
                        {
                            uint startOffset = linesCount * m_Width + lineChar;
                            for (int i = 0; i < stringLen; i++)
                            {
                                byte b = stream.ReadUInt8();
                                m_Data[startOffset + i] = b;
                            }
                            lineChar += stringLen;
                        }
                        else
                        {
                            lineChar = 0;
                            ++linesCount;

                            for (int i = 0; i < stringLen; i++)
                            {
                                byte b = stream.ReadUInt8();
                            }
                        }
                    }
                    else
                    {
                        // Run token

                        byte runCopy = (byte)(packetType / 2);
                        byte data = stream.ReadUInt8();
                        for (int i = 0; i < runCopy; ++i)
                        {
                            uint index = linesCount * m_Width + lineChar++;
                            if (index < m_Width * m_Height)
                                m_Data[index] = data;
                        }
                    }

                    while (lineChar > m_Width)
                    {
                        lineChar -= m_Width;
                        ++linesCount;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error while decoding: {0}", e);
            }
        }
    }
}
