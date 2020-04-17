
using System.Configuration;

namespace ServiceConsoleileWatcher
{
    public class PathSectionElement : ConfigurationElement
    {
        [ConfigurationProperty("Path")]
        public string Path
        {
            get { return (string)this["Path"]; }
        }
    }
   
}