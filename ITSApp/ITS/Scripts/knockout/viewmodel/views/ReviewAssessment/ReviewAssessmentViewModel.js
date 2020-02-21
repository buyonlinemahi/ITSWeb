function ReviewAssessmentGridViewModel() {
    var self = this;

    self.CaseWorkflowPatientReferrerProjects = ko.observableArray([]);
    self.TreatmentCategoryID = ko.observable();
    self.CaseWorkflowPatientReferrerProjectTotalCount = ko.observable();

    self.TotalItemCount = ko.computed(function () {
        return self.CaseWorkflowPatientReferrerProjectTotalCount();
    });

    var mapping = {
        'CaseReferrerDueDate': {
            create: function (options) { return moment(options.data).format("DD/MM/YYYY"); }
        },
        'CaseSubmittedDate': {
            create: function (options) { return moment(options.data).format("DD/MM/YYYY"); }
        }
    };

    self.Init = function (model) {
        if (model != null) {
            if (self.TreatmentCategoryID() != model.TreatmentCategoryID) {
                self.Pager().CurrentPage(1);
            }
            ko.mapping.fromJS(model, mapping, self);
        }
    };

    this.UpdateGrid = function (responseText, statusText, xhr, $form) {
        ko.mapping.fromJS($.parseJSON(responseText), mapping, self);

    };

    self.TreatmentCategoryIDChange = function () {
        $("#frmReviewAssessment").submit();
    };

    self.GetReviewAssessmentWithSkipTake = function (skip, take) {

        if (skip == undefined || take == undefined) {
            self.Skip(0);
            self.Take(pagingSettings.pageSize);
        }
        else {
            self.Skip(skip);
            self.Take(take);
        }
        $("#frmReviewAssessment").submit();

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
        self.GetReviewAssessmentWithSkipTake(skip, take);
    });

};