using System;
using System.Collections.Generic;
using System.IO;

namespace ServiceConsoleileWatcher
{
    class Program
    {       
        static void Main(string[] args)
        {
            Dictionary<string, string> rules = new Dictionary<string, string>()
            {
                {".txt", "txt"},
                {".docx", "doc"}
            };
            string path = @"..\..\..\..\FolderForWatching";

            var sfw = new ServiseFileWatcher(path, rules, x=>Console.WriteLine(x));
            sfw.Run();

        }        
    }
}
