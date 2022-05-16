using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VehicleManagementSystem.Models.VEHICLE_FACTORY.decorater
{
    public class DieselEngine : CarDecorater
    {
        public DieselEngine(Vehicles decoratedCar) : base(decoratedCar)
        {
            this.decoratedCar = decoratedCar;
            
        }
        public override Vehicles manufactureCar()
        {
            decoratedCar.manufactureCar();
            AddEngine();
            return decoratedCar;
        }


        public void AddEngine()
        {
            if(this.decoratedCar.VehicleBrand.ToLower()=="bmw")
            {
                this.decoratedCar.EngineType= "DieselEngine";
                this.decoratedCar.price= Setprice();
            }
            else if (this.decoratedCar.VehicleBrand.ToLower() == "hyndai")
            {
                this.decoratedCar.EngineType = "DieselEngine";
                this.decoratedCar.price = Setprice();
            }
            else if (this.decoratedCar.VehicleBrand.ToLower() == "mercedes")
            {
                this.decoratedCar.EngineType = "DieselEngine";
                this.decoratedCar.price = Setprice();
            }


        }
        private double setEnginePrice(string brand)
        {
            if (brand.ToLower() == "bmw")
            {
                -return 5522;
            }

            else if (brand.ToLower() == "hyndai")
            {
                return 3522;
            }
            else if (brand.ToLower() == "mercedes")
            {
                return 4522;
            }
            else
            {
                Console.WriteLine("SetEnginePrice : brand not  found ");
                return 0;
            }

        }

        public double Setprice()
        {

            return base.SetPrice() + setEnginePrice(this.decoratedCar.VehicleBrand);
        }
     
    }
    
}