using System;
using System.Collections.Generic;
using System.Text;

namespace Module_7.Models
{
    public class Patent 
    {
        public string Name { get; set; }
        public List<Creator> Creators { get; set; }
        public string Country { get; set; }
        public string RegistrationNumber { get; set; }
        public DateTime FilingDate { get; set; }
        public DateTime PublishDate { get; set; }
        public int PagesCount { get; set; }
        public string Note { get; set; }
    }
}
