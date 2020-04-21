using System;
using System.Collections.Generic;
using System.IO;


namespace ServiceConsoleileWatcher
{
    public interface IFileWatcher
    {
        public ICollection<FileSystemWatcher> FileSystemWatchers { get; set; }
        public void Run(Action<object, FileSystemEventArgs> onCreate);       
    }
}
