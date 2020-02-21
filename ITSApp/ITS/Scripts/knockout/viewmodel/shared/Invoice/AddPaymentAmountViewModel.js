function AddPaymentAmountViewModel(id, outstandingAmount) {
    var self = this;
    self.InvoiceID = ko.observable(id);
    self.Payment = ko.observable(0.00).extend({ numeric: 2 });
    self.AdjustedPayment = ko.observable(0.00).extend({ numeric: 2 });
    self.OutstandingAmount = ko.observable(outstandingAmount);
    self.Init = function () {

    }

    self.AddPaymentAmountFormBeforeSubmit = function (arr, $form, options) {

        if (self.OutstandingAmount() == 0 || self.OutstandingAmount() == null || self.OutstandingAmount() == undefined) {
            alert("No Outstanding Dues Remaning");
            return false;
        }
        else if (parseFloat(self.Payment()) + parseFloat(self.AdjustedPayment()) > self.OutstandingAmount()) {

            alert("Payment cannot be greater then Outstanding Amount");
            return false;
        }
        if ($form.jqBootstrapValidation('hasErrors')) return false;
        return true;
    };

    self.AddPaymentSuccess = function (responseText) {
        alert('Payment Successfully Processed');
        
        self.OutstandingAmount((self.OutstandingAmount()) - (parseFloat(self.Payment()) + parseFloat(self.AdjustedPayment())));
       
        self.SetDefaultValue();

    };

    self.SetDefaultValue = function () {
       
        self.Payment(0.00);
        self.AdjustedPayment(0.00);
    };
};

ko.extenders.numeric = function (target, precision) {
    var result = ko.computed({
        read: target,
        write: function (newValue) {
            target(parseFloat(Math.round(newValue * 100) / 100).toFixed(2));
        }
    }).extend({ notify: 'always' });
    result(target());
    return result;
};

