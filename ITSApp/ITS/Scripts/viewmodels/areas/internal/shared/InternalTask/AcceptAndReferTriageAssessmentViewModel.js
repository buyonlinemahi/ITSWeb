function TriageSupplier(supplierID, supplierName, address, postCode, phone, price) {
    var self = this;
    self.Address = ko.observable(address);
    self.Phone = ko.observable(phone);
    self.PostCode = ko.observable(postCode);
    self.Price = ko.observable(price);
    self.SupplierID = ko.observable(supplierID);
    self.SupplierName = ko.observable(supplierName);
};

function AcceptAndReferTriageAssessmentViewModel() {
    var self = this;
    self.PatientID = ko.observable();
    self.CaseID = ko.observable();
    self.Title = ko.observable();
    self.FirstName = ko.observable();
    self.LastName = ko.observable();
    self.Address = ko.observable();
    self.PostCode = ko.observable();
    self.InjuryDate = ko.observable().extend({ parseJsonDate: null });
    self.BirthDate = ko.observable().extend({ parseJsonDate: null });
    self.Age = ko.observable();
    self.Email = ko.observable();
    self.Gender = ko.observable();
    self.CaseNumber = ko.observable();
    self.CaseDateOfInquiry = ko.observable().extend({ parseJsonDate: null });
    self.HomePhone = ko.observable();
    self.WorkPhone = ko.observable();
    self.MobilePhone = ko.observable();
    self.TreatmentCategoryName = ko.observable();
    self.TreatmentCategoryID == ko.observable();
    self.TreatmentTypeName = ko.observable();
    self.TreatmentCategoryID = ko.observable(3);
    self.City = ko.observable();
    self.Region = ko.observable();
    self.CaseReferrerReferenceNumber = ko.observable();
    self.SupplierTriageArray = ko.observableArray([]);
    self.SubmitToSupplier = ko.observable(false);
    self.SelectedSupplier = ko.observable();
    self.SelectedPostCode = ko.observable();
    self.SupplierID = ko.observable();
    self.ShowGridData = ko.observable(true);
    self.ReferralFileDownloadPath = ko.observable();
    self.SelectedTriageSupplier = ko.observable();
    self.TriageSuppliers = ko.observableArray([]);
    self.SelectTriageSupplierID = ko.observable();


    self.SelectedTriageSupplier.subscribe(function (newvalue) {

        if (newvalue == null || newvalue == '' || newvalue == 'undefined')
            self.SubmitToSupplier(false);
    });

    this.Init = function (model) {

        self.PostCode(model.CasePatientTreatment.PostCode);
        ko.mapping.fromJS(model.CasePatientTreatment, {}, self);
        self.CaseID(model.CaseID);
        if (model.SupplierSupplierTreatmentsAndSupplierTreatmenPricing.length > 0 && model.SupplierSupplierTreatmentsAndSupplierTreatmenPricing != null) {
            self.SupplierTriageArray(model.SupplierSupplierTreatmentsAndSupplierTreatmenPricing);
            self.ShowGridData(true);
            self.GetSuppliersTriageTreamentPricingByTreatmentCategoryID();
        }
    };


    self.SelectTriageSupplierID.subscribe(function (newValue) {

        self.SupplierID(newValue);
        self.SubmitToSupplier(true);
    });


    this.SubmitTriageAssessmentCaseToSupplier = function () {

        $.ajax({
            url: '/AcceptAndReferTriageAssessment/SubmitTriageCaseToSupplier',
            type: 'post',
            cache: false,
            datatype: 'application/json',
            data: { caseID: self.CaseID(), supplierID: self.SupplierID() },
            success: function (response) {
                alert(response);
                window.location = '/Internal/InternalTask/';
            }
        });

    };


    this.GetSuppliersTriageTreamentPricingByTreatmentCategoryID = function () {

        $.ajax({
            url: '/AcceptAndReferTriageAssessment/GetSuppliersTriageTreamentPricingByTreatmentCategoryID',
            type: 'post',
            cache: false,
            datatype: 'application/json',
            data: { treatmentID: self.TreatmentCategoryID() },
            success: function (response) {


                $.each(response, function (index, value) {
                    self.TriageSuppliers.push(new TriageSupplier(value.SupplierID, value.SupplierName, value.Address, value.PostCode, value.Phone, value.Price));
                });

            }
        });
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

};

