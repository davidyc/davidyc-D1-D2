using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.Linq;
using System.Text;

namespace Module2Task
{
    public delegate void Info(string message);
    public class FileSystemVisitor : IEnumerable
    {
        private readonly IFileSystem fileSystem;

        private ICollection<string> filesFolders;
        private string startPath;
        private Func<string, bool> filteredFunc;
        private bool needStop;
        private bool onlyOneFile;
        private bool excludeFile;
        private bool excludeFolder;       

        public event Info StartEnd;
        public event Info FileFind;
        public event Info FolderFind;
        public event Info FileFindFitered;
        public event Info FolderFindFitered;


        public FileSystemVisitor(string path, IFileSystem fileSystem = null)
        {
         
            this.fileSystem = fileSystem;            
            startPath = path;
            filesFolders = new List<string>();
            needStop = false;
            excludeFile = false;
            excludeFolder = false;
            onlyOneFile = false;
        }

        public FileSystemVisitor(string path, Func<string, bool> func, bool onlyOneFile = false, bool excludeFile = false, bool excludeFolder = false, IFileSystem fileSystem = null) : this(path, fileSystem)
        {
            filteredFunc = func;
            this.onlyOneFile = onlyOneFile;
            this.excludeFile = excludeFile;
            this.excludeFolder = excludeFolder;
        }
         
        void GetFolderElements(string path)
        {
            string[] folders;
            // пойдет ли такой подход пусити это нужно только для тестов или лучше сделать какую то обертку сделать
            if (fileSystem == null)
                folders = Directory.GetDirectories(path);
            else
                folders = fileSystem.Directory.GetDirectories(path);
                      
            if (folders.Length == 0)
            {
                if (filteredFunc != null) // придумал только такой способ прерывания
                {
                    if (filteredFunc(path) && !excludeFolder && !needStop)
                    {
                        if (onlyOneFile)
                            needStop = true;
                        filesFolders.Add(path);
                        FolderFindFitered?.Invoke(path);
                    }
                    AddFilesToCollection(path);
                }
                else
                {
                    filesFolders.Add(path);
                    FolderFind?.Invoke(path);
                    AddFilesToCollection(path);
                }
            }
            else
            {
                foreach (var folder in folders)
                    GetFolderElements(folder);
                AddFilesToCollection(path);
            }
            
          
        }
        void AddFilesToCollection(string path)
        {
            string[] files;
            string filemane;
            if (fileSystem==null)
                files = Directory.GetFiles(path);
            else
                files = fileSystem.Directory.GetFiles(path);
            
            foreach (var file in files)
            {
                if (filteredFunc != null)
                {                    
                    if (fileSystem == null)
                        filemane = Path.GetFileName(file);
                    else
                        filemane = fileSystem.Path.GetFileName(file);
               
                    if (filteredFunc(filemane) && !excludeFile && !needStop)
                    {
                        if (onlyOneFile)
                            needStop = true;
                        filesFolders.Add(file);
                        FileFindFitered?.Invoke(file);
                    }                   
                }
                else
                {
                    filesFolders.Add(file);
                    FileFind?.Invoke(file);
                }
            }
        }

        public ICollection<string> GetAllElements()
        {
            StartEnd?.Invoke("Stard Search");
            GetFolderElements(startPath);
            StartEnd?.Invoke("End Search");
            return filesFolders;
        }
        public IEnumerator GetEnumerator()
        {
            foreach (var item in filesFolders)            
                yield return item;            
        }

    }    
}
