using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using VehicleManagementSystem.Models;

namespace VehicleManagementSystem.Classes.proxy
{
    public interface vms
    {
        bool openLink(account x, string viewName, string ControllerName);

    }
}
