﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VehicleManagementSystem.Models
{
    public partial class Vehicles 
    {
        public IQueryable<Component> Components { get; set; } 

        public  void manufactureCar()
        {

        }

        public double SetPrice()
        {
            return (double)price;
        }


    }
}