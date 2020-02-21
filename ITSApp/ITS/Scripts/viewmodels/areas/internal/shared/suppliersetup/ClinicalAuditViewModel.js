
/* 

Latest Version 1.0

Author          : Pardeep Kumar
Date            : 01-Jan-2013
Version         : 1.0
Purpose         : File added for the  Supplier Clinical Audit 

*/


// function to generate SupplierClinicalAudit Grid
function SupplierClinicalAudit(caseID, auditDate, supplierClinicalAuditID, auditPass, supplierDocumentID, supplierID, userID, userName, caseNumber, uploadPath, documentName, documentUrl) {
    var self = this;
    self.CaseID = ko.observable(caseID);
    self.AuditDate = ko.observable(auditDate);
    self.SupplierClinicalAuditID = ko.observable(supplierClinicalAuditID);
    self.DocumentName = ko.observable(documentName);
    self.SupplierDocumentID = ko.observable(supplierDocumentID);
    self.SupplierID = ko.observable(supplierID);
    self.UserID = ko.observable(userID);
    self.UserName = ko.observable(userName);
    self.CaseNumber = ko.observable(caseNumber);
    self.UploadPath = ko.observable(uploadPath);
    self.DocumentUrl = ko.observable(documentUrl);

    if (auditPass == 1) {

        self.AuditPass = ko.observable("Pass");
    }
    else {

        self.AuditPass = ko.observable("Fail");
    }
}


function SupplierClinicalAuditViewModel() {
    var self = this;

    // Clinical Audit Array Variable
    self.SupplierClinicalAuditArray = ko.observableArray([]);
    // Model variables
    self.UploadDate = ko.observable(new Date());
    self.DocumentUrl = ko.observable();
    self.UserID = ko.observable();
    self.SupplierID = ko.observable();
    self.AuditPass = ko.observable(false);
    self.AuditDate = ko.observable();
    self.UploadPath = ko.observable();
    self.DocumentName = ko.observable();
    self.DocumentTypeID = ko.observable(4);
    self.SupplierClinicalAuditID = ko.observable();
    self.CaseNumber = ko.observable();
    self.CaseID = ko.observable();
    self.SupplierDocumentID = ko.observable();
    self.UserName = ko.observable();

    // User Variables
    self.UserTypeId = ko.observable(3); //for userType "Internal"
    self.Users = ko.observableArray([]);

    self.PatientName = ko.observable();
    // Status Variable
    self.AuditPassFail = ko.observable();

    // additional Variables
    self.OldUploadFileName = ko.observable();
    self.SaveUpdateText = ko.observable("Save");
    //self.ClinicalAuditFileCondition = ko.observable("Add");

    // Action Url Variable
    self.SupplierClinicalActionUrl = ko.observable();
    // method variable to include the Action Url
    this.GetActionUrl = function () {
        return self.SupplierClinicalActionUrl();
    }

    // all Url Variables
    self.UrlGetUserByUserTypeID;
    self.UrlGetSupplierClinicalAuditBySupplierID;
    self.UrlUpdateSupplierClinicalAuditBySupplierClinicalAuditID;
    self.UrlAddSupplierClinicalAudit;
    self.UrlDeleteSupplierClinicalAuditBySupplierClinicalAuditID;
    self.UrlGetPatientByPatientId;
    self.UrlGetPatientNameByCaseId;

    // Initialize all the Url Variables from Global Constants
    this.InitializeClinicalUrls = function (values) {
        self.UrlGetUserByUserTypeID = values.UrlGetUserByUserTypeID;
        self.UrlGetSupplierClinicalAuditBySupplierID = values.UrlGetSupplierClinicalAuditBySupplierID;
        self.UrlUpdateSupplierClinicalAuditBySupplierClinicalAuditID = values.UrlUpdateSupplierClinicalAuditBySupplierClinicalAuditID;
        self.UrlAddSupplierClinicalAudit = values.UrlAddSupplierClinicalAudit;
        self.UrlDeleteSupplierClinicalAuditBySupplierClinicalAuditID = values.UrlDeleteSupplierClinicalAuditBySupplierClinicalAuditID;
        self.UrlGetPatientByPatientId = values.UrlGetPatientByPatientId;

        self.UrlGetPatientNameByCaseId = values.UrlGetPatientNameByCaseId;
    };

    // variable to accept the Error messages from the Global Resx file
    self.PleaseEnterDocumentName;
    self.PleaseSelectFile;
    self.PleaseEnterAuditeDate;
    self.PleaseChooseAuditor;
    self.PassFailStatus;

    // function to initialize the Error Message from the Global Error Resx File
    this.InitializeClinicalErrorMsg = function (msg) {
        self.PleaseEnterDocumentName = msg.PleaseEnterDocumentName;
        self.PleaseSelectFile = msg.PleaseSelectFile; ;
        self.PleaseEnterAuditeDate = msg.PleaseEnterAuditeDate;
        self.PleaseChooseAuditor = msg.PleaseChooseAuditor;
        self.PassFailStatus = msg.PassFailStatus;
    };

    //function to show the message after the save button is clicked
    this.showFileUploadMessage = function (msg) {
        self.clearModel();
        self.GetSupplierClinicalAuditBySupplierID();
        $("#modelPopSupplierClinicalAudit").dialog("close");
        alert(msg);
    }


    // Function to remove File and Remove SupplierClinicalAudit
    this.RemoveClinicalAudit = function (item) {
        $.ajax({
            url: self.UrlDeleteSupplierClinicalAuditBySupplierClinicalAuditID,
            type: "post",
            cache: false,
            datatype: 'application/json',
            data: { supplierClinicalAuditID: item.SupplierClinicalAuditID(), supplierDocumentID: item.SupplierDocumentID(), fileToDelete: item.UploadPath(), SupplierId: item.SupplierID() },
            success: function (data) {
                alert(data);
                self.GetSupplierClinicalAuditBySupplierID();
            }
        });
    }
    // Function to Bind all the users with specific typeId [Here UserType is Internal]
    this.BindUsers = function () {
        $.ajax({
            url: self.UrlGetUserByUserTypeID,
            type: 'post',
            cache: false,
            datatype: 'application/json',
            data: { userTypeID: 1 },
            success: function (data) {
                self.Users(data);
            }
        });
    }
    // Function to show the patientName after selecting CaseNumber
    this.UpdatePatientName = function (model) {
        self.PatientName(model);
    };

    // function to clear the value of input type is file
    this.clearFileInInput = function () {
        //For IE
        $("#ClinicalAuditDocumentFileUpload").replaceWith($("#ClinicalAuditDocumentFileUpload").clone(true));
        //For other browsers
        $("#ClinicalAuditDocumentFileUpload").val("");
    }


    // Show the Clinical ModelPopUp for adding new Record
    this.showClinicModal = function () {
        self.SupplierClinicalActionUrl(self.UrlAddSupplierClinicalAudit);
        self.clearFileInInput();
        self.clearModel();
        self.OldUploadFileName(null);
        self.SaveUpdateText("Save");
        self.SupplierClinicalAuditID(0);
        $("#modelPopSupplierClinicalAudit").dialog("open");
    };

    this.InitializClinicalAuditTab = function (data) {
        self.SupplierID(data.SupplierID);
    };
    // Initial Method which bind the SupplierClinicalAudit Grid
    this.LoadTab = function () {
        self.BindUsers();
        self.GetSupplierClinicalAuditBySupplierID();
    };

    this.GetSupplierClinicalAuditBySupplierID = function () {
        self.SupplierClinicalAuditArray([]);
        $('#divClinicalSpinner').spin(true);
        $.ajax({
            url: self.UrlGetSupplierClinicalAuditBySupplierID,
            type: 'post',
            cache: false,
            datatype: 'application/json',
            data: { supplierId: self.SupplierID() },
            success: function (data) {
                $.each(data, function (index, value) {
                    ko.utils.arrayForEach(self.Users(), function (item) {
                        if (item.UserID === value.UserID) {
                            value.UserName = item.UserName;
                        }
                    });

                    self.SupplierClinicalAuditArray.push(new SupplierClinicalAudit
                   (value.CaseID, value.AuditDate, value.SupplierClinicalAuditID, value.AuditPass,
                   value.SupplierDocumentID, value.SupplierID, value.UserID, value.UserName, value.CaseNumber, value.UploadPath, value.DocumentName, value.DocumentUrl));
                });
                $('#divClinicalSpinner').spin(false);
            },
            error: function () {
                $('#divClinicalSpinner').spin(false);
            }

        });
    }

    self.AuditPassFail.subscribe(function (value) {
        if (value === "Pass") {
            self.AuditPass(true);
        } else {
            self.AuditPass(false);
        }
    });

    // Function to clear Previous Records in ModelPopUp
    this.clearModel = function () {
        self.UserID(null);
        this.BindUsers();
        self.SupplierDocumentID(null);
        self.PatientName(null);
        self.AuditPassFail("Fail");
        self.DocumentName(null);
        self.AuditDate(null);

        self.CaseID(null);
        self.PatientName(null);
        self.CaseNumber('');


        self.UploadPath(null);
        self.UploadPath('');
    };

    // Function to ammend the single selected record
    this.AmmendClinicalAudit = function (item) {
        self.SupplierClinicalActionUrl(self.UrlUpdateSupplierClinicalAuditBySupplierClinicalAuditID);
        self.clearFileInInput();
        self.SupplierClinicalAuditID(item.SupplierClinicalAuditID());
        $("#modelPopSupplierClinicalAudit").dialog("open");
        self.CaseNumber(item.CaseNumber());
        self.AuditDate(item.AuditDate())
        self.DocumentName(item.DocumentName());
        self.AuditPassFail(item.AuditPass());
        self.UserID(item.UserID());

        self.OldUploadFileName(item.UploadPath());
        self.SaveUpdateText("Update");
        self.CaseID(item.CaseID());

        var gotDate = "";
        var prepairedDate = "";

        gotDate = item.AuditDate().split('/');
        prepairedDate = gotDate[2] + "-" + gotDate[0] + "-" + gotDate[1];
        self.AuditDate(prepairedDate);
        self.SupplierDocumentID(item.SupplierDocumentID());
        self.SupplierID(item.SupplierID());
        self.UserID(item.UserID());

        self.UploadPath('');

        $.ajax({
            url: self.UrlGetPatientNameByCaseId,
            cache: false,
            type: "POST", dataType: "json",
            data: { caseId: item.CaseID() },
            success: function (data) {
                self.UpdatePatientName(data);
            }
        });
    }

    // Function to close the ModelPopUp
    this.Cancel = function () {
        $("#modelPopSupplierClinicalAudit").dialog("close");
    }

    // form validation
    this.ClinicalFormValidations = function () {
        var errMsg = "";
        var formValidationStatus = true;

        var fileUpload = $("#ClinicalAuditDocumentFileUpload").val();

        if (self.CaseNumber() == null || self.CaseNumber() === '') {
            errMsg = " Please enter ITS Reference Number ";
            formValidationStatus = false;
        }
        else if (self.AuditPassFail() == null) {
            errMsg = self.PassFailStatus;
            formValidationStatus = true;
        }
        else if (self.UserID() == null) {
            errMsg = self.PleaseChooseAuditor;
            formValidationStatus = false;
        }
        else if (self.AuditDate() == null) {
            errMsg = self.PleaseEnterAuditeDate;
            formValidationStatus = false;
        }

        else if (self.DocumentName() == null) {
            errMsg = self.PleaseEnterDocumentName;
            formValidationStatus = false;
        }

        else if (fileUpload == null || fileUpload.length <= 0) {
            if (self.SupplierClinicalAuditID() > 0) {
                formValidationStatus = true;
            }
            else {
                errMsg = self.PleaseSelectFile;
                formValidationStatus = false;
            }
        }
        else if (fileUpload != null || fileUpload.length > 0) {
            var extensions = fileUpload.split('.');
            if (extensions[1].toUpperCase() === "PDF") {
                formValidationStatus = true;

            } else {
                errMsg = "Please select Pdf File Only ";
                formValidationStatus = false;
            }
        }
        else {
            formValidationStatus = true;
        }

        if (errMsg === '') {
            formValidationStatus = true;
        }
        else {
            alert(errMsg);
        }

        return formValidationStatus;
    };
}