using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using VehicleManagementSystem.Models;

namespace VehicleManagementSystem.Classes.proxy
{
    public class proxy_vms :  Controller,vms 
    {

        private vms obj = new real_vms();

        private static List<string> userSites = new List<string>();
      

        public bool openLink(account x, string viewName, string ControllerName)
        {
            if(x.role.ToLower() == "admin")
            {
                obj.openLink(x, viewName, ControllerName);  
              
                return true;
                throw new Exception("is admin . true access allowed");
            }
           return false;

        }
    }
}
