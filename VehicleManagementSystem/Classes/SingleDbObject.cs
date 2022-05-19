using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VehicleManagementSystem.Classes.proxy;


// SINGLTON Pattern is used for db entities 
namespace VehicleManagementSystem.Classes
{
    public class SingleDbObject
    {

        private static Models.VehicleMSysEntities dbObj = new Models.VehicleMSysEntities();
        private static proxy_vms newz = new proxy_vms();
        private SingleDbObject() {}
        public static Models.VehicleMSysEntities getInstance()
        {
            if(dbObj==null)
            {
                dbObj = new Models.VehicleMSysEntities();
            }
            return dbObj;
        }
        public static proxy_vms getInstanceProxy()
        {
            if (newz == null)
            {
                newz = new proxy_vms();
            }
            return newz;
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