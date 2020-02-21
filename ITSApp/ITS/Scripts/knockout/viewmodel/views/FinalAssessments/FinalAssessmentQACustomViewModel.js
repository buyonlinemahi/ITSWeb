function FinalAssessmentQACustomViewModel() {

    var self = this;
    self.Rating = ko.observable();
    self.CaseID = ko.observable();
    self.NotifyReferrer = ko.observable();
    self.isAccepted = ko.observable(true);
    self.ReferrerProjectTreatmentID = ko.observable();
    self.ReferrerID = ko.observable();
    self.IsNotifyEmailtoReferrer = ko.observable(false).extend({ changeToBool: null });
    self.IsNotifyEmailtoReferrer.subscribe(function (newval) {
        self.NotifyReferrer(newval);
    });

    self.Notify = ko.observable();

    self.Init = function (data) {
        if (data != null) {
            var mappingOptions = {
                'BirthDate': {
                    create: function (options) { if (data.casePatientTreatment.BirthDate != null) return moment(options.data).format("DD/MM/YYYY"); }
                },
                'InjuryDate': {
                    create: function (options) { if (data.casePatientTreatment.InjuryDate != null) return moment(options.data).format("DD/MM/YYYY"); }
                },
                'CaseSubmittedDate': {
                    create: function (options) { if (data.casePatientTreatment.CaseDateOfInquiry != null) return moment(options.data).format("DD/MM/YYYY"); }
                }
            };
            ko.mapping.fromJS(data, mappingOptions, self);
            self.CaseID(data.caseObj.CaseID);
            ko.mapping.fromJS(data.CaseAssessment.IsAccepted.toString(), {}, self.isAccepted);
            self.ReferrerProjectTreatmentID(data.caseObj.ReferrerProjectTreatmentID);
            self.ReferrerID(data.caseObj.ReferrerID);
            self.AppointmentDateTime1 = ko.computed(function () {
                return self.CaseAppointmentDate.strAppointmentDate() + ' ' + self.CaseAppointmentDate.strAppointmentTime();
            });
            ko.mapping.fromJS(data.CaseAssessment.IsAccepted.toString(), {}, self.isAccepted);
        }
    };
    function ClearTheUploadFIle() {
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
    self.UploadFinalVersionFile = function () {
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
                var control = $("#finalVersionFileUploadFile");
                control.replaceWith(control.val('').clone(true));
                alert('Please enter only doc,docx,pdf,tif,tiff extention files');
        }
        var iSize = ($("#finalVersionFileUploadFile")[0].files[0].size / 1024);
        iSize = (Math.round(iSize * 100) / 100);
        if (iSize < 10241) {
            $("#divFinalVersionUploadFile").modal('hide');
        }
        else {
            alert('Uploaded file exceed the limit of 10 Mb');
            ClearTheUploadFIle();
        }
    };

    $(".close").click(function () {
        $("#finalVersionFileUploadFile").val('');
        var control = $("#finalVersionFileUploadFile");
        control.replaceWith(control.val('').clone(true));
    });

    self.UpdateFinalAssessmentQASucess = function (responseText, statusText, xhr, $form) {
        alert("Successfully updated");
        $("#loader-main-div").addClass("hidden");
        window.location = '/FinalAssessments/Index';
    };

    self.FinalAssessmentQACustomBeforeSubmit = function (arr, $form, options) {
        if ($('#radioIsAccept').is(':checked')) {
            var file = $("#finalVersionFileUploadFile").val();
            if (file == "") {
                alert("Please Upload a file");
                return false;
            }
        }
        if ($form.jqBootstrapValidation('hasErrors')) return false;

        $("#loader-main-div").removeClass("hidden");
        return true;
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