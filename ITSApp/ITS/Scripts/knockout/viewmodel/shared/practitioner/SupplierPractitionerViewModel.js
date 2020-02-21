function SupplierPractitionerModel(data, suppliers, practitionerRegistrations) {
    var self = this;
    self.SupplierPractitionerID = ko.observable();
    self.SupplierID = ko.observable();
    self.SupplierName = ko.observable();
    self.PractitionerRegistrationID = ko.revertableObservable();
    self.RegistrationNumber = ko.observable();
    ko.mapping.fromJS(data, {}, self);

    ko.CommitChanges(self);

    self.Suppliers = ko.observableArray(suppliers);
    self.PractitionerRegistrations = ko.observableArray(practitionerRegistrations);

    //MARKED, FOR SIMPLIFICATION, NEWVAL COULD BE THE OBSERVABLE/JSON OBJECT ITSELF THAT COULD HAVE THE REGISTRATION NUMBER AND ID. DOING GREP MAKES NO SENSE.
    self.PractitionerRegistrationID.subscribe(function(newVal) {
        self.RegistrationNumber($.grep(self.PractitionerRegistrations(), function (item) {
            return item.PractitionerRegistrationID == newVal;
        })[0].RegistrationNumber);
    });
    
    self.SaveButtonDisable = ko.observable(false);
    self.SaveValidatedClick = function ($form, event) {
        self.SaveButtonDisable(true);
    };
}

function SupplierPractitionerGridViewModel(suppliers, practitionerRegistrations) {
    var self = this;
    self.SelectedSupplierPractitioner = ko.observable();
    self.SupplierPractitioners = ko.observableArray();
    self.PractitionerRegistrations = ko.observableArray(practitionerRegistrations);
    self.Suppliers = ko.observableArray(suppliers);

    self.Init = function (supplierPractitioners) {
        $.each(supplierPractitioners, function (index, value) {
            self.SupplierPractitioners.push(new SupplierPractitionerModel(value, self.Suppliers(), self.PractitionerRegistrations()));
        });
    };
    self.ViewSupplierPractitioner = function (item) {
        self.SelectedSupplierPractitioner(item);
    };
    self.AddSupplierPractitioner = function (item) {
        self.SupplierPractitioners.push(new SupplierPractitionerModel($.parseJSON(item), self.Suppliers(), self.PractitionerRegistrations()));
        $('#divAddSupplier').modal('hide');
    };
    self.AddPractitionerRegistration = function (responseText) {
      
        self.PractitionerRegistrations.push($.parseJSON(responseText));
    };
    self.DeletePractitionerRegistration = function (practitionerRegistrationID) {
        self.PractitionerRegistrations.remove(function (e) {
            return e.PractitionerRegistrationID == practitionerRegistrationID;
        });
    };
    self.DeletePractitionerSupplier = function (item) {
        var ajax = AjaxUtil.post('/PractitionerShared/DeleteSupplierPractitioner', 'json', { supplierPractitionerID: item.SupplierPractitionerID() });
        ajax.done(function (resp) {
            self.SupplierPractitioners.remove(function (e) {
                return e.SupplierPractitionerID == item.SupplierPractitionerID;
            });
        });
    };
    self.CloseClick = function (item) {
        ko.RevertChanges(item);
    };
}