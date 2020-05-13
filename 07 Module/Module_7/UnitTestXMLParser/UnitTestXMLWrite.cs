using System;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Module_7;
using Module_7.Models;
using Module_7.Writers;

namespace UnitTestXMLParser
{
    [TestClass]
    public class UnitTestXMLWrite
    {
        Catalog catalog;

        [TestInitialize]
        public void Init()
        {
            catalog = new Catalog();
        }
        [TestMethod]
        public void CatalogWriteTo_NoWriter_StockXML()
        {
            var actualXML = new StringBuilder();
            catalog.DateCreate = new DateTime(2020, 5, 12);
            catalog.WriteTo(actualXML, null);           

            var expectedXML = new StringBuilder(@"<?xml version=""1.0"" encoding=""utf-16""?><catalog datecreate=""12:05:2020"" />");

            Assert.IsTrue(expectedXML.Equals(actualXML));
        }

        [TestMethod]
        public void CatalogWriteTo_BookElement_XMLwithBook()
        {
            var actualXML = new StringBuilder();
            var books = Helpers.GetBook();
            catalog.AddWriters(new BookWriter());

            catalog.WriteTo(actualXML, books);
            catalog.DateCreate = new DateTime(2020, 5, 12);

            var xml = @"<?xml version=""1.0"" encoding=""utf-16""?><catalog datecreate=""12:05:2020"" >"
                + Helpers.GetBookXML()
                + @"</catalog/>";
            var expectedXML = new StringBuilder(xml);

            Assert.IsTrue(expectedXML.Equals(actualXML));
        }

        [TestMethod]
        public void CatalogWriteTo_Newspaper_XMLwithNewspaper()
        {
            var actualXML = new StringBuilder();
            var newspapers = Helpers.GetNewspaper();
            catalog.AddWriters(new BookWriter());

            catalog.WriteTo(actualXML, newspapers);
            catalog.DateCreate = new DateTime(2020, 5, 12);

            var xml = @"<?xml version=""1.0"" encoding=""utf-16""?><catalog datecreate=""12:05:2020"" >"
                + Helpers.GetNewspaper()
                + @"</catalog/>";
            var expectedXML = new StringBuilder(xml);

            Assert.IsTrue(expectedXML.Equals(actualXML));
        }

        [TestMethod]
        public void CatalogWriteTo_Newspaper_XMLwithPantent()
        {
            var actualXML = new StringBuilder();
            var patents = Helpers.GetPatant();
            catalog.AddWriters(new BookWriter());

            catalog.WriteTo(actualXML, patents);
            catalog.DateCreate = new DateTime(2020, 5, 12);

            var xml = @"<?xml version=""1.0"" encoding=""utf-16""?><catalog datecreate=""12:05:2020"" >"
                + Helpers.GetNewspaper()
                + @"</catalog/>";
            var expectedXML = new StringBuilder(xml);

            Assert.IsTrue(expectedXML.Equals(actualXML));
        }

    }
}
