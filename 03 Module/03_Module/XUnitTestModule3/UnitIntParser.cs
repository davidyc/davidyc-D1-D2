using System;
using Xunit;
using CustomerIntParser;

namespace XUnitTestModule3
{
    public class UnitIntParser
    {
        [Theory]
        [InlineData("10", 10)]
        [InlineData("0", 0)]
        [InlineData("257", 257)]
        [InlineData("-159", -159)]
        [InlineData("1234567", 1234567)]
        public void ParseInt_GivenStringNumber_ParseToIntNumber(string value, int expected)
        {
            var actual = IntParser.ParseInt(value);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ParseInt_GivenIncorrectStringNumber_ShouldIntFormatException()
        {
            //Arrange
            var inputString = "12Fr444";             
            // Act
            Action actual = () => IntParser.ParseInt(inputString);
            // Assert
            Assert.Throws<IntFormatException>(actual);              
        }
                
        [Theory]
        [InlineData("2147483648")]
        [InlineData("-2147483649")]
        public void ParseInt_GivenStringNumberToLargeInt_ShouldIntSizeException(string number)
        {
            //Arrange
            var inputString = number;
            // Act
            Action actual = () => IntParser.ParseInt(inputString);
            // Assert
            Assert.Throws<IntSizeException>(actual);
        }

        [Theory]
        [InlineData("10", true)]       
        [InlineData("257", true)]
        [InlineData("-159", true)]
        [InlineData("1234567", true)]        
        public void TryParseInt_GivenStringNumber_ShouldTrue(string value, bool expected)
        {
            int number;
            var actual = IntParser.TryParseInt(value, out number);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("2ddd57", false)]
        [InlineData("2147483648", false)]
        [InlineData("-2147483649", false)]
        public void TryParseInt_GivenIncorrectStringNumber_ShouldFalse(string value, bool expected)
        {
            int number;
            var actual = IntParser.TryParseInt(value, out number);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("10", 10)]        
        [InlineData("257", 257)]
        [InlineData("-159", -159)]
        [InlineData("1234567", 1234567)]
        public void TryParseInt_GivenStringNumber_ParseToIntNumber(string value, int expected)
        {
            int actual;
            var succseful = IntParser.TryParseInt(value, out actual);
            Assert.Equal(expected, actual);
        }
    }
}
