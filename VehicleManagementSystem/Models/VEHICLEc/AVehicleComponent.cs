using System;
namespace VehicleManagementSystem.Models.Vehicle
{
    //extends component class to inherit its variables only :
    public abstract class AVehicleComponent :  Component
    {
   
        protected AVehicleComponent(string componentName)
        {
            this.ComponentName = componentName;
        }

      
        public abstract void addComponent(AVehicleComponent obj);
        public abstract void deleteComponent(AVehicleComponent obj);
        public abstract void updateComponent(double Price, string Manufacture, string ComponentName);

        public abstract void changePrice(double newPrice);

        public abstract double getPrice();
        public abstract void showPrice();


        //  public Guid ComponentID { get; set; }
        //  public string ComponentName { get; set; }
        // public double price { get; set; }
        // public string Manufacture { get; set; }

    }
}
