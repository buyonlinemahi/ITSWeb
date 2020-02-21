namespace ITSPublicApp.Domain.Models
{
    public class ReferrerProject
    {
        public int ReferrerProjectID { get; set; }
        public string ProjectName { get; set; }
        public int ReferrerID { get; set; }
        public int StatusID { get; set; }
        public bool FirstAppointmentOffered { get; set; }
        public bool Enabled { get; set; }
        public bool IsTriage { get; set; }
        public bool? IsActive { get; set; }
    }
}
