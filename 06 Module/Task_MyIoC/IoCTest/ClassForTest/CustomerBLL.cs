using MyIoC.Attributes;

namespace IoCTest.ClassForTest
{
    [ImportConstructor]
    public class CustomerBLL
    {
        public CustomerBLL(ICustomerDAL dal, Logger logger)
        {
            Logger = logger;
            ICustomerDAL = dal;
        }

        public ICustomerDAL ICustomerDAL { get; set; }
        public Logger Logger { get; set; }
    }

}
