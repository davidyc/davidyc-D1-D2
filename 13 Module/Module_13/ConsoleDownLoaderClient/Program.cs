using SiteDownloader;
using SiteDownloader.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDownLoaderClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var saver = new ContentSaver(@"..\..\..\..\Target Folder");
            var logger = new ConsoleLogger();
            var maxDeepLevel = 2;
            var sd = new SiteContentDownloader(saver, logger, maxDeepLevel, true);
            sd.FileExetention = new List<string> { ".jpg", ".png" };
            sd.LoadFromURL("https://www.google.com/");          
            Console.Read();
        }
    }   
}
