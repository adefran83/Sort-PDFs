using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SortPDF
{
    class Program
    {
        internal static readonly string docketArchive = @"R:\DocketArchive\";
        internal static int count = 0;

        static void Main(string[] args)
        {
            Console.WriteLine("getting files to move");
            DirectoryInfo dirs = new DirectoryInfo(docketArchive);

            DirectoryInfo[] subDirs = dirs.GetDirectories();
            Parallel.ForEach(subDirs, dirt =>
            {
                DirectoryInfo[] dateDirs = dirt.GetDirectories();
                Parallel.ForEach(dateDirs, dir =>
                {
                    FileInfo[] files = dir.GetFiles("*.pdf", SearchOption.AllDirectories);
                    foreach (FileInfo file in files)
                    {
                        string fileName = file.Name;
                        string batch = file.Name.Substring(0, 5);
                        string finalLocation = docketArchive + batch + "\\";

                        if (!Directory.Exists(finalLocation))
                        {
                            Directory.CreateDirectory(finalLocation);
                        }

                        try
                        {
                            fileName = fileName.Replace("..pdf", ".pdf");
                        }
                        catch (Exception ex)
                        {
                        }

                        finalLocation += fileName;

                        if (!File.Exists(finalLocation))
                        {
                            File.Copy(file.FullName, finalLocation);
                            count++;
                        }
                    }
                });
            });
        }
    }
}
