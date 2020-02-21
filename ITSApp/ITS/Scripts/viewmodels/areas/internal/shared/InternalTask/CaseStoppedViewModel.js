
function StoppedCase(caseID, patientID, firstName, lastName, referrerID, caseNumber, caseDateOfInquiry, referrerProjectTreatmentID, treatmentTypeID, caseReferrerReferenceNumber, caseSpecialInstruction, caseReferrerDueDate, caseSubmittedDate, supplierID, treatmentTypeName, referrerName, projectName) {

    var self = this;
    self.CaseID = ko.observable(caseID);
    self.PatientID = ko.observable(patientID);
    self.FirstName = ko.observable(firstName);
    self.LastName = ko.observable(lastName);
    self.PatientName = ko.observable();
    self.ReferrerID = ko.observable(referrerID);
    self.CaseNumber = ko.observable(caseNumber);
    self.CaseDateOfInquiry = ko.observable(caseDateOfInquiry);
    self.ReferrerProjectTreatmentID = ko.observable(referrerProjectTreatmentID);
    self.TreatmentTypeID = ko.observable(treatmentTypeID);
    self.TreatmentTypeName = ko.observable(treatmentTypeName);
    self.CaseReferrerReferenceNumber = ko.observable(caseReferrerReferenceNumber);
    self.CaseSpecialInstruction = ko.observable(caseSpecialInstruction);
    self.CaseReferrerDueDate = ko.observable(caseReferrerDueDate);
    self.CaseSubmittedDate = ko.observable(caseSubmittedDate);
    self.SupplierID = ko.observable(supplierID);
    self.ReferrerName = ko.observable(referrerName);

    self.PatientName = ko.computed(function () {
        return self.FirstName() + " " + self.LastName();
    });
    self.ProjectName = ko.observable(projectName);
};


function CaseStoppedViewModel() {
    var self = this;
    self.StoppedCases = ko.observableArray([]);


    var pagingSettings = {
        pageSize: 5,
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
            self.GetCaseStoppedCaseWorkflowPatientProjects(function () { $('#divGridDisplaySpinner').spin(false); }, skip, take);
        else
            self.GetCaseStoppedCaseWorkflowPatientProjectsByTreatmentCategoryID(RadiobuttonSelectedID, function () { $('#divGridDisplaySpinner').spin(false); }, skip, take);

    });

    this.GetCaseStoppedCaseWorkflowPatientProjects = function (callback, skip, take) {
        if (skip == undefined || take == undefined) {
            skip = 0;
            take = pagingSettings.pageSize;
        }
        $.ajax({
            url: "/CaseStopped/GetCaseStoppedCaseWorkflowPatientProjects/",
            cache: false,
            type: "POST", dataType: "json",
            data: { skip: skip, take: take },
            success: function (model) {
                self.StoppedCases([]);
                self.TotalItemCount(model.CaseWorkflowPatientReferrerProjectTotalCount);
                $.each(model.CaseWorkflowPatientReferrerProjects, function (index, value) {
                    self.StoppedCases.push(new StoppedCase(value.CaseID, value.PatientID, value.FirstName, value.LastName, value.ReferrerID, value.CaseNumber, value.CaseDateOfInquiry,
                 value.ReferrerProjectTreatmentID, value.TreatmentTypeID, value.CaseReferrerReferenceNumber, value.CaseSpecialInstruction, self.dateFormat(value.CaseReferrerDueDate),
                 self.dateFormat(value.CaseSubmittedDate), value.SupplierID, value.TreatmentTypeName, value.ReferrerName, value.ProjectName));
                });
                callback(true);
            },
            error: function () {

            }
        });
    };

    this.GetCaseStoppedCaseWorkflowPatientProjectsByTreatmentCategoryID = function (selectedItem, callback, skip, take) {
        if (skip == undefined || take == undefined) {
            skip = 0;
            take = pagingSettings.pageSize;
        }
        $.ajax({
            url: "/CaseStopped/GetCaseStoppedCaseWorkflowPatientProjectsByTreatmentCategoryID/",
            cache: false,
            type: "POST", dataType: "json",
            data: { treatmentCatagoryID: selectedItem, skip: skip, take: take },
            success: function (model) {
                self.StoppedCases([]);
                self.TotalItemCount(model.CaseWorkflowPatientReferrerProjectTotalCount);
                $.each(model.CaseWorkflowPatientReferrerProjects, function (index, value) {
                    self.StoppedCases.push(new StoppedCase(value.CaseID, value.PatientID, value.FirstName, value.LastName, value.ReferrerID, value.CaseNumber, value.CaseDateOfInquiry,
                 value.ReferrerProjectTreatmentID, value.TreatmentTypeID, value.CaseReferrerReferenceNumber, value.CaseSpecialInstruction, self.dateFormat(value.CaseReferrerDueDate),
                 self.dateFormat(value.CaseSubmittedDate), value.SupplierID, value.TreatmentTypeName, value.ReferrerName, value.ProjectName));
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