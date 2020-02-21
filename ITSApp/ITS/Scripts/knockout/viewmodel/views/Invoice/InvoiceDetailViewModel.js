function InvoiceDetailViewModel() {
    var self = this;

    self.Init = function (data) {

        if (data != null) {
            ko.mapping.fromJS(data, {}, self);
        }

    };
};