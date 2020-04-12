﻿using System;
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
        public void TestParseInt(string value, int expected)
        {
            var actual = IntParser.ParseInt(value);
            Assert.Equal(expected, actual);
        }

        //пойдет ли такой тест на проверку эксепшена
        [Fact]
        public void TestIsntFormatExceptio()
        {
            //Arrange
            var value = "12Fr444";
            var expected = new IntFormatException("String have invalid symbol");
            IntFormatException actual = null;

            // Act
            try
            {
                var numint = IntParser.ParseInt(value);
            }
            catch(IntFormatException e)
            {
                // Assert          
                actual = e;                
            }
            Assert.IsType(expected.GetType(), actual);
        }
                
        [Theory]
        [InlineData("2147483648")]
        [InlineData("-2147483649")]
        public void TestIntSizeException(string number)
        {
            //Arrange
            var expected = new IntSizeException("Too large or too small to int");
            IntSizeException actual = null;

            // Act
            try
            {
                var numint = IntParser.ParseInt(number);               
            }
            catch (IntSizeException e)
            {
                // Assert           
                actual = e;                
            }

            Assert.IsType(expected.GetType(), actual);
        }

        [Theory]
        [InlineData("10", true)]       
        [InlineData("257", true)]
        [InlineData("-159", true)]
        [InlineData("1234567", true)]
        [InlineData("2ddd57", false)]
        [InlineData("2147483648", false)]
        [InlineData("-2147483649", false)]
        public void TestTryParseIntTrueFalse(string value, bool expected)
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
        public void TestTryParseInt(string value, int expected)
        {
            int actual;
            var succseful = IntParser.TryParseInt(value, out actual);
            Assert.Equal(expected, actual);
        }
    }
}
