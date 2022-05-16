
using VehicleManagementSystem.Models;

namespace VehicleManagementSystem.Models.VEHICLE_FACTORY
{
    public  class VehicleFactory 
    {
        
        public Vehicles getVehicle(string vehicle)
        {
            if (vehicle.ToLower()=="car")
            {
                Vehicles car = new Car();
                car.vehicleType = "car";

                return car;    
            }
            else if (vehicle.ToLower() == "bus")
            {
                Vehicles bus = new Bus();
                bus.vehicleType = "bus";
                return bus;
            }
            else if (vehicle.ToLower() == "bike")
            {
                Vehicles bike = new Bike();
                bike.vehicleType = "bike";
                return bike;
            }
            return null;
        }



    }
}
