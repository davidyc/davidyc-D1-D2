using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace Module1Task
{
    class Program
    {
        static void Main(string[] args)
        {           
           // FileSystemVisitor fsv = new FileSystemVisitor(@"..\..\..\..\FIleVisitor", x=>x.Contains("new"), findOnlyOne:true);
            FileSystemVisitor fsv = new FileSystemVisitor(@"..\..\..\..\FIleVisitor");
           
            fsv.StartEnd += Helpers.ConsoleInfo;
            fsv.FileFind += Helpers.FileFound;
            fsv.FolderFind += Helpers.FolderFound;
            fsv.GetAllElements();

            foreach (var item in fsv)
            {
                Console.WriteLine(item);
            }
        }
    }
}
