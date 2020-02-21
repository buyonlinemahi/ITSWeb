using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITS.Domain.Models
{
    public class CaseAppointmentDate
    {
        public int CaseID { get; set; }
        public DateTime AppointmentDateTime { get; set; }
        public DateTime? FirstAppointmentOfferedDate { get; set; }
        public int WorkflowID { get; set; }
        public int CaseIDPK { get; set; }
        public string strAppointmentTime { get; set; }
        public string strAppointmentDate { get; set; }
        public DateTime CaseBookIADate { get; set; }
        public bool IsCaseBookIADateUsed { get; set; }
    
    }
}