using SiteDownloader.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDownLoaderClient
{
    public class ConsoleLogger : ILogger
    {
        public void MakeLog(string text)
        {
            Console.WriteLine(text);
        }
    }
}
