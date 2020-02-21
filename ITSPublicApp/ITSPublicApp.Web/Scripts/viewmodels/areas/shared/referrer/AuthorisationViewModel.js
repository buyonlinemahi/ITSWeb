function ReferrerAuthorisation(caseID, patientID, patientFirstName, patientLastName, referrerID, caseNumber, caseReferrerReferenceNumber, caseReferrerDueDate, supplierID, workflowID, treatmentTypeName, IsCustom, UrlPath, IsFinalVersionUpload, EncryptedCaseID, AssessmentServiceID) {
    var self = this;
    self.CaseID = ko.observable(caseID);
    self.AssessmentServiceID = ko.observable(AssessmentServiceID);
    self.PatientID = ko.observable(patientID);
    self.PatientName = ko.observable();
    self.FirstName = ko.observable(patientFirstName);
    self.LastName = ko.observable(patientLastName);
    self.ReferrerID = ko.observable(referrerID);
    self.CurrentReportType = ko.observable();
    self.CaseNumber = ko.observable(caseNumber);
    self.CurrentReportTypeHead = ko.observable();
    self.TreatmentTypeName = ko.observable(treatmentTypeName);
    self.CaseReferrerReferenceNumber = ko.observable(caseReferrerReferenceNumber);
    self.CaseReferrerDueDate = ko.observable(caseReferrerDueDate).extend({ formatDate: 'DD/MM/YYYY' });
    self.SupplierID = ko.observable(supplierID);
    self.WorkflowID = ko.observable(workflowID);
    self.IsCustom = ko.observable(IsCustom);    
    self.IsFinalVersionUpload = ko.observable(IsFinalVersionUpload);
    self.UrlPath = ko.observable(UrlPath);
    self.EncryptedCaseID = ko.observable(EncryptedCaseID);
    self.StaticWorkflowIDsArray = ({ "Initial Assessment": 90, "Review Assessment": 180, "Review Assessment Report": 150, "Initial Assessment Custom": 92, "Review Assessment Custom": 182, "Review Assessment Report Custom": 152 });

    self.PatientName = ko.computed(function () {
        return self.FirstName() + " " + self.LastName();
    });

    self.CurrentReportType = ko.computed(function () {
        if ((self.WorkflowID() == self.StaticWorkflowIDsArray["Initial Assessment"]) || (self.WorkflowID() == self.StaticWorkflowIDsArray["Initial Assessment Custom"]))
            return "Initial Assessment Report Submitted";
        else if ((self.WorkflowID() == self.StaticWorkflowIDsArray["Review Assessment"]) || (self.WorkflowID() == self.StaticWorkflowIDsArray["Review Assessment Custom"]))
            return "Review Assessment";
        else if ((self.WorkflowID() == self.StaticWorkflowIDsArray["Review Assessment Report"]) || (self.WorkflowID() == self.StaticWorkflowIDsArray["Review Assessment Report Custom"]))
            return "Review Assessment Report Submitted";
    });
    self.CurrentReportTypeHead = ko.computed(function () {
        if ((self.WorkflowID() == self.StaticWorkflowIDsArray["Initial Assessment"]) || (self.WorkflowID() == self.StaticWorkflowIDsArray["Initial Assessment Custom"]))
            return "Initial Assessment Report Submitted";
        else if ((self.WorkflowID() == self.StaticWorkflowIDsArray["Review Assessment"]) || (self.WorkflowID() == self.StaticWorkflowIDsArray["Review Assessment Custom"]))
            return "Review Assessment";
        else if ((self.WorkflowID() == self.StaticWorkflowIDsArray["Review Assessment Report"]) || (self.WorkflowID() == self.StaticWorkflowIDsArray["Review Assessment Report Custom"]))
            return "Review Assessment";
    });
};


function AuthorisationViewModel() {
    var self = this;

    self.ReferrerID = ko.observable();
    self.IsCustom = ko.observable();
    self.SupplierID = ko.observable();
    self.WorkflowID = ko.observable();
    self.CaseID = ko.observable();
    self.AssessmentServiceID = ko.observable();
    self.UrlPath = ko.observable();
    self.EncryptedCaseID = ko.observable();
    self.GridMessage = ko.observable();
    self.StaticWorkflowIDForAuthorisationSentToInovate = ko.observable(100);
    self.ReferrerAuthorisations = ko.observableArray([]);
    self.CaseAssessmentModifications = ko.observableArray([]);
    this.Initialize = function (model) {
        self.ProcessModel(model);
    };

    this.GetReferrerCasesByWorkflowID = function (skip, take) {
        $('#divGridDisplaySpinner').spin(true);
        if (skip == undefined || take == undefined) {
            skip = 0;
            take = pagingSettings.pageSize;
        }

        $.ajax({
            url: "/Referrer/Home/GetReferrerAuthorisations/",
            cache: false,
            type: "POST",
            dataType: "json",
            data: { skip: skip, take: take },
            success: function (model) {
                self.GridMessage();
                self.ReferrerAuthorisations([]);
                self.ProcessModel(model);
                //alert("this");

                $('#divGridDisplaySpinner').spin(false);
            },
            error: function () {
                $('#divGridDisplaySpinner').spin(false);
            }
        });

    };

    this.ProcessModel = function (model) {
        $('#divGridDisplaySpinner').spin(true);
        if (model.ReferrerAuthorisationTotalCount == 0) {
            self.GridMessage("There are no current cases awaiting authorisation");
            $('#divGridDisplaySpinner').spin(false);
        }
        else {
            self.TotalItemCount(model.ReferrerAuthorisationTotalCount);
            $.each(model.ReferrerAuthorisations, function (index, value) {
                self.ReferrerAuthorisations.push(
                            new ReferrerAuthorisation(value.CaseID,
                                                    value.PatientID,
                                                    value.FirstName,
                                                    value.LastName,
                                                    self.ReferrerID(),
                                                    value.CaseNumber,
                                                    value.CaseReferrerReferenceNumber,
                                                    self.dateFormat(value.CaseReferrerDueDate),
                                                    value.SupplierID,
                                                    value.WorkflowID,
                                                    value.TreatmentTypeName,
                                                    value.IsCustom,
                                                    value.UrlPath,
                                                    value.IsFinalVersionUpload, value.EncryptedCaseID,
                    value.AssessmentServiceID
                    ));
            });
            $('#divGridDisplaySpinner').spin(false);
        }
    };

    this.ModifyAuthority = function () {
        window.location.href = '/Referrer/ModifyAuthority/Index/' + this.EncryptedCaseID();
    };
    this.CaseStopped = function () {
        window.location.href = '/Referrer/CaseStopped/Index/' + this.EncryptedCaseID();
    };

    this.Approve = function (Case) {      
        if (confirm("Are you sure you want to approve?")) {
            Case.WorkflowID(self.StaticWorkflowIDForAuthorisationSentToInovate());
            $.post("/Referrer/Home/ApproveAuthorisationtoInnovate", { CaseID: $("#hidCaseID").val() }, function (result) {
                alert(result);
                self.GetReferrerCasesByWorkflowID();
                self.Pager().CurrentPage(1);
            });
        }
    };


    this.OpenTreatmentMatrix = function () {
        $("#hidCaseID").val(this.EncryptedCaseID());
        $.post("/Referrer/ModifyAuthority/GetReferrerCaseModificatinTreatments", { CaseID: this.EncryptedCaseID() }, function (data) {
            self.CaseAssessmentModifications.removeAll();
            $.each(data, function (index, value) {
                self.CaseAssessmentModifications.push(new CaseAssessmentModificationsDetails(value));
            });


        });
    };
    this.ViewAssessment = function (Assesment) {

        $("#divGridDisplaySpinner").spin();
        var hidcontenttype = $("#hidcontenttype").val();
        var rptType = Assesment.CurrentReportType();
        var mywindow;
        mywindow = window.open("/Referrer/Home/ViewAssessment?id=" + this.EncryptedCaseID() + "&data=" + rptType, '_blank');
        $("#divGridDisplaySpinner").spin(false);
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
        self.GetReferrerCasesByWorkflowID(skip, take);
    });

    function CaseAssessmentModificationsDetails(value) {
        var self = this;
        self.Type = ko.observable(value.Type);
        self.QTY = ko.observable(value.QTY);
        self.Status = ko.observable(value.Status);

    };
};
