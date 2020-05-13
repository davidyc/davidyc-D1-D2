using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Module_7.Interfaces
{
    public interface IWriteElement
    {
        string ElementName { get; }
        void WriteElement(XmlWriter xmlWriter);
    }
}
