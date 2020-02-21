function InternalTaskViewModel(model) {
    var self = this;
    self.caseWorkflowCount = ko.observableArray([]);
    self.TreatmentCategoryID = ko.observable();
    ko.mapping.fromJS(model, {}, self);

    this.SelectedTreatment = function (item) {
        $("#frmReferralsCount").submit();
    };

    this.UpdateGrid = function (responseText) {
        ko.mapping.fromJS($.parseJSON(responseText), {}, self);
    };
};