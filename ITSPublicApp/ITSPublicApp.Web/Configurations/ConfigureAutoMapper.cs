using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ITSPublicApp.Infrastructure.ApplicationServices.Contracts;
using AutoMapper;

namespace ITSPublicApp.Web.Configurations
{
    public class ConfigureAutoMapper : ITask
    {
        public void Run()
        {
            Mapper.CreateMap<ITSService.UserService.User, ITSPublicApp.Domain.Models.ITSUser>();
            Mapper.CreateMap<ITSPublicApp.Domain.Models.ITSUser, ITSService.UserService.User>();

            Mapper.CreateMap<ITSService.ReferrerService.ReferrerProject, ITSPublicApp.Domain.Models.ReferrerProject>();
            Mapper.CreateMap<ITSPublicApp.Domain.Models.ReferrerProject, ITSService.ReferrerService.ReferrerProject>();

            Mapper.CreateMap<ITSService.ReferrerService.ReferrerAssignedUser, ITSPublicApp.Domain.Models.ReferrerAssignedUser>();
            Mapper.CreateMap<ITSPublicApp.Domain.Models.ReferrerAssignedUser, ITSService.ReferrerService.ReferrerAssignedUser>();

            Mapper.CreateMap<ITSPublicApp.Domain.Models.TreatmentPeriodType, ITSService.LookUpService.TreatmentPeriodType>();
            Mapper.CreateMap<ITSService.LookUpService.TreatmentPeriodType, ITSPublicApp.Domain.Models.TreatmentPeriodType>(); 

            Mapper.CreateMap<ITSService.ReferrerService.ReferrerProjectTreatmentName, ITSPublicApp.Domain.Models.ReferrerProjectTreatmentName>();
            Mapper.CreateMap<ITSPublicApp.Domain.Models.ReferrerProjectTreatmentName, ITSService.ReferrerService.ReferrerProjectTreatmentName>();




            Mapper.CreateMap<ITSService.ReferrerService.ReferrerLocation, ITSPublicApp.Domain.Models.ReferrerLocation>();
            Mapper.CreateMap<ITSPublicApp.Domain.Models.ReferrerLocation, ITSService.ReferrerService.ReferrerLocation>();

            Mapper.CreateMap<ITSService.CaseService.Case, ITSPublicApp.Domain.Models.Case>();
            Mapper.CreateMap<ITSPublicApp.Domain.Models.Case, ITSService.CaseService.Case>();

            Mapper.CreateMap<ITSPublicApp.Domain.Models.Patient, ITSService.PatientService.Patient>();
            Mapper.CreateMap<ITSService.PatientService.Patient, ITSPublicApp.Domain.Models.Patient>();

            Mapper.CreateMap<ITSPublicApp.Domain.Models.Patient, ITSService.CaseService.Patient>();
            Mapper.CreateMap<ITSService.CaseService.Patient, ITSPublicApp.Domain.Models.Patient>();

            Mapper.CreateMap<ITSService.CaseService.Solicitor, ITSPublicApp.Domain.Models.Solicitor>();
            Mapper.CreateMap<ITSPublicApp.Domain.Models.Solicitor, ITSService.CaseService.Solicitor>();



            Mapper.CreateMap<ITSService.SupplierService.TreatmentCategoriesTreatmentType, ITSPublicApp.Domain.Models.TreatmentCategoriesTreatmentType>();
            Mapper.CreateMap<ITSPublicApp.Domain.Models.TreatmentCategoriesTreatmentType, ITSService.SupplierService.TreatmentCategoriesTreatmentType>();

            Mapper.CreateMap<ITSService.CaseService.CaseContact, ITSPublicApp.Domain.Models.CaseContact>();
            Mapper.CreateMap<ITSPublicApp.Domain.Models.CaseContact, ITSService.CaseService.CaseContact>();

            Mapper.CreateMap<ITSService.SupplierService.TreatmentType, ITSPublicApp.Domain.Models.TreatmentType>();
            Mapper.CreateMap<ITSPublicApp.Domain.Models.TreatmentType, ITSService.SupplierService.TreatmentType>();

            Mapper.CreateMap<ITSPublicApp.Domain.Models.ReferrerAuthorisations, ITSService.CaseService.ReferrerAuthorisations>();
            Mapper.CreateMap<ITSService.CaseService.ReferrerAuthorisations, ITSPublicApp.Domain.Models.ReferrerAuthorisations>();

            Mapper.CreateMap<ITSPublicApp.Domain.ViewModels.PagedReferrerAuthorisations, ITSService.CaseService.PagedReferrerAuthorisations>();
            Mapper.CreateMap<ITSService.CaseService.PagedReferrerAuthorisations, ITSPublicApp.Domain.ViewModels.PagedReferrerAuthorisations>();

            Mapper.CreateMap<ITSService.SupplierService.SupplierCasePatient, ITSPublicApp.Domain.Models.SupplierCasePatient>();
            Mapper.CreateMap<ITSPublicApp.Domain.Models.SupplierCasePatient, ITSService.SupplierService.SupplierCasePatient>();

            Mapper.CreateMap<ITSService.ReferrerService.ReferrerProjectTreatmentTreatmentType, ITSPublicApp.Domain.Models.ReferrerProjectTreatmentTreatmentType>();
            Mapper.CreateMap<ITSPublicApp.Domain.Models.ReferrerProjectTreatmentTreatmentType, ITSService.ReferrerService.ReferrerProjectTreatmentTreatmentType>();

            Mapper.CreateMap<ITSPublicApp.Domain.Models.CasePatient, ITSService.CaseService.CasePatient>();
            Mapper.CreateMap<ITSService.CaseService.CasePatient, ITSPublicApp.Domain.Models.CasePatient>();

            Mapper.CreateMap<ITSPublicApp.Domain.Models.CaseAppointmentDate, ITSService.CaseService.CaseAppointmentDate>();
            Mapper.CreateMap<ITSService.CaseService.CaseAppointmentDate, ITSPublicApp.Domain.Models.CaseAppointmentDate>();

            Mapper.CreateMap<ITSPublicApp.Domain.Models.CasePatientContactAttempt, ITSService.CaseService.CasePatientContactAttempt>();
            Mapper.CreateMap<ITSService.CaseService.CasePatientContactAttempt, ITSPublicApp.Domain.Models.CasePatientContactAttempt>();

            Mapper.CreateMap<ITSPublicApp.Domain.Models.PatientWorkstatus, ITSService.AssessmentService.PatientWorkstatus>();
            Mapper.CreateMap<ITSService.AssessmentService.PatientWorkstatus, ITSPublicApp.Domain.Models.PatientWorkstatus>();

            Mapper.CreateMap<ITSPublicApp.Domain.Models.PatientImpact, ITSService.AssessmentService.PatientImpact>();
            Mapper.CreateMap<ITSService.AssessmentService.PatientImpact, ITSPublicApp.Domain.Models.PatientImpact>();

            Mapper.CreateMap<ITSPublicApp.Domain.Models.PatientImpactValue, ITSService.AssessmentService.PatientImpactValue>();
            Mapper.CreateMap<ITSService.AssessmentService.PatientImpactValue, ITSPublicApp.Domain.Models.PatientImpactValue>();

            Mapper.CreateMap<ITSPublicApp.Domain.Models.PsychologicalFactor, ITSService.AssessmentService.PsychologicalFactor>();
            Mapper.CreateMap<ITSService.AssessmentService.PsychologicalFactor, ITSPublicApp.Domain.Models.PsychologicalFactor>();

            Mapper.CreateMap<ITSPublicApp.Domain.Models.ReferrerSupplierCases, ITSService.CaseService.ReferrerSupplierCases>();
            Mapper.CreateMap<ITSService.CaseService.ReferrerSupplierCases, ITSPublicApp.Domain.Models.ReferrerSupplierCases>();

            Mapper.CreateMap<ITSPublicApp.Domain.Models.ReferrerSupplierCases, ITSService.ReferrerService.ReferrerSupplierCases>();
            Mapper.CreateMap<ITSService.ReferrerService.ReferrerSupplierCases, ITSPublicApp.Domain.Models.ReferrerSupplierCases>();

            Mapper.CreateMap<ITSPublicApp.Domain.Models.PatientImpactOnWork, ITSService.AssessmentService.PatientImpactOnWork>();
            Mapper.CreateMap<ITSService.AssessmentService.PatientImpactOnWork, ITSPublicApp.Domain.Models.PatientImpactOnWork>();

            Mapper.CreateMap<ITSPublicApp.Domain.Models.PatientLevelOfRecovery, ITSService.AssessmentService.PatientLevelOfRecovery>();
            Mapper.CreateMap<ITSService.AssessmentService.PatientLevelOfRecovery, ITSPublicApp.Domain.Models.PatientLevelOfRecovery>();

            Mapper.CreateMap<ITSPublicApp.Domain.Models.ProposedTreatmentMethod, ITSService.AssessmentService.ProposedTreatmentMethod>();
            Mapper.CreateMap<ITSService.AssessmentService.ProposedTreatmentMethod, ITSPublicApp.Domain.Models.ProposedTreatmentMethod>();

            Mapper.CreateMap<ITSPublicApp.Domain.Models.CasePatientTreatment, ITSService.PatientService.CasePatientTreatment>();
            Mapper.CreateMap<ITSService.PatientService.CasePatientTreatment, ITSPublicApp.Domain.Models.CasePatientTreatment>();

            Mapper.CreateMap<ITSPublicApp.Domain.Models.Practitioner, ITSService.PractitionerService.PracitionerSupplierTreatmentCategory>();
            Mapper.CreateMap<ITSService.PractitionerService.PracitionerSupplierTreatmentCategory, ITSPublicApp.Domain.Models.Practitioner>();

            Mapper.CreateMap<ITSPublicApp.Domain.Models.CaseAssessment, ITSService.AssessmentService.CaseAssessment>()
                .ForMember(assessment => assessment.CaseAssessmentProposedTreatmentMethods, opt => opt.MapFrom(src => src.ProposedTreatmentMethodIDs != null ? src.ProposedTreatmentMethodIDs.Select(proposedMethod => new ITSService.AssessmentService.CaseAssessmentProposedTreatmentMethod() { CaseID = src.CaseID, ProposedTreatmentMethodID = proposedMethod }) : new List<ITSService.AssessmentService.CaseAssessmentProposedTreatmentMethod>()));
            Mapper.CreateMap<ITSService.AssessmentService.CaseAssessment, ITSPublicApp.Domain.Models.CaseAssessment>()
                .ForMember(assessment => assessment.ProposedTreatmentMethodIDs, opt => opt.MapFrom(src => src.CaseAssessmentProposedTreatmentMethods.Select(proposedMethod => proposedMethod.ProposedTreatmentMethodID)));


            Mapper.CreateMap<ITSPublicApp.Domain.Models.CaseAssessmentPatientImpact, ITSService.AssessmentService.CaseAssessmentPatientImpact>();
            Mapper.CreateMap<ITSService.AssessmentService.CaseAssessmentPatientImpact, ITSPublicApp.Domain.Models.CaseAssessmentPatientImpact>();

            Mapper.CreateMap<ITSPublicApp.Domain.Models.CaseAssessmentPatientInjury, ITSService.AssessmentService.CaseAssessmentPatientInjury>();
            Mapper.CreateMap<ITSService.AssessmentService.CaseAssessmentPatientInjury, ITSPublicApp.Domain.Models.CaseAssessmentPatientInjury>();


           
            Mapper.CreateMap<ITSPublicApp.Domain.Models.CaseTreatmentPricingType, ITSService.CaseService.CaseTreatmentPricingType>();
            Mapper.CreateMap<ITSService.CaseService.CaseTreatmentPricingType, ITSPublicApp.Domain.Models.CaseTreatmentPricingType>();

            Mapper.CreateMap<ITSPublicApp.Domain.Models.PricingTypes, ITSService.ReferrerService.PricingType>();
            Mapper.CreateMap<ITSService.ReferrerService.PricingType, ITSPublicApp.Domain.Models.PricingTypes>();


            Mapper.CreateMap<ITSPublicApp.Domain.Models.TreatmentCategoriesBespokeService, ITSService.AssessmentService.TreatmentCategoriesBespokeService>();
            Mapper.CreateMap<ITSService.AssessmentService.TreatmentCategoriesBespokeService, ITSPublicApp.Domain.Models.TreatmentCategoriesBespokeService>();

            Mapper.CreateMap<ITSService.CaseService.TreatmentReferrerSupplierPricing, ITSPublicApp.Domain.Models.TreatmentReferrerSupplierPricing>();
            Mapper.CreateMap<ITSPublicApp.Domain.Models.TreatmentReferrerSupplierPricing, ITSService.CaseService.TreatmentReferrerSupplierPricing>();

            Mapper.CreateMap<ITSPublicApp.Domain.Models.CaseTreatmentPricing, ITSService.CaseService.CaseTreatmentPricing>();
            Mapper.CreateMap<ITSService.CaseService.CaseTreatmentPricing, ITSPublicApp.Domain.Models.CaseTreatmentPricing>();

            Mapper.CreateMap<ITSPublicApp.Domain.Models.CaseBespokeServicePricing, ITSService.CaseService.CaseBespokeServicePricing>();
            Mapper.CreateMap<ITSService.CaseService.CaseBespokeServicePricing, ITSPublicApp.Domain.Models.CaseBespokeServicePricing>();

            Mapper.CreateMap<ITSPublicApp.Domain.Models.CaseAssessmentRating, ITSService.AssessmentService.CaseAssessmentRating>();
            Mapper.CreateMap<ITSService.AssessmentService.CaseAssessmentRating, ITSPublicApp.Domain.Models.CaseAssessmentRating>();

            Mapper.CreateMap<ITSPublicApp.Domain.Models.CaseAssessmentDetail, ITSService.AssessmentService.CaseAssessmentDetail>();
            Mapper.CreateMap<ITSService.AssessmentService.CaseAssessmentDetail, ITSPublicApp.Domain.Models.CaseAssessmentDetail>();

            // Mapper for Updates in initiall Assessment Screen
            Mapper.CreateMap<ITSPublicApp.Domain.Models.PatientRole, ITSService.LookUpService.PatientRole>();
            Mapper.CreateMap<ITSService.LookUpService.PatientRole, ITSPublicApp.Domain.Models.PatientRole>();

            Mapper.CreateMap<ITSPublicApp.Domain.Models.Duration, ITSService.LookUpService.Duration>();
            Mapper.CreateMap<ITSService.LookUpService.Duration, ITSPublicApp.Domain.Models.Duration>();

            Mapper.CreateMap<ITSPublicApp.Domain.Models.CaseBespokeServicePricingType, ITSService.CaseService.CaseBespokeServicePricingType>();
            Mapper.CreateMap<ITSService.CaseService.CaseBespokeServicePricingType, ITSPublicApp.Domain.Models.CaseBespokeServicePricingType>();

            Mapper.CreateMap<ITSPublicApp.Domain.Models.ReferrerSupplierCases, ITSService.CaseService.PagedReferrerSupplierCaseSearch>();
            Mapper.CreateMap<ITSService.CaseService.PagedReferrerSupplierCaseSearch, ITSPublicApp.Domain.Models.ReferrerSupplierCases>();

            Mapper.CreateMap<ITSPublicApp.Domain.Models.ReferrerDocuments, ITSService.ReferrerService.ReferrerDocument>();
            Mapper.CreateMap<ITSService.ReferrerService.ReferrerDocument, ITSPublicApp.Domain.Models.ReferrerDocuments>();

            Mapper.CreateMap<ITSPublicApp.Domain.Models.ReferrerDocumentType, ITSService.ReferrerService.ReferrerDocumentType>();
            Mapper.CreateMap<ITSService.ReferrerService.ReferrerDocumentType, ITSPublicApp.Domain.Models.ReferrerDocumentType>();

            Mapper.CreateMap<ITSPublicApp.Domain.Models.SupplierDocument, ITSService.SupplierService.SupplierDocument>();
            Mapper.CreateMap<ITSService.SupplierService.SupplierDocument, ITSPublicApp.Domain.Models.SupplierDocument>();

            Mapper.CreateMap<ITSPublicApp.Domain.Models.CaseAssessmentCustom, ITSService.AssessmentService.CaseAssessmentCustom>();
            Mapper.CreateMap<ITSService.AssessmentService.CaseAssessmentCustom, ITSPublicApp.Domain.Models.CaseAssessmentCustom>();

            Mapper.CreateMap<ITSPublicApp.Domain.Models.InjuredPartyRepresentative, ITSService.ReferrerService.InjuredPartyRepresentative>();
            Mapper.CreateMap<ITSService.ReferrerService.InjuredPartyRepresentative, ITSPublicApp.Domain.Models.InjuredPartyRepresentative>();

            Mapper.CreateMap<ITSPublicApp.Domain.Models.InjuredRepresentativeOption, ITSService.ReferrerService.InjuredRepresentativeOption>();
            Mapper.CreateMap<ITSService.ReferrerService.InjuredRepresentativeOption, ITSPublicApp.Domain.Models.InjuredRepresentativeOption>();

            Mapper.CreateMap<ITSPublicApp.Domain.Models.InjuredPartyRepresentative, ITSService.CaseService.InjuredPartyRepresentative>();
            Mapper.CreateMap<ITSService.CaseService.InjuredPartyRepresentative, ITSPublicApp.Domain.Models.InjuredPartyRepresentative>();

            Mapper.CreateMap<ITSPublicApp.Domain.Models.ReferrerCaseAssessmentModification, ITSService.AssessmentService.ReferrerCaseAssessmentModification>();
            Mapper.CreateMap<ITSService.AssessmentService.ReferrerCaseAssessmentModification, ITSPublicApp.Domain.Models.ReferrerCaseAssessmentModification>();

            Mapper.CreateMap<ITSPublicApp.Domain.Models.ReferrerCaseAssessmentModificationAuthority, ITSService.AssessmentService.ReferrerCaseAssessmentModificationAuthority>();
            Mapper.CreateMap<ITSService.AssessmentService.ReferrerCaseAssessmentModificationAuthority, ITSPublicApp.Domain.Models.ReferrerCaseAssessmentModificationAuthority>();

            Mapper.CreateMap<ITSPublicApp.Domain.Models.EPNATreatmentSession, ITSService.CaseService.EPNATreatmentSession>();
            Mapper.CreateMap<ITSService.CaseService.EPNATreatmentSession, ITSPublicApp.Domain.Models.EPNATreatmentSession>();

            Mapper.CreateMap<ITSPublicApp.Domain.Models.CaseDocumentUser, ITSService.CaseService.CaseDocumentUser>();
            Mapper.CreateMap<ITSService.CaseService.CaseDocumentUser, ITSPublicApp.Domain.Models.CaseDocumentUser>();

            Mapper.CreateMap<ITSPublicApp.Domain.Models.AffectedArea, ITSService.LookUpService.AffectedArea>();
            Mapper.CreateMap<ITSService.LookUpService.AffectedArea, ITSPublicApp.Domain.Models.AffectedArea>();

            Mapper.CreateMap<ITSPublicApp.Domain.Models.RestrictionRange, ITSService.LookUpService.RestrictionRange>();
            Mapper.CreateMap<ITSService.LookUpService.RestrictionRange, ITSPublicApp.Domain.Models.RestrictionRange>();

            Mapper.CreateMap<ITSPublicApp.Domain.Models.StrengthTesting, ITSService.LookUpService.StrengthTesting>();
            Mapper.CreateMap<ITSService.LookUpService.StrengthTesting, ITSPublicApp.Domain.Models.StrengthTesting>();

            Mapper.CreateMap<ITSPublicApp.Domain.Models.SymptomDescription, ITSService.LookUpService.SymptomDescription>();
            Mapper.CreateMap<ITSService.LookUpService.SymptomDescription, ITSPublicApp.Domain.Models.SymptomDescription>();

            Mapper.CreateMap<ITSPublicApp.Domain.Models.CaseAssessmentPatientInjuryBL, ITSService.AssessmentService.CaseAssessmentPatientInjuryBL>();
            Mapper.CreateMap<ITSService.AssessmentService.CaseAssessmentPatientInjuryBL, ITSPublicApp.Domain.Models.CaseAssessmentPatientInjuryBL>();

            Mapper.CreateMap<ITSPublicApp.Domain.Models.IntialAssessmentReportDetail, ITSService.CaseService.IntialAssessmentReportDetail>();
            Mapper.CreateMap<ITSService.CaseService.IntialAssessmentReportDetail, ITSPublicApp.Domain.Models.IntialAssessmentReportDetail>();

            Mapper.CreateMap<ITSService.ReferrerService.Referrer, ITSPublicApp.Domain.Models.Referrer>();
            Mapper.CreateMap<ITSPublicApp.Domain.Models.Referrer, ITSService.ReferrerService.Referrer>();


            Mapper.CreateMap<ITSPublicApp.Domain.Models.Admitted, ITSService.LookUpService.Admitted>();
            Mapper.CreateMap<ITSService.LookUpService.Admitted, ITSPublicApp.Domain.Models.Admitted>();

            Mapper.CreateMap<ITSPublicApp.Domain.Models.Employment, ITSService.CaseService.Employment>();
            Mapper.CreateMap<ITSService.CaseService.Employment, ITSPublicApp.Domain.Models.Employment>();

            Mapper.CreateMap<ITSPublicApp.Domain.Models.EmploymentType, ITSService.LookUpService.EmploymentType>();
            Mapper.CreateMap<ITSService.LookUpService.EmploymentType, ITSPublicApp.Domain.Models.EmploymentType>();

            Mapper.CreateMap<ITSPublicApp.Domain.Models.FitForWork, ITSService.LookUpService.FitForWork>();
            Mapper.CreateMap<ITSService.LookUpService.FitForWork, ITSPublicApp.Domain.Models.FitForWork>();

            Mapper.CreateMap<ITSPublicApp.Domain.Models.Policie, ITSService.CaseService.Policie>();
            Mapper.CreateMap<ITSService.CaseService.Policie, ITSPublicApp.Domain.Models.Policie>();

            Mapper.CreateMap<ITSPublicApp.Domain.Models.PolicyCriteria, ITSService.LookUpService.PolicyCriteria>();
            Mapper.CreateMap<ITSService.LookUpService.PolicyCriteria, ITSPublicApp.Domain.Models.PolicyCriteria>();

            Mapper.CreateMap<ITSPublicApp.Domain.Models.PolicyType, ITSService.LookUpService.PolicyType>();
            Mapper.CreateMap<ITSService.LookUpService.PolicyType, ITSPublicApp.Domain.Models.PolicyType>();

            Mapper.CreateMap<ITSPublicApp.Domain.Models.TypeCover, ITSService.LookUpService.TypeCover>();
            Mapper.CreateMap<ITSService.LookUpService.TypeCover, ITSPublicApp.Domain.Models.TypeCover>();

            Mapper.CreateMap<ITSPublicApp.Domain.Models.NotificationBubble, ITSService.CaseService.NotificationBubble>();
            Mapper.CreateMap<ITSService.CaseService.NotificationBubble, ITSPublicApp.Domain.Models.NotificationBubble>();

            Mapper.CreateMap<ITSPublicApp.Domain.Models.EmployeeDetail, ITSService.CaseService.EmployeeDetail>();
            Mapper.CreateMap<ITSService.CaseService.EmployeeDetail, ITSPublicApp.Domain.Models.EmployeeDetail>();

            Mapper.CreateMap<ITSPublicApp.Domain.Models.WorkType, ITSService.LookUpService.WorkType>();
            Mapper.CreateMap<ITSService.LookUpService.WorkType, ITSPublicApp.Domain.Models.WorkType>();

            Mapper.CreateMap<ITSPublicApp.Domain.Models.RoleType, ITSService.LookUpService.RoleType>();
            Mapper.CreateMap<ITSService.LookUpService.RoleType, ITSPublicApp.Domain.Models.RoleType>();

            Mapper.CreateMap<ITSPublicApp.Domain.Models.Reinsurer, ITSService.LookUpService.Reinsurer>();
            Mapper.CreateMap<ITSService.LookUpService.Reinsurer, ITSPublicApp.Domain.Models.Reinsurer>();

            Mapper.CreateMap<ITSPublicApp.Domain.Models.PrimaryCondition, ITSService.ReferrerService.PrimaryCondition>();
            Mapper.CreateMap<ITSService.ReferrerService.PrimaryCondition, ITSPublicApp.Domain.Models.PrimaryCondition>();

            Mapper.CreateMap<ITSPublicApp.Domain.Models.Gender, ITSService.LookUpService.Gender>();
            Mapper.CreateMap<ITSService.LookUpService.Gender, ITSPublicApp.Domain.Models.Gender>();

            Mapper.CreateMap<ITSPublicApp.Domain.Models.PasswordHistory, ITSService.UserService.PasswordHistory>();
            Mapper.CreateMap<ITSService.UserService.PasswordHistory, ITSPublicApp.Domain.Models.PasswordHistory>();

            Mapper.CreateMap<ITSPublicApp.Domain.Models.AdditionalTest, ITSService.LookUpService.AdditionalTest>();
            Mapper.CreateMap<ITSService.LookUpService.AdditionalTest, ITSPublicApp.Domain.Models.AdditionalTest>();

            Mapper.CreateMap<ITSPublicApp.Domain.Models.ReasonForReferral, ITSService.LookUpService.ReasonForReferral>();
            Mapper.CreateMap<ITSService.LookUpService.ReasonForReferral, ITSPublicApp.Domain.Models.ReasonForReferral>();

            Mapper.CreateMap<ITSPublicApp.Domain.Models.NetworkRailStandardApplicable, ITSService.LookUpService.NetworkRailStandardApplicable>();
            Mapper.CreateMap<ITSService.LookUpService.NetworkRailStandardApplicable, ITSPublicApp.Domain.Models.NetworkRailStandardApplicable>();          

            Mapper.CreateMap<ITSPublicApp.Domain.Models.DrugTest, ITSService.CaseService.DrugTest>();
            Mapper.CreateMap<ITSService.CaseService.DrugTest, ITSPublicApp.Domain.Models.DrugTest>();

            Mapper.CreateMap<ITSPublicApp.Domain.Models.JobDemand, ITSService.CaseService.JobDemand>();
            Mapper.CreateMap<ITSService.CaseService.JobDemand, ITSPublicApp.Domain.Models.JobDemand>();

            Mapper.CreateMap<ITSPublicApp.Domain.Models.CaseReferrerAssignedUser, ITSService.CaseService.CaseReferrerAssignedUser>();
            Mapper.CreateMap<ITSService.CaseService.CaseReferrerAssignedUser, ITSPublicApp.Domain.Models.CaseReferrerAssignedUser>();

            Mapper.CreateMap<ITSPublicApp.Domain.Models.PolicyOpenDetail, ITSService.CaseService.PolicyOpenDetail>();
            Mapper.CreateMap<ITSService.CaseService.PolicyOpenDetail, ITSPublicApp.Domain.Models.PolicyOpenDetail>();

            Mapper.CreateMap<ITSPublicApp.Domain.Models.Gip, ITSService.CaseService.Gip>();
            Mapper.CreateMap<ITSService.CaseService.Gip, ITSPublicApp.Domain.Models.Gip>();

            Mapper.CreateMap<ITSPublicApp.Domain.Models.Iip, ITSService.CaseService.Iip>();
            Mapper.CreateMap<ITSService.CaseService.Iip, ITSPublicApp.Domain.Models.Iip>();

            Mapper.CreateMap<ITSPublicApp.Domain.Models.Tpd, ITSService.CaseService.Tpd>();
            Mapper.CreateMap<ITSService.CaseService.Tpd, ITSPublicApp.Domain.Models.Tpd>();

            Mapper.CreateMap<ITSPublicApp.Domain.Models.ElRehab, ITSService.CaseService.ElRehab>();
            Mapper.CreateMap<ITSService.CaseService.ElRehab, ITSPublicApp.Domain.Models.ElRehab>();


            Mapper.CreateMap<ITSPublicApp.Domain.Models.ReferrerUserDetail, ITSService.ReferrerService.ReferrerUserDetail>();
            Mapper.CreateMap<ITSService.ReferrerService.ReferrerUserDetail, ITSPublicApp.Domain.Models.ReferrerUserDetail>();

            Mapper.CreateMap<ITSPublicApp.Domain.Models.ReferrerGroup, ITSService.ReferrerService.ReferrerGroup>();
            Mapper.CreateMap<ITSService.ReferrerService.ReferrerGroup, ITSPublicApp.Domain.Models.ReferrerGroup>();
        }
    }
}
