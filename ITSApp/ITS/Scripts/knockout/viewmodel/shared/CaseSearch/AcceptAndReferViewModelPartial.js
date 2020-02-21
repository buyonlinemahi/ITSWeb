function AcceptAndReferViewModel(model) {
    var self = this;
    self.isTriage = ko.observable(model.CasePatientTreatment.IsTriage); 
    self.Age = ko.observable();
    self.SelectedSupplier = ko.observable();
    self.SelectedSupplierID = ko.observable();
    self.SelectedTriageSupplier = ko.observable();
    self.workflowID = ko.observable(model.workflowID);

    self.SelectedSupplierID.subscribe(function (value) {
        $.post("/Referral/SelectedSupplierDistanceRankPrice", { supplierID: value, treatmentCategoryID: self.CasePatientTreatment.TreatmentCategoryID(),
            postCode: self.CasePatientTreatment.PostCode()
        }, "json").done(function (response) {

            self.SelectedSupplier(response);

        });

    });
    var mapping = {
        'BirthDate': {
            create: function (options) { if (options.data != null) return moment(options.data).format("DD/MM/YYYY"); }
        },
        'InjuryDate': {
            create: function (options) { if (options.data != null) return moment(options.data).format("DD/MM/YYYY"); }
        }
    };

    self.Init = function (model) {
      
        if (model != null) {
       
            ko.mapping.fromJS(model, mapping, self);
        }
        if (self.CasePatientTreatment.BirthDate != null) {
            var today = new Date();
            var birthDate = new Date(self.CasePatientTreatment.BirthDate);
            var dt = today.getFullYear() - birthDate.getFullYear();
            var m = today.getMonth() - birthDate.getMonth();
            if (m < 0 || (m === 0 && today.getDate() < birthDate.getDate())) {
                dt--;
            }
            self.Age(dt);
        }
    };

    self.UpdateAcceptAndReferFormSucess = function (item) {
        //set timeout to accomodate download functionality of pdf letter.
        setTimeout(function () {
            alert($.parseJSON(item));
            window.location = '/Referral/Index';
        }, 2000)
    };

    self.SubmitGenerateLetterForm = function () {
        $("#frmGenerateLetters").submit();
    }

    self.SaveButtonDisable = ko.observable(false);
    self.SaveValidatedClick = function ($form, event) {

        self.SaveButtonDisable(true);
    };

    self.btnChangeSupplier = function (model) {

        var SupplierName = $('#txtSupplierName').val();
        var value = $('#SupplierID:checked').val();
        var CaseId = $('#hidCaseId').val();
        if (SupplierName == "") {
            $.post("/Referral/CaseSubmitToSupplier", { caseID: CaseId, supplierID: value }, function (data) {
                window.location = document.URL;

            });
        }
        else {
            $.post("/Referral/CaseSubmitToSupplierChange", { caseID: CaseId, supplierID: value }, function (data) {
                window.location = document.URL;

            });
        }
    };

    self.btnChangeTriageSupplier = function (model) {
        var SupplierName = $('#txtSupplierName').val();
        var value = $('#SupplierID:checked').val();
        var CaseId = $('#hidCaseId').val();
        if (SupplierName == "") {
            $.post("/AcceptAndReferTriageAssessment/SubmitTriageCaseToSupplier", { caseID: CaseId, supplierID: value }, function (data) {
                window.location = document.URL;

            });
        }
        else {
            $.post("/AcceptAndReferTriageAssessment/SubmitTriageCaseToSupplierChange", { caseID: CaseId, supplierID: value }, function (data) {
                window.location = document.URL;

            });
        }
    };
  
};