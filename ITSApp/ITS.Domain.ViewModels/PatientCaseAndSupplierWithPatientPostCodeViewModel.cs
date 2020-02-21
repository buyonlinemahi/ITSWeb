using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ITS.Domain.Models;


namespace ITS.Domain.ViewModels
{
    public class PatientCaseAndSupplierWithPatientPostCodeViewModel
    {
        public int CaseID { get; set; }
        public CasePatientTreatment CasePatientTreatment { get; set; }
        public IEnumerable<SupplierDistanceRankPrice> SupplierDistanceRankPrice { get; set; }
        public IEnumerable<SupplierSupplierTreatmentsAndSupplierTreatmenPricing> SupplierSupplierTreatmentsAndSupplierTreatmenPricing { get; set; }
        
    }
}
