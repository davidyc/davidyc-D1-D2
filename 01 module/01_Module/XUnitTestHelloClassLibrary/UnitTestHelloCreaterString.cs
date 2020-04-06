using System;
using Xunit;
using HelloWorldClassLiblary;

namespace XUnitTestHelloClassLibrary
{
    public class UnitTestHelloCreaterString
    {
        [Fact]
        public void HelloCreateString()
        {
            //Arrange
            var name = "Sergey";
            var dateTime = new DateTime(2020, 1,1,7,0,0);

            // Act
            var result = HelloCreaterString.HelloCreateString(name, dateTime);

            // Assert
            Assert.Equal(String.Format("07:00 Hello Sergey!", dateTime.ToString("HH:mm")), result);
        }
    }
}
