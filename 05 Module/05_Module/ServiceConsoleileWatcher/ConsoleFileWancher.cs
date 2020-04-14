using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ServiceConsoleileWatcher
{
    public class ConsoleFileWancher : IFileWatcer
    {
        public FileSystemWatcher FileSystemWatcher { get; set; }

        public void Run(Action<object, FileSystemEventArgs> onCreate)
        {
            FileSystemWatcher.NotifyFilter = NotifyFilters.CreationTime | NotifyFilters.FileName |
            NotifyFilters.DirectoryName;

            FileSystemWatcher.Created += new FileSystemEventHandler(onCreate);
            FileSystemWatcher.EnableRaisingEvents = true;

            Console.WriteLine("Press the Escape (Esc) key to quit");
            while (Console.ReadKey().Key != ConsoleKey.Escape) ;
        }
    }
}
