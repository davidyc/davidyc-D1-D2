using Module_7.Interfaces;
using Module_7.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Module_7.Parsers
{
    public class PatentParser : IParserElement
    {
        public string ElementName => "Patent";

        DateTime ConvertDate(string dateInvariant)
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

            var patent = new Patent
            {
                Name = GetElement(element, "Name").Value,
                RegistrationNumber = GetElement(element, "RegistrationNumber").Value,
                PagesCount = int.Parse(GetElement(element, "PagesCount").Value),
                Note = GetElement(element, "Note").Value,
                FilingDate = ConvertDate(GetElement(element, "FilingDate").Value),
                PublishDate = ConvertDate(GetElement(element, "PublishDate").Value),
                Creators = element.Element("Creators").Elements().Select(c => new Creator
                {
                    Name = c.Attribute("Name").Value,
                    Surname = c.Attribute("Surname").Value
                }).ToList()
            };



            //patent.Creators = GetElement(element, "Creators").Elements().Select(c => new Creator
            //{
            //    Name = GetAttributeValue(c, "Name"),
            //    Surname = GetAttributeValue(c, "Surname")
            //}).ToList();

            // это кусок кода остался потому что через GetElement с проверкой не идет так оно падает с 
            //Wrong struct no element Creators хотя в аргумент приходит  parentElement 
            //такое вроде есть  
            //< Patent >
            //  < Name > Patent </ Name >
            //  < Creators >
            //      < Creator Name = "Creator1" Surname = "Surname1" />   
            //      < Creator Name = "Creator2" Surname = "Surname2" />      
            //  </ Creators >      
            //    < RegistrationNumber > P123456789 </ RegistrationNumber >      
            //    < PagesCount > 10 </ PagesCount >      
            //    < Note > Non </ Note >      
            //    < FilingDate > 05 / 12 / 2020 </ FilingDate >      
            //    < PublishDate > 05 / 12 / 2020 </ PublishDate >
            //  </ Patent >

            //Если этот комментарии осталс значит я не разобрался ну или забыл вернуться можешь посмотреть? 
            // где то же я туплю


            return patent;
        }
    }

}