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
            var sd = new SiteContentDownloader(new Saver());
            sd.LoadFromURL("https://ru.wikipedia.org/wiki/%D0%A1%D0%B0%D0%B9%D1%82");
            Console.WriteLine();
        }
    }

    public class Saver : IContentSaver
    {
        public void SaveFile(Uri uri, Stream fileStream)
        {
            throw new NotImplementedException();
        }

        public void SaveHtmlDocument(Uri uri, string name, Stream documentStream)
        {
            throw new NotImplementedException();
        }
    }
}
