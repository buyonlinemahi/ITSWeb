using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITS.Domain.Models.UserModel
{
    public class User
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int UserTypeID { get; set; }
        public bool IsLocked { get; set; }
        public string SupplierTypes { get; set; }   
        public int? SupplierID { get; set; }
        public string ReferrerTypes { get; set; }   
        public int? ReferrerID { get; set; }
        public int? ReferrerLocationID { get; set; }
        public string Email { get; set; }
        public string Fax { get; set; }
        public string Telephone { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public int FailedAttemptCount { get; set; }
        public string DateAdded { get; set; }
        public bool IsPasswordDirty { get; set; }
        public int[] ReferrerProjectID { get; set; }
        public int? OldUserProjectSingleValue { get; set; }
        public DateTime? PasswordExpirationDate { get; set; }
        public string UserSessionID { get; set; }
        public string PasswordOTP { get; set; }
      

    }
}
