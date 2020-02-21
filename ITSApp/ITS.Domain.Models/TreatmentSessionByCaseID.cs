using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITS.Domain.Models
{
    public class TreatmentSessionByCaseID
    {
        public int AllTreatmentSessions { get; set; }
        public string ShowAllAttendedSessionsDateOfService { get; set; }
        public int AllFailedTreatmentSessions { get; set; }
    }
}
