using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rusada.ViewModelLayer
{
    public class AirList
    {
        public int? PlanID { get; set; }
        public String Make { get; set; }
        public String Model { get; set; }
        public String Registration { get; set; }
        public String Location { get; set; }
        public DateTime Modelyear { get; set; }
    }          
}
