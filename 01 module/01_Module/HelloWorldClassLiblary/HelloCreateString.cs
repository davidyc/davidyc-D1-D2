using System;


namespace HelloWorldClassLiblary
{
    public class HelloCreaterString
    {
        public static string HelloCreateString(string name, DateTime dateTime)
        {
            return $"{dateTime.ToString("HH:mm")} Hello {name}!";
        }
    }
}
