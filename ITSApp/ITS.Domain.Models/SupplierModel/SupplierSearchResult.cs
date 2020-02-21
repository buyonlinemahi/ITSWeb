using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITS.Domain.Models.SupplierModel
{
    public class SupplierSearchResult
    {
        public string SupplierID { get; set; }
        public string SupplierName { get; set; }
        public string TreatmentCategoryName { get; set; }
        public string PostCode { get; set; }
    }
}
