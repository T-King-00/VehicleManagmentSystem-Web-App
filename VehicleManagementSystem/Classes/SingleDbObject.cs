using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


// SINGLTON Pattern is used for db entities 
namespace VehicleManagementSystem.Classes
{
    public class SingleDbObject
    {

        private static Models.VehicleMSysEntities dbObj = new Models.VehicleMSysEntities();
        private SingleDbObject() {}
        public static Models.VehicleMSysEntities getInstance()
        {
            if(dbObj==null)
            {
                dbObj = new Models.VehicleMSysEntities();
            }
            return dbObj;
        }
        public static void Dispose(bool disposing)
        {
            if (disposing)
            {
                dbObj.Dispose();
            }
         
        }



    }
}