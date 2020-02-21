

function AcceptAndReferTriageAssessmentViewModel() {
    var self = this;

    self.SelectedTriageSupplier = ko.observable();
    self.Age = ko.observable();

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
            var age = today.getFullYear() - birthDate.getFullYear();
            var monthDifference = today.getMonth() - birthDate.getMonth();
            if (monthDifference < 0 || (monthDifference === 0 && today.getDate() < birthDate.getDate())) {
                age--;
            }
            self.Age(age);
        }
    };

    self.UpdateAcceptAndReferTriageFormSucess = function (item) {
        if ($("#ddlSupplierIDCustom option:selected").text() != 'Choose...') {
            $("#loader-main-div").removeClass("hidden");
            alert($.parseJSON(item));
            window.location = '/Referral/';
        } else {
            alert('Please select Supplier');
        }
    };

    self.SaveButtonDisable = ko.observable(false);
    self.SaveValidatedClick = function ($form, event) {

        self.SaveButtonDisable(true);
    };
};
