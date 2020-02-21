function StoppedCasesViewModel(config) {
    var self = this;
    self.CaseUploadArray = ko.observableArray([]);

    self.NewPatientCount = ko.observable();
    self.ExistingPatientsInitialAssessmentCount = ko.observable();
    self.ExistingPatientsNextAssessmentCount = ko.observable();
    self.AuthorisationCount = ko.observable();
    self.StoppedCaseCount = ko.observable();


    self.init = function (model) {
        //ko.mapping.fromJS(model, {}, self.SupplierCasePatients);
        $.ajax({
            url: "/Supplier/StoppedCases/GetStoppedCases",
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
                    self.Message(config.NoNewPatientsFound);
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
   
    self.SupplierCasePatients = ko.observableArray();
    self.Message = ko.observable();

}

