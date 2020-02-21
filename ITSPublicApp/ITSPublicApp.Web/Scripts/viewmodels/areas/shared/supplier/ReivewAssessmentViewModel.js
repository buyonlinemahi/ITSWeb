/// <reference path="../../../../knockout-3.0.0.js" />


ko.validation.rules["validArray"] = {
    validator: function (arr, bool) {
        if (!arr || typeof arr !== "object" || !(arr instanceof Array)) {
            throw "[validArray] Parameter must be an array";
        }
        return bool === (arr.filter(function (element) {
            return ko.validation.group(ko.utils.unwrapObservable(element))().length !== 0;
        }).length === 0);
    },
    message: "\n'{0}'"
};


function CaseAssessmentPatientInjury(caseAssessmentDetailID, data) {
    var self = this;
    this.CaseAssessmentDetailID = ko.observable(caseAssessmentDetailID);
    this.CaseAssessmentPatientInjuryID = ko.observable();
    this.AffectedArea = ko.observable();
    this.Score = ko.observable();
    this.Restriction = ko.observable();
    this.SymptomDescriptionID = ko.observable();
    this.StrengthTestingID = ko.observable();
    this.RestrictionRangeID = ko.observable();
    this.AffectedAreaID = ko.observable();
    this.OtherSymptomDesciption = ko.observable();
    ko.mapping.fromJS(data, {}, self);
};

function CaseAssessmentPatientInjuryBL(caseAssessmentDetailID, data) {
    var self = this;
    self.CaseAssessmentDetailID = ko.observable(caseAssessmentDetailID);
    self.CaseAssessmentPatientInjuryID = ko.observable();
    self.AffectedArea = ko.observable();
    self.Score = ko.observable();
    self.Restriction = ko.observable();
    self.SymptomDescriptionID = ko.observable();
    self.StrengthTestingID = ko.observable();
    self.RestrictionRangeID = ko.observable();
    self.AffectedAreaID = ko.observable();
    self.SymptomDescriptionName = ko.observable();
    self.StrengthTestingDescription = ko.observable();
    self.AffectedAreaDescription = ko.observable();
    self.RestrictionRangeDescription = ko.observable();
    self.OtherSymptomDesciption = ko.observable();

    ko.mapping.fromJS(data, {}, self);

}

function CaseAssessmentPatientImpact(data, patientImpactName) {
    var self = this;

    self.CaseAssessmentPatientImpactID = ko.observable();
    self.PatientImpactID = ko.observable();
    self.PatientImpactValueID = ko.observable().extend({ required: { message: '\Patient Impact is required.' } });
    self.Comment = ko.observable();
    self.CaseAssessmentDetailID = ko.observable();
    self.PatientImpactName = ko.observable(patientImpactName);
    ko.mapping.fromJS(data, {}, self);

};

function CaseBespokeServicePricing(data, dateOfServiceEnable, patientDidNotAttendEnable, wasAbandonedEnable) {
    var self = this;
    self.CaseBespokeServiceID = ko.observable();
    self.CaseID = ko.observable();
    self.TreatmentCategoryBespokeServiceID = ko.observable();
    self.ReferrerPrice = ko.observable();
    self.SupplierPrice = ko.observable();
    self.DateOfService = ko.observable();
    self.PatientDidNotAttend = ko.observable(false);
    self.DateOfService = ko.observable().extend({ parseJsonDate: null });
    self.WasAbandoned = ko.observable(false);
    self.IsComplete = ko.observable(false);

    self.DateOfServiceEnable = ko.observable(dateOfServiceEnable);
    self.PatientDidNotAttendEnable = ko.observable(patientDidNotAttendEnable);
    self.WasAbandonedEnable = ko.observable(wasAbandonedEnable);


    self.BespokeServiceName = ko.observable();
    ko.mapping.fromJS(data, {}, self);
};

function CaseTreatmentPricingType(data, dateOfServiceEnable, patientDidNotAttendEnable, wasAbandonedEnable) {
    var self = this;
    self.CaseTreatmentPricingID = ko.observable(data.CaseTreatmentPricingID);
    self.CaseID = ko.observable(data.CaseID);
    self.ReferrerPricingID = ko.observable(data.ReferrerPricingID);
    self.SupplierPriceID = ko.observable(data.SupplierPriceID);
    self.PricingTypeName = ko.observable(data.PricingTypeName);
    self.ReferrerPrice = ko.observable(data.ReferrerPrice);
    self.SupplierPrice = ko.observable(data.SupplierPrice);

    self.PatientDidNotAttend = ko.observable(false);

    if (data.DateOfService != null) {
        self.DateOfService = ko.observable(moment(data.DateOfService).format("DD/MM/YYYY")).extend({ parseJsonDate: null });
    }
    else {
        self.DateOfService = ko.observable(null).extend({ parseJsonDate: null });
    }
    if (data.PatientDidNotAttendDate != null) {
        self.PatientDidNotAttendDate = ko.observable(moment(data.PatientDidNotAttendDate).format("DD/MM/YYYY")).extend({ parseJsonDate: null });
        self.PatientDidNotAttend(true);
    }
    else {
        self.PatientDidNotAttendDate = ko.observable(null).extend({ parseJsonDate: null });
        self.PatientDidNotAttend(false);
    }

    self.WasAbandoned = ko.observable(false);
    self.IsComplete = ko.observable(false);

    self.DateOfServiceEnable = ko.observable(dateOfServiceEnable);
    self.PatientDidNotAttendEnable = ko.observable(patientDidNotAttendEnable);
    self.WasAbandonedEnable = ko.observable(wasAbandonedEnable);
    var mappingOptionsCaseTreatment = {
        'DateOfService': {
            create: function (options) {
                if (options.data != null)
                    return moment(options.data).format("DD/MM/YYYY");
            }
        }
    }
    ko.mapping.fromJS(data, mappingOptionsCaseTreatment, self);
};

ko.validation.configure({
    insertMessages: false,
    grouping: { deep: false }
});
function ReviewAssessmentViewModel() {

    var self = this;

    self.IsAccepted = ko.observable(true);
    var mappingOptions = {
        'DateOfInitialAssessment': {
            create: function (options) {
                if (options.data != null)
                    return moment(options.data).format("DD/MM/YYYY");
            }
        }
    }
    // Pricing Matrix


    $(document).ready(function () {     
        $('#sessionattended').bind("cut copy paste", function (e) {          
            e.preventDefault();
        });
        $('#sessionfailedtoattend').bind("cut copy paste", function (e) {
            e.preventDefault();
        });
    });

    self.SymptomDescriptionID = ko.observable();
    self.StrengthTestingID = ko.observable();
    self.RestrictionRangeID = ko.observable();
    self.AffectedAreaID = ko.observable();

    //Intial Assesscement incident and diagnosis/injury section deop downs
    self.SymptomDescriptions = ko.observableArray([]);
    self.StrengthTestings = ko.observableArray([]);
    self.RestrictionRanges = ko.observableArray([]);
    self.AffectedAreas = ko.observableArray([]);

    self.CaseTreatmentPricingTypeArray = ko.observableArray([]);
    self.CaseBespokeServicePricingsArray = ko.observableArray([]);
    self.Practitioners = ko.observableArray([]);
    self.BirthDate = ko.observable();
    self.CaseSubmittedDate = ko.observable();
    self.InjuryDate = ko.observable();
    self.PractitionerID = ko.observable();
    self.CaseID = ko.observable();
    self.EncryptDecryptCaseID = ko.observable();
    self.CaseAssessmentDetailID = ko.observable();
    // Patient Consent 
    self.HasPatientConsentForm = ko.observable().extend({ required: { message: '\nHas the patient completed a consent form is required.' } });
    self.IsPatientDischarge = ko.observable().extend({ required: { message: '\nAre you discharging the patient is required.' } });
    // Injury and Symptom Details
    self.IncidentAndDiagnosisDescription = ko.observable().extend({ required: { message: '\nInjury and Symptom Details description is required.' } });
    self.CaseAssessmentPatientInjuriesArray = ko.observableArray().extend({ validArray: { params: true, message: '\nEach entry under diagnosis/injury should have a Symptom Description / Affected, Restriction to range of movement and Pain Score must less than or equal to 10.' } });
    self.NeuralSymptomDescription = ko.observable();
    // Impact on work
    self.PatientOccupation = ko.observable().extend({ required: { message: "\nPatient occupation is required." } });
    self.PatientRoleID = ko.observable();
    self.HasThePatientHadTimeOff = ko.observable().extend({ required: { message: '\nHas the patient had any time off work is required.' } });
    self.AbsentPeriod = ko.observable().extend({ required: { onlyIf: function () { return self.HasThePatientHadTimeOff() == 'true'; }, message: "\nIf so, how long have then been absent?." }, number: { message: '\nNot a number, Please enter number  in how many weeks have they been absent' } });
    self.AbsentPeriodDurationID = ko.observable().extend({ required: { onlyIf: function () { return self.HasThePatientHadTimeOff() == 'true'; }, message: '\n how many weeks have they been absent Duratiopn is required.' } });
    self.HasThePatientReturnedToWork = ko.observable().extend({ required: { message: '\n Has the patient returned to work is required.' } });
    self.PatientImpactOnWorkID = ko.observable().extend({ required: { message: '\n Rate the impact of the remaining symptoms on the patients work is required.' } });
    self.PatientWorkstatusID = ko.observable().extend({ required: { message: '\n At what capacity is the patient working is required.' } });
    // Remaining Impact on Lifestyle
    self.CaseAssessmentPatientImpacts = ko.observableArray();

    self.PatientDateOfReturn = ko.observable().extend({ required: { message: "\nPatient Date Of Return is required.", onlyIf: function () { return self.HasThePatientReturnedToWork() === "true" } } });
    self.IsPatientReturnToPreInjuryDuties = ko.observable();
    self.PatientRecommendationReturn = ko.observable().extend({ required: { message: "\nRecommendations to Aide Return to Work is required.", onlyIf: function () { return self.HasThePatientReturnedToWork() === "true" } } });
    self.PatientPreInjuryDutiesDate = ko.observable().extend({ required: { message: "\nReturn to their pre-injury Date is required.", onlyIf: function () { return self.IsPatientReturnToPreInjuryDuties() === "true" } } });
    self.MainFactors = ko.observable().extend({ required: { message: "\nMain Factor is required.", onlyIf: function () { return self.PatientWorkstatusID() != 1 } } });

    self.PatientWorkstatuses = ko.observableArray().extend({ validArray: { params: true, message: '\n Current Work Status is required' } });
    // Treatment Recommendation
    self.SessionsPatientAttended = ko.observable();
    self.DatesOfSessionAttended = ko.observable().extend({ required: { message: '\nWhat were the dates of the attended sessions is required.' } });
    self.SessionsPatientFailedToAttend = ko.observable();
    self.FollowingTreatmentPatientLevelOfRecoveryID = ko.observable();
    self.HasCompliedHomeExerciseProgramme = ko.observable().extend({ required: { message: '\n patient been given a home exercise programme is required.' } });
    // Further Treatment
    self.IsFurtherTreatmentRecommended = ko.observable().extend({ required: { onlyIf: function () { return self.IsPatientDischarge() != 'true'; }, message: '\n Do you recommend any further treatment is required.' } });

    self.PatientTreatmentPeriodTypeID = ko.observable();
    self.PatientTreatmentPeriodDurationID = ko.observable().extend({ required: { message: "\nOver what period should these be carried out duration is required.", onlyIf: function () { return self.PatientTreatmentPeriodTypeID() === 4 && self.IsPatientDischarge() != 'true' } } });
    self.PatientTreatmentPeriod = ko.observable().extend({ required: { message: "\nOver what period should these be carried out is required.", onlyIf: function () { return self.PatientTreatmentPeriodTypeID() === 4 && self.IsPatientDischarge() != 'true' } }, number: { message: '\nPlease enter a valid number for "Over what period should these be carried out?"' } });

    self.selectedPatientLevelOfRecoveryID = ko.observable().extend({ required: { message: '\n What level of recovery do you anticipate the patient will make following this additional treatment is required.' } });
    
    
    self.IsFurtherInvestigationOrOnwardReferralRequired = ko.observable().extend({ required: { onlyIf: function () { return self.IsPatientDischarge() != 'true'; }, message: '\n Does the patient require any further investigation or onward referral.' } });


    // Additional Information
    self.AdditionalInformation = ko.observable();


    // Page Dropdowns
    self.Duration = ko.observableArray([]);
    self.PatientRole = ko.observableArray();
    // Additional variables
    self.DeniedMessageCheck = ko.observable(false);
    self.DeniedMessage = ko.observable();
    self.PatientRecommendedTreatmentSessionsDetail = ko.observable();
    self.PatientRecommendedTreatmentSessions = ko.observable();
    self.AnticipatedDateOfDischarge = ko.observable();
    self.AssessmentAuthorisationID = ko.observable();
    self.AssessmentServiceID = ko.observable();
    ////----Patient Impacts----------------------///
    self.PatientImpacts = ko.observableArray();
    self.PatientImpactValues = ko.observableArray();
    self.PatientImpactValues = ko.observableArray([self.PatientImpactValues(0)]);
    self.selectedItemPatientImpactValues = ko.observable(0);
    // New Fields Added
    self.RelevantTestUndertaken = ko.observable();
    self.hdRelevantTestUndertaken = ko.observable();

    self.Init = function (model) {
        if (model != null) {
            self.PatientTreatmentPeriodTypeID(model.CaseAssessment.CaseAssessmentDetail.TreatmentPeriodTypeID);
            self.showHideTreatmentPeriodOther(self.PatientTreatmentPeriodTypeID());
            self.PatientTreatmentPeriodDurationID(model.CaseAssessment.CaseAssessmentDetail.PatientTreatmentPeriodDurationID != null ? model.CaseAssessment.CaseAssessmentDetail.PatientTreatmentPeriodDurationID : 'nullable');

            self.IsAccepted(model.CaseAssessment.IsAccepted);
            self.PatientImpactValues(model.PatientImpactValues);
            if (model.CaseAssessment.IsSaved) {
                $("#btnSubmit").removeAttr("disabled");
            }

            if (model.CaseAssessment.CaseAssessmentDetail.AssessmentServiceID) {
                self.AbsentPeriod(model.CaseAssessment.CaseAssessmentDetail.AbsentPeriod);
                self.AbsentPeriodDurationID(model.CaseAssessment.CaseAssessmentDetail.AbsentPeriodDurationID);
                self.SessionsPatientAttended(model.CaseAssessment.CaseAssessmentDetail.SessionsPatientAttended);
                self.PatientTreatmentPeriod(model.CaseAssessment.CaseAssessmentDetail.PatientTreatmentPeriod);
                self.AdditionalInformation(model.CaseAssessment.CaseAssessmentDetail.AdditionalInformation);
                self.PatientImpactOnWorkID(model.CaseAssessment.CaseAssessmentDetail.PatientImpactOnWorkID != null ? model.CaseAssessment.CaseAssessmentDetail.PatientImpactOnWorkID : 'nullable');
                self.PatientWorkstatusID(model.CaseAssessment.CaseAssessmentDetail.PatientWorkstatusID);
                self.selectedPatientLevelOfRecoveryID(model.CaseAssessment.CaseAssessmentDetail.PatientLevelOfRecoveryID != null ? model.CaseAssessment.CaseAssessmentDetail.PatientLevelOfRecoveryID : 'nullable');
                self.HasCompliedHomeExerciseProgramme(model.CaseAssessment.CaseAssessmentDetail.HasCompliedHomeExerciseProgramme != null ? model.CaseAssessment.CaseAssessmentDetail.HasCompliedHomeExerciseProgramme.toString() : 'nullable');
                if (model.CaseAssessment.CaseAssessmentDetail.HasCompliedHomeExerciseProgramme == null)
                {
                    $("#HomeNotApplicable").prop("checked", true);
                }
                
                self.HasThePatientHadTimeOff(model.CaseAssessment.CaseAssessmentDetail.HasThePatientHadTimeOff != null ? model.CaseAssessment.CaseAssessmentDetail.HasThePatientHadTimeOff.toString() : 'nullable');
                
                if (model.CaseAssessment.CaseAssessmentDetail.HasThePatientHadTimeOff == null)
                {
                    $("#TimeNotApplicable").prop("checked", true);
                }
                self.HasThePatientReturnedToWork(model.CaseAssessment.CaseAssessmentDetail.HasThePatientReturnedToWork != null ? model.CaseAssessment.CaseAssessmentDetail.HasThePatientReturnedToWork.toString() : 'nullable');
                if (model.CaseAssessment.CaseAssessmentDetail.HasThePatientReturnedToWork == null)
                {
                    $("#ReturnedNotApplicable").prop("checked", true);
                }
                self.FollowingTreatmentPatientLevelOfRecoveryID(model.CaseAssessment.CaseAssessmentDetail.FollowingTreatmentPatientLevelOfRecoveryID != null ? model.CaseAssessment.CaseAssessmentDetail.FollowingTreatmentPatientLevelOfRecoveryID : 'nullable');
                self.HasPatientConsentForm(model.CaseAssessment.HasPatientConsentForm != null ? model.CaseAssessment.HasPatientConsentForm.toString() : 'nullable');
                self.IsPatientDischarge(model.CaseAssessment.IsPatientDischarge != null ? model.CaseAssessment.IsPatientDischarge.toString() : 'nullable');
                self.IncidentAndDiagnosisDescription(model.CaseAssessment.IncidentAndDiagnosisDescription);
                self.NeuralSymptomDescription(model.CaseAssessment.NeuralSymptomDescription);
                self.hdRelevantTestUndertaken(model.CaseAssessment.RelevantTestUndertaken);
                self.RelevantTestUndertaken(model.CaseAssessment.RelevantTestUndertaken);
                self.DatesOfSessionAttended(model.CaseAssessment.CaseAssessmentDetail.DatesOfSessionAttended);
                self.SessionsPatientFailedToAttend(model.CaseAssessment.CaseAssessmentDetail.SessionsPatientFailedToAttend);
                self.IsFurtherTreatmentRecommended(model.CaseAssessment.CaseAssessmentDetail.IsFurtherTreatmentRecommended != null ? model.CaseAssessment.CaseAssessmentDetail.IsFurtherTreatmentRecommended.toString() : 'nullable');
                self.PatientRecommendedTreatmentSessions(model.CaseAssessment.CaseAssessmentDetail.PatientRecommendedTreatmentSessions);
                self.PatientRecommendedTreatmentSessionsDetail(model.CaseAssessment.CaseAssessmentDetail.PatientRecommendedTreatmentSessionsDetail);
                self.IsFurtherInvestigationOrOnwardReferralRequired(model.CaseAssessment.CaseAssessmentDetail.IsFurtherInvestigationOrOnwardReferralRequired != null ? model.CaseAssessment.CaseAssessmentDetail.IsFurtherInvestigationOrOnwardReferralRequired.toString() : 'nullable');
                self.FurtherInvestigationOrOnwardReferral(model.CaseAssessment.CaseAssessmentDetail.FurtherInvestigationOrOnwardReferral);
                self.EvidenceOfTreatmentRecommendations(model.CaseAssessment.CaseAssessmentDetail.EvidenceOfTreatmentRecommendations);

                self.DeniedMessage(model.CaseAssessment.DeniedMessage);
            }

            self.SymptomDescriptions(model.SymptomDescriptions);
            self.RestrictionRanges(model.RestrictionRanges);
            self.StrengthTestings(model.StrengthTestings);
            self.AffectedAreas(model.AffectedAreas);

            self.DeniedMessageCheck = ko.observable(true);
            self.MainFactors(model.CaseAssessment.CaseAssessmentDetail.MainFactors);
            if (self.HasThePatientReturnedToWork() == "true") {
                if (model.CaseAssessment.CaseAssessmentDetail.PatientDateOfReturn != null) {
                    self.PatientDateOfReturn(moment(model.CaseAssessment.CaseAssessmentDetail.PatientDateOfReturn).format('DD/MM/YYYY'));
                }
            }

            self.PatientRecommendationReturn(model.CaseAssessment.CaseAssessmentDetail.PatientRecommendationReturn);

            self.IsPatientReturnToPreInjuryDuties(model.CaseAssessment.CaseAssessmentDetail.IsPatientReturnToPreInjuryDuties != null ? model.CaseAssessment.CaseAssessmentDetail.IsPatientReturnToPreInjuryDuties.toString() : 'nullable');
            if (self.IsPatientReturnToPreInjuryDuties()) {
                if (model.CaseAssessment.CaseAssessmentDetail.PatientPreInjuryDutiesDate != null) {
                    self.PatientPreInjuryDutiesDate(moment(model.CaseAssessment.CaseAssessmentDetail.PatientPreInjuryDutiesDate).format('DD/MM/YYYY'));
                }
            }
            $.each(model.CaseAssessment.CaseAssessmentPatientInjuriesBL, function (index, value) {

                self.CaseAssessmentPatientInjuriesArray.push(new CaseAssessmentPatientInjury(model.CaseAssessment.CaseAssessmentDetail.CaseAssessmentDetailID, value));
            });

            if (model.CaseAssessment.CaseAssessmentPatientImpacts.length > 0) {
                $.each(model.CaseAssessment.CaseAssessmentPatientImpacts, function (index, value) {

                    self.CaseAssessmentPatientImpacts.push(new CaseAssessmentPatientImpact(value, model.PatientImpacts[index].PatientImpactName));
                });
            }
            else {

                $.each(model.PatientImpacts, function (index, value) {
                    self.CaseAssessmentPatientImpacts.push(new CaseAssessmentPatientImpact(value, value.PatientImpactName));
                });
            }

            if (model.CaseAssessment.CaseAssessmentPatientInjuriesBL.length == 0) {
                self.CaseAssessmentPatientInjuriesArray.push(new CaseAssessmentPatientInjury());
            }
            self.PractitionerID(model.CaseAssessment.CaseAssessmentDetail.PractitionerID);
            if (model.Patient.BirthDate != null) {
                self.BirthDate(moment(model.Patient.BirthDate).format('DD/MM/YYYY'));
            }
            if (model.CaseSubmittedDate != null) {
                self.CaseSubmittedDate(moment(model.CaseSubmittedDate).format('DD/MM/YYYY'));
            }
            if (model.Patient.InjuryDate != null) {
                self.InjuryDate(moment(model.Patient.InjuryDate).format('DD/MM/YYYY'));
            }
            self.AssessmentAuthorisationID(model.CaseAssessment.AssessmentAuthorisationID);
            self.AssessmentServiceID(model.CaseAssessment.AssessmentServiceID);
            if (model.CaseAssessment.AnticipatedDateOfDischarge != null) {
                self.AnticipatedDateOfDischarge(moment(model.CaseAssessment.AnticipatedDateOfDischarge).format('DD/MM/YYYY'));
            }
            self.CaseID(model.CaseAssessment.CaseID);
            self.EncryptDecryptCaseID(model.Case.EncryptDecryptCaseID);
            self.CaseAssessmentDetailID(model.CaseAssessment.CaseAssessmentDetail.CaseAssessmentDetailID);
            self.PatientRoleID(model.CaseAssessment.PatientRoleID);
            self.PatientOccupation(model.CaseAssessment.PatientOccupation);
            self.PatientTreatmentPeriodDurationID(model.CaseAssessment.CaseAssessmentDetail.PatientTreatmentPeriodDurationID);
            ko.mapping.fromJS(model, mappingOptions, self);
            ko.mapping.fromJS(model.Duration, {}, self.Duration);
            ko.mapping.fromJS(model.PatientRole, {}, self.PatientRole);
            $.each(model.CaseTreatmentPricingTypes, function (index, value) {

                if (value.DateOfService !== null) {
                    self.CaseTreatmentPricingTypeArray.push(new CaseTreatmentPricingType(value, true, false, false));
                }
                else if (value.PatientDidNotAttendDate !== null) {
                    self.CaseTreatmentPricingTypeArray.push(new CaseTreatmentPricingType(value, false, true, false));
                }
                else if (value.PatientDidNotAttend !== null) {
                    self.CaseTreatmentPricingTypeArray.push(new CaseTreatmentPricingType(value, false, true, false));
                }
                else if (value.WasAbandoned !== null) {
                    self.CaseTreatmentPricingTypeArray.push(new CaseTreatmentPricingType(value, false, false, true));
                }
                else {
                    self.CaseTreatmentPricingTypeArray.push(new CaseTreatmentPricingType(value, true, true, true));
                }

            });

            $.each(model.CaseBespokeServicePricingTypes, function (index, value) {

                if (value.DateOfService !== null) {
                    self.CaseBespokeServicePricingsArray.push(new CaseBespokeServicePricing(value, true, false, false));
                }
                else if (value.PatientDidNotAttend !== null) {
                    self.CaseBespokeServicePricingsArray.push(new CaseBespokeServicePricing(value, false, true, false));
                }
                else if (value.WasAbandoned !== null) {
                    self.CaseBespokeServicePricingsArray.push(new CaseBespokeServicePricing(value, false, false, true));
                }
                else {
                    self.CaseBespokeServicePricingsArray.push(new CaseBespokeServicePricing(value, true, true, true));
                }
            });

            CheckHasThePatientReturnedToWork(self.HasThePatientReturnedToWork());
            CheckIsPatientReturnToPreInjuryDuties(self.IsPatientReturnToPreInjuryDuties());
        }
    };


    self.IsFurtherTreatmentRecommended.subscribe(function (item) {
        if (item == 1) {
            self.PatientRecommendedTreatmentSessionsDetail = ko.observable();
            self.PatientRecommendedTreatmentSessions = ko.observable().extend({ required: { message: "\nHow many sessions do you recommend is required." }, number: { message: '\nNot a number, Please enter number  in further sessions Recomemded' } });

        }
        else {

            self.PatientRecommendedTreatmentSessions = ko.observable();
            self.PatientRecommendedTreatmentSessionsDetail = ko.observable();
        }
    });

    self.IsFurtherInvestigationOrOnwardReferralRequired.subscribe(function (item) {
        if (item == 'true') {
            self.FurtherInvestigationOrOnwardReferral = ko.observable().extend({ required: { message: "\n Please detail if the patient require any further investigation or onward referral." } });
            self.EvidenceOfTreatmentRecommendations = ko.observable().extend({ required: { message: "\n Please provide evidence of clinical reasoning in support of treatment recommendations." } });
        }
        else {
            self.FurtherInvestigationOrOnwardReferral = ko.observable();
            self.EvidenceOfTreatmentRecommendations = ko.observable();
        }
    });

    self.changeCurrentWorkStatus = function () {
        if ($("#ddlCurrentWorkStatus").val() != 1) {
            $("#divMainFactor").removeClass("hide");
        }
        else {
            $("#divMainFactor").addClass("hide");
        }
    };

    self.changeCurrentWorkStatus();

    self.showHideTreatmentPeriodOther = function (treatmentTypeID) {
        if (treatmentTypeID == 4) {
            $("#divTreatmentPeriodOther").removeClass("hide");
            $("#divSpaceHeight").removeClass("hide");
        }
        else {
            $("#divSpaceHeight").addClass("hide");
            $("#divTreatmentPeriodOther").addClass("hide");
        }
    };

    self.changeTreatmentPeriod = function () {
        self.showHideTreatmentPeriodOther($("#ddlTreatmentPeriodType").val());
    };

    self.ValidateCaseBespokeServicePricing = function (item, e) {
        var context = ko.contextFor(e.target);
        var index = context.$index();
        var DateOfBeSpokeServiceValue = $("#TxtDateOsBespokeServie" + index).val();
        switch (e.target.type) {
            case 'checkbox':
                {
                    var id = (e.target.name).split('.');
                    switch (id[1]) {
                        case 'PatientDidNotAttend':
                            {
                                if (e.target.checked) {
                                    item.PatientDidNotAttendEnable(true);
                                    item.DateOfServiceEnable(false);
                                    item.WasAbandonedEnable(false);
                                    break;
                                }
                                else {
                                    item.PatientDidNotAttendEnable(true);
                                    item.DateOfServiceEnable(true);
                                    item.WasAbandonedEnable(true);
                                    break;
                                }
                            }
                        case 'WasAbandoned':
                            {
                                if (e.target.checked) {
                                    item.WasAbandonedEnable(true);
                                    item.PatientDidNotAttendEnable(false);
                                    item.DateOfServiceEnable(false);
                                    break;
                                }
                                else {
                                    item.PatientDidNotAttendEnable(true);
                                    item.DateOfServiceEnable(true);
                                    item.WasAbandonedEnable(true);
                                    break;
                                }
                            }
                    }
                    break;

                }
            case 'text':
                {
                    if (DateOfBeSpokeServiceValue == null) {
                        item.PatientDidNotAttendEnable(true);
                        item.DateOfServiceEnable(true);
                        item.WasAbandonedEnable(true);
                    }
                    else {
                        item.DateOfServiceEnable(true);
                        item.PatientDidNotAttendEnable(false);
                        item.WasAbandonedEnable(false);
                        item.PatientDidNotAttend(false);
                        item.WasAbandoned(false);
                    }
                    if (DateOfBeSpokeServiceValue == '') {
                        item.PatientDidNotAttendEnable(true);
                        item.DateOfServiceEnable(true);
                        item.WasAbandonedEnable(true);
                    }
                    break;
                }
        }
    };

    self.ValidateCaseTreatmentPricing = function (item, e) {
        
        var context = ko.contextFor(e.target);
        var index = context.$index();
        var DateofserviceValue = $("#TxtDateOsServie" + index).val();
        switch (e.target.type) {
            case 'checkbox':
                {
                    var id = (e.target.name).split('.');
                    switch (id[1]) {

                        case 'PatientDidNotAttend':
                            {
                                $("#TxtDateOsServie" + index).val("");
                                if (e.target.checked) {
                                    item.PatientDidNotAttendEnable(true);
                                    item.PatientDidNotAttendDate(true);
                                    item.DateOfServiceEnable(false);
                                    item.WasAbandonedEnable(false);
                                    break;
                                }
                                else {
                                    item.PatientDidNotAttendEnable(false);
                                    item.PatientDidNotAttendDate(false);
                                    item.DateOfServiceEnable(true);
                                    item.WasAbandonedEnable(false);
                                    $("#TxtPatientDidNotAttendDate" + index).val("");
                                    item.PatientDidNotAttend(false);
                                    
                                    break;
                                }
                            }
                        case 'WasAbandoned':
                            {
                                $("#TxtDateOsServie" + index).val("");
                                $("#TxtPatientDidNotAttendDate" + index).val("");

                                item.PatientDidNotAttend(false);

                                if (e.target.checked) {
                                    item.WasAbandonedEnable(true);
                                    item.PatientDidNotAttendEnable(false);
                                    item.PatientDidNotAttendDate(false);
                                    item.DateOfServiceEnable(false);

                                    break;
                                }
                                else {
                                    item.PatientDidNotAttendEnable(true);
                                    item.DateOfServiceEnable(true);
                                    item.WasAbandonedEnable(true);
                                    //item.PatientDidNotAttendDate(true);
                                    break;
                                }
                            }
                    }
                    break;
                }
            case 'text':
                {
                    if (DateofserviceValue == null) {
                        item.PatientDidNotAttendEnable(true);
                        //item.PatientDidNotAttendDate(true);
                        item.DateOfServiceEnable(true);
                        item.WasAbandonedEnable(true);

                    }
                    else {
                        item.DateOfServiceEnable(true);
                        item.PatientDidNotAttendEnable(false);
                        //item.PatientDidNotAttendDate(false);
                        item.WasAbandonedEnable(false);

                    }
                    if (DateofserviceValue == '') {
                        item.PatientDidNotAttendEnable(true);
                        //item.PatientDidNotAttendDate(true);
                        item.DateOfServiceEnable(false);
                        item.WasAbandonedEnable(true);
                    }
                    break;
                }
        }
    };

    this.PrintPrintReviewAssessment = function () {
        window.print();
    };

    self.errors = ko.validation.group(self);

    this.saveCaseTreatmentPricing = function (model) {
        if ($("form")[0].checkValidity()) {
            $('#frmReviewAssessment1').submit();
        };
    };

    this.Saveform = function () {
        $("#loader-main-div").removeClass("hidden");
        $('#frmReviewAssessment').submit();

    };
    $("#btnSave").click(function () {
        $("#btnSubmit").attr("disabled", true);
    });
    $("#btnSave2").click(function () {
        $("#btnSubmit").removeAttr("disabled");
    });

    this.SubmitData = function () {
        var errors = "Errors ! Click On The Save Button First ";
        if (!self.isValid()) {
            $.each(self.errors(), function (index, value) {
                errors = errors + ' ' + value();
            });
            
            $("#btnSubmit").attr("disabled", "disabled");            
        }
        
        $("#loader-main-div").removeClass("hidden");
        if (this.CaseID() != null) {
            $.post('/ReviewAssessment/SubmitReviewAssessment', { caseid: this.EncryptDecryptCaseID() }, function (resp) {
                window.location.href = '/Supplier/ExistingPatientsNextAssessment';
            });
        }
    };

    self.AfterSuccessCaseTreatmentPricings = function (responseText, statusText, xhr, $form) {
        alert($.parseJSON(responseText));
        $("#loader-main-div").addClass("hidden");
    };

    self.AfterSuccessReviewAssessment = function (responseText, statusText, xhr, $form) {
        alert($.parseJSON(responseText));
        $("#loader-main-div").addClass("hidden");
    };

    this.Close = function () {
        if (confirm('Are you sure you want to close this form?')) {
            window.location.href = '/Supplier/ExistingPatientsNextAssessment';
        }
    };

    this.AddMoreInjuryAndSymptoms = function () {
        self.CaseAssessmentPatientInjuriesArray.push(new CaseAssessmentPatientInjury(self.CaseAssessmentDetailID()));
    };

    this.RemovePatientInjury = function (item) {
        self.CaseAssessmentPatientInjuriesArray.remove(item);
    };

    this.EnableTextBox = function (item, event) {
        var context = ko.contextFor(event.target);
        var index = context.$index();
        var dropvalue = $("#dropdown_" + index).val();
        if (dropvalue == "18") {
            $("#prefix_" + index).removeAttr("disabled");
        }
        else {
            $("#prefix_" + index).attr("disabled", "disabled");
        }

    }

    ko.bindingHandlers.ajaxForm = {
        init: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
            var value = ko.utils.unwrapObservable(valueAccessor());
            $(element).ajaxForm(value);
        }
    };

    ko.bindingHandlers.allowBindings = {
        init: function (elem, valueAccessor) {
            // Let bindings proceed as normal *only if* my value is false
            var shouldAllowBindings = ko.utils.unwrapObservable(valueAccessor());
            return { controlsDescendantBindings: !shouldAllowBindings };
        }
    };

    ko.bindingHandlers.caseDateOfService = {
        init: function (element, valueAccessor) {
            var options = valueAccessor();
            $(element).datepicker(options || {});
        }
    };
};

// Validation for pain score
function checkPainScore() {
    if (document.getElementById("txtPainScore").value.trim() != '') {
        if (document.getElementById("txtPainScore").value.trim().match(new RegExp('(^([0-9][/]([1-9]|10))$)|(^([0-9]|10)$)'))) {
            var a = parseInt((document.getElementById("txtPainScore").value).substring(0, 1));
            var b = parseInt((document.getElementById("txtPainScore").value).substring(2, 5));
            if (a <= b) {
                if (b - a > 1) {
                    alert("Invalid Value - Hint : (7/8, 4/5)");
                    document.getElementById("txtPainScore").focus();
                }
            }
            else if (a > b) {
                alert("Invalid Value - Hint : (7/8, 4/5)");
                document.getElementById("txtPainScore").focus();
            }
        }
        else {
            alert("Invalid format (eg. 8/10)");
            document.getElementById("txtPainScore").focus();
        }
    }
}

function CheckIsPatientReturnToPreInjuryDuties(elem) {
    if (elem == 'true') {
        $("#divPatientReturnToPreInjuryDutiesDetails").removeClass("hide");
    }
    else {
        $("#divPatientReturnToPreInjuryDutiesDetails").addClass("hide");
    }
}

function CheckHasThePatientReturnedToWork(elem) {
    if (elem == 'true') {
        $("#divPatientReturnDetails").removeClass("hide");
    }
    else {
        $("#divPatientReturnDetails").addClass("hide");
    }
}
