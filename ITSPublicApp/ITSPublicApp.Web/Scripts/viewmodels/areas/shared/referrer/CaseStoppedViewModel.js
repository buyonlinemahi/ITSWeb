
function CaseStoppedViewModel() {

    var self = this;
    ko.validation.configure({
        insertMessages: false,
        grouping: { deep: true }
    });
    self.caseID = ko.observable();
    self.EncryptedCaseID = ko.observable();
    self.DownloadPath = ko.observable();
    self.authorisationDeniedMsg = ko.observable().extend({ required: { message: "Reason to close is required."} });
    self.ReferralDownloadPath = ko.observable();
    self.errors = ko.validation.group(self);
    self.IsCustom = ko.observable();
    this.Init = function (model) {
        self.EncryptedCaseID(model.caseObj.EncryptedCaseID);
        self.caseID(model.caseObj.CaseID);
        self.ReferralDownloadPath('/File/ViewReferral/?caseID=' + self.EncryptedCaseID());
        self.DownloadPath(model.DownloadPath);
        self.IsCustom(model.caseObj.IsCustom);

    };

    this.ViewAssessment = function () {

        $("#divGridDisplaySpinner").spin();

                $.ajax({
                    url: '/Referrer/Home/ViewAssessment/',
                    type: "post",
                    data: { id: this.EncryptedCaseID() },
                    success: function (resp) {
                  
                        var mywindow;
                        mywindow = window.open('', '', '');
                        mywindow.document.write(resp);
                        mywindow.document.close();
                        mywindow.focus();
                        $("#divGridDisplaySpinner").spin(false);
                    },
                    error: function () {
                        alert("Error while View Assessment");
                        $("#divGridDisplaySpinner").spin(false);
                    }
                });

    };

    this.SubmitClick = function () {
        var errors = "Errors ";
        if (!self.isValid()) {
            $.each(self.errors(), function (index, value) {
                errors = errors + ' ' + value();
            });
            alert(errors);
            return false;
        }

        $("#loader-main-div").removeClass("hidden");
        var jsonData = JSON.stringify({
            caseID: self.EncryptedCaseID(),
            authorisationDeniedMsg: self.authorisationDeniedMsg()

        });
        $.ajax({
            url: "/Referrer/CaseStopped/UpdateCaseAssessmentAuthorisationToClosedByCaseID",
            type: "POST",
            contentType: 'application/json',
            dataType: "json",
            data: jsonData,
            success: function (result) {
                $("#loader-main-div").addClass("hidden");
                alert(result);                
                window.location.href = '/Referrer/Home/Index/';
            },
            error: function () {
                $("#loader-main-div").addClass("hidden");
                alert("Error while updating the Case");
            }
        });

    };
}
