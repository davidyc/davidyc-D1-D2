using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading;
using System.Xml.Linq;
using Module_7.Abstracts;
using Module_7.Interfaces;
using Module_7.Models;

namespace Module_7.Parsers
{
    public class NewsparperParser : AbstractParser
    {
        public override string ElementName => "Newspaper";

        public override IEntity ReadElement(XElement element)
        {
            if (element == null)
            {
                throw new ArgumentNullException($"{nameof(element)} is null");
            }

            var newsPaper = new Newspaper
            {
                Name = GetElement(element, "Name").Value,
                PublicationCity = GetElement(element, "PublicationCity").Value,
                PublisherName = GetElement(element, "PublisherName").Value,
                PublishYear = int.Parse(GetElement(element, "PublishYear").Value),
                PagesCount = int.Parse(GetElement(element, "PagesCount").Value),
                Note = GetElement(element, "Note").Value,
                Number = int.Parse(GetElement(element, "Number").Value),
                Date = ConvertDate(GetElement(element, "Date").Value),
                ISSN = GetElement(element, "ISSN").Value
            };    
            return newsPaper;
        }
    }
}
