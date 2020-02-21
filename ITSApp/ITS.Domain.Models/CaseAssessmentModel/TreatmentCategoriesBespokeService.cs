using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITS.Domain.Models.CaseAssessmentModel
{
    public class TreatmentCategoriesBespokeService
    {
        public string TreatmentCategoryName { get; set; }
        public string BespokeServiceName { get; set; }
        public int TreatmentCategoryBespokeServiceID { get; set; }
        public int TreatmentCategoryID { get; set; }
        public int BespokeServiceID { get; set; }
    }
}
