using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using Module_7.Interfaces;
using Module_7.Models;

namespace Module_7.Writers
{
    public class NewspaperWriter : IWriteElement
    {
        public string ElementName => "Newspaper";

        void AddElement(XElement element, string newElementName, string value)
        {
            var newElement = new XElement(newElementName, value);
            element.Add(newElement);
        }

        public void WriteElement(XmlWriter xmlWriter, IEntity entity)
        {
            Newspaper newspaper = entity as Newspaper;
            if (newspaper == null)
            {
                throw new ArgumentException($"provided {ElementName} is null or not of type {nameof(Newspaper)}");
            }

            XElement element = new XElement(ElementName);
            AddElement(element, "Name", newspaper.Name);
           
            AddElement(element, "PublicationCity", newspaper.PublicationCity);
            AddElement(element, "PublisherName", newspaper.PublisherName);
            AddElement(element, "PublishYear", newspaper.PublishYear.ToString());
            AddElement(element, "PagesCount", newspaper.PagesCount.ToString());
            AddElement(element, "Note", newspaper.Note);
            AddElement(element, "ISSN", newspaper.ISSN);
            AddElement(element, "Number", newspaper.Number.ToString());           
            AddElement(element, "Date", newspaper.Date.ToString("dd:MM:yyyy"));

            element.WriteTo(xmlWriter);
       
        }
    }
}
