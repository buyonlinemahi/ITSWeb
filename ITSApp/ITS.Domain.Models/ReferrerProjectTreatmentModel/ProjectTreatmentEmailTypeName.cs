
using System.Collections.Generic;
namespace ITS.Domain.Models.ReferrerProjectTreatmentModel
{
    public class ProjectTreatmentEmailTypeName
    {

        public int ReferrerProjectTreatmentEmailID { get; set; }

        public int ReferrerProjectTreatmentID { get; set; }

        public int EmailTypeID { get; set; }

        public int EmailTypeValueID { get; set; }

        public string EmailTypeName { get; set; }

        public int UserTypeID { get; set; }

        public bool IsTextVisible { get; set; }

        public IEnumerable<EmailTypeValue> EmailTypeValues { get; set; }
    }
}
