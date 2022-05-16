using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VehicleManagementSystem.Controllers
{
    public class UserController : Controller
    {
        //usage of singleton pattern
        private Models.VehicleMSysEntities db = Classes.SingleDbObject.getInstance();

        // GET: User
        public ActionResult Products(string searchForType)
        {
            searchForType = Request["searchForType"];

            var VarVehicles = from v in db.Vehicles1
                              select v;


            if (!String.IsNullOrEmpty(searchForType))
            {
                VarVehicles = VarVehicles.Where(v => v.vehicleType.Contains(searchForType));
            }

            return View(VarVehicles.ToList());
           
        }


      

    }
}