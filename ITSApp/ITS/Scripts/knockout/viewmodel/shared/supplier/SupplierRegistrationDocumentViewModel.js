function SupplierDocumentTypesModel(data) {
    var self = this;
    self.SupplierDocumentID = ko.observable();
    self.DocumentTypeID = ko.observable();
    self.SupplierID = ko.observable();
    self.UserID = ko.observable();
    self.UploadDate = ko.observable().extend({ parseJsonDate: null });
    self.DocumentName = ko.revertableObservable();
    self.UserName = ko.observable();
    self.UploadPath = ko.observable();
    self.DocumentUrl = ko.observable();
    ko.mapping.fromJS(data, {}, self);

    ko.CommitChanges(self);

    self.DocumentURLUnescaped = ko.computed(function () {
        return self.DocumentUrl() == null ? self.DocumentUrl() : self.DocumentUrl().replace(/&amp;/g, '&');
    });

};

function SupplierRegistrationDocumentGridViewModel() {
    var self = this;
    self.SupplierDocumentTypes = ko.observableArray([]);
    self.SelectedRegistrationDocument = ko.observable();
   
    self.DisableUpdateButton = ko.observable(false);

    self.UpdateRegistrationFormBeforeSubmit = function (arr, $form, options) {
        
        if ($form.jqBootstrapValidation('hasErrors')) return false;
        self.DisableUpdateButton(true);
        return true;
    }

    self.viewRegistrationDocument = function (item) {
        self.SelectedRegistrationDocument(item);
    };

    self.DeSelectCurrentSelectedSupplierRegistrationDocument = function () {
        self.SelectedRegistrationDocument(null);
        $('#divViewRegistrationDocument').modal('hide');
    };

    self.CloseClick = function (item) {
        ko.RevertChanges(item);
        self.DeSelectCurrentSelectedSupplierRegistrationDocument();
    };

    self.initializeSupplierRegistrationDocument = function (supplierRegistrations) {
        if (supplierRegistrations != null) {
            $.each(supplierRegistrations, function (index, value) {
                self.SupplierDocumentTypes.push(new SupplierDocumentTypesModel(value));
            });
        };
    };

    self.AddRegistrationDocument = function (responseText, statusText, $form) {
        self.SupplierDocumentTypes.push(new SupplierDocumentTypesModel($.parseJSON(responseText)));
        alert("Added successfully");
    };

    self.UpdateSelectedRegistrationDocument = function (item) {
        ko.mapping.fromJS(item, {}, self.SelectedRegistrationDocument);
        ko.CommitChanges(self.SelectedRegistrationDocument());
        self.DeSelectCurrentSelectedSupplierRegistrationDocument();

        self.DisableUpdateButton(false);
        alert("Updated successfully");
    };

    self.DeleteRegistrationDocument = function (selectedItem) {
        var confermation = confirm("Are you Sure to Delete");
        if (confermation) {
            $.ajax({
                url: "/SupplierShared/DeleteRegistrationDocument/",
                cache: false,
                type: "POST", dataType: "json",
                contentType: 'application/json',
                data: ko.toJSON(this),
                success: function (data) {
                    self.SupplierDocumentTypes.remove(function (item) { return item.SupplierDocumentID == selectedItem.SupplierDocumentID })
                    alert(data);
                },
                error: function (data) {
                    alert("Error while deleting a new Document - " + data);
                }
            });
        }
    };
};