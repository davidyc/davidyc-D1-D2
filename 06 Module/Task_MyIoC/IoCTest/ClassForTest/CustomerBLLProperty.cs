using MyIoC.Attributes;


namespace IoCTest.ClassForTest
{
    public class CustomerBLLProperty
    {
        [Import]
        public ICustomerDAL CustomerDAL { get; set; }
        [Import]
        public Logger logger { get; set; }
    }

}
