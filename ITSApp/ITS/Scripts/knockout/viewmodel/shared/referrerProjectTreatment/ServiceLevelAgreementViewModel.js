function ServiceLevelAgreementViewModel() {
    var self = this;
    self.ServiceLevelAgreementSLA = ko.observableArray();

    self.Init = function (model) {
        if (model != null) {
            ko.mapping.fromJS(model, {}, self.ServiceLevelAgreementSLA);
        }
    };

    self.UpdateProjectTreatmentSLAFormSuccess = function () {
        alert("Updated Sucessfully");
    };

    self.UpdateProjectTreatmentSLAFormBeforeSubmit = function (arr, $form, options) {
        if ($form.jqBootstrapValidation('hasErrors')) return false;
        return true;
    };
};