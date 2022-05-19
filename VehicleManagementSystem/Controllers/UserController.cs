using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VehicleManagementSystem.Classes.proxy;
using VehicleManagementSystem.Models;

namespace VehicleManagementSystem.Controllers
{
    public class UserController : Controller
    {
        //usage of singleton pattern
        private Models.VehicleMSysEntities db;

        // GET: User

        
        public ActionResult Products(string searchForType)
        {
            db=Classes.SingleDbObject.getInstance(); 

            //gets chocies from radio boxes in view 
            string searchType  = Request["filter[1]"];
            string searchBrand = Request["filter[2]"];
            string searchPrice = Request["filter[3]"];
            Debug.Write("serach type "+ searchType + "search brand "+searchBrand + "search price" + searchPrice);

            var VarVehicles = from v in db.Vehicles1 
                             select v;
            if (!String.IsNullOrEmpty(searchType) && !String.IsNullOrEmpty(searchBrand) && !String.IsNullOrEmpty(searchPrice))
            {
                double x = double.Parse(searchPrice);
                VarVehicles = from v in db.Vehicles1
                              where v.price <= x
                              select v;
                VarVehicles = VarVehicles.Where(v => v.VehicleBrand.Contains(searchBrand) && v.vehicleType.Contains(searchType));
                
            }
            else if (!String.IsNullOrEmpty(searchType) && !String.IsNullOrEmpty(searchPrice) )
            {  
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
                VarVehicles = from v in db.Vehicles1
                              where v.vehicleType == searchType
                              select v;
                VarVehicles = VarVehicles.Where(v => v.VehicleBrand.Contains(searchBrand));
            }
            else if (!String.IsNullOrEmpty(searchPrice) && !String.IsNullOrEmpty(searchBrand))
            {
                double x=double.Parse(searchPrice);
                VarVehicles = from v in db.Vehicles1
                              where v.price <= x
                              select v;
                VarVehicles = VarVehicles.Where(v => v.VehicleBrand.Contains(searchBrand));

            }
            else if (!String.IsNullOrEmpty(searchType))
            {
                VarVehicles = VarVehicles.Where(v => v.vehicleType.Contains(searchType));
            }
            else if (!String.IsNullOrEmpty(searchPrice))
            {
                double x = double.Parse(searchPrice);
                VarVehicles = from v in db.Vehicles1
                              where v.price <= x
                              select v;
            }
            else if (!String.IsNullOrEmpty(searchBrand))
            {     
                VarVehicles = from v in db.Vehicles1
                                  where v.VehicleBrand == searchBrand.ToLower()
                                  select v;
            }
            //get comonent for each car
            var allVehicles = VarVehicles.ToList();
            int count = VarVehicles.Count();

            for (int i = 0; i < count; i++)
            {
                Vehicles y = allVehicles[i];
                y.Components = from c in db.Components
                               join v in db.VehicleComponentLists
                               on c.ComponentID equals v.ComponentID
                               where v.VehicleGUID == y.VehicleGUID
                               select c;
                allVehicles[i].Components = y.Components;

            }

            VarVehicles = allVehicles.AsQueryable();
            return View(VarVehicles.ToList());
           
        }

        //get one vehicle detail and its components
        public ActionResult OneVehicleDetails()
        {
            string id = Request["vehicleID"];
            db=Classes.SingleDbObject.getInstance();
            Guid id1 = Guid.Parse(id);
            var VarVehicles = (from v in db.Vehicles1
                                   where v.VehicleGUID == id1
                                   select v).First();



            VarVehicles.Components = from c in db.Components
                               join v in db.VehicleComponentLists
                               on c.ComponentID equals v.ComponentID
                               where v.VehicleGUID == VarVehicles.VehicleGUID
                               select c;
               


            

           




            return View(VarVehicles);  
        }

        public ActionResult Buy()
        {


            return View();  
        }


        //Access denied 
        public ActionResult AccessDenied()
        {
            return View();      
        }



    }
}