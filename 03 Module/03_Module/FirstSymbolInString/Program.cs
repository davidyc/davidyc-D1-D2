﻿using System;
using System.Collections.Generic;

namespace FirstSymbolInString
{
    class Program
    {
        static void Main(string[] args)
        {           
            for (int i = 0; i < 10; i++)
            {
                Console.Write("Input string --> ");
                var input = Console.ReadLine();
                try
                {
                    var firstSymbol = GetterFirstSymbol.GetFirstSymbol(input);
                    Console.WriteLine("Fist symbol is " + firstSymbol);
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }     
            }
           
        }
    }
}
