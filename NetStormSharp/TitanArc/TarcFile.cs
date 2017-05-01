using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;

namespace NetStormSharp.TitanArc
{
    public class TarcFile : IDisposable
    {
        private Stream m_Stream;
        private byte[] m_Key;
        private TarcHeader m_Header;
        private TarcFileEntry[] m_Files;

        private static byte[] Magic = new byte[] { 0x54, 0x41, 0x46, 0x46, 0x20, 0x76, 0x30, 0x2E, 0x32, 0x1A };
        private static byte[] DefaultKey = new byte[] { 0x6D, 0x79, 0x64, 0x6F, 0x67, 0x68, 0x61, 0x73, 0x66, 0x6C, 0x65, 0x61, 0x73 };

        public TarcFile(Stream stream, byte[] key) : this(stream)
        {
            if (m_Header.EncryptionType == EncryptionType.None && key != null)
                throw new Exception("Cannot use an encryption key when the TARC file is not encrypted!");

            m_Key = key;
        }

        public TarcFileEntry[] Files
        {
            get
            {
                return m_Files;
            }
        }

        public TarcFile(Stream stream)
        {
            m_Stream = stream;
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
                fileEntry.Length = stream.ReadInt32();
                fileEntry.Filename = stream.ReadAsciiNullTerminatedString();

                m_Files[i] = fileEntry;
            }

            m_Key = DefaultKey;
        }

        public bool ContainsFile(string filename)
        {
            for (int i = 0; i < m_Files.Length; i++)
            {
                if (m_Files[i].Filename.ToLowerInvariant() == filename.ToLowerInvariant())
                    return true;
            }

            return false;
        }

        public byte[] GetBytes(string filename)
        {
            TarcFileEntry fileEntry = FindEntry(filename);
            uint offset = m_Header.FileContentOffset + fileEntry.Offset;
            m_Stream.Seek(offset, SeekOrigin.Begin);
            byte[] buffer = new byte[fileEntry.Length];
            m_Stream.Read(buffer, 0, fileEntry.Length);

            if (m_Header.EncryptionType == EncryptionType.SimpleXor)
                Utility.XorCryptBytes(ref buffer, m_Key);

            return buffer;
        }

        public Stream GetStream(string filename)
        {
            byte[] bytes = GetBytes(filename);
            MemoryStream stream = new MemoryStream(bytes);

            return stream;
        }

        private TarcFileEntry FindEntry(string filename)
        {
            for (int i = 0; i < m_Files.Length; i++)
            {
                if (m_Files[i].Filename.ToLowerInvariant() == filename.ToLowerInvariant())
                    return m_Files[i];
            }

            throw new Exception("Unable to find filename: " + filename);
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
