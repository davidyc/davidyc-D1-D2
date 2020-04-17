using System.Configuration;

namespace ServiceConsoleileWatcher
{

    [ConfigurationCollection(typeof(PathSectionElement), AddItemName = "Folder")]
    public class FolderConfigSectionCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new PathSectionElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((PathSectionElement)element).Path;
        }
    }
}
