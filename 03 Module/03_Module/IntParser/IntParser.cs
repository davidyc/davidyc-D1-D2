using System;

namespace CustomerIntParser
{
    public class IntParser
    {
        public static int ParseInt(string inputString)
        {
            long number = 0;
            int dec = 1;
            var numberString = CleanNumber(inputString);
          
            for (int i = numberString.Length-1; i >= 0; i--)
            {
                if ((numberString[i] - 48) < 0 || (numberString[i] - 48) > 9)
                    throw new IntFormatException("String have invalid symbol");
                number += ((int)numberString[i] - 48) * dec;
                dec *= 10;
            }

            if (IsNegative(inputString))
                number *= -1;

            if (number > int.MaxValue || number < int.MinValue)
                throw new IntSizeException("Too large or too small to int");

            return (int)number;
        }
        public static bool TryParseInt(string number, out int resultInt)
        {
            try
            {
                resultInt = IntParser.ParseInt(number);
                return true;
            }
            catch
            {
                resultInt = 0;
                return false;
            }        
        }  
        static bool IsNegative(string numberString)
        {
            return numberString[0] == '-';
        }
        static string CleanNumber(string inputString)
        {
            var str = inputString.Replace(" ", "").Replace("-", "");
            return str;
        }
    }
}
