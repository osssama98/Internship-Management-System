//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace InternshipManagementSystem.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_academician
    {
        public int a_userid { get; set; }
        public string a_lastname { get; set; }
        public string a_name { get; set; }
        public int a_departmentid { get; set; }
    
        public virtual tbl_department tbl_department { get; set; }
        public virtual tbl_user tbl_user { get; set; }
    }
}
