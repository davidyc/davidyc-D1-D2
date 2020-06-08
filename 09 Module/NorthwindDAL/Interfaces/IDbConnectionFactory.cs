using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindDAL.Interfaces
{
    public interface IDbConnectionFactory
    {
        DbProviderFactory ProviderFactory { get; set; }
        string ConnectionString { get; set; }
        DbConnection CreateConnection();      
    }
}
