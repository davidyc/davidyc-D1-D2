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
            var charArray = numberString.ToCharArray();
            Array.Reverse(charArray);                


            foreach (var ch in charArray)
            {
                if ((ch - 48) < 0 || (ch - 48) > 9)
                    throw new IntFormatException("String have invalid symbol");
                number += ((int)ch - 48) * dec;
                dec *= 10;
            }

            if (IsNegative(inputString))
                number *= -1;

            if (number > int.MaxValue || number < int.MinValue)
                throw new IntSizeException("Too large or too small to int");

            return (int)number;
        }

        //этого в задинии нет но я решил создать аналоги парс и трайпарс что б тесты пописать на них
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
       
        // тут есть вопрос стоит ли тестит приватные методы как я понял в нете есть споры на счет этого. 
        //А как на практике
        static bool IsNegative(string numberString)
        {
            return numberString.Contains("-") ? true : false;
        }

        static string CleanNumber(string inputString)
        {
            var str = inputString.Replace(" ", "").Replace("-", "");
            return str;
        }
    }
}
