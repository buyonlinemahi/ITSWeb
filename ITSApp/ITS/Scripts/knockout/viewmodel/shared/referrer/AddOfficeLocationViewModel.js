function AddOfficeLocationViewModel(referrerID) {

    var self = this;
    self.ReferrerID = ko.observable(referrerID);

    self.OfficeLocationFormBeforeSubmit = function (arr, $form, options) {
        if ($form.jqBootstrapValidation('hasErrors'))
            return false;

    }

};