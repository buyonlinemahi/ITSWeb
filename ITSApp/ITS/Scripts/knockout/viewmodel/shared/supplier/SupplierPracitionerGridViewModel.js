


function SupplierPracitioner(supplierPractitionerID,
                            practitionerFirstName,
                            treatmentCategoryName,
                            registrationNumber,
                            practitionerLastName,
                            practitionerID,
                            registrationTypeName,
                            isActive,
                            practitionerRegistrationID
                                ) {

    var self = this;

    self.SupplierPractitionerID = ko.observable(supplierPractitionerID);
    self.PractitionerFirstName = ko.observable(practitionerFirstName);
    self.TreatmentCategoryName = ko.observable(treatmentCategoryName);
    self.RegistrationNumber = ko.observable(registrationNumber);
    self.PractitionerLastName = ko.observable(practitionerLastName);
    self.PractitionerID = ko.observable(practitionerID);
    self.RegistrationTypeName = ko.observable(registrationTypeName);
    self.IsActive = ko.observable(isActive);
    self.PractitionerRegistrationID = ko.observable(practitionerRegistrationID);

    self.PracitionerName = ko.computed(function () {
        return self.PractitionerFirstName() + ' ' + self.PractitionerLastName();
    });
};




function SupplierPracitionerGridViewModel() {
    var self = this;

    self.SupplierPracitioners = ko.observableArray([]);
    self.SupplierPractitionerID = ko.observable();

    this.loadPracitionerGrid = function (item) {
        $.each(item, function (index, value) {
            self.SupplierPracitioners.push(new SupplierPracitioner(
                                                value.SupplierPractitionerID,
                                                value.PractitionerFirstName,
                                                value.TreatmentCategoryName,
                                                value.RegistrationNumber,
                                                value.PractitionerLastName,
                                                value.PractitionerID,
                                                value.RegistrationTypeName,
                                                value.IsActive,
                                                value.PractitionerRegistrationID
            ));
        })
    };

    this.DetachSupplierPractitioner = function (itemToDelete) {
        var confirm = window.confirm("Are you sure to delete this item");
        if (confirm) {
            $.ajax({
                url: "/SupplierShared/DeleteSupplierPractitioner",
                cache: false,
                type: "POST", dataType: 'json',
                contentType: 'application/json',
                data: ko.toJSON(this),
                success: function (data) {
                    self.SupplierPracitioners.remove(function (item) { return item.SupplierPractitionerID == itemToDelete.SupplierPractitionerID })
                    alert(data);
                },
                error: function (data) {
                    alert("Error while deleting attached Supplier Practitioner - " + data);
                }
            });
        }
    };

};