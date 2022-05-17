using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VehicleManagementSystem.Models
{
    public class modelViewVehicle
    {

        public Vehicles v { get; set; }
        public IEnumerable<VehicleManagementSystem.Models.Component> c{ get; set; } 
    }
}