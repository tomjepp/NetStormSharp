using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NetStormSharp;
using NetStormSharp.TitanArc;

namespace ExtractTarc
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Usage: ExtractTarc filename");
                return;
            }

            string filename = args[0];

            string outputFolder = "out";

            using (Stream tarcStream = File.OpenRead(filename))
            {
                using (TarcFile tarc = new TarcFile(tarcStream))
                {
                    int i = 0;
                    foreach (TarcFileEntry file in tarc.Files)
                    {
                        i++;

                        string tfeFilename = file.Filename;
                        if (tfeFilename.StartsWith(@"\"))
                        {
                            tfeFilename = tfeFilename.Substring(1);
                        }

                        string outputPath = Path.Combine(outputFolder, tfeFilename);
                        string outputDir = Path.GetDirectoryName(outputPath);

                        Console.WriteLine("[{0}/{1}] {2} => {3}", i, tarc.Files.Length, tfeFilename, outputPath);
                        
                        Directory.CreateDirectory(outputDir);

                        using (Stream s = tarc.GetStream(file.Filename))
                        {
                            using (Stream outStream = File.Create(outputPath))
                            {
                                s.CopyTo(outStream);
                            }
                        }
                    }
                }
            }

            Console.WriteLine("Done.");
        }
    }
}
