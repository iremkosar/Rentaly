using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rentaly.EntityLayer.Entities
{
    public class Location
    {
        public int LocationId { get; set; }
        public string LocationName { get; set; } 
        public string City { get; set; }
    }
}
