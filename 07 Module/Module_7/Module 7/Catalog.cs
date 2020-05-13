using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Module_7.Interfaces;

namespace Module_7
{
    public class Catalog : ICatalog
    {
        private readonly IDictionary<string, IParserElement> readElementParsers;
        private readonly IDictionary<string, IWriteElement> writeElemenWriters;

        public string ElementName => "catalog";
        public DateTime DateCreate { get; set; }


        public Catalog()
        {
            DateCreate = DateTime.UtcNow;
            readElementParsers = new Dictionary<string, IParserElement>();
            writeElemenWriters = new Dictionary<string, IWriteElement>();
        }
        


        public void AddParsers(params IParserElement[] elementParsers)
        {
            foreach (var item in elementParsers)
            {
                readElementParsers.Add(item.ElementName, item);
            }
        }

        public void AddWriters(params IWriteElement[] elementWriters)
        {
            foreach (var item in elementWriters)
            {
                writeElemenWriters.Add(item.ElementName, item);
            }
        }

        public void WriteTo(StringBuilder xml, IEnumerable<IEntity> entities)
        {           
            using (XmlWriter xmlWriter = XmlWriter.Create(xml, new XmlWriterSettings()))
            {
                xmlWriter.WriteStartDocument();
                xmlWriter.WriteStartElement(ElementName);
                xmlWriter.WriteAttributeString("datecreate",DateCreate.ToString("dd:MM:yyyy"));
                foreach (var item in writeElemenWriters)
                {
                    item.Value.WriteElement(xmlWriter);
                }
               // xmlWriter.WriteEndElement();
            }
        }

        public IEnumerable<IEntity> ReadFrom(StringBuilder xml)
        {
            return null;
        }
    }
}
