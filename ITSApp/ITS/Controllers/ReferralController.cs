using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using ITS.Infrastructure.Base;
using ITS.Infrastructure.Global;
using ITS.Domain.ViewModels.InternalTasksViewModel;
using ITS.Domain.Models.CaseAssessmentModel;
using ITS.Domain.Models.CaseModel;
using ITS.Infrastructure.ApplicationFilters;
using System.Configuration;
using ITS.Domain.Models.UserModel;
using ITS.Domain.Models.SupplierModel;
using System.IO;
using ITS.Infrastructure.ApplicationServices;
using System.Net.Mail;
using System.Net;
using ITS.Domain.Models.CaseSearch;

namespace ITSWebApp.Controllers
{
    [AuthorizedUserCheck]
    [ValidSessionCheck]
    public class ReferralController : BaseController
    {
        private readonly ITSService.CaseService.ICaseService _caseService;
        private readonly ITSService.SupplierService.ISupplierService _supplierService;
        private readonly ITSService.PatientService.IPatientService _patientService;
        private readonly ITS.Infrastructure.ApplicationServices.Contracts.IReferrerStorage _referrerStorage;
        private readonly EmailService _emailService;

        public ReferralController(EmailService emailService, ITSService.CaseService.ICaseService caseService, ITSService.SupplierService.ISupplierService supplierService, ITSService.PatientService.IPatientService patientService,
            ITS.Infrastructure.ApplicationServices.Contracts.IReferrerStorage referrerStorage)
        {
            _emailService = emailService;
            _caseService = caseService;
            _patientService = patientService;
            _supplierService = supplierService;
            _referrerStorage = referrerStorage;

        }
        public ActionResult Index()
        {

            return View(GetReferralCases((int)GlobalConst.DefaultPageSizeValues.Skip, (int)GlobalConst.DefaultPageSizeValues.Take));
        }




        //Candidate for optimisation  -  var caseSpecialInstruction = _caseService.GetCaseByCaseID(id).CaseSpecialInstruction;
        public ActionResult AcceptAndReferTriageAssessment(int id)
        {
            CasePatientTreatment caseTreatment = Mapper.Map<CasePatientTreatment>(_patientService.GetPatientAndCaseByCaseID(id));
            Case caseObj = Mapper.Map<Case>(_caseService.GetCaseByCaseID(id));
            var caseSpecialInstruction = caseObj.CaseSpecialInstruction;
            caseTreatment.ReferralFileDownloadPath = Url.Action(GlobalConst.Actions.FileController.ViewReferral, GlobalConst.Controllers.File, new { caseID = id });
            caseTreatment.CaseID = id;
            IEnumerable<SupplierSupplierTreatmentsAndSupplierTreatmenPricing> supplierTreatmentsAndSupplierTreatmenPricing = Mapper.Map<IEnumerable<SupplierSupplierTreatmentsAndSupplierTreatmenPricing>>(_supplierService.GetTriageTopSuppliersTreamentPricingByTreatmentCategoryID(caseTreatment.TreatmentCategoryID));

            if (supplierTreatmentsAndSupplierTreatmenPricing == null)
                supplierTreatmentsAndSupplierTreatmenPricing = new List<SupplierSupplierTreatmentsAndSupplierTreatmenPricing>();
            else
                supplierTreatmentsAndSupplierTreatmenPricing = supplierTreatmentsAndSupplierTreatmenPricing.OrderBy(supplier => supplier.Ranking);

            var viewModel = new PatientCaseAndSupplierWithPatientPostCodeViewModel
            {
                CaseSpecialInstruction = caseSpecialInstruction,
                CasePatientTreatment = caseTreatment,
                TopTriageSupplierSupplierTreatmentsAndSupplierTreatmenPricing = supplierTreatmentsAndSupplierTreatmenPricing,
                IsFileExist = _referrerStorage.ReferralFileExists(caseObj.ReferrerID, caseObj.ReferrerProjectTreatmentID, id, caseObj.CaseNumber + GlobalConst.FileExtensions.PDF.ToString(), ConfigurationManager.AppSettings["StoragePath"])
            };

            viewModel.AllTriageSupplierSupplierTreatmentsAndSupplierTreatmenPricing = Mapper.Map<IEnumerable<SupplierSupplierTreatmentsAndSupplierTreatmenPricing>>(_supplierService.GetTriageSuppliersTreamentPricingByTreatmentCategoryID(viewModel.CasePatientTreatment.TreatmentCategoryID));

            return View(viewModel);
        }

        //Candidate for optimisation  -  var caseSpecialInstruction = _caseService.GetCaseByCaseID(id).CaseSpecialInstruction;
        public ActionResult AcceptAndRefer(int id)
        {
            CasePatientTreatment caseTreatment = Mapper.Map<CasePatientTreatment>(_patientService.GetPatientAndCaseByCaseID(id));      
            Case caseObj = Mapper.Map<Case>(_caseService.GetCaseByCaseID(id));
            var caseSpecialInstruction = caseObj.CaseSpecialInstruction;
            caseTreatment.ReferralFileDownloadPath = Url.Action(GlobalConst.Actions.FileController.ViewReferral, GlobalConst.Controllers.File, new { caseID = id });

            caseTreatment.CaseID = id;
            IEnumerable<SupplierDistanceRankPrice> supplierDistanceRankPrice = Mapper.Map<IEnumerable<SupplierDistanceRankPrice>>(_supplierService.GetSupplierWithinPostCode(caseTreatment.PostCode, 10, caseTreatment.TreatmentCategoryID));

            if (supplierDistanceRankPrice == null)
                supplierDistanceRankPrice = new List<SupplierDistanceRankPrice>();
            else
                supplierDistanceRankPrice = supplierDistanceRankPrice.OrderBy(supplier => supplier.Distance).ThenByDescending(supplier => supplier.Ranking);

            var viewModel = new PatientCaseAndSupplierWithPatientPostCodeViewModel()
            {
                CaseSpecialInstruction = caseSpecialInstruction,
                CasePatientReferrerSupplierWorkflow = Mapper.Map<CasePatientReferrerSupplierWorkflow>(_caseService.GetCasePatientReferrerSupplierWorkflowByCaseID(id)),               
                CasePatientTreatment = caseTreatment,
                SupplierDistanceRankPrice = supplierDistanceRankPrice,
                IsFileExist = _referrerStorage.ReferralFileExists(caseObj.ReferrerID, caseObj.ReferrerProjectTreatmentID, id, caseObj.CaseNumber + GlobalConst.FileExtensions.PDF.ToString(), ConfigurationManager.AppSettings["StoragePath"])
            };


            viewModel.AllSupplierWithinPostCode = Mapper.Map<IEnumerable<SuppliersName>>(_supplierService.GetAllSupplierWithinPostCode(caseTreatment.PostCode, 1000, caseTreatment.TreatmentCategoryID));
          

            return View(viewModel);
        }
        [HttpPost]
        public ActionResult SelectedSupplierDistanceRankPrice(int supplierID, int treatmentCategoryID, string postCode)
        {
            SupplierDistanceRankPrice supplierDistanceRankPrice = Mapper.Map<SupplierDistanceRankPrice>(_supplierService.GetSelectedSupplierDistanceRankPrice(supplierID, treatmentCategoryID, postCode));
            return Json(supplierDistanceRankPrice);
        }

        #region ReferralReceived Post Method Section
        [HttpPost]
        public ActionResult ReferralReceived(int treatmentCategoryID, int skip, int take)
        {
            PagedCaseWorkflowPatientReferrerProject viewModel;

            if (treatmentCategoryID == (int)GlobalConst.TreatmentCategoryValues.All)
            {
                viewModel = GetReferralCases(skip, take);
            }
            else
            {
                viewModel = GetReferralCasesByTreatmentCategoryID(treatmentCategoryID, skip, take);
            }
            return Json(viewModel, GlobalConst.ContentTypes.TextHTML);
        }
        [NonAction]
        public PagedCaseWorkflowPatientReferrerProject GetReferralCases(int skip, int take)
        {
            var pagedCaseWorkflowPatientReferrerProject = Mapper.Map<PagedCaseWorkflowPatientReferrerProject>(_caseService.GetReferralCases(skip, take));

            foreach (var caseObj in pagedCaseWorkflowPatientReferrerProject.CaseWorkflowPatientReferrerProjects)
            {
                caseObj.IsFileExist = _referrerStorage.ReferralFileExists(caseObj.ReferrerID, caseObj.ReferrerProjectTreatmentID, caseObj.CaseID, caseObj.CaseNumber + GlobalConst.FileExtensions.PDF.ToString(), ConfigurationManager.AppSettings["StoragePath"]);
                if (caseObj.IsTriage && !caseObj.IsTreatmentRequired)
                    caseObj.ActionUrl = Url.Action(GlobalConst.Actions.ReferralController.AcceptAndReferTriageAssessment, GlobalConst.Controllers.Referral, new { id = caseObj.CaseID });
                else
                    caseObj.ActionUrl = Url.Action(GlobalConst.Actions.ReferralController.AcceptAndRefer, GlobalConst.Controllers.Referral, new { id = caseObj.CaseID });
                caseObj.ReferralDownloadPath = Url.Action(GlobalConst.Actions.FileController.ViewReferral,
                                                          GlobalConst.Controllers.File, new { area = "", caseObj.CaseID });
            }
            pagedCaseWorkflowPatientReferrerProject.TreatmentCategoryID = (int)GlobalConst.TreatmentCategoryValues.All;
       

            return pagedCaseWorkflowPatientReferrerProject;
        }

        [NonAction]
        public PagedCaseWorkflowPatientReferrerProject GetReferralCasesByTreatmentCategoryID(int treatmentCategoryID, int skip, int take)
        {
            PagedCaseWorkflowPatientReferrerProject caseWorkflowPatientReferrerProjectsForRefReceived = Mapper.Map<PagedCaseWorkflowPatientReferrerProject>(_caseService.GetReferralCasesByTreatmentCategoryID(treatmentCategoryID, skip, take));
            foreach (var caseObj in caseWorkflowPatientReferrerProjectsForRefReceived.CaseWorkflowPatientReferrerProjects)
            {
                caseObj.IsFileExist = _referrerStorage.ReferralFileExists(caseObj.ReferrerID, caseObj.ReferrerProjectTreatmentID, caseObj.CaseID, caseObj.CaseNumber + GlobalConst.FileExtensions.PDF.ToString(), ConfigurationManager.AppSettings["StoragePath"]);
                if (caseObj.IsTriage)
                    caseObj.ActionUrl = Url.Action(GlobalConst.Actions.ReferralController.AcceptAndReferTriageAssessment, GlobalConst.Controllers.Referral, new { id = caseObj.CaseID });
                else
                    caseObj.ActionUrl = Url.Action(GlobalConst.Actions.ReferralController.AcceptAndRefer, GlobalConst.Controllers.Referral, new { id = caseObj.CaseID });
                caseObj.ReferralDownloadPath = Url.Action(GlobalConst.Actions.FileController.ViewReferral,
                                                          GlobalConst.Controllers.File, new { area = "", caseObj.CaseID });
            }
            caseWorkflowPatientReferrerProjectsForRefReceived.TreatmentCategoryID = treatmentCategoryID;
            return caseWorkflowPatientReferrerProjectsForRefReceived;
        }

        [HttpPost]
        public JsonResult CaseSubmitToSupplier(int caseID, int supplierID, string MessageToSupplier)
        {
            if (_caseService.UpdateCaseSupplier(caseID, supplierID) > 0)
            {
                if (_caseService.UpdateCaseWorkFlow(caseID, ITSUser.UserID))
                {
                    if (MessageToSupplier != "")
                    {
                        CaseNote objCaseNote = new CaseNote();
                        objCaseNote.CaseID = caseID;
                        objCaseNote.Note = MessageToSupplier;
                        objCaseNote.UserID = ITSUser.UserID;
                        objCaseNote.WorkflowID = 20;
                        _caseService.AddCaseNote(Mapper.Map<ITSService.CaseService.CaseNote>(objCaseNote));
                    }
                    int DocumentSetupTypeID = _caseService.GetReferrerProjectTreatmentDocumentSetup(caseID, GlobalConst.AssessmentService.InitialAssessment);
                    var _caseTreatment = _patientService.GetPatientAndCaseByCaseID(caseID);
                    var _supplierModel = _supplierService.GetSupplierBySupplierID(supplierID);
                    string FilePath = Server.MapPath(Request.ApplicationPath + "/Storage/Template/EmailTemplate.html");
                    System.IO.StreamReader str = new StreamReader(FilePath);
                    string MailText = str.ReadToEnd();
                    str.Close();
                    string _patientName = _caseTreatment.FirstName + " " + _caseTreatment.LastName;
                    MailText = MailText.Replace("##SupplierName##", _supplierModel.SupplierName);
                    MailText = MailText.Replace("##ReferenceNumber##", _caseTreatment.CaseReferrerReferenceNumber);
                    MailText = MailText.Replace("##PatientName##", _patientName);
                    MailText = MailText.Replace("##TreatmentType##", _caseTreatment.TreatmentCategoryName);
                    MailText = MailText.Replace("##LogoPath##", System.Configuration.ConfigurationManager.AppSettings[GlobalConst.Message.ReSetUrl] + GlobalConst.Message.EmailLogoPath);
                    MailText = MailText.Replace("##EmailLogo2Path##", System.Configuration.ConfigurationManager.AppSettings[GlobalConst.Message.ReSetUrl] + GlobalConst.Message.EmailLogo2Path);
                    MailText = MailText.Replace("##EmailLogo3Path##", System.Configuration.ConfigurationManager.AppSettings[GlobalConst.Message.ReSetUrl] + GlobalConst.Message.EmailLogo3Path);
                    bool result = _emailService.SendGeneralEmail(_supplierModel.Email, System.Configuration.ConfigurationManager.AppSettings["SupportInnovateHMGEmail"].ToString(), "Referrals sent to Supplier", MailText);


                    return Json(GlobalResource.UpdatedSuccessfully, GlobalConst.ContentTypes.TextHTML);
                }
            }
            return Json("Error while updating suppler to case", GlobalConst.ContentTypes.TextHTML);
        }
        [HttpPost]
        public JsonResult CaseSubmitToSupplierChange(int caseID, int supplierID)
        {
            if (_caseService.UpdateCaseSupplier(caseID, supplierID) > 0)
            {
                    return Json(GlobalResource.UpdatedSuccessfully, GlobalConst.ContentTypes.TextHTML);
            }
            return Json("Error while updating suppler to case", GlobalConst.ContentTypes.TextHTML);
        }
        #endregion

    }
}


