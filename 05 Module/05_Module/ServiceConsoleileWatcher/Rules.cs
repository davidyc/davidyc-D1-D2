using ServiceConsoleileWatcher.Enums;
using ServiceConsoleileWatcher.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ServiceConsoleileWatcher
{
    public class Rules : IRule
    {
        public string FolderPath { get; set; }
        public string Rule { get; set; }
        public NamePrefixs NamePrefixs { get; set; }
    }
}
