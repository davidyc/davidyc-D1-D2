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
            var dateTime = new DateTime(2020, 1,1,0,0,0);

            // Act
            var result = HelloCreaterString.HelloCreateString(name, dateTime);

            // Assert
            Assert.Equal(String.Format("{0} Hello Sergey!", dateTime.ToString("HH:mm")), result);
        }
    }
}
