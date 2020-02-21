
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

};

function CaseAssessmentPatientImpact(data, name, assementServiceID) {

    var self = this;
    var mapping;
    self.CaseAssessmentPatientImpactID = ko.observable();
    self.AssessmentServiceID = ko.observable(assementServiceID);
    self.PatientImpactID = ko.observable();
    self.PatientImpactValueID = ko.observable().extend({ required: { message: '\Patient Impact is required.' } });
    self.Comment = ko.observable();
    self.CaseAssessmentDetailID = ko.observable();
    self.PatientImpactName = ko.observable(name);
    // Mapping is necessary to ingnore when the caseReviewAssessment is added for the first time
    // and this is decided on the basis of AssessmentServiceID [AssessmentServiceID = 1 (new caseReviewAssessment) and AssessmentServiceID = 2 (update caseReviewAssessment)]
    if (self.AssessmentServiceID() == 1) {
        mapping = {
            'ignore': ["PatientImpactValueID", "Comment"]
        }
    }
    ko.mapping.fromJS(data, mapping, self);
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
    self.CaseTreatmentPricingID = ko.observable();
    self.CaseID = ko.observable();
    self.ReferrerPricingID = ko.observable();
    self.SupplierPriceID = ko.observable();
    self.PricingTypeName = ko.observable();
    self.ReferrerPrice = ko.observable();
    self.SupplierPrice = ko.observable();
    self.DateOfService = ko.observable().extend({ parseJsonDate: null });
    self.PatientDidNotAttend = ko.observable(false);
    self.WasAbandoned = ko.observable(false);
    self.IsComplete = ko.observable(false);
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
    self.DateOfServiceEnable = ko.observable(dateOfServiceEnable);
    self.PatientDidNotAttendEnable = ko.observable(patientDidNotAttendEnable);
    self.WasAbandonedEnable = ko.observable(wasAbandonedEnable);

    ko.mapping.fromJS(data, {}, self);
};

ko.validation.configure({
    insertMessages: false,
    grouping: { deep: false }
});



function FinalAssessmentViewModel() {
    var self = this;

    self.IsAccepted = ko.observable(true);

    self.SymptomDescriptionID = ko.observable();
    self.StrengthTestingID = ko.observable();
    self.RestrictionRangeID = ko.observable();
    self.AffectedAreaID = ko.observable();

    //Intial Assesscement incident and diagnosis/injury section deop downs
    self.SymptomDescriptions = ko.observableArray([]);
    self.StrengthTestings = ko.observableArray([]);
    self.RestrictionRanges = ko.observableArray([]);
    self.AffectedAreas = ko.observableArray([]);

    // New Fields Added
    self.RelevantTestUndertaken = ko.observable();
    self.hdRelevantTestUndertaken = ko.observable();

    // Pricing Matrix
    self.CaseTreatmentPricingTypeArray = ko.observableArray([]);
    self.CaseBespokeServicePricingsArray = ko.observableArray([]);

    self.BirthDate = ko.observable().extend({ parseJsonDate: null });
    self.CaseSubmittedDate = ko.observable().extend({ parseJsonDate: null });
    self.InjuryDate = ko.observable().extend({ parseJsonDate: null });
    self.DateOfInitialAssessment = ko.observable().extend({ parseJsonDate: null });
    self.Practitioners = ko.observableArray([]);
    self.PractitionerID = ko.observable();
    self.CaseID = ko.observable();
    self.EncryptedCaseID = ko.observable();
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
    self.AbsentPeriod = ko.observable().extend({ required: { onlyIf: function () { return self.HasThePatientHadTimeOff() == 'true'; }, message: "\nHow many days/weeks have they been absent is required." }, number: { message: '\nNot a number, Please enter number  in how many weeks have they been absent' } });
    self.AbsentPeriodDurationID = ko.observable().extend({ required: { onlyIf: function () { return self.HasThePatientHadTimeOff() == 'true'; }, message: '\n how many weeks have they been absent Duratiopn is required.' } });
    self.HasThePatientReturnedToWork = ko.observable().extend({ required: { message: '\n Has the patient returned to work is required.' } });
    self.PatientImpactOnWorkID = ko.observable().extend({ required: { message: '\n Rate the impact of the remaining symptoms on the patients work is required.' } });
    self.PatientWorkstatusID = ko.observable().extend({ required: { message: '\n At what capacity is the patient working is required.' } });
    // Remaining Impact on Lifestyle
    self.CaseAssessmentPatientImpacts = ko.observableArray().extend({ validArray: { params: true, message: "\nPlease select a value for each  impact on lifestyle criteria." } });

    // Treatment Recommendation
    self.SessionsPatientAttended = ko.observable().extend({ required: { message: "\n How many sessions has the patient attended is required." }, number: { message: '\nNot a number, Please enter number  in  How many sessions has the patient attended' } });
    self.DatesOfSessionAttended = ko.observable().extend({ required: { message: '\nWhat were the dates of the attended sessions is required.' } });
    self.SessionsPatientFailedToAttend = ko.observable().extend({ required: { message: "\nHow many sessions has the patient failed to attend is required." }, number: { message: '\nNot a number, Please enter number  in How many sessions has the patient failed to attend' } });
    self.FollowingTreatmentPatientLevelOfRecoveryID = ko.observable().extend({ required: { onlyIf: function () { return self.IsPatientDischarge() != 'true'; }, message: '\n What level of recovery do you anticipate the patient will make following this additional treatment is required.' } });
    self.HasCompliedHomeExerciseProgramme = ko.observable().extend({ required: { message: '\n patient been given a home exercise programme is required.' } });
    // Further Treatment
   

    self.IsFurtherTreatmentRecommended = ko.observable().extend({ required: { onlyIf: function () { return self.IsPatientDischarge() != 'true'; }, message: '\n Do you recommend any further treatment is required.' } });

    self.PatientTreatmentPeriodTypeID = ko.observable();
    self.PatientTreatmentPeriodDurationID = ko.observable().extend({ required: { message: "\nOver what period should these be carried out duration is required.", onlyIf: function () { return self.PatientTreatmentPeriodTypeID() === 4 && self.IsPatientDischarge() != 'true' } } });
    self.PatientTreatmentPeriod = ko.observable().extend({ required: { message: "\nOver what period should these be carried out is required.", onlyIf: function () { return self.PatientTreatmentPeriodTypeID() === 4 && self.IsPatientDischarge() != 'true' } }, number: { message: '\nPlease enter a valid number for "Over what period should these be carried out?"' } });

    self.PatientLevelOfRecoveryID = ko.observable().extend({ required: { message: '\n What level of recovery do you anticipate the patient will make following this additional treatment is required.' } });
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


    self.AnticipatedDateOfDischarge = ko.observable().extend({ parseJsonDate: null });
    self.AssessmentAuthorisationID = ko.observable();
    self.AssessmentServiceID = ko.observable();

    $(document).ready(function () {
        $('#sessionattended').bind("cut copy paste", function (e) {
            e.preventDefault();
        });
        $('#sessionfailedtoattend').bind("cut copy paste", function (e) {
            e.preventDefault();
        });
    });



    self.Init = function (model) {

        if (model != null) {

            self.IsAccepted(model.CaseAssessment.IsAccepted);
            if (model.CaseAssessment.AssessmentServiceID != '1') {
                self.PractitionerID(model.CaseAssessment.CaseAssessmentDetail.PractitionerID);
                self.AbsentPeriod(model.CaseAssessment.CaseAssessmentDetail.AbsentPeriod);
                self.AbsentPeriodDurationID(model.CaseAssessment.CaseAssessmentDetail.AbsentPeriodDurationID);
                self.IncidentAndDiagnosisDescription(model.CaseAssessment.IncidentAndDiagnosisDescription);
                self.NeuralSymptomDescription(model.CaseAssessment.NeuralSymptomDescription);
                self.SessionsPatientAttended(model.CaseAssessment.CaseAssessmentDetail.SessionsPatientAttended);
                self.PatientTreatmentPeriod(model.CaseAssessment.CaseAssessmentDetail.PatientTreatmentPeriod);
                self.PatientTreatmentPeriodDurationID(model.CaseAssessment.CaseAssessmentDetail.PatientTreatmentPeriodDurationID);
                self.DatesOfSessionAttended(model.CaseAssessment.CaseAssessmentDetail.DatesOfSessionAttended);
                self.SessionsPatientFailedToAttend(model.CaseAssessment.CaseAssessmentDetail.SessionsPatientFailedToAttend);
                self.AdditionalInformation(model.CaseAssessment.CaseAssessmentDetail.AdditionalInformation);
                self.HasPatientConsentForm(model.CaseAssessment.HasPatientConsentForm.toString());
                self.IsPatientDischarge(model.CaseAssessment.IsPatientDischarge != null ? model.CaseAssessment.IsPatientDischarge.toString() : 'nullable');
                self.hdRelevantTestUndertaken(model.CaseAssessment.RelevantTestUndertaken);
                self.RelevantTestUndertaken(model.CaseAssessment.RelevantTestUndertaken);

                self.DeniedMessageCheck = ko.observable(true);
                self.DeniedMessage(model.CaseAssessment.DeniedMessage);
                self.PatientImpactOnWorkID(model.CaseAssessment.CaseAssessmentDetail.PatientImpactOnWorkID);
                self.PatientWorkstatusID(model.CaseAssessment.CaseAssessmentDetail.PatientWorkstatusID);
                self.PatientLevelOfRecoveryID(model.CaseAssessment.CaseAssessmentDetail.PatientLevelOfRecoveryID);

                self.HasCompliedHomeExerciseProgramme(model.CaseAssessment.CaseAssessmentDetail.HasCompliedHomeExerciseProgramme != null ? model.CaseAssessment.CaseAssessmentDetail.HasCompliedHomeExerciseProgramme.toString() : 'nullable');
                self.HasThePatientHadTimeOff(model.CaseAssessment.CaseAssessmentDetail.HasThePatientHadTimeOff != null ? model.CaseAssessment.CaseAssessmentDetail.HasThePatientHadTimeOff.toString() : '*');
                self.HasThePatientReturnedToWork(model.CaseAssessment.CaseAssessmentDetail.HasThePatientReturnedToWork != null ? model.CaseAssessment.CaseAssessmentDetail.HasThePatientReturnedToWork.toString() : '*');

                if (model.CaseAssessment.CaseAssessmentDetail.IsFurtherTreatmentRecommended != null)
                    self.IsFurtherTreatmentRecommended(model.CaseAssessment.CaseAssessmentDetail.IsFurtherTreatmentRecommended.toString());
                self.FollowingTreatmentPatientLevelOfRecoveryID(model.CaseAssessment.CaseAssessmentDetail.FollowingTreatmentPatientLevelOfRecoveryID);

                self.PatientRecommendedTreatmentSessions(model.CaseAssessment.CaseAssessmentDetail.PatientRecommendedTreatmentSessions);
                self.PatientRecommendedTreatmentSessionsDetail(model.CaseAssessment.CaseAssessmentDetail.PatientRecommendedTreatmentSessionsDetail);
                $.each(model.CaseAssessment.CaseAssessmentPatientInjuriesBL, function (index, value) {

                    self.CaseAssessmentPatientInjuriesArray.push(new CaseAssessmentPatientInjury(model.CaseAssessment.CaseAssessmentDetail.CaseAssessmentDetailID, value));
                });
                $.each(model.CaseAssessment.CaseAssessmentPatientImpacts, function (index, value) {

                    self.CaseAssessmentPatientImpacts.push(new CaseAssessmentPatientImpact(value, model.PatientImpacts[index].PatientImpactName, model.CaseAssessment.AssessmentServiceID));
                });

                self.BirthDate(model.Patient.BirthDate);
                self.CaseSubmittedDate(model.Case.CaseSubmittedDate);
                self.InjuryDate(model.Patient.InjuryDate);
                self.DateOfInitialAssessment(model.DateOfInitialAssessment);

                self.SymptomDescriptions(model.SymptomDescriptions);
                self.RestrictionRanges(model.RestrictionRanges);
                self.StrengthTestings(model.StrengthTestings);
                self.AffectedAreas(model.AffectedAreas);



                self.AssessmentAuthorisationID(model.CaseAssessment.AssessmentAuthorisationID);
                self.AssessmentServiceID(model.CaseAssessment.AssessmentServiceID);
                self.AnticipatedDateOfDischarge(model.CaseAssessment.AnticipatedDateOfDischarge);
                self.CaseID(model.CaseAssessment.CaseID);
                self.EncryptedCaseID(model.Case.EncryptedCaseID);
                self.CaseAssessmentDetailID(model.CaseAssessment.CaseAssessmentDetail.CaseAssessmentDetailID);
                self.PatientRoleID(model.CaseAssessment.PatientRoleID);
                self.PatientOccupation(model.CaseAssessment.PatientOccupation);
                ko.mapping.fromJS(model, {}, self);
                ko.mapping.fromJS(model.Duration, {}, self.Duration);
                ko.mapping.fromJS(model.PatientRole, {}, self.PatientRole);

                $.each(model.CaseTreatmentPricingTypes, function (index, value) {

                    if (value.DateOfService !== null) {
                        self.CaseTreatmentPricingTypeArray.push(new CaseTreatmentPricingType(value, true, false, false));
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
            }
        };


        self.IsFurtherTreatmentRecommended.subscribe(function (item) {

            if (item == 1) {
                self.PatientRecommendedTreatmentSessionsDetail = ko.observable().extend({ required: { message: '\nDescribe the Treatment Sessions that you recommend is required.' } });
                self.PatientRecommendedTreatmentSessions = ko.observable().extend({ required: { message: "\nHow many sessions do you recommend is required." }, number: { message: '\nNot a number, Please enter number  in further sessions Recomemded' } });

            }
            else {

                self.PatientRecommendedTreatmentSessions = ko.observable();
                self.PatientRecommendedTreatmentSessionsDetail = ko.observable();
            }



        });



        self.ValidateCaseBespokeServicePricing = function (item, e) {

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
                        if (item.DateOfService() == null) {
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
                        if (item.DateOfService() == '') {
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

        $(document).ready(function () {
            $("#frmFinalAssessmentCustom1").ajaxForm({

                beforeSubmit: function () {
                    $("#loader-main-div").removeClass("hidden");
                },
                success: function (response, statusText, xhr, $form) {
                    $("#loader-main-div").addClass("hidden");
                    if (response === "\"Saved Sucessfully\"") {
                        alert("Saved Sucessfully");
                        $("#loader-main-div").addClass("hidden");
                    }
                }
            });
        });

        this.PrintPrintReviewAssessment = function () {

            $.post('/PrintPopUp/PrintFinalAssessment', { caseID: self.EncryptedCaseID() }, function (resp) {
                var mywindow;
                if (navigator.appName == 'Microsoft Internet Explorer') {
                    mywindow = window.open('', '', '');
                    mywindow.document.write(resp);
                    mywindow.document.close();
                    mywindow.focus();
                    mywindow.print();
                    mywindow.close();
                } else {
                    mywindow = window.open('', '', '');
                    mywindow.document.write(resp);
                    mywindow.print();
                }
                return false;
            });

        };


        self.errors = ko.validation.group(self);

        this.Saveform = function () {
            $("#loader-main-div").removeClass("hidden");
            $('#frmFinalAssessment').submit();

        };
        $("#btnSave").click(function () {
            $("#btnSubmit").attr("disabled", true);
        });
        $("#btnSave2").click(function () {
            $("#btnSubmit").removeAttr("disabled");
        });
        this.Submitform = function () {
            var errors = "Errors ! Click On The Save Button First ";
            if (!self.isValid()) {
                $.each(self.errors(), function (index, value) {
                    errors = errors + ' ' + value();
                });
                alert(errors);
                $("#btnSubmit").attr("disabled", "disabled");
                return false;
            }
            $("#loader-main-div").removeClass("hidden");
            if (this.CaseID() != null) {
                $.post('/FinalAssessment/SubmitFinalAssessment', { caseid: this.EncryptedCaseID() }, function (resp) {
                    window.location.href = '/Supplier/ExistingPatientsNextAssessment';
                });
            }
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
        self.UpdateFinalAssessment = function (responseText, statusText, xhr, $form) {
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
        ko.bindingHandlers.ajaxForm = {
            init: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
                var value = ko.utils.unwrapObservable(valueAccessor());
                $(element).ajaxForm(value);
            }
        };
        ko.bindingHandlers.datepicker = {
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
}