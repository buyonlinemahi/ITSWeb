/*
*  Latest Version  : 1.4


*  Author  :    : Manjit Singh
*  Version      : 1.0
*  Date         : 22-Nov-2012
*  Description  : Added viewModel to Implement Assessment Services tab

*  Modified by  : Manjit Singh
*  Version      : 1.1
*  Date         : 01-Dec-2012
*  Description  : Changed viewModel to Implement for both type of Assessment Services

*  Modified by  : Anuj Batra
*  Version      : 1.2
*  Date         : 04-Dec-2012
*  Description  : Changed the view model to add the assessment services.

*  Modified By  : Manjit Singh
*  Date         : 10-Dec-2012
*  Version      : 1.3
*  Description  : Changed viewModel to Implement Visibility of Buttons

*  Modified By  : Manjit Singh
*  Date         : 11-Dec-2012
*  Version      : 1.4
*  Description  : Changed viewModel to Implement update Assessment Services
*/

function AsessmentService(assessmentServiceID, assessmentTypeID, referrerProjectTreatmentID, assessmentServiceName, assessmentTypes) {
    var self = this;
    self.AssessmentServiceID = ko.observable(assessmentServiceID);
    self.AssessmentTypeID = ko.observable(assessmentTypeID);
    self.ReferrerProjectTreatmentID = ko.observable(referrerProjectTreatmentID);
    self.AssessmentServiceName = ko.observable(assessmentServiceName);
    self.AssessmentTypes = ko.observableArray(assessmentTypes);
}

function AssessementViewModel() {
    var self = this;
    self.ReferrerProjectTreatmentID = ko.observable('');
    self.SaveAvailability = ko.observable(false);
    self.UpdateAvailability = ko.observable(false);
    self.SaveVisible = ko.observable(false);
    self.UpdateVisible = ko.observable(false);
    self.AmendVisible = ko.observable(true);
    self.CancelVisible = ko.observable(false);
    self.ViewEnabled = ko.observable();
    self.AssessmentTypes = [{ TypeID: 1, AssessmentTypeName: "Default" }, { TypeID: 2, AssessmentTypeName: "Custom"}];
    self.Assessments = ko.observableArray([]);
    self.ClearValue = function () {
        self.ReferrerProjectTreatmentID(null);
    };

    this.Initialize = function (referrerProjectTreatment) {
        self.ReferrerProjectTreatmentID(referrerProjectTreatment.ReferrerProjectTreatmentID);
        self.Assessments.destroyAll()
        self.GetAllService();

    };
    this.EnableDisable = function (flag) {
        self.ViewEnabled(flag);

    };
    this.GetAllService = function () {
        $("#divProjectSpin").spin(true);
        $.ajax({
            url: "/ReferrerAssesmentService/GetAllAssessmentService/",
            type: 'post',
            cache: false,
            dataType: 'json',
            data: { referrerProjectTreatmentID: self.ReferrerProjectTreatmentID() },
            success: function (services) {
                $.each(services, function (index, value) {
                    self.Assessments.push(new AsessmentService(services[index].AssessmentServiceID, services[index].AssessmentTypeID, self.ReferrerProjectTreatmentID(), services[index].AssessmentServiceName, self.AssessmentTypes));
                });
                $("#divProjectSpin").spin(false);
            },
            error: function () {
                $("#divProjectSpin").spin(false);
            }
        });
    };


    this.UpdateAssessment = function () {
        $.ajax({
            url: "/ReferrerAssesmentService/UpdateReferrerProjectTreatmentAssessment/",
            type: 'post',
            data: ko.toJSON(self.Assessments()),
            contentType: 'application/json',
            success: function (result) {
                alert(result);
            }
        });
    };
};

