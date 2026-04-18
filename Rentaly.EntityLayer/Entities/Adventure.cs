using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rentaly.EntityLayer.Entities
{
    public class Adventure
    {
        public int AdventureId { get; set; }
        public string Icon { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
