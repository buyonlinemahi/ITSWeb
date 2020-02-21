/*
*  Latest Version  : 1.0

*  Modified by  : Harpreet Singh
*  Version      : 1.0
*  Date         : 24-Dec-2012
*  Description  : Create viewModel to implement supplier setup pricing

*/

function Pricingmatrix(pricingID, pricingTypeID, price, supplierID, pricingTypeName, supplierTreatmentID, tretmentid, isEnabled) {
    var self = this;
    self.PricingID = ko.observable(pricingID);
    self.PricingTypeID = ko.observable(pricingTypeID);
    self.Price = ko.observable(price);
    self.SupplierID = ko.observable(supplierID);
    self.PricingTypeName = ko.observable(pricingTypeName);
    self.TreatmentCategoryID = ko.observable(tretmentid);
    self.SupplierTreatmentID = ko.observable(supplierTreatmentID);
    self.IsEnabled = ko.observable(isEnabled);
    self.MakeEnableOrDisable = ko.observable(false);
};
function PricingControlViewModel() {
    var self = this;
    self.pricingEnable = ko.observable(false);
    self.SupplierID = ko.observable();
    self.SupplierTreatmentID = ko.observable();
    self.Save = ko.observable(true);
    self.Ammend = ko.observable(false);
    self.Update = ko.observable(false);
    self.SupplierPricingMatrix = ko.observableArray([]);
    self.UpdateVisible = ko.observable(false);
    self.SaveVisible = ko.observable(true);
    self.IsEnabled = ko.observable(false);


    this.EnablePricing = function (isEnabled, treatmentId) {
        var selectedTreatmentID = treatmentId;
        ko.utils.arrayForEach(self.SupplierPricingMatrix(), function (Treatmentscatagories) {
            if (Treatmentscatagories.TreatmentCategoryID() == selectedTreatmentID) {
                Treatmentscatagories.IsEnabled(isEnabled);
                $("#tabs-inner").tabs("select", 0);
            }
        });

    };
    this.LoadPricingMatrix = function (treatmentCategoriesCount) {
        self.SupplierPricingMatrix([]);
        $('#divPricingSpinner').spin(true);
        $.ajax({
            url: "/SupplierSetUp/GetAllPricingTypes/",
            type: "POST", dataType: "json",
            data: { count: treatmentCategoriesCount },
            cache: false,
            success: function (model) {

                $.each(model, function (index, value) {
                    self.SupplierPricingMatrix.push(new Pricingmatrix(value.PricingID, value.PricingTypeID,
                     value.Price, self.SupplierID, value.PricingTypeName, value.SupplierTreatmentID, value.TreatmentCategoryID, value.IsEnabled));
                });
                $('#divPricingSpinner').spin(false);

            },
            error: function (result) {
                alert("error" + result.toString());
                $('#divPricingSpinner').spin(false);
            }
        });
    };
    this.InitializePricingTab = function (model) {
        self.SupplierID(model.SupplierID);
        self.Ammend(true);
    };


    this.EnableDisablePricingMatrix = function (enableDisable) {
        ko.computed(function () {
            ko.utils.arrayForEach(self.SupplierPricingMatrix(), function (item) {
                if (item.IsEnabled() == true)
                    item.MakeEnableOrDisable(enableDisable);
            });

        });
    }
    this.AmmendPricing = function () {
        self.Update(true);
        self.Ammend(false);
        self.EnableDisablePricingMatrix(true);
        self.IsEnabled(true);
    }
    this.GetAllPricingTypes = function (newValues) {
        self.SupplierPricingMatrix([]);
        $('#divPricingSpinner').spin(true);
        $.ajax({
            url: "/SupplierSetUp/GetPricingTypesBySupplierTreatmentCategoryID/",
            type: 'POST',
            data: ko.toJSON(newValues),
            contentType: 'application/json',
            dataType: 'json',
            cache: false,
            success: function (model) {
                $.each(model, function (index, value) {
                    self.SupplierPricingMatrix.push(new Pricingmatrix(value.PricingID, value.PricingTypeID,
                     value.Price, self.SupplierID, value.PricingTypeName, value.SupplierTreatmentID, value.TreatmentCategoryID, value.IsEnabled));
                });
                self.EnableDisablePricingMatrix(false);
                $('#divPricingSpinner').spin(false);
            },
            error: function (result) {
                alert("error" + result.toString());
                $('#divPricingSpinner').spin(false);
            }
        });
    };

    this.PricingStatus = ko.computed(function () {
        var count = 0;
        var Status = "Completed";
        var Actions = { Status: "Status" }
        if (self.SupplierPricingMatrix().length > 0) {
            ko.utils.arrayForEach(self.SupplierPricingMatrix(), function (item) {
                if (item.IsEnabled() == true && item.Price() == 0)
                    Status = "InCompleted";
            });
            InitilizedPricingTab(Actions.Status, Status);

        }
    });

    this.AddSupplierPricing = function (model) {
        $.each(model, function (index, value) {
            ko.utils.arrayForEach(self.SupplierPricingMatrix(), function (treatmentCatagories) {
                if (value.TreatmentCategoryID == treatmentCatagories.TreatmentCategoryID()) {
                    treatmentCatagories.SupplierID = value.SupplierID;
                    self.SupplierID(value.SupplierID);
                    treatmentCatagories.SupplierTreatmentID = value.SupplierTreatmentID;
                }
            });
        });

        $.ajax({
            url: "/SupplierSetup/AddSupplierPricing/",
            type: 'post',
            data: ko.toJSON(self.SupplierPricingMatrix()),
            contentType: 'application/json',
            success: function (result) {
                self.GetAllPricingTypes(result);
                self.Ammend(true);
            }
        });
    };

    this.UpdateSupplierPricing = function () {
        ko.utils.arrayForEach(self.SupplierPricingMatrix(), function (treatmentCatagories) {
            treatmentCatagories.SupplierID = self.SupplierID();
            self.Update(false);
            self.Ammend(true);
            self.IsEnabled(false);

        });

        $.ajax({
            url: "/SupplierSetup/UpdateSupplierTreatmentPricing/",
            type: 'post',
            data: ko.toJSON(self.SupplierPricingMatrix()),
            contentType: 'application/json',
            success: function (result) {
                self.SupplierPricingMatrix([]);
                self.GetAllPricingTypes(result);
                self.EnableDisablePricingMatrix(false);
                $("#pricingAlertSupplier").show();
                callback();
//                $("#lblSupplierpricing").text("Supplier pricing successfully Updated");
//                $("#lblSupplierpricing").addClass("message");
//                setTimeout(function () {
//                    $("#lblSupplierpricing").slideUp("slow");
//                }, 5000);

            }
        });
    };

    function callback() {
        setTimeout(function () { $("#pricingAlertSupplier").hide(); }, 5000);
    };
};