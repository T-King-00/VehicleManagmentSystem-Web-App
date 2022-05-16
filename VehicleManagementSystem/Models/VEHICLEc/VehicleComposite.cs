using System;
using System.Collections.Generic;

using System.Data.Entity.Validation;
using System.Net;

using System.Data.Entity;
using System.Linq;
using System.Web.ModelBinding;
using VehicleManagementSystem.Models.Vehicle;

namespace VehicleManagementSystem.Models.Vehicle
{
    public class VehicleComposite : AVehicleComponent
    {

        public Vehicles vehicle { get; set; }
    
        public VehicleComposite(Vehicles vehicle, string c): base(c)
        {
         
            this.vehicle = vehicle;
            //vehicle.vehicleCompositeList = new List<AVehicleComponent>();
        }

         public override void addComponent(AVehicleComponent obj)
        {
           // vehicle.vehicleCompositeList.Add(obj);
            Models.VehicleMSysEntities db = Classes.SingleDbObject.getInstance();
            var result =from v in db.VehicleComponentLists
                        where v.VehicleGUID.Equals(vehicle.VehicleGUID)
                        select v;

            VehicleComponentList entity = new VehicleComponentList();
            entity.ComponentListID = vehicle.componentListID;
            entity.VehicleGUID = vehicle.VehicleGUID;
            entity.ComponentID = obj.ComponentID;
           // entity.Vehicle = vehicle;
          //  entity.Component = obj;

            db.VehicleComponentLists.Add(entity);

           

            try
            {
               
                db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                // Provide for exceptions.
            }
        
        }


        public override void deleteComponent(AVehicleComponent obj)
        {
            vehicle.vehicleCompositeList.Remove(obj);
        }


        public override void updateComponent(double Price, string Manufacture, string ComponentName)
        {
            throw new NotImplementedException();
        }

        public override double returnPrice()
        {
            double sum = 0;

            foreach (AVehicleComponent comp in vehicle.vehicleCompositeList)
            {
                sum = sum + comp.returnPrice();

            }
            return sum;
        }     
    }
}
