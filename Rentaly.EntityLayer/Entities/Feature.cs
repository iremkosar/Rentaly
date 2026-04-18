using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rentaly.EntityLayer.Entities
{
    public class Feature
    {
        public int FeatureId { get; set; }
        public string Icon { get; set; }       
        public string Title { get; set; }     
        public string Description { get; set; } 
        public string ImageUrl { get; set; }    
        public string SectionTitle { get; set; }    
        public string SectionDescription { get; set; } 
    }
}
