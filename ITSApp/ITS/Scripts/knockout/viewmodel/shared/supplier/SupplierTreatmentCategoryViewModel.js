function SupplierTreatmentCategoryViewModel() {
    var self = this;
    self.IsTriage = ko.observable();
    self.TreatmentCategories = ko.observableArray([]);
    self.SelectedTreatmentCategory = ko.observable();
    self.viewTreatmentCategory = function (item) {
        self.SelectedTreatmentCategory(item);
    };

    self.AddTreatmentCategory = function (treatmentCategoryID, treatmentCategoryName) {
        self.TreatmentCategories.push(new TreatmentCategoryModel(treatmentCategoryID, treatmentCategoryName));
    };

    self.Init = function (model) {
        if (model != null) {
            $.each(model, function (index, value) {
                self.TreatmentCategories.push(new TreatmentCategoryModel(value.TreatmentCategoryID, value.TreatmentCategoryName, value.SupplierTreatmentID, value.Pricing));
            });
        }
    };

    self.AddTreatmentCategoryAndPricing = function (model) {
        self.TreatmentCategories.push(new TreatmentCategoryModel(model.TreatmentCategoryID, model.TreatmentCategoryName, model.SupplierTreatmentID, model.Pricing));
    }

    self.InitSupplierTreatmentCategoryModel = function (model, IsTriage) {
        self.IsTriage(IsTriage);
        self.Init(model);
    };
}
