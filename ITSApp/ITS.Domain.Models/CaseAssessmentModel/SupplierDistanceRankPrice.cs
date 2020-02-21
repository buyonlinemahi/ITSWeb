using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITS.Domain.Models.CaseAssessmentModel
{
    public class SupplierDistanceRankPrice
    {
        public int SupplierID { get; set; }
        public string PostCode { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Ranking { get; set; }
        public string RankingText { get; set; }
        public IEnumerable<PriceAverage> PriceAverages { get; set; }
        public string SupplierName { get; set; }
        public double Distance { get; set; }
    }
}
