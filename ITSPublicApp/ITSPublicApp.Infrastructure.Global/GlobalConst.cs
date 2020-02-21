namespace ITSPublicApp.Infrastructure.Global
{
    public class GlobalConst
    {
        public enum SupplierCaseSearchCriteria { PatientName = 1, CaseNumber = 2 }
        public enum ReferrerCaseSearchCriteria { PatientName = 1, ReferrerReferernce = 2 }
        public enum GroupNameCaseSearchCriteria { PatientName = 1 , GroupName = 3 }


        public struct SessionKeys
        {
            public const string USER = "User";
        }

        public enum UserType
        {
            Internal = 1,
            Referrer = 2,
            Supplier = 3
        }

        public struct DocumentTypes
        {
            public const int InitialAssessmentCustomFinal = 14;
            public const int FinalAssessmentFinalCustom = 18;
            public const int ReviewAssessmentFinalCustom = 19;
        }

        public struct SupplierDocumentType
        {
            public const string Registration = "Registration";
            public const string Insurance = "Insurance";
            public const string SupplierAudit = "Supplier Audit";
            public const string SupplierClinicalAudit = "Supplier Clinical Audit";
            public const string Triage = "Triage";
            public const string InitialAssessmentSupplierCustom = "InitialAssessmentSupplierCustom";
            public const string ReviewAssessmentSupplierCustom = "ReviewAssessmentSupplierCustom";
            public const string FinalAssessmentSupplierCustom = "FinalAssessmentSupplierCustom";
            public const string ReturnAssessmentCustom = "Return Assessment Custom";
            public const string FinalAssessmentCustom = "Final Assessment Custom";
            public const string InitialAssessmentFinalCustom = "InitialAssessmentFinalCustom";
            public const string AuthorisationSenttoInnovateCustom = "Authorisation Sent to Innovate Custom";
        }

        public enum SupplierDocumentTypeID
        {
            Registration = 1,
            Insurance = 2,
            SupplierAudit = 3,
            SupplierClinicalAudit4,
            Referral = 5,
            Triage = 6,
        }

        public struct CaseDocumentTypeId
        {
            public const int InitialAssessment = 8;
            public const int ReviewAssessment = 9;
            public const int FinalAssessment = 10;
        }

        public const string CaseDocumentDownloadPath = "/Storage/CaseDocuments/";
        
        public const string CaseDocumentStoragePath = "~/Storage/CaseDocuments";
        
        public struct FileExtensions
        {
            public const string PDF = ".pdf";
        }

        public struct ContentTypes
        {
            public const string PDF = "application/pdf";
            public const string TextHtml = "text/html";
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
            AuthorisationSenttoInnovateCustom = 102,
            PatientInTreatment = 105,
            PatientInTreatmentCustom = 107,
            AuthorisationSenttoSupplierOrPatientinTreatment = 110,
            AuthorisationSenttoSupplierOrPatientinTreatmentCustom = 115,
            ReviewAssessmentAttended = 120,
            ReviewAssessmentReportSubmittedtoInnovate = 130,
            ReviewAssessmentReportSubmittedtoInnovateCustom = 132,
            ReviewAssessmentReportNotAccepted = 140,
            ReviewAssessmentReportSubmittedtoReferrer = 150,
            FinalAssessmentReportSubmittedtoInnovate = 160,
            FinalAssessmentReportSubmittedtoInnovateCustom = 162,
            FinalAssessmentReportNotAccepted = 170,
            FinalAssessmentReportSubmittedtoReferrer = 180,
            CaseStopped = 200,
            CaseCompleted = 210,
            InvoiceSent = 220,
            CaseStoppedSentToSupplier = 205,
            InitialAssessmentCustom = 55,
            InitialAssessmentSubmittedtoInnovateCustom = 72
        }

        public struct Controllers
        {
            public const string Home = "Home";
            public const string User = "User";
            public const string Referrer = "Referrer";
            public const string Supplier = "Supplier";
            public const string File = "File";
            public struct Area
            {
                public struct Referrer
                {
                    public const string Home = "Home";
                    public const string ReferrerCaseSearch = "CaseSearch";
                    public const string AddReferral = "AddReferral"; 
                }
                public struct Supplier
                {
                    public const string Home = "Home";
                    public const string BookIA = "BookIA";
                    public const string TriageAssessment = "TriageAssessment";
                    public const string StoppedCases = "StoppedCases";
                    public const string ReviewAssessment = "ReviewAssessment";
                    public const string ReviewAssessmentCustom = "ReviewAssessmentCustom";
                    public const string FinalAssessment = "FinalAssessment";
                    public const string SupplierCaseSearch = "CaseSearch";
                    public const string InitialAssessmentCustom = "InitialAssessmentCustom";
                }
            }
        }

        public struct Actions
        {
            //not area specific controller
            public struct HomeController
            {
                public const string Index = "Index";
                public const string EmailSendByEmailID = "EmailSendByEmailID";
                public const string Resetpassword = "Resetpassword";
                public const string LogOut = "LogOut";
                public const string Error = "Error";
            }
            public struct FileController
            {
                public const string ViewReferral = "ViewReferral";
                public const string DownloadTraigeAssessmentForm = "DownloadTraigeAssessmentForm";
            }
            public struct Area
            {
                public struct Referrer
                {
                    public struct HomeController
                    {
                        public const string Index = "Index";

                    }
                    public struct CaseSearch
                    {
                        public const string Search = "Search";
                    }
                    public struct AddReferral
                    {
                        public const string Save = "Save";
                    }
                }
                public struct Supplier
                {
                    public struct HomeController
                    {
                        public const string Index = "Index";
                    }
                    public struct BookIA
                    {
                        public const string Index = "Index";
                    }
                    public struct CaseSearch
                    {
                        public const string Search = "Search";
                    }
                    public struct InitialAssessment
                    {
                        public const string Index = "Index";
                    }
                    public struct TriageAssessment
                    {
                        public const string Index = "Index";
                    }
                    public struct StoppedCases
                    {
                        public const string UpdateCaseStopped = "UpdateCaseStopped";
                    }
                    public struct ReviewAssessment
                    {
                        public const string AddReviewAssessment = "AddReviewAssessment";
                    }
                    public struct ReviewAssessmentCustom
                    {
                        public const string AddReviewAssessmentCustom = "AddReviewAssessmentCustom";
                    }
                    public struct FinalAssessment
                    {
                        public const string SaveFinalAssessment = "SaveFinalAssessment";
                        public const string SubmitFinalAssessment = "SubmitFinalAssessment";
                    }
                }
            }
            public struct Views
            {
                public struct Area
                {
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
            public struct Areas
            {
                public const string Referrer = "Referrer";
                public const string Supplier = "Supplier";
            }
        }
        
        public struct Areas
        {
            public const string Referrer = "Referrer";
            public const string Supplier = "Supplier";
        }
        
        public struct Message
        {
            public const string PasswordNotcorrect = "Password Not correct";
            public const string PasswordUpdatedSuccessfully = "Password Updated Successfully";
            public const string InvalidUser = "Invalid User";
            public const string EmailNotExists = "Email or UserName Not Exists";
            public const string EmailSentSuccessfully = "Email Sent Successfully";
            public const string EmailSentToRegisteredEmail = "Email has been sent to the registered address";
            public const string ReSetUrl = "ReSetUrl";
            public const string EmailLogoPath = "/Storage/Template/logo.png";
            public const string EmailLogo2Path = "/Storage/Template/logo2.png";
            public const string EmailLogo3Path = "/Storage/Template/logo3.png";
        }
        
        public struct Views
        {
            public struct Home
            {
                public const string Index = "Index";
                public const string Error = "Error";
            }
            public struct User
            {
                public const string Resetpassword = "Resetpassword";
            }
        }
        
        public const string TriageAssessment = "Triage Assessment";
        
        public struct ReferrerDocumentType
        {
            public const string ReturnAssessmentCustom = "Return Assessment Custom";
            public const string FinalAssessmentCustom = "Final Assessment Custom";
        }
        
        public enum ReferrerDocumentTypeID
        {
            InitialAssessmentCustom = 11,
            ReturnAssessmentCustom = 12,
            FinalAssessmentCustom = 13
        }
        
        public enum SupplierrDocumentTypeID
        {
            InitialAssessmentSupplierCustom = 15,
            ReviewAssessmentSupplierCustom = 16,
            ReviewAssessmentFinalCustom = 19,
            FinalAssessmentSupplierCustom = 17,
            InitialAssessmentFinalCustom = 14
        }
        
        public struct AssessmentService
        {
            public const int InitialAssessment = 1;
            public const int ReviewAssessment = 2;
            public const int FinalAssessment = 3;
        }
        
        public struct ConstantChar
        {
            public const string Blank = "";
        }
        
        //public struct EmailSetting
        //{
        //    public const string SupportInnovateHMGEmail = "support@innovatehmg.co.uk";
        //    public const string EmailHost = "smtp.office365.com";
        //    public const int EmailPort = 587;
        //    public const string EmailUserId = "software@hcrg.com";
        //    public const string EmailPwd = "Cuxo9003";
        //}

    }
}
