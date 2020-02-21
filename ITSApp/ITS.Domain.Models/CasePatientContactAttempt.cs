using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITS.Domain.Models
{
   public class CasePatientContactAttempt
    {
        public int CasePatientContactAttemptID { get; set; }
        public int PatientID { get; set; }
        public int CaseID { get; set; }
        public DateTime? ContactAttemptDate { get; set; }
        public DateTime? PatientContactDate { get; set; }
    }
}
