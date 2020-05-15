using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace Module_7.Interfaces
{
    public interface IParserElement
    {
        string ElementName { get; }
        IEntity ReadElement(XElement element);
    }
}
