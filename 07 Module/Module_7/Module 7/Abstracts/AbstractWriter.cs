using Module_7.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace Module_7.Abstracts
{
    public abstract class AbstractWriter : IWriteElement, IDate
    {
        public abstract string ElementName { get; }

        protected void AddElement(XElement element, string newElementName, string value)
        {
            var newElement = new XElement(newElementName, value);
            element.Add(newElement);
        }

        protected void AddAttribute(XElement element, string newElementName, string name, string surname)
        {
            var newElement = new XElement(newElementName);
            newElement.SetAttributeValue("Name", name);
            newElement.SetAttributeValue("Surname", surname);
            element.Add(newElement);
        }
        public string GetDate(DateTime date)
        {
            var day = date.Day < 10 ? $"0{date.Day}" : $"{date.Day}";
            var mouth = date.Month < 10 ? $"0{date.Month}" : $"{date.Month}";
            return $"{mouth}/{day}/{date.Year}";
        }
        public abstract void WriteElement(XmlWriter xmlWriter, IEntity entity);
    }
}
