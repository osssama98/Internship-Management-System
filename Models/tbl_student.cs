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
    
    public partial class tbl_student
    {
        public int s_studentnumber { get; set; }
        public int s_userid { get; set; }
        public string s_name { get; set; }
        public string s_lastname { get; set; }
        public int s_departmentid { get; set; }
        public int s_internid { get; set; }
        public int s_studyyear { get; set; }
        public byte s_semester { get; set; }
        public string s_address { get; set; }
        public string s_advisorname { get; set; }
        public int s_advisorid { get; set; }
        public string s_idnumber { get; set; }
        public string s_nationality { get; set; }
        public System.DateTime s_dateofbirth { get; set; }
        public byte[] s_photo { get; set; }
        public Nullable<int> s_internid2 { get; set; }
        public Nullable<int> s_applicationid { get; set; }
    
        public virtual tbl_application tbl_application { get; set; }
        public virtual tbl_department tbl_department { get; set; }
        public virtual tbl_internship tbl_internship { get; set; }
        public virtual tbl_internship tbl_internship1 { get; set; }
        public virtual tbl_user tbl_user { get; set; }
    }
}
