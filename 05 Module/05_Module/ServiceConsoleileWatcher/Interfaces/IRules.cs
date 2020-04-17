using ServiceConsoleileWatcher.Enums;

namespace ServiceConsoleileWatcher.Interfaces
{
    public interface IRule
    {
        public string FolderPath { get; set; }
        public string Rule { get; set; }
        public NamePrefixs NamePrefixs { get; set; }
    }
}
