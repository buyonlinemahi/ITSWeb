/* Latest Version 1.8


Author          : Manjit Singh
Date            : 26-Dec-2012
Version         : 1.0
Purpose         : File added for the  Supplier Site Audit 

Updated By      : Manjit Singh
Date            : 27-Dec-2012
Version         : 1.1
Purpose         : Functionality to get Supplier Site Audit and popup for add/amend 

Updated By      : Manjit Singh
Date            : 29-Dec-2012
Purpose         : Functionality to delete and update Supplier Site Audit
Version         : 1.2

Updated By      : Manjit Singh
Date            : 3-Jan-2013
Version         : 1.3
Purpose         : Functionality to get users by user type ID

Updated By      : Manjit Singh
Date            : 7-Jan-2013
Version         : 1.4
Purpose         : Model changed (View Upload)

Updated By      : Manjit Singh
Date            : 8-Jan-2013
Version         : 1.5
Purpose         : validations for upload ducument and changed technique to display Pass Fail

Updated By      : Manjit Singh
Date            : 14-Jan-2013
Version         : 1.6
Purpose         : Applied Calender Control for Date and display date changed for grid

Updated By      : Manjit Singh
Date            : 15-Jan-2013
Version         : 1.7
Purpose         : functionality changed to view file and upload file

Updated By      : Manjit Singh
Date            : 19-Feb-2013
Version         : 1.8
Purpose         : functionality changed to add, update, upload file and validations

*/
function SiteAudits(supplierSiteAuditID, auditNotes, auditPass, userID, userName, auditDate, supplierID, supplierDocumentID, uploadPath, documentName, documentUrl, uploadDateString) {

    var self = this;
    self.SupplierSiteAuditID = ko.observable(supplierSiteAuditID);
    self.AuditNotes = ko.observable(auditNotes);
    self.AuditPass = ko.observable(auditPass);
    self.UserID = ko.observable(userID);
    self.AuditDate = ko.observable(auditDate);
    self.SupplierID = ko.observable(supplierID);
    self.SupplierDocumentID = ko.observable(supplierDocumentID);
    self.UserName = ko.observable(userName);
    self.DocumentName = ko.observable(documentName);
    self.UploadPath = ko.observable(uploadPath);
    self.DocumentUrl = ko.observable(documentUrl);
    self.UploadDate = ko.observable(uploadDateString);

};
function Auditors(userId, userName) {
    var self = this;
    self.UserName = ko.observable(userName);
    self.UserID = ko.observable(userId);
}
function SupplierSiteAuditViewModel() {

    var self = this;
    self.SupplierSiteAuditID = ko.observable();
    self.AuditNotes = ko.observable();
    self.AuditPass = ko.observable();
    self.UserID = ko.observable();
    self.UserName = ko.observable();
    self.AuditDate = ko.observable();
    self.DocumentName = ko.observable();
    self.UploadPath = ko.observable();
    self.SupplierSiteAuditArray = ko.observableArray([]);
    self.SupplierDocumentID = ko.observable();
    self.SupplierID = ko.observable();
    self.SupplierDocumentID = ko.observable();
    self.DocumentTypeID = ko.observable(3);
    self.UploadDate = ko.observable(new Date());
    self.UserTypeId = ko.observable(1); //for userType "Internal"
    self.Users = ko.observableArray([]);
    self.IsSaveVisible = ko.observable(true);
    self.IsUpdateVisible = ko.observable(false);
    self.PassFail = ko.observable();
    self.OldUploadPath = ko.observable();
    self.SiteAuditActionUrl = ko.observable();
    self.DocumentUrl = ko.observable();
    self.UrlGetSupplierSiteAuditBySupplierID;
    self.UrlGetUsersByUserTypeID;
    self.UrlAddSupplierSiteAudit;
    self.UrlDeleteSupplierSiteAuditBySupplierSiteAuditID;
    self.UrlUpdateSupplierSiteAuditBySupplierSiteAuditID;
    self.SiteAuditDateEnable = "readonly";
    self.PassFail.subscribe(function (value) {
        if (value == "1") {
            self.AuditPass("true");
        } else if (value == "0") {
            self.AuditPass("false");
        }
    });
    this.InitializSiteAuditTab = function (data) {
        self.SupplierID(data.SupplierID);

    };

    this.LoadTab = function () {
        self.GetUsersByUserTypeID();
        self.GetSupplierSiteAuditBySupplierID();
    };

    this.InitializeSiteAuditUrls = function (values) {
        self.UrlGetUsersByUserTypeID = values.UrlGetUsersByUserTypeID;
        self.UrlGetSupplierSiteAuditBySupplierID = values.UrlGetSupplierSiteAuditBySupplierID;
        self.UrlAddSupplierSiteAudit = values.UrlAddSupplierSiteAudit;
        self.UrlDeleteSupplierSiteAuditBySupplierSiteAuditID = values.UrlDeleteSupplierSiteAuditBySupplierSiteAuditID;
        self.UrlUpdateSupplierSiteAuditBySupplierSiteAuditID = values.UrlUpdateSupplierSiteAuditBySupplierSiteAuditID;
    };
    self.PleaseEnterSupplierNotes;
    self.PleaseEnterDocumentName;
    self.PleaseSelectFile;
    self.PleaseEnterAuditeDate;
    self.PleaseChooseAuditor;
    self.PassFailStatus;
    self.PleaseBrowsePDFFileOnly;
//    this.InitializeSiteAuditErrorMsg = function (msg) {
//        self.PleaseEnterSupplierNotes = msg.PleaseEnterSupplierNotes;
//        self.PleaseEnterDocumentName = msg.PleaseEnterDocumentName;
//        self.PleaseSelectFile = msg.PleaseSelectFile; ;
//        self.PleaseEnterAuditeDate = msg.PleaseEnterAuditeDate;
//        self.PleaseChooseAuditor = msg.PleaseChooseAuditor;
//        self.PassFailStatus = msg.PassFailStatus;
//        self.PleaseBrowsePDFFileOnly = msg.PleaseBrowsePDFFileOnly;
//    }
    this.GetUsersByUserTypeID = function () {
        $.ajax({
            url: self.UrlGetUsersByUserTypeID,
            type: 'post',
            cache: false,
            datatype: 'application/json',
            data: { userTypeId: self.UserTypeId() },
            success: function (getuser) {
                self.Users([]);
                $.each(getuser, function (index, value) {
                    self.Users.push(new Auditors(value.UserID, value.UserName));
                });
            }
        });
    };
    this.editItem = function (item) {
        var gotDate = "";
        var prepairedDate = "";
        gotDate = item.AuditDate().split('/');
        prepairedDate = gotDate[2] + "-" + gotDate[0] + "-" + gotDate[1];
        self.UserID(item.UserID());
        self.UserName(item.UserName());
        self.AuditNotes(item.AuditNotes());
        self.PassFail(item.AuditPass());
        self.AuditDate(prepairedDate);
        self.DocumentName(item.DocumentName());
        self.UploadPath(item.UploadPath());
        self.SupplierSiteAuditID(item.SupplierSiteAuditID());
        self.SupplierDocumentID(item.SupplierDocumentID());
        self.UploadDate(item.UploadDate());
        self.OldUploadPath(item.UploadPath());
        self.clearFileInput();
        $("#starDocUpload").css("visibility", "hidden");
        self.IsSaveVisible(false);
        self.IsUpdateVisible(true);
        $("#modelPopSupplierAudit").dialog("open");
        self.SiteAuditActionUrl(self.UrlUpdateSupplierSiteAuditBySupplierSiteAuditID);
    };
    this.clearFileInput = function () {
        //For IE
        $("#fileToUploadForSiteAudit").replaceWith($("#fileToUploadForSiteAudit").clone(true));
        //For other browsers
        $("#fileToUploadForSiteAudit").val("");
    }
    this.ResetAndGetValues = function (message) {
        self.GetSupplierSiteAuditBySupplierID();
        self.clearModel();
        $("#modelPopSupplierAudit").dialog("close");
        alert(message);
    }
    this.validationsSiteAudit = function () {
        var errmsg = "";
        if (self.AuditNotes() == null || self.AuditNotes() == "") {
            errmsg = self.PleaseEnterSupplierNotes;
        }
        else if (self.PassFail() == null) {
            errmsg = self.PassFailStatus;
        }
        else if (self.UserID() == null) {
            errmsg = self.PleaseChooseAuditor;
        }
        else if (self.AuditDate() == null || self.AuditDate() == "") {
            errmsg = self.PleaseEnterAuditeDate;
        }
        else if (self.DocumentName() == null || self.DocumentName() == "") {
            errmsg = self.PleaseEnterDocumentName;
        }
        else if ($('#fileToUploadForSiteAudit').val() != "") 
        {
            var FName = $('#fileToUploadForSiteAudit').val();
            var FNameIndex = FName.split('.');
            if (FNameIndex[1] != "pdf" && FNameIndex[1] != "PDF" &&
            FNameIndex[1] != "doc" && FNameIndex[1] != "DOC" &&
            FNameIndex[1] != "docx" && FNameIndex[1] != "DOCX" &&
            FNameIndex[1] != "jpg" && FNameIndex[1] != "JPG" &&
            FNameIndex[1] != "JPEG" && FNameIndex[1] != "JPEG" &&
            FNameIndex[1] != "tif" && FNameIndex[1] != "TIF" &&
            FNameIndex[1] != "tiff" && FNameIndex[1] != "TIFF"
          ) {
                alert(FNameIndex[1]);
                errmsg = "Select File";
            }
        }
        else if ($('#fileToUploadForSiteAudit').val() == "") {
            if (self.SiteAuditActionUrl() == self.UrlAddSupplierSiteAudit) {
                errmsg = self.PleaseSelectFile;
            }
        }
        if (errmsg != "") {
            alert(errmsg);
            return false;
        }
        else {
            if (self.SiteAuditActionUrl() == self.UrlAddSupplierSiteAudit) {
                self.SiteAuditActionUrl(self.UrlAddSupplierSiteAudit);
            }
            if (self.SiteAuditActionUrl() == self.UrlAddSupplierSiteAudit == self.UrlUpdateSupplierSiteAuditBySupplierSiteAuditID) {
                self.SiteAuditActionUrl(self.UrlUpdateSupplierSiteAuditBySupplierSiteAuditID);
            }
        }
        return true;
    };
    this.clearModel = function () {
        self.IsSaveVisible(true);
        self.IsUpdateVisible(false);
        self.AuditNotes(null);
        self.AuditPass(null);
        self.PassFail(null);
        self.UserID(null);
        self.UserName(null);
        self.AuditDate(null);
        self.DocumentName(null);
        self.UploadPath(null);
        self.SupplierSiteAuditID(null);
        self.SupplierDocumentID(null);
        self.OldUploadPath(null);
        self.clearFileInput();
        $("#starDocUpload").css("visibility", "visible");
    };
    this.Cancel = function () {
        self.clearModel();
        $("#modelPopSupplierAudit").dialog("close");
    };
    this.Remove = function (item) {
        $.ajax({
            url: self.UrlDeleteSupplierSiteAuditBySupplierSiteAuditID,
            cache: false,
            type: "POST", dataType: "json",
            data: { supplierSiteAuditID: item.SupplierSiteAuditID, supplierDocumentID: item.SupplierDocumentID, fileName: item.UploadPath(), supplierID: item.SupplierID },
            success: function (data) {
                alert(data);
                self.GetSupplierSiteAuditBySupplierID();
            },
            error: function (data) {
                alert(data);
            }
        });
    };
    this.showModal = function () {
        self.clearModel();
        self.SiteAuditActionUrl(self.UrlAddSupplierSiteAudit);
        $("#modelPopSupplierAudit").dialog("open");
    };
    this.GetSupplierSiteAuditBySupplierID = function () {
        self.SupplierSiteAuditArray([]);
        $('#divSiteAuditSpinner').spin(true);
        $.ajax({
            url: self.UrlGetSupplierSiteAuditBySupplierID,
            type: 'post',
            cache: false,
            datatype: 'application/json',
            data: { supplierId: self.SupplierID(), documentTypeID: self.DocumentTypeID() },
            success: function (siteAudits) {
                $.each(siteAudits, function (index, value) {
                    var UploadDateNonString = parseJsonDateString(value.UploadDate);
                    var UploadDateString = UploadDateNonString.getFullYear() + "-" + (UploadDateNonString.getMonth() + 1) + "-" + UploadDateNonString.getDate();
                    ko.utils.arrayForEach(self.Users(), function (item) {
                        if (item.UserID() === value.UserID) {
                            value.UserName = item.UserName();
                        }
                    });
                    self.SupplierSiteAuditArray.push(new SiteAudits
                   (value.SupplierSiteAuditID, value.AuditNotes, value.AuditPass, value.UserID, value.UserName,
                   value.AuditDate, value.SupplierID, value.SupplierDocumentID, value.UploadPath, value.DocumentName, value.DocumentUrl, UploadDateString));
                });
                $('#divSiteAuditSpinner').spin(false);
            },
            error: function () {
                $('#divSiteAuditSpinner').spin(false);
            }
        });
    };

};