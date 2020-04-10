using System;

namespace CustomerIntParser
{
    public class IntParser
    {
        public static int ParseInt(string inputString)
        {
            int number = 0;
            int dec = 1;
            var numberString = CleanNumber(inputString);
            var charArray = numberString.ToCharArray();
            Array.Reverse(charArray);

            foreach (var ch in charArray)
            {
                number += ((int)ch - 48) * dec;
                dec *= 10;
            }

            if (IsNegative(inputString))
                number *= -1;

            return number;
        }

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
