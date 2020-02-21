function AddPractitionerRegistrationViewModel(practitionerID, registrationTypes, treatmentCategories) {
    var self = this;
    self.PractitionerID = ko.observable(practitionerID);
    self.RegistrationTypes = ko.observableArray(registrationTypes);
    self.TreatmentCategories = ko.observableArray(treatmentCategories);
    self.YearsQualified = ko.observable();
    self.QualificationDate = ko.observable();

    self.QualificationDate.subscribe(function (val) {
        var today = new Date();
        var splitedDate = val.split("/");
        val = splitedDate[2] + "/" + splitedDate[1] + "/" + splitedDate[0];
        var qualificationDate = new Date(val);
        var yearqualifiedDiff = today.getFullYear() - qualificationDate.getFullYear();
        var month = today.getMonth() - qualificationDate.getMonth();
        if (month < 0 || (month === 0 && today.getDate() < qualificationDate.getDate())) {
            yearqualifiedDiff--;
        }
        self.YearsQualified(yearqualifiedDiff);
    });


    self.selectedTreatmentCategoryId = ko.observable();

    self.selectedTreatmentCategoryId.subscribe(function (newValue) {
        if (newValue != null)
            self.GetRegistrationTypesByTreatmentCatagoryId(newValue);

    });

    self.GetRegistrationTypesByTreatmentCatagoryId = function (id) {

        //var ajax = AjaxUtil.post('/PractitionerShared/GetRegistrationTypes', 'json', { treatmentCategoryID: id });
        //ajax.done(function (resp) {
        //    self.RegistrationTypes(resp);
        //});
    };

    self.AddPractitionerRegistrationFormBeforeSubmit = function (arr, $form, options) {
        if ($form.jqBootstrapValidation('hasErrors')) return false;
        return true;
    };
}