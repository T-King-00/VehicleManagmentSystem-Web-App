
namespace VehicleManagementSystem.Models.VEHICLE_FACTORY
{
    using VehicleManagementSystem.Models;
    public abstract class CarDecorater : Vehicles
    {

        protected Vehicles decoratedCar;

        public CarDecorater(Vehicles decoratedCar)
        {

            this.decoratedCar = decoratedCar;
        }


        public override double SetPrice()
        {
            return decoratedCar.SetPrice(); 
        }


        public abstract Vehicles manufactureCar();
      




    }
}
