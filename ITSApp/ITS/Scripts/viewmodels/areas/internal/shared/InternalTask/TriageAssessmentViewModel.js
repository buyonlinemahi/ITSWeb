
function Case(caseID, patientID, firstName, lastName, referrerID, caseNumber, caseDateOfInquiry, referrerProjectTreatmentID, treatmentTypeID, caseReferrerReferenceNumber, caseSpecialInstruction, caseReferrerDueDate, caseSubmittedDate, supplierID, workflowID, treatmentTypeName, referrerName, projectName, referralDownloadPath) {

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
    self.WorkflowID = ko.observable(workflowID);
    self.ReferrerName = ko.observable(referrerName);
    self.ProjectName = ko.observable(projectName);
    self.PatientName = ko.computed(function () {
        return self.FirstName() + " " + self.LastName();
    });
    self.ReferralDownloadPath = ko.observable(referralDownloadPath);
};


function TriageAssessmentViewModel() {

    var self = this;
    self.ReferrerID = ko.observable(3);
    self.WorkflowID = ko.observable();
    self.CaseID = ko.observable();
    self.Cases = ko.observableArray([]);

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
            self.GetTriageAssessmentCases(function () { $('#divGridDisplaySpinner').spin(false); }, skip, take);
        else
            self.GetTriageAssessmentCasesByTreatmentCategoryID(RadiobuttonSelectedID, function () { $('#divGridDisplaySpinner').spin(false); }, skip, take);

    });


    this.GetTriageAssessmentCases = function (callback, skip, take) {

        if (skip == undefined || take == undefined) {
            skip = 0;
            take = pagingSettings.pageSize;
        }
        $.ajax({
            url: "/TriageAssessmentQA/GetTriageAssessmentCases/",
            cache: false,
            type: "POST",
            data: { skip: skip, take: take },
            dataType: "json",
            success: function (model) {
               
                self.Cases([]);
                self.TotalItemCount(model.CaseWorkflowPatientReferrerProjectTotalCount);
                $.each(model.CaseWorkflowPatientReferrerProjects, function (index, value) {
                    self.Cases.push(new Case(value.CaseID, value.PatientID, value.FirstName, value.LastName, value.ReferrerID, value.CaseNumber, self.dateFormat(value.CaseDateOfInquiry),
                                 value.ReferrerProjectTreatmentID, value.TreatmentTypeID, value.CaseReferrerReferenceNumber, value.CaseSpecialInstruction, self.dateFormat(value.CaseReferrerDueDate),
                                 self.dateFormat(value.CaseSubmittedDate), value.SupplierID, value.WorkflowID, value.TreatmentTypeName, value.ReferrerName, value.ProjectName, value.ReferralDownloadPath));
                });

                callback(true);

            },
            error: function () {

            }
        });
    };

    this.GetTriageAssessmentCasesByTreatmentCategoryID = function (selectedItem, callback, skip, take) {

        if (skip == undefined || take == undefined) {
            skip = 0;
            take = pagingSettings.pageSize;
        }
        $.ajax({
            url: "/TriageAssessmentQA/GetTriageAssessmentCasesByTreatmentCategoryID/",
            cache: false,
            type: "POST", dataType: "json",
            data: { treatmentCatagoryID: selectedItem, skip: skip, take: take },
            success: function (model) {

                self.Cases([]);
                self.TotalItemCount(model.CaseWorkflowPatientReferrerProjectTotalCount);
                $.each(model.CaseWorkflowPatientReferrerProjects, function (index, value) {

                    self.Cases.push(new Case(value.CaseID, value.PatientID, value.FirstName, value.LastName, value.ReferrerID, value.CaseNumber, self.dateFormat(value.CaseDateOfInquiry),
                 value.ReferrerProjectTreatmentID, value.TreatmentTypeID, value.CaseReferrerReferenceNumber, value.CaseSpecialInstruction, self.dateFormat(value.CaseReferrerDueDate),
                 self.dateFormat(value.CaseSubmittedDate), value.SupplierID, value.WorkflowID, value.TreatmentTypeName, value.ReferrerName, value.ProjectName, value.ReferralDownloadPath));
                });
                callback(true);
            },
            error: function () {

            }
        });
    };



    this.CaseNumberClick = function (newValue) {

        window.location = '/TriageAssessmentQA/GetTriageAssessmentByCaseID/' + newValue.CaseID();

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