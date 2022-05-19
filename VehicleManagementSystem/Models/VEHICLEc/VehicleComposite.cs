using System;
using System.Collections.Generic;

using System.Data.Entity.Validation;
using System.Net;

using System.Data.Entity;
using System.Linq;
using System.Web.ModelBinding;
using VehicleManagementSystem.Models.Vehicle;
using System.Diagnostics;

namespace VehicleManagementSystem.Models.Vehicle
{
    public class VehicleComposite : AVehicleComponent
    {

        public Vehicles vehicle { get; set; }

        public Vehicles Vehicles
        {
            get => default;
            set
            {
            }
        }

        private List<AVehicleComponent> vehicleCompositeList = new List<AVehicleComponent>();

        public VehicleComposite(Vehicles vehicle, string c): base(c)
        {
         
            this.vehicle = vehicle;
            
        }

         public override void addComponent(AVehicleComponent obj)
        {
            vehicleCompositeList.Add(obj);
           // vehicle.vehicleCompositeList.Add(obj);
            Models.VehicleMSysEntities db = Classes.SingleDbObject.getInstance();
            /*   var result =from v in db.VehicleComponentLists
                           where v.VehicleGUID.Equals(vehicle.VehicleGUID)
                           select v;*/

            //removing old components from db of that car and updating price
            var x = (from v in db.VehicleComponentLists
                     where v.VehicleGUID == vehicle.VehicleGUID
                     select v).ToList();

            int count = x.Count();

            for(int i=0; i<count;i++)
            {
                int comid = x[i].ComponentID;
               var component = (from v in db.Components
                                where v.ComponentID.Equals(comid) 
                                select v).ToList();
                foreach (var c in component)
                {
                    vehicle.price = vehicle.price - c.price;
                }


            }
        
   
            //removing them from database
            List<VehicleComponentList> entity1; 
            entity1 = (List < VehicleComponentList >) x;
            foreach(var v in entity1)
            {
              
                db.VehicleComponentLists.Remove(v);
            }


            //adding new component
            vehicle.price = vehicle.price + obj.price;
            VehicleComponentList entity = new VehicleComponentList();
            entity.ComponentListID = vehicle.componentListID;
            entity.VehicleGUID = vehicle.VehicleGUID;
            entity.ComponentID = 1;

            entity.Vehicle = vehicle;

            db.VehicleComponentLists.Add(entity);



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
                       Debug.WriteLine("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                    }
                }
            }

        }


        public override void deleteComponent(AVehicleComponent obj)
        {
            vehicleCompositeList.Remove(obj);
            //vehicle.vehicleCompositeList.Remove(obj);
        }


        public override void updateComponent(double Price, string Manufacture, string ComponentName)
        {
            throw new NotImplementedException();
        }

        public override void changePrice(double newPrice)
        {
            this.price=newPrice;
     

           /* double sum = 0;

            foreach (AVehicleComponent comp in vehicle.vehicleCompositeList)
            {
                sum = sum + comp.returnPrice();

            }
            return sum; */
        }

        public override double getPrice()
        {
            
            foreach(var item in vehicleCompositeList)
            {
                item.showPrice();
            }

            return 0;
        }

     

        public override void showPrice()
        {
            throw new NotImplementedException();
        }
    }
}
