using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rentaly.EntityLayer.Entities
{
    public class HowItWork
    {
        public int HowItWorkId { get; set; }
        public int StepNumber { get; set; }      
        public string Title { get; set; }        
        public string Description { get; set; } 
    }
}
