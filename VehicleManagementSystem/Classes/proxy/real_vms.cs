using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VehicleManagementSystem.Models;

namespace VehicleManagementSystem.Classes.proxy
{
    public class real_vms : vms
    {
     

        public bool openLink(account x, string viewName, string ControllerName)
        {
            // connected 

            Debug.WriteLine("authorized");
            //throw new NotImplementedException();
            return true;
        }
    }
}