
/*
================================================
Latest  : Version 1.0
Author  : Harpreet Singh
Created On : 7-Nov-2012
Purpous : Implementaion of Invoice Method Tab 
Version : 1.0
================================================
*/


function InvoiceMethods(typeID, invoicePrice, referrerProjectTreatmentID, invoiceTypes, referrerProjectTreatmentInvoiceID, managementPrice, managementFeeEnabled, enable) {

    var self = this;
    self.InvoiceMethodID = ko.observable(typeID);
    self.ReferrerProjectTreatmentInvoiceID = ko.observable(referrerProjectTreatmentInvoiceID);
    self.InvoicePrice = ko.observable(invoicePrice);
    self.ReferrerProjectTreatmentID = ko.observable(referrerProjectTreatmentID);
    self.InvoiceTypeArray = ko.observableArray(invoiceTypes);
    self.ManagementPrice = ko.observable(managementPrice);
    self.ManagementFeeEnabled = ko.observable(managementFeeEnabled);
    self.Enabled = ko.observable(enable);



    this.InvoiceMethodID.subscribe(function (newValue) {

        if (newValue == 1) {
            self.Enabled(false);
            self.InvoicePrice(0);
        }
        else {
            self.Enabled(true);
            self.InvoicePrice(0);
        }
    });



};

// initilize the model variables
function InvoiceMethodViewModel() {
    var self = this;

    self.InvoicesTypes = ko.observableArray();
    self.ReferrerProjectTreatmentID = ko.observable();
    self.ManagementPrice = ko.observable().extend({ number: { params: true, message: 'Please enter a number in price matrix'} });
    self.ManagementFeeEnabled = ko.observable();
    self.managmentEnable = ko.observable(true);
    self.ViewEnabled = ko.observable();
    self.errors = ko.validation.group(self);
    this.ManagementFeeEnabled.subscribe(function (newValue) {
        self.managmentEnable(newValue);
    });

    this.EnableDisable = function (flag) {
        self.ViewEnabled(flag);

    };
    this.InitializeInvoiceTab = function (referrerProjectTreatment) {

        self.ReferrerProjectTreatmentID(referrerProjectTreatment.ReferrerProjectTreatmentID);
        self.InvoicesTypes([]);
        self.GetAllInvoiceMethods();

    };

    this.ValidateInvoice = ko.computed(function () {
        if (!self.isValid())

            $("#btnSave").attr("disabled", "disabled");
        else
            $("#btnSave").removeAttr("disabled");


    });

    this.ClearInvoiceTab = function () {
        self.InvoicesTypes([]);
        self.ManagementPrice(0);
        self.ManagementFeeEnabled(false);
    };
    //Get All Invoice Methods
    this.GetAllInvoiceMethods = function () {

        $.ajax({
            url: "/ReferrerInvoiceMethod/GetAllInvoiceMethodByReferrerProjectTreatmentID/",
            type: 'post',
            cache: false,
            dataType: 'json',
            data: { referrerProjectTreatmentID: self.ReferrerProjectTreatmentID() },
            success: function (InvoicesResult) {
                //UPDATE INVOICE TYPES ARRAY
                self.InvoicesTypes.push(new InvoiceMethods(InvoicesResult.InvoiceMethodID,
                     InvoicesResult.InvoicePrice,
                     InvoicesResult.ReferrerProjectTreatmentID,
                     InvoicesResult.ReferrerInvoiceMethodTreatment, InvoicesResult.ReferrerProjectTreatmentInvoiceID, InvoicesResult.ManagementPrice, InvoicesResult.ManagementFeeEnabled, false));
                //UPDATE MANAGMENT FEE PART
                self.ManagementPrice(InvoicesResult.ManagementPrice);
                self.ManagementFeeEnabled(InvoicesResult.ManagementFeeEnabled);

            },
            error: function (result) {
                alert("error while processing request in invoice method");
            }
        });

    };
    //Method for update managment fee 
    this.UpdateManagemenntFee = function () {
        self.InvoicesTypes()[0].ManagementPrice(self.ManagementPrice);
        self.InvoicesTypes()[0].ManagementFeeEnabled(self.ManagementFeeEnabled);

    };
    //Method for update Invoce Treatments
    this.UpdateInvoiceMethod = function () {
        self.UpdateManagemenntFee();

        $.ajax({
            url: "/ReferrerInvoiceMethod/UpdateInvoiceMethodByReferrerProjectTreatmentID/",
            type: 'post',
            data: ko.toJSON(self.InvoicesTypes()),
            contentType: 'application/json',
            success: function (result) {
                self.InvoicesTypes([]);
                self.GetAllInvoiceMethods();
                alert(result);
            }
        });
    };

};