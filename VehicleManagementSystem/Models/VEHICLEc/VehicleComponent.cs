using System;
using VehicleManagementSystem.Models.Vehicle;

namespace VehicleManagementSystem.Models
{
    public class VehicleComponent : AVehicleComponent     //leafObject

    {
        public VehicleComponent(string componentName) : base(componentName)
        {
            this.ComponentName = componentName;
        }

        // properties support encapsulation 

    

        public override void addComponent(AVehicleComponent obj)
        {
            throw new NotImplementedException();
        }

        public override void deleteComponent(AVehicleComponent obj)
        {
            throw new NotImplementedException();
        }


        public override double returnPrice()
        {
            return this.price;
        }

        public override void updateComponent(double Price, string Manufacture, string ComponentName)
        {
            this.price = Price;
            this.Manufacture = Manufacture; 
            this.ComponentName = ComponentName; 
        }
    }
}
