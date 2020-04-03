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
            var stringcreater = new HelloCreaterString(new StaticTime());

            // Act
            var result = stringcreater.HelloCreateString(name);

            // Assert
            Assert.Equal(result, "12:00:00 AM Hello Sergey!");
        }
    }
}
