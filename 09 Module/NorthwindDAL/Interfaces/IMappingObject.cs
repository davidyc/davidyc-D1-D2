using System.Data.Common;

namespace NorthwindDAL.Interfaces
{
    public interface IMappingObject
    {
        T MappinObject<T>(DbDataReader reader);
    }
}
