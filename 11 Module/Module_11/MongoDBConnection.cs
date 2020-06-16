using Module_11.Interfaces;
using Module_11.Model;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module_11
{
    public class MongoDBConnection : IMongoDB
    {
        private IMongoDatabase db;

        public MongoDBConnection(string database)
        {
            var client = new MongoClient();
            db = client.GetDatabase(database);
        }

        public void AddBook<T>(string nameTable, T record)
        {
            var collection = db.GetCollection<T>(nameTable);
            collection.InsertOne(record);
        }

        public void ShowNameBook(string nameTable, int limit)
        {                     
            var col = db.GetCollection<Book>(nameTable);
            var filter = Builders<Book>.Filter.Gt("Count", 1);
            var result = col.Find(filter).Sort("{Name: 1}").Limit(limit).ToList();

            foreach (var item in result)
            {
                Console.WriteLine(item.ToString());
            }
            // не уверен что правильно понял пункт D во 2 задании
            Console.WriteLine($"Count = {result.Sum(x => x.Count)}");
        }

        public void GetBookMinCount(string nameTable)
        {
            var col = db.GetCollection<Book>(nameTable);
            var result = col.Aggregate().SortBy((a) => a.Count).FirstOrDefault();

            Console.WriteLine($"Book name = {result.Name} Count = {result.Count}");
        }
        public void GetBookMaxCount(string nameTable)
        {
            var col = db.GetCollection<Book>(nameTable);
            var result = col.Aggregate().SortByDescending((a) => a.Count).FirstOrDefault();

            Console.WriteLine($"Book name = {result.Name} Count = {result.Count}");
        }

        public void GetBookWithoutAuthor(string nameTable)
        {
            var collection = db.GetCollection<Book>(nameTable);
            var filter = Builders<Book>.Filter.Eq<Book>("Author", null);
            var result = collection.Find(filter).ToList();
            foreach (var item in result)
            {
                Console.WriteLine(item.Name);
            }
        }

        public void GetAllAuthor(string nameTable)
        {
            var collection = db.GetCollection<Book>(nameTable);
            var result = collection.AsQueryable<Book>().Where(x => x.Author != null).Select(e => e.Author).Distinct();

            foreach (var item in result)
            {
                Console.WriteLine(item);
            }
        }


        public void AddOneCountAllBook(string nameTable)
        {
            var collection = db.GetCollection<Book>(nameTable);
            var result = collection.Find(x => true).ToList();

            foreach (var item in result)
            {
                var filter = Builders<Book>.Filter.Eq("_id", item.ID);
                var update = Builders<Book>.Update.Set("Count", ++item.Count);
                collection.UpdateOne(filter, update);
            }
            Console.WriteLine("All books counts wae updated");

        }
    }
}
