using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module_11.Interfaces
{
    interface IMongoDB
    {
        void AddBook<T>(string table, T record);
        void ShowNameBook(string nameTable, int limit);
        void GetBookMinCount(string nameTable);
        void GetBookMaxCount(string nameTable);
        void GetBookWithoutAuthor(string nameTable);
        void GetAllAuthor(string nameTable);
        void AddOneCountAllBook(string nameTable);
        void AddAdditionalGenge(string nameTable, string mainGenre, string additionalGenre);
        void DeleteBookWhereCountLess(string nameTable, int count);
        void DeleteAll(string nameTable);
    }
}
