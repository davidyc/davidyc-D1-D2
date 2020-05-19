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
    public class PatentWriter : AbstractWriter
    {
        public override string ElementName => "Patent";
  
        public override void WriteElement(XmlWriter xmlWriter, IEntity entity)
        {
            Patent patent = entity as Patent;
            if (patent == null)
            {
                throw new ArgumentException($"provided {ElementName} is null or not of type {nameof(Patent)}");
            }

            XElement element = new XElement(ElementName);
            AddElement(element, "Name", patent.Name);           
            var Creators = new XElement("Creators");
            foreach (var item in patent.Creators)
            {
                AddAttribute(Creators,  "Creator", item.Name, item.Surname);
            }
            element.Add(Creators);
            AddElement(element, "RegistrationNumber", patent.RegistrationNumber);
            AddElement(element, "PagesCount", patent.PagesCount.ToString());
            AddElement(element, "Note", patent.Note);
            AddElement(element, "FilingDate", GetDate(patent.FilingDate));
            AddElement(element, "PublishDate", GetDate(patent.PublishDate));
            element.WriteTo(xmlWriter);
        }
    }
}
