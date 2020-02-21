namespace ITSPublicApp.Domain.Models
{
    public class Solicitor
    {
        public int SolicitorID { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PostCode { get; set; }
        public string Fax { get; set; }
        public string ReferenceNumber { get; set; }
        public bool IsReferralUnderJointInstruction { get; set; }
        public string City { get; set; }

        public string Region { get; set; }
    }
}
