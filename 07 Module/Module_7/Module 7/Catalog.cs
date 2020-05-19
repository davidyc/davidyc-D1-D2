using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;
using System.Xml;
using System.Xml.Linq;
using Module_7.Interfaces;

namespace Module_7
{
    public class Catalog : ICatalog, IDate
    {
        private readonly IDictionary<string, IParserElement> readElementParsers;
        private readonly IDictionary<string, IWriteElement> writeElemenWriters;

        public string GetDate(DateTime date)
        {
            var day = date.Day < 10 ? $"0{date.Day}" : $"{date.Day}";
            var mouth = date.Month < 10 ? $"0{date.Month}" : $"{date.Month}";
            return $"{mouth}/{day}/{date.Year}";
        }

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
                xmlWriter.WriteAttributeString("datecreate",GetDate(DateCreate));
                if(entities != null)
                {
                    foreach (var item in entities)
                    {
                        var x = writeElemenWriters[item.GetType().Name.ToString()];
                        x.WriteElement(xmlWriter, item);
                    }
                }               
                xmlWriter.WriteEndElement();
            }
        }

        public IEnumerable<IEntity> ReadFrom(TextReader textReader)     
        {     
            using (XmlReader xmlReader = XmlReader.Create(textReader, new XmlReaderSettings
            {
                    IgnoreWhitespace = true,
                    IgnoreComments = true
                }))
                {
                    xmlReader.ReadToFollowing(ElementName);                 
                    xmlReader.ReadStartElement();

                    do
                    {
                        while (xmlReader.NodeType == XmlNodeType.Element)
                        {
                            var node = XNode.ReadFrom(xmlReader) as XElement;
                            IParserElement parser;
                            if (readElementParsers.TryGetValue(node.Name.LocalName, out parser))
                            {
                                yield return parser.ReadElement(node);
                            }
                            else
                            {
                                throw new Exception($"Founded unknown element tag: {node.Name.LocalName}");
                            }
                        }
                    } while (xmlReader.Read());
                }                
            }

       
    }
}
