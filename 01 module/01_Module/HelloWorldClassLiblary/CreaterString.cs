using System;


namespace HelloWorldClassLiblary
{
    public class CreaterString
    {
        public static string CreateString(string name)
        {            
            string currentTime = DateTime.Now.ToLongTimeString();
            return String.Format("{0} Hello {1}!", currentTime, name);
        }
    }
}
