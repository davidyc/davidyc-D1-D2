

using ServiceConsoleileWatcher;
using System.Configuration;

namespace ServiceConsoleileWatcher
{

    [ConfigurationCollection(typeof(PathSectionElement), AddItemName = "Rule")]
    public class RulesConfigSectionCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new RuleSectionElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((RuleSectionElement)element);
        }
    }
}
