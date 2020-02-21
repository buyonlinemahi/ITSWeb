using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITS.Infrastructure.Global
{
    public class GlobalConst
    {
        public enum ReferrerSearchCriteria { ReferrerName = 1 }
        public enum SupplierSearchCriteria { SupplierName = 1, PostCode = 2, TreatmentCategory = 3 }
        public enum PractitionerSearchCriteria { PractitionerName = 1, TreatmentCategory = 2 }
        public enum UserSearchCriteria { Name = 1, UserName = 2, UserTypeName = 3, ReferrerName = 4, SupplierName = 5 }

        public enum CaseSearchCriteria { PatientName = 1, CaseNumber = 2, PostCode = 3, ReferrerRefNumber = 4, TreatmentCategory = 5, TreatmentType = 6, ReferrerName = 7, AllCases = 8, Active = 9, Inactive = 10 }

        public enum UserType { Internal = 1, Referrer = 2, Supplier = 3 }
        public enum TreatmentCategoryValues { PhysicalTherapy = 1, Psychological = 2, Diagnostic = 3, SpecialServices = 4, All = 5 }

        public enum DefaultPageSizeValues { Skip = 0, Take = 5 }

        public struct ReferralReceivedTypes
        {
            public const string Referrals = "Referrals";
            public const string TriageAssessmentQA = "Triage Assessment QA";
            public const string InitialAssessmentQA = "Initial Assessment QA";
            public const string Authorisation = "Authorisation";
            public const string ReviewAssessmentQA = "Review Assessment QA";
            public const string FinalAssessmentQA = "Final Assessment QA";
            public const string CaseStopped = "Case Stopped";
            public const string CaseCompleted = "Case Completed";
            public const string ReferralTasksDueToday = "Referral Tasks Due Today";
            public const string PatientinTreatmentCustom = "Patient in Treatment Custom";
        }
       
       
        public struct DocumentType
        {
            public const string Registration = "Registration";
            public const string Insurance = "Insurance";
            public const string SupplierAudit = "Supplier Audit";
            public const string SupplierClinicalAudit = "Supplier Clinical Audit";
            public const string Referral = "Referral";
            public const string Triage = "Triage";
            public const string Misc = "Misc";
            public const string InitialAssessment = "Initial Assessment";
            public const string InitialAssessmentQA = "Initial Assessment QA";
            public const string ReturnAssessment = "Return Assessment";
            public const string ReturnAssessmentQA = "Return Assessment QA";
            public const string FinalAssessment = "Final Assessment";
            public const string FinalAssessmentQA = "Final Assessment QA";
            public const string ReviewAssessment = "Review Assessment";
        }
        public struct ReportFormat
        {
            public const string Word = "WORD";
            public const string PDF = "PDF";
        }

        public struct ReportFormatExt
        {
            public const string doc = "doc";
            public const string PDF = "PDF";
        }

        public enum WorkFlows
        {
            ReferralSubmitted = 10,
            ReferralAccepted = 20,
            PatientContacted = 30,
            ReferredtoSupplier = 40,
            ReferreredtoTriageSupplier = 42,
            InitialAssessmentBooked = 50,
            InitialAssessmentAttended = 60, //not used anymore  
            TriageAssessmentSubmittedtoInnovate = 65,
            InitialAssessmentSubmittedtoInnovate = 70,
            ReportNotAccepted = 80,
            ReportNotAcceptedCustom = 82,
            InitialAssessmentReportSubmittedtoReferrerOrAwaitingAuthorisation = 90,
            AuthorisationSenttoInnovate = 100,
            PatientInTreatment = 105,
            PatientinTreatmentCustom = 107,
            AuthorisationSenttoSupplierOrPatientinTreatment = 110,
            AuthorisationSenttoSupplierOrPatientinTreatmentCustom = 115,
            ReviewAssessmentAttended = 120,
            ReviewAssessmentReportSubmittedtoInnovate = 130,
            ReviewAssessmentReportSubmittedtoInnovateCustom = 132,
            ReviewAssessmentReportNotAccepted = 140,
            ReviewAssessmentReportSubmittedtoReferrer = 150,
            ReviewAssessmentReportSubmittedtoReferrerCustom = 152,
            FinalAssessmentReportSubmittedtoInnovate = 160,
            FinalAssessmentReportNotAccepted = 170,
            FinalAssessmentReportSubmittedtoReferrer = 180,
            FinalAssessmentReportSubmittedtoReferrerCustom = 182,
            CaseStopped = 200,
            CaseCompleted = 210,
            InvoiceSent = 220,
            CaseStoppedSentToSupplier = 205,
            InitialAssessmentBookedCustom = 52,
            InitialAssessmentCustom = 55,
            InitialAssessmentSubmittedtoInnovateCustom = 72,
            InitialAssessmentReportSubmittedtoReferrerOrAwaitingAuthorisationCustom = 92,
            FinalAssessmentReportNotAcceptedCustom=172,
            ReviewAssessmentReportNotAcceptedCustom=142

        }
        public struct WorkflowEventDescription
        {
            public const string ReferralSubmitted = "Referral Submitted";
            public const string ReferralAccepted = "Referral Accepted";
            public const string PatientContacted = "Patient Contacted";
            public const string ReferredtoSupplier = "Referred to Supplier";
            public const string ReferreredtoTriageSupplier = "Referrer to Triage Supplier";
            public const string InitialAssessmentBooked = "Initial Assessment Booked";
            public const string InitialAssessmentAttended = "Initial Assessment Attended";
            public const string TriageAssessmentSubmittedtoInnovate = "Triage Assessment Submitted to Innovate";
            public const string InitialAssessmentSubmittedtoInnovate = "Initial Assessment Submitted to Innovate";
            public const string ReportNotAccepted = "Report Not Accepted";
            public const string InitialAssessmentReportSubmittedtoReferrerOrAwaitingAuthorisation = "Initial Assessment Report Submitted to Referrer Or Awaiting Authorisation";
            public const string AuthorisationSenttoInnovate = "Authorisation Sent to Innovate";
            public const string PatientInTreatment = "Patient in Treatment";
            public const string AuthorisationSenttoSupplierOrPatientinTreatment = "Authorisation Sent to Supplier Or Patient in Treatment";
            public const string ReviewAssessmentAttended = "Review Assessment Attended";
            public const string FinalAssessmentAttended = "Final Assessment Attended";
            public const string ReviewAssessmentReportSubmittedtoInnovate = "Review Assessment Report Submitted to Innovate";
            public const string ReviewAssessmentReportSubmittedtoInnovateCustom = "Review Assessment Report Submitted to Innovate Custom";
            public const string ReviewAssessmentReportNotAccepted = "Review Assessment Report Not Accepted";
            public const string FinalAssessmentReportSubmittedtoInnovate = "Final Assessment Report Submitted to Innovate";
            public const string FinalAssessmentReportNotAccepted = "Final Assessment Report Not Accepted";
            public const string ReviewAssessmentReportSubmittedtoReferrer = "Review Assessment Report Submitted to Referrer";
            public const string FinalAssessmentReportSubmittedtoReferrer = "Final Assessment Report Submitted to Referrer";
            public const string CaseStopped = "Case Stopped";
            public const string CaseCompleted = "Case Completed";
            public const string InvoiceSent = "Invoice Sent";
            public const string CaseStoppedSenttoSupplier = "Case Stopped Sent to Supplier";
            public const string InvoiceEmailSend = "InvoiceEmailSend";
            public const string InitialAssessmentCustom = "Initial Assessment Custom";
            public const string InitialAssessmentBookedCustom = "Initial Assessment Booked Custom";
            public const string InitialAssessmentSubmittedtoInnovateCustom = "Initial Assessment Submitted to Innovate Custom";
            public const string InitialAssessmentReportSubmittedtoReferrerOrAwaitingAuthorisationCustom = "Initial Assessment Report Submitted to Referrer Or Awaiting Authorisation Custom";
        }

        public struct Controllers
        {
            //not area specific controllers
            public const string Home = "Home";
            public const string InternalTasks = "InternalTasks";
            public const string InternalTaskShared = "InternalTaskShared";
            public const string User = "User";
            public const string Referrer = "Referrer";
            public const string Supplier = "Supplier";
            public const string SupplierShared = "SupplierShared";
            public const string ReferrerShared = "ReferrerShared";
            public const string UserShared = "UserShared";
            public const string ReferrerProject = "ReferrerProject";
            public const string Practitioner = "Practitioner";
            public const string PractitionerShared = "PractitionerShared";
            public const string File = "File";
            public const string ReferrerProjectTreatmentShared = "ReferrerProjectTreatmentShared";

            public const string Referral = "Referral";
            public const string InitialAssessment = "InitialAssessment";
            public const string ReviewAssessment = "ReviewAssessment";
            public const string FinalAssessment = "FinalAssessments";
            public const string TriageAssessment = "TriageAssessment";
            public const string Authorisation = "Authorisations";
            public const string CaseStop = "CaseStop";
            public const string CaseComplete = "CaseComplete";

            public const string CaseSearch = "CaseSearch";
            public const string CaseSearchShared = "CaseSearchShared";
            public const string Invoice="Invoice";

            //area specific controllers
            public struct Area
            {
                public struct Internal
                {
                    public const string InternalTask = "InternalTask";
                    public const string CaseSearch = "CasesSearch";
                    public const string ReferrerSetup = "ReferrerSetup";
                    public const string Management = "Management";
                    public const string SupplierSetup = "SupplierSetup";
                    public const string SupplierDocumentRegistration = "SupplierDocumentRegistration";
                    public const string SupplierInsured = "SupplierInsured";
                    public const string SupplierClinicalAudit = "SupplierClinicalAudit";
                    public const string SupplierSiteAudit = "SupplierSiteAudit";
                    public const string PractitionerSetup = "PractitionerSetup";
                    public const string AcceptAndRefer = "AcceptAndRefer";
                    public const string AcceptAndReferTriageAssessment = "AcceptAndReferTriageAssessment";
                }

                public struct Referrer
                {
                    public const string RefHome = "RefHome";
                }

                public struct Supplier
                {
                    public const string Home = "Home";
                }
            }


        }

        public struct Actions
        {
            //not area specific controller
            public struct HomeController
            {
                public const string Index = "Index";
                public const string ResetPassword = "ResetPassword";
                public const string LogOut = "LogOut";
            }

            public struct CaseSearchController
            {
                public const string Index = "Index";
                public const string CaseSearch = "CaseSearch";
                public const string BookInitialAppointment = "BookInitialAppointment";
                public const string GetGeneratedAssessmentReport = "GetGeneratedAssessmentReport";
                public const string UpdateCaseSearchTreatmentPricing = "UpdateCaseSearchTreatmentPricing";
                public const string Detail = "Detail";
            }

            public struct InternalTasksController
            {
                public const string Index = "Index";
                public const string GetCaseCountByTreatmentCategoryID = "GetCaseCountByTreatmentCategoryID";
            }


            public struct ReferralController
            {
                public const string Index = "Index";
                public const string ReferralReceived = "ReferralReceived";
                public const string AcceptAndReferTriageAssessment = "AcceptAndReferTriageAssessment";
                public const string AcceptAndRefer = "AcceptAndRefer";
                public const string CaseSubmitToSupplier = "CaseSubmitToSupplier";

            }
            public struct InvoiceController
            {
                public const string Index = "Index";
                public const string InvoiceDetail = "InvoiceDetail";
                public const string AddPaymentAmount = "AddPaymentAmount";
                public const string AddCollectionAction = "AddCollectionAction";    
                

            }
            public struct TriageAssessmentController
            {
                public const string Index = "Index";
                public const string TriageAssessment = "TriageAssessment";
                public const string TriageAssessmentQA = "TriageAssessmentQA";
                public const string UpdateTriageAssessmentQA = "UpdateTriageAssessmentQA";


            }
            public struct InitialAssessmentController
            {
                public const string Index = "Index";
                public const string InitialAssessment = "InitialAssessment";
                public const string InitialAssessmentQA = "InitialAssessmentQA";
                public const string InitialAssessmentQACustom = "InitialAssessmentQACustom";
                public const string UpdateInitialAssessmentQA = "UpdateInitialAssessmentQA";
                public const string UpdateInitialAssessmentQACustom = "UpdateInitialAssessmentQACustom";

            }
            public struct ReviewAssessmentController
            {
                public const string Index = "Index";
                public const string ReviewAssessment = "ReviewAssessment";
                public const string ReviewAssessmentQA = "ReviewAssessmentQA";
                public const string ReviewAssessmentCustomQA = "ReviewAssessmentCustomQA";
                public const string UpdateReviewAssessmentQA = "UpdateReviewAssessmentQA";
                public const string UpdateReviewAssessmentCustomQA = "UpdateReviewAssessmentCustomQA";
            }
            public struct FinalAssessmentController
            {
                public const string Index = "Index";
                public const string FinalAssessment = "FinalAssessment";
                public const string FinalAssessmentQA = "FinalAssessmentQA";
                public const string FinalAssessmentQACustom = "FinalAssessmentQACustom";
                public const string UpdateFinalAssessmentQA = "UpdateFinalAssessmentQA";
                public const string UpdateFinalAssessmentQACustom = "UpdateFinalAssessmentQACustom";

            }
            public struct AuthorisationController
            {
                public const string Index = "Index";
                public const string Authorisation = "Authorisation";
                public const string AuthorisationQA = "AuthorisationQA";
                public const string UpdateAuthorisationQA = "UpdateAuthorisationQA";
            }
            public struct CaseStopController
            {
                public const string Index = "Index";
                public const string CaseStoppedScreen = "CaseStoppedScreen";
                public const string GetCaseStopped = "GetCaseStopped";
                public const string SubmitCaseStopped = "SubmitCaseStopped";
            }

            public struct CaseCompleteController
            {
                public const string Index = "Index";
                public const string GetCaseComplete = "GetCaseComplete";
                public const string CaseCompleteSubmit = "CaseCompleteSubmit";

            }

            public struct UserController
            {
                public const string Index = "Index";
                public const string Search = "Search";
                public const string GetUser = "GetUser";
                public const string UserNameAutoComplete = "UserNameAutoComplete";
                public const string Add = "Add";
                public const string Save = "Save";
                public const string Detail = "Detail";
                public const string Update = "Update";

            }

            public struct UserSharedController
            {
                public const string SearchUser = "SearchUser";
            }


            public struct ReferrerController
            {
                public const string GetReferrer = "GetReferrer";
                public const string Index = "Index";
                public const string ReferrerAutoComplete = "ReferrerAutoComplete";
                public const string GetReferrerMainOffice = "GetReferrerMainOffice";
                public const string GetReferrerAllLocation = "GetReferrerLocationsByReferrerID";
                public const string Add = "Add";
                public const string Detail = "Detail";
                public const string Save = "Save";
                public const string Search = "Search";
                public const string AddGroup = "AddGroup";
            }

            public struct SupplierController
            {
                public const string GetAllSupplier = "GetAllSupplier";
                public const string AddSupplier = "AddSupplier";
                public const string SupplierAutoCompleteByName = "SupplierAutoCompleteByName";
                public const string SupplierAutoCompleteByPostalCode = "SupplierAutoCompleteByPostalCode";
                public const string GetSupplierBySupplierId = "GetSupplierBySupplierId";
                public const string UpdateSupplier = "UpdateSupplier";
                public const string Search = "Search";
                public const string Add = "Add";
                public const string Index = "Index";
                public const string Detail = "Detail";
                public const string Save = "Save";
                public const string Update = "Update";
            }

            public struct CaseSearchSharedContoller
            {
                public const string AddCaseUploads = "AddCaseUploads";
                public const string AddCaseNote = "AddCaseNote";
                public const string ResendEmail = "ResendEmail";
                public const string UpdateSolicitor = "UpdateSolicitor";
                public const string UpdateCasePatient = "UpdateCasePatient";
                public const string Index = "Index";
                public const string SaveUploadCheckData = "SaveUploadCheckData";
            }

            public struct SupplierSharedController
            {
                public const string UpdateSupplier = "UpdateSupplier";
                public const string AddSiteAudit = "AddSiteAudit";
                public const string UpdateSupplierComplaint = "UpdateSupplierComplaint";
                public const string AddSupplierClinicalAudit = "AddSupplierClinicalAudit";
                public const string AddSupplierComaplaint = "AddSupplierComplaint";
                public const string AddSupplierInsurance = "AddSupplierInsurance";
                public const string AddSupplierRegistrationDocument = "AddSupplierRegistrationDocument";
                public const string UpdateSupplierRegistrationDocument = "UpdateSupplierRegistrationDocument";
                public const string UpdateSupplierInsurance = "UpdateSupplierInsurance";
                public const string UpdateSupplierClinicalAudit = "UpdateSupplierClinicalAudit";
                public const string GetDocumentFile = "GetDocumentFile";
                public const string UpdateTreatmentCategoryPricing = "UpdateTreatmentCategoryPricing";
                public const string SearchSupplier = "SearchSupplier";

            }
            public struct ReferrerSharedController
            {
                public const string AddReferrerLocation = "AddReferrerLocation";
                public const string UpdateReferrerLocation = "UpdateReferrerLocation";
                public const string ReferrerSearch = "ReferrerSearch";
                public const string AddProjectTreatmentCategory = "AddProjectTreatmentCategory";
                public const string UpdateReferrer = "UpdateReferrer";

            }
            public struct ReferrerProjectController
            {
                public const string AddReferrerProject = "AddReferrerProject";
                public const string UpdateReferrerProject = "UpdateReferrerProject";
            }

            public struct ReferrerProjectTreatmentSharedController
            {
                public const string UpdateDelegateAuthorisation = "UpdateDelegateAuthorisation";
                public const string UpdateReferrerProjectTreatment = "UpdateReferrerProjectTreatment";
                public const string UpdateTreatmentCategoryPricing = "UpdateTreatmentCategoryPricing";
                public const string UpdateInvoiceMethod = "UpdateInvoiceMethod";
                public const string UpdateProjectTreatmentSLA = "UpdateProjectTreatmentSLA";
                public const string UpdateReferrerProjectTreatmentEmail = "UpdateReferrerProjectTreatmentEmail";
                public const string UpdateDocumentSetupProjectTreatment = "UpdateDocumentSetupProjectTreatment";

            }

            public struct PractitionerController
            {
                public const string Index = "Index";
                public const string Save = "Save";
                public const string Detail = "Detail";
                public static string Add = "Add";
                public const string Search = "Search";
            }

            public struct PractitionerSharedController
            {
                public const string UpdatePractitioner = "UpdatePractitioner";
                public const string DeletePractitioner = "DeletePractitioner";
                public const string UpdateRegistration = "UpdateRegistration";
                public const string AddRegistration = "AddRegistration";
                public const string DeleteRegistration = "DeleteRegistration";
                public const string AddSupplierPractitioner = "AddSupplierPractitioner";
                public const string UpdateSupplierPractitioner = "UpdateSupplierPractitioner";
                public const string DeleteSupplierPractitioner = "DeleteSupplierPractitioner";
                public const string PractitionerSearch = "PractitionerSearch";
            }

            public struct FileController
            {
                public const string ViewReferral = "ViewReferral";
                public const string GeneratedPatientReferrerToSupplierLetter = "GeneratedPatientReferrerToSupplierLetter";
                public const string GetGeneratedInitialAssessmentReport = "GetGeneratedInitialAssessmentReport";
                public static string GetGeneratedReviewAssessmentReport = "GetGeneratedReviewAssessmentReport";
                public static string GetGeneratedFinalAssessmentReport = "GetGeneratedFinalAssessmentReport";
            }


            public struct Area
            {
                public struct Internal
                {
                    public struct CaseSearchController
                    {
                        public const string Index = "Index";
                    }
                    public struct InternalTaskController
                    {
                        public const string Index = "Index";
                    }
                    public struct ReferrerSetupController
                    {
                        public const string Index = "Index";
                        public const string GetReferrerProjectsbyReferrerID = "GetReferrerProjectsbyReferrerID";
                        public const string AddReferrerProjectTreatment = "AddReferrerProjectTreatment";
                        public const string GetPricingTypesByTreatmentCategoryID = "GetPricingTypesByTreatmentCategoryID";
                        public const string AddProjectTreatmentSLA = "AddProjectTreatmentSLA";
                        public const string AddReferrerProjectTreatmentPricing = "AddReferrerProjectTreatmentPricing";
                        public const string UpdateReferrerProjects = "UpdateReferrerProjects";
                        public const string GetReferrerProjectTreatmentPricingByReferrerProjectTreatmentID = "GetReferrerProjectTreatmentPricingByReferrerProjectTreatmentID";
                        public const string Default = "Default";
                        public const string Custom = "Custom";
                        public const string InitialAssessment = "Initial Assessment";
                        public const string ReturnAssessment = "Return Assessment";
                        public const string FinalAssessment = "Final Assessment";

                    }

                    public struct SupplierSetupController
                    {
                        public const string Index = "Index";
                        public const string MethodForGetPratitionerBySeacrhHere = "MethodForGetPratitionerBySeacrhHere";

                    }
                    public struct SupplierInsuranceController
                    {
                        public const string GetSupplierInsured = "GetSupplierInsured";
                        public const string RemoveFile = "RemoveFile";
                        public const string AddSupplierInsurance = "AddSupplierInsurance";
                        public const string GetSupplierInsuranceFile = "GetSupplierInsuranceFile";
                        public const string UpdateSupplierInsurance = "UpdateSupplierInsurance";

                    }
                    // Actions of Supplier Clinical Audit
                    public struct SupplierClinicalAuditController
                    {
                        public const string GetUserByUserTypeID = "GetUserByUserTypeID";
                        public const string GetSupplierClinicalAuditBySupplierID = "GetSupplierClinicalAuditBySupplierID";
                        public const string AddSupplierClinicalAudit = "AddSupplierClinicalAudit";
                        public const string UpdateSupplierClinicalAudit = "UpdateSupplierClinicalAudit";
                        public const string DeleteSupplierClinicalAuditBySupplierClinicalAuditID = "DeleteSupplierClinicalAuditBySupplierClinicalAuditID";
                        public const string GetPatientNameByPatientId = "GetPatientNameByPatientId";
                        public const string GetPatientNameByCaseId = "GetPatientNameByCaseId";
                        public const string GetCaseByLikeCaseNumber = "GetCaseByLikeCaseNumber";
                        public const string ViewClinicalFile = "ViewClinicalFile";
                        public const string AddUpdateSupplierClinicalAuditAndFile = "AddUpdateSupplierClinicalAuditAndFile";

                    }

                    public struct ManagementController
                    {
                        public const string Index = "Index";
                    }

                    public struct SupplierDocumentRegistrationController
                    {
                        public const string Index = "Index";
                        public const string GetSupplierDocumentBySupplierIDAndDocumentTypeID = "GetSupplierDocumentBySupplierIDAndDocumentTypeID";
                        public const string AddSupplierDocumentsFiles = "AddSupplierDocumentsFiles";
                        public const string GetRegistrationFile = "GetRegistrationFile";

                    }

                    public struct SupplierSiteAuditController
                    {
                        public const string GetSupplierSiteAuditBySupplierID = "GetSupplierSiteAuditBySupplierID";
                        public const string GetUsersByUserTypeID = "GetUsersByUserTypeID";
                        public const string AddSupplierSiteAuditFile = "AddSupplierSiteAuditFile";
                        public const string AddSupplierSiteAudit = "AddSupplierSiteAudit";
                        public const string DeleteSupplierSiteAuditBySupplierSiteAuditID = "DeleteSupplierSiteAuditBySupplierSiteAuditID";
                        public const string UpdateSupplierSiteAuditBySupplierSiteAuditID = "UpdateSupplierSiteAuditBySupplierSiteAuditID";
                        public const string GetSiteAuditFile = "GetSiteAuditFile";
                    }
                    public struct PractitionerSetupController
                    {
                        public const string Index = "Index";
                        public const string GetPractitionerByPractitionerID = "GetPractitionerByPractitionerId";

                    }

                    public struct AcceptAndReferController
                    {
                        public const string Index = "Index";

                    }
                    public struct AcceptAndReferTriageAssessmentController
                    {
                        public const string Index = "Index";

                    }
                }

                public struct Referrer
                {
                    public struct RefHomeController
                    {
                        public const string Index = "Index";
                    }

                }

                public struct Supplier
                {
                    public struct HomeController
                    {
                        public const string Index = "Index";
                    }
                }
            }


        }

        public struct Views
        {
            public struct Area
            {
                public struct Internal
                {
                }

                public struct Referrer
                {
                }

                public struct Supplier
                {
                }
            }

            public struct Home
            {
                public const string Index = "Index";
            }
        }

        public struct PartialViews
        {

            public struct Area
            {
                public const string InternalArea = "~/Areas/Internal/Views/Shared/";
                public const string Cshtml = ".cshtml";

                public struct Internal
                {
                    public const string ProjectControlPartial = "ProjectControlPartial";

                    public struct MainScreen
                    {

                        public const string CaseTreatmentPricingPartial = "MainScreen/_CaseTreatmentPricingPartial";
                        public const string CaseBespokePricingPartial = "MainScreen/_CaseBespokePricingPartial";
                        public const string ReferralsReceivedPartial = "MainScreen/_ReferralsReceivedPartial";

                    }

                    public struct InternalTaskSetUp
                    {
                        public const string InternalTaskPartial = "MainScreen/_InternalTaskPartial";
                        public const string ReferralsReceivedPartial = "MainScreen/_ReferralsReceivedPartial";
                        public const string AuthorisationPartial = "MainScreen/_AuthorisationPartial";
                        public const string CaseStoppedPartial = "MainScreen/_CaseStoppedPartial";
                        public const string InitialAssessmentPartial = "MainScreen/_InitialAssessmentPartial";
                        public const string FinalAssessmentPartial = "MainScreen/_FinalAssessmentPartial";
                        public const string ReferralTasksDueTodayPartial = "MainScreen/_ReferralTasksDueTodayPartial";
                        public const string ReviewAssessmentPartial = "MainScreen/_ReviewAssessmentPartial";

                    }

                    public struct Management
                    {
                        public const string UserControlPartial = "Management/_UserControlPartial";
                        public const string GroupControlPartial = "Management/_GroupControlPartial";
                        public const string ReportsPartiall = "Management/_ReportsPartial";
                        public const string EmailControlPartial = "Management/_EmailControlPartial";
                        public const string InternalSecurityPartial = "Management/_InternalSecurityPartial";
                        public const string ReferrerSecurityPartial = "Management/_ReferrerSecurityPartial";
                        public const string SupplierSecurityPartial = "Management/_SupplierSecurityPartial";

                    }
                    public struct ReferrerSetup
                    {
                        public const string ReferrerSetupPartial = "ReferrerSetup/_ReferrerSetupPartial";
                        public const string ProjectControlPartial = "ReferrerSetup/_ProjectControlPartial";
                        public const string OfficeLocationsPartial = "ReferrerSetup/_OfficeLocationsPartial";
                        public const string PricingPartial = "ReferrerSetup/_PricingPartial";
                        public const string DelegatedAuthorisationPartial = "ReferrerSetup/_DelegatedAuthorisationPartial";
                        public const string AssessementServicesPartial = "ReferrerSetup/_AssessementServicesPartial";
                        public const string InvoiceMethodPartial = "ReferrerSetup/_InvoiceMethodPartial";
                        public const string DocumentSetupPartial = "ReferrerSetup/_DocumentSetupPartial";
                        public const string EmailSetupPartial = "ReferrerSetup/_EmailSetupPartial";
                    }

                    public struct SupplierSetup
                    {
                        public const string SupplierPartial = "SupplierSetup/_SupplierPartial";
                        public const string PricingPartial = "SupplierSetup/_PricingPartial";
                        public const string ClinicalAuditPartial = "SupplierSetup/_ClinicalAuditPartial";
                        public const string SiteAuditPartial = "SupplierSetup/_SiteAuditPartial";
                        public const string ComplaintPartial = "SupplierSetup/_ComplaintPartial";
                        public const string PractitionerPartial = "SupplierSetup/_PractitionerPartial";
                        public const string InsuredPartial = "SupplierSetup/_InsuredPartial";
                        public const string RegistrationDocumentsPartial = "SupplierSetup/_RegistrationDocumentsPartial";


                    }
                    public struct PractitionerSetup
                    {
                        public const string PractitionerPartial = "PractitionerSetup/_PractitionerPartial";

                    }
                }

                public struct Referrer
                {
                }

                public struct Supplier
                {
                }
            }
        }

        public struct Areas
        {
            public const string Internal = "Internal";
            public const string Referrer = "Referrer";
            public const string Supplier = "Supplier";
        }

        public struct SessionKeys
        {
            public const string USERID = "UserID";
            public const string USER = "User";
        }

        public struct SupplierDocumentType
        {
            public const string Registration = "Registration";
            public const string Insurance = "Insurance";
            public const string SupplierAudit = "Supplier Audit";
            public const string SupplierClinicalAudit = "Supplier Clinical Audit";
            public const string Triage = "Triage";
            public const string Misc = "Misc";
            public const string InitialAssessmentCustomFinal = "InitialAssessmentFinalCustom";
            public const string ReviewAssessmentFinalCustom = "ReviewAssessmentFinalCustom";
            public const string FinalAssessmentFinalCustom = "FinalAssessmentFinalCustom";

        }
       
        public struct SupplierDocumentTypeId
        {
            public const int Registration = 1;
            public const int Insurance = 2;
            public const int SupplierAudit = 3;
            public const int SupplierClinicalAudit = 4;
            public const int Triage = 6;
            public const int Misc = 7;
        }

        public struct CaseDocumentTypeId
        {
            public const int InitialAssessment = 8;
            public const int ReviewAssessment = 9;
            public const int FinalAssessment = 10;
            public const int InitialAssessmentCustom = 11;
            public const int ReviewAssessmentCustom = 12;
            public const int FinalAssessmentCustom = 13;
            public const int InitialAssessmentCustomFinal = 14;
            public const int InitialAssessmentSupplierCustom = 15;
            public const int ReviewAssessmentSupplierCustom = 16;
            public const int FinalAssessmentSupplierCustom = 17;
            public const int FinalAssessmentFinalCustom = 18;
            public const int ReviewAssessmentFinalCustom = 19;
        }
        public const string CaseDocumentStoragePath = "~/Storage/CaseDocuments";

        public struct FileExtensions
        {
            public const string PDF = ".pdf";
            public const string DOC = ".doc";
            public const string DOCX = ".docx";
            public const string JPG = ".jpg";
            public const string JPEG = ".jpeg";
            public const string TIFF= ".tiff";
            public const string TIF = ".tif";
           
           
        }

        public struct PriceTypeId
        {
                       public const int VAT = 7;
        }
        public struct ContentTypes
        {
            public const string PDF = "application/pdf";
            public const string TextHTML = "text/html";
        }
       
        public struct AssessmentService
        {
            public const int InitialAssessment = 1;
            public const int ReviewAssessment = 2;
            public const int FinalAssessment = 3;

        }

        public struct DocumentSetupType
        {
            public const int Default = 1;
            public const int Custom = 2;
        }

        public struct Folders
        {
            public const string CaseDocuments = "/CaseDocuments/";
            public const string Referrers = "/Referrers/";
            public const string Suppliers = "/Suppliers/";
        }

        public struct Message
        {
            public const string GroupAdd = "Group Added";
            public const string GroupExist = "Group Name Already Exicted";
            public const string GroupUpdate = "Group update SuccessFully";
            public const string text_html = "text/html";
            public const string SaveSuccessfully = "Save Successfully";
            public const string PasswordNotcorrect = "Password Not correct";
            public const string PasswordUpdatedSuccessfully = "Password Updated Successfully";
            public const string InvalidUser = "Invalid User";
            public const string EmailNotExists = "Email Not Exists";
            public const string EmailSentToRegisteredEmail = "Email has been sent to the registered address";
            public const string EmailSentSuccessfully = "Email Sent Successfully";
            public const string ReSetUrl = "ReSetUrl";
            public const string EmailLogoPath = "/Storage/Template/logo.png";
            public const string EmailLogo2Path = "/Storage/Template/logo2.png";
            public const string EmailLogo3Path = "/Storage/Template/logo3.png";
        }
       
    }
}
