using System;
using VehicleManagementSystem.Models;


namespace VehicleManagementSystem.Models.VEHICLE_FACTORY.decorater
{
    public class PetrolEngine : CarDecorater
    {


        public PetrolEngine(Vehicles decoratedCar) : base(decoratedCar)
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
            if (this.decoratedCar.VehicleBrand.ToLower() == "bmw")
            {
                this.decoratedCar.EngineType = "PetrolEngine";
                this.decoratedCar.price = Setprice();
            }
            if (this.decoratedCar.EngineType.ToLower() == "hyndai")
            {
                this.decoratedCar.EngineType = "PetrolEngine";
                this.decoratedCar.price = Setprice();
            }
            else if (this.decoratedCar.VehicleBrand.ToLower() == "mercedes")
            {
                this.decoratedCar.EngineType = "PetrolEngine";
                this.decoratedCar.price = Setprice();
            }


        }
        private double setEnginePrice(string brand)
        {
            if (brand.ToLower() == "bmw")
            {
                return 5511;
            }

            else if (brand.ToLower() == "hyndai")
            {
                return 3511;
            }
            else if (brand.ToLower() == "mercedes")
            {
                return 4511;
            }
            else
            {
                Console.WriteLine("SetEnginePrice : brand not  found ");
                return 0;
            }
          

        }

        public double Setprice()
        {

            return base.SetPrice() + setEnginePrice(this.VehicleBrand);
        }

    }
}
