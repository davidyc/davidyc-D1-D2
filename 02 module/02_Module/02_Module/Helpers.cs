using System;
using System.Collections.Generic;
using System.Text;

namespace Module2Task
{
    public class Helpers
    {
        public static void ConsoleInfo(string info)
        {
            Console.WriteLine(info);
        }

        public static void FileFound(string message)
        {
            Console.WriteLine(message + " was found");
        }

        public static void FolderFound(string message)
        {
            Console.WriteLine("Folder " + message + " was found");
        }
        public static void FileFiltered(string message)
        {
            Console.WriteLine(message + " was Filtered");
        }

        public static void FolderFiltered(string message)
        {
            Console.WriteLine("Folder " + message + " was Filtered");
        }


    }

}

