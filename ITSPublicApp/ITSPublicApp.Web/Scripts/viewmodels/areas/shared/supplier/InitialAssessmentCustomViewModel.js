
function InitialAssessmentCustomViewModel() {
    var self = this;
    self.CasePatientTreatment = ko.observable();
    self.CaseService = ko.observable();
    //self.supplierdocumentModel = ko.observableArray();
    self.ReferrerDocument = ko.observableArray();
    self.test = ko.observable();

    this.loadPatientDemographics = function (casePatientTreatment) {
        self.CasePatientTreatment(new CasePatientTreatment(casePatientTreatment));
    };

    this.loadCaseService = function (caseService) {
        self.CaseService(new CaseService(caseService));
    };


    this.loadCaseService = function (caseService) {
        self.CaseService(new CaseService(caseService));
    };
    function ClearTheUploadFIle() {
        $('#InitialAssessmentCustomUploadFile').each(function () {
            $(this).after($(this).clone(true)).remove();
        });

        var control = $('#InitialAssessmentCustomUploadFile');
        control.replaceWith(control.val('').clone(true));

        control.on({
            change: function () { console.log("Changed") },
            focus: function () { console.log("Focus") }
        });
    }
    this.submit = function () {

        var fileUpload = $("#InitialAssessmentCustomUploadFile").val();
        if (fileUpload == null || fileUpload.length <= 0) {
            alert("Please upload an Assessment file for the patient in .doc, .docx, .tif and .pdf format.");
            return false;
        }
        else if (fileUpload != null || fileUpload.length > 0) {
            $("#loader-main-div").addClass("hidden");
            var extensions = fileUpload.split('.').pop().toUpperCase();

            var formData = new FormData();
            var fileInput = document.getElementById('InitialAssessmentCustomUploadFile');
            for (i = 0; i < fileInput.files.length; i++) {
                formData.append(fileInput.files[i].name, fileInput.files[i]);
            }

            if (extensions === 'DOCX' || extensions === 'PDF' || extensions === 'XLSX' || extensions === 'JPG' || extensions === 'JPEG' || extensions === 'PNG') {
                switch (extensions) {
                    case "JPG":
                    case "JPEG":
                    case "PNG":
                        $.ajax({
                            type: "POST",
                            url: "/InitialAssessmentCustom/IsImage",
                            contentType: false,
                            processData: false,
                            data: formData,
                            success: function (_res) {
                                if (_res == 'True') {
                                    return true;
                                }
                                else {
                                    alert("Invalid or Corrupt file");
                                    return false;
                                }
                            },
                            error: function () {
                            }
                        });
                        break;

                    case "DOCX":
                        $.ajax({
                            type: "POST",
                            url: "/InitialAssessmentCustom/IsDocx",
                            contentType: false,
                            processData: false,
                            data: formData,
                            success: function (_res) {
                                if (_res == 'True') {
                                    return true;
                                }
                                else {
                                    alert("Invalid or Corrupt file");
                                    return false;
                                }
                            },
                            error: function () {
                            }
                        });
                        break;

                    case "XLSX":
                        $.ajax({
                            type: "POST",
                            url: "/InitialAssessmentCustom/IsXlsx",
                            contentType: false,
                            processData: false,
                            data: formData,
                            success: function (_res) {
                                if (_res == 'True') {
                                    return true;
                                }
                                else {
                                    alert("Invalid or Corrupt file");
                                    return false;
                                }
                            },
                            error: function () {
                            }
                        });
                        break;
                                            
                    case "PDF":
                        $.ajax({
                            type: "POST",
                            url: "/InitialAssessmentCustom/IsPdf",
                            contentType: false,
                            processData: false,
                            data: formData,
                            success: function (_res) {
                                if (_res == 'True') {                                    
                                    return true;
                                }
                                else {
                                    alert("Invalid or Corrupt file");
                                    return false;
                                }
                            },
                            error: function () {
                            }
                        });
                        break;
                }

                var iSize = ($("#InitialAssessmentCustomUploadFile")[0].files[0].size / 1024);
                iSize = (Math.round(iSize * 100) / 100);
                if (iSize < 10241) {

                    var options = {
                        dataType: 'html',
                        contentType: "application/html; charset=utf-8",
                        success: function (result) {
                            alert('Initial Assessment Custom has been submitted.');
                            window.location.href = '/Supplier/Home';
                        }
                    };

                    $('#InitialAssessmentCustomForm').ajaxSubmit(options);
                    $("#loader-main-div").removeClass("hidden");
                    return true;
                }
                else {
                    alert('Uploaded file exceed the limit of 10 Mb');
                    ClearTheUploadFIle();
                    return false;
                }

            } else {
                alert("Please a select file in .pdf, .docx, .xlsx, .jpg, jpeg and .png format.");
                ClearTheUploadFIle();
                return false;
            }

        }
    };
}

function CasePatientTreatment(model) {
    var self = this;
    self.FirstName = ko.observable(model.FirstName);
    self.LastName = ko.observable(model.LastName);
    self.Address = ko.observable(model.Address);
    self.City = ko.observable(model.City);
    self.Region = ko.observable(model.Region);
    self.PostCode = ko.observable(model.PostCode);
    self.CaseNumber = ko.observable(model.CaseNumber);
    self.CaseReferrerReferenceNumber = ko.observable(model.CaseReferrerReferenceNumber);
    self.TreatmentCategoryName = ko.observable(model.TreatmentCategoryName);
    self.TreatmentTypeName = ko.observable(model.TreatmentTypeName);
    self.HomePhone = ko.observable(model.HomePhone);
    self.MobilePhone = ko.observable(model.MobilePhone);
    self.CaseID = ko.observable(model.CaseID);
    self.TreatmentCategoryID = ko.observable(model.TreatmentCategoryID);

}
function CaseService(model) {
    var self = this;
    self.ReferrerProjectTreatmentID = ko.observable(model.ReferrerProjectTreatmentID);
    self.SupplierID = ko.observable(model.SupplierID);
    self.ReferrerID = ko.observable(model.ReferrerID);
}
