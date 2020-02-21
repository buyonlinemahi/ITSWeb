

function CaseBespokeTreatmentPricingGridViewModel() {
    var self = this;
    self.BespokeTreatmentPricings = ko.observableArray([]);


    self.AccountPayable = ko.observable(0);
    self.AccountReceivable = ko.observable(0);


    self.Init = function (data) {

        if (data.length > 0) {
            ko.mapping.fromJS(data, mappingOptions, self.BespokeTreatmentPricings);
        }


    };

    mappingOptions = {
        'DateOfService': {
            create: function (options) {
                if (options.data != null) {
                    return moment(options.data).format("DD/MM/YYYY");
                }
            }
        },

        'ReferrerPrice': {
            create: function (options) {
                if (options.data != null) {

                    self.AccountReceivable(parseFloat(self.AccountReceivable()) + parseFloat(options.data));
                    return options.data;
                }
            }
        },
        'SupplierPrice': {
            create: function (options) {
                if (options.data != null) {

                    self.AccountPayable(parseFloat(self.AccountPayable()) + parseFloat(options.data));
                    return options.data;
                }
            }
        }
    };
};