using ServiceConsoleileWatcher.Enums;
using System.Configuration;

namespace ServiceConsoleileWatcher
{
    public class RuleSectionElement : ConfigurationElement
    {
        [ConfigurationProperty("FolderPath")]
        public string FolderPath
        {
            get { return (string)this["FolderPath"]; }
        }
        [ConfigurationProperty("Rule")]
        public string Rule
        {
            get { return (string)this["Rule"]; }
        }
        [ConfigurationProperty("NamePrefixs")]
        public NamePrefixs NamePrefixs
        {
            get { return (NamePrefixs)this["NamePrefixs"]; }
        }
    }

}
