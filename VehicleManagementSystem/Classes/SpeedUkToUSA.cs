using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VehicleManagementSystem.Classes
{
    public class SpeedUkToUSA : ISpeed
    {
        double speed;

        public SpeedUkToUSA(double? speed)
        {
            if(speed!=null)
            {
                this.speed = (double)speed;
        
            }
          

        }


        public double ChangeSpeed()
        {
            return this.speed*= 1.25;
        }
    }
}