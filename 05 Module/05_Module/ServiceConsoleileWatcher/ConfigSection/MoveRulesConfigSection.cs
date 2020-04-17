

using ServiceConsoleileWatcher;
using System.Configuration;

namespace ServiceConsoleileWatcher
{
    public class MoveRulesConfigSection : ConfigurationSection
    {
        [ConfigurationProperty("Rules", IsDefaultCollection = true)]
        public RulesConfigSectionCollection Rules
        {
            get { return (RulesConfigSectionCollection)this["Rules"]; }
        }
    }
}
