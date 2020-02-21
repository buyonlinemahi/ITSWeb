function FinanceViewModel() {
    var self = this;
    self.AccountReceivableChasingPoint = ko.observable();
    self.AccountReceivableCollection = ko.observable();
    self.ReferrerProjectTreatmentID = ko.observable();
    self.ReferrerProjectID = ko.observable();
    self.TreatmentCategoryID = ko.observable();
    self.Enabled = ko.observable();

    this.Init = function (model) {
        if (model != null) {
            ko.mapping.fromJS(model, {}, self);
        }
    };

    this.UpdateProjectTreatmentFormBeforeSubmit = function (arr, $form, options) {
        if ($form.jqBootstrapValidation('hasErrors')) return false;
        return true;
    };

    this.UpdateProjectTreatmentFinanceFormSuccess = function (responseText) {
        alert("Finance successfully updated");
    };

};