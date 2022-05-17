using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VehicleManagementSystem.Controllers
{
    public class UserController : Controller
    {
        //usage of singleton pattern
        private Models.VehicleMSysEntities db;

        // GET: User
        public ActionResult Products(string searchForType)
        {   db=Classes.SingleDbObject.getInstance(); 

                //searchForType = Request["searchForType"];
      
            string searchType  = Request["filter[1]"];
            string searchBrand = Request["filter[2]"];
            string searchPrice = Request["filter[3]"];


            Debug.Write("serach type "+ searchType + "search brand "+searchBrand + "search price" + searchPrice);

            bool isType = false, isBrand = false, isPrice = false;
            bool isPrice_Brand = false, isPrice_Type = false;  
            bool isType_brand = false, isType_brand_price=false;

            var VarVehicles = from v in db.Vehicles1 
                             select v;

            if (!String.IsNullOrEmpty(searchType) && !String.IsNullOrEmpty(searchBrand) && !String.IsNullOrEmpty(searchPrice))
            {
                isType_brand_price = true;
                double x = double.Parse(searchPrice);
                VarVehicles = from v in db.Vehicles1
                              where v.price <= x
                              select v;

                VarVehicles = VarVehicles.Where(v => v.VehicleBrand.Contains(searchBrand) && v.vehicleType.Contains(searchType));
                
            }
            else if (!String.IsNullOrEmpty(searchType) && !String.IsNullOrEmpty(searchPrice) )
            {
                isPrice_Type = true;
                double x = double.Parse(searchPrice);
                VarVehicles = from v in db.Vehicles1
                              where v.price <= x
                              select v;

                VarVehicles = from v in db.Vehicles1
                                  where v.vehicleType == searchType 
                                  select v;
                VarVehicles = VarVehicles.Where(v => v.vehicleType.Contains(searchType));


            }
            else if (!String.IsNullOrEmpty(searchType) && !String.IsNullOrEmpty(searchBrand))
            {
                isType_brand = true;
                VarVehicles = from v in db.Vehicles1
                              where v.vehicleType == searchType
                              select v;
                VarVehicles = VarVehicles.Where(v => v.VehicleBrand.Contains(searchBrand));

            }
            else if (!String.IsNullOrEmpty(searchPrice) && !String.IsNullOrEmpty(searchBrand))
            {
                isPrice_Brand = true;
                double x=double.Parse(searchPrice);
                VarVehicles = from v in db.Vehicles1
                              where v.price <= x
                              select v;
                VarVehicles = VarVehicles.Where(v => v.VehicleBrand.Contains(searchBrand));

            }
            else if (!String.IsNullOrEmpty(searchType))
            {
                isType = true;
                VarVehicles = VarVehicles.Where(v => v.vehicleType.Contains(searchType));

            }
            else if (!String.IsNullOrEmpty(searchPrice))
            {
                isPrice = true;
                double x = double.Parse(searchPrice);
                VarVehicles = from v in db.Vehicles1
                              where v.price <= x
                              select v;
            }
            else if (!String.IsNullOrEmpty(searchBrand))
            {
                isBrand = true;
                VarVehicles = from v in db.Vehicles1
                                  where v.VehicleBrand == searchBrand.ToLower()
                                  select v;
              

            }




         //   db.Dispose();
         

            return View(VarVehicles.ToList());
           
        }

      

    }
}