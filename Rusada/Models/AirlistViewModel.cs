using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rusada.Models
{
    public class AirlistViewModel
    {
        public int? PlanID { get; set; }
        public String Make { get; set; }
        public String Model { get; set; }
        public String Registration { get; set; }
        public String Location { get; set; }
        public DateTime Modelyear { get; set; }
    }
    public class AirCraftListmodel
    {
        public List<AirlistViewModel> AirListModel { get; set; }
     }
}