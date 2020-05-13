using System;
using System.Collections.Generic;
using System.Text;

namespace Module_7.Interfaces
{
    public interface ICatalog
    {
        string ElementName { get; }
        void AddParsers(params IParserElement[] elementParsers);
        void AddWriters(params IWriteElement[] writers);
        void WriteTo(StringBuilder xml, IEnumerable<IEntity> entities);
        IEnumerable<IEntity> ReadFrom(StringBuilder xml);

    }
}

