using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ServiceConsoleileWatcher
{
    public class ServiseFileWatcher
    {
        private IDictionary<string, string> rules;
        private string path;
        //private IFileWatcer fileSystemWatcher; //позже добавить обертку
        private FileSystemWatcher fileSystemWatcher;
        private Action<string> show;

        public ServiseFileWatcher(string path, IDictionary<string,string> rules, Action<string> show) 
        {
            this.path = path;
            fileSystemWatcher = new FileSystemWatcher();
            fileSystemWatcher.Path = path;
            this.rules = rules;
            this.show = show;
        }
      

        private void OnCreate(object source, FileSystemEventArgs e)
        {
            show.Invoke("File: " + e.FullPath + " " + e.ChangeType);
            MoveFile(e);
        }

        private void MoveFile(FileSystemEventArgs e)
        {
            foreach (var item in rules)
            {
                if (e.Name.Contains(item.Key))
                {
                    show.Invoke($"Rules {item.Key} was found ");
                    File.Move(e.FullPath, Path.Combine(path, item.Value, e.Name));
                    show.Invoke($"File {e.Name} was moved to {Path.Combine(path, item.Value, e.Name)}");
                    break;
                }
            }

            if (File.Exists(e.FullPath))
            {
                show.Invoke($"Rules wasn't found");
                File.Move(e.FullPath, Path.Combine(path, "default", e.Name));
                show.Invoke($"File {e.Name} was moved to {Path.Combine(path, "default", e.Name)}");
            }
        }
        public void Run()
        {
            //fileSystemWatcher.Run(OnCreate);
            fileSystemWatcher.NotifyFilter = NotifyFilters.CreationTime | NotifyFilters.FileName |
            NotifyFilters.DirectoryName;

            fileSystemWatcher.Created += new FileSystemEventHandler(OnCreate);
            fileSystemWatcher.EnableRaisingEvents = true;

            Console.WriteLine("Press the Escape (Esc) key to quit");
            while (Console.ReadKey().Key != ConsoleKey.Escape) ;
        }

    }
}
