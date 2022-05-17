using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Net;


using VehicleManagementSystem.Models;
using VehicleManagementSystem.Models.VEHICLE_FACTORY;
using VehicleManagementSystem.Models.VEHICLE_FACTORY.decorater;
using Car = VehicleManagementSystem.Models.Car;
using VehicleManagementSystem.Models.Vehicle;
using VehicleManagementSystem.Classes;

namespace VehicleManagementSystem.Controllers
{
    public class VehicleController : Controller
    {
        private Models.VehicleMSysEntities db = Classes.SingleDbObject.getInstance();
        // GET: Vehicles
        public ActionResult Index()
        {
            return View(db.Vehicles1.ToList());
        }

        public ActionResult VehicleDetails(string searchForType)
        {
            searchForType = Request["searchForType"];

            var VarVehicles = from v in db.Vehicles1
                              select v;

            var allVehicles = VarVehicles.ToList();
            int count = VarVehicles.Count();

            for(int i=0;i< count;i++)
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

            if (!String.IsNullOrEmpty(searchForType))
            {
                VarVehicles = VarVehicles.Where(v => v.vehicleType.Contains(searchForType.ToLower()));
            }

            return View(VarVehicles);
        }

        // GET: Cars1/Details/5
        public ActionResult Details(Guid? id)
         {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Vehicles vehicle = db.Vehicles1.Find(id);
                if (vehicle == null)
                {
                    return HttpNotFound();
                }
                return View(vehicle);
          }

            // GET: Cars1/Create
        public ActionResult Create()
        {
            return View();
        }

            // POST: Cars1/Create
            // To protect from overposting attacks, enable the specific properties you want to bind to, for 
            // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult Create([Bind(Include = ",EngineType,price,componentListID,isAvailable,color,model,manufactureCompany")] Vehicles vehicle)
            {
                if (ModelState.IsValid)
                {
                    VehicleFactory Factory = new VehicleFactory();
                    Car n = new Car();
                    Vehicles carObj = Factory.getVehicle("car");

                    carObj.VehicleGUID= Guid.NewGuid();
                    carObj.isAvailable = true;
                    db.Vehicles1.Add(carObj);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(vehicle);
            }


            //Get : e   // Shows view only start of adding vehicle by choosing type
            public ActionResult ChooseVehicleType()
            {
                return View();
            }


            //sends value of the type of vehicle to server on clicking a button in the view . 
            [HttpPost]
            public ActionResult AddVehicle()
            {
                string vehicleType = Request["vehicleType"];
                VehicleFactory Factory = new VehicleFactory();
                Vehicles newVehicle = Factory.getVehicle(vehicleType);
                ViewBag.vehicleType = vehicleType;

                if (newVehicle.vehicleType.ToLower() == "car")
                {

                    return View(newVehicle);
                 }
            if (newVehicle.vehicleType.ToLower() == "bus")
            {

                return View(newVehicle);
            }
        
            if (newVehicle.vehicleType.ToLower() == "bike")
            {

                return View(newVehicle);
    }

            return null;
            }




            //sends value of the type of vehicle to server on clicking a button in the view . 
            [HttpPost]
            public ActionResult AddVehicleToDb([Bind(Include = "vehicleType,VehicleBrand,price,model,isAvailable,color")] Vehicles vehicle , string vehicleType)
            {
             //Console.WriteLine(vehicle.price);
                string vehicleType0 = Request["vehicleType"];
                string vehicleType1 = vehicleType;
                vehicle.VehicleGUID = Guid.NewGuid();
                vehicle.EngineType = "default";
                Random random = new Random();
                vehicle.componentListID = random.Next();
                
                db.Vehicles1.Add(vehicle);

         
                try
                {
                    db.SaveChanges();
                }
                catch (DbEntityValidationException ex)
                {
                    foreach (var entityValidationErrors in ex.EntityValidationErrors)
                    {
                        foreach (var validationError in entityValidationErrors.ValidationErrors)
                        {
                            Response.Write("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                        }
                    }
                }
                ViewBag.vehicleGuid = vehicle.VehicleGUID;
                return RedirectToAction("ChooseEngineTypeStatic", vehicle);

            }

            public ActionResult ChooseEngineType(Vehicles vehicle)
            {
                List<EngineModel> query = (from e in db.Engines
                                           where e.VehicleModel == vehicle.model
                                           select new EngineModel()
                                           {
                                               EngineType = e.EngineType,
                                               EnginePrice = e.EnginePrice,
                                               EngineId = e.EngineId
                                           }).ToList();

         
                ViewBag.enginesLength = query.Count;
                ViewBag.engines = query;
                ViewBag.engines = (List<EngineModel>)ViewBag.engines;

                ViewBag.Vehicle = (Vehicles) vehicle;
                return View(vehicle);
            }
        
         
            [HttpPost]
            public ActionResult ChooseEngineType()
            {
            
                string engineType = Request["engineType"];
                string vehicleId = Request["vehicleID"];
                Vehicles dbVehicle = db.Vehicles1.Find(Guid.Parse(vehicleId));
                dbVehicle =new PetrolEngine(dbVehicle);

                dbVehicle.EngineType = engineType;
                if (ModelState.IsValid)
                {
                    db.Entry(dbVehicle).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("VehicleDetails");
                }
                return View();
            }  

            public ActionResult ChooseEngineTypeStatic(Vehicles vehicle)
            {
              

                ViewBag.Vehicle = (Vehicles) vehicle;
                return View(vehicle);
            }




            [HttpPost]
            public ActionResult ChooseEngineTypeStatic(string engineType)
            {

                
                string ReqEngineType = Request["engineType"];
                string vehicleId = Request["vehicleID"];
                Vehicles dbVehicle = db.Vehicles1.Find(Guid.Parse(vehicleId));
                if(dbVehicle.vehicleType.ToLower()=="car")
                {
                    if(ReqEngineType.ToLower() == "dieselengine")
                    {
                        DieselEngine carWithDiselEngine = new DieselEngine(dbVehicle);
                        carWithDiselEngine.manufactureCar();
                    }
                    else if (ReqEngineType.ToLower() == "petrolengine")
                    {
                        PetrolEngine carWithDiselEngine = new PetrolEngine(dbVehicle);
                        carWithDiselEngine.manufactureCar();
                    }

            }
           
                if (ModelState.IsValid)
                {
                    db.Entry(dbVehicle).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("AddComponentsForVehicle", dbVehicle);
                }
                return View();
            }





        // GET: Cars1/Edit/5
        public ActionResult Edit(Guid? id)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Vehicles vehicle = db.Vehicles1.Find(id);
                if (vehicle == null)
                {
                    return HttpNotFound();
                }
                return View(vehicle);
            }

            // POST: Cars1/Edit/5
            // To protect from overposting attacks, enable the specific properties you want to bind to, for 
            // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult Edit([Bind(Include = "VehicleGUID,vehicleType,componentListID,isAvailable,color,model,VehicleBrand")] Vehicles veh)
            {
                ViewBag.vehicleGuid = veh.VehicleGUID;
                veh.EngineType = "default";

            if (ModelState.IsValid)
            {
                //  db.Entry(veh).State = EntityState.Modified;
                db.SaveChanges();
                if (veh.vehicleType.ToLower() == "car")
                {
                    return RedirectToAction("ChooseEngineTypeStatic", veh);
                }
                else
                {
                    return RedirectToAction("AddComponentsForVehicle", veh);
                }
            }
            
                return View(veh);
            }

            // GET: Cars1/Delete/5
            public ActionResult Delete(Guid? id)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Vehicles v = db.Vehicles1.Find(id);
                if (v == null)
                {
                    return HttpNotFound();
                }
                return View(v);
            }

            // POST: Cars1/Delete/5
            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public ActionResult DeleteConfirmed(Guid id)
            {
                Vehicles v = db.Vehicles1.Find(id);
                db.Vehicles1.Remove(v);
                db.SaveChanges();
                return RedirectToAction("Index");
            }


        public ActionResult AddComponentsForVehicle(Vehicles vehicle)
        {
           // string vehicleId = Request["vehicleID"];
           // Vehicles dbVehicle = db.Vehicles1.Find(Guid.Parse(vehicleId));
            ViewBag.VehicleGUID = vehicle.VehicleGUID;
            ViewBag.vehicle = vehicle;
            modelViewVehicle x = new modelViewVehicle();
            x.c = db.Components.ToList();
            x.v = vehicle;
            return View(x);

           
        }

        [HttpPost]
        public ActionResult AddComponentsForVehicle()
        {
           string vehicleId = Request["vehicleID"];
           string ComponentID = Request["ComponentID"];
            //   string compomnentId = Request["ComponentID"];

            //  Vehicles f = ViewBag.vehicle;
            Vehicles v = db.Vehicles1.Find(Guid.Parse(vehicleId));
            //get component details from db
            Component  xx=db.Components.Find(int.Parse(ComponentID));

            VehicleComposite composite = new VehicleComposite(v, "vehicle");
            //create obj 
            VehicleComponent componentX = new VehicleComponent(xx.ComponentName,xx.price);
            composite.addComponent(componentX);
            return RedirectToAction("VehicleDetails");


        }

     /*   protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (db != null)
                {
                   db.Dispose();
                   
                }
            }

            base.Dispose(disposing);
        }*/


    }
}
