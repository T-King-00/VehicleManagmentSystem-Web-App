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
    
    public partial class CarComponentList
    {
        public System.Guid CarGUID { get; set; }
        public int ComponentID { get; set; }
        public int ComponentListID { get; set; }
    
        public virtual Car Car { get; set; }
        public virtual Component Component { get; set; }
    }
}