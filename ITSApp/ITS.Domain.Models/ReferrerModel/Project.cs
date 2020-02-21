using System.Collections.Generic;
namespace ITS.Domain.Models.ReferrerModel
{
    public class Project
    {
        public int ReferrerProjectID { get; set; }
        public string ProjectName { get; set; }
        public int ReferrerID { get; set; }
        public int StatusID { get; set; }
        public bool FirstAppointmentOffered { get; set; }
        public bool Enabled { get; set; }
        public bool IsTriage { get; set; }
        public int EmailSendingOptionID { get; set; }
        public string CentralEmail { get; set; }
        public bool? IsActive { get; set; }
        public IEnumerable<ProjectTreatment> TreatmentCategories { get; set; }
    }
}
