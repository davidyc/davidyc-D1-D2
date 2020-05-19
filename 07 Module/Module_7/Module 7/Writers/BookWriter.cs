using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using Module_7.Abstracts;
using Module_7.Interfaces;
using Module_7.Models;

namespace Module_7.Writers
{
    public class BookWriter : AbstractWriter
    {
        public override string ElementName => "Book";
        public override void  WriteElement(XmlWriter xmlWriter, IEntity entity)
        {
            Book book = entity as Book;
            if (book == null)
            {
                throw new ArgumentException($"provided {ElementName} is null or not of type {nameof(Book)}");
            }

            XElement element = new XElement(ElementName);
            AddElement(element, "Name", book.Name);
            var authors = new XElement("Authors");
            foreach (var item in book.Authors)
            {
                AddAttribute(authors, "Author", item.Name, item.Surname);
            }
            element.Add(authors);
         
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
