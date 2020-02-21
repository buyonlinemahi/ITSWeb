using System;
/*
 * Latest Version:  1.0
 
Page Name:  SupplierComplaint.cs      
Auther:     Anuj Batra
Version:    1.0                                              
Desc:       Created a model for SupplierComplaints                                                     

*/
namespace ITS.Domain.Models
{
    public class SupplierComplaint
    {
        public int SupplierComplaintID { get; set; }
        public int ComplaintTypeID { get; set; }
        public int ComplaintStatusID { get; set; }
        public string ComplaintDescription { get; set; }
        public DateTime ComplaintDate { get; set; }
        public int  SupplierID { get; set; }
        public string ComplaintStatusName { get; set; }
        public string ComplaintTypeName { get; set; }
    }
}
