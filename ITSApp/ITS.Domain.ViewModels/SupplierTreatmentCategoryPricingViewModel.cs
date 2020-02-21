using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ITS.Domain.Models.SupplierModel;
namespace ITS.Domain.ViewModels
{
    public class SupplierTreatmentCategoryPricingViewModel
    {
        public int TreatmentCategoryID { get; set; }
        public string TreatmentCategoryName { get; set; }
        public int SupplierID { get; set; }
        public bool Enabled { get; set; }
        public int SupplierTreatmentID { get; set; }
        public IList<TreatmentPricing> Pricing { get; set; }

    }
}
