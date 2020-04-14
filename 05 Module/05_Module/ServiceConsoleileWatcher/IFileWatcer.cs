using System;
using System.IO;


namespace ServiceConsoleileWatcher
{
    interface IFileWatcer
    {
        public FileSystemWatcher FileSystemWatcher { get; set; }
        public void Run(Action<object, FileSystemEventArgs> onCreate);
    }
}
