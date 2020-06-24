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
            var saver = new ContentSaver(@"C:\Sergey Davydov\C#\EPAM Mentor program\Module 13");
            var logger = new ConsoleLogger();
            var maxDeepLevel = 2;
            var sd = new SiteContentDownloader(saver, logger, maxDeepLevel, true);
            sd.FileExetention = new List<string> { ".jpg" };
            sd.LoadFromURL("http://davidyc.pythonanywhere.com/");          
            Console.Read();
        }
    }   
}
