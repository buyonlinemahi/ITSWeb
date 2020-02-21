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

function CaseAssessmentPatientImpacts(data, name) {
    var self = this;
    self.CaseAssessmentPatientImpactID = ko.observable();
    self.PatientImpactID = ko.observable();
    self.PatientImpactValueID = ko.observable();
    self.Comment = ko.observable();
    self.CaseAssessmentDetailID = ko.observable();
    self.PatientImpactName = ko.observable(name);
    ko.mapping.fromJS(data, {}, self);
};


function CaseTreatmentPricingModel(data, treatmentTypeName, pricingtypeid) {   
    var self = this;
    self.PricingTypeName = ko.observable(treatmentTypeName);
    self.PricingTypeID = ko.observable(pricingtypeid);
    // self.CaseID = ko.observable();
    self.CaseTreatmentPricingID = ko.observable();
    self.DateOfService = ko.observable();
    self.IsComplete = ko.observable();
    self.PatientDidNotAttend = ko.observable();
    self.PriceString = ko.observable();
    self.ReferrerPrice = ko.observable(100);
    self.ReferrerPricingID = ko.observable();
    self.SupplierPrice = ko.observable();
    self.SupplierPriceID = ko.observable();
    if ($("#objQuantityReviewID").val() != "QTY")
        self.Quantity = ko.observable(1);
    else {
        self.Quantity = ko.observable(0);
    }
    self.WasAbandoned = ko.observable();
    self.AuthorizationStatus = ko.observable(null);
    ko.mapping.fromJS(data, {}, self);
};

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



function ReviewAssessmentQAViewModel() {

    var self = this;
    var CaseTreatmentPricingCount = 0;
    self.CaseID = ko.observable();
    self.CaseAssessment = ko.observableArray([]);
    self.CaseAssessmentPatientInjuries = ko.observableArray([]);
    self.PatientImpacts = ko.observableArray([]);
    self.CaseAssessmentPatientImpacts = ko.observableArray([]);
    self.PatientImpactValues = ko.observableArray([]);
    self.IsAccepted = ko.observable();
    self.HasCompliedHomeExerciseProgramme = ko.observable();
    self.HasThePatientHadTimeOff = ko.observable();
    self.HasThePatientReturnedToWork = ko.observable();
    self.IsFurtherTreatmentRecommended = ko.observable();
    self.Practitioners = ko.observableArray([]);
    self.PractitionerID = ko.observable();
    self.NotifyReferrer = ko.observable();
    self.IsNotifyEmailtoReferrer = ko.observable(false).extend({ changeToBool: null });
    self.IsFurtherInvestigationOrOnwardReferralRequired = ko.observable();

    self.CaseAssessmentDetailID = ko.observable();
    self.PatientWorkstatusID = ko.observable();
    self.PatientWorkstatuses = ko.observableArray([]);
    self.PatientImpactOnWorks = ko.observableArray([]);
   
    self.IsNotifyEmailtoReferrer.subscribe(function (newval) {
      
        self.NotifyReferrer(newval);
    });

    //Intial Assesscement incident and diagnosis/injury section drop downs
    self.SymptomDescriptions = ko.observableArray([]);
    self.StrengthTestngs = ko.observableArray([]);
    self.RestrictionRanges = ko.observableArray([]);
    self.AffectedAreas = ko.observableArray([]);

    self.Init = function (model) {
        var mappingOptions = {
            'BirthDate': {
                create: function (options) { if (model.Patient.BirthDate != null) return moment(options.data).format("DD/MM/YYYY"); }
            },
            'InjuryDate': {
                create: function (options) { if (model.Patient.InjuryDate != null) return moment(options.data).format("DD/MM/YYYY"); }
            },
            'CaseBookIADate': {
                create: function (options) { if (model.caseAppointmentDate.CaseBookIADate != null) return moment(options.data).format("DD/MM/YYYY"); }
            },
            'CaseSubmittedDate': {
                create: function (options) { if (model.Case.CaseSubmittedDate != null) return moment(options.data).format("DD/MM/YYYY"); }
            },
            'AnticipatedDateOfDischarge': {
                create: function (options) { if (model.CaseAssessment.AnticipatedDateOfDischarge != null) return moment(options.data).format("DD/MM/YYYY"); }
            }
        };

        ko.mapping.fromJS(model, mappingOptions, self);
        ko.mapping.fromJS(model.CaseAssessment.CaseID, {}, self.CaseID);
        ko.mapping.fromJS(model.TreatmentCategoriesBespokeServices, {}, self.CaseBespokeServicePricings);
        ko.mapping.fromJS(model.CaseAssessment.IsAccepted.toString(), {}, self.IsAccepted);
        ko.mapping.fromJS(model.CaseAssessment.CaseAssessmentDetail.IsFurtherTreatmentRecommended.toString(), {}, self.IsFurtherTreatmentRecommended);
        ko.mapping.fromJS(model.CaseAssessment.CaseAssessmentDetail.IsFurtherInvestigationOrOnwardReferralRequired.toString(), {}, self.IsFurtherInvestigationOrOnwardReferralRequired);

        self.HasThePatientHadTimeOff(model.CaseAssessment.CaseAssessmentDetail.HasThePatientHadTimeOff != null ? model.CaseAssessment.CaseAssessmentDetail.HasThePatientHadTimeOff.toString() : 'nullable');
        self.HasCompliedHomeExerciseProgramme(model.CaseAssessment.CaseAssessmentDetail.HasCompliedHomeExerciseProgramme != null ? model.CaseAssessment.CaseAssessmentDetail.HasCompliedHomeExerciseProgramme.toString() : 'nullable');
        self.HasThePatientReturnedToWork(model.CaseAssessment.CaseAssessmentDetail.HasThePatientReturnedToWork != null ? model.CaseAssessment.CaseAssessmentDetail.HasThePatientReturnedToWork.toString() : 'nullable');
        self.PractitionerID(model.CaseAssessment.CaseAssessmentDetail.PractitionerID);

        ko.mapping.fromJS(model.PatientWorkstatuses, {}, self.PatientWorkstatuses);
        ko.mapping.fromJS(model.PatientImpactOnWorks, {}, self.PatientImpactOnWorks);
        self.PatientWorkstatusID(model.CaseAssessment.CaseAssessmentDetail.PatientWorkstatusID);

        self.SymptomDescriptions(model.SymptomDescriptions);
        self.RestrictionRanges(model.RestrictionRanges);
        self.StrengthTestings(model.StrengthTestings);
        self.AffectedAreas(model.AffectedAreas);

        if (model.CaseAssessment.CaseAssessmentPatientInjuriesBL.length > 0) {
            $.each(model.CaseAssessment.CaseAssessmentPatientInjuriesBL, function (index, value) {
                self.CaseAssessmentPatientInjuries.push(new CaseAssessmentPatientInjury(self.CaseAssessmentDetailID(), value));
            });
        }


        self.CaseAssessmentPatientImpacts([]);
        $.each(model.CaseAssessment.CaseAssessmentPatientImpacts, function (index, value) {
            var impact = new CaseAssessmentPatientImpacts(value, model.PatientImpacts[index].PatientImpactName);
            self.CaseAssessmentPatientImpacts.push(impact);
        });

        self.Practitioners([]);
        $.each(model.Practitioners, function (index, value) {
            self.Practitioners.push(new Practitioner(value));
        });

        self.AppointmentDateTime = ko.computed(function () {
            return self.caseAppointmentDate.strAppointmentDate() + ' ' + self.caseAppointmentDate.strAppointmentTime();
        });

        self.IsFurtherInvestigationOrOnwardReferralRequired.subscribe(function (item) {
            self.CaseAssessment().CaseAssessmentDetail.FurtherInvestigationOrOnwardReferral = ko.observable();
            self.CaseAssessment().CaseAssessmentDetail.EvidenceOfTreatmentRecommendations = ko.observable();
        });

    };

    
    self.CaseTreatmentPricing = ko.observableArray([]);
    self.SelectedReferrerTreatmentPricing = ko.observable();

    self.AddNewTreatmentInReview = function () {
        var ReviewQty = $("#objQuantityReviewID").val();       
        var drop = self.SelectedReferrerTreatmentPricing();
        if (ReviewQty > 0) {
            if (drop == undefined) {
                alert("Please  Select Treatment Type");
            }
            else {
                for (var i = 0; i < ReviewQty; i++) {
                    self.CaseTreatmentPricing.push(new CaseTreatmentPricingModel(self.SelectedReferrerTreatmentPricing()));
                    CaseTreatmentPricingCount++;
                }
                $("#divAddReviewAssessmentCaseTreatmentPricing").modal('hide');
            }
        }
        else {
            alert("Please select the quantity");
        }
       
    };

    self.CaseBespokeServicePricingRows = ko.observableArray([]);
    self.CaseBespokeServicePricings = ko.observableArray([]);
    self.SelectedBespokeServicePricing = ko.observable();
    self.ReferrerPrice = ko.observable();
    self.SupplierPrice = ko.observable();

    self.AddNewBespokeService = function () {
        if (self.ReferrerPrice() == null || self.SupplierPrice() == null || self.ReferrerPrice() == '' || self.SupplierPrice() == '') {
            alert("Referrer & Supplier price are required");
        }
        else {
            self.CaseBespokeServicePricingRows.push(new CaseBespokeServicePricingRow(self.SelectedBespokeServicePricing().TreatmentCategoryBespokeServiceID(), self.ReferrerPrice(), self.SupplierPrice(), self.SelectedBespokeServicePricing().BespokeServiceName(), self.CaseID(), self));
            self.ReferrerPrice(null);
            self.SupplierPrice(null);
            $("#divCreateReviewAssessmentBespoke").modal('hide');
        }
    };

    self.removeCaseTreatmentPricing = function (item)
    {
        if (confirm("Are you sure to delete this pricing")) {
            if (this.CaseTreatmentPricingID() != undefined) {
                $.post("/ReviewAssessment/DeleteCaseTreatmentPricingByCaseTreatmentPricingID", { caseTreatmentPricingID: this.CaseTreatmentPricingID });
            }
            self.CaseTreatmentPricing.remove(item);
            CaseTreatmentPricingCount--;
        }
    }

    self.removeAttachment = function (event) {
        document.getElementById("DocUploadDiv").innerHTML = document.getElementById("DocUploadDiv").innerHTML;

        self.UploadedDocumentName('');
    };
    self.UploadedDocumentName = ko.observable('');
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
                $("#divFinalVersionUpload").modal('hide');
            } else {
                alert('Uploaded file exceed the limit of 10 Mb');
                ClearTheUploadFIle();
            }

        }
        else {
            alert("No file Selected");
        }
    };

    self.UpdateReviewAssessment = function (result) {
        if (result != "UpdatedSuccessfully") {
            alert(result);
        }
        else {
            alert("Successfully updated");
        }
        $("#loader-main-div").addClass("hidden");
         window.location = '/ReviewAssessment/Index';
       // }
    };

   

    self.ReviewAssessmentQAFormBeforeSubmit = function (arr, $form, options) {
        if ($form.jqBootstrapValidation('hasErrors')) return false;
        $("#loader-main-div").removeClass("hidden");
        if (CaseTreatmentPricingCount == 1) {
            if (confirm("You did not add any further treatment, please double check before submitting the report")) {
                $("#loader-main-div").removeClass("hidden");
                return true;
            }
            else {
                $("#loader-main-div").addClass("hidden");
                return false;
            }
        }
        else if (CaseTreatmentPricingCount == 0) {
            $("#loader-main-div").addClass("hidden");
            alert("You did not add any treatment, please double check before submitting the report");
            return false;
        }
        else {
            return true;
        }
    };

    this.PrintReviewAssessmentQA = function () {

        $.post('/PrintPopUp/PrintReviewAssessmentQA', { caseid: self.CaseID() }, function (resp) {
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

    self.GenerateReport = function () {
        $("#frmReviewAssessmentDownloadReport").submit();
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

    this.changeTreatmentPeriod = function () {
        self.showHideTreatmentPeriodOther($("#ddlTreatmentPeriodType").val());
    };
};