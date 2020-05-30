using System;
using System.Collections.Generic;

namespace Module_09.Model
{ 
    public class Region
    {
        public Region()
        {
            this.Territories = new HashSet<Territory>();
        }

        public int RegionID { get; set; }
        public string RegionDescription { get; set; }

        public ICollection<Territory> Territories { get; set; }
    }
}

