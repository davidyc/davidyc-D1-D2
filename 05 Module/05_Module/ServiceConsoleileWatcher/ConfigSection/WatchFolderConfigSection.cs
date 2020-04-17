using ServiceConsoleileWatcher;
using System.Configuration;

namespace ServiceConsoleileWatcher
{
    public class WatchFolderConfigSection : ConfigurationSection
    {
        [ConfigurationProperty("Folders", IsDefaultCollection = true)]
        public FolderConfigSectionCollection Folders
        {
            get { return (FolderConfigSectionCollection)this["Folders"]; }
        }   
    }
}
