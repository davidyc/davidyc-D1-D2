using HelloWorldClassLiblary.Interfaces;
using System;


namespace HelloWorldClassLiblary
{
    public class HelloCreaterString
    {
        public static string HelloCreateString(string name, DateTime dateTime)
        {
            return String.Format("{0} Hello {1}!", dateTime.ToString("HH:mm"), name);
        }
    }
}
