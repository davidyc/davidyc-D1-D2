using ServiceConsoleileWatcher;
using ServiceConsoleileWatcher.Enums;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Threading;

namespace ServiceConsoleileWatcher
{  
    class Program
    {
        static void Main(string[] args)
        {
            var culture  = ConfigurationManager.AppSettings.Get("language");
            var defaultFolder = ConfigurationManager.AppSettings.Get("defaulfFolder");

            WatchFolderConfigSection configSection = (WatchFolderConfigSection)ConfigurationManager.GetSection("WatchFolderConfigSection");
            FolderConfigSectionCollection folders = configSection.Folders;

            var listPath = new List<string>();
            foreach (PathSectionElement folder in folders)
            {
                listPath.Add(folder.Path);
            }

            MoveRulesConfigSection ruleConfigSection = (MoveRulesConfigSection)ConfigurationManager.GetSection("MoveRulesConfigSection");
            var rulesConfig = ruleConfigSection.Rules;

            var rules = new List<Rules>();
            foreach (RuleSectionElement rule in rulesConfig)
            {
                rules.Add(new Rules { FolderPath = rule.FolderPath, Rule = rule.Rule, NamePrefixs = rule.NamePrefixs });
            }

            Thread.CurrentThread.CurrentUICulture = new CultureInfo(culture);

            var cfw = new ConsoleFileWancher(listPath);
            var sfw = new ServiseFileWatcher(defaultFolder, rules, cfw, x => Console.WriteLine(x));            
            sfw.Run();

        }
    }
}
