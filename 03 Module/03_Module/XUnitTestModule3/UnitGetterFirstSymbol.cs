using System;
using Xunit;
using FirstSymbolInString;

namespace XUnitTestModule3
{
    public class UnitGetterFirstSymbol
    {
        [Theory]
        [InlineData("Hello", "H")]
        [InlineData("luck", "l")]
        [InlineData("1234567", "1")]
        public void GetFirstSymbol_GivenNotEmptyString_ReturnFirstSymbol(string value, string expected)
        {
            var actual = GetterFirstSymbol.GetFirstSymbol(value);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetFirstSymbol_GivenEmptyString_StringNullOrEmptyException()
        {
            //Arrange
            var inputString = String.Empty;
            // Act
            Action actual = () => GetterFirstSymbol.GetFirstSymbol(inputString);
            // Assert
            Assert.Throws<StringNullOrEmptyException>(actual);
        }

        [Fact]
        public void GetFirstSymbol_GivenNull_StringNullOrEmptyException()
        {
            //Arrange
            string inputString = null;
            // Act
            Action actual = () => GetterFirstSymbol.GetFirstSymbol(inputString);
            // Assert
            Assert.Throws<StringNullOrEmptyException>(actual);
        }
    }
}
