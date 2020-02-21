using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ITS.Domain.Models.ReferrerModel;
namespace ITS.Domain.ViewModels
{
    public class ReferrerProjectTreatmentCategoryPricingViewModel
    {
        public int TreatmentCategoryID { get; set; }
        public string TreatmentCategoryName { get; set; }
        public int ReferrerProjectTreatmentID { get; set; }
        public bool Enabled { get; set; }
        public int ReferrerProjectID { get; set; }
        public IList<TreatmentPricing> Pricing { get; set; }
    }

    public class ReferrerProjectTreatmentCategoryViewModel
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
        public IList<ReferrerProjectTreatmentCategoryPricingViewModel> TreatmentCategories { get; set; }
    }
}
