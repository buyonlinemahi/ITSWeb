function AddCollectionActionViewModel(id) {
    var self = this;
    self.InvoiceID = ko.observable(id);


    self.AddCollectionActionFormBeforeSubmit = function (arr, $form, options) {
        if ($form.jqBootstrapValidation('hasErrors')) return false;
        return true;
    };

};