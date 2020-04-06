using HelloWorldClassLiblary.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace HelloWorldClassLiblary
{
    public class StaticTime : ITime
    {
        public DateTime DateTime { get; }

        public StaticTime(int year, int mounth, int day, int hour, int minute, int second)
        {
            DateTime = new DateTime(year, mounth, day, hour, minute, second);
        }

    }
}
