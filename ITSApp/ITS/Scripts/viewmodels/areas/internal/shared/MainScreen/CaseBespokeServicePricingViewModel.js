function CaseBespokeServicePricingRow(pricingID, referrerPrice, supplierPrice, pricingTypeName, ownerViewModel) {

    this.PricingID = ko.observable(pricingID);
    this.ReferrerPrice = ko.observable(referrerPrice);
    this.SupplierPrice = ko.observable(supplierPrice);
    this.PricingTypeName = ko.observable(pricingTypeName);
    this.remove = function () {
        ownerViewModel.CaseBespokeServicePricingRows.remove(this);
    };
}

function CaseBespokeServicePricingViewModel() {

    var self = this;
    self.CaseBespokeServicePricingRows = ko.observableArray([]);
    self.SelectedBespokeServicePricing = ko.observable();
    self.CaseBespokeServicePricings = ko.observableArray([]);
    self.AddNewBespokeService = function () {
        self.CaseBespokeServicePricingRows.push(new CaseBespokeServicePricingRow(self.SelectedReferrerTreatmentPricing().PricingID, self.SelectedReferrerTreatmentPricing().ReferrerPrice, self.SelectedReferrerTreatmentPricing().SupplierPrice, self.SelectedReferrerTreatmentPricing().PricingTypeName, self));
    };

}

function CaseBespokeServicePricing(pricingID, pricingTypeID, referrerPrice,supplierPrice, referrerProjectTreatmentID, pricingTypeName, referrerProjectID, ownerViewModel)
    {
        this.PricingID = pricingID;
        this.PricingTypeID = pricingTypeID;
        this.ReferrerPrice = referrerPrice;
        this.SupplierPrice = supplierPrice;
        this.ReferrerProjectTreatmentID = referrerProjectTreatmentID;
        this.PricingTypeName = pricingTypeName;
        this.ReferrerProjectID = referrerProjectID;
      // this.SupplierTreatmentID = referrerProjectID;

    }
    
    
