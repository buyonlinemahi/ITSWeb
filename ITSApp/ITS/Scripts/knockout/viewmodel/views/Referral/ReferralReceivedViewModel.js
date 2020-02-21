
function ReferralReceivedViewModel() {
    var self = this;
    self.CaseWorkflowPatientReferrerProjects = ko.observableArray([]);
    self.TreatmentCategoryID = ko.observable();
    self.CaseWorkflowPatientReferrerProjectTotalCount = ko.observable();

    self.TotalItemCount = ko.computed(function () {
        return self.CaseWorkflowPatientReferrerProjectTotalCount();
    });

    var mappingOptions = {
        'CaseReferrerDueDate': {
            create: function (options) {
                if (options.data != null) {
                    return moment(options.data).format("DD/MM/YYYY");
                }
            }
        }
    };

    self.Init = function (data) {
        if (self.TreatmentCategoryID() != data.TreatmentCategoryID) {
            self.Pager().CurrentPage(1);
        }
        if (data != null) {
            ko.mapping.fromJS(data, mappingOptions, self);
        }
    };


    self.TreatmentCategoryIDChange = function () {
        $("#frmReferralReceivedCount").submit();
    };


    self.GetReferralRecivedWithSkipTake = function (skip, take) {

        if (skip == undefined || take == undefined) {
            self.Skip(0);
            self.Take(pagingSettings.pageSize);
        }
        else {
            self.Skip(skip);
            self.Take(take);
        }
        $("#frmReferralReceivedCount").submit();

    };

    var pagingSettings = {
        pageSize: 5,
        pageSlide: 2
    };

    self.Skip = ko.observable(0);
    self.Take = ko.observable(pagingSettings.pageSize);

    self.Pager = ko.pager(self.TotalItemCount);
    self.Pager().PageSize(pagingSettings.pageSize);
    self.Pager().PageSlide(pagingSettings.pageSlide);
    self.Pager().CurrentPage(1);


    self.Pager().CurrentPage.subscribe(function () {

        var skip = pagingSettings.pageSize * (self.Pager().CurrentPage() - 1);
        var take = pagingSettings.pageSize;
        self.GetReferralRecivedWithSkipTake(skip, take);

    });

};