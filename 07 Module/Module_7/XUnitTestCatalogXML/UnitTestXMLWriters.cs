using System;
using Xunit;
using Module_7;
using Module_7.Writers;
using System.Text;
using System.Collections.Generic;
using Module_7.Interfaces;
using Module_7.Parsers;
using DeepEqual.Syntax;

namespace XUnitTestCatalogXML
{
     public class UnitTestXMLWriters
     {
        Catalog catalog = new Catalog();
       
        [Fact]
        public void CatalogWriteTo_NoWriter_StockXML()
        {       
            var actualXML = new StringBuilder();
            catalog.DateCreate = new DateTime(2020, 5, 12);
            catalog.WriteTo(actualXML, null);

            var expectedXML = new StringBuilder(@"<?xml version=""1.0"" encoding=""utf-16""?><catalog datecreate=""12:05:2020"" />");

            Assert.True(expectedXML.Equals(actualXML));
        }

        [Fact]
        public void CatalogWriteTo_BookElement_XMLwithBook()
        {
            var actualXML = new StringBuilder();
            var books = Helpers.GetBook();
            catalog.AddWriters(new BookWriter());
            catalog.DateCreate = new DateTime(2020, 5, 12);

            catalog.WriteTo(actualXML, books);
           

            var xml = @"<?xml version=""1.0"" encoding=""utf-16""?><catalog datecreate=""12:05:2020"">"
                + Helpers.GetBookXML()
                + @"</catalog>";
            var expectedXML = new StringBuilder(xml);

            Assert.True(expectedXML.Equals(actualXML));
        }

        [Fact]
        public void CatalogWriteTo_Newspaper_XMLwithNewspaper()
        {
            var actualXML = new StringBuilder();
            var newspapers = Helpers.GetNewspaper();
            catalog.AddWriters(new NewspaperWriter());
            catalog.DateCreate = new DateTime(2020, 5, 12);
            catalog.WriteTo(actualXML, newspapers);
           

            var xml = @"<?xml version=""1.0"" encoding=""utf-16""?><catalog datecreate=""12:05:2020"">"
                + Helpers.GetNewspaperXML()
                + @"</catalog>";
            var expectedXML = new StringBuilder(xml);

            Assert.True(expectedXML.Equals(actualXML));
        }

        [Fact]
        public void CatalogWriteTo_Patent_XMLwithPantent()
        {
            var actualXML = new StringBuilder();
            var patents = Helpers.GetPatant();
            catalog.AddWriters(new PatentWriter());

            catalog.DateCreate = new DateTime(2020, 5, 12);
            catalog.WriteTo(actualXML, patents);

            var xml = @"<?xml version=""1.0"" encoding=""utf-16""?><catalog datecreate=""12:05:2020"">"
                + Helpers.GetPatantXML()
                + @"</catalog>";
            var expectedXML = new StringBuilder(xml);

            Assert.True(expectedXML.Equals(actualXML));
        }

        [Fact]
        public void CatalogWriteTo_MultiElements_XMLwithMultiElements()
        {
            var actualXML = new StringBuilder();
            var xmlElements = new List<IEntity>();
            xmlElements.AddRange(Helpers.GetBook());
            xmlElements.AddRange(Helpers.GetNewspaper());
            xmlElements.AddRange(Helpers.GetPatant());

            catalog.AddWriters(new BookWriter(), new NewspaperWriter(), new PatentWriter());
            catalog.DateCreate = new DateTime(2020, 5, 12);
            catalog.WriteTo(actualXML, xmlElements);
           

            var xml = @"<?xml version=""1.0"" encoding=""utf-16""?><catalog datecreate=""12:05:2020"">"
                + Helpers.GetBookXML()
                + Helpers.GetNewspaperXML()
                + Helpers.GetPatantXML()
                + @"</catalog>";
            var expectedXML = new StringBuilder(xml);

            Assert.True(expectedXML.Equals(actualXML));
        }

        [Fact]
        public void CatalogReadFrom_XMLwithBook_BookElement()
        {            
            var books = Helpers.GetBook();
            catalog.AddParsers(new BookParser());

            var expectedCollection = Helpers.GetBook();

            var xml = @"<?xml version=""1.0"" encoding=""utf-16""?><catalog datecreate=""12:05:2020"" >"
                + Helpers.GetBookXML()
                + @"</catalog/>";
            var str = new StringBuilder(xml);

            var actualCollection = catalog.ReadFrom(str);          

            Assert.True(expectedCollection.IsDeepEqual(actualCollection));
        }

        [Fact]
        public void CatalogReadFrom_XMLwithNewspaper_Newspaper()
        {
            var books = Helpers.GetBook();
            catalog.AddParsers(new NewsparperParser());

            var expectedCollection = Helpers.GetNewspaper();

            var xml = @"<?xml version=""1.0"" encoding=""utf-16""?><catalog datecreate=""12:05:2020"" >"
                + Helpers.GetNewspaperXML()
                + @"</catalog/>";
            var str = new StringBuilder(xml);

            var actualCollection = catalog.ReadFrom(str);

            Assert.True(expectedCollection.IsDeepEqual(actualCollection));
        }

        [Fact]
        public void CatalogReadFrom_XMLwithMultiElements_EmtityCollection()
        {
            var books = Helpers.GetBook();
            catalog.AddParsers(new BookParser(), new NewsparperParser(), new PatentParser());

            var expectedCollection = Helpers.GetPatant();

            var xml = @"<?xml version=""1.0"" encoding=""utf-16""?><catalog datecreate=""12:05:2020"" >"
                + Helpers.GetBookXML()
                + Helpers.GetNewspaperXML()
                + Helpers.GetPatantXML()
                + @"</catalog/>";
            var str = new StringBuilder(xml);

            var actualCollection = catalog.ReadFrom(str);

            Assert.True(expectedCollection.IsDeepEqual(actualCollection));
        }

        [Fact]
        public void CatalogReadFrom_XMLwithPatent_Patent()
        {
            var books = Helpers.GetBook();
            catalog.AddParsers(new PatentParser());

            var expectedCollection = Helpers.GetPatant();

            var xml = @"<?xml version=""1.0"" encoding=""utf-16""?><catalog datecreate=""12:05:2020"" >"
                + Helpers.GetPatantXML()
                + @"</catalog>";
            var str = new StringBuilder(xml);

            var actualCollection = catalog.ReadFrom(str);

            Assert.True(expectedCollection.IsDeepEqual(actualCollection));
        }
    }
}

