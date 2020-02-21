function TreatmentPricingViewModel(isTriage, treatmentCategoryID, treatmentPricing, referrerProjectID) {
    var self = this;

    self.TreatmentPricing = ko.observableArray([]);
    ko.mapping.fromJS(treatmentPricing, {}, self.TreatmentPricing);
    if ((treatmentCategoryID == 1 || treatmentCategoryID == 2) && !isTriage) {
        self.TreatmentPricing.remove(function (item) { return item.PricingTypeID() == 15 })
    };

    this.UpdateTreatmentCategoryPricingFormBeforeSubmit = function (arr, $form, options) {
        if ($form.jqBootstrapValidation('hasErrors')) return false;
        return true;
    };

    this.UpdateTreatmentCategoryPricingFormSuccess = function (responseText, statusText) {
        var ajax = AjaxUtil.post('/ReferrerProjectShared/UpdateReferrerProjectStatus', 'json', { referrerProjectID: referrerProjectID, isTriage: isTriage });
        ajax.done(function (resp) {
            alert("Tratment Category pricing successfully updated");
        });
    }
}; 