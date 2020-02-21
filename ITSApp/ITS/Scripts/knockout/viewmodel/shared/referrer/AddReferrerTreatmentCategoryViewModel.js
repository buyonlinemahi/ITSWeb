
function AddReferrerTreatmentCategoryViewModel(referrerTreamentCategories, referrerID) {
    var self = this;
    self.ReferrerID = ko.observable(referrerID);
    self.ReferrerProjectID = ko.observable();
    self.TreatmentCategories = ko.observableArray(referrerTreamentCategories);

    self.TreatmentCategoriesPricingTypes = ko.observableArray();
    self.TreatmentCategoryID = ko.observable();
    self.SelectedTreatmentCategory = ko.observable();
    self.IsTriage = ko.observable();

    self.SelectedTreatmentCategory.subscribe(function (newVal) {
        if (newVal !== undefined) {
            self.SetTreatmentCategoriesPricingTypesByTreatmentCategoryID(newVal.TreatmentCategoryID);
        } else {
            self.TreatmentCategoriesPricingTypes([]);
        }
    });

    self.Init = function (referrerProjectID, isTriage) {
        self.IsTriage(isTriage);
        self.ReferrerProjectID(referrerProjectID);
        self.SelectedTreatmentCategory(undefined);
    };


    self.FilteredTreatmentCategoriesPricingTypes = function (treatmentCategoryID) {
        return ko.computed(function () {
            return ko.utils.arrayFilter(self.TreatmentCategoriesPricingTypes(), function (p) {
                if (self.IsTriage()) {
                    return true;
                }
                if (!self.IsTriage()) {

                    return p.PricingTypeID != 15;
                }
                //                if ((treatmentCategoryID == 1 || treatmentCategoryID == 2) && self.IsTriage()) {
                //                    return true;
                //                }
                //                if ((treatmentCategoryID == 1 || treatmentCategoryID == 2) && !self.IsTriage()) {
                //                    return p.PricingTypeID != 15;
                //                }
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
        var request = AjaxUtil.post('/ReferrerShared/GetTreatmentCategoriesPricingTypesByTreatmentCategoryID', "json", { treatmentCategoryID: treatmentCategoryID }, undefined, false);
        request.done(function (data) {
            self.TreatmentCategoriesPricingTypes(data);
        });
    }
};