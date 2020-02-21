/// <reference path="../../../../knockout-2.0.0.js" />


function DelegateAuthorisationViewModel() {

    var self = this;
    self.Delegates = ko.observableArray([]);

    self.selectedDelegate = ko.observable();
  

    self.Init = function (model, DelegateAutorisationTypesModel) {
        if (model != null) {
            ko.mapping.fromJS(model, {}, self.Delegates);
        }

        $.each(self.Delegates(), function (index, value) {
            if (value.Enabled() == true) {
                self.selectedDelegate(value.DelegatedAuthorisationTypeID());
            }
        });
    };

    self.UpdateDelegateAuthorisationFormSuccess = function () {
        alert("Updated Sucessfully");
    };

    self.UpdateDelegateAuthorisationFormBeforeSubmit = function (arr, $form, options) {
        if ($form.jqBootstrapValidation('hasErrors')) {
            return false;
        }
    };
};