using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using Module_7.Interfaces;
using Module_7.Models;

namespace Module_7.Writers
{
    public class PatentWriter : IWriteElement
    {
        public string ElementName => "Patent";
        void AddElement(XElement element, string newElementName, string value)
        {
            var newElement = new XElement(newElementName, value);
            element.Add(newElement);
        }

        void AddAttribute(XElement element, string newElementName, string name, string surname)
        {
            var newElement = new XElement(newElementName);
            newElement.SetAttributeValue("Name", name);
            newElement.SetAttributeValue("Surname", surname);
            element.Add(newElement);
        }
        public void WriteElement(XmlWriter xmlWriter, IEntity entity)
        {
            Patent patent = entity as Patent;
            if (patent == null)
            {
                throw new ArgumentException($"provided {ElementName} is null or not of type {nameof(Patent)}");
            }

            XElement element = new XElement(ElementName);
            AddElement(element, "Name", patent.Name);
            foreach (var item in patent.Creators)
            {
                AddAttribute(element, "Creator", item.Name, item.Surname);
            }
            AddElement(element, "RegistrationNumber", patent.RegistrationNumber);
            AddElement(element, "PagesCount", patent.PagesCount.ToString());
            AddElement(element, "Note", patent.Note);
            AddElement(element, "FilingDate", patent.FilingDate.ToString("dd:MM:yyyy"));
            AddElement(element, "PublishDate", patent.PublishDate.ToString("dd:MM:yyyy"));
            element.WriteTo(xmlWriter);
        }
    }
}
