using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task.TestHelpers
{
    public interface ICustomSerializer<TData>
    {
        TData Deserialization(MemoryStream stream);
        void Serialization(TData data, MemoryStream stream);
    }
}
