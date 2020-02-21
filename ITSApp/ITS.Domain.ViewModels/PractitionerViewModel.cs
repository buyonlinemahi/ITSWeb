using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ITS.Domain.Models.PractitionerModel;

namespace ITS.Domain.ViewModels
{
    public class PractitionerViewModel
    {
        public IEnumerable<Practitioner> Practitioners { get; set; }
        public Practitioner Practitioner { get; set; }
        public IEnumerable<Supplier> Suppliers { get; set; }
        public IEnumerable<AreasofExpertise> AreasofExpertise { get; set; }
        public IEnumerable<Registration> Registrations { get; set; }
        public IEnumerable<RegistrationType> RegistrationTypes { get; set; }
        public IEnumerable<TreatmentCategory> TreatmentCategories { get; set; }
        public IEnumerable<SupplierPractitioner> SupplierPractitioners { get; set; }
    }
}
