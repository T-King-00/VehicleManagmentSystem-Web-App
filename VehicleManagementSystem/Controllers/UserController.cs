using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VehicleManagementSystem.Classes;
using VehicleManagementSystem.Classes.proxy;
using VehicleManagementSystem.Models;



namespace VehicleManagementSystem.Controllers
{
    //this class implements Single Responsibility Principle as all pages are user pages 
    public class UserController : Controller
    {
        //usage of singleton pattern
        private Models.VehicleMSysEntities db;

        private List<Vehicles> ListOfFavVehicles;

        // GET: User
        public UserController()
        {
            ListOfFavVehicles=new List<Vehicles>(); 
        }

        public ActionResult Products(string searchForType)
        {
            db=Classes.SingleDbObject.getInstance(); 

            //gets chocies from radio boxes in view 
            string searchType  = Request["filter[1]"];
            string searchBrand = Request["filter[2]"];
            string searchPrice = Request["filter[3]"];
            string loc         = Request["filter[4]"];
            string fav         = Request["filter[5]"];
            ViewBag.TempLoc=loc;

           
            Debug.Write("serach type "+ searchType + "search brand "+searchBrand + "search price" + searchPrice);

            var VarVehicles = from v in db.Vehicles1
                              where v.isAvailable == true
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
            if(loc=="usa")
            { 
                
                for (int i = 0; i < count; i++)
                {
                    Vehicles y = allVehicles[i];

                    SpeedToUSA chaneSpeedToUsa= new SpeedToUSA(y.price);
                    y.price = chaneSpeedToUsa.ChangeSpeed();
                    allVehicles[i].price = y.price;

                }


            }
           else if (loc == "uk")
            {

                for (int i = 0; i < count; i++)
                {
                    Vehicles y = allVehicles[i];
                    SpeedtoUk chaneSpeedToUk = new SpeedtoUk(y.price);
                    y.price = chaneSpeedToUk.ChangeSpeed();

                    allVehicles[i].price = y.price;

                }
            }
            else if (String.IsNullOrEmpty(loc))
            {

                for (int i = 0; i < count; i++)
                {
                    Vehicles y = allVehicles[i];
                

                    allVehicles[i].price = y.price;

                }
            }


            if (fav == "fav")
            {
                List <Vehicles> temp = Session["FavVehicles"] as List<Vehicles>;


                if (temp != null)
                {
                    allVehicles = Session["FavVehicles"] as List<Vehicles>;
                }
                else
                {
                    allVehicles = null;
                }
            }
            else if (String.IsNullOrEmpty(fav))
            {

            }
            if(allVehicles != null)
            {
                VarVehicles = allVehicles.AsQueryable();
                return View(VarVehicles.ToList());
            }
            
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
            db = Classes.SingleDbObject.getInstance();
            string id = Request["vehicleID"];
            Guid id1 = Guid.Parse(id);
            var V = (from v in db.Vehicles1
                               where v.VehicleGUID == id1
                               select v).First();


            V.isAvailable = false;
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {

            }
            return View();  
        }


        public ActionResult AddtoFav()
        {
            db = Classes.SingleDbObject.getInstance();
            string id = Request["vehicleID"];
            Guid id1=Guid.Parse(id);    
            Vehicles vehicle=db.Vehicles1.Where(v => v.VehicleGUID == id1).First();



            if (Session["FavCounter"] != null)
            {
                ListOfFavVehicles = Session["FavVehicles"] as List<Vehicles>;
                ListOfFavVehicles.Add(vehicle);
                Session["FavCounter"] = ListOfFavVehicles.Count;
                Session["FavVehicles"] = ListOfFavVehicles;

            }
            else
            {
                ListOfFavVehicles.Add(vehicle);
                Session["FavCounter"] = ListOfFavVehicles.Count;
                Session["FavVehicles"] = ListOfFavVehicles;


            }

            return RedirectToAction("Products");
           

        }

        public ActionResult RemoveFromFav()
        {
            db = Classes.SingleDbObject.getInstance();
            string id = Request["vehicleID"];
            Guid id1 = Guid.Parse(id);
            Vehicles vehicle = db.Vehicles1.Where(v => v.VehicleGUID == id1).First();



            if (Session["FavCounter"] != null)
            {
                ListOfFavVehicles = Session["FavVehicles"] as List<Vehicles>;
                ListOfFavVehicles.Remove(vehicle);
                Session["FavCounter"] = ListOfFavVehicles.Count;
                Session["FavVehicles"] = ListOfFavVehicles;

            }
            else
            {
                ListOfFavVehicles.Remove(vehicle);
                Session["FavCounter"] = ListOfFavVehicles.Count;
                Session["FavVehicles"] = ListOfFavVehicles;


            }

            return RedirectToAction("Products");


        }

        //Access denied 
        public ActionResult AccessDenied()
        {
            return View();      
        }



    }

    public class CopyOfUserController : Controller
    {
        //usage of singleton pattern
        private Models.VehicleMSysEntities db;
        private List<Vehicles> ListOfFavVehicles;
        // GET: User
        public CopyOfUserController()
        {
            ListOfFavVehicles = new List<Vehicles>();
        }

        public ActionResult Products(string searchForType)
        {
            db = Classes.SingleDbObject.getInstance();

            //gets chocies from radio boxes in view 
            string searchType = Request["filter[1]"];
            string searchBrand = Request["filter[2]"];
            string searchPrice = Request["filter[3]"];
            string loc = Request["filter[4]"];
            string fav = Request["filter[5]"];
            ViewBag.TempLoc = loc;


            Debug.Write("serach type " + searchType + "search brand " + searchBrand + "search price" + searchPrice);

            var VarVehicles = from v in db.Vehicles1
                              where v.isAvailable == true
                              select v;



            if (!String.IsNullOrEmpty(searchType) && !String.IsNullOrEmpty(searchBrand) && !String.IsNullOrEmpty(searchPrice))
            {
                double x = double.Parse(searchPrice);
                VarVehicles = from v in db.Vehicles1
                              where v.price <= x
                              select v;
                VarVehicles = VarVehicles.Where(v => v.VehicleBrand.Contains(searchBrand) && v.vehicleType.Contains(searchType));

            }
            else if (!String.IsNullOrEmpty(searchType) && !String.IsNullOrEmpty(searchPrice))
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
                double x = double.Parse(searchPrice);
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
            if (loc == "usa")
            {

                for (int i = 0; i < count; i++)
                {
                    Vehicles y = allVehicles[i];

                    SpeedToUSA chaneSpeedToUsa = new SpeedToUSA(y.price);
                    y.price = chaneSpeedToUsa.ChangeSpeed();
                    allVehicles[i].price = y.price;

                }


            }
            else if (loc == "uk")
            {

                for (int i = 0; i < count; i++)
                {
                    Vehicles y = allVehicles[i];
                    SpeedtoUk chaneSpeedToUk = new SpeedtoUk(y.price);
                    y.price = chaneSpeedToUk.ChangeSpeed();

                    allVehicles[i].price = y.price;

                }
            }
            else if (String.IsNullOrEmpty(loc))
            {
                var VarVehicles2 = (from v in db.Vehicles1
                                  where v.isAvailable == true
                                  select v);
                 allVehicles = VarVehicles2.ToList();

              
            }


            if (fav == "fav")
            {
                List<Vehicles> temp = Session["FavVehicles"] as List<Vehicles>;


                if (temp != null)
                {
                    allVehicles = Session["FavVehicles"] as List<Vehicles>;
                }
                else
                {
                    allVehicles = null;
                }
            }
            else if (String.IsNullOrEmpty(fav))
            {

            }
            if (allVehicles != null)
            {
                VarVehicles = allVehicles.AsQueryable();
                return View(VarVehicles.ToList());
            }

            return View(VarVehicles.ToList());

        }

        //get one vehicle detail and its components
        public ActionResult OneVehicleDetails()
        {
            string id = Request["vehicleID"];
            db = Classes.SingleDbObject.getInstance();
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
            db = Classes.SingleDbObject.getInstance();
            string id = Request["vehicleID"];
            Guid id1 = Guid.Parse(id);
            var V = (from v in db.Vehicles1
                     where v.VehicleGUID == id1
                     select v).First();


            V.isAvailable = false;
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {

            }
            return View();
        }


        public ActionResult AddtoFav()
        {
            db = Classes.SingleDbObject.getInstance();
            string id = Request["vehicleID"];
            Guid id1 = Guid.Parse(id);
            Vehicles vehicle = db.Vehicles1.Where(v => v.VehicleGUID == id1).First();



            if (Session["FavCounter"] != null)
            {
                ListOfFavVehicles = Session["FavVehicles"] as List<Vehicles>;
                ListOfFavVehicles.Add(vehicle);
                Session["FavCounter"] = ListOfFavVehicles.Count;
                Session["FavVehicles"] = ListOfFavVehicles;

            }
            else
            {
                ListOfFavVehicles.Add(vehicle);
                Session["FavCounter"] = ListOfFavVehicles.Count;
                Session["FavVehicles"] = ListOfFavVehicles;


            }

            return RedirectToAction("Products");


        }

        public ActionResult RemoveFromFav()
        {
            db = Classes.SingleDbObject.getInstance();
            string id = Request["vehicleID"];
            Guid id1 = Guid.Parse(id);
            Vehicles vehicle = db.Vehicles1.Where(v => v.VehicleGUID == id1).First();



            if (Session["FavCounter"] != null)
            {
                ListOfFavVehicles = Session["FavVehicles"] as List<Vehicles>;
                ListOfFavVehicles.Remove(vehicle);
                Session["FavCounter"] = ListOfFavVehicles.Count;
                Session["FavVehicles"] = ListOfFavVehicles;

            }
            else
            {
                ListOfFavVehicles.Remove(vehicle);
                Session["FavCounter"] = ListOfFavVehicles.Count;
                Session["FavVehicles"] = ListOfFavVehicles;


            }

            return RedirectToAction("Products");


        }

        //Access denied 
        public ActionResult AccessDenied()
        {
            return View();
        }



    }
}