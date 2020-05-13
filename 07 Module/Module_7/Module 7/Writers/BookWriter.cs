using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using Module_7.Interfaces;
using Module_7.Models;

namespace Module_7.Writers
{
    public class BookWriter : IWriteElement
    {
        public string ElementName => "Book";

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
            Book book = entity as Book;
            if (book == null)
            {
                throw new ArgumentException($"provided {ElementName} is null or not of type {nameof(Book)}");
            }

            XElement element = new XElement(ElementName);
            AddElement(element, "Name", book.Name);
            foreach (var item in book.Authors)
            {
                AddAttribute(element, "Author", item.Name, item.Surname);
            }
            AddElement(element, "PublicationCity", book.PublicationCity);
            AddElement(element, "PublisherName", book.PublisherName);
            AddElement(element, "PublishYear", book.PublishYear.ToString());
            AddElement(element, "PagesCount", book.PagesCount.ToString());          
            AddElement(element, "Note", book.Note);
            AddElement(element, "ISBN", book.ISBN);

            element.WriteTo(xmlWriter);
        }
    }
}
