using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using ITS.Infrastructure.Base;
using ITS.Infrastructure.Global;
using AutoMapper;
using ITS.Domain.Models.CaseSearch;
using ITS.Domain.Models.CaseModel;
using ITS.Domain.ViewModels.CaseSearchViewModel;
using ITS.Domain.Models.CaseAssessmentModel;
using ITSWebApp.ITSService.UserService;
using System.Configuration;
using ITS.Infrastructure.ApplicationFilters;
using ITS.Domain.Models.SolicitorModel;
using ITS.Domain.Models.UserModel;
using ITSModel = ITS.Domain.Models;
using System.Net;
using ITS.Domain.ViewModels.InternalTasksViewModel;
using System.IO;
using ITS.Infrastructure.ApplicationServices.Contracts;
using ITS.Infrastructure.ApplicationServices;
using System;
using RTE;
using System.Web.UI.WebControls;
using System.Web;
using ITS.Domain.ViewModels.ReferralViewModel;
using ITSWebApp.ITSService.ReferrerService;


namespace ITSWebApp.Controllers
{
    [AuthorizedUserCheck]
    [ValidSessionCheck]
    public class CaseSearchController : BaseController
    {
        //
        // GET: /CaseSearch/
        private readonly ITSService.CaseService.ICaseService _caseService;
        private readonly ITSService.LookUpService.ILookUpService _lookUpService;
        private readonly ITSService.SupplierService.ISupplierService _supplierService;
        private readonly ITSService.PatientService.IPatientService _patientService;
        private readonly IUserService _userService;
        private readonly ITSService.SolicitorService.ISolicitorService _SolicitorService;
        private readonly ITS.Infrastructure.ApplicationServices.Contracts.IReferrerStorage _referrerStorage;
        private readonly ITSWebApp.ITSService.AssessmentService.IAssessmentService _assessmentService;
        private readonly ILetterGeneration _letterGeneration;
        private readonly EmailService _emailService;
        private readonly IReferrerStorage _referrerStorageService;
        private readonly ISupplierStorage _supplierStorageService;
        private readonly ITSService.ReferrerService.IReferrerService _referrerService;

        public CaseSearchController(ITSService.CaseService.ICaseService caseService, ITSService.LookUpService.ILookUpService lookUpService,
            ITSService.PatientService.IPatientService patientService, ITSService.UserService.IUserService userService, ITSService.SupplierService.ISupplierService supplierService,
            ITS.Infrastructure.ApplicationServices.Contracts.IReferrerStorage referrerStorage, ITSService.SolicitorService.ISolicitorService SolicitorService, ITSService.AssessmentService.IAssessmentService assessmentService,
            ILetterGeneration letterGeneration, EmailService emailService, ISupplierStorage supplierStorageService, IReferrerStorage referrerStorageService, ITSService.ReferrerService.IReferrerService referrerService)
        {
            _caseService = caseService;
            _lookUpService = lookUpService;
            _patientService = patientService;
            _userService = userService;
            _referrerStorage = referrerStorage;
            _SolicitorService = SolicitorService;
            _assessmentService = assessmentService;
            _supplierService = supplierService;
            _letterGeneration = letterGeneration;
            _emailService = emailService;
            _supplierStorageService = supplierStorageService;
            _referrerStorageService = referrerStorageService;
            _referrerService = referrerService;
        }

        public ActionResult Index()
        {
            var searchModel = new ITS.Domain.ViewModels.CaseSearchViewModel.CaseSearchResultViewModel();
            return View(searchModel);
        }


        public ActionResult ReferralDetail(int id)
        {
            var _case = _caseService.GetCaseByCaseID(id);
            var _patient = _patientService.GetPatientByPatientID(_case.PatientID);
            ////var _caseContact = _caseService.GetCaseContactsByCaseID(id);
            int referrerID = _case.ReferrerID;
            if (ITSUser.ReferrerID != null)
            {
                referrerID = ITSUser.ReferrerID.Value;
            }
            AddReferralViewModel model = new AddReferralViewModel();

            model.Case = Mapper.Map<ITSModel.Case>(_case);
            model.Patient = Mapper.Map<ITSModel.Patient>(_patient);

            if (_patient.SolicitorID != null)
                model.Solicitor = Mapper.Map<ITSModel.SolicitorModel.Solicitor>(_SolicitorService.GetSolicitorBySolicitorID(_patient.SolicitorID.Value));
            if (_patient.EmploymentID != null)
                model.Employment = Mapper.Map<ITSModel.Employment>(_caseService.GetEmploymentByEmploymentID(_patient.EmploymentID.Value));
            if (_patient.PolicyID != null)
                model.Policie = Mapper.Map<ITSModel.Policie>(_caseService.GetPolicyByPolicyID(_patient.PolicyID.Value));
            if (_patient.InjuredID != null)
                model.injuredPartyRepresentative = Mapper.Map<ITSModel.InjuredPartyRepresentative>(_referrerService.GetInjuredPartyRepresentativesByID(_patient.InjuredID.Value));
            if (_case.EmployeeDetailID != null)
                model.EmployeeDetail = Mapper.Map<ITSModel.EmployeeDetail>(_caseService.GetEmployeeDetailById(_case.EmployeeDetailID.Value));
            if (_patient.PolicyOpenDetailID != null)
                model.policyOpenDetail = Mapper.Map<ITSModel.PolicyOpenDetail>(_caseService.GetPolicyOpenDetailById(_patient.PolicyOpenDetailID.Value));
            model.CaseContacts = Mapper.Map<IEnumerable<ITSModel.CaseContact>>(_caseService.GetCaseContactsByCaseID(id));
            model.ITSUser = ITSUser;
            model.ReferrerProjects = Mapper.Map<IEnumerable<ITSService.ReferrerService.ReferrerProject>, IEnumerable<ITSModel.ReferrerProject>>(_referrerService.GetReferrerCompleteProjectsByReferrerID(referrerID));
            model.ReferrerAssignedUsers = Mapper.Map<IEnumerable<ITSService.ReferrerService.ReferrerAssignedUser>, IEnumerable<ITSModel.ReferrerAssignedUser>>(_referrerService.GetReferrerAssignedUserByReferrerID(referrerID));
            model.ReferrerLocations = Mapper.Map<IEnumerable<ITSService.ReferrerService.ReferrerLocation>, IEnumerable<ITSModel.ReferrerLocation>>(_referrerService.GetReferrerLocationsByReferrerID(referrerID));
            model.EmploymentTypes = Mapper.Map<IEnumerable<ITSModel.EmploymentType>>(_lookUpService.GetAllEmploymentType());
            model.PolicyTypes = Mapper.Map<IEnumerable<ITSModel.PolicyType>>(_lookUpService.GetAllPolicyType());
            model.TypeCovers = Mapper.Map<IEnumerable<ITSModel.TypeCover>>(_lookUpService.GetAllTypeCover());
            model.PolicyCriterias = Mapper.Map<IEnumerable<ITSModel.PolicyCriteria>>(_lookUpService.GetAllPolicyCriteria());
            model.FitForWorks = Mapper.Map<IEnumerable<ITSModel.FitForWork>>(_lookUpService.GetAllFitForWork());
            model.Admitteds = Mapper.Map<IEnumerable<ITSModel.Admitted>>(_lookUpService.GetAllAdmitted());
            model.Referrer = Mapper.Map<ITSModel.Referrer>(_referrerService.GetReferrerDetailsByReferrerID(referrerID));
            model.WorkTypes = Mapper.Map<IEnumerable<ITSModel.WorkType>>(_lookUpService.GetAllWorkType());
            model.RoleTypes = Mapper.Map<IEnumerable<ITSModel.RoleType>>(_lookUpService.GetAllRoleType());
            model.GenderTypes = Mapper.Map<IEnumerable<ITSModel.Gender>>(_lookUpService.GetAllGenderTypes());
            model.Reinsurers = Mapper.Map<IEnumerable<ITSModel.Reinsurer>>(_lookUpService.GetAllReinsurer());
            model.AdditionalTests = Mapper.Map<IEnumerable<ITSModel.AdditionalTest>>(_lookUpService.GetAllAdditionalTest());
            model.ReasonForReferral = Mapper.Map<IEnumerable<ITSModel.ReasonForReferral>>(_lookUpService.GetAllReasonForReferral());
            model.NetworkRailStandardApplicable = Mapper.Map<IEnumerable<ITSModel.NetworkRailStandardApplicable>>(_lookUpService.GetAllNetworkRailStandardApplicable());
            if (_case.DrugTestID != 0)
            {
                model.DrugTest = Mapper.Map<ITSModel.DrugTest>(_caseService.GetDrugTestByCaseID(id));
            }     
                        
            model.JobDemand = Mapper.Map<ITSModel.JobDemand>(_caseService.GetJobDemandByCaseID(id));
            model.CaseReferrerAssignedUsers = Mapper.Map<IEnumerable<ITSModel.CaseReferrerAssignedUser>>(_caseService.GetCaseReferrerAssignedUsersByCaseID(id));
            return View(model);
        }

        [HttpGet]
        public JsonResult GetAllInjuredRepresentativeOption()
        {
            return Json(_referrerService.GetAllInjuredRepresentativeOption(), GlobalConst.ContentTypes.TextHTML, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetAllPrimaryCondition()
        {
            return Json(_referrerService.GetAllPrimaryCondition(), GlobalConst.ContentTypes.TextHTML, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetAllGips()
        {
            return Json(_caseService.GetAllGips(), GlobalConst.ContentTypes.TextHTML, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetAllIips()
        {
            return Json(_caseService.GetAllIips(), GlobalConst.ContentTypes.TextHTML, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetAllTpds()
        {
            return Json(_caseService.GetAllTpds(), GlobalConst.ContentTypes.TextHTML, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetAllElRehabs()
        {
            return Json(_caseService.GetAllElRehabs(), GlobalConst.ContentTypes.TextHTML, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetUserContacts(int caseID)
        {
            IEnumerable<ITSService.UserService.User> dtoUsers = _userService.GetReferrerUsersByCaseID(caseID);
            IEnumerable<ITSModel.ITSUser> users = Mapper.Map<IEnumerable<ITSService.UserService.User>, IEnumerable<ITSModel.ITSUser>>(dtoUsers);
            return Json(users);
        }

        [HttpPost]
        public JsonResult GetEmailContacts(int caseID)
        {
            var emailContacts = Mapper.Map<IEnumerable<ITSModel.CaseContact>>(_caseService.GetCaseContactsByCaseID(caseID));
            return Json(emailContacts);
        }

        [HttpPost]
        public JsonResult GetTreatmentCategories(int referrerProjectID)
        {
            if (referrerProjectID != 0)
            {
                IEnumerable<ReferrerProjectTreatmentName> referrerProjectTreatmentCategories = Mapper.Map<IEnumerable<ITSService.ReferrerService.ReferrerProjectTreatmentName>,
                    IEnumerable<ReferrerProjectTreatmentName>>(_referrerService.GetReferrerEnabledProjectTreatmentNamesByReferrerProjectID(referrerProjectID));
                return Json(referrerProjectTreatmentCategories);
            }
            return null;
        }

        [HttpPost]
        public JsonResult GetTreatmentTypes(int referrerProjectTreatmentID)
        {
            if (referrerProjectTreatmentID != 0)
            {
                IEnumerable<ReferrerProjectTreatmentTreatmentType> treatmentCategoriesTreatmentTypes = Mapper.Map<IEnumerable<ITSService.ReferrerService.ReferrerProjectTreatmentTreatmentType>,
                            IEnumerable<ReferrerProjectTreatmentTreatmentType>>(_referrerService.GetReferrerProjectTreatmentTreatmentTypeByReferrerProjectTreatmentTypeID(referrerProjectTreatmentID));
                return Json(treatmentCategoriesTreatmentTypes);
            }
            return null;
        }

        [HttpPost]
        public JsonResult AddCaseAssignedUser(ITSModel.CaseReferrerAssignedUser _assignUser)
        {
            try
            {
                if (_assignUser.CaseID != 0)
                {
                    int id = _caseService.AddCaseReferrerAssignedUser(Mapper.Map<ITSService.CaseService.CaseReferrerAssignedUser>(_assignUser));
                    return Json(id);
                }
                else
                {
                    return Json("");
                }
            }
            catch
            {
                return Json(null);
            }
        }

        [HttpPost]
        public JsonResult DeleteCaseAssignedUser(int _id)
        {
            try
            {
                _caseService.DeleteCaseReferrerAssignedUserByID(_id);
                return Json(true);
            }
            catch
            {
                return Json(false);
            }
        }

        [HttpPost]
        public ActionResult UpdateReferralDetail(AddReferralViewModel viewModel)
        {
            if (viewModel.Case.SendInvoiceTo == "ReferrerProjectUser")
            {
                viewModel.Case.SendInvoiceName = "";
                viewModel.Case.SendInvoiceEmail = "";
                viewModel.Case.SupplierAssignedUser = 0;
            }

            if (viewModel.Case.SupplierAssignedUser == null)
            {
                viewModel.Case.SupplierAssignedUser = 0;
            }

            viewModel.Case.SendInvoiceTo = "Assigned User"; // as pe task 3502 point 1a.
            viewModel.Case.SendInvoiceName = "NULL"; // as pe task 3502 point 1a.
            viewModel.Case.SendInvoiceEmail = "NULL"; // as pe task 3502 point 1a.

            if (viewModel.DrugTest != null)
            {
                if (!viewModel.DrugTest.IsDrugAndAlcohalTest)
                {
                    viewModel.DrugTest.NetworkRailStandardApplicableID = 0;
                    viewModel.DrugTest.ReasonForReferralID = 0;
                    viewModel.DrugTest.IsSentinalUpdating = true;
                    viewModel.DrugTest.SentinalNumber = null;
                    viewModel.DrugTest.AdditionalTestID = 2;
                    viewModel.DrugTest.AdditionalTestOther = null;
                }
            }
            var res = _caseService.UpdateReferral(Mapper.Map<ITSService.CaseService.Case>(viewModel.Case), Mapper.Map<ITSService.CaseService.Patient>(viewModel.Patient), ITSUser.UserID
                , Mapper.Map<ITSService.CaseService.Solicitor>(viewModel.Solicitor), Mapper.Map<ITSService.CaseService.InjuredPartyRepresentative>(viewModel.injuredPartyRepresentative)
                , Mapper.Map<ITSService.CaseService.Employment>(viewModel.Employment), Mapper.Map<ITSService.CaseService.Policie>(viewModel.Policie)
                , Mapper.Map<ITSService.CaseService.EmployeeDetail>(viewModel.EmployeeDetail), Mapper.Map<ITSService.CaseService.PolicyOpenDetail>(viewModel.policyOpenDetail)
                , Mapper.Map<ITSService.CaseService.DrugTest>(viewModel.DrugTest), Mapper.Map<ITSService.CaseService.JobDemand>(viewModel.JobDemand));

            //to remove contact
            var emailContacts = Mapper.Map<IEnumerable<ITSModel.CaseContact>>(_caseService.GetCaseContactsByCaseID(viewModel.Case.CaseID));
            foreach (var contact in emailContacts)
            {
                if (checkContactsToBeRemoved(viewModel.Case.CaseID, contact.UserID, viewModel.CaseContacts))
                {
                    contact.CaseID = viewModel.Case.CaseID;
                    _caseService.DeleteCaseContactByID(contact.CaseContactID);
                }
            }


            foreach (var contact in viewModel.CaseContacts)
            {
                if (checkContact(viewModel.Case.CaseID, contact.UserID))
                {
                    contact.CaseID = viewModel.Case.CaseID;
                    _caseService.AddCaseContact(Mapper.Map<ITS.Domain.Models.CaseContact, ITSService.CaseService.CaseContact>(contact));
                }
            }

            return RedirectToAction("ReferralDetail?CaseID=" + viewModel.Case.CaseID);

        }


        public bool checkContactsToBeRemoved(int caseId, int userId, IEnumerable<ITS.Domain.Models.CaseContact> contacts)
        {
            foreach (var prevContact in contacts)
            {
                if (userId != prevContact.UserID)
                    return true;
            }
            return false;
        }

        public bool checkContact(int caseId, int userId)
        {
            var emailContacts = Mapper.Map<IEnumerable<ITSModel.CaseContact>>(_caseService.GetCaseContactsByCaseID(caseId));
            foreach (var prevContact in emailContacts)
            {
                if (userId == prevContact.UserID)
                    return false;
            }
            return true;
        }

        [HttpPost]
        public ActionResult CaseSearch(ITS.Domain.ViewModels.CaseSearchViewModel.CaseSearchResultViewModel searchModel /*UPDATE THIS MODEL IF NEEDED*/)
        {

            switch (searchModel.CaseSearch.SearchCriteria)
            {
                case (int)GlobalConst.CaseSearchCriteria.PatientName:
                    var byPatientNameResults = _caseService.GetCasePatientTreatmentWorkflowLikePatientName(searchModel.CaseSearch.AdditionalParam, searchModel.CaseSearch.SearchText, searchModel.Skip, searchModel.Take);
                    searchModel.Cases = Mapper.Map<IEnumerable<CasePatientTreatmentWorkflow>>(byPatientNameResults.CasePatientTreatmentWorkflows);
                    searchModel.TotalCount = byPatientNameResults.TotalCount;
                    break;
                case (int)GlobalConst.CaseSearchCriteria.CaseNumber:
                    var byCaseNumberResults = _caseService.GetCasePatientTreatmentWorkflowLikeCaseNumber(searchModel.CaseSearch.AdditionalParam, searchModel.CaseSearch.SearchText, searchModel.Skip, searchModel.Take);
                    searchModel.Cases = Mapper.Map<IEnumerable<CasePatientTreatmentWorkflow>>(byCaseNumberResults.CasePatientTreatmentWorkflows);
                    searchModel.TotalCount = byCaseNumberResults.TotalCount;
                    break;
                case (int)GlobalConst.CaseSearchCriteria.PostCode:
                    var byPostCodeResults = _caseService.GetCasePatientTreatmentWorkflowLikePostCode(searchModel.CaseSearch.AdditionalParam, searchModel.CaseSearch.SearchText, searchModel.Skip, searchModel.Take);
                    searchModel.Cases = Mapper.Map<IEnumerable<CasePatientTreatmentWorkflow>>(byPostCodeResults.CasePatientTreatmentWorkflows);
                    searchModel.TotalCount = byPostCodeResults.TotalCount;
                    break;
                case (int)GlobalConst.CaseSearchCriteria.ReferrerRefNumber:
                    var byReferrerRefNumberResults = _caseService.GetCasePatientTreatmentWorkflowLikeCaseReferrerReferenceNumber(searchModel.CaseSearch.AdditionalParam, searchModel.CaseSearch.SearchText, searchModel.Skip, searchModel.Take);
                    searchModel.Cases = Mapper.Map<IEnumerable<CasePatientTreatmentWorkflow>>(byReferrerRefNumberResults.CasePatientTreatmentWorkflows);
                    searchModel.TotalCount = byReferrerRefNumberResults.TotalCount;
                    break;
                case (int)GlobalConst.CaseSearchCriteria.TreatmentCategory:
                    var byTreatmentCategoryResults = _caseService.GetCasePatientTreatmentWorkflowLikeTreatmentCategoryName(searchModel.CaseSearch.AdditionalParam, searchModel.CaseSearch.SearchText, searchModel.Skip, searchModel.Take);
                    searchModel.Cases = Mapper.Map<IEnumerable<CasePatientTreatmentWorkflow>>(byTreatmentCategoryResults.CasePatientTreatmentWorkflows);
                    searchModel.TotalCount = byTreatmentCategoryResults.TotalCount;
                    break;
                case (int)GlobalConst.CaseSearchCriteria.TreatmentType:
                    var byTreatmentTypeResults = _caseService.GetCasePatientTreatmentWorkflowLikeTreatmentTypeName(searchModel.CaseSearch.AdditionalParam, searchModel.CaseSearch.SearchText, searchModel.Skip, searchModel.Take);
                    searchModel.Cases = Mapper.Map<IEnumerable<CasePatientTreatmentWorkflow>>(byTreatmentTypeResults.CasePatientTreatmentWorkflows);
                    searchModel.TotalCount = byTreatmentTypeResults.TotalCount;
                    break;
                case (int)GlobalConst.CaseSearchCriteria.ReferrerName:
                    var byReferrerNameResults = _caseService.GetCasePatientTreatmentWorkflowLikeReferrerName(searchModel.CaseSearch.AdditionalParam, searchModel.CaseSearch.SearchText, searchModel.Skip, searchModel.Take);
                    searchModel.Cases = Mapper.Map<IEnumerable<CasePatientTreatmentWorkflow>>(byReferrerNameResults.CasePatientTreatmentWorkflows);
                    searchModel.TotalCount = byReferrerNameResults.TotalCount;
                    break;
            }
            return Json(searchModel, ITS.Infrastructure.Global.GlobalConst.ContentTypes.TextHTML);
        }

        [HttpGet]
        public ActionResult Detail(int? caseID)
        {
            string storagePath = ConfigurationManager.AppSettings["StoragePath"];
            if (!caseID.HasValue)
                return RedirectToAction(GlobalConst.Actions.CaseSearchController.Index);
            ViewBag.caseID = caseID.Value;
            CasePatientTreatment caseTreatment = Mapper.Map<CasePatientTreatment>(_patientService.GetPatientAndCaseByCaseID(caseID.Value));
            Case caseObj = Mapper.Map<Case>(_caseService.GetCaseByCaseID(caseID.Value));
            int DocumentSetupTypeID = _caseService.GetReferrerProjectTreatmentDocumentSetup(caseID.Value, GlobalConst.AssessmentService.InitialAssessment);
            caseTreatment.ReferralFileDownloadPath = Url.Action(GlobalConst.Actions.FileController.ViewReferral, GlobalConst.Controllers.File, new { caseID = caseID.Value });
            IEnumerable<SupplierDistanceRankPrice> supplierDistanceRankPrice = Mapper.Map<IEnumerable<SupplierDistanceRankPrice>>(_supplierService.GetSupplierWithinPostCode(caseTreatment.PostCode, 10, caseTreatment.TreatmentCategoryID));

            if (supplierDistanceRankPrice == null)
                supplierDistanceRankPrice = new List<SupplierDistanceRankPrice>();
            else
                supplierDistanceRankPrice = supplierDistanceRankPrice.OrderBy(supplier => supplier.Distance).ThenByDescending(supplier => supplier.Ranking);

            IEnumerable<ITS.Domain.Models.SupplierSupplierTreatmentsAndSupplierTreatmenPricing> supplierTreatmentsAndSupplierTreatmenPricing = Mapper.Map<IEnumerable<ITS.Domain.Models.SupplierSupplierTreatmentsAndSupplierTreatmenPricing>>(_supplierService.GetTriageTopSuppliersTreamentPricingByTreatmentCategoryID(caseTreatment.TreatmentCategoryID));

            if (supplierTreatmentsAndSupplierTreatmenPricing == null)
                supplierTreatmentsAndSupplierTreatmenPricing = new List<ITS.Domain.Models.SupplierSupplierTreatmentsAndSupplierTreatmenPricing>();
            else
                supplierTreatmentsAndSupplierTreatmenPricing = supplierTreatmentsAndSupplierTreatmenPricing.OrderBy(supplier => supplier.Ranking);

            CaseSearchDetailViewModel obj = new CaseSearchDetailViewModel();
            obj.CaseAppointmentDate = Mapper.Map<ITSModel.CaseAppointmentDate>(_caseService.GetCaseAppointmentDateByCaseID((int)caseID));
            var viewModel = new CaseSearchDetailViewModel()
            {
                CasePatientReferrerSupplierWorkflow = Mapper.Map<CasePatientReferrerSupplierWorkflow>(_caseService.GetCasePatientReferrerSupplierWorkflowByCaseID(caseID.Value)),
                CaseNotes = Mapper.Map<IEnumerable<CaseNoteUser>>(_caseService.GetCaseNotesByCaseID(caseID.Value)),
                CaseHistories = Mapper.Map<IEnumerable<CaseHistoryUser>>(_caseService.GetCaseHistoriesByCaseID(caseID.Value)),
                CaseTreatmentPricingTypes = Mapper.Map<IEnumerable<CaseTreatmentPricingType>>(_caseService.GetCaseTreatmentPricingByCaseIDCaseSearch(caseID.Value)),
                CaseBespokeServicePricingTypes = Mapper.Map<IEnumerable<CaseBespokeServicePricingType>>(_caseService.GetCaseBespokeServicePricingForInvoice(caseID.Value)),
                CaseDocumentUsers = Mapper.Map<IEnumerable<CaseDocumentUser>>(_caseService.GetCaseDocumentUserByCaseID(caseID.Value)),
                IsFileExist = _referrerStorage.ReferralFileExists(caseObj.ReferrerID, caseObj.ReferrerProjectTreatmentID, caseObj.CaseID, caseObj.CaseNumber + GlobalConst.FileExtensions.PDF.ToString(), ConfigurationManager.AppSettings["StoragePath"]),
                CaseCommunicationHistoryUser = Mapper.Map<IEnumerable<CaseCommunicationHistoryUser>>(_caseService.GetCaseCommunicationHistoriesByCaseID(caseID.Value)),
                Solicitor = Mapper.Map<Solicitor>(_SolicitorService.GetSolicitorByPatientID(caseObj.PatientID)),
                CaseAppointmentDate = Mapper.Map<ITSModel.CaseAppointmentDate>(_caseService.GetCaseAppointmentDateByCaseID((int)caseID)),
                CaseAssessmentDetails = Mapper.Map<IEnumerable<ITS.Domain.Models.CaseAssessmentDetail>>(_assessmentService.GetQASubmitedCaseAssessmentDetailsByCaseID((int)caseID)),
                SupplierDistanceRankPrice = supplierDistanceRankPrice,
                CasePatientTreatment = caseTreatment,
                TopTriageSupplierSupplierTreatmentsAndSupplierTreatmenPricing = supplierTreatmentsAndSupplierTreatmenPricing,
                workflowID = caseObj.WorkflowID,
                TreatmentReferrerSupplierPricing = caseObj.SupplierID != null ? Mapper.Map<IEnumerable<TreatmentReferrerSupplierPricing>>(_caseService.GetTreatmentReferrerSupplierPricingQA(caseObj.SupplierID.Value, caseObj.ReferrerProjectTreatmentID, caseTreatment.TreatmentCategoryID)) : null, //treatmentReferrerSupplierPricing,
                CaseReportsDetailsCustom = Mapper.Map<IEnumerable<ITS.Domain.Models.SupplierDocument>>(_supplierService.GetSupplierDocumentByCaseId(caseID.Value)),
                CasePatientContactDates = Mapper.Map<IEnumerable<ITS.Domain.Models.CasePatientContactAttempt>>(_caseService.GetPatientContactDate((int)caseID)),
                CaseUnsuccessfullContactDates = Mapper.Map<IEnumerable<ITS.Domain.Models.CasePatientContactAttempt>>(_caseService.GetUnsuccessfulContactDate((int)caseID))
            };
            viewModel.AllTriageSupplierSupplierTreatmentsAndSupplierTreatmenPricing = Mapper.Map<IEnumerable<ITS.Domain.Models.SupplierSupplierTreatmentsAndSupplierTreatmenPricing>>(_supplierService.GetTriageSuppliersTreamentPricingByTreatmentCategoryID(viewModel.CasePatientTreatment.TreatmentCategoryID));
            viewModel.CaseReportsDetails = Mapper.Map<IEnumerable<ITSModel.CaseSearch.CaseReportsDetail>>(from casereports in viewModel.CaseHistories
                                                                                                          where casereports.EventDescription == GlobalConst.WorkflowEventDescription.InitialAssessmentSubmittedtoInnovate
                                                                                                          || casereports.EventDescription == GlobalConst.WorkflowEventDescription.InitialAssessmentReportSubmittedtoReferrerOrAwaitingAuthorisation
                                                                                                          || casereports.EventDescription == GlobalConst.WorkflowEventDescription.ReviewAssessmentReportSubmittedtoInnovate
                                                                                                          || casereports.EventDescription == GlobalConst.WorkflowEventDescription.ReviewAssessmentReportSubmittedtoReferrer
                                                                                                          || casereports.EventDescription == GlobalConst.WorkflowEventDescription.FinalAssessmentReportSubmittedtoInnovate
                                                                                                          || casereports.EventDescription == GlobalConst.WorkflowEventDescription.FinalAssessmentReportSubmittedtoReferrer
                                                                                                          || casereports.EventDescription == GlobalConst.WorkflowEventDescription.InvoiceSent
                                                                                                          orderby (casereports.CaseHistoryID)
                                                                                                          select casereports);

            foreach (ITS.Domain.Models.CaseSearch.CaseReportsDetail ReportsDetail in viewModel.CaseReportsDetails)
            {
                if (ReportsDetail.EventDescription == "Final Assessment Report Submitted to Innovate" || ReportsDetail.EventDescription == "Final Assessment Report Submitted to Referrer")
                {
                    ReportsDetail.AssessmentServiceID = 3;
                    var CaseAssessmentDetailID = (from casereports in viewModel.CaseAssessmentDetails
                                                  where casereports.AssessmentServiceID == 3
                                                  select casereports.CaseAssessmentDetailID);
                    foreach (int DetailID in CaseAssessmentDetailID)
                    {
                        ReportsDetail.CaseAssessmentDetailID = DetailID;
                    }
                }
            }

            Parallel.ForEach(viewModel.CaseReportsDetails, _CaseReportsDetails =>
            {
                switch (_CaseReportsDetails.EventDescription)
                {
                    case GlobalConst.WorkflowEventDescription.InitialAssessmentSubmittedtoInnovate:
                        _CaseReportsDetails.DocumentName = viewModel.CasePatientReferrerSupplierWorkflow.FirstName + "_" + viewModel.CasePatientReferrerSupplierWorkflow.LastName + "_Initial Assessment_Supplier";
                        break;
                    case GlobalConst.WorkflowEventDescription.InitialAssessmentReportSubmittedtoReferrerOrAwaitingAuthorisation:
                        _CaseReportsDetails.DocumentName = viewModel.CasePatientReferrerSupplierWorkflow.FirstName + "_" + viewModel.CasePatientReferrerSupplierWorkflow.LastName + "_Initial Assessment_Final";
                        break;
                    case GlobalConst.WorkflowEventDescription.ReviewAssessmentReportSubmittedtoInnovate:
                        _CaseReportsDetails.DocumentName = viewModel.CasePatientReferrerSupplierWorkflow.FirstName + "_" + viewModel.CasePatientReferrerSupplierWorkflow.LastName + "_Review Assessment_Supplier";
                        break;
                    case GlobalConst.WorkflowEventDescription.ReviewAssessmentReportSubmittedtoReferrer:
                        _CaseReportsDetails.DocumentName = viewModel.CasePatientReferrerSupplierWorkflow.FirstName + "_" + viewModel.CasePatientReferrerSupplierWorkflow.LastName + "_Review Assessment_Final";
                        break;
                    case GlobalConst.WorkflowEventDescription.FinalAssessmentReportSubmittedtoInnovate:
                        _CaseReportsDetails.DocumentName = viewModel.CasePatientReferrerSupplierWorkflow.FirstName + "_" + viewModel.CasePatientReferrerSupplierWorkflow.LastName + "_Final Assessment_Supplier";
                        break;
                    case GlobalConst.WorkflowEventDescription.FinalAssessmentReportSubmittedtoReferrer:
                        _CaseReportsDetails.DocumentName = viewModel.CasePatientReferrerSupplierWorkflow.FirstName + "_" + viewModel.CasePatientReferrerSupplierWorkflow.LastName + "_Final Assessment_Final";
                        break;
                    case GlobalConst.WorkflowEventDescription.InvoiceSent:
                        _CaseReportsDetails.DocumentName = viewModel.CasePatientReferrerSupplierWorkflow.FirstName + "_" + viewModel.CasePatientReferrerSupplierWorkflow.LastName + "_Invoice_Sent";
                        break;
                }
            });

            if (viewModel.CaseAppointmentDate != null)
                viewModel.CaseAppointmentDate.WorkflowID = caseObj.WorkflowID;

            viewModel.ReferralFileDownloadPath =
                Url.Action(GlobalConst.Actions.FileController.ViewReferral, GlobalConst.Controllers.File, new { caseID = caseID });

            viewModel.AllSupplierWithinPostCode = Mapper.Map<IEnumerable<SuppliersName>>(_supplierService.GetAllSupplierWithinPostCode(caseTreatment.PostCode, 1000, caseTreatment.TreatmentCategoryID));
            if (caseObj.SupplierID != null)
                viewModel.ReportFileDownloadPath = _supplierStorageService.GetProjectTreatmentSupplierUploadFolderStoragePathwithoutfile((int)caseObj.SupplierID, caseObj.ReferrerProjectTreatmentID, caseObj.CaseID, storagePath);
            else
                viewModel.ReportFileDownloadPath = "";

            //Decodeing SpecialInstructionNotes field if exists And rich text editor code
            string decodedHTML = "";
            if (viewModel.CasePatientReferrerSupplierWorkflow.SpecialInstructionNotes != null)
            {
                decodedHTML = HttpUtility.HtmlDecode(viewModel.CasePatientReferrerSupplierWorkflow.SpecialInstructionNotes);
                viewModel.CasePatientReferrerSupplierWorkflow.SpecialInstructionNotes = decodedHTML;
            }
            Editor EditorCaseSearch = new Editor(System.Web.HttpContext.Current, "EditorCaseSearch");
            EditorCaseSearch.LoadFormData(decodedHTML);
            EditorCaseSearch.Toolbar = "hide";
            EditorCaseSearch.ShowBottomBar = false;
            EditorCaseSearch.ClientFolder = "/richtexteditor/";
            EditorCaseSearch.Width = Unit.Pixel(800);
            EditorCaseSearch.Height = Unit.Pixel(200);
            EditorCaseSearch.ResizeMode = RTEResizeMode.Disabled;
            EditorCaseSearch.FullScreen = false;
            EditorCaseSearch.DisabledItems = "save, help";
            string content = Request.Form["EditorCaseSearch"];
            EditorCaseSearch.MvcInit();
            ViewData["EditorCaseSearch"] = EditorCaseSearch.MvcGetString();
            EditorCaseSearch.ResizeMode = RTEResizeMode.Disabled;
            return View(viewModel);
        }

        [HttpGet]
        public ActionResult ViewCaseUploads(string UploadPath, int CaseId)
        {
            var _case = _caseService.GetCaseByCaseID(CaseId);
            var filePath = "";
            string storagePath = Server.MapPath(ConfigurationManager.AppSettings["StoragePath"]);
            filePath = _referrerStorageService.GetProjectTreatmentReferralCaseUploadFolderStoragePath(_case.ReferrerID, _case.ReferrerProjectTreatmentID, _case.CaseID, UploadPath, storagePath);

            if (Path.GetExtension(filePath.ToLower()) == ".doc" || Path.GetExtension(filePath.ToLower()) == ".docx")
                return File(filePath, "application/msword", UploadPath);
            else if (Path.GetExtension(filePath.ToLower()) == ".jpg" || Path.GetExtension(filePath.ToLower()) == ".jpeg")
                return File(filePath, "image/jpeg", UploadPath);
            else
                return File(filePath, "application/pdf", UploadPath);
        }

        [HttpPost]
        public ActionResult BookInitialAppointment(ITSModel.CaseAppointmentDate caseAppointmentDate)
        {
            int result;
            if (caseAppointmentDate.CaseID > 0)
                result = _caseService.UpdateCaseAppointmentDate(Mapper.Map<ITSService.CaseService.CaseAppointmentDate>(caseAppointmentDate));
            else
            {
                caseAppointmentDate.CaseID = caseAppointmentDate.CaseIDPK;
                result = _caseService.AddCaseAppointmentDate(Mapper.Map<ITSService.CaseService.CaseAppointmentDate>(caseAppointmentDate));
            }

            if (result > 0)
                return Json(caseAppointmentDate.CaseID, GlobalConst.ContentTypes.TextHTML);
            else
                return Json("Error occor please try again", GlobalConst.ContentTypes.TextHTML);
        }


        [HttpPost]
        public ActionResult UpdateCaseSearchTreatmentPricing(CaseTreatmentPricingType caseTreatmentPricingType, IEnumerable<TreatmentReferrerSupplierPricing> selectedTreatmentPricing, int caseID)
        {
            caseTreatmentPricingType.ReferrerPrice = Convert.ToDecimal(caseTreatmentPricingType.ReferrerPrice1);
            caseTreatmentPricingType.SupplierPrice = Convert.ToDecimal(caseTreatmentPricingType.SupplierPrice1);
            caseTreatmentPricingType.CaseID = caseID;
            foreach (var SelectedTreatment in selectedTreatmentPricing)
            {
                if (SelectedTreatment.PricingTypeName == caseTreatmentPricingType.PricingTypeName)
                {
                    caseTreatmentPricingType.ReferrerPricingID = SelectedTreatment.ReferrerPricingID;
                    caseTreatmentPricingType.SupplierPriceID = SelectedTreatment.SupplierPriceID;
                    caseTreatmentPricingType.CaseID = caseID;
                    break;
                }
            }
            if (caseTreatmentPricingType.CaseTreatmentPricingID != 0)
            {
                _caseService.UpdateCaseTreatmentPricingPriceByCaseTreatmentPricingID(Mapper.Map<ITSService.CaseService.CaseTreatmentPricing>(caseTreatmentPricingType));
                caseTreatmentPricingType.IsChanged = 1;
                //_caseService.UpdateCaseTreatmentPricingPriceByReferrerPricingID(Mapper.Map<ITSService.CaseService.CaseTreatmentPricingCaseSearch>(caseTreatmentPricingType));
            }
            else
            {
                caseTreatmentPricingType.Quantity = 1;
                caseTreatmentPricingType.CaseTreatmentPricingID = _caseService.AddCaseTreatmentPricingCaseSearch(Mapper.Map<ITSService.CaseService.CaseTreatmentPricing>(caseTreatmentPricingType));
                caseTreatmentPricingType.IsChanged = 0;
            }

            CaseSearchDetailViewModel viewModel = new CaseSearchDetailViewModel();
            viewModel.CaseTreatmentPricingTypes = Mapper.Map<IEnumerable<CaseTreatmentPricingType>>(_caseService.GetCaseTreatmentPricingByCaseIDCaseSearch(caseID));

            caseTreatmentPricingType.ReferrerPriceTotal = 0;
            caseTreatmentPricingType.SupplierPriceTotal = 0;
            foreach (var refferSupplierData in viewModel.CaseTreatmentPricingTypes)
            {
                caseTreatmentPricingType.ReferrerPriceTotal += refferSupplierData.ReferrerPrice;
                caseTreatmentPricingType.SupplierPriceTotal += refferSupplierData.SupplierPrice;
            }

            return Json(caseTreatmentPricingType);
        }

        [HttpPost]
        public ActionResult DeleteCaseTreatmentPricingByCaseTreatmentPricingID(int caseTreatmentPricingID, int WorkFlowID, int caseID)
        {
            if (WorkFlowID < 180)
                _caseService.DeleteCaseTreatmentPricingByCaseTreatmentPricingID(caseTreatmentPricingID);

            CaseSearchDetailViewModel viewModel = new CaseSearchDetailViewModel();
            viewModel.CaseTreatmentPricingTypes = Mapper.Map<IEnumerable<CaseTreatmentPricingType>>(_caseService.GetCaseTreatmentPricingByCaseIDCaseSearch(caseID));
            CaseTreatmentPricingType caseTreatmentPricingType = new CaseTreatmentPricingType();
            caseTreatmentPricingType.ReferrerPriceTotal = 0;
            caseTreatmentPricingType.SupplierPriceTotal = 0;
            foreach (var refferSupplierData in viewModel.CaseTreatmentPricingTypes)
            {
                caseTreatmentPricingType.ReferrerPriceTotal += refferSupplierData.ReferrerPrice;
                caseTreatmentPricingType.SupplierPriceTotal += refferSupplierData.SupplierPrice;
            }
            return Json(caseTreatmentPricingType);
        }
        [HttpPost]
        public ActionResult GetGeneratedAssessmentReport(int rptCaseID, string EventDescription, CaseCommunicationHistory caseCommunicationHistory)
        {
            int Count = 0;
            WebClient client = new WebClient();
            client.Credentials = CredentialCache.DefaultNetworkCredentials;
            string reportURL = "";
            string repName = "";
            int CaseId = 0;
            byte[] arrreportURL = new byte[1];
            switch (EventDescription)
            {
                case GlobalConst.WorkflowEventDescription.InitialAssessmentSubmittedtoInnovate:
                    reportURL = string.Format(ConfigurationManager.AppSettings["InitialAssessmentReportUrl"], rptCaseID, GlobalConst.ReportFormat.Word);
                    repName = "InitialAssessment-{0}.doc";
                    EventDescription = "";
                    break;
                case GlobalConst.WorkflowEventDescription.InitialAssessmentReportSubmittedtoReferrerOrAwaitingAuthorisation:
                    reportURL = string.Format(ConfigurationManager.AppSettings["InitialAssessmentReportUrl"], rptCaseID, GlobalConst.ReportFormat.Word);
                    repName = "InitialAssessment-{0}.doc";
                    EventDescription = "";
                    break;
                case GlobalConst.WorkflowEventDescription.ReviewAssessmentReportSubmittedtoInnovate:
                    reportURL = string.Format(ConfigurationManager.AppSettings["ReviewAssessmentReportUrl"], rptCaseID, "Review Assessment", GlobalConst.ReportFormat.Word);
                    repName = "ReviewAssessment-{0}.doc";
                    EventDescription = "";
                    break;
                case GlobalConst.WorkflowEventDescription.ReviewAssessmentReportSubmittedtoReferrer:
                    reportURL = string.Format(ConfigurationManager.AppSettings["ReviewAssessmentReportUrl"], rptCaseID, "Review Assessment", GlobalConst.ReportFormat.Word);
                    repName = "ReviewAssessment-{0}.doc";
                    EventDescription = "";
                    break;
                case GlobalConst.WorkflowEventDescription.FinalAssessmentReportSubmittedtoInnovate:
                    reportURL = string.Format(ConfigurationManager.AppSettings["ReviewAssessmentReportUrl"], rptCaseID, "Final Assessment", GlobalConst.ReportFormat.Word);
                    repName = "FinalAssessment-{0}.doc";
                    EventDescription = "";
                    break;
                case GlobalConst.WorkflowEventDescription.FinalAssessmentReportSubmittedtoReferrer:
                    reportURL = string.Format(ConfigurationManager.AppSettings["ReviewAssessmentReportUrl"], rptCaseID, "Final Assessment", GlobalConst.ReportFormat.Word);
                    repName = "FinalAssessment-{0}.doc";
                    EventDescription = "";
                    break;
                case GlobalConst.WorkflowEventDescription.InvoiceSent:
                    CaseId = rptCaseID;
                    var casePatientTreatment = Mapper.Map<CasePatientTreatment>(_patientService.GetPatientAndCaseByCaseID(rptCaseID));
                    var caseObj = Mapper.Map<ITS.Domain.Models.Case>(_caseService.GetCaseByCaseID(rptCaseID));
                    var viewModel = new CaseCompleteInvoicingViewModel()
                    {
                        CaseID = CaseId,
                        CaseTreatmentPricingType = Mapper.Map<IEnumerable<CaseTreatmentPricingType>>(_caseService.GetCaseTreatmentPricingsForInvoice(CaseId)),
                        CaseBespokeServicePricingType = Mapper.Map<IEnumerable<CaseBespokeServicePricingType>>(_caseService.GetCaseBespokeServicePricingForInvoice(CaseId)),
                        ReferrerAndSupplierVATPricing = Mapper.Map<ReferrerAndSupplierPricing>(_caseService.GetReferrerAndSupplierPricingVat(CaseId)),
                    };
                    decimal totalAmountOut;
                    string invoiceNumberOut;
                    ITS.Domain.Models.CaseModel.CasePatientReferrer casePatientReferrer = Mapper.Map<ITS.Domain.Models.CaseModel.CasePatientReferrer>(_caseService.GetCasePatientReferrerByCaseID(CaseId));
                    decimal vat = viewModel.ReferrerAndSupplierVATPricing.ReferrerPrice.Value;
                    repName = string.Format("{0}.doc", System.Guid.NewGuid().ToString());
                    arrreportURL = _letterGeneration.GenerateInvoice(casePatientReferrer, viewModel.CaseTreatmentPricingType, viewModel.CaseBespokeServicePricingType, vat, Server.MapPath("~/Content/Images/innovate-logo.jpg"),
                               out totalAmountOut, out invoiceNumberOut);
                    Count = 1;
                    EventDescription = "";
                    break;
                case GlobalConst.WorkflowEventDescription.InvoiceEmailSend:
                    CaseId = rptCaseID;
                    var _casePatientTreatment = Mapper.Map<CasePatientTreatment>(_patientService.GetPatientAndCaseByCaseID(rptCaseID));
                    var _caseObj = Mapper.Map<ITS.Domain.Models.Case>(_caseService.GetCaseByCaseID(rptCaseID));
                    var _viewModel = new CaseCompleteInvoicingViewModel()
                    {
                        CaseID = CaseId,
                        CaseTreatmentPricingType = Mapper.Map<IEnumerable<CaseTreatmentPricingType>>(_caseService.GetCaseTreatmentPricingsForInvoice(CaseId)),
                        CaseBespokeServicePricingType = Mapper.Map<IEnumerable<CaseBespokeServicePricingType>>(_caseService.GetCaseBespokeServicePricingForInvoice(CaseId)),
                        ReferrerAndSupplierVATPricing = Mapper.Map<ReferrerAndSupplierPricing>(_caseService.GetReferrerAndSupplierPricingVat(CaseId)),
                    };
                    decimal _totalAmountOut;
                    string _invoiceNumberOut;
                    ITS.Domain.Models.CaseModel.CasePatientReferrer _casePatientReferrer = Mapper.Map<ITS.Domain.Models.CaseModel.CasePatientReferrer>(_caseService.GetCasePatientReferrerByCaseID(CaseId));
                    decimal _vat = _viewModel.ReferrerAndSupplierVATPricing.ReferrerPrice.Value;
                    repName = string.Format("{0}.doc", System.Guid.NewGuid().ToString());
                    string _fileName = string.Format("{0}.doc", /*Guid.NewGuid().ToString()*/"Invoice");
                    MemoryStream memmoryStreamObj = new MemoryStream(_letterGeneration.GenerateInvoice(_casePatientReferrer, _viewModel.CaseTreatmentPricingType, _viewModel.CaseBespokeServicePricingType, _vat, Server.MapPath("~/Content/Images/innovate-logo.jpg"),
                               out _totalAmountOut, out _invoiceNumberOut));
                    bool result = _emailService.SendGeneralEmail(caseCommunicationHistory.SentTo, caseCommunicationHistory.SentCC, System.Configuration.ConfigurationManager.AppSettings["SupportInnovateHMGEmail"].ToString(), caseCommunicationHistory.Subject, caseCommunicationHistory.Message,
                        new System.Net.Mail.Attachment(memmoryStreamObj, _fileName));
                    Count = 2;
                    TempData["AlertMessage"] = "Email send successfully.";
                    EventDescription = "";
                    break;
            }
            if (Count == 0)
                return File(client.DownloadData(reportURL), "application/msword", string.Format(repName, rptCaseID));
            else if (Count == 1)
                return File(arrreportURL, "application/msword", string.Format(repName, rptCaseID));
            else
                return RedirectToAction("Detail/" + CaseId);
        }

        [HttpPost]
        public ActionResult UpdateCasePatient(ITS.Domain.Models.PatientModel.Patient patient)
        {
            if (_patientService.UpdatePatientByPatientID(Mapper.Map<ITSService.PatientService.Patient>(patient)) > 0)
                return Json("Successfully Updated", GlobalConst.ContentTypes.TextHTML);
            return Json("Error occor please try again", GlobalConst.ContentTypes.TextHTML);
        }
        public ActionResult CaseUpload(int id)
        {
            CaseSearchDetailViewModel ObjCaseDocuments = new CaseSearchDetailViewModel();
            var _caseDocUser = Mapper.Map<IEnumerable<ITS.Domain.Models.CaseSearch.CaseDocumentUser>>(_caseService.GetCaseDocumentUserByCaseID(id));
            ObjCaseDocuments.CaseDocumentUsers = Mapper.Map<IEnumerable<ITS.Domain.Models.CaseSearch.CaseDocumentUser>>(_caseService.GetCaseDocumentUserByCaseID(id));
            ViewBag.CaseID = id;
            return View(ObjCaseDocuments.CaseDocumentUsers);
        }

    }
}