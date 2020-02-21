
ko.extenders.changeToBool = function (target) {
    var result = ko.computed({
        read: target,
        write: function (newValue) {
            if (newValue != undefined) {
                if (newValue == "1") {
                    return target(true);
                } else {
                    return target(false);
                }
            }
        }
    });
    result(target());
    return result;
};
function Practitioner(data) {
    var self = this;
    self.PractitionerID = ko.observable();
    self.PractitionerFirstName = ko.observable();
    self.PractitionerLastName = ko.observable();
    ko.mapping.fromJS(data, {}, self);
    self.PractitionerFullName = ko.computed(function () {
        return self.PractitionerFirstName() + " " + self.PractitionerLastName();
    });
};

function CaseTreatmentPricingModel(data, treatmentTypeName, pricingtypeid) {
    var self = this;
    self.PricingTypeName = ko.observable(treatmentTypeName);
    self.PricingTypeID = ko.observable(pricingtypeid);
    self.CaseID = ko.observable();
    self.CaseTreatmentPricingID = ko.observable();
    self.DateOfService = ko.observable();
    self.IsComplete = ko.observable();
    self.PatientDidNotAttend = ko.observable();
    self.PriceString = ko.observable();
    self.ReferrerPrice = ko.observable();
    self.ReferrerPricingID = ko.observable();
    self.SupplierPrice = ko.observable();
    self.SupplierPriceID = ko.observable();
    self.PatientDidNotAttendDate = ko.observable();
    if ($("#objQuantityID").val() != "QTY")
          self.Quantity = ko.observable(1);
    else {
        self.Quantity = ko.observable(0);
    }
    self.WasAbandoned = ko.observable();
    self.AuthorizationStatus = ko.observable(null);
    ko.mapping.fromJS(data, {}, self);
};



function CaseAssessmentPatientImpact(data, name) {
    var self = this;
    self.CaseAssessmentPatientImpactID = ko.observable();
    self.PatientImpactID = ko.observable();
    self.PatientImpactValueID = ko.observable();
    self.Comment = ko.observable();
    self.CaseAssessmentDetailID = ko.observable();
    self.PatientImpactName = ko.observable(name);
    ko.mapping.fromJS(data, {}, self);
};

function CaseBespokeServicePricingRow(caseBespokeServiceID, treatmentCategoryBespokeServiceID, referrerPrice, supplierPrice, bespokeServiceName, caseID, ownerViewModel) {

    this.CaseBespokeServiceID = ko.observable(caseBespokeServiceID);
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

function CaseAssessmentPatientInjury(caseAssessmentDetailID, data) {
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
    self.OtherSymptomDesciption = ko.observable();
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

function InitialAssessmentQAViewModel() {

    var self = this;
    var CaseTreatmentPricingCount = 0;
    self.CaseID = ko.observable();
    self.CaseAssessmentPatientImpacts = ko.observableArray([]);
    self.CaseAssessment = ko.observableArray([]);
    self.CaseAssessmentRating = ko.observable();
    self.Practitioners = ko.observableArray([]);
    self.PractitionerID = ko.observable();
    self.PatientRoleName = ko.observable();
    self.IsAccepted = ko.observable();


    //Case pricing
    self.CaseTreatmentPricing = ko.observableArray([]);
    self.SelectedReferrerTreatmentPricing = ko.observable();
    //bespoke pricing
    self.CaseBespokeServicePricingRows = ko.observableArray([]);
    self.CaseBespokeServicePricings = ko.observableArray([]);
    self.ProposedTreatmentMethodIDs = ko.observableArray([]);
    self.IsAccepted = ko.observable();
    self.SelectedBespokeServicePricing = ko.observable();
    self.ReferrerPrice = ko.observable();
    self.SupplierPrice = ko.observable();
    self.Quantity = ko.observable(0);
    self.CaseAssessmentDetailID = ko.observable();
    self.CaseAssessmentPatientInjuries = ko.observableArray();

    self.NotifyReferrer = ko.observable();
    self.IsNotifyEmailtoReferrer = ko.observable(false).extend({ changeToBool: null });

    self.IsNotifyEmailtoReferrer.subscribe(function (newval) {
        self.NotifyReferrer(newval);
    });
    
    self.PatientTreatmentPeriodTypeID = ko.observable();
    self.PatientTreatmentPeriodDurationID = ko.observable().extend({ required: { message: "\nOver what period should these be carried out duration is required.", onlyIf: function () { return self.PatientTreatmentPeriodTypeID() === 4 } } });
    self.PatientTreatmentPeriod = ko.observable().extend({ required: { message: "\nOver what period should these be carried out is required.", onlyIf: function () { return self.PatientTreatmentPeriodTypeID() === 4 } }, number: { message: '\nPlease enter a valid number for "Over what period should these be carried out?"' } });

    self.HasRedFlags = ko.observable();
    self.HasYellowFlags = ko.observable();
    self.IsPatientUndergoingTreatment = ko.observable();
    self.IsPatientTakingMedication = ko.observable();
    self.PatientRequiresFurtherInvestigation = ko.observable();
    self.HasThePatientHadTimeOff = ko.observable();
    self.HasThePatientReturnedToWork = ko.observable();
    self.WasPatientWorkingAtTheTimeOfTheAccident = ko.observable();
    self.FactorsAffectingTreatmentDescription = ko.observable();
    self.UploadedDocumentName = ko.observable('');
    //Intial Assesscement incident and diagnosis/injury section drop downs
    self.SymptomDescriptions = ko.observableArray([]);
    self.StrengthTestngs = ko.observableArray([]);
    self.RestrictionRanges = ko.observableArray([]);
    self.AffectedAreas = ko.observableArray([]);
    

    self.PatientWorkstatuses = ko.observableArray([]);
    self.PatientImpactOnWorks = ko.observableArray([]);

    self.AddNewTreatment = function () {

        if ($("#objQuantityID").val() == 'QTY') {
            alert("Please Select Quantity.");
            return false;
        }

        var InitialQty = $("#objQuantityID").val();

        if (InitialQty > 0) {
            for (var i = 0; i < InitialQty; i++)
            {
                self.CaseTreatmentPricing.push(new CaseTreatmentPricingModel(self.SelectedReferrerTreatmentPricing()));
                CaseTreatmentPricingCount++;
            }
        }
        $("#divCreateCaseTreatmentGrid").modal('hide');
    };


  

    self.AddNewBespokeService = function () {
     
        if (self.ReferrerPrice() == null || self.SupplierPrice() == null || self.ReferrerPrice() == '' || self.SupplierPrice() == '') {
            alert("Referrer & Supplier price are required");
        }
        else {
            self.CaseBespokeServicePricingRows.push(new CaseBespokeServicePricingRow(0, self.SelectedBespokeServicePricing().TreatmentCategoryBespokeServiceID(), self.ReferrerPrice(), self.SupplierPrice(), self.SelectedBespokeServicePricing().BespokeServiceName(), self.CaseID(), self));
            self.ReferrerPrice(null);
            self.SupplierPrice(null);
            $("#divCreateBespokeGrid").modal('hide');
        }
    };

    self.Init = function (model) {
        var mappingOptions = {
            'BirthDate': {
                create: function (options) { if (model.Patient.BirthDate != null) return moment(options.data).format("DD/MM/YYYY"); }
            },
            'DateOfInitialAssessment': {
                create: function (options) { if (model.DateOfInitialAssessment != null) return moment(options.data).format("DD/MM/YYYY"); }
            },
            'InjuryDate': {
                create: function (options) { if (model.Patient.InjuryDate != null) return moment(options.data).format("DD/MM/YYYY"); }
            }
            , 'AnticipatedDateOfDischarge': {
                create: function (options) { if (model.CaseAssessment.AnticipatedDateOfDischarge != null) return moment(options.data).format("DD/MM/YYYY"); }
            }
            , 'CaseSubmittedDate': {
                create: function (options) { if (model.Case.CaseSubmittedDate != null) return moment(options.data).format("DD/MM/YYYY"); }
            }
        };



        ko.mapping.fromJS(model, mappingOptions, self);
        ko.mapping.fromJS(model.CaseAssessment.IsAccepted.toString(), {}, self.IsAccepted);
        ko.mapping.fromJS(model.CaseAssessment.HasYellowFlags.toString(), {}, self.HasYellowFlags);
        ko.mapping.fromJS(model.CaseAssessment.HasRedFlags.toString(), {}, self.HasRedFlags);
        ko.mapping.fromJS(model.CaseAssessment.IsPatientUndergoingTreatment.toString(), {}, self.IsPatientUndergoingTreatment);
        ko.mapping.fromJS(model.CaseAssessment.IsPatientTakingMedication.toString(), {}, self.IsPatientTakingMedication);
        ko.mapping.fromJS(model.CaseAssessment.PatientRequiresFurtherInvestigation.toString(), {}, self.PatientRequiresFurtherInvestigation);
        ko.mapping.fromJS(model.CaseAssessment.FactorsAffectingTreatmentDescription, {}, self.FactorsAffectingTreatmentDescription);
        ko.mapping.fromJS(model.TreatmentCategoriesBespokeServices, {}, self.CaseBespokeServicePricings);
        ko.mapping.fromJS(model.PatientImpactValues, {}, self.PatientImpactValues);
        ko.mapping.fromJS(model.PatientImpacts, {}, self.PatientImpacts);
        ko.mapping.fromJS(model.CaseAssessment.CaseID, {}, self.CaseID);
        ko.mapping.fromJS(model.CaseAssessment.CaseAssessmentDetail.CaseAssessmentDetailID, {}, self.CaseAssessmentDetailID);

        self.SymptomDescriptions(model.SymptomDescriptions);
        self.RestrictionRanges(model.RestrictionRanges);
        self.StrengthTestings(model.StrengthTestings);
        self.AffectedAreas(model.AffectedAreas);
       
        ko.mapping.fromJS(model.PatientWorkstatuses, {}, self.PatientWorkstatuses);
        ko.mapping.fromJS(model.PatientImpactOnWorks, {}, self.PatientImpactOnWorks);
        
        self.PatientTreatmentPeriodTypeID(model.CaseAssessment.CaseAssessmentDetail.TreatmentPeriodTypeID);
        self.showHideTreatmentPeriodOther(self.PatientTreatmentPeriodTypeID());
        self.PatientTreatmentPeriod(model.CaseAssessment.CaseAssessmentDetail.PatientTreatmentPeriod);


        if (model.CaseAssessment.CaseAssessmentPatientInjuriesBL.length > 0) {
            $.each(model.CaseAssessment.CaseAssessmentPatientInjuriesBL, function (index, value) {
                self.CaseAssessmentPatientInjuries.push(new CaseAssessmentPatientInjury(self.CaseAssessmentDetailID(), value));
            });
        }

        if (model.CaseTreatmentPricings.length > 0) {
            $.each(model.CaseTreatmentPricings, function (index, value) {

                if (value.ReferrerPricingID === model.TreatmentReferrerSupplierPricing[index].ReferrerPricingID) {
                    self.CaseTreatmentPricing.push(new CaseTreatmentPricingModel(value, model.TreatmentReferrerSupplierPricing[index].PricingTypeName));
                    CaseTreatmentPricingCount++;
                }
            });
        }


        if (model.CaseAssessment.ProposedTreatmentMethodIDs.length > 0) {
            $.each(model.CaseAssessment.ProposedTreatmentMethodIDs, function (index, value) {
                self.ProposedTreatmentMethodIDs.push(value.toString());
            });
        }


        self.PractitionerID(model.CaseAssessment.CaseAssessmentDetail.PractitionerID);

        self.HasThePatientHadTimeOff(model.CaseAssessment.CaseAssessmentDetail.HasThePatientHadTimeOff != null ? model.CaseAssessment.CaseAssessmentDetail.HasThePatientHadTimeOff.toString() : 'null');
        self.HasThePatientReturnedToWork(model.CaseAssessment.CaseAssessmentDetail.HasThePatientReturnedToWork != null ? model.CaseAssessment.CaseAssessmentDetail.HasThePatientReturnedToWork.toString() : 'null');
        self.WasPatientWorkingAtTheTimeOfTheAccident(model.CaseAssessment.WasPatientWorkingAtTheTimeOfTheAccident != null ? model.CaseAssessment.WasPatientWorkingAtTheTimeOfTheAccident.toString() : 'null');

        self.CaseAssessmentPatientImpacts([]);
        $.each(model.CaseAssessment.CaseAssessmentPatientImpacts, function (index, value) {
            var impact = new CaseAssessmentPatientImpact(value, model.PatientImpacts[index].PatientImpactName);
            self.CaseAssessmentPatientImpacts.push(impact);
        });

        this.patientrolename = ko.utils.arrayFirst(model.PatientRole, function (item) {
            if (model.CaseAssessment.PatientRoleID === item.PatientRoleID) {
                self.PatientRoleName(item.PatientRoleName);
            }
        });
        self.Practitioners([]);
        $.each(model.Practitioners, function (index, value) {
            self.Practitioners.push(new Practitioner(value));
        });

        self.AppointmentDateTime = ko.computed(function () {
            return self.CaseAppointmentDate.strAppointmentDate() + ' ' + self.CaseAppointmentDate.strAppointmentTime();
        });
    };
    self.removeAttachment = function (event) {
        document.getElementById("DocUploadDiv").innerHTML = document.getElementById("DocUploadDiv").innerHTML;
        self.UploadedDocumentName('');
    };

    this.EnableTextBox = function (item, event) {
        var context = ko.contextFor(event.target);
        var index = context.$index();
        var dropvalue = $("#dropdown_" + index).val();
        if (dropvalue == "18") {
            $("#prefix_" + index).removeAttr("disabled");
        }
        else
        {
            $("#prefix_" + index).attr("disabled", "disabled");
        }
    }

    function ClearTheUploadFIle() {
        $('#finalVersionFileUpload').each(function () {
            $(this).after($(this).clone(true)).remove();
        });

        var control = $('#finalVersionFileUpload');
        control.replaceWith(control.val('').clone(true));

        control.on({
            change: function () { console.log("Changed") },
            focus: function () { console.log("Focus") }
        });
    }
    self.UploadFinalVersion = function (newVal) {
        var file = $("#finalVersionFileUpload").val();
        if (file != '') {
            if (self.UploadedDocumentName() == '') {
                alert('Document Name is Required.');
                return;
            }
            var extension = file.substr((file.lastIndexOf('.') + 1));
            switch (extension.toLowerCase()) {
                case 'pdf':
                case 'doc':
                case 'docx':
                case 'jpeg':
                case 'jpg':
                case 'tiff':
                case 'png':
                case 'tif':
                    $("#divFinalVersionUpload").modal('hide');
                    break;
                default:
                    $("#finalVersionFileUpload").val('');
                    alert('Please enter only pdf , Jpeg , jpg , png , tif , tiff or doc extention files');
            }
            var iSize = ($("#finalVersionFileUpload")[0].files[0].size / 1024);
            iSize = (Math.round(iSize * 100) / 100);
            if (iSize < 10241) {
             
            }
            else {
                alert('Uploaded file exceed the limit of 10 Mb');
                ClearTheUploadFIle();
            }

        }
        else {
            alert("No file Selected");
        }
    };

    self.removeCaseTreatmentPricing = function (item) {
        if (confirm("Are you sure to delete this pricing"))
        {
            if (this.CaseTreatmentPricingID() != undefined)
            {
                $.post("/InitialAssessment/DeleteCaseTreatmentPricingByCaseTreatmentPricingID", { caseTreatmentPricingID: this.CaseTreatmentPricingID });
            }
            self.CaseTreatmentPricing.remove(item);
            CaseTreatmentPricingCount--;
        }
    }


    self.UpdateInitialAssessment = function (result) {
        alert("Successfully updated");
        $("#loader-main-div").addClass("hidden");
        window.location = '/InitialAssessment/Index';
    };

    self.UpdateInitialAssesmentQAFormBeforeSubmit = function (arr, $form, options) {
        if ($form.jqBootstrapValidation('hasErrors')) return false;
        $("#loader-main-div").removeClass("hidden");
        if (CaseTreatmentPricingCount == 1)
        {
            if (confirm("You did not add any further treatment, please double check before submitting the report")) {
                $("#loader-main-div").removeClass("hidden");
                return true;
            }
            else {
                $("#loader-main-div").addClass("hidden");
                return false;
            }
        }
        else if (CaseTreatmentPricingCount == 0)
        {
            $("#loader-main-div").addClass("hidden");
            alert("You did not add any treatment, please double check before submitting the report");
            return false;
        }
        else
        {
            return true;
        }
    };

    this.PrintInitialAssessmentQA = function () {
        $.post('/PrintPopUp/PrintInitialAssessmentQA', { caseid: this.CaseID }, function (resp) {
            var mywindow;
            if (navigator.appName == 'Microsoft Internet Explorer') {
                mywindow = window.open('', '', '');
                mywindow.document.write(resp);
                mywindow.document.close();
                mywindow.focus();
                mywindow.print();
                mywindow.close();
            }
            else {
                mywindow = window.open('', '', '');
                mywindow.document.write(resp);
                mywindow.print();
            }
            return false;
        }, "html");

    };

    self.GenerateReport = function () {
        $("#frmInitialAssessmentDownloadReport").submit();
    }
    // Over what period should these be carried out? DropDown And TextBox Show Hide condition
    this.showHideTreatmentPeriodOther = function (treatmentTypeID) {
          if (treatmentTypeID == 4) {
            $("#divTreatmentPeriodOther").removeClass("hide");
            $("#divSpaceHeight").removeClass("hide");
        }
        else {
            $("#divSpaceHeight").addClass("hide");
            $("#divTreatmentPeriodOther").addClass("hide");
        }
    };

    this.changeTreatmentPeriod = function ()
    {
           self.showHideTreatmentPeriodOther($("#ddlTreatmentPeriodType").val());
    };
}

