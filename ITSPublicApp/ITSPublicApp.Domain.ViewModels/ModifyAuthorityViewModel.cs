using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ITSPublicApp.Domain.Models;

namespace ITSPublicApp.Domain.ViewModels
{
    public class ModifyAuthorityViewModel
    {
        public int CaseID { get; set; }
        public string CaseNumber { get; set; }
        public Case cases  { get; set; }
        public string DownloadPath { get; set; }
        public string EncryptedCaseID { get; set; }
    }
}
