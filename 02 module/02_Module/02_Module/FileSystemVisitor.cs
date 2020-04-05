using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Module1Task
{
    public delegate void Info(string message);
    public delegate void FindElement(string message, bool needStop, bool excludeFile, bool excludeFolder);
    public class FileSystemVisitor : IEnumerable
    {
        private ICollection<string> filesFolders;
        private string startPath;
        private Func<string, bool> filteredFunc;
        private bool needStop;
        private bool excludeFile;
        private bool excludeFolder;

        public event Info StartEnd;
        public event FindElement FileFind;
        public event FindElement FolderFind;
        public event Info FileorForderFilteredFind;
         
        public FileSystemVisitor(string path)
        {
            startPath = path;
            filesFolders = new List<string>();
            needStop = false;
            excludeFile = false;
            excludeFolder = false;

        }

        public FileSystemVisitor(string path, Func<string, bool> func, bool needStop = false, bool excludeFile = false, bool excludeFolder = false) : this(path)
        {
            filteredFunc = func;
            this.needStop = needStop;
            this.excludeFile = excludeFile;
            this.excludeFolder = excludeFolder;
        }
         
        void GetFolderElements(string path)
        {
            List<string> folders = Directory.GetDirectories(path).ToList<string>();

            if(filteredFunc != null) 
            {
                folders = folders.Where(filteredFunc).ToList<string>();
            }   

            if (folders.Count == 0)
            {
                filesFolders.Add(path);
                FolderFind?.Invoke(path, false, false, false);
                var files = Directory.GetFiles(path);
                foreach (var file in files)
                {
                    FileFind?.Invoke(file, false, false, false);
                    filesFolders.Add(file);
                }                                 
            }
            else
            {
                foreach (var pat in folders)
                    GetFolderElements(pat);                
                var files = Directory.GetFiles(path);
                foreach (var file in files)
                {
                    filesFolders.Add(file);
                    FileFind?.Invoke(file, false, false, false);
                }
                                
            }
        }

        public void GetAllElements()
        {
            StartEnd?.Invoke("Stard Search");
            GetFolderElements(startPath);
            StartEnd?.Invoke("End Search");           
        }
        public IEnumerator GetEnumerator()
        {
            foreach (var item in filesFolders)            
                yield return item;            
        }

    }
}
