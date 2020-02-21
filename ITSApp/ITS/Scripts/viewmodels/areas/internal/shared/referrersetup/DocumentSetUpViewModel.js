/*
*  Latest Version  : 1.0


*  Auther       : Anuj Batra
*  Version      : 1.0
*  Date         : 06-Dec-2012
*  Description  : Created view model for Document Set up.
*/

function Documents(assessmentServiceID, documentSetupTypeID, referrerProjectTreatmentID, assessmentServiceName, documentSetupTypes, referrerProjectTreatmentDocumentSetupID) {
    var self = this;
    self.ReferrerProjectTreatmentDocumentSetupID = referrerProjectTreatmentDocumentSetupID;
    self.AssessmentServiceID = ko.observable(assessmentServiceID);
    self.DocumentSetupTypeID = ko.observable(documentSetupTypeID);
    self.ReferrerProjectTreatmentID = ko.observable(referrerProjectTreatmentID);
    self.AssessmentServiceName = ko.observable(assessmentServiceName);
    self.DocumentSetupTypes = ko.observableArray(documentSetupTypes);
    self.controlName = ko.computed(function () {
        return self.AssessmentServiceName()+ "ForDocuments";
    });
}

function DocumentSetUpViewModel() {
    var self = this;
    self.ReferrerProjectTreatmentID = ko.observable('');
    self.notAvailable = ko.observable(false);
    self.DocumentSetUpTypes = [{ TypeID: 1, DocumentSetupTypeName: "Default" }, { TypeID: 2, DocumentSetupTypeName: "Custom"}];
    self.AssessmentForDocuments = ko.observableArray([]);
    self.ViewEnabled = ko.observable();
    this.InitializeDocumentSetUp = function (referrerProjectTreatment) {
        self.ReferrerProjectTreatmentID(referrerProjectTreatment.ReferrerProjectTreatmentID);
        self.AssessmentForDocuments.removeAll()
        this.GetAllService();
    };
    this.EnableDisable = function (flag) {
        self.ViewEnabled(flag);

    };
    this.GetAllService = function () {

        $.ajax({
            url: "/ReferrerDocumentSetup/GetAllReferrerProjectTreatmentDocumentsByReferrerProjectTreatmentID/",
            type: 'post',
            cache: false,
            dataType: 'json',
            data: { referrerProjectTreatmentID: self.ReferrerProjectTreatmentID() },
            success: function (services) {
                
                $.each(services, function (index, value) {
                    self.AssessmentForDocuments.push(new Documents(services[index].AssessmentServiceID, services[index].DocumentSetupTypeID, self.ReferrerProjectTreatmentID(), services[index].AssessmentServiceName, self.DocumentSetUpTypes, services[index].ReferrerProjectTreatmentDocumentSetupID));
                });
            }
        });
    };


    this.updateAssessment = function () {
        $.ajax({
            url: "/ReferrerDocumentSetup/UpdateReferrerProjectTreatmentDocumentSetUp/",
            type: 'post',
            data: ko.toJSON(self.AssessmentForDocuments()),
            contentType: 'application/json',
            success: function (result) {
                alert(result);
            }
        });
    };
};
