using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.Data;

namespace Task
{
	public interface IDataSource
	{
		List<Product> Products { get; }
		List<Supplier> Suppliers { get; }
		List<Customer> Customers { get; }
		string Resource { get; set; }
	}
}
