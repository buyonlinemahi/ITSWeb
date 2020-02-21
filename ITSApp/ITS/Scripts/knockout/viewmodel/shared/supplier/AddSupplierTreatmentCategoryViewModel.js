function AddSupplierTreatmentCategoryViewModel(treatmentCategories, supplierID, isTriage) {
    var self = this;
    self.TreatmentCategories = ko.observableArray(treatmentCategories);
    self.TreatmentCategoriesPricingTypes = ko.observableArray();
    self.SupplierID = ko.observable(supplierID);
    self.TreatmentCategoryID = ko.observable();
    self.SelectedTreatmentCategory = ko.observable();
    self.IsTriage = ko.observable(isTriage);
    self.SelectedTreatmentCategory.subscribe(function (newVal) {
        if (newVal !== undefined) {
            self.SetTreatmentCategoriesPricingTypesByTreatmentCategoryID(newVal.TreatmentCategoryID);
        } else {
            self.TreatmentCategoriesPricingTypes([]);
        }
    });

    self.FilteredTreatmentCategoriesPricingTypes = function (treatmentCategoryID) {
        return ko.computed(function () {
            return ko.utils.arrayFilter(self.TreatmentCategoriesPricingTypes(), function (p) {
                if (self.IsTriage()) {
                    return true;
                }
                if (!self.IsTriage()) {

                    return p.PricingTypeID != 15;
                }
                //                if (treatmentCategoryID != 1 || treatmentCategoryID != 2) {
                //                    return true;
                //                }
            });
        }, self)();
    };

    self.AddTreatmentCategoryFormBeforeSubmit = function (arr, $form, options) {
        if ($form.jqBootstrapValidation('hasErrors')) return false;
        return true;
    };

    self.SetTreatmentCategoriesPricingTypesByTreatmentCategoryID = function (treatmentCategoryID) {
        var request = AjaxUtil.post('/SupplierShared/GetTreatmentCategoriesPricingTypesByTreatmentCategoryID', "json", { treatmentCategoryID: treatmentCategoryID }, undefined, false);
        request.done(function (data) {
            self.TreatmentCategoriesPricingTypes(data);
        });
    }
}