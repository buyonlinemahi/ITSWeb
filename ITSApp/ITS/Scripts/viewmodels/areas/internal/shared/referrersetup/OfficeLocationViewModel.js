/*
*  Latest Version  : 1.0


*  Modified by  : Anuj Batra
*  Version      : 1.0
*  Date         : 16-Nov-2012
*  Description  : Added viewModel to Implement Office location tab- referrer setup
*/

function OfficeLocationViewModel() {
    var self = this;
    self.Name = ko.observable('');
    self.Address = ko.observable('');
    self.City = ko.observable('');
    self.Region = ko.observable('');
    self.PostCode = ko.observable('');
    self.IsMainOffice = ko.observable(null);
    self.ReferrerLocationID = ko.observable('');
    self.ReferrerID = ko.observable();
    self.IsActive = ko.observable(true);
    self.locations = ko.observableArray();
    self.trueFalse = ko.observable();
    self.IsSaveVisible = ko.observable(true);
    self.IsUpdateVisible = ko.observable(false);
    self.previousMainOfficeValue = ko.observable('');

    this.editItem = function (item) {

        self.ReferrerLocationID(item.ReferrerLocationID);
        self.Name(item.Name);
        self.Address(item.Address);
        self.City(item.City);
        self.Region(item.Region);
        self.PostCode(item.PostCode);
        self.IsMainOffice(item.IsMainOffice);
        self.IsUpdateVisible(true);
        self.IsSaveVisible(false);
        $("#addNewLocation").dialog("open");
        self.IsMainOffice(item.IsMainOffice);
        self.previousMainOfficeValue(item.IsMainOffice);

    };

    this.closeBtnClick = function () {
        $("#closeButtonPopUp").dialog("open");
    };

    this.closePopUpYesBtnClick = function () {
        self.clearModelValues();
        $("#closeButtonPopUp").dialog("close");
        $("#addNewLocation").dialog("close");
    };

    this.closePopUpNoBtnClick = function () {
        $("#closeButtonPopUp").dialog("close");
    };

    this.clearModelValues = function () {
        self.ReferrerLocationID(null);
        self.Name(null);
        self.Address(null);
        self.City(null);
        self.Region(null);
        self.PostCode(null);
        self.IsMainOffice(null);
        self.IsSaveVisible(true);
        self.IsUpdateVisible(false);
        self.previousMainOfficeValue(null);
    };

    this.getReferrerOfficeLocation = function (ReferrerID) {

        $.ajax({
            url: "/Referrer/GetReferrerLocationsByReferrerID/",
            cache: false,
            type: "POST", dataType: "json",
            data: { ReferrerID: ReferrerID },
            success: function (data) {
                if (data != null) {
                    self.locations(data);
                    self.ReferrerID(data[0].ReferrerID);
                }
            }
        });

    };

    this.deleteConfermationPopUp = function (item) {
        $("#deleteConfermationPopUp").dialog("open");
        self.ReferrerLocationID(item.ReferrerLocationID);
    };

    this.cancelDeletePopUp = function () {
        $("#deleteConfermationPopUp").dialog("close");
    };

    this.deleteByReferrerLocationID = function (item) {
        $.ajax({
            url: "/Referrer/DeleteByReferrerLocationID/",
            cache: false,
            type: "POST", dataType: "json",
            data: { ReferrerLocationID: item.ReferrerLocationID },
            success: function () {
                self.getReferrerOfficeLocation(self.ReferrerID);

            },
            error: function () {

            }
        });

        $("#deleteConfermationPopUp").dialog("close");
    };

    this.ValidateFields = function () {

        var errmsg = "";
        var hasError = false;
        if (self.Name() == null || self.Name() == '') {
            hasError = true;
            errmsg = "Location Name is required Field";
        }
        else if (self.Address() == null || self.Address() == '') {
            hasError = true;
            errmsg = "Location Address is required Field";
        }

        else if (self.PostCode() == null || self.PostCode() == '') {
            hasError = true;
            errmsg = "Location PostCode is required Field";
        }

        else if (self.IsMainOffice() === '' || self.IsMainOffice() == null) {
            hasError = true;
            errmsg = "Please Select Head Office";
        }
        if (hasError) {
            alert(errmsg);
            return false;
        }
        else { return true; }
    };

    this.saveReferrerLocation = function () {

        if (!self.ValidateFields()) { }
        else {

            $.ajax({
                url: "/Referrer/AddReferrerLocation/",
                type: 'post',
                data: ko.toJSON(this),
                contentType: 'application/json',
                success: function (result) {
                    self.getReferrerOfficeLocation(self.ReferrerID);

                }
            });

            self.clearModelValues();
            $("#addNewLocation").dialog("close");
        }
    };

    this.updateLocation = function (item) {

        if (self.previousMainOfficeValue() == true && self.IsMainOffice() == 'false') {
            alert("Not Possible..! Atleast one Main Office location is required.");
            return;
        }
        if (!this.ValidateFields()) { }
        else {
            $.ajax({

                url: "/Referrer/UpdateReferrerLocation/",
                type: 'post',
                data: ko.toJSON(this),
                contentType: 'application/json',
                success: function (result) {
                    self.getReferrerOfficeLocation(self.ReferrerID);

                },
                error: function (result) { }
            });
            self.clearModelValues();
            $("#addNewLocation").dialog("close");
        }
    };


};


//Office location header script

$(function () {

    $("#addNewLocation").dialog({
        autoOpen: false,
        width: 1020,
        modal: true

    });

    $("#deleteConfermationPopUp").dialog({
        autoOpen: false,
        width: 620,
        modal: true
    });

    $("#closeButtonPopUp").dialog({
        autoOpen: false,
        width: 620,
        modal: true
    });

    $('#closeButtonPopUp #closePopUpNoBtn').click(function () {

        $("#closeButtonPopUp").dialog("close");

    });

    $('#closePopUpYesBtn').click(function () {

        officeViewModel.clearModelValues();

        $("#closeButtonPopUp").dialog("close");
        $("#addNewLocation").dialog("close");

    });

    $("#addLocation").button().click(function () {
        var hasError;
        if (officeViewModel.ReferrerID() == null) {
            hasError = true;
            errmsg = "Please Select Referrer First";
        }
        if (hasError) {
            alert(errmsg);
            return;
        }
        else {
            $("#addNewLocation").dialog("open");
            return true;
        }

    });

    $("#addNewLocation").removeClass("ui-dialog-content");
    $("#addNewLocation").removeClass("ui-widget-content");

});
