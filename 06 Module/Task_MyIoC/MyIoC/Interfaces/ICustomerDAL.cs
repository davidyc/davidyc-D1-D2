using MyIoC.Attributes;

namespace MyIoC.Interfaces
{
	public interface ICustomerDAL
	{
	}

	[Export(typeof(ICustomerDAL))]
	public class CustomerDAL : ICustomerDAL
	{ }
}