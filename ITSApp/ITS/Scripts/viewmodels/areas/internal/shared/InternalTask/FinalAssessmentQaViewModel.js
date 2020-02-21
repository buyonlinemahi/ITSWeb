
/*
Author :Harpreet Singh
Created on:01-5-2013
*/

function FinalAssessmentQAViewModel() {
    var self = this;
    self.FinalAssessments = ko.observableArray([]);
    

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
            self.GetFinalAssessmentCaseWorkflow(function () { $('#divGridDisplaySpinner').spin(false); }, skip, take);
        else
            self.GetFinalAssessmentCaseWorkflowByTreatmentID(RadiobuttonSelectedID, function () { $('#divGridDisplaySpinner').spin(false); }, skip, take);

    });

    this.GetFinalAssessmentCaseWorkflow = function (callback, skip, take) {
        if (skip == undefined || take == undefined) {
            skip = 0;
            take = pagingSettings.pageSize;
        }
        $.ajax({
            url: '/FinalAssessment/GetFinalAssessmentCaseWorkflowPatientProjects',
            type: 'post',
            cache: false,
            datatype: 'application/json',
            data: { skip: skip, take: take },
            success: function (data) {
                self.TotalItemCount(data.CaseWorkflowPatientReferrerProjectTotalCount);
                ko.mapping.fromJS(data.CaseWorkflowPatientReferrerProjects, mappingOptions, self.FinalAssessments);
                callback(true);
            }
        });
    };
    this.GetFinalAssessmentCaseWorkflowByTreatmentID = function (selectedTreatment, callback, skip, take) {
        if (skip == undefined || take == undefined) {
            skip = 0;
            take = pagingSettings.pageSize;
        }
        $.ajax({
            url: '/FinalAssessment/GetFinalAssessmentCaseWorkflowPatientProjectsByTreatmentCategoryID',
            type: 'post',
            cache: false,
            datatype: 'application/json',
            data: { treatmentID: selectedTreatment, skip: skip, take: take },
            success: function (data) {
                self.TotalItemCount(data.CaseWorkflowPatientReferrerProjectTotalCount);
                ko.mapping.fromJS(data.CaseWorkflowPatientReferrerProjects, mappingOptions, self.FinalAssessments);
                callback(true);
            }
        });
    };

    var mappingOptions = {
        create: function (options) {
            return (new (function () {
                this.CaseDueDate = ko.computed(function () {

                    this.CaseSubmittedDate = ko.computed(function () {
                        return self.dateFormat(this.CaseSubmittedDate());

                    }, this);

                    this.FullName = ko.computed(function () {
                        return this.FirstName() + " " + this.LastName();

                    }, this);

                    return self.dateFormat(this.CaseReferrerDueDate());
                }, this);

                ko.mapping.fromJS(options.data, {}, this);
            })(/* call the ctor here */));
        }

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