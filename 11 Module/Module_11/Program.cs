using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using Module_11;
using Module_11.Model;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace MongoApp
{
    class Program
    {
        static string nameDB = "BooksDB";
        static string nameTable = "BooksTable";

        static void Main(string[] args)
        {
            // for test
            var client = new MongoClient();
            var db = client.GetDatabase(nameDB);

            var mongo = new MongoDBConnection(nameDB);
            //var books = GetTestDataBooks();
            //foreach (var book in books)
            //{
            //    mongo.AddBook<Book>(nameTable, book);
            //}
            //mongo.ShowNameBookCountMore1(nameTable, 3);
            //mongo.GetBookMinCount(nameTable);
            //mongo.GetBookMaxCount(nameTable);
            //mongo.GetBookWithoutAuthor(nameTable);
            //mongo.GetAllAuthor(nameTable);
            //mongo.AddOneCountAllBook(nameTable);



        

            Console.Read();
        }

        public static List<Book> GetTestDataBooks()
        {
            return new List<Book>()
            {              
               new Book()
               {
                    Author = "Tolkien",
                    Name = "Hobbit",
                    Count = 5,
                    Genre = new string[] { "fantasy" },
                    Year = 2014
               },
               new Book()
               {
                    Author = "Tolkien",
                    Name = "Lord of the rings",
                    Count = 3,
                    Genre = new string[] { "fantasy" },
                    Year = 2015
               },
               new Book()
               {                   
                    Name = "Kolobok",
                    Count = 10,
                    Genre = new string[] { "kids" },
                    Year = 2000
               },
               new Book()
               {
                    Name = "Repka",
                    Count = 11,
                    Genre = new string[] { "kids" },
                    Year = 2000
               },
               new Book()
               {
                    Author = "Mihalkov",
                    Name = "Dyadya Stiopa",
                    Count = 1,
                    Genre = new string[] { "fantasy" },
                    Year = 2001
               },
            };
        }
    }
   
}
