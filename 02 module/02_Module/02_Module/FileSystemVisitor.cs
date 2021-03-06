﻿using System;
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

            if (fileSystem == null)
                fileSystem = new FileSystem();
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
         
        void AddPathToCollection(string path, Info eventInfo )
        {
            filesFolders.Add(path);
            eventInfo?.Invoke(path);
        }

        void GetFolderElements(string path)
        {
            string[] folders;

            if (needStop)
                return;
                        
            folders = fileSystem.Directory.GetDirectories(path);
                      
            if (folders.Length == 0)
            {
                if (filteredFunc != null)
                {
                    if (filteredFunc(path) && !excludeFolder)
                    {
                        if (onlyOneFile)
                            needStop = true;
                        AddPathToCollection(path, FolderFindFitered);
                    }                   
                }
                else
                {
                    AddPathToCollection(path, FolderFind);      
                }                
            }
            else
            {
                foreach (var folder in folders)
                    GetFolderElements(folder);               
            }

            AddFilesToCollection(path);
        }
        void AddFilesToCollection(string path)
        {
            string[] files;
            string filemane;

            if (needStop || excludeFile)
                return;
           
            files = fileSystem.Directory.GetFiles(path);
            
            foreach (var file in files)
            {
                if (filteredFunc != null)
                {                    
                    filemane = fileSystem.Path.GetFileName(file);
               
                    if (filteredFunc(filemane))
                    {
                        if (onlyOneFile)
                            needStop = true;
                        AddPathToCollection(file, FileFindFitered);
                    }                   
                }
                else
                {
                    AddPathToCollection(file, FileFind);
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
