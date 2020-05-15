using System;
using Xunit;
using Module_7;
using Module_7.Writers;
using System.Text;
using System.Collections.Generic;
using Module_7.Interfaces;
using Module_7.Parsers;
using DeepEqual.Syntax;
using System.IO;

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

            var expectedXML = new StringBuilder(@"<?xml version=""1.0"" encoding=""utf-16""?><catalog datecreate=""05/12/2020"" />");

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
           

            var xml = @"<?xml version=""1.0"" encoding=""utf-16""?><catalog datecreate=""05/12/2020"">"
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
           

            var xml = @"<?xml version=""1.0"" encoding=""utf-16""?><catalog datecreate=""05/12/2020"">"
                + Helpers.GetNewspaperXML()
                + @"</catalog>";
            var expectedXML = new StringBuilder(xml);

            Assert.True(expectedXML.Equals(actualXML));
        }

        [Fact]
        public void CatalogWriteTo_Patent_XMLwithPantent()
        {
            var actualXML = new StringBuilder();
            var patents = Helpers.GetPatent();
            catalog.AddWriters(new PatentWriter());

            catalog.DateCreate = new DateTime(2020, 5, 12);
            catalog.WriteTo(actualXML, patents);

            var xml = @"<?xml version=""1.0"" encoding=""utf-16""?><catalog datecreate=""05/12/2020"">"
                + Helpers.GetPatentXML()
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
            xmlElements.AddRange(Helpers.GetPatent());

            catalog.AddWriters(new BookWriter(), new NewspaperWriter(), new PatentWriter());
            catalog.DateCreate = new DateTime(2020, 5, 12);
            catalog.WriteTo(actualXML, xmlElements);
           

            var xml = @"<?xml version=""1.0"" encoding=""utf-16""?><catalog datecreate=""05/12/2020"">"
                + Helpers.GetBookXML()
                + Helpers.GetNewspaperXML()
                + Helpers.GetPatentXML()
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

            var xml = @"<?xml version=""1.0"" encoding=""utf-16""?><catalog datecreate=""05/12/2020"" >"
                + Helpers.GetBookXML()
                + @"</catalog>";
            TextReader str = new StringReader(xml);

            var actualCollection = catalog.ReadFrom(str);          

            Assert.True(expectedCollection.IsDeepEqual(actualCollection));
        }

        [Fact]
        public void CatalogReadFrom_XMLwithNewspaper_Newspaper()
        {
            var books = Helpers.GetNewspaper();
            catalog.AddParsers(new NewsparperParser());

            var expectedCollection = Helpers.GetNewspaper();

            var xml = @"<?xml version=""1.0"" encoding=""utf-16""?><catalog datecreate=""05/12/2020"" >"
                + Helpers.GetNewspaperXML()
                + @"</catalog>";
           TextReader str = new StringReader(xml);

            IEnumerable<IEntity> actualCollection = catalog.ReadFrom(str);

            Assert.True(expectedCollection.IsDeepEqual(actualCollection));
        }

        [Fact]
        public void CatalogReadFrom_XMLwithMultiElements_EmtityCollection()
        {
            var books = Helpers.GetBook();
            var newspapers = Helpers.GetNewspaper();
            var patents = Helpers.GetPatent();

            
            catalog.AddParsers(new BookParser(), new NewsparperParser(), new PatentParser());

            var expectedCollection = new List<IEntity>();
            expectedCollection.AddRange(books);
            expectedCollection.AddRange(newspapers);
            expectedCollection.AddRange(patents);

            var xml = @"<?xml version=""1.0"" encoding=""utf-16""?><catalog datecreate=""05/12/2020"" >"
                + Helpers.GetBookXML()
                + Helpers.GetNewspaperXML()
                + Helpers.GetPatentXML()
                + @"</catalog>";
            TextReader str = new StringReader(xml);

            var actualCollection = catalog.ReadFrom(str);

            Assert.True(expectedCollection.IsDeepEqual(actualCollection));
        }

        [Fact]
        public void CatalogReadFrom_XMLwithPatent_Patent()
        {
            var patent = Helpers.GetPatent();
            catalog.AddParsers(new PatentParser());

            var expectedCollection = Helpers.GetPatent();

            var xml = @"<?xml version=""1.0"" encoding=""utf-16""?><catalog datecreate=""05/12/2020"" >"
                + Helpers.GetPatentXML()
                + @"</catalog>";
            TextReader str = new StringReader(xml);

            IEnumerable<IEntity> actualCollection = catalog.ReadFrom(str);

            Assert.True(expectedCollection.IsDeepEqual(actualCollection));
        }

        [Fact]
        public void CatalogReadFrom_XMLwithTwoPatent_TwoPatents()
        {
            var patent1 = Helpers.GetPatent();
            var patent2 = Helpers.GetPatent();
            catalog.AddParsers(new PatentParser());

            var expectedCollection = new List<IEntity>();
            expectedCollection.AddRange(patent1);
            expectedCollection.AddRange(patent2);
        

            var xml = @"<?xml version=""1.0"" encoding=""utf-16""?><catalog datecreate=""05/12/2020"" >"
                + Helpers.GetPatentXML()
                + Helpers.GetPatentXML()
                + @"</catalog>";
            TextReader str = new StringReader(xml);

            IEnumerable<IEntity> actualCollection = catalog.ReadFrom(str);

            Assert.True(expectedCollection.IsDeepEqual(actualCollection));
        }
    }
}

