using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Web.Mvc;

using VehicleManagementSystem.Models;
using VehicleManagementSystem.Models.VEHICLE_FACTORY;
using VehicleManagementSystem.Models.VEHICLE_FACTORY.decorater;
using Car = VehicleManagementSystem.Models.Car;

namespace VehicleManagementSystem.Controllers
{
    public class CarController : Controller
    {
        private Models.VehicleMSysEntities db = new Models.VehicleMSysEntities();

        // GET: Cars1
        public ActionResult Index()
        {
            return View(db.Cars.ToList());
        }

        public ActionResult VehicleDetails()
        {
            return View(db.Cars.ToList());
        }

        // GET: Cars1/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car car = db.Cars.Find(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            return View(car);
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
        public ActionResult Create([Bind(Include = ",EngineType,componentListID,isAvailable,color,model,manufactureCompany")] Car car)
        {
            if (ModelState.IsValid)
            {
                VehicleFactory Factory = new VehicleFactory();
                Car n = new Car();
                Vehicles carObj = Factory.getVehicle("Car");

                car.CarGUID = Guid.NewGuid();
                car.isAvailable = true;
                db.Cars.Add(car);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(car);
        }


        //Get : Vehicle/CreateVehicle   // Shows view only 
        public ActionResult CreateVehicle()
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

            return null;
        }




        //sends value of the type of vehicle to server on clicking a button in the view . 
        [HttpPost]

        public ActionResult AddVehicleToDb([Bind(Include = "brand,model,isAvailable,color")] Car car)
        {

            car.VehicleGUID = Guid.NewGuid();
            car.CarGUID = car.VehicleGUID;
            car.name = car.VehicleBrand;
            car.EngineType = "default";
            Random random = new Random();
            car.componentListID = random.Next();
            car.manufactureCompany = car.VehicleBrand;
            db.Cars.Add(car);
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
            ViewBag.carGuid = car.CarGUID;
            return RedirectToAction("ChooseEngineType", car);

        }

        public ActionResult ChooseEngineType(Car car)
        {



            List<EngineModel> query = (from e in db.Engines
                                       where e.VehicleModel == car.model
                                       select new EngineModel()
                                       {
                                           EngineType = e.EngineType,
                                           EnginePrice = e.EnginePrice,
                                           EngineId = e.EngineId
                                       }).ToList();

            ViewBag.enginesLength = query.Count;
            ViewBag.engines = query;
            ViewBag.engines = (List<EngineModel>)ViewBag.engines;

            ViewBag.car = (Car)car;
            return View(car);
        }






        [HttpPost]
        public ActionResult ChooseEngineType(Guid carID)
        {
            string engineType = Request["engineType"];
            string carId = Request["carID"];

            Car dbCar = db.Cars.Find(Guid.Parse(carId));

            dbCar.EngineType = engineType;
            if (ModelState.IsValid)
            {
                db.Entry(dbCar).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("VehicleDetails");
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
            Car car = db.Cars.Find(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            return View(car);
        }

        // POST: Cars1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CarGUID,name,EngineType,componentListID,isAvailable,color,model,manufactureCompany")] Car car)
        {
            if (ModelState.IsValid)
            {
                db.Entry(car).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(car);
        }

        // GET: Cars1/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car car = db.Cars.Find(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            return View(car);
        }

        // POST: Cars1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Car car = db.Cars.Find(id);
            db.Cars.Remove(car);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }













    }
}