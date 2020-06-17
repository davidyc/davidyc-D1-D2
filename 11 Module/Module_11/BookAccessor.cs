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
    public class BookAccessor : IMongoDB
    {
        private IMongoDatabase db;

        public BookAccessor(string database)
        {
            var client = new MongoClient();
            db = client.GetDatabase(database);
        }

        public void AddBook<T>(string nameTable, T record)
        {
            var collection = db.GetCollection<T>(nameTable);
            collection.InsertOne(record);
        }

        public IEnumerable<Book> GetNameBook(string nameTable, int limit)
        {
            var col = db.GetCollection<Book>(nameTable);
            var filter = Builders<Book>.Filter.Gt("Count", 1);
            return  col.Find(filter).Sort("{Name: 1}").Limit(limit).ToList();            
        }            

        public Book GetBookMinCount(string nameTable)
        {
            var col = db.GetCollection<Book>(nameTable);
            return col.Aggregate().SortBy((a) => a.Count).FirstOrDefault();

           
        }
        public Book GetBookMaxCount(string nameTable)
        {
            var col = db.GetCollection<Book>(nameTable);
            return col.Aggregate().SortByDescending((a) => a.Count).FirstOrDefault();
        }
        public IEnumerable<Book> GetBookWithoutAuthor(string nameTable)
        {
            var collection = db.GetCollection<Book>(nameTable);
            var filter = Builders<Book>.Filter.Eq<Book>("Author", null);
            return collection.Find(filter).ToList();            
        }

        public IQueryable<String> GetAllAuthor(string nameTable)
        {
            var collection = db.GetCollection<Book>(nameTable);
            return collection.AsQueryable<Book>().Where(x => x.Author != null).Select(e => e.Author).Distinct();           
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
        }

        public void AddAdditionalGenge(string nameTable, string mainGenre, string additionalGenre)
        {
            var collection = db.GetCollection<Book>(nameTable);
            var result = collection.Find(x => true).ToList().Where(x => x.Genre.Contains(mainGenre) && !x.Genre.Contains(additionalGenre));

            foreach (var item in result)
            {
                var newArrayGenre = item.Genre.ToList();
                newArrayGenre.Add(additionalGenre);
                var filter = Builders<Book>.Filter.Eq("_id", item.ID);
                var update = Builders<Book>.Update.Set("Genre", newArrayGenre.ToArray());
                collection.UpdateOne(filter, update);
            }         

        }

        public void DeleteBookWhereCountLess(string nameTable, int count)
        {
            var collection = db.GetCollection<Book>(nameTable);
            var result = collection.DeleteMany<Book>(p => p.Count < 3);            
        }

        public void DeleteAll(string nameTable)
        {
            var collection = db.GetCollection<Book>(nameTable);
            var result = collection.DeleteMany<Book>(p => true);            
        }
    }
}
