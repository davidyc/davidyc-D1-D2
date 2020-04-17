using ServiceConsoleileWatcher.ConfigSection;
using ServiceConsoleileWatcher.Enums;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Threading;

namespace ServiceConsoleileWatcher
{    
    class Program
    {
        static void Main(string[] args)
        {
            var culture  = ConfigurationManager.AppSettings.Get("language");
            var defaultFolder = ConfigurationManager.AppSettings.Get("defaulfFolder");

            WatchFolderConfigSection configSection = (WatchFolderConfigSection)ConfigurationManager.GetSection("WatchFolderConfigSection");
            FolderConfigSectionCollection folders = configSection.Folders;

            var listPath = new List<string>();
            foreach (PathSectionElement folder in folders)
            {
                listPath.Add(folder.Path);
            }
            
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(culture);

            IEnumerable<Rules> rules = new List<Rules>()
            {
                new Rules { FolderPath = @"..\..\..\..\FolderForWatching\txt", Rule = ".txt" , NamePrefixs = NamePrefixs.count},
                new Rules { FolderPath = @"..\..\..\..\FolderForWatching\doc", Rule = ".docx" , NamePrefixs = NamePrefixs.date}
            };          

            var cfw = new ConsoleFileWancher(listPath);
            var sfw = new ServiseFileWatcher(defaultFolder, rules, cfw, x => Console.WriteLine(x));            
            sfw.Run();

        }
    }
}
