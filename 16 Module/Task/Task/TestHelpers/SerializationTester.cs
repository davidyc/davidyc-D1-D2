using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task.TestHelpers
{
	public  class SerializationTester<TData>
	{
		private ICustomSerializer<TData> _serializer;
		private bool _showResult;

		public SerializationTester(ICustomSerializer<TData> serializer, bool showResult = false)
		{
			_serializer = serializer;
			_showResult = showResult;
		}

		public TData SerializeAndDeserialize(TData data)
		{
			var stream = new MemoryStream();

			Console.WriteLine("Start serialization");
			_serializer.Serialization(data, stream);
			Console.WriteLine("Serialization finished");

			if (_showResult)
			{
				var r = Console.OutputEncoding.GetString(stream.GetBuffer(), 0, (int)stream.Length);
				Console.WriteLine(r);
			}

			stream.Seek(0, SeekOrigin.Begin);
			Console.WriteLine("Start deserialization");
			TData result = _serializer.Deserialization(stream);
			Console.WriteLine("Deserialization finished");

			return result;
		}
	}
}
