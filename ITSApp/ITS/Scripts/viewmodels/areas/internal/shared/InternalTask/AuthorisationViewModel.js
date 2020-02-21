/*
*  Latest Version  : 1.0
*  Auther       : Robin Singh
*  Version      : 1.0
*  Date         : 26-April-2013
*  Description  : Created AuthorisationViewModel.
*/
function Authorisation(caseID, patientID, firstName, lastName, referrerID, caseNumber, referrerProjectTreatmentID, treatmentTypeID, caseReferrerReferenceNumber, caseReferrerDueDate, caseSubmittedDate, supplierID, treatmentTypeName, referrerName, assessmentAuthorisationName) {

    var self = this;
    self.CaseID = ko.observable(caseID);
    self.PatientID = ko.observable(patientID);
    self.FirstName = ko.observable(firstName);
    self.LastName = ko.observable(lastName);
    self.PatientName = ko.observable();
    self.ReferrerID = ko.observable(referrerID);
    self.CaseNumber = ko.observable(caseNumber);
    self.ReferrerProjectTreatmentID = ko.observable(referrerProjectTreatmentID);
    self.TreatmentTypeID = ko.observable(treatmentTypeID);
    self.TreatmentTypeName = ko.observable(treatmentTypeName);
    self.CaseReferrerReferenceNumber = ko.observable(caseReferrerReferenceNumber);
    self.CaseReferrerDueDate = ko.observable(caseReferrerDueDate);
    self.CaseSubmittedDate = ko.observable(caseSubmittedDate);
    self.SupplierID = ko.observable(supplierID);
    self.ReferrerName = ko.observable(referrerName);
    self.AssessmentAuthorisationName = ko.observable(assessmentAuthorisationName);
    self.PatientName = ko.computed(function () {
        return self.FirstName() + " " + self.LastName();
    });

};
function AuthorisationViewModel() {
    var self = this;

    self.Authorisations = ko.observableArray([]);

    var pagingSettings = {
        pageSize:5,
        pageSlide: 2
    };

    // page variables and methods used for pagging  
    self.TotalItemCount = ko.observable();
    self.Pager = ko.pager(self.TotalItemCount);
    self.Pager().PageSize(pagingSettings.pageSize);
    self.Pager().PageSlide(pagingSettings.pageSlide);
    self.Pager().CurrentPage(1);
    self.Pager().CurrentPage.subscribe(function () {
        var skip = pagingSettings.pageSize * (self.Pager().CurrentPage() - 1);
        var take = pagingSettings.pageSize;
        $('#divGridDisplaySpinner').spin();
        if (RadiobuttonSelectedID == 5)
            self.GetAuthorisationCases(function () { $('#divGridDisplaySpinner').spin(false); }, skip, take);
        else
            self.GetAuthorisationCasesByTreatmentCategoryID(RadiobuttonSelectedID, function () { $('#divGridDisplaySpinner').spin(false); }, skip, take);

    });

    this.GetAuthorisationCases = function (callback, skip, take) {
        if (skip == undefined || take == undefined) {
            skip = 0;
            take = pagingSettings.pageSize;
        }
        $.ajax({
            url: "/Authorisation/GetAuthorisationCaseWorkflowPatientProjects/",
            cache: false,
            type: "POST", dataType: "json",
            data: { skip: skip, take: take },
            success: function (model) {
                self.Authorisations([]);
                self.TotalItemCount(model.CaseWorkflowPatientReferrerProjectTotalCount);
                $.each(model.CaseWorkflowPatientReferrerProjects, function (index, value) {
                    self.Authorisations.push(new Authorisation(value.CaseID, value.PatientID, value.FirstName, value.LastName, value.ReferrerID, value.CaseNumber,
                 value.ReferrerProjectTreatmentID, value.TreatmentTypeID, value.CaseReferrerReferenceNumber, self.dateFormat(value.CaseReferrerDueDate),
                 self.dateFormat(value.CaseSubmittedDate), value.SupplierID, value.TreatmentTypeName, value.ReferrerName, value.AssessmentAuthorisationName));
                });
                callback(true);
            },
            error: function () {

            }
        });
    };

    this.GetAuthorisationCasesByTreatmentCategoryID = function (selectedItem, callback, skip, take) {

        if (skip == undefined || take == undefined) {
            skip = 0;
            take = pagingSettings.pageSize;
        }

        $.ajax({
            url: "/Authorisation/GetAuthorisationCaseWorkflowPatientProjectsByTreatmentCategoryID/",
            cache: false,
            type: "POST", dataType: "json",
            data: { treatmentCatagoryID: selectedItem, skip: skip, take: take },
            success: function (model) {
                self.Authorisations([]);
                self.TotalItemCount(model.CaseWorkflowPatientReferrerProjectTotalCount);
                $.each(model.CaseWorkflowPatientReferrerProjects, function (index, value) {
                    self.Authorisations.push(new Authorisation(value.CaseID, value.PatientID, value.FirstName, value.LastName, value.ReferrerID, value.CaseNumber,
                 value.ReferrerProjectTreatmentID, value.TreatmentTypeID, value.CaseReferrerReferenceNumber, self.dateFormat(value.CaseReferrerDueDate),
                 self.dateFormat(value.CaseSubmittedDate), value.SupplierID, value.TreatmentTypeName, value.ReferrerName, value.AssessmentAuthorisationName));
                });
                callback(true);
            },
            error: function () {

            }
        });
    };

    this.dateFormat = function (jsonDate) {

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
};