using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITS.Domain.Models.CaseSearch
{
    public class CaseNote
    {

        public int CaseNoteID { get; set; }
        public string Note { get; set; }

        public int CaseID { get; set; }

        public DateTime DateAdded { get; set; }

        public int UserID { get; set; }
        public int WorkflowID { get; set; }
    }
}
