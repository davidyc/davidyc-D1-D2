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
    }
}
