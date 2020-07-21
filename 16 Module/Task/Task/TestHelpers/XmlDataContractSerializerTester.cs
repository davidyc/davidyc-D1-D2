using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Task.TestHelpers
{
	public class XmlDataContractSerializerTester<T> : ICustomSerializer<T>
	{
		private  XmlObjectSerializer _serializer;
		public XmlDataContractSerializerTester(XmlObjectSerializer serializer)
		{
			_serializer = serializer;
		}

		public T Deserialization(MemoryStream stream)
		{
			return (T)_serializer.ReadObject(stream);
		}

		public void Serialization(T data, MemoryStream stream)
		{
			_serializer.WriteObject(stream, data);
		}
		
	}
}
