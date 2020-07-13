using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Task1.Models;

namespace Task1
{
    class Program
    {
        static void Main(string[] args)
        {
            var baseXMLParse = "..\\..\\XML\\";
            var nameFolderNewXML = "NewXML";
            XmlSerializer serializer = new XmlSerializer(typeof(Catalog));

            Catalog catalog;
            using (var fileStream = File.OpenRead(baseXMLParse + "books.xml"))
            {
                catalog = (Catalog)serializer.Deserialize(fileStream);
            }

            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, Catalog.XmlNamespace);
            using (var xmlWriter = XmlWriter.Create($"{baseXMLParse}\\{nameFolderNewXML}\\books.xml", new XmlWriterSettings
            {
                Encoding = Encoding.UTF8,
                Indent = true
            }))
            {
                serializer.Serialize(xmlWriter, catalog, namespaces);
            }
        }
    }
}
