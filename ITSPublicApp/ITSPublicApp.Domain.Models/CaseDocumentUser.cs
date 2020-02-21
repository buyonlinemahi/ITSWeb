﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITSPublicApp.Domain.Models
{
    public class CaseDocumentUser
    {
        public int CaseID { get; set; }
        public string EncryptedCaseID { get; set; }
        public int DocumentTypeID { get; set; }
        public DateTime UploadDate { get; set; }
        public string DocumentName { get; set; }
        public string UploadPath { get; set; }
        public int? UserID { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ReferrerDocumentTypeDesc { get; set; }
        public bool? SupplierCheck { get; set; }
        public bool? ReferrerCheck { get; set; }
        
       
    }
}
