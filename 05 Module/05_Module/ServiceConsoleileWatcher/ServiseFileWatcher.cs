using ServiceConsoleileWatcher.Enums;
using ServiceConsoleileWatcher.Interfaces;
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

        private IEnumerable<IRule> rules;      
        private IFileWatcher fileSystemWatcher;       
        private Action<string> show;
        private string folderDefault;

        public ServiseFileWatcher(string folderDefault, IEnumerable<IRule> rules, IFileWatcher fileSystemWatcher,
            Action<string> show) 
        {
            this.folderDefault = folderDefault;
            this.fileSystemWatcher = fileSystemWatcher;
            this.rules = rules;
            this.show = show;
          
        }      

        private void OnCreate(object source, FileSystemEventArgs e)
        {
            show.Invoke(String.Format(WorkMessage.FileCreated, e.Name, DateTime.Now.ToString(Thread.CurrentThread.CurrentUICulture)));
            CheckRulesForMoveFile(e);
        }

        private void MoveFile(FileSystemEventArgs e, string endFolderName, NamePrefixs namePrefixs = NamePrefixs.none)
        {
            string fileName = e.Name;
            if (namePrefixs == NamePrefixs.date)
            {
                fileName = $"{DateTime.Now.ToLongDateString()}_{fileName}";     
            }
            else if (namePrefixs == NamePrefixs.count)
            {
                fileName = $"({count})_{fileName}";
                count++;                
            }   
            
           
            if (File.Exists(Path.Combine(endFolderName, fileName)))
            {
                Random rnd = new Random();
                fileName = $"{rnd.Next(100000,999999)}_{fileName}";
            }

            try
            {
                File.Move(e.FullPath, Path.Combine(endFolderName, fileName));
                show.Invoke(String.Format(WorkMessage.MoveFile, e.Name, endFolderName));
            }
            catch
            {
                show.Invoke(String.Format(WorkMessage.CannotMove, e.Name));
            }           
        }
       
        private void CheckRulesForMoveFile(FileSystemEventArgs e)
        {
            foreach (var rule in rules)
            {
                var rgx = new Regex(rule.Rule);
                if (rgx.IsMatch(e.Name))
                {
                    show.Invoke(String.Format(WorkMessage.RuleFound, rule.Rule));
                    MoveFile(e, rule.FolderPath, rule.NamePrefixs);             
                    break;
                }
            }

            if (File.Exists(e.FullPath))
            {
                show.Invoke(WorkMessage.RuleNotFound);
                MoveFile(e, folderDefault);
            }
        }
        public void Run()
        {
            fileSystemWatcher.Run(OnCreate);
        }

    }
}
