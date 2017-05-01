using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NetStormSharp;

namespace XorCrypt
{
    class Program
    {
        private static byte[] DefaultKey = new byte[] { 0x6D, 0x79, 0x64, 0x6F, 0x67, 0x68, 0x61, 0x73, 0x66, 0x6C, 0x65, 0x61, 0x73 };

        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Usage: XorCrypt filename");
                return;
            }

            string filename = args[0];
            byte[] bytes = File.ReadAllBytes(filename);


            Utility.XorCryptBytes(ref bytes, DefaultKey);

            File.WriteAllBytes(filename, bytes);
        }
    }
}
