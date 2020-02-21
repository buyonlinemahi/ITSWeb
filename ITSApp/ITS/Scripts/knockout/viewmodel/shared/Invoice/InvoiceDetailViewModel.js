function InvoiceDetailViewModel() {
    var self = this;
    self.CaseInvoicePatientReferrer = ko.observable();
    self.OutstandingAmount = ko.observable();

    self.Init = function (model) {

        if (model != null)
            ko.mapping.fromJS(model, {}, self);
        if (model.CaseInvoicePatientReferrer != null)
            ko.mapping.fromJS(model.CaseInvoicePatientReferrer.OutstandingAmount, {}, self.OutstandingAmount);

    };
    self.OutstandingAmount(parseFloat(Math.round(self.OutstandingAmount * 100) / 100).toFixed(2));



};