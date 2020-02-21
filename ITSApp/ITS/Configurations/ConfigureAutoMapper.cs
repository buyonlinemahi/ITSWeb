using System;
using ITS.Infrastructure.ApplicationServices.Contracts;
using AutoMapper;
using System.Linq;
using System.Collections.Generic;

namespace ITSWebApp.Configurations
{
    public class ConfigureAutoMapper : ITask
    {
        public void Run()
        {
            Mapper.CreateMap<ITS.Domain.Models.CaseSearch.CaseHistoryUser, ITS.Domain.Models.CaseSearch.CaseReportsDetail>();
            Mapper.CreateMap<ITS.Domain.Models.CaseSearch.CaseReportsDetail, ITS.Domain.Models.CaseSearch.CaseHistoryUser>();

            Mapper.CreateMap<ITSService.UserService.User, ITS.Domain.Models.ITSUser>();
            Mapper.CreateMap<ITS.Domain.Models.ITSUser, ITSService.UserService.User>();

            Mapper.CreateMap<ITSService.ReferrerService.Referrer, ITS.Domain.Models.Referrer>();
            Mapper.CreateMap<ITS.Domain.Models.Referrer, ITSService.ReferrerService.Referrer>();

            Mapper.CreateMap<ITS.Domain.Models.ReferrerLocation, ITSService.ReferrerService.ReferrerLocation>();
            Mapper.CreateMap<ITSService.ReferrerService.ReferrerLocation, ITS.Domain.Models.ReferrerLocation>();

            Mapper.CreateMap<ITS.Domain.Models.ReferrerProject, ITSService.ReferrerService.ReferrerProject>();
            Mapper.CreateMap<ITSService.ReferrerService.ReferrerProject, ITS.Domain.Models.ReferrerProject>();

            Mapper.CreateMap<ITS.Domain.Models.ReferrerAssignedUser, ITSService.ReferrerService.ReferrerAssignedUser>();
            Mapper.CreateMap<ITSService.ReferrerService.ReferrerAssignedUser, ITS.Domain.Models.ReferrerAssignedUser>();

            Mapper.CreateMap<ITS.Domain.Models.ReferrerLocation, ITSService.ReferrerService.ReferrerLocation>();
            Mapper.CreateMap<ITSService.ReferrerService.ReferrerLocation, ITS.Domain.Models.ReferrerLocation>();

            Mapper.CreateMap<ITS.Domain.Models.EmploymentType, ITSService.LookUpService.EmploymentType>();
            Mapper.CreateMap<ITSService.LookUpService.EmploymentType, ITS.Domain.Models.EmploymentType>();

            Mapper.CreateMap<ITS.Domain.Models.Employment, ITSService.CaseService.Employment>();
            Mapper.CreateMap<ITSService.CaseService.Employment, ITS.Domain.Models.Employment>();

            Mapper.CreateMap<ITS.Domain.Models.Policie, ITSService.CaseService.Policie>();
            Mapper.CreateMap<ITSService.CaseService.Policie, ITS.Domain.Models.Policie>();

            Mapper.CreateMap<ITS.Domain.Models.PolicyType, ITSService.LookUpService.PolicyType>();
            Mapper.CreateMap<ITSService.LookUpService.PolicyType, ITS.Domain.Models.PolicyType>();

            Mapper.CreateMap<ITS.Domain.Models.TypeCover, ITSService.LookUpService.TypeCover>();
            Mapper.CreateMap<ITSService.LookUpService.TypeCover, ITS.Domain.Models.TypeCover>();

            Mapper.CreateMap<ITS.Domain.Models.PolicyCriteria, ITSService.LookUpService.PolicyCriteria>();
            Mapper.CreateMap<ITSService.LookUpService.PolicyCriteria, ITS.Domain.Models.PolicyCriteria>();

            Mapper.CreateMap<ITS.Domain.Models.FitForWork, ITSService.LookUpService.FitForWork>();
            Mapper.CreateMap<ITSService.LookUpService.FitForWork, ITS.Domain.Models.FitForWork>();

            Mapper.CreateMap<ITS.Domain.Models.Admitted, ITSService.LookUpService.Admitted>();
            Mapper.CreateMap<ITSService.LookUpService.Admitted, ITS.Domain.Models.Admitted>();

            Mapper.CreateMap<ITS.Domain.Models.AdditionalTest, ITSService.LookUpService.AdditionalTest>();
            Mapper.CreateMap<ITSService.LookUpService.AdditionalTest, ITS.Domain.Models.AdditionalTest>();

            Mapper.CreateMap<ITS.Domain.Models.ReasonForReferral, ITSService.LookUpService.ReasonForReferral>();
            Mapper.CreateMap<ITSService.LookUpService.ReasonForReferral, ITS.Domain.Models.ReasonForReferral>();

            Mapper.CreateMap<ITS.Domain.Models.NetworkRailStandardApplicable, ITSService.LookUpService.NetworkRailStandardApplicable>();
            Mapper.CreateMap<ITSService.LookUpService.NetworkRailStandardApplicable, ITS.Domain.Models.NetworkRailStandardApplicable>();

            Mapper.CreateMap<ITS.Domain.Models.ReferrerProjectTreatment, ITSService.ReferrerService.ReferrerProjectTreatment>();
            Mapper.CreateMap<ITSService.ReferrerService.ReferrerProjectTreatment, ITS.Domain.Models.ReferrerProjectTreatment>();

            Mapper.CreateMap<ITS.Domain.Models.InjuredPartyRepresentative, ITSService.ReferrerService.InjuredPartyRepresentative>();
            Mapper.CreateMap<ITSService.ReferrerService.InjuredPartyRepresentative, ITS.Domain.Models.InjuredPartyRepresentative>();

            Mapper.CreateMap<ITS.Domain.Models.InjuredRepresentativeOption, ITSService.ReferrerService.InjuredRepresentativeOption>();
            Mapper.CreateMap<ITSService.ReferrerService.InjuredRepresentativeOption, ITS.Domain.Models.InjuredRepresentativeOption>();

            Mapper.CreateMap<ITS.Domain.Models.InjuredPartyRepresentative, ITSService.CaseService.InjuredPartyRepresentative>();
            Mapper.CreateMap<ITSService.CaseService.InjuredPartyRepresentative, ITS.Domain.Models.InjuredPartyRepresentative>();
            
            Mapper.CreateMap<ITS.Domain.Models.CaseContact, ITSService.CaseService.CaseContact>();
            Mapper.CreateMap<ITSService.CaseService.CaseContact, ITS.Domain.Models.CaseContact>();
            
            Mapper.CreateMap<ITSService.SupplierService.Supplier, ITS.Domain.Models.Supplier>();

            Mapper.CreateMap<ITS.Domain.Models.ProjectTreatmentSLA, ITSService.ReferrerService.ProjectTreatmentSLA>();
            Mapper.CreateMap<ITSService.ReferrerService.ProjectTreatmentSLA, ITS.Domain.Models.ProjectTreatmentSLA>();

            Mapper.CreateMap<ITS.Domain.Models.ReferrerProjectTreatmentPricing, ITSService.ReferrerService.ReferrerProjectTreatmentPricing>().ForMember(dest => dest.Price, opt => opt.MapFrom(src => Convert.ToDecimal(src.Price)));
            Mapper.CreateMap<ITSService.ReferrerService.ReferrerProjectTreatmentPricing, ITS.Domain.Models.ReferrerProjectTreatmentPricing>().ForMember(dest => dest.Price, opt => opt.MapFrom(src => Convert.ToDecimal(src.Price)));

            Mapper.CreateMap<ITS.Domain.Models.ReferrerProjectTreatmentAssessment, ITSService.ReferrerService.ReferrerProjectTreatmentAssessment>();
            Mapper.CreateMap<ITSService.ReferrerService.ReferrerProjectTreatmentAssessment, ITS.Domain.Models.ReferrerProjectTreatmentAssessment>();

            Mapper.CreateMap<ITSService.ReferrerService.TreatmentCategoriesPricingTypes, ITS.Domain.Models.TreatmentCategoriesPricingTypes>();
            Mapper.CreateMap<ITS.Domain.Models.TreatmentCategoriesPricingTypes, ITSService.ReferrerService.TreatmentCategoriesPricingTypes>();

            Mapper.CreateMap<ITS.Domain.Models.AssessmentService, ITSService.ReferrerService.AssessmentService>();
            Mapper.CreateMap<ITSService.ReferrerService.AssessmentService, ITS.Domain.Models.AssessmentService>();

            Mapper.CreateMap<ITS.Domain.Models.AssessmentType, ITSService.ReferrerService.AssessmentType>();
            Mapper.CreateMap<ITSService.ReferrerService.AssessmentType, ITS.Domain.Models.AssessmentType>();

            Mapper.CreateMap<ITS.Domain.Models.ReferrerProjectTreatmentAuthorisation, ITSService.ReferrerService.ReferrerProjectTreatmentAuthorisation>().ForMember(dest => dest.Amount, opt => opt.MapFrom(src => Convert.ToDecimal(src.Amount)));
            Mapper.CreateMap<ITSService.ReferrerService.ReferrerProjectTreatmentAuthorisation, ITS.Domain.Models.ReferrerProjectTreatmentAuthorisation>().ForMember(dest => dest.Amount, opt => opt.MapFrom(src => Convert.ToDecimal(src.Amount)));

            Mapper.CreateMap<ITS.Domain.Models.DelegatedAuthorisation, ITSService.ReferrerService.DelegatedAuthorisation>();
            Mapper.CreateMap<ITSService.ReferrerService.DelegatedAuthorisation, ITS.Domain.Models.DelegatedAuthorisation>();

            Mapper.CreateMap<ITS.Domain.Models.EmailTypes, ITSService.ReferrerService.EmailType>();
            Mapper.CreateMap<ITSService.ReferrerService.EmailType, ITS.Domain.Models.EmailTypes>();

            Mapper.CreateMap<ITS.Domain.Models.ReferrerProjectTreatmentDocumentSetUp, ITSService.ReferrerService.ReferrerProjectTreatmentDocumentSetup>();
            Mapper.CreateMap<ITSService.ReferrerService.ReferrerProjectTreatmentDocumentSetup, ITS.Domain.Models.ReferrerProjectTreatmentDocumentSetUp>();

            Mapper.CreateMap<ITS.Domain.Models.ReferrerProjectTreatmentEmailSetUp, ITSService.ReferrerService.ReferrerProjectTreatmentEmail>();
            Mapper.CreateMap<ITSService.ReferrerService.ReferrerProjectTreatmentEmail, ITS.Domain.Models.ReferrerProjectTreatmentEmailSetUp>();

            Mapper.CreateMap<ITS.Domain.Models.PricingTypes, ITSService.ReferrerService.PricingType>();
            Mapper.CreateMap<ITSService.ReferrerService.PricingType, ITS.Domain.Models.PricingTypes>();

            Mapper.CreateMap<ITS.Domain.Models.InvoiceMethod, ITSService.ReferrerService.InvoiceMethod>();
            Mapper.CreateMap<ITSService.ReferrerService.InvoiceMethod, ITS.Domain.Models.InvoiceMethod>();

            Mapper.CreateMap<ITS.Domain.Models.InvoiceMethod, ITS.Domain.Models.ReferrerProjectTreatmentInvoice>();
            Mapper.CreateMap<ITS.Domain.Models.ReferrerProjectTreatmentInvoice, ITS.Domain.Models.InvoiceMethod>();

            Mapper.CreateMap<ITS.Domain.Models.ReferrerProjectTreatmentInvoice, ITSService.ReferrerService.ReferrerProjectTreatmentInvoice>().ForMember(dest => dest.InvoicePrice, opt => opt.MapFrom(src => Convert.ToDecimal(src.InvoicePrice))).ForMember(dest => dest.ManagementPrice, opt => opt.MapFrom(src => Convert.ToDecimal(src.ManagementPrice)));
            Mapper.CreateMap<ITSService.ReferrerService.ReferrerProjectTreatmentInvoice, ITS.Domain.Models.ReferrerProjectTreatmentInvoice>().ForMember(dest => dest.InvoicePrice, opt => opt.MapFrom(src => Convert.ToDecimal(src.InvoicePrice))).ForMember(dest => dest.ManagementPrice, opt => opt.MapFrom(src => Convert.ToDecimal(src.ManagementPrice)));

            Mapper.CreateMap<ITS.Domain.Models.ServiceLevelAgreement, ITSService.ReferrerService.ServiceLevelAgreement>();
            Mapper.CreateMap<ITSService.ReferrerService.ServiceLevelAgreement, ITS.Domain.Models.ServiceLevelAgreement>();

            Mapper.CreateMap<ITS.Domain.Models.Supplier, ITSService.SupplierService.Supplier>();
            Mapper.CreateMap<ITSService.SupplierService.Supplier, ITS.Domain.Models.Supplier>();

            Mapper.CreateMap<ITS.Domain.Models.SupplierComplaint, ITSService.SupplierService.SupplierComplaint>();
            Mapper.CreateMap<ITSService.SupplierService.SupplierComplaint, ITS.Domain.Models.SupplierComplaint>();

            Mapper.CreateMap<ITS.Domain.Models.ComplaintType, ITSService.SupplierService.ComplaintType>();
            Mapper.CreateMap<ITSService.SupplierService.ComplaintType, ITS.Domain.Models.ComplaintType>();

            Mapper.CreateMap<ITS.Domain.Models.ComplaintStatus, ITSService.SupplierService.ComplaintStatus>();
            Mapper.CreateMap<ITSService.SupplierService.ComplaintStatus, ITS.Domain.Models.ComplaintStatus>();

            Mapper.CreateMap<ITS.Domain.Models.SupplierTreatment, ITSService.SupplierService.SupplierTreatment>();
            Mapper.CreateMap<ITSService.SupplierService.SupplierTreatment, ITS.Domain.Models.SupplierTreatment>();

            Mapper.CreateMap<ITS.Domain.Models.SupplierDocument, ITSService.SupplierService.SupplierDocument>();
            Mapper.CreateMap<ITSService.SupplierService.SupplierDocument, ITS.Domain.Models.SupplierDocument>();

            Mapper.CreateMap<ITS.Domain.Models.SupplierTreatmentCategoriesPricingType, ITSService.ReferrerService.TreatmentCategoriesPricingTypes>();
            Mapper.CreateMap<ITSService.ReferrerService.TreatmentCategoriesPricingTypes, ITS.Domain.Models.SupplierTreatmentCategoriesPricingType>();

            Mapper.CreateMap<ITS.Domain.Models.SupplierInsurance, ITSService.SupplierService.SupplierInsurance>();
            Mapper.CreateMap<ITSService.SupplierService.SupplierInsurance, ITS.Domain.Models.SupplierInsurance>();

            Mapper.CreateMap<ITS.Domain.Models.SupplierTreatmentPricing, ITSService.SupplierService.SupplierTreatmentPricing>().ForMember(dest => dest.Price, opt => opt.MapFrom(src => Convert.ToDecimal(src.Price)));
            Mapper.CreateMap<ITSService.SupplierService.SupplierTreatmentPricing, ITS.Domain.Models.SupplierTreatmentPricing>().ForMember(dest => dest.Price, opt => opt.MapFrom(src => Convert.ToDecimal(src.Price)));

            Mapper.CreateMap<ITS.Domain.Models.SupplierSiteAudit, ITSService.SupplierService.SupplierSiteAudit>();
            Mapper.CreateMap<ITSService.SupplierService.SupplierSiteAudit, ITS.Domain.Models.SupplierSiteAudit>();

            Mapper.CreateMap<ITS.Domain.Models.SupplierSearch, ITSService.SupplierService.SupplierSearch>();
            Mapper.CreateMap<ITSService.SupplierService.SupplierSearch, ITS.Domain.Models.SupplierSearch>();

            Mapper.CreateMap<ITS.Domain.Models.SupplierClinicalAudit, ITSService.SupplierService.SupplierClinicalAudit>();
            Mapper.CreateMap<ITSService.SupplierService.SupplierClinicalAudit, ITS.Domain.Models.SupplierClinicalAudit>();

            Mapper.CreateMap<ITS.Domain.Models.Practitioner, ITSService.PractitionerService.Practitioner>();
            Mapper.CreateMap<ITSService.PractitionerService.Practitioner, ITS.Domain.Models.Practitioner>();

            Mapper.CreateMap<ITS.Domain.Models.TreatmentCategories, ITSService.PractitionerService.TreatmentCategory>();
            Mapper.CreateMap<ITSService.PractitionerService.TreatmentCategory, ITS.Domain.Models.TreatmentCategories>();

            Mapper.CreateMap<ITS.Domain.Models.RegistrationTypes, ITSService.SupplierService.RegistrationType>();
            Mapper.CreateMap<ITSService.SupplierService.RegistrationType, ITS.Domain.Models.RegistrationTypes>();

            Mapper.CreateMap<ITS.Domain.Models.AreasofExpertise, ITSService.PractitionerService.AreasofExpertise>();
            Mapper.CreateMap<ITSService.PractitionerService.AreasofExpertise, ITS.Domain.Models.AreasofExpertise>();

            Mapper.CreateMap<ITS.Domain.Models.PractitionerExperties, ITSService.PractitionerService.PractitionerExpertise>();
            Mapper.CreateMap<ITSService.PractitionerService.PractitionerExpertise, ITS.Domain.Models.PractitionerExperties>();

            Mapper.CreateMap<ITS.Domain.Models.SupplierPractitioner, ITSService.SupplierService.SupplierPractitioner>();
            Mapper.CreateMap<ITSService.SupplierService.SupplierPractitioner, ITS.Domain.Models.SupplierPractitioner>();

            Mapper.CreateMap<ITS.Domain.Models.TreatmentType, ITSService.SupplierService.TreatmentType>();
            Mapper.CreateMap<ITSService.SupplierService.TreatmentType, ITS.Domain.Models.TreatmentType>();

            Mapper.CreateMap<ITS.Domain.Models.TreatmentCategories, ITSService.ReferrerService.TreatmentCategory>();
            Mapper.CreateMap<ITSService.ReferrerService.TreatmentCategory, ITS.Domain.Models.TreatmentCategories>();

            Mapper.CreateMap<ITS.Domain.Models.CaseAppointmentDate, ITSService.CaseService.CaseAppointmentDate>();
            Mapper.CreateMap<ITSService.CaseService.CaseAppointmentDate, ITS.Domain.Models.CaseAppointmentDate>();

            Mapper.CreateMap<ITS.Domain.Models.TreatmentCategoriesRegistrationTypes, ITSService.SupplierService.TreatmentCategoriesRegistrationTypes>();
            Mapper.CreateMap<ITSService.SupplierService.TreatmentCategoriesRegistrationTypes, ITS.Domain.Models.TreatmentCategoriesRegistrationTypes>();

            Mapper.CreateMap<ITS.Domain.Models.TreatmentCategoriesAreasofExpertises, ITSService.SupplierService.TreatmentCategoriesAreasofExpertises>();
            Mapper.CreateMap<ITSService.SupplierService.TreatmentCategoriesAreasofExpertises, ITS.Domain.Models.TreatmentCategoriesAreasofExpertises>();

            Mapper.CreateMap<ITS.Domain.Models.SupplierAuditor, ITSService.UserService.User>();
            Mapper.CreateMap<ITSService.UserService.User, ITS.Domain.Models.SupplierAuditor>();

            Mapper.CreateMap<ITS.Domain.Models.SupplierDocumentUser, ITSService.SupplierService.SupplierDocumentUser>();
            Mapper.CreateMap<ITSService.SupplierService.SupplierDocumentUser, ITS.Domain.Models.SupplierDocumentUser>();

            Mapper.CreateMap<ITS.Domain.Models.Patient, ITSService.PatientService.Patient>();
            Mapper.CreateMap<ITSService.PatientService.Patient, ITS.Domain.Models.Patient>();

            Mapper.CreateMap<ITS.Domain.Models.Patient, ITSService.CaseService.Patient>();
            Mapper.CreateMap<ITSService.CaseService.Patient, ITS.Domain.Models.Patient>();

            Mapper.CreateMap<ITS.Domain.Models.PractitionerRegistration, ITSService.PractitionerService.PractitionerRegistration>();
            Mapper.CreateMap<ITSService.PractitionerService.PractitionerRegistration, ITS.Domain.Models.PractitionerRegistration>();

            Mapper.CreateMap<ITS.Domain.Models.PractitionerTreatmentRegistration, ITSService.SupplierService.PractitionerTreatmentRegistration>();
            Mapper.CreateMap<ITSService.SupplierService.PractitionerTreatmentRegistration, ITS.Domain.Models.PractitionerTreatmentRegistration>();

            Mapper.CreateMap<ITS.Domain.Models.SupplierPractitionerTreatmentRegistration, ITSService.SupplierService.SupplierPractitionerTreatmentRegistration>();
            Mapper.CreateMap<ITSService.SupplierService.SupplierPractitionerTreatmentRegistration, ITS.Domain.Models.SupplierPractitionerTreatmentRegistration>();

            Mapper.CreateMap<ITS.Domain.Models.Case, ITSService.CaseService.Case>();
            Mapper.CreateMap<ITSService.CaseService.Case, ITS.Domain.Models.Case>();

            Mapper.CreateMap<ITS.Domain.Models.CaseWorkflowCount, ITSService.CaseService.CaseWorkflowCount>();
            Mapper.CreateMap<ITSService.CaseService.CaseWorkflowCount, ITS.Domain.Models.CaseWorkflowCount>();

            Mapper.CreateMap<ITS.Domain.Models.CaseWorkflowPatientReferrerProject, ITSService.CaseService.CaseWorkflowPatientReferrerProject>();
            Mapper.CreateMap<ITSService.CaseService.CaseWorkflowPatientReferrerProject, ITS.Domain.Models.CaseWorkflowPatientReferrerProject>();
            
            Mapper.CreateMap<ITS.Domain.Models.CasePatientTreatment, ITSService.PatientService.CasePatientTreatment>();
            Mapper.CreateMap<ITSService.PatientService.CasePatientTreatment, ITS.Domain.Models.CasePatientTreatment>();

            Mapper.CreateMap<ITSService.SupplierService.SupplierDistanceRankPrice, ITS.Domain.Models.SupplierDistanceRankPrice>()
                .ForMember(supplier => supplier.RankingText, opt => opt.MapFrom(source => double.IsNaN(source.Ranking) ? "N/A" : string.Format("{0}%", source.Ranking)))
                .ForMember(supplier => supplier.Ranking, opt => opt.MapFrom(source => double.IsNaN(source.Ranking) ? 100 : source.Ranking));

            Mapper.CreateMap<ITSService.CaseService.CasePatientReferrerSupplier, ITS.Domain.Models.CasePatientReferrerSupplier>();

            Mapper.CreateMap<ITS.Domain.Models.CaseAssessmentPatientImpact, ITSService.AssessmentService.CaseAssessmentPatientImpact>();
            Mapper.CreateMap<ITSService.AssessmentService.CaseAssessmentPatientImpact, ITS.Domain.Models.CaseAssessmentPatientImpact>();

            Mapper.CreateMap<ITS.Domain.Models.CaseAssessment, ITSService.AssessmentService.CaseAssessment>();
            Mapper.CreateMap<ITSService.AssessmentService.CaseAssessment, ITS.Domain.Models.CaseAssessment>();

            Mapper.CreateMap<ITS.Domain.Models.CaseAssessmentCustom, ITSService.AssessmentService.CaseAssessmentCustom>();
            Mapper.CreateMap<ITSService.AssessmentService.CaseAssessmentCustom, ITS.Domain.Models.CaseAssessmentCustom>();

            Mapper.CreateMap<ITS.Domain.Models.CaseAssessmentPatientInjury, ITSService.AssessmentService.CaseAssessmentPatientInjury>();
            Mapper.CreateMap<ITSService.AssessmentService.CaseAssessmentPatientInjury, ITS.Domain.Models.CaseAssessmentPatientInjury>();

            Mapper.CreateMap<ITS.Domain.Models.CaseAssessmentRating, ITSService.AssessmentService.CaseAssessmentRating>();
            Mapper.CreateMap<ITSService.AssessmentService.CaseAssessmentRating, ITS.Domain.Models.CaseAssessmentRating>();

            Mapper.CreateMap<ITS.Domain.Models.PsychologicalFactor, ITSService.AssessmentService.PsychologicalFactor>();
            Mapper.CreateMap<ITSService.AssessmentService.PsychologicalFactor, ITS.Domain.Models.PsychologicalFactor>();

            Mapper.CreateMap<ITS.Domain.Models.PatientImpact, ITSService.AssessmentService.PatientImpact>();
            Mapper.CreateMap<ITSService.AssessmentService.PatientImpact, ITS.Domain.Models.PatientImpact>();

            Mapper.CreateMap<ITS.Domain.Models.PatientImpactValue, ITSService.AssessmentService.PatientImpactValue>();
            Mapper.CreateMap<ITSService.AssessmentService.PatientImpactValue, ITS.Domain.Models.PatientImpactValue>();

            Mapper.CreateMap<ITS.Domain.Models.PatientWorkstatus, ITSService.AssessmentService.PatientWorkstatus>();
            Mapper.CreateMap<ITSService.AssessmentService.PatientWorkstatus, ITS.Domain.Models.PatientWorkstatus>();

            Mapper.CreateMap<ITS.Domain.Models.PatientImpactOnWork, ITSService.AssessmentService.PatientImpactOnWork>();
            Mapper.CreateMap<ITSService.AssessmentService.PatientImpactOnWork, ITS.Domain.Models.PatientImpactOnWork>();

            Mapper.CreateMap<ITS.Domain.Models.PatientLevelOfRecovery, ITSService.AssessmentService.PatientLevelOfRecovery>();
            Mapper.CreateMap<ITSService.AssessmentService.PatientLevelOfRecovery, ITS.Domain.Models.PatientLevelOfRecovery>();

            Mapper.CreateMap<ITS.Domain.Models.ProposedTreatmentMethod, ITSService.AssessmentService.ProposedTreatmentMethod>();
            Mapper.CreateMap<ITSService.AssessmentService.ProposedTreatmentMethod, ITS.Domain.Models.ProposedTreatmentMethod>();

            Mapper.CreateMap<ITS.Domain.Models.CaseTreatmentPricing, ITSService.CaseService.CaseTreatmentPricing>();
            Mapper.CreateMap<ITSService.CaseService.CaseTreatmentPricing, ITS.Domain.Models.CaseTreatmentPricing>();

            Mapper.CreateMap<ITS.Domain.Models.CaseAssessmentModel.CaseTreatmentPricingCaseSearch, ITSService.CaseService.CaseTreatmentPricingCaseSearch>();
            Mapper.CreateMap<ITSService.CaseService.CaseTreatmentPricingCaseSearch, ITS.Domain.Models.CaseAssessmentModel.CaseTreatmentPricingCaseSearch>();

            Mapper.CreateMap<ITS.Domain.Models.CaseAssessmentModel.CaseTreatmentPricingType, ITSService.CaseService.CaseTreatmentPricing>();
            Mapper.CreateMap<ITSService.CaseService.CaseTreatmentPricing, ITS.Domain.Models.CaseAssessmentModel.CaseTreatmentPricingType>();

            Mapper.CreateMap<ITS.Domain.Models.CaseAssessmentModel.CaseTreatmentPricingType, ITSService.CaseService.CaseTreatmentPricingCaseSearch>();
            Mapper.CreateMap<ITSService.CaseService.CaseTreatmentPricingCaseSearch, ITS.Domain.Models.CaseAssessmentModel.CaseTreatmentPricingType>();

            Mapper.CreateMap<ITS.Domain.Models.CaseBespokeServicePricing, ITSService.CaseService.CaseBespokeServicePricing>();
            Mapper.CreateMap<ITSService.CaseService.CaseBespokeServicePricing, ITS.Domain.Models.CaseBespokeServicePricing>();

            Mapper.CreateMap<ITS.Domain.Models.TreatmentCategoriesBespokeService, ITSService.AssessmentService.TreatmentCategoriesBespokeService>();
            Mapper.CreateMap<ITSService.AssessmentService.TreatmentCategoriesBespokeService, ITS.Domain.Models.TreatmentCategoriesBespokeService>();

            Mapper.CreateMap<ITS.Domain.ViewModels.PagedCaseWorkflowPatientReferrerProject, ITSService.CaseService.PagedCaseWorkflowPatientReferrerProject>();
            Mapper.CreateMap<ITSService.CaseService.PagedCaseWorkflowPatientReferrerProject, ITS.Domain.ViewModels.PagedCaseWorkflowPatientReferrerProject>();

            Mapper.CreateMap<ITS.Domain.Models.SupplierModel.PractitionerTreatmentRegistration, ITSService.SupplierService.SupplierPractitionerTreatmentRegistration>();
            Mapper.CreateMap<ITSService.SupplierService.SupplierPractitionerTreatmentRegistration, ITS.Domain.Models.SupplierModel.PractitionerTreatmentRegistration>();

            Mapper.CreateMap<ITS.Domain.Models.SupplierModel.Complaint, ITSService.SupplierService.SupplierComplaintAndStatusAndType>();
            Mapper.CreateMap<ITSService.SupplierService.SupplierComplaintAndStatusAndType, ITS.Domain.Models.SupplierModel.Complaint>();

            Mapper.CreateMap<ITS.Domain.Models.SupplierSupplierTreatmentsAndSupplierTreatmenPricing, ITSService.SupplierService.SupplierSupplierTreatmentsAndSupplierTreatmenPricing>();
            Mapper.CreateMap<ITSService.SupplierService.SupplierSupplierTreatmentsAndSupplierTreatmenPricing, ITS.Domain.Models.SupplierSupplierTreatmentsAndSupplierTreatmenPricing>();

            Mapper.CreateMap<ITSService.CaseService.CasePatientSearch, ITS.Domain.Models.CasePatientSearch>();
            Mapper.CreateMap<ITS.Domain.Models.CasePatientSearch, ITSService.CaseService.CasePatientSearch>();


            Mapper.CreateMap<ITSService.CaseService.CasePatientContactAttempt, ITS.Domain.Models.CasePatientContactAttempt>();
            Mapper.CreateMap<ITS.Domain.Models.CasePatientContactAttempt, ITSService.CaseService.CasePatientContactAttempt>();

            Mapper.CreateMap<ITS.Domain.Models.RoleType, ITSService.LookUpService.RoleType>();
            Mapper.CreateMap<ITSService.LookUpService.RoleType, ITS.Domain.Models.RoleType>();

            Mapper.CreateMap<ITS.Domain.Models.WorkType, ITSService.LookUpService.WorkType>();
            Mapper.CreateMap<ITSService.LookUpService.WorkType, ITS.Domain.Models.WorkType>();

            Mapper.CreateMap<ITS.Domain.Models.EmployeeDetail, ITSService.CaseService.EmployeeDetail>();
            Mapper.CreateMap<ITSService.CaseService.EmployeeDetail, ITS.Domain.Models.EmployeeDetail>();

            Mapper.CreateMap<ITS.Domain.Models.Gender, ITSService.LookUpService.Gender>();
            Mapper.CreateMap<ITSService.LookUpService.Gender, ITS.Domain.Models.Gender>();

            Mapper.CreateMap<ITS.Domain.Models.Reinsurer, ITSService.LookUpService.Reinsurer>();
            Mapper.CreateMap<ITSService.LookUpService.Reinsurer, ITS.Domain.Models.Reinsurer>();

            Mapper.CreateMap<ITS.Domain.Models.PasswordHistory, ITSService.UserService.PasswordHistory>();
            Mapper.CreateMap<ITSService.UserService.PasswordHistory, ITS.Domain.Models.PasswordHistory>();

            Mapper.CreateMap<ITS.Domain.Models.DrugTest, ITSService.CaseService.DrugTest>();
            Mapper.CreateMap<ITSService.CaseService.DrugTest, ITS.Domain.Models.DrugTest>();

            Mapper.CreateMap<ITS.Domain.Models.JobDemand, ITSService.CaseService.JobDemand>();
            Mapper.CreateMap<ITSService.CaseService.JobDemand, ITS.Domain.Models.JobDemand>();

            Mapper.CreateMap<ITS.Domain.Models.CaseReferrerAssignedUser, ITSService.CaseService.CaseReferrerAssignedUser>();
            Mapper.CreateMap<ITSService.CaseService.CaseReferrerAssignedUser, ITS.Domain.Models.CaseReferrerAssignedUser>();

            Mapper.CreateMap<ITS.Domain.Models.PolicyOpenDetail, ITSService.CaseService.PolicyOpenDetail>();
            Mapper.CreateMap<ITSService.CaseService.PolicyOpenDetail, ITS.Domain.Models.PolicyOpenDetail>();

            Mapper.CreateMap<ITS.Domain.Models.ReferrerGroup, ITSService.ReferrerService.ReferrerGroup>();
            Mapper.CreateMap<ITSService.ReferrerService.ReferrerGroup, ITS.Domain.Models.ReferrerGroup>();

          

            #region new supplier mappings
            Mapper.CreateMap<ITS.Domain.Models.SupplierModel.Supplier, ITSService.SupplierService.Supplier>();
            Mapper.CreateMap<ITSService.SupplierService.Supplier, ITS.Domain.Models.SupplierModel.Supplier>();

            Mapper.CreateMap<ITS.Domain.Models.SupplierModel.ClinicalAudit, ITSService.SupplierService.SupplierClinicalAudit>();
            Mapper.CreateMap<ITSService.SupplierService.SupplierClinicalAudit, ITS.Domain.Models.SupplierModel.ClinicalAudit>();

            Mapper.CreateMap<ITS.Domain.Models.SupplierModel.SiteAudit, ITSService.SupplierService.SupplierSiteAudit>();
            Mapper.CreateMap<ITSService.SupplierService.SupplierSiteAudit, ITS.Domain.Models.SupplierModel.SiteAudit>();

            Mapper.CreateMap<ITS.Domain.Models.SupplierModel.Document, ITSService.SupplierService.SupplierDocumentUser>();
            Mapper.CreateMap<ITSService.SupplierService.SupplierDocumentUser, ITS.Domain.Models.SupplierModel.Document>();
            Mapper.CreateMap<ITS.Domain.Models.SupplierModel.Document, ITSService.SupplierService.SupplierDocument>();
            Mapper.CreateMap<ITSService.SupplierService.SupplierDocument, ITS.Domain.Models.SupplierModel.Document>();


            Mapper.CreateMap<ITS.Domain.Models.SupplierModel.Insurance, ITSService.SupplierService.SupplierInsurance>();
            Mapper.CreateMap<ITSService.SupplierService.SupplierInsurance, ITS.Domain.Models.SupplierModel.Insurance>();


            Mapper.CreateMap<ITS.Domain.Models.SupplierModel.Complaint, ITSService.SupplierService.SupplierComplaintAndStatusAndType>();
            Mapper.CreateMap<ITSService.SupplierService.SupplierComplaintAndStatusAndType, ITS.Domain.Models.SupplierModel.Complaint>();

            Mapper.CreateMap<ITS.Domain.Models.SupplierModel.Complaint, ITSService.SupplierService.SupplierComplaint>();
            Mapper.CreateMap<ITSService.SupplierService.SupplierComplaint, ITS.Domain.Models.SupplierModel.Complaint>();


            Mapper.CreateMap<ITS.Domain.Models.SupplierModel.Registration, ITSService.SupplierService.SupplierDocumentUser>();
            Mapper.CreateMap<ITSService.SupplierService.SupplierDocumentUser, ITS.Domain.Models.SupplierModel.Registration>();


            Mapper.CreateMap<ITS.Domain.Models.SupplierModel.ClinicalAudit, ITSService.SupplierService.SupplierClinicalAuditSupplierDocument>();
            Mapper.CreateMap<ITSService.SupplierService.SupplierClinicalAuditSupplierDocument, ITS.Domain.Models.SupplierModel.ClinicalAudit>();
            Mapper.CreateMap<ITS.Domain.Models.SupplierModel.Document, ITSService.SupplierService.SupplierClinicalAuditSupplierDocument>();
            Mapper.CreateMap<ITSService.SupplierService.SupplierClinicalAuditSupplierDocument, ITS.Domain.Models.SupplierModel.Document>();

            Mapper.CreateMap<ITS.Domain.Models.SupplierModel.SiteAudit, ITSService.SupplierService.SupplierSiteAuditSupplierDocument>();
            Mapper.CreateMap<ITSService.SupplierService.SupplierSiteAuditSupplierDocument, ITS.Domain.Models.SupplierModel.SiteAudit>();
            Mapper.CreateMap<ITS.Domain.Models.SupplierModel.Document, ITSService.SupplierService.SupplierSiteAuditSupplierDocument>();
            Mapper.CreateMap<ITSService.SupplierService.SupplierSiteAuditSupplierDocument, ITS.Domain.Models.SupplierModel.Document>();

            Mapper.CreateMap<ITS.Domain.Models.SupplierModel.Insurance, ITSService.SupplierService.SupplierInsuranceSupplierDocument>();
            Mapper.CreateMap<ITSService.SupplierService.SupplierInsuranceSupplierDocument, ITS.Domain.Models.SupplierModel.Insurance>();
            Mapper.CreateMap<ITS.Domain.Models.SupplierModel.Document, ITSService.SupplierService.SupplierInsuranceSupplierDocument>();
            Mapper.CreateMap<ITSService.SupplierService.SupplierInsuranceSupplierDocument, ITS.Domain.Models.SupplierModel.Document>();

            Mapper.CreateMap<ITS.Domain.Models.SupplierModel.Registration, ITSService.SupplierService.SupplierRegistrationSupplierDocument>();
            Mapper.CreateMap<ITSService.SupplierService.SupplierRegistrationSupplierDocument, ITS.Domain.Models.SupplierModel.Registration>();
            Mapper.CreateMap<ITS.Domain.Models.SupplierModel.Document, ITSService.SupplierService.SupplierRegistrationSupplierDocument>();
            Mapper.CreateMap<ITSService.SupplierService.SupplierRegistrationSupplierDocument, ITS.Domain.Models.SupplierModel.Document>();

            Mapper.CreateMap<ITSService.SupplierService.SupplierSearch, ITS.Domain.Models.SupplierModel.SupplierSearchResult>();
            #endregion

            #region practitioner

            Mapper.CreateMap<ITS.Domain.Models.PractitionerModel.Practitioner, ITSService.PractitionerService.Practitioner>();
            Mapper.CreateMap<ITSService.PractitionerService.Practitioner, ITS.Domain.Models.PractitionerModel.Practitioner>();

            Mapper.CreateMap<ITS.Domain.Models.PractitionerModel.Expertise, ITSService.PractitionerService.PractitionerExpertise>();
            Mapper.CreateMap<ITSService.PractitionerService.PractitionerExpertise, ITS.Domain.Models.PractitionerModel.Expertise>();

            Mapper.CreateMap<ITS.Domain.Models.PractitionerModel.Registration, ITSWebApp.ITSService.PractitionerService.PractitionerRegistration>();
            Mapper.CreateMap<ITSWebApp.ITSService.PractitionerService.PractitionerRegistration, ITS.Domain.Models.PractitionerModel.Registration>();

            Mapper.CreateMap<ITSWebApp.ITSService.PractitionerService.AreasofExpertise, ITS.Domain.Models.PractitionerModel.AreasofExpertise>();
            Mapper.CreateMap<ITS.Domain.Models.PractitionerModel.AreasofExpertise, ITSWebApp.ITSService.PractitionerService.AreasofExpertise>();

            Mapper.CreateMap<ITSWebApp.ITSService.SupplierService.RegistrationType, ITS.Domain.Models.PractitionerModel.RegistrationType>();
            Mapper.CreateMap<ITS.Domain.Models.PractitionerModel.RegistrationType, ITSWebApp.ITSService.SupplierService.RegistrationType>();

            Mapper.CreateMap<ITSWebApp.ITSService.SupplierService.RegistrationType, ITS.Domain.Models.PractitionerModel.RegistrationType>();
            Mapper.CreateMap<ITS.Domain.Models.PractitionerModel.RegistrationType, ITSWebApp.ITSService.SupplierService.RegistrationType>();

            Mapper.CreateMap<ITSWebApp.ITSService.PractitionerService.TreatmentCategory, ITS.Domain.Models.PractitionerModel.TreatmentCategory>();
            Mapper.CreateMap<ITS.Domain.Models.PractitionerModel.TreatmentCategory, ITSWebApp.ITSService.PractitionerService.TreatmentCategory>();

            Mapper.CreateMap<ITSWebApp.ITSService.SupplierService.Supplier, ITS.Domain.Models.PractitionerModel.Supplier>();
            Mapper.CreateMap<ITS.Domain.Models.PractitionerModel.Supplier, ITSWebApp.ITSService.SupplierService.Supplier>();

            Mapper.CreateMap<ITSWebApp.ITSService.PractitionerService.SupplierPractitionerSupplier, ITS.Domain.Models.PractitionerModel.SupplierPractitioner>();
            Mapper.CreateMap<ITS.Domain.Models.PractitionerModel.SupplierPractitioner, ITSWebApp.ITSService.PractitionerService.SupplierPractitionerSupplier>();

            Mapper.CreateMap<ITS.Domain.Models.PractitionerModel.SupplierPractitioner, ITSWebApp.ITSService.SupplierService.SupplierPractitioner>();
            Mapper.CreateMap<ITSWebApp.ITSService.SupplierService.SupplierPractitioner, ITS.Domain.Models.PractitionerModel.SupplierPractitioner>();

            //TODO need to remove PractitionerSerach DTO
            //Mapper.CreateMap<ITSService.PractitionerService.PractitionerSearch, ITS.Domain.Models.PractitionerModel.PractitionerSearchResult>();
            Mapper.CreateMap<ITSWebApp.ITSService.PractitionerService.PractitionerTreatmentRegistration, ITS.Domain.Models.PractitionerModel.PractitionerSearchResult>();
            Mapper.CreateMap<ITS.Domain.Models.PractitionerModel.PractitionerSearchResult, ITSWebApp.ITSService.PractitionerService.PractitionerTreatmentRegistration>();

            Mapper.CreateMap<ITSWebApp.ITSService.PractitionerService.PracitionerSupplierTreatmentCategory, ITS.Domain.Models.PractitionerModel.Practitioner>();
            Mapper.CreateMap<ITS.Domain.Models.PractitionerModel.Practitioner, ITSWebApp.ITSService.PractitionerService.PracitionerSupplierTreatmentCategory>();

            Mapper.CreateMap<ITSWebApp.ITSService.SupplierService.SuppliersName, ITS.Domain.Models.PractitionerModel.Supplier>();
            Mapper.CreateMap<ITS.Domain.Models.PractitionerModel.Supplier, ITSWebApp.ITSService.SupplierService.SuppliersName>();





            #endregion

            #region new case mappings
            Mapper.CreateMap<ITSService.CaseService.Case, ITS.Domain.Models.CaseModel.Case>();
            Mapper.CreateMap<ITSService.CaseService.Case, ITS.Domain.Models.CaseModel.Case>();

            Mapper.CreateMap<ITSService.CaseService.CaseCommunicationHistory, ITS.Domain.Models.CaseAssessmentModel.CaseCommunicationHistory>();
            Mapper.CreateMap<ITS.Domain.Models.CaseAssessmentModel.CaseCommunicationHistory, ITSService.CaseService.CaseCommunicationHistory>();

            Mapper.CreateMap<ITSService.CaseService.CaseCommunicationHistoryUser, ITS.Domain.Models.CaseSearch.CaseCommunicationHistoryUser>();
            Mapper.CreateMap<ITS.Domain.Models.CaseSearch.CaseCommunicationHistoryUser, ITSService.CaseService.CaseCommunicationHistoryUser>();

            Mapper.CreateMap<ITS.Domain.Models.CaseSearch.CaseCommunicationHistoryUser, ITSService.CaseService.CaseCommunicationHistory>();




            #endregion

            #region new treatmentcategory mapping
            Mapper.CreateMap<ITSService.ReferrerService.TreatmentCategoriesPricingTypes, ITS.Domain.Models.TreatmentCategoryModel.TreatmentCategoriesPricingTypes>();

            Mapper.CreateMap<ITS.Domain.ViewModels.SupplierTreatmentCategoryPricingViewModel, ITSService.SupplierService.SupplierTreatment>();
            Mapper.CreateMap<ITSService.SupplierService.SupplierTreatment, ITS.Domain.ViewModels.SupplierTreatmentCategoryPricingViewModel>();

            Mapper.CreateMap<ITSService.SupplierService.SupplierPricingValue, ITS.Domain.Models.SupplierModel.TreatmentPricing>();

            Mapper.CreateMap<ITS.Domain.Models.SupplierModel.TreatmentPricing, ITSService.SupplierService.SupplierTreatmentPricing>()
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => Convert.ToDecimal(src.Price)));

            Mapper.CreateMap<ITSService.ReferrerService.ReferrerPricingValue, ITS.Domain.Models.ReferrerModel.TreatmentPricing>();
            Mapper.CreateMap<ITS.Domain.Models.ReferrerModel.TreatmentPricing, ITSService.ReferrerService.ReferrerPricingValue>()
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => Convert.ToDecimal(src.Price)));

            #endregion

            #region new referrer mappings
            Mapper.CreateMap<ITSService.ReferrerService.Referrer, ITS.Domain.Models.ReferrerModel.Referrer>();
            Mapper.CreateMap<ITS.Domain.Models.ReferrerModel.Referrer, ITSService.ReferrerService.Referrer>();

            Mapper.CreateMap<ITSService.ReferrerService.ReferrerLocation, ITS.Domain.Models.ReferrerModel.Location>();
            Mapper.CreateMap<ITS.Domain.Models.ReferrerModel.Location, ITSService.ReferrerService.ReferrerLocation>();

            Mapper.CreateMap<ITSService.ReferrerService.ReferrerProject, ITS.Domain.Models.ReferrerModel.Project>();
            Mapper.CreateMap<ITS.Domain.Models.ReferrerModel.Project, ITSService.ReferrerService.ReferrerProject>();

            //Need to change
            Mapper.CreateMap<ITS.Domain.Models.ReferrerModel.ReferrerSearchResult, ITSService.ReferrerService.ReferrerLocationReferrer>();
            Mapper.CreateMap<ITSService.ReferrerService.ReferrerLocationReferrer, ITS.Domain.Models.ReferrerModel.ReferrerSearchResult>();


            Mapper.CreateMap<ITSService.ReferrerService.TreatmentCategory, ITS.Domain.Models.ReferrerModel.TreatmentCategory>();
            Mapper.CreateMap<ITS.Domain.Models.ReferrerModel.TreatmentCategory, ITSService.ReferrerService.TreatmentCategory>();

            //Mapper.CreateMap<ITSService.ReferrerService.ReferrerPricingValue, ITS.Domain.ViewModels.ReferrerPricingValue>();
            //Mapper.CreateMap<ITS.Domain.ViewModels.ReferrerPricingValue, ITSService.ReferrerService.ReferrerPricingValue>();

            Mapper.CreateMap<ITSService.ReferrerService.ReferrerProject, ITS.Domain.ViewModels.ReferrerProjectTreatmentCategoryViewModel>();

            Mapper.CreateMap<ITSService.ReferrerService.ReferrerProjectTreatment, ITS.Domain.ViewModels.ReferrerProjectTreatmentCategoryPricingViewModel>();
            Mapper.CreateMap<ITS.Domain.ViewModels.ReferrerProjectTreatmentCategoryPricingViewModel, ITSService.ReferrerService.ReferrerProjectTreatment>();

            Mapper.CreateMap<ITSWebApp.ITSService.ReferrerService.ReferrerProjectTreatmentPricing, ITS.Domain.Models.ReferrerModel.TreatmentPricing>();
            Mapper.CreateMap<ITS.Domain.Models.ReferrerModel.TreatmentPricing, ITSWebApp.ITSService.ReferrerService.ReferrerProjectTreatmentPricing>();

            //Mapper.CreateMap<ITSWebApp.ITSService.ReferrerService.ReferrerProjectTreatmentPricing, ITS.Domain.Models.ReferrerProjectTreatmentModel.ProjectTreatmentPricing>();
            //Mapper.CreateMap<ITS.Domain.Models.ReferrerProjectTreatmentModel.ProjectTreatmentPricing, ITSWebApp.ITSService.ReferrerService.ReferrerProjectTreatmentPricing>();

            Mapper.CreateMap<ITSWebApp.ITSService.ReferrerService.ReferrerProjectTreatmentAssessment, ITS.Domain.Models.ReferrerProjectTreatmentModel.ProjectTreatmentAssessmentService>();
            Mapper.CreateMap<ITS.Domain.Models.ReferrerProjectTreatmentModel.ProjectTreatmentAssessmentService, ITSWebApp.ITSService.ReferrerService.ReferrerProjectTreatmentAssessment>();

            Mapper.CreateMap<ITSWebApp.ITSService.ReferrerService.AssessmentService, ITS.Domain.Models.ReferrerModel.AssessmentService>();
            Mapper.CreateMap<ITS.Domain.Models.ReferrerModel.AssessmentService, ITSWebApp.ITSService.ReferrerService.AssessmentService>();


            Mapper.CreateMap<ITSWebApp.ITSService.ReferrerService.ReferrerProjectTreatmentDocumentSetup, ITS.Domain.Models.ReferrerProjectTreatmentModel.ProjectTreatmentDocumentSetup>();
            Mapper.CreateMap<ITS.Domain.Models.ReferrerProjectTreatmentModel.ProjectTreatmentDocumentSetup, ITSWebApp.ITSService.ReferrerService.ReferrerProjectTreatmentDocumentSetup>();


            Mapper.CreateMap<ITSWebApp.ITSService.ReferrerService.ProjectTreatmentSLAName, ITS.Domain.Models.ReferrerProjectTreatmentModel.ProjectTreatmentSLAName>();
            Mapper.CreateMap<ITS.Domain.Models.ReferrerProjectTreatmentModel.ProjectTreatmentSLAName, ITSWebApp.ITSService.ReferrerService.ProjectTreatmentSLAName>();

            Mapper.CreateMap<ITSWebApp.ITSService.ReferrerService.ProjectTreatmentSLA, ITS.Domain.Models.ReferrerProjectTreatmentModel.ProjectTreatmentSLAName>();
            Mapper.CreateMap<ITS.Domain.Models.ReferrerProjectTreatmentModel.ProjectTreatmentSLAName, ITSWebApp.ITSService.ReferrerService.ProjectTreatmentSLA>();

            //Mapper.CreateMap<ITSWebApp.ITSService.ReferrerService.ReferrerProjectTreatmentPricing, ITS.Domain.Models.ReferrerModel.ProjectTreatment>();
            //Mapper.CreateMap<ITS.Domain.Models.ReferrerModel.ProjectTreatment, ITSWebApp.ITSService.ReferrerService.ReferrerProjectTreatmentPricing>();

            Mapper.CreateMap<ITSWebApp.ITSService.ReferrerService.ReferrerProjectTreatment, ITS.Domain.Models.ReferrerModel.ProjectTreatment>();
            Mapper.CreateMap<ITS.Domain.Models.ReferrerModel.ProjectTreatment, ITSWebApp.ITSService.ReferrerService.ReferrerProjectTreatment>();

            Mapper.CreateMap<ITSWebApp.ITSService.ReferrerService.ReferrerProjectTreatmentAuthorisation, ITS.Domain.Models.ReferrerProjectTreatmentModel.ProjectTreatmentDelegateAuthorisation>();
            Mapper.CreateMap<ITS.Domain.Models.ReferrerProjectTreatmentModel.ProjectTreatmentDelegateAuthorisation, ITSWebApp.ITSService.ReferrerService.ReferrerProjectTreatmentAuthorisation>();

            Mapper.CreateMap<ITSWebApp.ITSService.ReferrerService.ReferrerProjectTreatmentInvoiceMethod, ITS.Domain.Models.ReferrerProjectTreatmentModel.ProjectTreatmentInvoiceMethod>();
            Mapper.CreateMap<ITS.Domain.Models.ReferrerProjectTreatmentModel.ProjectTreatmentInvoiceMethod, ITSWebApp.ITSService.ReferrerService.ReferrerProjectTreatmentInvoiceMethod>();


            Mapper.CreateMap<ITSWebApp.ITSService.ReferrerService.ReferrerProjectTreatmentInvoice, ITS.Domain.Models.ReferrerProjectTreatmentModel.ProjectTreatmentInvoiceMethod>();
            Mapper.CreateMap<ITS.Domain.Models.ReferrerProjectTreatmentModel.ProjectTreatmentInvoiceMethod, ITSWebApp.ITSService.ReferrerService.ReferrerProjectTreatmentInvoice>();

            Mapper.CreateMap<ITSWebApp.ITSService.ReferrerService.ReferrerProjectTreatmentEmailTypeName, ITS.Domain.Models.ReferrerProjectTreatmentModel.ProjectTreatmentEmailTypeName>();
            Mapper.CreateMap<ITS.Domain.Models.ReferrerProjectTreatmentModel.ProjectTreatmentEmailTypeName, ITSWebApp.ITSService.ReferrerService.ReferrerProjectTreatmentEmailTypeName>();


            Mapper.CreateMap<ITSWebApp.ITSService.ReferrerService.ReferrerProjectTreatmentEmail, ITS.Domain.Models.ReferrerProjectTreatmentModel.ProjectTreatmentEmailTypeName>();
            Mapper.CreateMap<ITS.Domain.Models.ReferrerProjectTreatmentModel.ProjectTreatmentEmailTypeName, ITSWebApp.ITSService.ReferrerService.ReferrerProjectTreatmentEmail>();


            #endregion

            #region new user mappings

            Mapper.CreateMap<ITSService.UserService.UserTypeUser, ITS.Domain.Models.UserModel.UserSearchResult>();
            Mapper.CreateMap<ITS.Domain.Models.UserModel.UserSearchResult, ITSService.UserService.UserTypeUser>();

            Mapper.CreateMap<ITSService.UserService.User, ITS.Domain.Models.UserModel.User>();
            Mapper.CreateMap<ITS.Domain.Models.UserModel.User, ITSService.UserService.User>();

            Mapper.CreateMap<ITSService.ReferrerService.ReferrersName, ITS.Domain.Models.UserModel.ReferrersName>();
            Mapper.CreateMap<ITSService.SupplierService.SuppliersName, ITS.Domain.Models.UserModel.SuppliersName>();

            #endregion

            #region new Internal Tasks mappings
            Mapper.CreateMap<ITS.Domain.ViewModels.InternalTasksViewModel.PagedCaseWorkflowPatientReferrerProject, ITSService.CaseService.PagedCaseWorkflowPatientReferrerProject>();
            Mapper.CreateMap<ITSService.CaseService.PagedCaseWorkflowPatientReferrerProject, ITS.Domain.ViewModels.InternalTasksViewModel.PagedCaseWorkflowPatientReferrerProject>();

            Mapper.CreateMap<ITS.Domain.Models.InternalTaskModel.CaseWorkflowPatientReferrerProject, ITSService.CaseService.CaseWorkflowPatientReferrerProject>();
            Mapper.CreateMap<ITSService.CaseService.CaseWorkflowPatientReferrerProject, ITS.Domain.Models.InternalTaskModel.CaseWorkflowPatientReferrerProject>();

            Mapper.CreateMap<ITS.Domain.Models.InternalTaskModel.CaseWorkflowCount, ITSService.CaseService.CaseWorkflowCount>();
            Mapper.CreateMap<ITSService.CaseService.CaseWorkflowCount, ITS.Domain.Models.InternalTaskModel.CaseWorkflowCount>();


            Mapper.CreateMap<ITS.Domain.Models.CaseAssessmentModel.CasePatientTreatment, ITSService.PatientService.CasePatientTreatment>();
            Mapper.CreateMap<ITSService.PatientService.CasePatientTreatment, ITS.Domain.Models.CaseAssessmentModel.CasePatientTreatment>();

            Mapper.CreateMap<ITS.Domain.Models.CaseAssessmentModel.SupplierDistanceRankPrice, ITSService.SupplierService.SupplierDistanceRankPrice>();
            Mapper.CreateMap<ITSService.SupplierService.SupplierDistanceRankPrice, ITS.Domain.Models.CaseAssessmentModel.SupplierDistanceRankPrice>();

            Mapper.CreateMap<ITS.Domain.Models.CaseAssessmentModel.SupplierSupplierTreatmentsAndSupplierTreatmenPricing, ITSService.SupplierService.SupplierSupplierTreatmentsAndSupplierTreatmenPricing>();
            Mapper.CreateMap<ITSService.SupplierService.SupplierSupplierTreatmentsAndSupplierTreatmenPricing, ITS.Domain.Models.CaseAssessmentModel.SupplierSupplierTreatmentsAndSupplierTreatmenPricing>();


            Mapper.CreateMap<ITS.Domain.Models.CaseAssessmentModel.PatientRole, ITSService.LookUpService.PatientRole>();
            Mapper.CreateMap<ITSService.LookUpService.PatientRole, ITS.Domain.Models.CaseAssessmentModel.PatientRole>();

            Mapper.CreateMap<ITS.Domain.Models.CaseAssessmentModel.Duration, ITSService.LookUpService.Duration>();
            Mapper.CreateMap<ITSService.LookUpService.Duration, ITS.Domain.Models.CaseAssessmentModel.Duration>();

            Mapper.CreateMap<ITS.Domain.Models.Duration, ITSService.LookUpService.Duration>();
            Mapper.CreateMap<ITSService.LookUpService.Duration, ITS.Domain.Models.Duration>();


            Mapper.CreateMap<ITS.Domain.Models.CaseAssessmentDetail, ITSService.AssessmentService.CaseAssessmentDetail>();
            Mapper.CreateMap<ITSService.AssessmentService.CaseAssessmentDetail, ITS.Domain.Models.CaseAssessmentDetail>();

            Mapper.CreateMap<ITS.Domain.Models.CaseAssessmentModel.CaseTreatmentPricing, ITSService.CaseService.CaseTreatmentPricing>();
            Mapper.CreateMap<ITSService.CaseService.CaseTreatmentPricing, ITS.Domain.Models.CaseAssessmentModel.CaseTreatmentPricing>();

            Mapper.CreateMap<ITS.Domain.Models.CaseAssessmentModel.CaseBespokeServicePricing, ITSService.CaseService.CaseBespokeServicePricing>();
            Mapper.CreateMap<ITSService.CaseService.CaseBespokeServicePricing, ITS.Domain.Models.CaseAssessmentModel.CaseBespokeServicePricing>();

            //Mapper.CreateMap<ITS.Domain.Models.ReferrerProjectTreatment, ITSService.ReferrerService.ReferrerProjectTreatment>();
            //Mapper.CreateMap<ITSService.ReferrerService.ReferrerProjectTreatment, ITS.Domain.Models.ReferrerProjectTreatment>();

            //Mapper.CreateMap<ITSService.ReferrerService.ReferrerPricingValue, ITS.Domain.ViewModels.ReferrerPricingValue>();
            //Mapper.CreateMap<ITS.Domain.ViewModels.ReferrerPricingValue, ITSService.ReferrerService.ReferrerPricingValue>();

            Mapper.CreateMap<ITSService.CaseService.TreatmentReferrerSupplierPricing, ITS.Domain.Models.CaseAssessmentModel.TreatmentReferrerSupplierPricing>();
            Mapper.CreateMap<ITS.Domain.Models.CaseAssessmentModel.TreatmentReferrerSupplierPricing, ITSService.CaseService.TreatmentReferrerSupplierPricing>();


            //Mapper.CreateMap<ITS.Domain.Models.CaseAssessmentModel.CaseAssessment, ITSService.AssessmentService.CaseAssessment>();
            //Mapper.CreateMap<ITSService.AssessmentService.CaseAssessment, ITS.Domain.Models.CaseAssessmentModel.CaseAssessment>();
            Mapper.CreateMap<ITS.Domain.Models.CaseAssessmentModel.CaseAssessment, ITSService.AssessmentService.CaseAssessment>()
               .ForMember(assessment => assessment.CaseAssessmentProposedTreatmentMethods, opt => opt.MapFrom(src => src.ProposedTreatmentMethodIDs != null ? src.ProposedTreatmentMethodIDs.Select(proposedMethod => new ITSService.AssessmentService.CaseAssessmentProposedTreatmentMethod() { CaseID = src.CaseID, ProposedTreatmentMethodID = proposedMethod }) : new List<ITSService.AssessmentService.CaseAssessmentProposedTreatmentMethod>()));


            Mapper.CreateMap<ITSService.AssessmentService.CaseAssessment, ITS.Domain.Models.CaseAssessmentModel.CaseAssessment>()
                .ForMember(assessment => assessment.ProposedTreatmentMethodIDs, opt => opt.MapFrom(src => src.CaseAssessmentProposedTreatmentMethods.Select(proposedMethod => proposedMethod.ProposedTreatmentMethodID)));

            Mapper.CreateMap<ITS.Domain.Models.CaseAssessmentModel.PsychologicalFactor, ITSService.AssessmentService.PsychologicalFactor>();
            Mapper.CreateMap<ITSService.AssessmentService.PsychologicalFactor, ITS.Domain.Models.CaseAssessmentModel.PsychologicalFactor>();

            Mapper.CreateMap<ITS.Domain.Models.CaseAssessmentModel.PatientImpact, ITSService.AssessmentService.PatientImpact>();
            Mapper.CreateMap<ITSService.AssessmentService.PatientImpact, ITS.Domain.Models.CaseAssessmentModel.PatientImpact>();

            Mapper.CreateMap<ITS.Domain.Models.CaseAssessmentModel.PatientImpactValue, ITSService.AssessmentService.PatientImpactValue>();
            Mapper.CreateMap<ITSService.AssessmentService.PatientImpactValue, ITS.Domain.Models.CaseAssessmentModel.PatientImpactValue>();

            Mapper.CreateMap<ITS.Domain.Models.CaseAssessmentModel.PatientWorkstatus, ITSService.AssessmentService.PatientWorkstatus>();
            Mapper.CreateMap<ITSService.AssessmentService.PatientWorkstatus, ITS.Domain.Models.CaseAssessmentModel.PatientWorkstatus>();

            Mapper.CreateMap<ITS.Domain.Models.CaseAssessmentModel.PatientImpactOnWork, ITSService.AssessmentService.PatientImpactOnWork>();
            Mapper.CreateMap<ITSService.AssessmentService.PatientImpactOnWork, ITS.Domain.Models.CaseAssessmentModel.PatientImpactOnWork>();

            Mapper.CreateMap<ITS.Domain.Models.PatientLevelOfRecovery, ITSService.AssessmentService.PatientLevelOfRecovery>();
            Mapper.CreateMap<ITSService.AssessmentService.PatientLevelOfRecovery, ITS.Domain.Models.CaseAssessmentModel.PatientLevelOfRecovery>();

            Mapper.CreateMap<ITS.Domain.Models.CaseAssessmentModel.ProposedTreatmentMethod, ITSService.AssessmentService.ProposedTreatmentMethod>();
            Mapper.CreateMap<ITSService.AssessmentService.ProposedTreatmentMethod, ITS.Domain.Models.CaseAssessmentModel.ProposedTreatmentMethod>();

            Mapper.CreateMap<ITS.Domain.Models.CaseAssessmentModel.TreatmentCategoriesBespokeService, ITSService.AssessmentService.TreatmentCategoriesBespokeService>();
            Mapper.CreateMap<ITSService.AssessmentService.TreatmentCategoriesBespokeService, ITS.Domain.Models.CaseAssessmentModel.TreatmentCategoriesBespokeService>();

            Mapper.CreateMap<ITS.Domain.Models.CaseAssessmentModel.PatientRole, ITSService.LookUpService.PatientRole>();
            Mapper.CreateMap<ITSService.LookUpService.PatientRole, ITS.Domain.Models.CaseAssessmentModel.PatientRole>();
            Mapper.CreateMap<ITS.Domain.Models.PatientRole, ITSService.LookUpService.PatientRole>();
            Mapper.CreateMap<ITSService.LookUpService.PatientRole, ITS.Domain.Models.PatientRole>();

            // TODO: Move Model from Assessment to Case
            Mapper.CreateMap<ITS.Domain.Models.CaseAssessmentModel.CaseTreatmentPricingType, ITSService.CaseService.CaseTreatmentPricingType>();
            Mapper.CreateMap<ITSService.CaseService.CaseTreatmentPricingType, ITS.Domain.Models.CaseAssessmentModel.CaseTreatmentPricingType>();


            Mapper.CreateMap<ITS.Domain.Models.CaseAssessmentModel.CaseBespokeServicePricingType, ITSService.CaseService.CaseBespokeServicePricingType>();
            Mapper.CreateMap<ITSService.CaseService.CaseBespokeServicePricingType, ITS.Domain.Models.CaseAssessmentModel.CaseBespokeServicePricingType>();

            Mapper.CreateMap<ITS.Domain.Models.CaseAssessmentModel.ReferrerAndSupplierPricing, ITSService.CaseService.ReferrerAndSupplierPricing>();
            Mapper.CreateMap<ITSService.CaseService.ReferrerAndSupplierPricing, ITS.Domain.Models.CaseAssessmentModel.ReferrerAndSupplierPricing>();


            Mapper.CreateMap<ITS.Domain.Models.CaseAssessmentModel.CaseVAT, ITSService.CaseService.CaseVAT>();
            Mapper.CreateMap<ITSService.CaseService.CaseVAT, ITS.Domain.Models.CaseAssessmentModel.CaseVAT>();

            Mapper.CreateMap<ITS.Domain.Models.CaseAssessmentModel.CaseAssessmentDocument, ITSService.CaseService.CaseDocument>();


            #endregion

            #region New Case Mappings
            Mapper.CreateMap<ITS.Domain.Models.CaseModel.CaseStopReason, ITSService.CaseService.CaseStopReason>();
            Mapper.CreateMap<ITSService.CaseService.CaseStopReason, ITS.Domain.Models.CaseModel.CaseStopReason>();
            #endregion

            #region New Case search Mappings
            Mapper.CreateMap<ITS.Domain.Models.CaseSearch.CasePatientTreatmentWorkflow, ITSService.CaseService.CasePatientTreatmentWorkflow>();
            Mapper.CreateMap<ITSService.CaseService.CasePatientTreatmentWorkflow, ITS.Domain.Models.CaseSearch.CasePatientTreatmentWorkflow>();

            Mapper.CreateMap<ITS.Domain.Models.CaseSearch.CaseNote, ITSService.CaseService.CaseNote>();
            Mapper.CreateMap<ITSService.CaseService.CaseNote, ITS.Domain.Models.CaseSearch.CaseNote>();
            Mapper.CreateMap<ITS.Domain.Models.CaseSearch.CaseHistoryUser, ITSService.CaseService.CaseHistoryUser>();
            Mapper.CreateMap<ITSService.CaseService.CaseHistoryUser, ITS.Domain.Models.CaseSearch.CaseHistoryUser>();

            Mapper.CreateMap<ITS.Domain.Models.CaseSearch.CasePatientReferrerSupplierWorkflow, ITSService.CaseService.CasePatientReferrerSupplierWorkflow>();
            Mapper.CreateMap<ITSService.CaseService.CasePatientReferrerSupplierWorkflow, ITS.Domain.Models.CaseSearch.CasePatientReferrerSupplierWorkflow>();


            Mapper.CreateMap<ITS.Domain.Models.CaseSearch.CaseNoteUser, ITSService.CaseService.CaseNoteUser>();
            Mapper.CreateMap<ITSService.CaseService.CaseNoteUser, ITS.Domain.Models.CaseSearch.CaseNoteUser>();

            Mapper.CreateMap<ITS.Domain.Models.CaseSearch.CaseNoteUser, ITSService.CaseService.CaseNote>();
            Mapper.CreateMap<ITSService.CaseService.CaseNote, ITS.Domain.Models.CaseSearch.CaseNoteUser>();

            Mapper.CreateMap<ITS.Domain.Models.CaseSearch.CaseDocumentUser, ITSService.CaseService.CaseDocumentUser>();
            Mapper.CreateMap<ITSService.CaseService.CaseDocumentUser, ITS.Domain.Models.CaseSearch.CaseDocumentUser>();

            Mapper.CreateMap<ITS.Domain.Models.CaseSearch.CaseDocumentUser, ITSService.CaseService.CaseDocument>();
            Mapper.CreateMap<ITSService.CaseService.CaseDocument, ITS.Domain.Models.CaseSearch.CaseDocumentUser>();

            Mapper.CreateMap<ITS.Domain.Models.SolicitorModel.Solicitor, ITSService.SolicitorService.Solicitor>();
            Mapper.CreateMap<ITSService.SolicitorService.Solicitor, ITS.Domain.Models.SolicitorModel.Solicitor>();

            Mapper.CreateMap<ITS.Domain.Models.SolicitorModel.Solicitor, ITSService.CaseService.Solicitor>();
            Mapper.CreateMap<ITSService.CaseService.Solicitor, ITS.Domain.Models.SolicitorModel.Solicitor>();
            #endregion

            #region Email Type Value
            Mapper.CreateMap<ITSService.ReferrerService.EmailTypeValue, ITS.Domain.Models.EmailTypeValue>();
            Mapper.CreateMap<ITSService.CaseService.CasePatientReferrer, ITS.Domain.Models.CaseModel.CasePatientReferrer>();
            Mapper.CreateMap<ITSService.SupplierService.PriceAverage, ITS.Domain.Models.CaseAssessmentModel.PriceAverage>();
            Mapper.CreateMap<ITS.Domain.Models.CaseAssessmentModel.PriceAverage, ITSService.SupplierService.PriceAverage>();
            Mapper.CreateMap<ITSService.PatientService.Patient, ITS.Domain.Models.PatientModel.Patient>();
            Mapper.CreateMap<ITS.Domain.Models.PatientModel.Patient, ITSService.PatientService.Patient>();
            #endregion

            #region Invoice Mappings

            Mapper.CreateMap<ITS.Domain.Models.InvoiceModel.Invoice, ITSService.FinanceService.Invoice>();
            Mapper.CreateMap<ITSService.FinanceService.Invoice, ITS.Domain.Models.InvoiceModel.Invoice>();

            Mapper.CreateMap<ITS.Domain.Models.InvoiceModel.CaseInvoicePatientReferrer, ITSService.FinanceService.CaseInvoicePatientReferrer>();
            Mapper.CreateMap<ITSService.FinanceService.CaseInvoicePatientReferrer, ITS.Domain.Models.InvoiceModel.CaseInvoicePatientReferrer>();

            Mapper.CreateMap<ITS.Domain.Models.InvoiceModel.InvoiceCollectionAction, ITSService.FinanceService.InvoiceCollectionAction>();
            Mapper.CreateMap<ITSService.FinanceService.InvoiceCollectionAction, ITS.Domain.Models.InvoiceModel.InvoiceCollectionAction>();

            Mapper.CreateMap<ITS.Domain.Models.InvoiceModel.InvoiceCollectionActionUserName, ITSService.FinanceService.InvoiceCollectionAction>();
            Mapper.CreateMap<ITSService.FinanceService.InvoiceCollectionActionUserName, ITS.Domain.Models.InvoiceModel.InvoiceCollectionActionUserName>();

            Mapper.CreateMap<ITS.Domain.Models.InvoiceModel.InvoicePayment, ITSService.FinanceService.InvoicePayment>();
            Mapper.CreateMap<ITSService.FinanceService.InvoicePayment, ITS.Domain.Models.InvoiceModel.InvoicePayment>();

            Mapper.CreateMap<ITS.Domain.Models.InvoiceModel.InvoicePaymentUserName, ITSService.FinanceService.InvoicePayment>();
            Mapper.CreateMap<ITSService.FinanceService.InvoicePaymentUserName, ITS.Domain.Models.InvoiceModel.InvoicePaymentUserName>();
          
            Mapper.CreateMap<ITS.Domain.ViewModels.InvoiceViewModel.PagedCaseInvoicePatientReferrer, ITSService.FinanceService.PagedCaseInvoicePatientReferrer>();
            Mapper.CreateMap<ITSService.FinanceService.PagedCaseInvoicePatientReferrer, ITS.Domain.ViewModels.InvoiceViewModel.PagedCaseInvoicePatientReferrer>();

            #endregion

            #region Evaluate Delegated Authorisation Cost
            Mapper.CreateMap<ITSService.CaseService.EvaluateDelegatedAuthorisationCost, ITS.Domain.Models.EvaluateDelegatedAuthorisationCost>();
            Mapper.CreateMap<ITS.Domain.Models.EvaluateDelegatedAuthorisationCost, ITSService.CaseService.EvaluateDelegatedAuthorisationCost>();


            Mapper.CreateMap<ITS.Domain.Models.CaseAssessmentModel.AffectedArea, ITSService.LookUpService.AffectedArea>();
            Mapper.CreateMap<ITSService.LookUpService.AffectedArea, ITS.Domain.Models.CaseAssessmentModel.AffectedArea>();
            Mapper.CreateMap<ITS.Domain.Models.AffectedArea, ITSService.LookUpService.AffectedArea>();
            Mapper.CreateMap<ITSService.LookUpService.AffectedArea, ITS.Domain.Models.AffectedArea>();


            Mapper.CreateMap<ITS.Domain.Models.CaseAssessmentModel.RestrictionRange, ITSService.LookUpService.RestrictionRange>();
            Mapper.CreateMap<ITSService.LookUpService.RestrictionRange, ITS.Domain.Models.CaseAssessmentModel.RestrictionRange>();
            Mapper.CreateMap<ITS.Domain.Models.RestrictionRange, ITSService.LookUpService.RestrictionRange>();
            Mapper.CreateMap<ITSService.LookUpService.RestrictionRange, ITS.Domain.Models.RestrictionRange>();

            Mapper.CreateMap<ITS.Domain.Models.CaseAssessmentModel.StrengthTesting, ITSService.LookUpService.StrengthTesting>();
            Mapper.CreateMap<ITSService.LookUpService.StrengthTesting, ITS.Domain.Models.CaseAssessmentModel.StrengthTesting>();

            Mapper.CreateMap<ITS.Domain.Models.StrengthTesting, ITSService.LookUpService.RestrictionRange>();
            Mapper.CreateMap<ITSService.LookUpService.StrengthTesting, ITS.Domain.Models.StrengthTesting>();


            Mapper.CreateMap<ITS.Domain.Models.CaseAssessmentModel.SymptomDescription, ITSService.LookUpService.SymptomDescription>();
            Mapper.CreateMap<ITSService.LookUpService.SymptomDescription, ITS.Domain.Models.CaseAssessmentModel.SymptomDescription>();

            Mapper.CreateMap<ITS.Domain.Models.SymptomDescription, ITSService.LookUpService.SymptomDescription>();
            Mapper.CreateMap<ITSService.LookUpService.SymptomDescription, ITS.Domain.Models.SymptomDescription>();

            Mapper.CreateMap<ITS.Domain.Models.CaseAssessmentModel.TreatmentPeriodType, ITSService.LookUpService.TreatmentPeriodType>();
            Mapper.CreateMap<ITSService.LookUpService.TreatmentPeriodType, ITS.Domain.Models.CaseAssessmentModel.TreatmentPeriodType>();

            Mapper.CreateMap<ITS.Domain.Models.TreatmentPeriodType, ITSService.LookUpService.TreatmentPeriodType>();
            Mapper.CreateMap<ITSService.LookUpService.TreatmentPeriodType, ITS.Domain.Models.TreatmentPeriodType>();


            Mapper.CreateMap<ITS.Domain.Models.CaseAssessmentPatientInjuryBL, ITSService.AssessmentService.CaseAssessmentPatientInjuryBL>();
            Mapper.CreateMap<ITSService.AssessmentService.CaseAssessmentPatientInjuryBL, ITS.Domain.Models.CaseAssessmentPatientInjuryBL>();

            Mapper.CreateMap<ITSService.CaseService.IntialAssessmentReportDetail, ITS.Domain.Models.IntialAssessmentReportDetail>();
            Mapper.CreateMap<ITS.Domain.Models.IntialAssessmentReportDetail, ITSService.CaseService.IntialAssessmentReportDetail>();

            Mapper.CreateMap<ITSService.AssessmentService.ReferrerCaseAssessmentModification, ITS.Domain.Models.CaseAssessmentModel.ReferrerCaseAssessmentModification>();
            Mapper.CreateMap<ITS.Domain.Models.CaseAssessmentModel.ReferrerCaseAssessmentModification, ITSService.AssessmentService.ReferrerCaseAssessmentModification>();

            Mapper.CreateMap<ITS.Domain.Models.TreatmentSessionByCaseID, ITSService.CaseService.TreatmentSessionByCaseID>();
            Mapper.CreateMap<ITSService.CaseService.TreatmentSessionByCaseID, ITS.Domain.Models.TreatmentSessionByCaseID>();
            #endregion

            Mapper.CreateMap<ITS.Domain.Models.ReferrerUserDetail, ITSService.ReferrerService.ReferrerUserDetail>();
            Mapper.CreateMap<ITSService.ReferrerService.ReferrerUserDetail, ITS.Domain.Models.ReferrerUserDetail>();

            Mapper.CreateMap<ITS.Domain.Models.UpdateReferrerGroup, ITSService.ReferrerService.UpdateReferrerGroup>();
            Mapper.CreateMap<ITSService.ReferrerService.UpdateReferrerGroup, ITS.Domain.Models.UpdateReferrerGroup>();

            Mapper.CreateMap<ITS.Domain.Models.Gip, ITSService.CaseService.Gip>();
            Mapper.CreateMap<ITSService.CaseService.Gip, ITS.Domain.Models.Gip>();

            Mapper.CreateMap<ITS.Domain.Models.Iip, ITSService.CaseService.Iip>();
            Mapper.CreateMap<ITSService.CaseService.Iip, ITS.Domain.Models.Iip>();

            Mapper.CreateMap<ITS.Domain.Models.Tpd, ITSService.CaseService.Tpd>();
            Mapper.CreateMap<ITSService.CaseService.Tpd, ITS.Domain.Models.Tpd>();

            Mapper.CreateMap<ITS.Domain.Models.ElRehab, ITSService.CaseService.ElRehab>();
            Mapper.CreateMap<ITSService.CaseService.ElRehab, ITS.Domain.Models.ElRehab>();
        }
    }
}
