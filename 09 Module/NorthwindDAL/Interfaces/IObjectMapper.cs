using System.Data.Common;

namespace NorthwindDAL.Interfaces
{
    public interface IObjectMapper
    {
        T MapReaderToObject<T>(DbDataReader reader);
    }
}
