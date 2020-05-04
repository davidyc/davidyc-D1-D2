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
		List<Product> Products { get; set; }
		List<Supplier> Suppliers { get; set; }
		List<Customer> Customers { get; set; }
		string Resource { get; set; }
	}
}
