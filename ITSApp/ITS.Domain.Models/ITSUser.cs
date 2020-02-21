/*
 Page Name:  ITSUser                     
 Latest Version:  1.1                                              
  Purpose:  Created Model For User Login Information.                                                      
  Revision History:                                      
    
 * version  :  1.0
 * Date     :   11/02/2012
 * Edited By: Vishal Sen
 * Reason   :   Create User Validation For Login Purpose.
 * 
 
 * version  :  1.1
 * Date     :   11/06/2012
 * Edited By: Pardeep,Harpreet Singh
 * Reason   :   Change the Model according to ITS Service.

 */
using System;
namespace ITS.Domain.Models
{
    [Serializable()]
    public class ITSUser
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsLocked { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get { return FirstName + " " + LastName; } }
        public int UserTypeID { get; set; }
        public int? SupplierID { get; set; }
        public int? ReferrerID { get; set; }
        public int? ReferrerLocationID { get; set; }
        public string Email { get; set; }
        public string Fax { get; set; }
        public string Telephone { get; set; }
        public string UserSessionID { get; set; }


        public int Cnt { get; set; }
        public string UserIDEncry { get; set; }
        public string str { get; set; }
        public string Message { get; set; }
        public string PasswordOTP { get; set; }
        public string PasswordOTPDB { get; set; }  
    }
}
