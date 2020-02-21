function AddRegistrationDocumentViewModel(SupplierID) {
    var self = this;

    self.SupplierID = ko.observable(SupplierID);
    self.UserID = ko.observable();
    self.DocumentName = ko.observable();
    self.UploadPath = ko.observable();
    self.DocumentUrl = ko.observable();

    self.DisableAddButton = ko.observable(false);

    this.ResetDisabledAddButton = function () {
        self.DisableAddButton(false);
    };

    self.RegistrationDocumentFormBeforeSubmit = function (arr, $form, options) {
        if ($form.jqBootstrapValidation('hasErrors'))
            return false;
        else
            var extention = $("#RegistrationDocumentFileUpload").val().split('.').pop().toLowerCase();

        if (extention == "jpg" || extention == "jpeg" || extention == "doc" || extention == "docx" || extention == "tiff" || extention == 'pdf' || extention == 'tif') {
            self.DisableAddButton(true);
            return true;
        }
        else {
            alert("Please select pdf , doc , docx , jpg , jpeg , tif or tiff files only");
            return false;

        }
    }
}