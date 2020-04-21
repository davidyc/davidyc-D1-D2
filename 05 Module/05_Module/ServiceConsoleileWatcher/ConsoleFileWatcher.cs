using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;
using WorkMessage = ServiceConsoleileWatcher.Resoures.WorkStatusStrings; 

namespace ServiceConsoleileWatcher
{
    public class ConsoleFileWatcher : IFileWatcher
    {
        private IEnumerable<string> path;
        public ICollection<FileSystemWatcher> FileSystemWatchers { get; set; }

        public ConsoleFileWatcher(IEnumerable<string> path)
        {
            this.path = path;
            FileSystemWatchers = new List<FileSystemWatcher>();

            foreach (var item in path)
            {
                FileSystemWatchers.Add(new FileSystemWatcher(item));
            }                  
        }

        private void SetFileSystemWatchers(Action<object, FileSystemEventArgs> onCreate)
        {
            foreach (var fileSystemWatcher in FileSystemWatchers)
            {
                fileSystemWatcher.NotifyFilter = NotifyFilters.CreationTime | NotifyFilters.FileName |
            NotifyFilters.DirectoryName;

                fileSystemWatcher.Created += new FileSystemEventHandler(onCreate);
                fileSystemWatcher.EnableRaisingEvents = true;
            }              
        }

        public void Run(Action<object, FileSystemEventArgs> onCreate)
        {
            SetFileSystemWatchers(onCreate);         
            Console.WriteLine(WorkMessage.StopWorkMessage);
            while (Console.ReadKey().Key != ConsoleKey.Escape) ;
        }
    }
}
