function TreatmentCategoryModel(id, name, treatmentID, pricing) {
    var self = this;
    //var initialPrice = pricing.slice(0);
    self.TreatmentCategoryID = ko.observable(id);
    self.TreatmentCategoryName = ko.observable(name);
    self.TreatmentCategoryPricings = ko.mapping.fromJS([]);
    self.TreatmentID = ko.observable(treatmentID);

    var dataMappingOptions = {
        create: function (options) {

            return new TreatmentCategoryPricing(options.data);
        }
    };
    ko.mapping.fromJS(pricing, dataMappingOptions, self.TreatmentCategoryPricings);

    self.IsPhysicalAndPsychological = ko.computed(function () {
        return self.TreatmentCategoryID() == 1 || self.TreatmentCategoryID() == 2;
    });

    self.FilterTreatmentCategoryPricing = function (treatmentCategoryID, isTriage) {
        return ko.computed(function () {
            return ko.utils.arrayFilter(self.TreatmentCategoryPricings(), function (p) {
                if (isTriage) {
                    return true;
                }
                if (!isTriage) {
                    return p.PricingTypeID() != 15;
                }
                //                if (treatmentCategoryID != 1 || treatmentCategoryID != 2) {
                //                    return true;
                //                }
            });
        }, self)();
    };

    self.UpdateTreatmentCategoryFormBeforeSubmit = function (arr, $form, options) {
        if ($form.jqBootstrapValidation('hasErrors')) return false;
        ko.utils.arrayForEach(self.TreatmentCategoryPricings(), function (item) {
            item.Price.commit();
        });
        return true;
    };

    self.CloseClick = function (item) {

        ko.utils.arrayForEach(self.TreatmentCategoryPricings(), function (item) {
            item.Price.reset();
        });
    };
}

function TreatmentCategoryPricing(data) {
    var self = this;
    self.PricingID = ko.observable(data.PricingID);
    self.PricingTypeID = ko.observable(data.PricingTypeID);
    self.Price = ko.revertableObservable(data.Price);
    self.TreatmentID = ko.observable(data.TreatmentID);
    self.PricingTypeName = ko.observable(data.PricingTypeName);
}
