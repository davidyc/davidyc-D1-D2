using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using WorkMessage = ServiceConsoleileWatcher.Resoures.WorkStatusStrings;

namespace ServiceConsoleileWatcher
{
   

    public class ServiseFileWatcher
    {
        static int count = 0;

        private IDictionary<string, string> rules;
        private IEnumerable<string> path;
        private IFileWatcer fileSystemWatcher;       
        private Action<string> show;
        private bool addDateMove;
        private bool addCount;

        public static string name_folder_for_move_file = @"C:\";

        public ServiseFileWatcher(IEnumerable<string> path, IDictionary<string,string> rules, Action<string> show, 
            bool addDate = false, bool addCount = false) 
        {
            this.path = path;
            fileSystemWatcher = new ConsoleFileWancher(path); // передавать сюда бул что бы было видно что оно работает
            this.rules = rules;
            this.show = show;
            this.addCount = addCount;
            this.addDateMove = addDate;
        }      

        private void OnCreate(object source, FileSystemEventArgs e)
        {
            show.Invoke(String.Format(WorkMessage.FileCreated, e.Name, DateTime.Now.ToString(Thread.CurrentThread.CurrentUICulture)));
            CheckRulesForMoveFile(e);
        }

        private void MoveFile(FileSystemEventArgs e, string endFolderName)
        {
            string fileName = e.Name;
            if (addDateMove)
                fileName = $"{DateTime.Now.ToLongDateString()}_{fileName}";
            if (addCount)
            {
                fileName = $"({count})_{fileName}";
                count++;
            }

            File.Move(e.FullPath, Path.Combine(endFolderName, fileName));
            show.Invoke(String.Format(WorkMessage.MoveFile, e.Name, endFolderName));
        }
       
        private void CheckRulesForMoveFile(FileSystemEventArgs e)
        {
            foreach (var item in rules)
            {
                var rgx = new Regex(item.Key);
                if (rgx.IsMatch(e.Name))
                {
                    show.Invoke(String.Format(WorkMessage.RuleFound, item.Key));
                    MoveFile(e, item.Value);             
                    break;
                }
            }

            if (File.Exists(e.FullPath))
            {
                show.Invoke(WorkMessage.RuleNotFound);
                MoveFile(e, name_folder_for_move_file);
            }
        }
        public void Run()
        {
            fileSystemWatcher.Run(OnCreate);
        }

    }
}
