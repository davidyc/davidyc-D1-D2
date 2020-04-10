using System;
using Xunit;
using CustomerIntParser;

namespace XUnitTestModule3
{
    public class UnitIntParser
    {
        [Theory]
        [InlineData("100", 100)]
        [InlineData("0", 0)]
        [InlineData("257", 257)]
        [InlineData("-159", -159)]
        [InlineData("1234567", 1234567)]
        public void TestParseInt(string value, int expected)
        {
            var actual = IntParser.ParseInt(value);
            Assert.Equal(expected, actual);
        }
    }
}
