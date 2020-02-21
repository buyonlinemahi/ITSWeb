
/* 
Created by Harpreet Singh
*/

function AcceptAndReferViewModel() {
    var self = this;
    self.PatientID = ko.observable();
    self.CaseID = ko.observable();
    self.Title = ko.observable();
    self.FirstName = ko.observable();
    self.LastName = ko.observable();
    self.Address = ko.observable();
    self.PostCode = ko.observable();
    self.InjuryDate = ko.observable();
    self.BirthDate = ko.observable();
    self.Age = ko.observable();
    self.Email = ko.observable();
    self.Gender = ko.observable();
    self.CaseNumber = ko.observable();
    self.CaseDateOfInquiry = ko.observable();
    self.HomePhone = ko.observable();
    self.WorkPhone = ko.observable();
    self.MobilePhone = ko.observable();
    self.TreatmentCategoryName = ko.observable();
    self.TreatmentTypeName = ko.observable();
    self.City = ko.observable();
    self.Region = ko.observable();
    self.CaseReferrerReferenceNumber = ko.observable();
    self.SupplierDistanceRankPriceArray = ko.observableArray([]);
    self.SubmitToSupplier = ko.observable(false);
    self.SelectedSupplier = ko.observable();
    self.SelectedPostCode = ko.observable();
    self.SupplierID = ko.observable();
    self.CurrentYear = ko.observable(new Date().getFullYear());
    self.NORecoredFound = ko.observable(false);
    self.ShowGridData = ko.observable(true);
    self.ReferralFileDownloadPath = ko.observable();

    this.InitilizeAccepAndReferScreen = function (model) {
       
        $("#AcceptAndRefPopUp").spin();

        self.PostCode(model.CasePatientTreatment.PostCode);
        //        self.GetPatientAndCaseByCaseID();

        ko.mapping.fromJS(model.CasePatientTreatment, {}, self);
        self.CaseID(model.CaseID);
        if (model.SupplierDistanceRankPrice.length > 0) {
            self.SupplierDistanceRankPriceArray(model.SupplierDistanceRankPrice);
            self.NORecoredFound(false);
            self.ShowGridData(true);
        }
        else {
            self.NORecoredFound(true);
            self.ShowGridData(false);

        }
    };

    self.SelectedSupplier.subscribe(function (newvalue) {
        self.SupplierID(newvalue);
        self.SubmitToSupplier(true);
    });

    this.SubmitCaseToSupplier = function (newvalue) {
        alert($("#ddlSupplierID").val());
        /*
        $.ajax({
            url: '/AcceptAndRefer/SubmitCaseToSupplier',
            type: 'post',
            cache: false,
            datatype: 'application/json',
            data: { caseID: self.CaseID(), supplierID: self.SupplierID() },
            success: function (response) {
                alert(response);
                window.location = '/Internal/InternalTask/';
            }
        });*/

    };


    var dateFormat = function (jsonDate) {
        var value = parseJsonDateString(jsonDate);
        var strDate = value.getMonth() + 1 + "/" + value.getDate() + "/" + value.getFullYear();
        return strDate;
    };

    var jsonDateRE = /^\/Date\((-?\d+)(\+|-)?(\d+)?\)\/$/;

    var parseJsonDateString = function (value) {
        var arr = value && jsonDateRE.exec(value);
        if (arr) {
            return new Date(parseInt(arr[1]));
        }

        return value;
    };

    this.CalculatePatientAge = ko.computed(function () {
        if (self.BirthDate() != null) {
            var today = new Date();
            var pQDt = new Date(self.BirthDate());
            var dt = today.getFullYear() - pQDt.getFullYear();
            var m = today.getMonth() - pQDt.getMonth();
            if (m < 0 || (m === 0 && today.getDate() < pQDt.getDate())) {
                dt--;
            }
            self.Age(dt);
        }

    });

    this.DateOfInquiry = ko.computed(function () {
        if (self.CaseDateOfInquiry() != null) {
            self.CaseDateOfInquiry(dateFormat(self.CaseDateOfInquiry()));
        }

    });
    this.DateOfBirth = ko.computed(function () {
        if (self.BirthDate() != null) {
            self.BirthDate(dateFormat(self.BirthDate()));
        }

    });

};

$(function () {

    $("#patient-code-notifcton").dialog({
        autoOpen: false,
        height: 300,
        width: 350,
        modal: true

    });

    $("#Seeinstructons").click(function () {
        $("#patient-code-notifcton").dialog("open");
    });

    $("#Email-text-ammend").dialog({
        autoOpen: false,
        height: 300,
        width: 350,
        modal: true

    });

    $("#ammendEmailText").click(function () {
        $("#Email-text-ammend").dialog("open");
    });


});