function StoppedCaseScreenViewModel() {
    var self = this;
    self.AuthorisationDetail = ko.observable();
    self.CaseID = ko.observable();

    self.init = function (model) {
        ko.mapping.fromJS(model, {}, self);
    };
    self.UpdateCaseStopped = function (responseText) {
        alert(responseText);
        window.location = '/Supplier/StoppedCases';
    };


    ko.bindingHandlers.ajaxForm = {
        init: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
            var value = ko.utils.unwrapObservable(valueAccessor());
            $(element).ajaxForm(value);
        }
    };

}