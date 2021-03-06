﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Abstractions.TestingHelpers;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace Module2Task
{
    class Program
    {
        static void Main(string[] args)
        {                       
            FileSystemVisitor fsv = new FileSystemVisitor(@"..\..\..\..\FIleVisitor",
                x => x.Contains("New"), onlyOneFile:true);
            //FileSystemVisitor fsv = new FileSystemVisitor(@"..\..\..\..\FIleVisitor");
           
            fsv.StartEnd += Helpers.ConsoleInfo;
            fsv.FileFind += Helpers.FileFound;
            fsv.FolderFind += Helpers.FolderFound;
            fsv.FileFindFitered += Helpers.FileFiltered;
            fsv.FolderFindFitered += Helpers.FolderFiltered;
            fsv.GetAllElements();

            foreach (var item in fsv)
            {
                Console.WriteLine(item);
            }
        }
    }
}
