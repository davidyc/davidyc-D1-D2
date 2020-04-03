using HelloWorldClassLiblary.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace HelloWorldClassLiblary
{
    public class StaticTime : ITime
    {
        public DateTime GetDateTime { get { return new DateTime(2020, 1, 1, 0, 0, 0); } }
        
    }
}
