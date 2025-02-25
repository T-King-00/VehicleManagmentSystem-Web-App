﻿using System;
using VehicleManagementSystem.Models.Vehicle;

namespace VehicleManagementSystem.Models
{
    public class VehicleComponent : AVehicleComponent     //leafObject

    {
        public VehicleComponent(string componentName,double price) : base(componentName)
        {
            this.ComponentName = componentName;
            this.price = price;
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


        public override void changePrice(double newPrice)
        {
             this.price = newPrice;
        }
        public override double getPrice()
        {
           return this.price ;
        }
        public override void showPrice()
        {
           
        }


        public override void updateComponent(double Price, string Manufacture, string ComponentName)
        {
            this.price = Price;
            this.Manufacture = Manufacture; 
            this.ComponentName = ComponentName; 
        }
    }
}
