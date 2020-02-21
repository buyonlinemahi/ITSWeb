function AddInsuredViewModel(supplierID) {
    var self = this;
    self.SupplierID = ko.observable(supplierID);

    self.DisableAddButton = ko.observable(false);

    this.ResetDisabledAddButton = function () {
        self.DisableAddButton(false);
    };

    self.InsuredFormBeforeSubmit = function (arr, $form, options) {
        if ($form.jqBootstrapValidation('hasErrors')) {
            return false;
        }
        else {
            var extention = $("#InsuredDocumentFile").val().split('.').pop().toLowerCase();

            if (extention == "jpg" || extention == "jpeg" || extention == "doc" || extention == "docx" || extention == "tiff" || extention == 'pdf' || extention == 'tif')
             {
                self.DisableAddButton(true);
                return true;
             
            }
            else
             {
                  alert("Please select pdf,doc,docx,jpg,jpeg,tif or tiff files only");
                return false;
               
            }
        }
    }

};