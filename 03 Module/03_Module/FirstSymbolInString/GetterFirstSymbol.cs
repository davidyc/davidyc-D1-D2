using System;


namespace FirstSymbolInString
{
    public class GetterFirstSymbol
    {
        public static string GetFirstSymbol(string inputString)
        {   
            if (string.IsNullOrEmpty(inputString))
                throw new StringNullOrEmptyException("Input string is null or empty");
            return inputString[0].ToString();        
        }
    }
}
