﻿using System;
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
       
        public void TestGetFirstSymbol(string value, string expected)
        {
            var actual = GetterFirstSymbol.GetFirstSymbol(value);  
            Assert.Equal(expected, actual);
        }
    }
}
