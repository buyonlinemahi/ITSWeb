function CasePatientTreatment() {
    var self = this;
    // Pricing Matrix
    self.CaseTreatmentPricingTypeArray = ko.observableArray([]);
    self.CaseBespokeServicePricingsArray = ko.observableArray([]);
    self.FirstName = ko.observable();
    self.LastName = ko.observable();
    self.Address = ko.observable();
    self.City = ko.observable();
    self.Region = ko.observable();
    self.PostCode = ko.observable();
    self.CaseNumber = ko.observable();
    self.CaseReferrerReferenceNumber = ko.observable();
    self.TreatmentCategoryName = ko.observable();
    self.TreatmentTypeName = ko.observable();
    self.HomePhone = ko.observable();
    self.MobilePhone = ko.observable();
    self.CaseID = ko.observable();
    self.TreatmentCategoryID = ko.observable();
    self.ReferrerProjectTreatmentID = ko.observable();
    self.IsFurtherTreatment = ko.observable();
    self.AssessmentCustomUploadFile = ko.observableArray([]);
    self.SupplierID = ko.observable();
    self.ReferrerID = ko.observable();
    self.EncryptedCaseID = ko.observable();
    self.RequiredTraetment = ko.observable('Next');
    
    self.Init = function (model) {        
        if (model != null) {
            self.FirstName = ko.observable(model.casePatientTreatment.FirstName);
            self.LastName = ko.observable(model.casePatientTreatment.LastName);
            self.Address = ko.observable(model.casePatientTreatment.Address);
            self.City = ko.observable(model.casePatientTreatment.City);
            self.Region = ko.observable(model.casePatientTreatment.Region);
            self.PostCode = ko.observable(model.casePatientTreatment.PostCode);
            self.CaseNumber = ko.observable(model.casePatientTreatment.CaseNumber);
            self.CaseReferrerReferenceNumber = ko.observable(model.casePatientTreatment.CaseReferrerReferenceNumber);
            self.TreatmentCategoryName = ko.observable(model.casePatientTreatment.TreatmentCategoryName);
            self.TreatmentTypeName = ko.observable(model.casePatientTreatment.TreatmentTypeName);
            self.HomePhone = ko.observable(model.casePatientTreatment.HomePhone);
            self.MobilePhone = ko.observable(model.casePatientTreatment.MobilePhone);
            self.CaseID = ko.observable(model.casePatientTreatment.CaseID);
            self.TreatmentCategoryID = ko.observable(model.casePatientTreatment.TreatmentCategoryID);
            self.ReferrerProjectTreatmentID = ko.observable(model.CaseService.ReferrerProjectTreatmentID);
            if (model.caseAssessmentCustom != null) {
                self.IsFurtherTreatment = ko.observable(model.caseAssessmentCustom.IsFurtherTreatment);
            }
            self.AssessmentFileCustomURLPath = ko.observable(model.AssessmentFileCustomURLPath);
            self.SupplierID = ko.observable(model.CaseService.SupplierID);
            self.ReferrerID = ko.observable(model.CaseService.ReferrerID);
            self.EncryptedCaseID(model.CaseService.EncryptedCaseID);
            ko.mapping.fromJS(model.AssessmentCustomUploadFile, {}, self.AssessmentCustomUploadFile);
            $.each(model.CaseTreatmentPricingTypes, function (index, value) {

                if (value.DateOfService !== null) {
                    self.CaseTreatmentPricingTypeArray.push(new CaseTreatmentPricingType(value, true, false, false));
                }
                else if (value.PatientDidNotAttend !== null) {
                    self.CaseTreatmentPricingTypeArray.push(new CaseTreatmentPricingType(value, false, true, false));
                }
                else if (value.PatientDidNotAttendDate !== null) {
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



        }
    }
    ko.bindingHandlers.datepicker = {
        init: function (element, valueAccessor) {
            var options = valueAccessor();
            $(element).datepicker(options || {});
        }
    };
    self.errors = ko.validation.group(self);


    function ClearTheUploadFIle() {
        $('#AssessmentCustomUploadFile').each(function () {
            $(this).after($(this).clone(true)).remove();
        });

        var control = $('#AssessmentCustomUploadFile');
        control.replaceWith(control.val('').clone(true));

        control.on({
            change: function () { console.log("Changed") },
            focus: function () { console.log("Focus") }
        });
    }
    $(document).ready(function () {
        $("#frmReviewAssessment").ajaxForm({ 
            beforeSubmit: function () {
                var fileUpload = $("#AssessmentCustomUploadFile").val();
                if (fileUpload == null || fileUpload.length <= 0) {
                    alert("Please upload an Assessment file for the patient in .doc or .docx format.");
                    return false;
                }
                else if (fileUpload != null || fileUpload.length > 0) {                   
                        $("#loader-main-div").addClass("hidden");
                        var extensions = fileUpload.split('.').pop().toUpperCase();
                        if (extensions === 'DOC' || extensions === 'DOCX' || extensions === 'PDF' || extensions === 'TIF' || extensions === 'TIFF') {
                            var iSize = ($("#AssessmentCustomUploadFile")[0].files[0].size / 1024);
                            iSize = (Math.round(iSize * 100) / 100);
                            if (iSize < 10241) {
                            $("#loader-main-div").removeClass("hidden");
                            }
                            else {
                                alert('Uploaded file exceed the limit of 10 Mb');
                                ClearTheUploadFIle();
                                return false;
                            }

                            //return true;
                        } else {
                            alert("Please a select file in .doc, .docx, .tif, .tiff and .pdf  format.");
                            ClearTheUploadFIle();
                            return false;
                        }
                    
                }
                $("#loader-main-div").removeClass("hidden");
            },
            success: function (response, statusText, xhr, $form) {
                if (response === "\"Sucessfully Submitted\"") {
                    alert("Sucessfully Submitted");
                    $("#loader-main-div").addClass("hidden");
                    window.location.href = '/Supplier/ExistingPatientsNextAssessment';
                }
            }
        });
        $("#frmReviewAssessmentCustom1").ajaxForm({

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
        $('.RequiredTreatment').change(function () {
            if (this.value == "true") {
                self.RequiredTraetment('Final');
            }
            else {
                if (this.value == "false") {
                    self.RequiredTraetment('Review');
                }
            }
        });

    });
   
}
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