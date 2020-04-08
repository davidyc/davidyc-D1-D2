﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Module1Task
{
    public delegate void Info(string message);
    public class FileSystemVisitor : IEnumerable
    {
        private ICollection<string> filesFolders;
        private string startPath;
        private Func<string, bool> filteredFunc;
        private bool needStop;
        private bool excludeFile;
        private bool excludeFolder;

        public event Info StartEnd;
        public event Info FileFind;
        public event Info FolderFind;
        public event Info FileFindFitered;
        public event Info FolderFindFitered;


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
                        
            if (folders.Count == 0)
            {
                if (filteredFunc != null)
                {                    
                    if (filteredFunc(path) && !excludeFolder)
                    {
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
                foreach (var pat in folders)
                    GetFolderElements(pat);               
                AddFilesToCollection(path);
            }
        }
        void AddFilesToCollection(string path)
        {
            var files = Directory.GetFiles(path);
            foreach (var file in files)
            {
                if (filteredFunc != null)
                {               
                    var x = Path.GetFileName(file);
                    if (filteredFunc(x) && !excludeFile)
                    {
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
