using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;
using WorkMessage = ServiceConsoleileWatcher.Resoures.WorkStatusStrings;

namespace ServiceConsoleileWatcher
{
   

    public class ServiseFileWatcher
    {
        private IDictionary<string, string> rules;
        private string path;
        private IFileWatcer fileSystemWatcher;       
        private Action<string> show;

        public static string name_folder_for_move_file = "default";

        public ServiseFileWatcher(string path, IDictionary<string,string> rules, Action<string> show) 
        {
            this.path = path;
            fileSystemWatcher = new ConsoleFileWancher(path);            
            this.rules = rules;
            this.show = show;
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("ru-RU");
        }      

        private void OnCreate(object source, FileSystemEventArgs e)
        {
            show.Invoke(String.Format(WorkMessage.FileCreated, e.Name, DateTime.Now.ToString(Thread.CurrentThread.CurrentUICulture)));
            MoveFile(e);
        }

        private void MoveFile(FileSystemEventArgs e)
        {
            foreach (var item in rules)
            {
                if (e.Name.Contains(item.Key))
                {
                    show.Invoke(String.Format(WorkMessage.RuleFound, item.Key));
                    File.Move(e.FullPath, Path.Combine(path, item.Value, e.Name));
                    show.Invoke(String.Format(WorkMessage.MoveFile, e.Name, Path.Combine(path, item.Value, e.Name)));
                    break;
                }
            }

            if (File.Exists(e.FullPath))
            {
                show.Invoke(WorkMessage.RuleNotFound);
                File.Move(e.FullPath, Path.Combine(path, name_folder_for_move_file, e.Name));
                show.Invoke(String.Format(WorkMessage.MoveFile, e.Name, Path.Combine(path, name_folder_for_move_file,
                    e.Name)));
            }
        }
        public void Run()
        {
            fileSystemWatcher.Run(OnCreate);
        }

    }
}
