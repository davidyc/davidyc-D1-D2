using System;
using System.Collections.Generic;
using System.Text;

namespace Module1Task
{
    public class Helpers
    {
        public static void ConsoleInfo(string info)
        {
            Console.WriteLine(info);
        }

        public static void FileFound(string message, bool needStop = false, bool excludeFile = false, bool excludeFolder = false)
        {
            Console.WriteLine(message + " was found");
        }

        public static void FolderFound(string message, bool needStop = false, bool excludeFile = false, bool excludeFolder = false)
        {
            Console.WriteLine("Folder " + message + " was found");
        }


    }
}
