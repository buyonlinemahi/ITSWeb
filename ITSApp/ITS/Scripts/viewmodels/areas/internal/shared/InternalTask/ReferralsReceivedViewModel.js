

function ReferralsReceivedViewModel() {
    var self = this;
    self.ReferrerID = ko.observable();// this is mock data 
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
            self.GetReferralCases(function () { $('#divGridDisplaySpinner').spin(false); }, skip, take);
        else
            self.GetReferralCasesByTreatmentCategoryID(RadiobuttonSelectedID, function () { $('#divGridDisplaySpinner').spin(false); }, skip, take);

    });


    this.GetReferralCases = function (callback, skip, take) {
      
        if (skip == undefined || take == undefined) {
            skip = 0;
            take = pagingSettings.pageSize;
        }
        $.ajax({
            url: "/ReferralsReceived/GetReferralCases/",
            cache: false,
            type: "POST",
            data: { skip: skip, take: take },
            dataType: "json",
            success: function (model) {
                self.Cases([]);
                self.TotalItemCount(model.CaseWorkflowPatientReferrerProjectTotalCount);
                ko.mapping.fromJS(model.CaseWorkflowPatientReferrerProjects, {}, self.Cases);
                ko.utils.arrayForEach(self.Cases(), function (item) {
                    item.CaseReferrerDueDate(self.dateFormat(item.CaseReferrerDueDate()));
                });
                callback(true);

            },
            error: function () {

            }
        });
    };

    this.GetReferralCasesByTreatmentCategoryID = function (selectedItem, callback, skip, take) {
        
        if (skip == undefined || take == undefined) {
            skip = 0;
            take = pagingSettings.pageSize;
        }
        $.ajax({
            url: "/ReferralsReceived/GetReferralCasesByTreatmentCategoryID/",
            cache: false,
            type: "POST", dataType: "json",
            data: { treatmentCatagoryID: selectedItem, skip: skip, take: take },
            success: function (model) {
                self.Cases([]);
                self.TotalItemCount(model.CaseWorkflowPatientReferrerProjectTotalCount);
                ko.mapping.fromJS(model.CaseWorkflowPatientReferrerProjects, {}, self.Cases);
                ko.utils.arrayForEach(self.Cases(), function (item) {
                    item.CaseReferrerDueDate(self.dateFormat(item.CaseReferrerDueDate()));
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