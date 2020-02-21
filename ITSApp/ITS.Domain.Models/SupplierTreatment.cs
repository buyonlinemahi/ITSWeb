
namespace ITS.Domain.Models
{
    public class SupplierTreatment
    {
        public int SupplierTreatmentID { get; set; }
        public int TreatmentCategoryID { get; set; }
        public string TreatmentCategoryName { get; set; }
        public int SupplierID { get; set; }
        public bool Enabled { get; set; }
    }
}
