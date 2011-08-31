using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;

namespace NetStormSharp.TitanArc
{
    public class TarcFile : IDisposable
    {
        private Stream m_Stream;
        private TarcHeader m_Header;
        private TarcFileEntry[] m_Files;

        private static byte[] Magic = new byte[] { 0x54, 0x41, 0x46, 0x46, 0x20, 0x76, 0x30, 0x2E, 0x32, 0x1A };

        public TarcFile(Stream stream)
        {
            m_Header = stream.ReadStruct<TarcHeader>();
            if (!Utility.CompareBytes(m_Header.Magic, Magic))
                throw new Exception("Magic values do not match! Not a TARC file?");

            if (!(m_Header.MajorVersion == 0 && m_Header.MinorVersion == 2))
                throw new Exception("Unsupported TARC version!");

            uint[] fileListEntryOffsets = new uint[m_Header.FileCount];
            stream.Seek(m_Header.FileListOffsetListOffset, SeekOrigin.Begin);
            for (int i = 0; i < fileListEntryOffsets.Length; i++)
            {
                fileListEntryOffsets[i] = stream.ReadUInt32();
            }

            m_Files = new TarcFileEntry[m_Header.FileCount];

            for (int i = 0; i < m_Files.Length; i++)
            {
                uint fileEntryOffset = m_Header.FileListOffset + fileListEntryOffsets[i];
                stream.Seek(fileEntryOffset, SeekOrigin.Begin);
                TarcFileEntry fileEntry = new TarcFileEntry();
                fileEntry.Offset = stream.ReadUInt32();
                fileEntry.Length = stream.ReadUInt32();
                fileEntry.Filename = stream.ReadAsciiNullTerminatedString();

                Console.WriteLine("{0} {1} {2}", fileEntry.Offset, fileEntry.Length, fileEntry.Filename);

                m_Files[i] = fileEntry;
            }
        }

        public void Dispose()
        {
            if (m_Stream != null)
            {
                m_Stream.Dispose();
                m_Stream = null;
            }
        }
    }
}
