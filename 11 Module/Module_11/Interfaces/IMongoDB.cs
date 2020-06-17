using System;
using System.Collections.Generic;
using Module_11.Model;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module_11.Interfaces
{
    interface IMongoDB
    {
        void AddBook<T>(string table, T record);
        IEnumerable<Book> GetNameBook(string nameTable, int limit);
        Book GetBookMinCount(string nameTable);
        Book GetBookMaxCount(string nameTable);
        IEnumerable<Book> GetBookWithoutAuthor(string nameTable);
        IQueryable<String> GetAllAuthor(string nameTable);
        void AddOneCountAllBook(string nameTable);
        void AddAdditionalGenge(string nameTable, string mainGenre, string additionalGenre);
        void DeleteBookWhereCountLess(string nameTable, int count);
        void DeleteAll(string nameTable);
    }
}
