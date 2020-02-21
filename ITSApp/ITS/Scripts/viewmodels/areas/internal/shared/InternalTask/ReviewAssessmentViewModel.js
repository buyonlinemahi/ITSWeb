
function ReviewAssessment(caseID, patientID, firstName, lastName, referrerID, caseNumber, caseDateOfInquiry, referrerProjectTreatmentID, treatmentTypeID, caseReferrerReferenceNumber, caseSpecialInstruction, caseReferrerDueDate, caseSubmittedDate, supplierID, workflowID, treatmentTypeName, referrerName, projectName) {

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

};




function ReviewAssessmentViewModel() {
    var self = this;
    self.ReviewAssessments = ko.observableArray([]);
    var ReviewAssessmentAllSelectedId = 5;



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
            self.GetAllReviewAssessmentProjects(function () { $('#divGridDisplaySpinner').spin(false); }, skip, take);
        else
            self.GetReviewAssessmentProjectsByTreatmentCategoryID(RadiobuttonSelectedID, function () { $('#divGridDisplaySpinner').spin(false); }, skip, take);

    });

//    this.InitializeReviewAssessmentViewModel = function (selectedTreatmentCategoryID) {

//        self.ReviewAssessments([]);
//        if (selectedTreatmentCategoryID == ReviewAssessmentAllSelectedId) {
//            self.GetAllReviewAssessmentProjects();
//        }
//        else {
//            self.GetReviewAssessmentProjectsByTreatmentCategoryID(selectedTreatmentCategoryID);
//        }
//    };


    this.GetAllReviewAssessmentProjects = function (callback, skip, take) {

       
        if (skip == undefined || take == undefined) {
            skip = 0;
            take = pagingSettings.pageSize;
        }
        self.ReviewAssessments([]);
        $.ajax({
            url: "/ReviewAssessmentQA/GetReviewAssessmentQACaseWorkflowPatientProjects/",
            cache: false,
            dataType: "json",
            data: { skip: skip, take: take },
            type: "POST",
            success: function (resultData) {
                self.TotalItemCount(resultData.CaseWorkflowPatientReferrerProjectTotalCount);
                self.BindReviewAssessmentProjects(resultData);
                callback(true);
            }
        });
    };

    this.GetReviewAssessmentProjectsByTreatmentCategoryID = function (selectedTreatmentCategoryID, callback, skip, take) {
        
        if (skip == undefined || take == undefined) {
            skip = 0;
            take = pagingSettings.pageSize;
        }
        $.ajax({
            url: "/ReviewAssessmentQA/GetReviewAssessmentQACaseWorkflowPatientProjectsByTreatmentCategoryID/",
            cache: false,
            data: { treatmentCategoryID: selectedTreatmentCategoryID, skip: skip, take: take },
            dataType: "json",
            type: "POST",
            success: function (resultData) {
                self.ReviewAssessments([]);
                self.TotalItemCount(resultData.CaseWorkflowPatientReferrerProjectTotalCount);
                self.BindReviewAssessmentProjects(resultData);
                callback(true);
            }
        });
    };

    this.BindReviewAssessmentProjects = function (data) {
        $.each(data.CaseWorkflowPatientReferrerProjects, function (index, value) {
            self.ReviewAssessments.push(new ReviewAssessment(value.CaseID, value.PatientID, value.FirstName, value.LastName, value.ReferrerID, value.CaseNumber, value.CaseDateOfInquiry,
                 value.ReferrerProjectTreatmentID, value.TreatmentTypeID, value.CaseReferrerReferenceNumber, value.CaseSpecialInstruction, self.dateFormat(value.CaseReferrerDueDate),
                 self.dateFormat(value.CaseSubmittedDate), value.SupplierID, value.WorkflowID, value.TreatmentTypeName, value.ReferrerName, value.ProjectName));
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

    // events
    this.CaseNumberClick = function (item) {
       
        $("#ReviewAssessmentQAScreenPopUp").dialog("open");

    }
}


$(function () {

    $("#ReviewAssessmentQAScreenPopUp").dialog({
        autoOpen: false,
        width: 1020,
        height: 900,
        modal: true

    });

});




