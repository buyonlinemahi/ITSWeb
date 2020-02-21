using System;
using ITSPublicApp.Infrastructure.Global;

namespace ITSPublicApp.Domain.Models
{
    public class ReferrerSupplierCases
    {
        public int PatientID { get; set; }
        public DateTime CaseSubmittedDate { get; set; }
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
        public string PostCode { get; set; }
        public int CaseID { get; set; }
        public string EncryptCaseID { get; set; }
        public string CaseNumber { get; set; }
        public int WorkflowID { get; set; }
        public string CaseReferrerReferenceNumber { get; set; }
        public string HomePhone { get; set; }
        public string WorkPhone { get; set; }
        public string MobilePhone { get; set; }
        public bool IsFileExist { get; set; }
        public string UserFullName { get; set; }
        public string Status
        {
            get
            {
                switch (WorkflowID)
                {
                    case (int)GlobalConst.WorkFlows.ReferredtoSupplier:
                        return "Referrals Received";
                    case (int)GlobalConst.WorkFlows.ReferralSubmitted:
                        return "Referral Submitted";
                    case (int)GlobalConst.WorkFlows.InitialAssessmentBooked:
                        return "Initial Assessment Booked";
                    case (int)GlobalConst.WorkFlows.InitialAssessmentAttended:
                        return "Initial Assessment Attended";
                    case (int)GlobalConst.WorkFlows.ReviewAssessmentReportSubmittedtoInnovate:
                        return "Review Assessment Attended";
                    case (int)GlobalConst.WorkFlows.InitialAssessmentSubmittedtoInnovate:
                        return "Initial Assessment Submitted to Innovate";
                    case (int)GlobalConst.WorkFlows.ReviewAssessmentReportNotAccepted:
                        return "Review Assessment Report Not Accepted";
                    case (int)GlobalConst.WorkFlows.PatientInTreatment:
                        return "Patient in Treatment";
                    case (int)GlobalConst.WorkFlows.AuthorisationSenttoSupplierOrPatientinTreatment:
                        return "Authorisation Sent to Supplier / Patient in Treatment";
                    case (int)GlobalConst.WorkFlows.CaseStopped:
                        return "Case Stopped";
                    case (int)GlobalConst.WorkFlows.ReferreredtoTriageSupplier:
                        return "Referrals Triage Assessment Received";
                    case (int)GlobalConst.WorkFlows.ReportNotAccepted:
                        return "Report Not Accepted";
                    case (int)GlobalConst.WorkFlows.FinalAssessmentReportSubmittedtoInnovate:
                        return "Final Assessment Report Submitted to Innovate";
                    case (int)GlobalConst.WorkFlows.FinalAssessmentReportNotAccepted:
                        return "Final Assessment Report Not Accepted";
                    case (int)GlobalConst.WorkFlows.CaseStoppedSentToSupplier:
                        return "Case Stopped Sent to Supplier";
                    case (int)GlobalConst.WorkFlows.FinalAssessmentReportSubmittedtoReferrer:
                        return "Final Assessment Report Submitted to Referrer";
                    case (int)GlobalConst.WorkFlows.InitialAssessmentReportSubmittedtoReferrerOrAwaitingAuthorisation:
                        return "Initial Assessment Report Submitted to Referrer / Awaiting Authorisation";
                    case (int)GlobalConst.WorkFlows.ReviewAssessmentReportSubmittedtoReferrer:
                        return "Review Assessment Report Submitted to Referrer";
                    case (int)GlobalConst.WorkFlows.ReferralAccepted:
                        return "Referral Accepted";
                    case (int)GlobalConst.WorkFlows.PatientContacted:
                        return "Patient Contacted";
                    case (int)GlobalConst.WorkFlows.TriageAssessmentSubmittedtoInnovate:
                        return "Triage Assessment Submitted to Innovate";
                    case (int)GlobalConst.WorkFlows.AuthorisationSenttoInnovate:
                        return "Authorisation Sent to Innovate";
                    case (int)GlobalConst.WorkFlows.ReviewAssessmentAttended:
                        return "Review Assessment Attended";
                    case (int)GlobalConst.WorkFlows.CaseCompleted:
                        return "Case Completed";
                    case (int)GlobalConst.WorkFlows.InvoiceSent:
                        return "Invoice Sent";

                }
                return null;
            }
        }
        public DateTime? InitialAssessmentDate { get; set; }
        public DateTime? FinalAssessmentDate { get; set; }
        public int? InitialCaseAssessmentDetailID { get; set; }
        public int? FinalCaseAssessmentDetailID { get; set; }
        public string EncryptInitialCaseAssessmentDetailID { get; set; }
        public string EncryptFinalCaseAssessmentDetailID { get; set; }
        public int? InitialAssessmentServiceID { get; set; }
        public int? FinalAssessmentServiceID { get; set; }
    }

}
