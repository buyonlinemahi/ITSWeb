function CaseTreatmentPricingModel(data, pricingTypeName, caseid) {
    var self = this;
    self.PricingTypeName = ko.observable(pricingTypeName);
    self.CaseID = ko.observable(caseid);
    self.CaseTreatmentPricingID = ko.observable();
    self.DateOfService = ko.observable();
    self.IsComplete = ko.observable();
    self.PatientDidNotAttend = ko.observable();
    self.ReferrerPrice = ko.observable();
    self.ReferrerPricingID = ko.observable();
    self.SupplierPrice = ko.observable();
    self.SupplierPriceID = ko.observable();
    self.WasAbandoned = ko.observable();
    ko.mapping.fromJS(data, {}, self);
};

function CaseBespokeServicePricingModel(data, bespokeServiceName, referrerPrice, supplierPrice, caseid) {
    var self = this;
    self.CaseBespokeServiceID = ko.observable();
    self.TreatmentCategoryName = ko.observable();
    self.BespokeServiceName = ko.observable(bespokeServiceName);
    self.TreatmentCategoryBespokeServiceID = ko.observable();
    self.TreatmentCategoryID = ko.observable();
    self.BespokeServiceID = ko.observable();
    self.ReferrerPrice = ko.observable(referrerPrice);
    self.SupplierPrice = ko.observable(supplierPrice);
    self.CaseID = ko.observable(caseid);
    ko.mapping.fromJS(data, {}, self);

}

function AuthorityAcceptanceScreenViewModel() {
    var self = this;
    self.CaseTreatmentPricing = ko.observableArray();
    self.CaseBespokeServicePricing = ko.observableArray();
    this.Init = function (model) {
        ko.mapping.fromJS(model, {}, self);


        $.each(model.CaseTreatmentPricings, function (index, value) {
            var matchedRow = ko.utils.arrayFirst(model.TreatmentReferrerSupplierPricing, function (item) {
                return (value.ReferrerPricingID === item.ReferrerPricingID);
            });
            self.CaseTreatmentPricing.push(new CaseTreatmentPricingModel(value, matchedRow.PricingTypeName));
        });

        $.each(model.CaseBespokeServicePricings, function (index, value) {
            var matchedRow = ko.utils.arrayFirst(model.TreatmentCategoriesBespokeServices, function (item) {
                return (value.TreatmentCategoryBespokeServiceID === item.TreatmentCategoryBespokeServiceID);
            });
            self.CaseBespokeServicePricing.push(new CaseBespokeServicePricingModel(value, matchedRow.BespokeServiceName));
        });
    };
    this.UpdateAuthorityAcceptanceScreen = function (responseText, statusText, xhr, $form) {
        alert(responseText);
        window.location = "/Supplier/Authorisation";
    };
    ko.bindingHandlers.ajaxForm = {
        init: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
            var value = ko.utils.unwrapObservable(valueAccessor());
            $(element).ajaxForm(value);
        }
    };

}