using System;
using System.Collections.Generic;
using Module_7.Models;
using System.Text;

namespace XUnitTestCatalogXML
{
    public class Helpers
    {
        public static IEnumerable<Book> GetBook()
        {
            var books = new List<Book>()
            {
                new Book
                {
                    Name = "Book",
                    Authors = new List<Author>
                    {
                        new Author { Name = "Author1", Surname="Surname1"},
                        new Author { Name = "Author2", Surname="Surname2"},
                    },
                    PublicationCity = "City 17",
                    PublisherName = "Publish House",
                    PublishYear = 2020,
                    PagesCount = 100,
                    Note = "Non",
                    ISBN = "ISBN12345"
                }
            };
            return books;
        }
        public static string GetBookXML()
        {
            return
                $"<Book>" +
                $"<Name>Book</Name>" +
                $"<Author Name = \"Author1\", Surname=\"Surname1\"></Author>" +
                $"<Author Name = \"Author2\", Surname=\"Surname2\"></Author>" +
                $"<PublicationCity>City 17</PublicationCity>" +
                $"<PublisherName>Publish House</PublisherName>" +
                $"<PublishYear>2020</PublishYear>" +
                $"<PagesCount>100</PagesCount>" +
                $"<Note>Non</Note>" +
                $"<ISBN>ISBN12345</ISBN>" +
                $"</Book>";
        }

        public static IEnumerable<Newspaper> GetNewspaper()
        {
            var newspapers = new List<Newspaper>()
            {
                new Newspaper
                {
                    Name = "Newspaper",
                    PublicationCity = "City 17",
                    PublisherName = "Publish House",
                    PublishYear = 2020,
                    PagesCount = 100000,
                    Note = "Non",
                    ISSN = "ISSN12345",
                    Number =  15,
                    Date = new DateTime(2020,05,12)
                }
            };
            return newspapers;
        }
        public static string GetNewspaperXML()
        {
            return
                $"<Newspaper>" +
                $"<Name>Newspaper</Name>" +
                $"<PublicationCity>City 17</PublicationCity>" +
                $"<PublisherName>Publish House</PublisherName>" +
                $"<PublishYear>2020</PublishYear>" +
                $"<PagesCount>100000</PagesCount>" +
                $"<Note>Non</Note>" +
                $"<ISBN>ISSN12345</ISBN>" +
                $"<Number>15</Number>" +
                $"<Date>05:12:2020</Date>" +
                $"</Newspaper>";
        }


        public static IEnumerable<Patent> GetPatant()
        {
            var patents = new List<Patent>()
            {
                new Patent
                {
                    Name = "Patent",
                    Creators = new List<Creator>
                    {
                        new Creator{ Name = "Creator1", Surname="Surname1"},
                        new Creator{ Name = "Creator2", Surname="Surname2"},
                    },
                    RegistrationNumber = "P123456789",
                    PagesCount = 10,
                    Note = "Non",
                    FilingDate = new DateTime(2020,05,12),
                    PublishDate = new DateTime(2020,05,12)
                }
            };
            return patents;
        }
        public static string GetPatantXML()
        {
            return
                $"<Patent>" +
                $"<Name>Patent</Name>" +
                $"<Creator Name = \"Creator1\", Surname=\"Surname1\"></Creator>" +
                $"<Creator Name = \"Creator2\", Surname=\"Surname2\"></Creator>" +
                $"<RegistrationNumber>P123456789</RegistrationNumber>" +
                $"<PagesCount>10</PagesCount>" +
                $"<Note>Non</Note>" +
                $"<FilingDate>05:12:2020</FilingDate>" +
                $"<PublishDate>05:12:2020</PublishDate>" +
                $"</Patent>";
        }
    }
}
