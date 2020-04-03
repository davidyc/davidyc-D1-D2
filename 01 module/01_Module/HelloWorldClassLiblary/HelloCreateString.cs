using HelloWorldClassLiblary.Interfaces;
using System;


namespace HelloWorldClassLiblary
{
    public class HelloCreaterString 
    {
        ITime time;

        public HelloCreaterString(ITime time)
        {
            this.time = time;
        }

        public string HelloCreateString(string name)
        {
            var currentTime = time.GetDateTime.ToLongTimeString();
            return String.Format("{0} Hello {1}!", currentTime, name);
        }
    }
}
