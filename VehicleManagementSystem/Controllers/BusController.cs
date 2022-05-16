using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VehicleManagementSystem.Models;

namespace VehicleManagementSystem.Controllers
{
    public class BusController : Controller
    {
        private VehicleMSysEntities db = new VehicleMSysEntities();

        // GET: Bus
        public ActionResult Index()
        {
            var buses = db.Buses.ToList();
            return View(buses.ToList());
        }

        // GET: Bus/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bus bus = db.Buses.Find(id);
            if (bus == null)
            {
                return HttpNotFound();
            }
            return View(bus);
        }

        // GET: Bus/Create
        public ActionResult Create()
        {
            ViewBag.BusGUID = new SelectList(db.Buses, "BusGUID", "name");
       
            return View();
        }

        // POST: Bus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BusGUID,name,EngineType,componentListID,isAvailable,color,model,manufactureCompany")] Bus bus)
        {
            if (ModelState.IsValid)
            {
                bus.BusGUID = Guid.NewGuid();
                db.Buses.Add(bus);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BusGUID = new SelectList(db.Buses, "BusGUID", "name", bus.BusGUID);
  
            return View(bus);
        }

        // GET: Bus/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bus bus = db.Buses.Find(id);
            if (bus == null)
            {
                return HttpNotFound();
            }
            ViewBag.BusGUID = new SelectList(db.Buses, "BusGUID", "name", bus.BusGUID);
            
            return View(bus);
        }

        // POST: Bus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BusGUID,name,EngineType,componentListID,isAvailable,color,model,manufactureCompany")] Bus bus)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bus).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BusGUID = new SelectList(db.Buses, "BusGUID", "name", bus.BusGUID);
            ViewBag.BusGUID = new SelectList(db.Buses, "BusGUID", "name", bus.BusGUID);
            return View(bus);
        }

        // GET: Bus/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bus bus = db.Buses.Find(id);
            if (bus == null)
            {
                return HttpNotFound();
            }
            return View(bus);
        }

        // POST: Bus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Bus bus = db.Buses.Find(id);
            db.Buses.Remove(bus);
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
