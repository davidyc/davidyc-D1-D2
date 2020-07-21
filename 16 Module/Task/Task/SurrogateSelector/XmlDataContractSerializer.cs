using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.TestHelpers;
using System.IO;
using System.Runtime.Serialization;

namespace Task.SurrogateSelector
{
    public class XmlDataContractSerializer<T> : ICustomSerializer<T>
    {
        private readonly XmlObjectSerializer serializer;

        public XmlDataContractSerializer(XmlObjectSerializer serializer)
        {
            this.serializer = serializer;
        }

        public T Deserialization(MemoryStream stream)
        {
            return (T)serializer.ReadObject(stream);
        }              

        public void Serialization(T data, MemoryStream stream)
        {
            serializer.WriteObject(stream, data);
        }       
    }
}
