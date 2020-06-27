using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WebApplicationForCodeTest.Entities
{
    [DataContract]
    public class Model1
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
}