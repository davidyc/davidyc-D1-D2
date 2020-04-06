using HelloWorldClassLiblary.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace HelloWorldClassLiblary
{
    public class SystemTime : ITime
    {
        public DateTime DateTime { get { return DateTime.Now; } }
    }
}
