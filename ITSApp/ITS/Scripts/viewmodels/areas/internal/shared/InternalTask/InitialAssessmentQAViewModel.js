function InitaialAssessmentQA(caseID, patientID, firstName, lastName, referrerID, caseNumber, caseDateOfInquiry, referrerProjectTreatmentID, treatmentTypeID, caseReferrerReferenceNumber, caseSpecialInstruction, caseReferrerDueDate, caseSubmittedDate, supplierID, treatmentTypeName, referrerName, projectName) {

    var self = this;
    self.CaseID = ko.observable(caseID);
    self.PatientID = ko.observable(patientID);
    self.FirstName = ko.observable(firstName);
    self.LastName = ko.observable(lastName);
    self.PatientName = ko.observable();
    self.ReferrerID = ko.observable(referrerID);
    self.CaseNumber = ko.observable(caseNumber);
    self.CaseDateOfInquiry = ko.observable(caseDateOfInquiry);
    self.ReferrerProjectTreatmentID = ko.observable(referrerProjectTreatmentID);
    self.TreatmentTypeID = ko.observable(treatmentTypeID);
    self.TreatmentTypeName = ko.observable(treatmentTypeName);
    self.CaseReferrerReferenceNumber = ko.observable(caseReferrerReferenceNumber);
    self.CaseSpecialInstruction = ko.observable(caseSpecialInstruction);
    self.CaseReferrerDueDate = ko.observable(caseReferrerDueDate);
    self.CaseSubmittedDate = ko.observable(caseSubmittedDate);
    self.SupplierID = ko.observable(supplierID);
    self.ReferrerName = ko.observable(referrerName);
    self.ProjectName = ko.observable(projectName);
    self.PatientName = ko.computed(function () {
        return self.FirstName() + " " + self.LastName();
    });

};

ko.extenders.changeToBool = function (target) {
    var result = ko.computed({
        read: target,
        write: function (newValue) {
            if (newValue != undefined) {
                if (newValue == "1") {
                    return target(true);
                } else if (newValue == "clear") {
                    return target(undefined);
                } else {
                    return target(false);
                }
            }
        }
    });
    result(target());
    return result;
};


function InitialAssessmentQAViewModel() {

    ko.validation.configure({
        insertMessages: false,
        grouping: { deep: true }
    });

    //    var pagingSettings = {
    //        pageSize: 5,
    //        pageSlide: 2
    //    };
    //    var self = this;

    //    // page variables and methods used for pagging  
    //    self.TotalItemCount = ko.observable();
    //    self.Pager = ko.pager(self.TotalItemCount);
    //    self.Pager().PageSize(pagingSettings.pageSize);
    //    self.Pager().PageSlide(pagingSettings.pageSlide);
    //    self.Pager().CurrentPage(1);
    //    self.Pager().CurrentPage.subscribe(function () {
    //        var skip = pagingSettings.pageSize * (self.Pager().CurrentPage() - 1);
    //        var take = pagingSettings.pageSize;

    //        if (RadiobuttonSelectedID == 5)
    //            self.GetAllInitialAssessmentProjects(function () { $('#divGridDisplaySpinner').spin(false); }, skip, take);
    //        else
    //            self.GetInitialAssessmentProjectsByTreatmentCategoryID(RadiobuttonSelectedID, function () { $('#divGridDisplaySpinner').spin(false); }, skip, take);

    //    });


    self.SelectedTreatmentCategoryID = ko.observable();
    // self.InitialAssessmentVisible = ko.observable(false);
    self.CaseID = ko.observable();
    self.PatientImpacts = ko.observableArray();
    self.PatientImpactValues = ko.observableArray();
    /**PATIENT DETAILS**/
    self.FirstName = ko.observable();
    self.LastName = ko.observable();
    self.PatientName = ko.computed(function () {
        return self.FirstName() + ' ' + self.LastName();
    });
    self.DateOfBirth = ko.observable().extend({ parseJsonDate: null });
    self.DateOfReferral = ko.observable().extend({ parseJsonDate: null });
    self.PractitionerID = ko.observable();
    self.Practitioners = ko.observableArray();
    self.DateOfAccident = ko.observable().extend({ parseJsonDate: null });
    self.DateOfInitialAssessment = ko.observable().extend({ parseJsonDate: null });
    /**end - PATIENT DETAILS**/

    /*Patient Consent form*/
    self.HasPatientConsentForm = ko.observable().extend({ changeToBool: null }).extend({ required: { message: 'Patient Consent is required.'} });

    /**INJURY AND SYMPTOMS**/
    self.IncidentAndDiagnosisDescription = ko.observable().extend({ required: { message: 'Injury and Symptom Details description is required.'} });
    self.CaseAssessmentPatientInjuries = ko.observableArray([]);
    //Neutal symptoms
    self.NeuralSymptomDescription = ko.observable().extend({ required: { message: 'Nueral Symptoms description is required.'} });

    /**FACTORS AFFECTING TREATMENT**/
    self.PreExistingConditionDescription = ko.observable().extend({ required: { message: 'Pre Existing Condition description is required.'} });
    self.PsychologicalFactors = ko.observableArray();
    self.PsychologicalFactorID = ko.observable().extend({ required: { message: '\nYou need to select a Psychological Factor.'} });
    self.IsPatientUndergoingTreatment = ko.observable().extend({ changeToBool: null }).extend({ required: { message: '\nIs Patient undergoing treatment is required.'} });
    self.IsPatientTakingMedication = ko.observable().extend({ changeToBool: null }).extend({ required: { message: '\nIs Patient taking medication is required.'} });
    self.PatientRequiresFurtherInvestigation = ko.observable().extend({ changeToBool: null }).extend({ required: { message: '\nDoes Patient require any further investigation is required.'} });
    self.FactorsAffectingTreatmentDescription = ko.observable().extend({ required: { message: '\nFactors affecting treatment description is required.'} });
    /**end - FACTORS AFFECTING TREATMENT**/

    /**IMPACT ON LIFESTYLE**/
    self.CaseAssessmentPatientImpacts = ko.observableArray();
    /**end - IMPACT ON LIFESTYLE**/

    /**IMPACT ON WORK**/
    self.PatientOccupation = ko.observable().extend({ required: { message: '\nPatient occupation is required.'} });
    self.PatientRole = ko.observable().extend({ required: { message: "\nPatient's role is required."} });
    self.WasPatientWorkingAtTheTimeOfTheAccident = ko.observable().extend({ changeToBool: null }).extend({ required: { message: "\nPatient's working at the time of the accident is required."} });
    self.HasThePatientHadTimeOff = ko.observable().extend({ changeToBool: null }).extend({ required: { message: "\nHas the patient had any time off is required."} });
    self.HasThePatientReturnedToWork = ko.observable().extend({ changeToBool: null }).extend({ required: { message: "\nHas the patient return to work is required."} });
    self.AbsentDetail = ko.observable().extend({ required: { message: '\nIf they have been absent is required.' }, number: { message: '\nEnter number only in absent details'} });
    self.IsPatientSufferingFinancialLoss = ko.observable().extend({ changeToBool: null }).extend({ required: { message: "\nIs the patient still suffering a financial loss is required."} });
    self.PatientWorkstatusID = ko.observable().extend({ required: { message: "\nCurrent work status is required."} });
    self.PatientWorkstatuses = ko.observableArray();
    self.PatientImpactOnWorkID = ko.observable().extend({ required: { message: '\nCurrent impact on work is required.'} });
    self.PatientImpactOnWorks = ko.observableArray();
    /**end - IMPACT ON WORK**/

    /**TREATMENT RECOMMENDATION**/
    self.PatientRecommendedTreatmentSessions = ko.observable().extend({ required: { message: "\nHow many sessions do you recommend is required." }, min: 0, number: { message: '\nNot a number, Please enter number  in session Recomemded'} }),
    self.PatientTreatmentPeriod = ko.observable().extend({ required: { message: "\nOver what period should these be carried out is required."} });
    self.AnticipatedDateOfDischarge = ko.observable().extend({ required: { message: "\nWhat is the anticipated date of discharge is required."} });
    self.PatientLevelOfRecoveryID = ko.observable().extend({ required: { message: "\nPatient's level of recovery is required."} });
    self.PatientLevelOfRecoveries = ko.observableArray();
    self.HasPatientHomeExerciseProgramme = ko.observable().extend({ changeToBool: null }).extend({ required: { message: "\nHas the patient been given a home exercise programme is required."} });
    self.ProposedTreatmentMethodID = ko.observable().extend({ required: { message: "\nProposed treatment methods is required."} });
    self.ProposedTreatmentMethods = ko.observableArray();
    self.AdditionalInformation = ko.observable().extend({ required: { message: "\nAdditional Information is required."} });
    /**end - TREATMENT RECOMMENDATION**/

    /**CASERATING**/
    self.Rating = ko.observable();
    self.CaseAssessmentRating = ko.observable();
    /**end -CASERATING**/

    /**TREATMENTS**/
    self.TreatmentPriceRows = ko.observableArray([]);
    self.SelectedReferrerTreatmentPricing = ko.observable();
    //self.ReferrerTreatmentPricings = ko.observableArray([new ReferrerTreatmentPricing(1, 1, 50, 1, 'treatment type 1', 1), new ReferrerTreatmentPricing(2, 2, 100, 2, 'treatment type 2', 2)]);
    self.ReferrerTreatmentPricings = ko.observableArray([]);
    self.AddNewTreatment = function () {
        self.TreatmentPriceRows.push(new CaseTreatmentPricing(self.SelectedReferrerTreatmentPricing().PricingID(), self.SelectedReferrerTreatmentPricing().Price(), self.SelectedReferrerTreatmentPricing().PricingTypeName(), self.CaseID(), self));
    };
    /**end - TREATMENTS**/

    /**BESPOKE**/
    self.CaseBespokeServicePricingRows = ko.observableArray([]); //para sa grid

    self.CaseBespokeServicePricings = ko.observableArray([]); //lalagyanan from webservice

    self.SelectedBespokeServicePricing = ko.observable(); //eto yung selected
    self.ReferrerPrice = ko.observable();
    self.SupplierPrice = ko.observable();
    self.AddNewBespokeService = function () {
        if (!isNumber(self.ReferrerPrice()) && !isNumber(self.SupplierPrice())) {
            alert('Must choose numeric values');
            return false;
        }
        self.CaseBespokeServicePricingRows.push(new CaseBespokeServicePricingRow(self.SelectedBespokeServicePricing().TreatmentCategoryBespokeServiceID(), self.ReferrerPrice(), self.SupplierPrice(), self.SelectedBespokeServicePricing().BespokeServiceName(), self.CaseID(), self));
        self.ReferrerPrice('');
        self.SupplierPrice('');
    };
    /**end - BESPOKE**/

    self.IsAccepted = ko.observable().extend({ changeToBool: null }).extend({ required: { message: "\nAccept or Reject is required."} });

    self.DeniedMessage = ko.observable();
    self.InitaialAssessmentQAs = ko.observableArray([]);
    var InitialAssessmentAllSelectedId = 5;
    self.CaseAssessmentPatientInjuries = ko.observableArray([]);
    self.CaseAssessment = ko.observableArray([]);



    self.errors = ko.validation.group(self);

    // this varibale to maintain checked 

    self.EnableSubmitButton = ko.observable(true);

    self.PsychologicalFactorIDCheckedVal = ko.observable();
    self.PatientWorkstatusIDCheckedVal = ko.observable();
    self.PatientImpactOnWorkIDCheckedVal = ko.observable();
    self.PatientLevelOfRecoveryIDCheckedVal = ko.observable();
    self.ProposedTreatmentMethodIDCheckedVal = ko.observable();

    // Subscribe event to bind the new Updated Checked value for
    self.PsychologicalFactorIDCheckedVal.subscribe(function (newvalue) {
        self.PsychologicalFactorID(newvalue);
    });

    self.PatientWorkstatusIDCheckedVal.subscribe(function (newvalue) {
        self.PatientWorkstatusID(newvalue);
    });

    self.PatientImpactOnWorkIDCheckedVal.subscribe(function (newvalue) {
        self.PatientImpactOnWorkID(newvalue);
    });

    self.PatientLevelOfRecoveryIDCheckedVal.subscribe(function (newvalue) {
        self.PatientLevelOfRecoveryID(newvalue);
    });

    self.ProposedTreatmentMethodIDCheckedVal.subscribe(function (newvalue) {
        self.ProposedTreatmentMethodID(newvalue);
    });




    this.Init = function (model) {

      
        self.CaseID(model.Case.CaseID);

        self.PsychologicalFactorIDCheckedVal(model.CaseAssessment.PsychologicalFactorID);
        self.PatientWorkstatusIDCheckedVal(model.CaseAssessment.PatientWorkstatusID);
        self.PatientImpactOnWorkIDCheckedVal(model.CaseAssessment.PatientImpactOnWorkID);
        self.PatientLevelOfRecoveryIDCheckedVal(model.CaseAssessment.PatientLevelOfRecoveryID);
        self.ProposedTreatmentMethodIDCheckedVal(model.CaseAssessment.ProposedTreatmentMethodID);

        $.each(model.CaseAssessment.CaseAssessmentPatientInjuries, function (index, value) {
            self.CaseAssessmentPatientInjuries.push(new CaseAssessmentPatientInjury(value));
        });
        
        self.FirstName(model.Patient.FirstName);
        self.LastName(model.Patient.LastName);
        self.DateOfBirth(model.Patient.BirthDate);
        self.DateOfAccident(model.Patient.InjuryDate);
        self.DateOfInitialAssessment(model.DateOfInitialAssessment);
        self.DateOfReferral(model.Case.CaseSubmittedDate);

        self.HasPatientConsentForm(model.CaseAssessment.HasPatientConsentForm);

        /**INJURY AND SYMPTOMS**/
        self.IncidentAndDiagnosisDescription(model.CaseAssessment.IncidentAndDiagnosisDescription);
        //self.CaseAssessmentPatientInjuries = ko.observableArray();
        self.NeuralSymptomDescription(model.CaseAssessment.NeuralSymptomDescription);
        /**end - INJURY AND SYMPTOMS**/

        /**FACTORS AFFECTING TREATMENT**/
        self.PreExistingConditionDescription(model.CaseAssessment.PreExistingConditionDescription);
        //self.PsychologicalFactors = ko.observableArray();





        self.IsPatientUndergoingTreatment(model.CaseAssessment.IsPatientUndergoingTreatment);
        self.IsPatientTakingMedication(model.CaseAssessment.IsPatientUndergoingTreatment);
        self.PatientRequiresFurtherInvestigation(model.CaseAssessment.PatientRequiresFurtherInvestigation);
        self.FactorsAffectingTreatmentDescription(model.CaseAssessment.FactorsAffectingTreatmentDescription);
        /**end - FACTORS AFFECTING TREATMENT**/

        /**IMPACT ON LIFESTYLE**/
        //self.CaseAssessmentPatientImpacts = ko.observableArray();
        /**end - IMPACT ON LIFESTYLE**/

        /**IMPACT ON WORK**/
        self.PatientOccupation(model.CaseAssessment.PatientOccupation);
        self.PatientRole(model.CaseAssessment.PatientRole);
        self.WasPatientWorkingAtTheTimeOfTheAccident(model.CaseAssessment.WasPatientWorkingAtTheTimeOfTheAccident);
        self.HasThePatientHadTimeOff(model.CaseAssessment.HasThePatientHadTimeOff);
        self.HasThePatientReturnedToWork(model.CaseAssessment.HasThePatientReturnedToWork);
        self.AbsentDetail(model.CaseAssessment.AbsentDetail);
        self.IsPatientSufferingFinancialLoss(model.CaseAssessment.IsPatientSufferingFinancialLoss);

        //self.PatientWorkstatuses = ko.observableArray();
        //self.PatientImpactOnWorks = ko.observableArray();
        /**end - IMPACT ON WORK**/

        /**TREATMENT RECOMMENDATION**/
        self.PatientRecommendedTreatmentSessions(model.CaseAssessment.PatientRecommendedTreatmentSessions);
        self.PatientTreatmentPeriod(model.CaseAssessment.PatientTreatmentPeriod);
        self.AnticipatedDateOfDischarge(moment(model.CaseAssessment.AnticipatedDateOfDischarge).format('DD/MM/YYYY'));

        //self.PatientLevelOfRecoveries = ko.observableArray();
        self.HasPatientHomeExerciseProgramme(model.CaseAssessment.HasPatientHomeExerciseProgramme);

        //self.ProposedTreatmentMethods = ko.observableArray();
        self.AdditionalInformation(model.CaseAssessment.AdditionalInformation);
        /**end - TREATMENT RECOMMENDATION**/
       
        self.DeniedMessage(model.CaseAssessment.DeniedMessage);

        self.CaseAssessmentRating(new CaseAssessmentRating(model.Case.CaseID, model.CaseAssessment.CaseAssessmentRating !== 'undefined' && model.CaseAssessment.CaseAssessmentRating != null ? model.CaseAssessment.CaseAssessmentRating.CaseAssessmentRatingID : 0));

        ko.mapping.fromJS(model.Practitioners, {}, self.Practitioners);
        ko.mapping.fromJS(model.PsychologicalFactors, {}, self.PsychologicalFactors);
        ko.mapping.fromJS(model.PatientImpactValues, {}, self.PatientImpactValues);
        ko.mapping.fromJS(model.PatientImpacts, {}, self.PatientImpacts);
        $.each(model.PatientImpacts, function (index, value) {
            var caseAssessmentPatientImpacts = new CaseAssessmentPatientImpacts(value.PatientImpactName);
            caseAssessmentPatientImpacts.CaseID = self.CaseID();
            caseAssessmentPatientImpacts.PatientImpactID = value.PatientImpactID;
            caseAssessmentPatientImpacts.PatientImpactValues = self.PatientImpactValues;
            $.each(model.CaseAssessment.CaseAssessmentPatientImpacts, function (index, assessmentValue) {
                if (assessmentValue.PatientImpactID === caseAssessmentPatientImpacts.PatientImpactID) {
                    caseAssessmentPatientImpacts.CaseAssessmentPatientImpactID(assessmentValue.CaseAssessmentPatientImpactID);
                    caseAssessmentPatientImpacts.PatientImpactValueID(assessmentValue.PatientImpactValueID);
                    caseAssessmentPatientImpacts.Comment(assessmentValue.Comment);
                }
            });
            self.CaseAssessmentPatientImpacts.push(caseAssessmentPatientImpacts);
        });


        ko.mapping.fromJS(model.PatientWorkstatuses, {}, self.PatientWorkstatuses);
        ko.mapping.fromJS(model.PatientImpactOnWorks, {}, self.PatientImpactOnWorks);
        ko.mapping.fromJS(model.PatientLevelOfRecoveries, {}, self.PatientLevelOfRecoveries);
        ko.mapping.fromJS(model.ProposedTreatmentMethods, {}, self.ProposedTreatmentMethods);
        ko.mapping.fromJS(model.ReffererProjectTreatmentPricings, {}, self.ReferrerTreatmentPricings);
        ko.mapping.fromJS(model.TreatmentCategoriesBespokeServices, {}, self.CaseBespokeServicePricings);

    };

    this.AddMoreInjuryAndSymptoms = function () {

        self.CaseAssessmentPatientInjuries.push(new PatientInjury());
    };

    // TODO : Function to Print the Assessment QA Screen
    this.PrinBlankInitialAssessmentQA = function () {
        var rating = self.CaseAssessmentRating;
        rating().Rating(self.Rating());

        var json = JSON.stringify({
            Patient: {
                PatientName: self.PatientName(),
                BirthDate: self.DateOfBirth(),
                InjuryDate: self.DateOfAccident()
            },
            Case: {
                CaseSubmittedDate: self.DateOfReferral()
            },
            PsychologicalFactors: ko.toJS(self.PsychologicalFactors),
            PatientImpacts: ko.toJS(self.PatientImpacts),
            PatientWorkstatuses: ko.toJS(self.PatientWorkstatuses),
            PatientImpactOnWorks: ko.toJS(self.PatientImpactOnWorks),
            PatientLevelOfRecoveries: ko.toJS(self.PatientLevelOfRecoveries),
            ProposedTreatmentMethods: ko.toJS(self.ProposedTreatmentMethods),
            CaseAssessment: {
                CaseID: self.CaseID(),
                AssessmentDate: self.DateOfInitialAssessment(),
                HasPatientConsentForm: self.HasPatientConsentForm(),
                IncidentAndDiagnosisDescription: self.IncidentAndDiagnosisDescription(),
                NeuralSymptomDescription: self.NeuralSymptomDescription(),
                PreExistingConditionDescription: self.PreExistingConditionDescription(),
                PsychologicalFactorID: self.PsychologicalFactorID(),
                IsPatientUndergoingTreatment: self.IsPatientUndergoingTreatment(),
                IsPatientTakingMedication: self.IsPatientTakingMedication(),
                PatientRequiresFurtherInvestigation: self.PatientRequiresFurtherInvestigation(),
                FactorsAffectingTreatmentDescription: self.FactorsAffectingTreatmentDescription(),
                PatientOccupation: self.PatientOccupation(),
                PatientRole: self.PatientRole(),
                WasPatientWorkingAtTheTimeOfTheAccident: self.WasPatientWorkingAtTheTimeOfTheAccident(),
                HasThePatientHadTimeOff: self.HasThePatientHadTimeOff(),
                HasThePatientReturnedToWork: self.HasThePatientReturnedToWork(),
                AbsentDetail: self.AbsentDetail(),
                IsPatientSufferingFinancialLoss: self.IsPatientSufferingFinancialLoss(),
                PatientWorkstatusID: self.PatientWorkstatusID(),
                PatientImpactOnWorkID: self.PatientImpactOnWorkID(),
                PatientRecommendedTreatmentSessions: self.PatientRecommendedTreatmentSessions(),
                PatientTreatmentPeriod: self.PatientTreatmentPeriod(),
                AnticipatedDateOfDischarge: self.AnticipatedDateOfDischarge(),
                PatientLevelOfRecoveryID: self.PatientLevelOfRecoveryID(),
                HasPatientHomeExerciseProgramme: self.HasPatientHomeExerciseProgramme(),
                ProposedTreatmentMethodID: self.ProposedTreatmentMethodID(),
                AdditionalInformation: self.AdditionalInformation(),
                IsAccepted: self.IsAccepted(),
                CaseAssessmentPatientImpacts: ko.toJS(self.CaseAssessmentPatientImpacts()),
                CaseAssessmentPatientInjuries: ko.toJS(self.CaseAssessmentPatientInjuries()),
                CaseAssessmentRating: ko.toJS(rating)
            }
        });

        $.ajax({
            url: "/PrintPopUp/PrintInitialAssessmentQA/",
            contentType: 'application/json',
            dataType: 'html',
            data: json,
            type: "POST"
        }).done(function (resp) {
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

    function CaseAssessmentPatientInjury(value) {
        //this.showRemove = ko.observable(true);
        this.CaseAssessmentPatientInjuryID = ko.observable(value.CaseAssessmentPatientInjuryID);
        this.CaseID = ko.observable(value.CaseID);
        this.AffectedArea = ko.observable(value.AffectedArea).extend({ required: { message: '\nAffected area is required.'} });
        this.PainScore = ko.observable(value.PainScore).extend({ required: { message: '\nPainScore is required' }, number: { message: '\nEnter number only in Pain Score' }, min: 0, max: 10 });
        this.RestrictionPercentage = ko.observable(value.RestrictionPercentage).extend({ required: { message: '\nRestriction Percentage is required.' }, number: { message: '\nEnter number only in Restriction Percentage' }, min: 0, max: 100 });
    }

    function PatientInjury() {
        this.AffectedArea = ko.observable();
        this.PainScore = ko.observable();
        this.RestrictionPercentage = ko.observable();
    }

    function CaseAssessmentPatientImpacts(patientImpactName) {
        this.CaseAssessmentPatientImpactID = ko.observable();
        this.CaseID = ko.observable();
        this.PatientImpactID = ko.observable();
        this.PatientImpactName = ko.observable(patientImpactName);
        this.PatientImpactValueID = ko.observable().extend({ required: { message: patientImpactName + ' radio button is required.'} });
        this.Comment = ko.observable().extend({ required: { message: patientImpactName + ' Comment is required.'} });
        this.PatientImpactValues = ko.observableArray();
    }

    function CaseAssessmentRating(caseID, caseAssessmentRatingID) {
        this.CaseAssessmentRatingID = ko.observable(caseAssessmentRatingID);
        this.CaseID = ko.observable(caseID);
        this.AssessmentTypeID = ko.observable();
        this.Rating = ko.observable();
        this.RatingDate = ko.observable();
    }

    function CaseTreatmentPricing(pricingID, price, pricingTypeName, caseID, ownerViewModel) {
        this.Price = ko.observable(price);
        this.PricingID = ko.observable(pricingID);
        this.PricingTypeName = ko.observable(pricingTypeName);
        this.CaseID = ko.observable(caseID);
        this.ReferrerPricingID = ko.observable(pricingID);
        this.remove = function () {
            ownerViewModel.TreatmentPriceRows.remove(this);
        };
        this.PriceString = ko.observable(price);
    }

    function ReferrerTreatmentPricing(pricingID, pricingTypeID, price, referrerProjectTreatmentID, pricingTypeName, referrerProjectID, ownerViewModel) {
        this.PricingID = pricingID;
        this.PricingTypeID = pricingTypeID;
        this.Price = price;
        this.ReferrerProjectTreatmentID = referrerProjectTreatmentID;
        this.PricingTypeName = pricingTypeName;
        this.ReferrerProjectID = referrerProjectID;
    }

    function CaseBespokeServicePricingRow(treatmentCategoryBespokeServiceID, referrerPrice, supplierPrice, bespokeServiceName, caseID, ownerViewModel) {
        this.TreatmentCategoryName = ko.observable();
        this.BespokeServiceName = ko.observable(bespokeServiceName);
        this.TreatmentCategoryBespokeServiceID = ko.observable(treatmentCategoryBespokeServiceID);
        this.TreatmentCategoryID = ko.observable();
        this.BespokeServiceID = ko.observable();
        this.ReferrerPrice = ko.observable(referrerPrice);
        this.SupplierPrice = ko.observable(supplierPrice);
        this.CaseID = caseID;
        this.remove = function () {
            ownerViewModel.CaseBespokeServicePricingRows.remove(this);
        };
    }

    function CaseBespokeServicePricing(pricingID, pricingTypeID, referrerPrice, supplierPrice, referrerProjectTreatmentID, pricingTypeName, referrerProjectID, ownerViewModel) {
        this.PricingID = pricingID;
        this.PricingTypeID = pricingTypeID;
        this.ReferrerPrice = referrerPrice;
        this.SupplierPrice = supplierPrice;
        this.ReferrerProjectTreatmentID = referrerProjectTreatmentID;
        this.PricingTypeName = pricingTypeName;
        this.ReferrerProjectID = referrerProjectID;
    }

    function isNumber(n) {
        return !isNaN(parseFloat(n)) && isFinite(n);
    }

   
    this.dateFormat = function (jsonDate) {

        var value = parseJsonDateString(jsonDate);
        var strDate = value.getMonth() + 1 + "/" + value.getDate() + "/" + value.getFullYear();
        return strDate;
    };
    var jsonDateRE = /^\/Date\((-?\d+)(\+|-)?(\d+)?\)\/$/;

    var parseJsonDateString = function (value) {
        var arr = value && jsonDateRE.exec(value);
        if (arr) {
            return new Date(parseInt(arr[1]));
        }

        return value;
    };

   
    this.Submit = function () {

        // TO DO This Code Occuring Error

        //        var errors = "Errors ";
        //        if (!self.isValid()) {
        //            $.each(self.errors(), function (index, value) {
        //                errors = errors + ' ' + value();
        //            });
        //            alert(errors);
        //            return false;
        //        }

        var rating = self.CaseAssessmentRating();

        rating.Rating(self.Rating());

        var json = JSON.stringify({
            CaseAssessment: {
                CaseID: self.CaseID(),
                AssessmentDate: self.DateOfInitialAssessment(),
                HasPatientConsentForm: self.HasPatientConsentForm(),
                IncidentAndDiagnosisDescription: self.IncidentAndDiagnosisDescription(),
                NeuralSymptomDescription: self.NeuralSymptomDescription(),
                PreExistingConditionDescription: self.PreExistingConditionDescription(),
                PsychologicalFactorID: self.PsychologicalFactorID(),
                IsPatientUndergoingTreatment: self.IsPatientUndergoingTreatment(),
                IsPatientTakingMedication: self.IsPatientTakingMedication(),
                PatientRequiresFurtherInvestigation: self.PatientRequiresFurtherInvestigation(),
                FactorsAffectingTreatmentDescription: self.FactorsAffectingTreatmentDescription(),
                PatientOccupation: self.PatientOccupation(),
                PatientRole: self.PatientRole(),
                WasPatientWorkingAtTheTimeOfTheAccident: self.WasPatientWorkingAtTheTimeOfTheAccident(),
                HasThePatientHadTimeOff: self.HasThePatientHadTimeOff(),
                HasThePatientReturnedToWork: self.HasThePatientReturnedToWork(),
                AbsentDetail: self.AbsentDetail(),
                IsPatientSufferingFinancialLoss: self.IsPatientSufferingFinancialLoss(),
                PatientWorkstatusID: self.PatientWorkstatusID(),
                PatientImpactOnWorkID: self.PatientImpactOnWorkID(),
                PatientRecommendedTreatmentSessions: self.PatientRecommendedTreatmentSessions(),
                PatientTreatmentPeriod: self.PatientTreatmentPeriod(),
                AnticipatedDateOfDischarge: self.AnticipatedDateOfDischarge(),
                PatientLevelOfRecoveryID: self.PatientLevelOfRecoveryID(),
                HasPatientHomeExerciseProgramme: self.HasPatientHomeExerciseProgramme(),
                ProposedTreatmentMethodID: self.ProposedTreatmentMethodID(),
                AdditionalInformation: self.AdditionalInformation(),
                IsAccepted: self.IsAccepted(),
                DeniedMessage: self.DeniedMessage(),
                CaseAssessmentPatientImpacts: ko.toJS(self.CaseAssessmentPatientImpacts()),
                CaseAssessmentPatientInjuries: ko.toJS(self.CaseAssessmentPatientInjuries()),
                CaseAssessmentRating: ko.toJS(rating)
            },
            CaseTreatmentPricings: ko.toJS(self.TreatmentPriceRows()),
            CaseBespokeServicePricings: ko.toJS(self.CaseBespokeServicePricingRows())
        });

        $.ajax({
            url: "/InitialAssessmentQA/Update/",
            cache: false,
            dataType: "json",
            contentType: 'application/json',
            data: json,
            type: "POST",
            success: function (resultData) {
                if (resultData == "success") {
                    self.EnableSubmitButton(false);
                    alert("Initial Assessment QA has been submitted");
                    //  self.InitializeInitialAssessmentQAViewModel(self.SelectedTreatmentCategoryID());
                    self.TreatmentPriceRows.removeAll();
                    self.CaseBespokeServicePricingRows.removeAll();
                    self.IsAccepted('clear');
                    self.DeniedMessage(undefined);
                    self.Rating(undefined);
                    //TODO:Should go to Initial screen grid
                    $('#divInitialAccessment').dialog('close');
                }
            }
        });
    };

    this.Close = function () {
        self.ClearValues();
        $('#divInitialAccessment').dialog('close');
    };

    this.ClearValues = function () {
        self.TreatmentPriceRows.removeAll();
        self.CaseBespokeServicePricingRows.removeAll();
        self.IsAccepted('clear');
        self.DeniedMessage(undefined);
        self.Rating(undefined);
    };
};

$(function () {
    $("#divInitialAccessment").dialog({
        autoOpen: false,
        height: 1100,
        width: 1200,
        modal: true
    });
});