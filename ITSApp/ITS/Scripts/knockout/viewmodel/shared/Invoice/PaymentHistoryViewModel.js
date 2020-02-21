function PaymentHistoryViewModel() {
    var self = this;
    self.InvoicePaymentHistory = ko.observableArray();

    mappingOptions = {
        'InvoicePaymentDate': {
            create: function (options) {
                if (options.data != null) {
                    return moment(options.data).format("DD/MM/YYYY");
                }
            }
        }
    };
    self.Init = function (model) {
        ko.mapping.fromJS(model, mappingOptions, self.InvoicePaymentHistory);

    };

    self.UpdatePaymentHistoryGrid = function (responseText) {

        var response = $.parseJSON(responseText);
        response.InvoicePaymentDate = moment(response.InvoicePaymentDate).format("DD/MM/YYYY");
        self.InvoicePaymentHistory.push(response);

    };
};