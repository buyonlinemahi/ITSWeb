function ExistingPatientsNextAssessmentViewModel(config) {
    var self = this;
    self.CaseUploadArray = ko.observableArray([]);


    self.NewPatientCount = ko.observable();
    self.ExistingPatientsInitialAssessmentCount = ko.observable();
    self.ExistingPatientsNextAssessmentCount = ko.observable();
    self.AuthorisationCount = ko.observable();
    self.StoppedCaseCount = ko.observable();


    self.init = function () {
        self.loadGrid();
    };
    self.loadGrid = function () {
        $('#divGridDisplaySpinner').spin();
        $.ajax({
            url: "/Supplier/ExistingPatientsNextAssessment/GetNextAssessments",
            type: 'post',
            dataType: 'json',
            success: function (result) {
                self.SupplierCasePatients(result.supplierCasePatient);

                if (result.notificationBubble != "") {
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
                    self.Message(config.NoNextAssessmentFound);
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

    self.OpenLink = function (item) {

        //TODO:screen can be review assessment or final assessment based on assessmentserviceid or workflowid
        if (this.WorkflowID == 115)
            window.location = "ReviewAssessmentCustom/Index/" + this.EncryptedCaseID;
        else if (this.WorkflowID == 170)
            window.location = "FinalAssessment/Index/" + this.EncryptedCaseID;
        else
            window.location = "ReviewAssessment/Index/" + this.EncryptedCaseID;
    };
    self.PrinBlankRAFA = function () {
        if (this.WorkflowID == 170)
            $.post('/PrintPopUp/PrintBlankFinalAssessment', { caseid: this.EncryptedCaseID }, function (resp) {
                PrintForm(resp);
            });
        else
            $.post('/PrintPopUp/PrintBlankReviewAssessment', { caseid: this.EncryptedCaseID }, function (resp) {
                PrintForm(resp);
            });
    };

    self.GetEPNATreatmentSessionDetail = function () {
        //alert(this.CaseID);
        $.post('/Supplier/ExistingPatientsNextAssessment/GetEPNATreatmentSessionDetail', { _caseID: this.EncryptedCaseID },
            function (data) {
                $("#txtSessionsAuthorised").val(data.SessionsAuthorised);
                $("#txtSessionsAttended").val(data.SessionsAttended);
            });
    };
    



    function PrintForm(resp) {
        var mywindow;
        if (navigator.appName == 'Microsoft Internet Explorer') {
            mywindow = window.open('', '', '');
            mywindow.document.write(resp);
            mywindow.document.close();
            mywindow.focus();
            mywindow.print();
            mywindow.close();
        } else {
            mywindow = window.open('', '', '');
            mywindow.document.write(resp);
            mywindow.print();
        }
        return false;

    };
    self.SupplierCasePatients = ko.observableArray();
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