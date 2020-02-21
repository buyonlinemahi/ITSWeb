/*
 * Latest Version 1.20
 * 
 * Modified By  : Robin Singh
 * Date         : 14-Dec-2012
 * Purpose      : Update Enable field NUll 
 * Version      : 1.1
 * 
 * 
 */
namespace ITS.Domain.Models
{
    public class ReferrerProjectTreatment
    {
        public int ReferrerProjectTreatmentID { get; set; }
        public int ReferrerProjectID { get; set; }
        public int TreatmentCategoryID { get; set; }
        public int? AccountReceivableChasingPoint { get; set; }
        public int? AccountReceivableCollection { get; set; }
        public bool Enabled { get; set; }
        public string TreatmentCategoryName { get; set; }
        public int ReferrerId { get; set; }
    }
}
