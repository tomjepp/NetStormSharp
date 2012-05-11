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

        private byte[,] m_Data;

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
        public byte[,] Data
        {
            get
            {
                return m_Data;
            }
        }

        private uint m_CurrentX;
        private uint m_CurrentY;
        private void WriteByte(byte b)
        {
           if (m_CurrentY >= m_Height || m_CurrentX >= m_Width)
               return;

           m_Data[m_CurrentY, m_CurrentX] = b;

            m_CurrentX++;
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
                m_Data = new byte[m_Height, m_Width];
            }
            catch
            {
                return;
            }

            if (m_Data.Length == 0)
                return;

            try
            {
                while (m_CurrentY < m_Height)
                {
                    if (stream.Position == stream.Length)
                        break;
                    byte packetType = stream.ReadUInt8();

                    if (packetType == 0)
                    {
                        uint toWrite = m_Width - m_CurrentX;
                        // new line
                        for (int i = 0; i < toWrite; i++)
                        {
                            WriteByte(0xff);
                        }
                        m_CurrentY++;
                        m_CurrentX = 0;
                    }
                    else if (packetType == 1)
                    {
                        // Skip token
                        byte nSkip = stream.ReadUInt8();
                        for (int i = 0; i < nSkip; i++)
                        {
                            WriteByte(0xff);
                        }
                    }
                    else if ((packetType & 1) != 0)
                    {
                        // String token
                        byte stringLen = (byte)((packetType >> 1));
                        for (int i = 0; i < stringLen; i++)
                        {
                            byte b = stream.ReadUInt8();
                            WriteByte(b);
                        }
                    }
                    else
                    {
                        // Run token

                        byte runCopy = (byte)((packetType >> 1));
                        byte data = stream.ReadUInt8();
                        for (int i = 0; i < runCopy; ++i)
                        {
                            WriteByte(data);
                        }
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
