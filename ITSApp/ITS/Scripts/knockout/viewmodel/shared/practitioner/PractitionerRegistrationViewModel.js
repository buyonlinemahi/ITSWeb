function PractitionerRegistrationModel(registrations, registrationTypes, treatmentCategories) {
    var self = this;
    self.PractitionerRegistrationID = ko.observable();
    self.PractitionerID = ko.observable();
    self.RegistrationNumber = ko.revertableObservable();
    self.Qualification = ko.revertableObservable();
    self.QualificationDate = ko.revertableObservable();
    self.ExpiryDate = ko.revertableObservable();
    self.RegistrationTypeID = ko.revertableObservable();
    self.TreatmentCategoryID = ko.revertableObservable();
    self.RegistrationTypes = ko.observableArray(registrationTypes);
    self.TreatmentCategories = ko.observableArray(treatmentCategories);
    self.YearsQualified = ko.revertableObservable();
    self.selectedTreatmentCategoryId = ko.observable();

    self.selectedTreatmentCategoryId.subscribe(function (newValue) {
        if (newValue != null) {
            self.TreatmentCategoryID(newValue);
            self.GetRegistrationTypesByTreatmentCatagoryId(newValue);
        }
    });

    self.GetRegistrationTypesByTreatmentCatagoryId = function (id) {
        //var ajax = AjaxUtil.post('/PractitionerShared/GetRegistrationTypes', 'json', { treatmentCategoryID: id });
        //ajax.done(function (resp) {
        //    self.RegistrationTypes(resp);
        //});
    };

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


    ko.mapping.fromJS(registrations, {}, self);
    ko.CommitChanges(self);


    self.UpdateRegistrationFormBeforeSubmit = function (arr, $form, options) {
        if ($form.jqBootstrapValidation('hasErrors')) return false;
        return true;
    };
}



function PractitionerRegistrationGridViewModel(registrationTypes, treatmentCategories) {
    var self = this;
    self.SelectedPractitionerRegistration = ko.observable();
    self.PractitionerRegistrations = ko.observableArray();
    self.Init = function (registrations) {
        $.each(registrations, function (index, value) {
            value.QualificationDate = moment(value.QualificationDate).format('DD/MM/YYYY');
            value.ExpiryDate = moment(value.ExpiryDate).format('DD/MM/YYYY');
            self.PractitionerRegistrations.push(new PractitionerRegistrationModel(value, registrationTypes, treatmentCategories));
        });


    };
    self.ViewPractitionerRegistration = function (item) {
        self.SelectedPractitionerRegistration(item);
        ko.CommitChanges(self.SelectedPractitionerRegistration());
    };

    self.AddPractitionerRegistration = function (responseText) {
        var response = $.parseJSON(responseText);
        response.QualificationDate = moment(response.QualificationDate).format('DD/MM/YYYY');
        response.ExpiryDate = moment(response.ExpiryDate).format('DD/MM/YYYY');
        self.PractitionerRegistrations.push(new PractitionerRegistrationModel(response, registrationTypes, treatmentCategories));
    };

    self.DeletePractitionerRegistration = function (item) {
        var ctx = confirm('Are you sure you want to delete this registration?');
        if (ctx) {
            var ajax = AjaxUtil.post('/PractitionerShared/DeleteRegistration', 'json', { practitionerRegistrationID: item.PractitionerRegistrationID() });
            ajax.done(function (resp) {
                self.PractitionerRegistrations.remove(function (e) {
                    return e.PractitionerRegistrationID == item.PractitionerRegistrationID;
                });
            });
        }
        return item.PractitionerRegistrationID();
    };

    self.CloseClick = function (item) {
        ko.RevertChanges(item);
    };

    self.UpdateSelectedPractitionerRegistration = function () {
        ko.CommitChanges(self.SelectedPractitionerRegistration);
        $('#divViewRegistration').modal('hide');
    };
}