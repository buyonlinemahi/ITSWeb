/*
*  Latest Version  : 1.2

*  Created by   : Robin Singh
*  Version      : 1.0
*  Date         : 20-Dec-2012
*  Description  : Create viewModel to implement supplier Document Registration setup

*  Updated by   : Robin Singh
*  Version      : 1.1
*  Date         : 29-Dec-2012
*  Description  : Added method GetDocumentRegistration to Get documentregistration details

*  Updated by   : Robin Singh
*  Version      : 1.2
*  Date         : 02-Jan-2012
*  Description  : Implement method SaveDocumentsRegistration to add documentregistration details and download document file details.
*/

function SupplierDocumentTypesModel(supplierDocumentID, documentTypeID, supplierID, userID, uploadDate, documentName, uploadPath, userName, documentUrl) {
    var self = this;
    self.SupplierDocumentID = ko.observable(supplierDocumentID);
    self.DocumentTypeID = ko.observable(documentTypeID);
    self.SupplierID = ko.observable(supplierID);
    self.UserID = ko.observable(userID);
    self.UploadDate = ko.observable(uploadDate);
    self.DocumentName = ko.observable(documentName);
    self.UploadPath = ko.observable(uploadPath);
    self.UserName = ko.observable(userName);
    self.DocumentUrl = ko.observable(documentUrl);
}

function SupplierDocumentViewModel(data) {
    var self = this;
    self.SupplierDocumentTypes = ko.observableArray([]);
    self.SupplierDocumentID = ko.observable();
    // Here DocumentTypeId is manually passed
    self.DocumentTypeID = ko.observable(1);
    self.SupplierID = ko.observable();
    self.UploadDate = ko.observable();
    self.DocumentName = ko.observable();
    self.UploadPath = ko.observable();
    self.Message = ko.observable();
    self.Status = ko.observable();
    self.NoRecordFound = ko.observable(true);
    self.UrlGetSupplierDocumentBySupplierIDAndDocumentTypeID = ko.observable();

    this.clearModelValues = function () {
        self.DocumentName(null);
        self.SupplierDocumentID(null);
        self.UploadPath(null);
        //For IE
        $("#fileToUploadRegistrationDocument").replaceWith($("#fileToUploadRegistrationDocument").clone(true));
        //For other browsers
        $("#fileToUploadRegistrationDocument").val("");
    };

    this.InitializeDocumentViewModel = function (data) {
        self.SupplierID(data.SupplierID);
    };

    this.InitializeDocumentRegistrationUrls = function (data) {
        self.UrlGetSupplierDocumentBySupplierIDAndDocumentTypeID(data.UrlGetSupplierDocumentBySupplierIDAndDocumentTypeID);
    };

    this.LoadTab = function () {
        self.SupplierDocumentTypes([]);
        self.GetDocumentRegistration(self.SupplierID(), self.DocumentTypeID());
    };
    this.GetDocumentRegistration = function (newsupplierId, newdocumentTypeID) {
        self.SupplierDocumentTypes([]);
        $("#divRegistrationDocumentSpinner").spin(true);
        $.ajax({
            url: self.UrlGetSupplierDocumentBySupplierIDAndDocumentTypeID(),
            type: "POST",
            datatype: 'application/json',
            data: { supplierID: newsupplierId, documentTypeID: self.DocumentTypeID },
            success: function (model) {
                $.each(model, function (index, value) {
                    self.SupplierDocumentTypes.push(new SupplierDocumentTypesModel(value.SupplierDocumentID, value.DocumentTypeID, self.SupplierID, value.UserID, value.UploadDate, value.DocumentName, value.UploadPath, model[index].UserName, value.DocumentUrl));
                })
                if (model.length <= 0) {
                    self.NoRecordFound(true);
                    self.Status("No Record Found");
                }
                else {
                    self.NoRecordFound(false);
                }
                $("#divRegistrationDocumentSpinner").spin(false);
            },
            error: function () {
                $("#divRegistrationDocumentSpinner").spin(false);
            }
        });
    };

    // variable to accept the Error messages from the Global Resx file
    self.PleaseEnterDocumentName;
    self.PleaseSelectFile;
    self.PleaseBrowsePDFFileOnly;

    // function to initialize the Error Message from the Global Error Resx File
    this.InitializeDocumentRegistrationErrorMsg = function (msg) {
        self.PleaseEnterDocumentName = msg.PleaseEnterDocumentName;
        self.PleaseSelectFile = msg.PleaseSelectFile;
        self.PleaseBrowsePDFFileOnly = msg.PleaseBrowsePDFFileOnly;
    };

    // Implement validation for supplier Registration Document
    this.ValidateDocumentRegistrationForm = function () {
        if (self.DocumentName() == null || self.DocumentName() == '') {
            alert(self.PleaseEnterDocumentName);
            return false;
        }
        else if (self.UploadPath() == null) {
            alert(self.PleaseSelectFile);
            return false;
        }
        else if ($('#fileToUploadRegistrationDocument').val() != "") {
            var FName = $('#fileToUploadRegistrationDocument').val();
            var FNameIndex = FName.split('.');
            if (FNameIndex[1] != "pdf" && FNameIndex[1] != "PDF") {
                alert(self.PleaseBrowsePDFFileOnly);
                return false;
            }
        }
        return true;
    };

    this.SaveDocumentRegistration = function () {
        self.GetDocumentRegistration(self.SupplierID(), self.DocumentTypeID());
        self.clearModelValues();
    };

};

