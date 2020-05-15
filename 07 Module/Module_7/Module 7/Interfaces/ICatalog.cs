using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace Module_7.Interfaces
{
    public interface ICatalog
    {
        string ElementName { get; }
        void AddParsers(params IParserElement[] elementParsers);
        void AddWriters(params IWriteElement[] writers);
        void WriteTo(StringBuilder xml, IEnumerable<IEntity> entities);
        IEnumerable<IEntity> ReadFrom(TextReader textReader);

    }
}

