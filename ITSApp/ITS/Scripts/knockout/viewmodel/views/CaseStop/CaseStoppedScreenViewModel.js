function CaseStoppedScreenViewModel() {
    var self = this;

    self.CaseId = ko.observable();
    self.CaseStopReasons = ko.observableArray();

    self.UpdateFinalAssessmentQASucess = function () {
        $("#loader-main-div").addClass("hidden");
    }

    this.Init = function (caseId) {
        self.CaseId(caseId);
        ko.mapping.fromJS(model, {}, self);
    };
    self.CaseStoppedScreenQAFormBeforeSubmit = function ()
    {
        $("#loader-main-div").removeClass("hidden");
    }

};