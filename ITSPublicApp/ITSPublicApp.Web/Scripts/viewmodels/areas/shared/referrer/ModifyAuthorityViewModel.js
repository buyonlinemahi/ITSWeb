
function ModifyAuthorityViewModel() {
   
    var self = this;
    ko.validation.configure({
        insertMessages: false,
        grouping: { deep: true }
    });
    self.caseID = ko.observable();
    self.EncryptedCaseID = ko.observable();
    self.DownloadPath = ko.observable();
   
    self.IsCustom = ko.observable();
    self.CaseNumber = ko.observable();
    self.modifyTextArea = ko.observable().extend({ required: { message: "Modification Text is required."} });
    self.ReferralDownloadPath = ko.observable();
    self.errors = ko.validation.group(self);
    self.CaseAssessmentModifications = ko.observableArray([]);
    self.Type = ko.observable();
    self.QTY = ko.observable();
    self.Status = ko.observable();
 
    
    this.Init = function (Model) {
        self.caseID(Model.CaseID);
        self.EncryptedCaseID(Model.EncryptedCaseID);
        self.CaseNumber(Model.CaseNumber);
        self.ReferralDownloadPath('/File/ViewReferral/?caseID=' + self.EncryptedCaseID());
        self.DownloadPath(Model.DownloadPath);
        self.IsCustom(Model.cases.IsCustom);
        GridBinding();
    };

    this.SubmitClick = function () {
        var errors = "Errors ";
        if (!self.isValid()) {
            $.each(self.errors(), function (index, value) {
                errors = errors + ' ' + value();
            });
            alert(errors);
            return false;
        }

        $("#loader-main-div").removeClass("hidden");
        var jsonData = JSON.stringify({
            caseID: self.caseID(),
            authorisationModification: self.modifyTextArea(),
            treatmentSession: '0', 
            assessmentServiceID: '0' 
        });
                $.ajax({
                    url: "/Referrer/ModifyAuthority/UpdateCaseAssessmentAuthorisationToModifiedByCaseID",
                    type: "POST",
                    contentType: 'application/json',
                    dataType: "json",
                    data: jsonData,
                    success: function (result) {
                        alert(result);
                        $("#loader-main-div").addClass("hidden");
                        window.location.href = '/Referrer/Home/Index/';
                    },
                    error: function () {
                        $("#loader-main-div").addClass("hidden");
                    }
                });

    };


    //**********  Modifiy Page Grid Binding*****************///
 function GridBinding() {
     $.post("/Referrer/ModifyAuthority/GetReferrerCaseModificatinTreatments", { CaseID: self.EncryptedCaseID() }, function (data) {
            $.each(data, function (index, value) {
                self.CaseAssessmentModifications.push(new CaseAssessmentModificationsDetails(value));
            });

           
        });
        self.CaseAssessmentModifications();
      
 };

 function CaseAssessmentModificationsDetails(value) {
     var self = this;
     self.Type = ko.observable(value.Type);
     self.QTY = ko.observable(value.QTY);
     self.Status = ko.observable(value.Status);
    
 };
    //**************** End ********************//


   


}
