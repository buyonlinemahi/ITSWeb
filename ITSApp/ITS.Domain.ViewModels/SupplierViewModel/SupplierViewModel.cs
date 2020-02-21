using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ITS.Domain.Models;
using ITS.Domain.Models.SupplierModel;
using PractitionerTreatmentRegistration = ITS.Domain.Models.SupplierModel.PractitionerTreatmentRegistration;
using Supplier = ITS.Domain.Models.SupplierModel.Supplier;

namespace ITS.Domain.ViewModels
{
    public class SupplierViewModel
    {
        public Supplier Supplier { get; set; }
        public IEnumerable<Supplier> Suppliers { get; set; }


        public IEnumerable<TreatmentCategory> SupplierTreamentCategories { get; set; }
        public IEnumerable<ClinicalAudit> SupplierClinicalAudits { get; set; }
        public IEnumerable<SiteAudit> SupplierSiteAudits { get; set; }
        public IEnumerable<Insurance> SupplierInsurances { get; set; }
        public IEnumerable<Registration> SupplierRegistrations { get; set; }
        public IEnumerable<PractitionerTreatmentRegistration> SupplierPracitioners { get; set; }
        public IEnumerable<Complaint> SupplierComplaints { get; set; }
        public IEnumerable<ITSUser> Users { get; set; }
        public IEnumerable<TreatmentCategories> TreatmentCategories { get; set; }
        public IEnumerable<SupplierTreatmentCategoryPricingViewModel> SupplierTreatmentCategoryPricing { get; set; }
    }
}
