using ITS.Domain.Models;
using ITS.Domain.Models.SolicitorModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace ITS.Domain.ViewModels.ReferralViewModel
{
    public class AddReferralViewModel
    {
        public Case Case { get; set; }
        public Patient Patient { get; set; }
        public Solicitor Solicitor { get; set; }
        public InjuredPartyRepresentative injuredPartyRepresentative { get; set; }
        public Employment Employment { get; set; }
        public EmployeeDetail EmployeeDetail { get; set; }
        public CaseContact CaseContact { get; set; }
        public IEnumerable<EmploymentType> EmploymentTypes { get; set; }
        public Policie Policie { get; set; }
        public IEnumerable<PolicyType> PolicyTypes { get; set; }
        public IEnumerable<TypeCover> TypeCovers { get; set; }
        public IEnumerable<PolicyCriteria> PolicyCriterias { get; set; }
        public IEnumerable<FitForWork> FitForWorks { get; set; }
        public IEnumerable<Admitted> Admitteds { get; set; }
        public IEnumerable<ReferrerProject> ReferrerProjects { get; set; }
        public IEnumerable<ReferrerAssignedUser> ReferrerAssignedUsers { get; set; }
        //public int ReferrerProjectID { get; set; }
        public ReferrerProject ReferrerProject { get; set; }
        public IEnumerable<ReferrerLocation> ReferrerLocations { get; set; }
        public int ReferrerLocationID { get; set; }
        public IEnumerable<CaseContact> CaseContacts { get; set; }        
        public HttpPostedFileBase MedicalReportFileUpload { get; set; }
        public HttpPostedFileBase PatientConsentFileUpload { get; set; }
        public IEnumerable<WorkType> WorkTypes { get; set; }
        public IEnumerable<RoleType> RoleTypes { get; set; }
        //public IEnumerable<ReferrerDocumentType> ReferrerDocumentTypes { get; set; }
        //public IEnumerable<ReferrerDocuments> ReferrerDocuments { get; set; }
        public ITSUser ITSUser { get; set; }
        public Referrer Referrer { get; set; }
        public IEnumerable<Gender> GenderTypes { get; set; }
        public IEnumerable<Reinsurer> Reinsurers { get; set; }
        public DrugTest DrugTest { get; set; }
        public IEnumerable<AdditionalTest> AdditionalTests { get; set; }
        public IEnumerable<ReasonForReferral> ReasonForReferral { get; set; }
        public IEnumerable<NetworkRailStandardApplicable> NetworkRailStandardApplicable { get; set; }
        public JobDemand JobDemand { get; set; }
        public PolicyOpenDetail policyOpenDetail { get; set; }
        public IEnumerable<CaseReferrerAssignedUser> CaseReferrerAssignedUsers { get; set; }
        public IEnumerable<int> OldCaseReferrerAssignedUsers { get; set; }
    }
}
