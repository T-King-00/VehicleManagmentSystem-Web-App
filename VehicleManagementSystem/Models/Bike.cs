//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VehicleManagementSystem.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Bike : Vehicles
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Bike()
        {
            this.BikeComponentLists = new HashSet<BikeComponentList>();
        }
    
        public System.Guid BikeGUID { get; set; }
        public string name { get; set; }
       /* public string EngineType { get; set; }
        public int componentListID { get; set; }
        public bool isAvailable { get; set; }
        public string color { get; set; }
        public string model { get; set; }*/
        public string manufactureCompany { get; set; }
       // public Nullable<double> price { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BikeComponentList> BikeComponentLists { get; set; }
    }
}
