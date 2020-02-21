function InvoiceMethodViewModel() {
    var self = this;

    self.ReferrerInvoiceMethods = ko.observableArray([]);
   self.ReferrerProjectTreatmentID = ko.observable();
    self.ReferrerProjectTreatmentInvoiceID = ko.observable();
    self.InvoiceMethodID = ko.observable();
    self.ManagementFeeEnabled = ko.observable();

    this.Init = function (modelValue) {
       
        if (modelValue != null) {
            ko.mapping.fromJS(modelValue, {}, self);
        }
    };

    self.UpdateReferrerInvoiceMethodFormBeforeSubmit = function (arr, $form, options) {
        if ($form.jqBootstrapValidation('hasErrors')) return false;
        return true;
    };
    var mapping = {
        'ignore': ["ReferrerInvoiceMethods"]
    };
    self.UpdateReferrerInvoiceMethodSuccess = function (responseText, statusText, xhr, $form) {
        ko.mapping.fromJS($.parseJSON(responseText), mapping, self);
        alert("Invoice Methods successfully updated");
    };


};
