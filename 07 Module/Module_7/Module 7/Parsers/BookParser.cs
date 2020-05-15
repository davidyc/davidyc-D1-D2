using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Module_7.Interfaces;
using Module_7.Models;

namespace Module_7.Parsers
{
    public class BookParser : IParserElement
    {
        public string ElementName => "Book";     

        protected string GetAttributeValue(XElement element, string name)
        {
            if (string.IsNullOrEmpty(element?.Value))
            {
                throw new Exception($"{name}");
            }
            return element.Attribute(name).Value;
        }

        XElement GetElement(XElement parentElement, string name)
        {
            var element = parentElement.Element(name);
            if (string.IsNullOrEmpty(element?.Value))
            {
                throw new Exception($"Wrong struct no element {name}");
            }

            return element;
        }

        public IEntity ReadElement(XElement element)
        {
            if (element == null)
            {
                throw new ArgumentNullException($"{nameof(element)} is null");
            }

            var book = new Book
            {
                Name = GetElement(element, "Name").Value,             
                PagesCount = int.Parse(GetElement(element, "PagesCount").Value),
                Note = GetElement(element, "Note").Value,
                PublicationCity = GetElement(element, "PublicationCity").Value,
                PublisherName = GetElement(element, "PublisherName").Value,
                PublishYear = int.Parse(GetElement(element, "PublishYear").Value),
                ISBN = GetElement(element, "ISBN").Value,

                Authors = element.Element("Authors").Elements().Select(c => new Author
                {
                    Name = c.Attribute("Name").Value,
                    Surname = c.Attribute("Surname").Value
                }).ToList()
            };

            //book.Authors = GetElement(element, "Authors").Elements().Select(c => new Author
            //{
            //    Name = GetAttributeValue(c, "Name"),
            //    Surname = GetAttributeValue(c, "Surname")
            //}).ToList();
             
            // тут как в патентах  там болле расписанно есди это первое увидел

            return book;
        }
    }
}