//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VOCATION_MVC.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Tbl_VacationAppeals
    {
        public int id { get; set; }
        public int PersonId { get; set; }
        public Nullable<bool> Status { get; set; }
        public System.DateTime RegDate { get; set; }
        public System.DateTime StartDate { get; set; }
        public int DayCount { get; set; }
        public bool IsActive { get; set; }
    
        public virtual Tbl_Persons Tbl_Persons { get; set; }
    }
}
