function ReviewAssessmentCustomQAViewModel(model) {
    var CaseTreatmentPricingCount = 0;
    self.CaseBespokeServicePricingRows = ko.observableArray([]);
    self.CaseID = ko.observable(model.Case.CaseID);
    self.CaseBespokeServicePricings = ko.observableArray([]);
    self.CaseTreatmentPricing = ko.observableArray([]);
    self.Patient = ko.observableArray([]);
    self.NotifyReferrer = ko.observable();
    self.IsNotifyEmailtoReferrer = ko.observable(false).extend({ changeToBool: null });
    self.SelectedBespokeServicePricing = ko.observable();
    self.ReferrerPrice = ko.observable(model.CaseBespokeServicePricings.ReferrerPrice);
    self.SupplierPrice = ko.observable(model.CaseBespokeServicePricings.SupplierPrice);
    self.isAccepted = ko.observable();
    self.SelectedReferrerTreatmentPricing = ko.observable();
    self.UploadedDocumentName = ko.observable('');
    self.TreatmentReferrerSupplierPricing = ko.observableArray([]);
    self.IsNotifyEmailtoReferrer.subscribe(function (newval) {
        self.NotifyReferrer(newval);
    });
    self.ReviewAssesessmentCustomFilePath = ko.observable(model.ReviewAssesessmentCustomFilePath);
    self.Notify = ko.observable();

    $(document).ready(function () {
        $('.isAccepted').change(function () {
            if (this.value == "true") {
                if ($('#txtMessageSupplier').val().length < 1) {
                    $('#txtMessageSupplier').val(' ');
                }
                $('#divMeassageToSupplier').hide();
            }
            else {
                if (this.value == "false") {

                    $('#divMeassageToSupplier').show();
                }
            }
        });
        $('.isNottify').change(function () {
            if (this.value == "1") {
                self.Notify(true);
            }
            else {
                if (this.value == "0") {
                    self.Notify(false);
                }
            }
        });

    });
    var mappingOptions = {
        'BirthDate': {
            create: function (options) { if (model.Patient.BirthDate != null) return moment(options.data).format("DD/MM/YYYY"); }
        },
        'InjuryDate': {
            create: function (options) { if (model.Patient.InjuryDate != null) return moment(options.data).format("DD/MM/YYYY"); }
        },
        'CaseSubmittedDate': {
            create: function (options) { if (model.Case.CaseSubmittedDate != null) return moment(options.data).format("DD/MM/YYYY"); }
        }
    };
    ko.mapping.fromJS(model, mappingOptions, self);
    ko.mapping.fromJS(model.CaseAssessment.IsAccepted.toString(), {}, self.isAccepted);
    ko.mapping.fromJS(model.TreatmentCategoriesBespokeServices, {}, self.CaseBespokeServicePricings);
    self.AppointmentDateTime1 = ko.computed(function () {
        return self.CaseAppointmentDate.strAppointmentDate() + ' ' + self.CaseAppointmentDate.strAppointmentTime();
    });
    self.UpdateReviewAssessment = function (result) {
        alert("Successfully updated");
        $("#loader-main-div").addClass("hidden");
        window.location = '/ReviewAssessment/Index';
    };
    self.ReviewAssessmentQAFormBeforeSubmit = function (arr, $form, options) {
        if ($('#radioIsAccept').is(':checked')) {
            var file = $("#finalVersionFileUploadFile").val();
            if (file == "") {
                alert("Please Upload a file");
                return false;
            }
        }
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

    self.AddNewTreatment = function () {
        

        if ($("#objQuantityID").val() == 'QTY') {
            alert("Please Select Quantity.");
            return false;
        }

        var InitialQty = $("#objQuantityID").val();

        if (InitialQty > 0) {
            for (var i = 0; i < InitialQty; i++) {
                self.CaseTreatmentPricing.push(new CaseTreatmentPricingModel(self.SelectedReferrerTreatmentPricing()));
                CaseTreatmentPricingCount++;
            }
        }
        $("#divCreateCaseTreatmentGrid").modal('hide');
    };
    self.removeCaseTreatmentPricing = function (item) {
        if (confirm("Are you sure to delete this pricing")) {
            if (this.CaseTreatmentPricingID() != undefined) {
                $.post("/ReviewAssessment/DeleteCaseTreatmentPricingByCaseTreatmentPricingID", { caseTreatmentPricingID: this.CaseTreatmentPricingID });
            }
            self.CaseTreatmentPricing.remove(item);
            CaseTreatmentPricingCount--;
        }
    }
    self.AddNewBespokeService = function () {
        if (self.ReferrerPrice() == null || self.SupplierPrice() == null || self.ReferrerPrice() == '' || self.SupplierPrice() == '') {
            alert("Referrer & Supplier price are required");
        }
        else {
            self.CaseBespokeServicePricingRows.push(new CaseBespokeServicePricingRow(self.SelectedBespokeServicePricing().TreatmentCategoryBespokeServiceID(), self.ReferrerPrice(), self.SupplierPrice(), self.SelectedBespokeServicePricing().BespokeServiceName(), self.CaseID(), self));
            self.ReferrerPrice(null);
            self.SupplierPrice(null);
            $("#divCreateBespokeGrid").modal('hide');
        }
    };

    self.removeAttachment = function (event) {
        document.getElementById("DocUploadDiv").innerHTML = document.getElementById("DocUploadDiv").innerHTML;
        self.UploadedDocumentName('');
    };

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
    function ClearTheUploadFIleFinalVersion() {
        $('#finalVersionFileUploadFile').each(function () {
            $(this).after($(this).clone(true)).remove();
        });

        var control = $('#finalVersionFileUploadFile');
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
                    alert('Please enter only pdf , Jpeg , jpg , png , tif , tiff,docx or doc extention files');
            }
            var iSize = ($("#finalVersionFileUpload")[0].files[0].size / 1024);
            iSize = (Math.round(iSize * 100) / 100);
            if (iSize < 10241) {
                $("#divFinalVersionUpload").modal('hide');
            
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

    self.UploadFinalVersionFile = function (newVal) {
        var file = $("#finalVersionFileUploadFile").val();
        var extension = file.substr((file.lastIndexOf('.') + 1));
        switch (extension.toLowerCase()) {
            case 'doc':
            case 'docx':
            case 'pdf':
            case 'tif':
            case 'tiff':
                $("#divFinalVersionUploadFile").modal('hide');
                break;
            default:
                $("#finalVersionFileUploadFile").val('');
                alert('Please enter only doc,docx,pdf,tif,tiff extention files');
        }
        var iSize = ($("#finalVersionFileUploadFile")[0].files[0].size / 1024);
        iSize = (Math.round(iSize * 100) / 100);
        if (iSize < 10241) {
            $("#divFinalVersionUploadFile").modal('hide');
        }
        else {
            alert('Uploaded file exceed the limit of 10 Mb');
            ClearTheUploadFIleFinalVersion();
        }
    };

}

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
    self.WasAbandoned = ko.observable();
    self.AuthorizationStatus = ko.observable(null);
    if ($("#objQuantityID").val() != "QTY")
        self.Quantity = ko.observable(1);
    else {
        self.Quantity = ko.observable(0);
    }
    ko.mapping.fromJS(data, {}, self);
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