using LinqToDB;
using LinqToDB.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task1.Model;

namespace Task01
{
    public class NorthwindDataConnection : DataConnection
    {
        public NorthwindDataConnection() : base("Northwind") { }
        public ITable<Category> Categories { get { return GetTable<Category>(); } }
    }
}
