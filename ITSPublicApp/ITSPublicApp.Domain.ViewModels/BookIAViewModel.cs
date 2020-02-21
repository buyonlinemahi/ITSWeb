using System;
using System.Collections.Generic;
using ITSPublicApp.Domain.Models;

namespace ITSPublicApp.Domain.ViewModels
{
    public class BookIAViewModel
    {
        public CasePatient CasePatient { get; set; }
        public CaseAppointmentDate CaseAppointmentDate { get; set; }
        public IEnumerable<CasePatientContactAttempt> CasePatientContactAttempts { get; set; }
        public IEnumerable<ITSUser> SuppliarAssignedUsers { get; set; }
        public int? SuppliarAssignedUserID { get; set; }
        public DateTime PatientContactDate { get; set; }
        public int CaseID { get; set; }
        public string ReferralDownloadPath { get; set; }
        public string InnovateNote { get; set; }
        public bool IsFileExist
        {
            get;
            set;
        }
        public bool IsFirstAppointmentOffered
        {
            get;
            set;
        }
    }
}