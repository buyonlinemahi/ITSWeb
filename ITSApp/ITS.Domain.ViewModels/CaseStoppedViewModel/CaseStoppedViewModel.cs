using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ITS.Domain.Models.CaseModel;

namespace ITS.Domain.ViewModels.CaseStoppedViewModel
{
    public class CaseStoppedViewModel
    {
        public int CaseID { get; set; }
        public string AuthorisationDetail { get; set; }
        public IEnumerable<CaseStopReason> CaseStopReasons { get; set; }
    }
}
