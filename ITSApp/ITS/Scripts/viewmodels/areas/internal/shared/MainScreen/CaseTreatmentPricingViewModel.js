    function TreatmentPriceRow(pricingID, price, pricingTypeName, ownerViewModel) {
        this.PricingID = ko.observable(pricingID);
        this.Price = ko.observable(price);
        this.PricingTypeName = ko.observable(pricingTypeName);
        this.remove = function() {
            ownerViewModel.TreatmentPriceRows.remove(this);
        };
    }

    function TreatmentPricingViewModel() {
        var self = this;
        self.TreatmentPriceRows = ko.observableArray([]);
        self.SelectedReferrerTreatmentPricing = ko.observable();
        //self.ReferrerTreatmentPricings = ko.observableArray([new ReferrerTreatmentPricing(1, 1, 50, 1, 'treatment type 1', 1), new ReferrerTreatmentPricing(2, 2, 100, 2, 'treatment type 2', 2)]);
        self.ReferrerTreatmentPricings = ko.observableArray([]);
        self.AddNewTreatment = function() {
            self.TreatmentPriceRows.push(new TreatmentPriceRow(self.SelectedReferrerTreatmentPricing().PricingID, self.SelectedReferrerTreatmentPricing().Price, self.SelectedReferrerTreatmentPricing().PricingTypeName, self));
        };

    }

    function ReferrerTreatmentPricing(pricingID, pricingTypeID, price, referrerProjectTreatmentID, pricingTypeName, referrerProjectID, ownerViewModel)
    {
        this.PricingID = pricingID;
        this.PricingTypeID = pricingTypeID;
        this.Price = price;
        this.ReferrerProjectTreatmentID = referrerProjectTreatmentID;
        this.PricingTypeName = pricingTypeName;
        this.ReferrerProjectID = referrerProjectID;
    }
    