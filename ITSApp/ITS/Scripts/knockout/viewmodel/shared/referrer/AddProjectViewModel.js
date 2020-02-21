
function AddReferrerProjectViewModel(referrerID) {
    var self = this;
    self.ReferrerID = ko.observable(referrerID);

    //By Default StatusID is Incomplete 
    self.StatusID = ko.observable(2);

    self.AddReferrerProjectFormBeforeSubmit = function (arr, $form, options) {
        if ($form.jqBootstrapValidation('hasErrors')) return false;
        return true;
    }
};