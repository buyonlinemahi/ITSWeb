using System;
using ITSPublicApp.Infrastructure.Global;

namespace ITSPublicApp.Domain.Models
{

    public class SupplierCasePatient
    {
        public int CaseID { get; set; }
        public string EncryptedCaseID { get; set; }
        public string ReferralDownloadPath
        {
            get
            {
                return string.Format("/{0}/{1}/?caseID={2}",
                                     GlobalConst.Controllers.File,
                                     GlobalConst.Actions.FileController.ViewReferral,
                                     CaseID);
            }
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime InitialAssessmentDate  { get; set; }
        public string strInitialAssessmentDate { get; set; }
        public string Fullname { get { return FirstName + " " + LastName; } }
        public string CaseNumber { get; set; }
        public int WorkflowID { get; set; }
        public bool IsCustom { get; set; }
        public string ReferralDownloadPathCustom { get; set; }
        public string Url { get; set; }
        public bool IsFileExist
        {
            get;
            set;
        }
        public string Status
        {
            get
            {
                switch (WorkflowID)
                {
                    case (int)GlobalConst.WorkFlows.ReferredtoSupplier:
                        return "Referrals Received";
                    case (int)GlobalConst.WorkFlows.InitialAssessmentBooked:
                        return "Initial Assessment Booked";
                    case (int)GlobalConst.WorkFlows.InitialAssessmentAttended:
                        return "Initial Assessment Attended";
                    case (int)GlobalConst.WorkFlows.ReviewAssessmentReportSubmittedtoInnovate:
                        return "Review Assessment Attended";
                    case (int)GlobalConst.WorkFlows.ReviewAssessmentReportNotAccepted:
                        return "Review Assessment Report Not Accepted";
                    case (int)GlobalConst.WorkFlows.PatientInTreatment:
                        return "Patient in Treatment";
                    case (int)GlobalConst.WorkFlows.PatientInTreatmentCustom:
                        return "Patient in Treatment";
                    case (int)GlobalConst.WorkFlows.AuthorisationSenttoSupplierOrPatientinTreatment:
                        return "Authorisation Sent to Supplier / Patient in Treatment";
                    case (int)GlobalConst.WorkFlows.AuthorisationSenttoSupplierOrPatientinTreatmentCustom:
                        return "Authorisation Sent to Supplier / Patient in Treatment";
                    case (int)GlobalConst.WorkFlows.CaseStopped:
                        return "Case Stopped";
                    case (int)GlobalConst.WorkFlows.ReferreredtoTriageSupplier:
                        return "Referrals Triage Assessment Received";
                    case (int)GlobalConst.WorkFlows.ReportNotAccepted:
                        return "Report Not Accepted";
                    case (int)GlobalConst.WorkFlows.ReportNotAcceptedCustom:
                        return "Report Not Accepted";
                    case (int)GlobalConst.WorkFlows.FinalAssessmentReportSubmittedtoInnovate:
                        return "Final Assessment Report Submitted to Innovate";
                    case (int)GlobalConst.WorkFlows.FinalAssessmentReportNotAccepted:
                        return "Final Assessment Report Not Accepted";
                    case (int)GlobalConst.WorkFlows.CaseStoppedSentToSupplier:
                        return "Case Stopped Sent to Supplier";
                    case (int)GlobalConst.WorkFlows.InitialAssessmentCustom:
                        return "Initial Assessment Custom";
                }
                return null;
            }
        }
        public string NextStatus
        {
            get
            {
                switch (WorkflowID)
                {
                    case (int)GlobalConst.WorkFlows.InitialAssessmentBooked:
                        return "Initial Assessment";
                    case (int)GlobalConst.WorkFlows.InitialAssessmentAttended:
                        return "Initial Assessment";
                    case (int)GlobalConst.WorkFlows.ReviewAssessmentAttended:
                        return "RA/FA Assessment";
                    case (int)GlobalConst.WorkFlows.ReviewAssessmentReportSubmittedtoInnovate:
                        return "RA/FA Assessment";
                    case (int)GlobalConst.WorkFlows.AuthorisationSenttoSupplierOrPatientinTreatment:
                        return "RA/FA Assessment";
                    case (int)GlobalConst.WorkFlows.AuthorisationSenttoSupplierOrPatientinTreatmentCustom:
                        return "RA/FA Assessment";
                    case (int)GlobalConst.WorkFlows.ReviewAssessmentReportNotAccepted:
                        return "RA/FA Assessment";
                    case (int)GlobalConst.WorkFlows.ReferreredtoTriageSupplier:
                        return "Triage Assessment";
                    case (int)GlobalConst.WorkFlows.ReferredtoSupplier:
                        return "Book IA";
                    case (int)GlobalConst.WorkFlows.ReportNotAccepted:
                        return "Initial Assessment";
                    case (int)GlobalConst.WorkFlows.ReportNotAcceptedCustom:
                        return "Initial Assessment";
                    case (int)GlobalConst.WorkFlows.PatientInTreatment:
                        return "Authorisation sent to Supplier";
                    case (int)GlobalConst.WorkFlows.PatientInTreatmentCustom:
                        return "Authorisation sent to Supplier";
                    case (int)GlobalConst.WorkFlows.FinalAssessmentReportSubmittedtoInnovate:
                        return "RA/FA Assessment";
                    case (int)GlobalConst.WorkFlows.FinalAssessmentReportNotAccepted:
                        return "RA/FA Assessment";
                    case (int)GlobalConst.WorkFlows.CaseStoppedSentToSupplier:
                        return "Case Completed";
                    case (int)GlobalConst.WorkFlows.InitialAssessmentCustom:
                        return "Book IA";
                }
                return null;
            }
        }

        
    }
}
