using NorthwindDAL.Attributes;
using NorthwindDAL.Interfaces;
using System;
using System.Data.Common;
using System.Linq;

namespace NorthwindDAL
{
    public class MapObject : IMappingObject
    {
        public T MappinObject<T>(DbDataReader reader)
        {
            var type = typeof(T);
            var properties = type.GetProperties();
            T instance = (T)Activator.CreateInstance(type);
            for (int i = 0; i < properties.Length; i++)
            {
                var Attributes = properties[i].CustomAttributes.FirstOrDefault(x => x.AttributeType == typeof(IgnoreMapping));
                if (Attributes == null)
                {
                    var value = reader[properties[i].Name];
                    if (value.ToString().Equals(String.Empty))
                        properties[i].SetValue(instance, null);
                    else
                        properties[i].SetValue(instance, value);
                }
            }
            return instance;
        }

    }
}
