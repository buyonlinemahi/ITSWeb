using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ITS.Domain.Models.SupplierModel;
namespace ITS.Domain.ViewModels
{
    public class SupplierSearchResultViewModel
    {
        public SupplierSearch SupplierSearch { get; set; }
        public IEnumerable<SupplierSearchResult> Suppliers { get; set; }
        public int TotalCount { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
    }
}
