using ServiceConsoleileWatcher.ConfigSection;
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

            WatchFolderConfigSection configSection = (WatchFolderConfigSection)ConfigurationManager.GetSection("WatchFolderConfigSection");
            FolderConfigSectionCollection folders = configSection.Folders;


            var tmpListPath = new List<string>();
            foreach (PathSectionElement folder in folders)
            {
                tmpListPath.Add(folder.Path);
            }

            Thread.CurrentThread.CurrentUICulture = new CultureInfo(culture);
            Dictionary<string, string> rules = new Dictionary<string, string>()
            {
                {".txt", @"C:\Sergey Davydov\C#\EPAM Mentor program\davidyc-D1-D2\05 Module\05_Module\FolderForWatching\txt"},
                { ".docx", @"C:\Sergey Davydov\C#\EPAM Mentor program\davidyc-D1-D2\05 Module\05_Module\FolderForWatching\doc"}
            };

           

            var sfw = new ServiseFileWatcher(tmpListPath.ToArray(), rules, x => Console.WriteLine(x), addCount:true);
            ServiseFileWatcher.name_folder_for_move_file = @"C:\Sergey Davydov\C#\EPAM Mentor program\davidyc-D1-D2\05 Module\05_Module\FolderForWatching\default";
            sfw.Run();

        }
    }
}
