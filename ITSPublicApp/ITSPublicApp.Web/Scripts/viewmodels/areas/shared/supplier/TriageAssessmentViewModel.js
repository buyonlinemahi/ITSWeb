
function TriageAssessmentViewModel() {
    var self = this;
    self.CasePatientTreatment = ko.observable();

    self.test = ko.observable();

    this.loadPatientDemographics = function (casePatientTreatment) {
        self.CasePatientTreatment(new CasePatientTreatment(casePatientTreatment));
    };

    function ClearTheUploadFIle() {
        $('#TriageAssessmentUploadFile').each(function () {
            $(this).after($(this).clone(true)).remove();
        });

        var control = $('#TriageAssessmentUploadFile');
        control.replaceWith(control.val('').clone(true));

        control.on({
            change: function () { console.log("Changed") },
            focus: function () { console.log("Focus") }
        });
    }
    this.submit = function () {
     
        var fileUpload = $("#TriageAssessmentUploadFile").val();
        if (fileUpload == null || fileUpload.length <= 0) {
            alert("Please upload a triage assessment for the patient in .doc, .docx, or .pdf format.");
            return false;
        }
        else if (fileUpload != null || fileUpload.length > 0) {
            
                var extensions = fileUpload.split('.').pop().toUpperCase();
                if (extensions === 'PDF' || extensions === 'DOC' || extensions === 'DOCX' || extensions === 'JPG' || extensions === 'JPEG' || extensions === 'TIFF' || extensions == "TIF") {

                    var iSize = ($("#TriageAssessmentUploadFile")[0].files[0].size / 1024);
                    iSize = (Math.round(iSize * 100) / 100);
                    if (iSize < 10241) {

                            var options = {
                                dataType: 'html',
                                contentType: "application/html; charset=utf-8",
                                success: function (result) {
                                    alert('Triage assessment has been submitted.');
                                    window.location.href = '/Supplier/Home';
                                }
                            };

                            $('#TriageAssessmentForm').ajaxSubmit(options);
                            return true;

                    }
                    else {
                        alert('Uploaded file exceed the limit of 10 Mb');
                        ClearTheUploadFIle();
                        return false;
                    }

                } else {
                    alert("Please a select file in .doc, .docx, .jpg, .jpeg, .tiff ,.tif or .pdf format.");
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
    self.TreatmentCategoryID  =ko.observable(model.TreatmentCategoryID);
}   
        