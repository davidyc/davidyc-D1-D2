using MyIoC.Attributes;

namespace IoCTest.ClassForTest
{
    [ImportConstructor]
    public class CustomerBLL
    {
        public CustomerBLL(ICustomerDAL dal, Logger logger)
        { }
    }

}
