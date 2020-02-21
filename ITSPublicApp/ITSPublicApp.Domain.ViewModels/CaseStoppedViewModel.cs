using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ITSPublicApp.Domain.Models;

namespace ITSPublicApp.Domain.ViewModels
{
    public class CaseStoppedViewModel
    {
        public Case caseObj { get; set; }
        public string DownloadPath { get; set; }
    }
}
