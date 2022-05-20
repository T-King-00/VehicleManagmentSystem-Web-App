using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VehicleManagementSystem.Classes
{
    public class SpeedtoUk : ISpeed
    {

        double speed;

        public SpeedtoUk(double? speed)
        {
            if (speed != null)
            {
                this.speed = (double)speed;

            }


        }


        public double ChangeSpeed()
        {
            return this.speed /= 1.25;
        }
    }
}