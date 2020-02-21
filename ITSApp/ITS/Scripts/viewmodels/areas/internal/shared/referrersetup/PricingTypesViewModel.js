/*
*  Latest Version  : 1.0
*  Auther       : Robin Singh
*  Version      : 1.0
*  Date         : 08-Dec-2012
*  Description  : Added Method for Pricing/Finance tabs.
*/

function PricingTypesModel(pricingID, pricingTypeID, pricingTypeName, price, referrerProjectTreatmentID, referrerProjectID, isTriage) {
    var self = this;
    self.PricingID = ko.observable(pricingID);
    self.PricingTypeID = ko.observable(pricingTypeID);
    self.PricingTypeName = ko.observable(pricingTypeName);
    self.Price = ko.observable(price).extend({ number: { params: true, message: 'Please enter a number in price matrix'} }); ;
    self.ReferrerProjectTreatmentID = ko.observable(referrerProjectTreatmentID);
    self.ReferrerProjectID = ko.observable(referrerProjectID);
    self.errors = ko.validation.group(self);
    self.IsTriage = ko.observable(isTriage);

}

function ServiceLevelAgreements(serviceLevelAgreementName, serviceLevelAgreementID, numberOfDays, enabled, referrerProjectTreatmentID, projectTreatmentSLAID) {
    var self = this;
    self.ServiceLevelAgreementName = ko.observable(serviceLevelAgreementName);
    self.ServiceLevelAgreementID = ko.observable(serviceLevelAgreementID);
    self.NumberOfDays = ko.observable(numberOfDays).extend({ number: { params: true, message: 'Please enter a number in SLA'} });
    self.Enabled = ko.observable(enabled);
    self.ReferrerProjectTreatmentID = ko.observable(referrerProjectTreatmentID);
    self.IsOnOffNumberOfdays = ko.observable(false);
    self.ProjectTreatmentSLAID = ko.observable(projectTreatmentSLAID);
    self.Flag = ko.observable();
    self.errors = ko.validation.group(self);
    self.RadioButtonControlName = ko.computed(function () {
        return self.ServiceLevelAgreementName() + "ForPricingTab";
    });

    this.IsEnabled = ko.computed({
        read: function () {
            return self.Enabled().toString();
        },
        write: function (newValue) {
            self.Enabled(newValue === "true");
        },
        owner: this
    });
}




function ReferrerProjectTreatment(referrerProjectTreatmentID, referrerProjectID, treatmentCategoryID, accountReceivableChasingPoint, accountReceivableCollection, enabled, treatmentCategoryName) {
    var self = this;
    self.ReferrerProjectTreatmentID = ko.observable(referrerProjectTreatmentID);
    self.ReferrerProjectID = ko.observable(referrerProjectID);
    self.TreatmentCategoryID = ko.observable(treatmentCategoryID);
    self.AccountReceivableChasingPoint = ko.observable(accountReceivableChasingPoint).extend({ number: { params: true, message: 'Please enter a numeric value'} });
    self.AccountReceivableCollection = ko.observable(accountReceivableCollection).extend({ number: { params: true, message: 'Please enter a numeric value'} });
    self.Enabled = ko.observable(enabled);
    self.TreatmentCategoryName = ko.observable(treatmentCategoryName);
    self.errors = ko.validation.group(self);

}

function PricingTypesViewModel() {
    var self = this;
    self.ReferrerProjectTreatments = ko.observableArray([]);
    self.PricingTypes = ko.observableArray([]);
    self.ServiceLevelAgreementSLA = ko.observableArray();
    self.AccountReceivableChasingPoint = ko.observable(0);   //.extend({ numeric: 0 });
    self.AccountReceivableCollection = ko.observable(0);  //.extend({ numeric: 0 });
    self.TreatmentCategoryID = ko.observable();
    self.TreatmentCategoryPricingTypeID = ko.observable();
    self.TreatmentCategoryName = ko.observable();
    self.ReferrerProjectTreatmentID = ko.observable();
    self.ReferrerProjectID = ko.observable();
    self.EnableDisablePricing = ko.observable(false);
    self.EnableAmmendPricing = ko.observable(false);
    self.Flag = ko.observable(true);
    self.PricingFlag = ko.observable(false);
    self.ViewEnabled = ko.observable(false);
    self.IsTriage = ko.observable();


    this.InitializePricingTypes = function (referrerProjectTreatment, IsTriage) {

        self.IsTriage(IsTriage);
        self.ServiceLevelAgreementSLA.removeAll();
        self.PricingTypes.removeAll();
        self.ReferrerProjectTreatmentID(referrerProjectTreatment.ReferrerProjectTreatmentID);
        self.TreatmentCategoryID(referrerProjectTreatment.TreatmentCategoryID);
        self.ReferrerProjectID(referrerProjectTreatment.ReferrerProjectID);

        this.GetPricingTypes(referrerProjectTreatment.TreatmentCategoryID, referrerProjectTreatment.ReferrerProjectTreatmentID);
        this.GetTreatmentSLA();
        this.GetProjectTreatment();

    };

    this.HaveErrors = ko.computed(function () {
        var HasError = false;
        var count = 0;
        ko.utils.arrayForEach(self.PricingTypes(), function (item) {
            if (!item.isValid()) {
                count++;
            }
        });
        ko.utils.arrayForEach(self.ServiceLevelAgreementSLA(), function (item) {
            if (!item.isValid()) {
                count++;
            }
        });
        if (count > 0)
            return true;
        else
            return false;

    });
    this.ValidateMatrixs = ko.computed(function () {
        if (self.HaveErrors() == true) {
            if (self.HaveErrors() == true)

                $("#btnupdateAll").attr("disabled", "disabled");
            else
                $("#btnupdateAll").removeAttr("disabled");
        }

    });

    this.EnableDisablePricingMatrix = function (flag) {

        self.ViewEnabled(flag);

    };
    this.CheckChangesMadeInPricingTab = function () {
        if (self.PricingFlag())
            return true;
        else
            return false;
    };

    this.IsPricingTabHaveChanges = function () {
        self.PricingFlag(true);
    };

    this.GetPricingTabCompleteOrUnComplete = function () {

        var IsDataPresentInPriceMatrix = 0;
        var IsDataPresentInAccountReceivable = 0;

        ko.utils.arrayForEach(self.PricingTypes(), function (item) {
            if (item.Price() == 0)
                IsDataPresentInPriceMatrix++;
        });

        if (self.ReferrerProjectTreatments().AccountReceivableChasingPoint() == 0 || self.ReferrerProjectTreatments().AccountReceivableCollection() == 0)
            IsDataPresentInAccountReceivable++;

        if (IsDataPresentInPriceMatrix > 0 || IsDataPresentInAccountReceivable > 0)
            return true;
        else false;

    };

    this.EnablePricing = function (value) {
        self.EnableDisablePricing(value);
    };

    this.ClearModels = function () {
        self.ServiceLevelAgreementSLA.removeAll();
        self.PricingTypes.removeAll();
        self.ReferrerProjectTreatments([]);
    };

    this.AmmendPricing = function () {
        self.EnableDisablePricing(true);
    };
    this.GetPricingTypes = function (newSelectedTreatmentCategoryID, newSelectedReferrerProjectTreatmentID) {
        $.ajax({
            url: "/ReferrerPricing/GetPricingTypesByTreatmentCategoryID/",
            type: 'post',
            data: { treatmentCategoryID: newSelectedTreatmentCategoryID, referrerProjectTreatmentID: newSelectedReferrerProjectTreatmentID },
            dataType: 'json',
            cache: false,
            success: function (result) {
                self.UpdatePriceTypeModel(result);
            },
            error: function (result) {
                alert("error" + result.toString());

            }
        });
    };

    this.GetTreatmentSLA = function () {
        $.ajax({
            url: "/ReferrerPricing/GetProjectTreatmentSLAsByReferrerProjectTreatmentID/",
            type: "POST",
            data: { referrerProjectTreatmentID: self.ReferrerProjectTreatmentID() },
            success: function (model) {
                $.each(model, function (index, value) {
                    self.ServiceLevelAgreementSLA.push(new ServiceLevelAgreements(model[index].ServiceLevelAgreementName, model[index].ServiceLevelAgreementID, model[index].NumberOfDays, model[index].Enabled, self.ReferrerProjectTreatmentID, model[index].ProjectTreatmentSLAID));
                })
            },
            error: function (result) {
                alert("error" + result.toString());
            }
        });
    };

    this.UpdatePriceTypeModel = function (model) {
        $.each(model, function (index, value) {
            if (model[index].ReferrerProjectTreatmentID != 0) {
                self.PricingTypes.push(new PricingTypesModel(model[index].PricingID, model[index].PricingTypeID, model[index].PricingTypeName, model[index].Price, self.ReferrerProjectTreatmentID(), self.ReferrerProjectID(), self.IsTriage()));
            }
        });

    };
    this.GetProjectTreatment = function () {
        self.ReferrerProjectTreatments.destroyAll();
        $.ajax({
            url: "/ReferrerPricing/GetReferrerProjectTreatmentByReferrerProjectTreatmentID/",
            cache: false,
            type: "POST", dataType: "json",
            data: { referrerProjectID: self.ReferrerProjectID, treatmentCategoryID: self.TreatmentCategoryID },
            success: function (data) {

                self.ReferrerProjectTreatments(new ReferrerProjectTreatment(data.ReferrerProjectTreatmentID, data.ReferrerProjectID, data.TreatmentCategoryID, data.AccountReceivableChasingPoint, data.AccountReceivableCollection, data.Enabled, data.TreatmentCategoryName));

            },
            error: function (result) {
                alert("error" + result.toString());

            }

        });
    };


    this.SaveFinance = function () {
     
        ko.utils.arrayForEach(self.PricingTypes(), function (item) {

            if (item.PricingTypeID() == 15) {
                if (item.IsTriage() == false) {
                    item.Price(0);
                }
            }
        });

        $.ajax({
            url: "/ReferrerPricing/UpdateReferrerProjectTreatment/",
            type: "POST",
            data: ko.toJSON(self.ReferrerProjectTreatments),
            contentType: 'application/json', cache: false,
            success: function (result) {

            },
            error: function (result) {

            }
        });

        $.ajax({
            url: "/ReferrerPricing/UpdateReferrerProjectTreatmentPricingByPricingID/",
            type: 'post',
            data: ko.toJSON(self.PricingTypes()),
            cache: false,
            contentType: 'application/json; charset=utf-8',
            success: function (result) {
                self.PricingTypes([]);
                self.GetPricingTypes(self.TreatmentCategoryID(), self.ReferrerProjectTreatmentID());
                ReferrerProjects();
                setTimeout(function () {
                    $("#lblMsg").slideUp("slow");

                }, 5000);
            }
        });

        //SAVE SLAs
        $.ajax({
            url: "/ReferrerPricing/UpdateProjectTreatmentSLA/",
            type: 'post',
            data: ko.toJSON(self.ServiceLevelAgreementSLA),
            contentType: 'application/json',
            success: function (result) {
                $("#lblSavePricingMessage").text("Pricing successfully updated....");

                $("#lblSavePricingMessage").show();

                setInterval(function () {
                    $("#lblSavePricingMessage").hide();
                }, 5000);
            },
            error: function (result) {
                alert(result.toString());
            }
        });
        self.EnableDisablePricing(false);
        self.PricingFlag(false);
    };
    self.CloseConfirmPricingDialog = function () {
        if (checkPricingTabForChanges()) {
            var answer = confirm("Changes made to pricing matrix for current treatment category.  Do you wish to save changes?");
            if (answer == true) {
                self.SaveFinance();
            }
            else if (answer == false) {
                changePricingFlag();
                $("#divProjectDetailContainer").dialog("close");
            }
        }
        else if (!checkPricingTabForChanges()) {
            $("#divProjectDetailContainer").dialog("close");

        }
    };
};

