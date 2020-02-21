function AuthorisationViewModel(config) {
    var self = this;
    self.CaseUploadArray = ko.observableArray([]);

    self.NewPatientCount = ko.observable();
    self.ExistingPatientsInitialAssessmentCount = ko.observable();
    self.ExistingPatientsNextAssessmentCount = ko.observable();
    self.AuthorisationCount = ko.observable();
    self.StoppedCaseCount = ko.observable();

    self.init = function () {
        $('#divGridDisplaySpinner').spin();
        $.ajax({
            url: "/Supplier/Authorisation/GetAuthorisations",
            type: 'post',
            dataType: 'json',
            success: function (result) {
                self.SupplierCasePatients(result.supplierCasePatient);

                if (result.notificationBubble != "")
                {
                    self.NewPatientCount(result.notificationBubble.NewPatientCount);
                    self.ExistingPatientsInitialAssessmentCount(result.notificationBubble.ExistingPatientsInitialAssessmentCount);
                    self.ExistingPatientsNextAssessmentCount(result.notificationBubble.ExistingPatientsNextAssessmentCount);
                    self.AuthorisationCount(result.notificationBubble.AuthorisationCount);
                    self.StoppedCaseCount(result.notificationBubble.StoppedCaseCount);

                    if (result.notificationBubble.NewPatientCount == 0)
                        $("#NewPatientCount").css("opacity", "0");
                    if (result.notificationBubble.ExistingPatientsInitialAssessmentCount == 0)
                        $("#ExistingPatientsInitialAssessmentCount").css("opacity", "0");
                    if (result.notificationBubble.ExistingPatientsNextAssessmentCount == 0)
                        $("#ExistingPatientsNextAssessmentCount").css("opacity", "0");
                    if (result.notificationBubble.AuthorisationCount == 0)
                        $("#AuthorisationCount").css("opacity", "0");
                    if (result.notificationBubble.StoppedCaseCount == 0)
                        $("#StoppedCaseCount").css("opacity", "0");
                }

                if (result.length == 0) {
                    self.Message(config.NoAuthorisationFound);
                    $('#grid-div').hide();
                } else {
                    $('#grid-div').show();
                }
                $('#divGridDisplaySpinner').spin(false);
            },
            error: function () {
                $('#divGridDisplaySpinner').spin(false);
            }
        });

    };


    self.ViewAuthorisation = function () {
        window.location = 'Authorisation/AuthorityAcceptanceScreen/' + this.EncryptedCaseID;
    };
    self.SupplierCasePatients = ko.observableArray();
    self.notificationBubble = ko.observableArray();
    self.Message = ko.observable();
    self.PressBtnViewUploads = function (data) {
        $("#btnUploadDoc").click();
        $.ajax({
            url: "/Supplier/CaseSearch/CaseUploadsDocumentUser",
            type: 'post',
            dataType: 'json',
            data: { id: data.EncryptedCaseID },
            success: function (model) {
                self.CaseUploadArray.removeAll();
                $.each(model, function (index, value) {
                    self.CaseUploadArray.push(new CaseUploadModelUpdate(value));
                });

            },
            error: function () {

            }
        });
        $.post("", { id: data.EncryptedCaseID }, function (model) {


        });
    };
    self.ViewUploads = function (data) {
         var DoctypeID = data.DocumentTypeID();
        if (DoctypeID == 7 || DoctypeID == 8 || DoctypeID == 9 || DoctypeID == 10) {
            window.location = '/file/ViewCaseUploads?UploadPath=' + data.UploadPath() + '&DocumentName=' + data.DocumentName();
        }
        else {
            window.location = '/Supplier/CaseSearch/ViewCaseUploads?UploadPath=' + data.UploadPath() + '&CaseId=' + data.EncryptedCaseID();
        }

       };

    function CaseUploadModelUpdate(model) {
        var self = this;
        self.ReferrerDocumentTypeDesc = ko.observable(model.ReferrerDocumentTypeDesc);
        self.DocumentName = ko.observable(model.DocumentName);
        self.UploadDate = ko.observable(moment(model.UploadDate).format("DD/MM/YYYY"));
        self.CaseID = ko.observable(model.CaseID);
        self.UploadPath = ko.observable(model.UploadPath);
        self.DocumentTypeID = ko.observable(model.DocumentTypeID);
        self.EncryptedCaseID = ko.observable(model.EncryptedCaseID);
    };
}