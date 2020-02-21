using System;
namespace ITSPublicApp.Domain.Models
{
    [Serializable()]
    public class ITSUser
    {
        public int UserID { get; set; }
        public string EncryptUserID { get; set; }

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
        public string EyptReferrerLocationID { get; set; }
        public string Email { get; set; }
        public string Fax { get; set; }
        public string Telephone { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public int FailedAttemptCount { get; set; }
        public DateTime? PasswordExpirationDate { get; set; }
        public int Cnt { get; set; }
        public string UserIDEncry { get; set; }
        public string str { get; set; }
        public string Message { get; set; }
        public string PasswordOTP { get; set; }
        public string PasswordOTPDB { get; set; }     
    }
}