function AddSupplierPractitionerViewModel(suppliers, practitionerRegistrations) {
    var self = this;
    self.PractitionerRegistrations = ko.observableArray(practitionerRegistrations);
    self.Suppliers = ko.observableArray(suppliers);
    self.SelectedSupplierID = ko.observable();
    self.SelectedPractitionerRegistrationID = ko.observable();

    self.SelectedSupplierID.subscribe(function (newVal) {
        self.SupplierName($.grep(self.Suppliers(), function (item) {
            return item.SupplierID == newVal;
        })[0].SupplierName);
    });

    self.SelectedPractitionerRegistrationID.subscribe(function (newVal) {
        self.RegistrationNumber($.grep(self.PractitionerRegistrations(), function (item) {
            return item.PractitionerRegistrationID == newVal;
        })[0].RegistrationNumber);
    });

    self.AddPractitionerRegistration = function (responseText) {
        self.PractitionerRegistrations.push($.parseJSON(responseText));
    };

    self.DeletePractitionerRegistration = function (practitionerRegistrationID) {
        //I have commnted this line 
        //console.log(this.PractitionerRegistrations().length);
    };
    self.SupplierName = ko.observable();
    self.RegistrationNumber = ko.observable();

    self.AddSupplierPractitionerFormBeforeSubmit = function (arr, $form, options) {
        if ($form.jqBootstrapValidation('hasErrors')) return false;
        return true;
    };
}