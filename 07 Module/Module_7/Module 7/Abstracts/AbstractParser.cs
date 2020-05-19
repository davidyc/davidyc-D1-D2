using Module_7.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Xml.Linq;

namespace Module_7.Abstracts
{
    public abstract class AbstractParser : IParserElement
    { 
        public abstract string ElementName { get; }

        protected DateTime ConvertDate(string dateInvariant)
        {
            return DateTime.ParseExact(dateInvariant, CultureInfo.InvariantCulture.DateTimeFormat.ShortDatePattern,
                CultureInfo.InvariantCulture.DateTimeFormat);
        }

        protected string GetAttributeValue(XElement element, string name)
        {

            if (string.IsNullOrEmpty(element?.Value))
            {
                throw new Exception($"{name}");
            }
            return element.Attribute(name).Value;
        }

        protected XElement GetElement(XElement parentElement, string name)
        {
            var element = parentElement.Element(name);
            if (string.IsNullOrEmpty(element?.Value))
            {
                throw new Exception($"Wrong struct no element {name}");
            }
            return element;
        }      
        public abstract IEntity ReadElement(XElement element);
    }
}
