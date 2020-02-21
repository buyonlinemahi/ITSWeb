
using System.Collections.Generic;
namespace ITS.Domain.Models
{
    public class Supplier
    {
        public int SupplierID { get; set; }
        public string SupplierName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostCode { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Website { get; set; }
        public string Ranking { get; set; }
        public string RankingText { get; set; }
        public string Notes { get; set; }
        public bool IsWheelChairAccessibility { get; set; }
        public bool IsWeekends { get; set; }
        public bool IsEvenings { get; set; }
        public bool IsParking { get; set; }
        public bool IsHomeVisit { get; set; }
        public bool IsTriage { get; set; }
        public string Email { get; set; }
        public int StatusID { get; set; }
        public IEnumerable<SupplierTreatment> TreatmentCategories { get; set; }

    }
}
