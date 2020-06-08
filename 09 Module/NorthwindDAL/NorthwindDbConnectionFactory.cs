using NorthwindDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindDAL
{
    public class NorthwindDbConnectionFactory : IDbConnectionFactory
    {
        public DbProviderFactory ProviderFactory { get; set; }
        public string ConnectionString { get; set; }
       
        public DbConnection CreateConnection()
        {           
            var connection = ProviderFactory.CreateConnection();
            connection.ConnectionString = ConnectionString;
            connection.Open();
            return connection;
        }
    }
}
