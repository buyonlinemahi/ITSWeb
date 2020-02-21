function TriageAssessmentQaViewModel() {

    var self = this;
    self.CaseID = ko.observable();
    self.FirstName = ko.observable();
    self.LastName = ko.observable();
    self.Address = ko.observable();
    self.City = ko.observable();
    self.Region = ko.observable();
    self.PostCode = ko.observable();
    self.HomePhone = ko.observable();
    self.MobilePhone = ko.observable();
    self.CaseNumber = ko.observable();
    self.TreatmentCategoryName = ko.observable();
    self.CaseReferrerReferenceNumber = ko.observable();
    self.TreatmentTypeName = ko.observable();
    self.UploadPath = ko.observable();
    self.TriageAssessmentUploadFile = ko.observable();
    self.SupplierID = ko.observable();

    this.Init = function (model) {
        console.log(model);
        ko.mapping.fromJS(model.CasePatientTreatment, {}, self);
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
    this.CheckTriageFileExtentionAndValidation = function (newValue) {

        if (!$('#IsTreatmentRequired').is(':checked') && !$('#IsTreatmentNotRequired').is(':checked')) {
            alert("Please choose whether treatment is required or not");
            return false;
        }
        var fileUpload = $("#TriageAssessmentUploadFile").val();
        if (fileUpload != "") {
            var extention = $("#TriageAssessmentUploadFile").val().split(".").pop().toUpperCase();
            if (extention == 'PDF' || extention == 'DOC' || extention == 'DOCX' || extention == 'JPG' || extention == 'JPEG' || extention == 'TIFF' || extention == 'TIF') {
                    var iSize = ($("#TriageAssessmentUploadFile")[0].files[0].size / 1024);
                    iSize = (Math.round(iSize * 100) / 100);
                    if (iSize < 10241) {
                        return true;
                    }
                    else {
                        alert('Uploaded file exceed the limit of 10 Mb');
                        ClearTheUploadFIle();
                        return false;
                    }
            }
            else {
                alert("Please select file in .doc, .docx, .jpg , .jpeg , .tif ,  .tiff  or .pdf format"); return false;
            }
        }
        
    }

    this.TriageAssesmentQASumitSucess = function () {
        alert("Triage assessment qa has been processed");
        window.location = '/Internal/InternalTask/';
    };


};
$(function () {
    $(function () {
        $("#triage-Email-Popup").dialog({
            autoOpen: false,
            height: 400,
            width: 300,
            modal: true
        });
    });

    $('#ammendEmail').click(function () {
        $("#triage-Email-Popup").dialog('open');
    });
});
