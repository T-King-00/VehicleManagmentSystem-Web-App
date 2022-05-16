using System;
namespace VehicleManagementSystem.Models.Vehicle
{
    public abstract class AVehicleComponent : Component
    {
      //  public Guid ComponentID { get; set; }
      //  public string ComponentName { get; set; }
      // public double price { get; set; }
      // public string Manufacture { get; set; }

        protected AVehicleComponent(string componentName)
        {
            this.ComponentName = componentName;
        }

      
        public abstract void addComponent(AVehicleComponent obj);
        public abstract void deleteComponent(AVehicleComponent obj);
        public abstract void updateComponent(double Price, string Manufacture, string ComponentName);

        public abstract double returnPrice();



    }
}
