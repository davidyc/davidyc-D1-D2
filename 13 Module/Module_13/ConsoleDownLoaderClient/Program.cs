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
            var sd = new SiteContentDownloader(new Saver(), 2);
            sd.LoadFromURL("http://davidyc.pythonanywhere.com/");          
            Console.Read();
        }
    }

    public class Saver : IContentSaver
    {
        public void SaveFile(Uri uri, Stream fileStream)
        {
            Console.Write("FIle -> ");
            Console.WriteLine(uri.AbsoluteUri);
        }

        public void SaveHtmlDocument(Uri uri, string name, Stream documentStream)
        {
            Console.WriteLine(name);
        }
    }
}
