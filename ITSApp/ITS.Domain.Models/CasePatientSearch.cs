using System;
namespace ITS.Domain.Models
{
    public class CasePatientSearch
    {
        public string PatientFirstName { get; set; }
        public string PatientLastName { get; set; }
        public int CaseID { get; set; }
        public string CaseNumber { get; set; }
    }
}
