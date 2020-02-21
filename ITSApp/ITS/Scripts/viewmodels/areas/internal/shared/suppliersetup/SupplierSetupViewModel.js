
/*
*  Latest Version  : 1.0

*  Modified by  : Harpreet Singh
*  Version      : 1.0
*  Date         : 18-Dec-2012
*  Description  : Create viewModel to implement supplier setup

*  Latest Version  : 1.1

*  Modified by  : Harpreet Singh
*  Version      : 1.0
*  Date         : 28-Dec-2012
*  Description  : Modify Search cretaria

*/
function TreatmentCategory(name, val, isChecked, supplierid, supplierTretmentId, isVisible) {
    var self = this;
    self.TreatmentCategoryName = ko.observable(name);
    self.TreatmentCategoryID = ko.observable(val);
    self.Enabled = ko.observable(isChecked);
    self.SupplierID = ko.observable(supplierid);
    self.SupplierTreatmentID = ko.observable(supplierTretmentId);
    self.Enabled.subscribe(function (newValue) {
        EnablePricingtab(newValue, self.TreatmentCategoryID());
    });
}

function SupplierControlViewModel() {
    var self = this;
    var Actions = { AddNewSupplier: "AddNewSupplier", UpdateSupplier: "UpdateSupplier", AddSupplierPricing: "AddSupplierPricing" };

    self.ActionUrl = ko.observable("/Supplier/SupplierAutoCompleteByName");
    self.SupplierID = ko.observable();
    self.SupplierName = ko.observable();
    self.Address = ko.observable();
    self.City = ko.observable();
    self.Region = ko.observable();
    self.PostCode = ko.observable();
    self.Phone = ko.observable().extend({ number: { params: true, message: 'Please enter only a number in Supplier Telephone Number'} });
    self.Fax = ko.observable().extend({ number: { params: true, message: 'Please enter only a number in Supplier Fax Number'} });
    self.Website = ko.observable();
    self.Ranking = ko.observable('');
    self.Notes = ko.observable();
    self.IsWheelChairAccessibility = ko.observable();
    self.IsWeekends = ko.observable();
    self.IsEvenings = ko.observable();
    self.IsParking = ko.observable();
    self.IsHomeVisit = ko.observable();
    self.Status = ko.observable("InCompleted");
    self.pricingStatus = ko.observable(false);
    self.Enabled = ko.observable(false);
    self.Save = ko.observable(false);
    self.AddNew = ko.observable(true);
    self.Cancel = ko.observable(false);
    self.Ammend = ko.observable(false);
    self.Update = ko.observable(false);
    self.visibleSave = ko.observable(true);
    self.Message = ko.observable();
    self.IsVisible = ko.observable(false);
    self.SearchText = ko.observable(null);
    self.NewRec = ko.observable(false);
    self.UpdateRec = ko.observable(false);
    self.errors = ko.validation.group(self);

    this.InitializeSupplierTab = function (newSupplierValue) {
        self.clearModelValues();
        if (newSupplierValue.TreatmentCategories.length > 0) {
            self.TreatmentCategories([]);
            $.each(newSupplierValue.TreatmentCategories, function (index, value) {
                self.TreatmentCategories.push(new TreatmentCategory(value.TreatmentCategoryName, value.TreatmentCategoryID, value.Enabled, value.SupplierID, value.SupplierTreatmentID, true));
            });

        }
        self.updateModelSupplier(newSupplierValue);
    };

    this.ValidateNumeric = function () {
        var error;
        if (!self.isValid()) {
            $.each(self.errors(), function (index, value) {
                error = value();
                return false;
            });
            return error;
        } else {
            return null;
        }
    };

    this.clearModelValues = function () {
        self.SupplierID(null);
        self.SupplierName(null);
        self.Address(null);
        self.City(null);
        self.Region(null);
        self.PostCode(null);
        self.Phone(null);
        self.Fax(null);
        self.Website(null);
        self.Region(null);
        self.Ranking(null);
        self.Notes(null);
        self.IsWheelChairAccessibility(null);
        self.IsWeekends(null);
        self.IsEvenings(null);
        self.IsParking(null);
        self.IsHomeVisit(null);
    };

    this.updateModelSupplier = function (model) {
        self.SupplierID(model.SupplierID);
        self.SupplierName(model.SupplierName);
        self.Address(model.Address);
        self.City(model.City);
        self.Region(model.Region);
        self.PostCode(model.PostCode);
        self.Phone(model.Phone);
        self.Fax(model.Fax);
        self.Website(model.Website);
        self.Region(model.Region);
        self.Ranking(model.Ranking);
        self.Notes(model.Notes);
        self.IsWheelChairAccessibility(model.IsWheelChairAccessibility);
        self.IsWeekends(model.IsWeekends);
        self.IsEvenings(model.IsEvenings);
        self.IsParking(model.IsParking);
        self.IsHomeVisit(model.IsHomeVisit);
        self.Status(model.Status);
        pricinglViewModel.UpdateVisible(true);
        self.Ammend(true);
    };

    self.SelectedUrl = ko.observable(true);
    //categories should come from database, make it static for now.
    self.TreatmentCategories = ko.observableArray([new TreatmentCategory('Physiotherapy', 1, false, self.SupplierID, false),
                                                        new TreatmentCategory('Psychological', 2, false, self.SupplierID, false),
                                                        new TreatmentCategory('Diagnostic', 3, false, self.SupplierID, false),
                                                        new TreatmentCategory('Specialist Services', 4, false, self.SupplierID, false)]);
    //search categories.
    self.SearchCategories = [{ SearchCategoryName: 'SupplierName', SearchCategoryID: 1, IsSearchBy: self.SelectedUrl() },
                                                        { SearchCategoryName: 'Supplier Postal Code', SearchCategoryID: 2, IsSearchBy: false },
                                                      { SearchCategoryName: 'Supplier Treatment', SearchCategoryID: 3, IsSearchBy: false}];

    self.SelectedUrl.subscribe(function (newValue) {
        self.SearchText(null);
        if (newValue == 1)
            self.ActionUrl('/Supplier/SupplierAutoCompleteByName');
        else if (newValue == 2)
            self.ActionUrl('/Supplier/SupplierAutoCompleteByPostalCode');
        else if (newValue == 3)
            self.ActionUrl('/Supplier/SupplierAutoCompleteByTreatmentType');
    });

    this.ClearTreatmentCategories = function () {

        $.each(self.TreatmentCategories(), function (index, value) {
            if (value.Enabled() == true)
                value.Enabled(false);
        });
    };

    this.AddNewClick = function () {
        self.clearModelValues();
        self.Enabled(true);
        self.Save(true);
        self.AddNew(false);
        self.Cancel(true);
        self.Ammend(false);
        self.IsVisible(true);
        self.ClearTreatmentCategories();
        self.Update(false);
        InitilizedPricingTab(Actions.AddNewSupplier, self.TreatmentCategories().length);
        $("#tabs-inner").css("display", "none");
        window.setTimeout(function () {
            self.Status('InCompleted');
        }, 800);
    };
    this.AmmendClick = function () {
        self.Update(true);
        self.AddNew(false);
        self.Cancel(true);
        self.Ammend(false);
        self.Enabled(true);
        self.IsVisible(true);
        self.Save(false);
        self.visibleSave(false);
    };
    this.CancelClick = function () {
        self.Save(false);
        self.AddNew(true);
        self.Cancel(false);
        if (self.SupplierName() != null) {
            self.Ammend(true);
        }
        else {
            self.Ammend(false);
            $("#tabs-inner").css("display", "none");
        }
        self.Enabled(false);
        self.Update(false);
        self.visibleSave(true);
        self.IsVisible(false);
        self.clearModelValues();
    };

    this.AddSupplier = function () {
        var errmsg = "";
        var hasError = false;

        var validateNumeric = self.ValidateNumeric();

        if (self.SupplierName() == null) {
            errmsg = "Please Enter Supplier Name";
            hasError = true;
        }
        else if (self.Address() == null) {
            errmsg = "Please Enter Supplier Address";
            hasError = true;
        }
        else if (self.City() == null) {
            errmsg = "Please Enter Supplier City";
            hasError = true;
        }
        else if (self.Region() == null) {
            errmsg = "Please Enter Supplier Region";
            hasError = true;
        }
        else if (self.PostCode() == null || self.PostCode() == "0") {
            errmsg = "Please Enter Supplier PostCode";
            hasError = true;
        }
        else if (self.Phone() == null || self.Phone() == "0") {
            errmsg = "Please Enter Supplier Phone";
            hasError = true;
        } else if (self.Website() == null) {
            errmsg = "Please Enter Supplier Website";
            hasError = true;
        } else if (validateNumeric !== null) {
            errmsg = validateNumeric;
            hasError = true;
        }
        if (hasError == true) {
            alert(errmsg);
        }
        else {
           
            var validateTreatmentCategories = false;

            $.each(self.TreatmentCategories(), function (index, value) {
                if (value.Enabled() == true)
                    validateTreatmentCategories = true;
            });
            if (validateTreatmentCategories == false) {
                alert("Please Checked at least One treatment Category.");
                return false;
            }
            else {
                $.ajax({
                    url: "/Supplier/AddSupplierAndTreatmentTypes/",
                    type: 'post',
                    data: ko.toJSON(this),
                    contentType: 'application/json',
                    success: function (result) {
                        InitializeTabs(result);
                        self.IsVisible(false);
                        self.InitializeSupplierTab(result);

                        InitilizedPricingTab(Actions.AddSupplierPricing, result.TreatmentCategories);
                        $("#lblMessage").text("Supplier  " + self.SupplierName() + "  successfully created");
                        $("#lblMessage").show();
                        setInterval(function () { $("#lblMessage").hide(); }, 5000);
                        self.CancelClick();
                        $("#tabs-inner").css("display", "block");
                    }
                });

            }
        }
    };

    this.UpdateSupplier = function () {
        var errmsg = "";
        var hasError = false;

        var validateNumeric = self.ValidateNumeric();
        if (self.SupplierName() == null) {
            errmsg = "Please Enter Supplier Name";
            hasError = true;
        }
        else if (self.Address() == null) {
            errmsg = "Please Enter Supplier Address";
            hasError = true;
        }
        else if (self.City() == null) {
            errmsg = "Please Enter Supplier City";
            hasError = true;
        }
        else if (self.Region() == null) {
            errmsg = "Please Enter Supplier Region";
            hasError = true;
        }
        else if (self.PostCode() == null || self.PostCode() == '0') {
            errmsg = "Please Enter Supplier PostCode";
            hasError = true;
        }
        else if (self.Phone() == null || self.Phone() == '0') {
            errmsg = "Please Enter Supplier Phone";
            hasError = true;
        }
        else if (self.Website() == null) {
            errmsg = "Please Enter Supplier Website";
            hasError = true;
        } else if (validateNumeric !== null) {
            errmsg = validateNumeric;
            hasError = true;
        }
        if (hasError == true) {
            alert(errmsg);
        }
        else {
            $.ajax({
                url: "/Supplier/UpdateSupplier/",
                type: 'post',
                data: ko.toJSON(this),
                contentType: 'application/json',
                success: function (result) {
                    self.InitializeSupplierTab(result);
                    $("#lblMessage").text("Supplier  " + self.SupplierName() + "  successfully created");
                    $("#lblMessage").show();
                    setInterval(function () { $("#lblMessage").hide(); }, 5000);
                    self.IsVisible(false);
                    self.CancelClick();


                }
            });
        }
    };


    //get all area of experties
    this.GetAllTretmentCategories = function () {
        $.ajax({
            url: "/SupplierSetup/GetAllTreatmentCategories/",
            type: 'post',
            contentType: 'application/json',
            success: function (model) { 
                self.AreaOfExpertiesArray([]);
                $.each(model, function (index, value) {
                    self.AreaOfExpertiesArray.push(new AreaOfExperties(value.AreasofExpertiseID, value.AreasofExpertiseName));
                });

            }
        });

    };

    this.SuccessMessages = function (supplierName, sucess) {
        self.Message = ko.computed({
            read: function () {
                return "Supplier  " + supplierName() + " is successfully" + "  " + sucess + "  " + "to the supplier portal ";
            }
        });

    }

    ko.bindingHandlers.disableClick = {
        init: function (element, valueAccessor) {
            $(element).click(function (evt) {
                if (valueAccessor()) {
                    evt.preventDefault();
                    evt.stopImmediatePropagation();
                    // $("#popup").show();
                    $("#pricingAlert").show();
                    callback();
                }
            });
        },
        update: function (element, valueAccessor) {
            var value = ko.utils.unwrapObservable(valueAccessor());
            ko.bindingHandlers.css.update(element, function () { return { disabled_anchor: value }; });
        }

    };
    function callback() {
        setTimeout(function () { $("#pricingAlert").hide(); }, 5000);
    };

};



