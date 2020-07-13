using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Task1.Models
{
    public enum Genre
    {
        [XmlEnum("Computer")]
        Computer,
        [XmlEnum("Fantasy")]
        Fantasy,
        [XmlEnum("Romance")]
        Romance,
        [XmlEnum("Horror")]
        Horror,
        [XmlEnum("Science Fiction")]
        ScienceFiction
    }
}
