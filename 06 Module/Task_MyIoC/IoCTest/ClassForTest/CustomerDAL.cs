using MyIoC.Attributes;

namespace IoCTest.ClassForTest
{
    [Export(typeof(ICustomerDAL))]
    public class CustomerDAL : ICustomerDAL
    { }
}

