

function GetInsuredUrl() {
    // Supplier Insurance File upload Methods
    var SupplierInsuranceFileUpload = {
        url: InsuredViewModel.GetActionUrlInsurance(),
        type: "post",
        success: function (response) {
            alert(response);
            InsuredViewModel.GetAllService();
        }
    };
    $('#SupplierInsuredform').ajaxForm(SupplierInsuranceFileUpload);
}

function InsuredControlViewModel() {
    var self = this;
    self.SupplierInsuredID = ko.observable(0);
    self.LevelOfCover = ko.observable('');
    self.RenewalDate = ko.observable('');
    self.SupplierID = ko.observable();
    self.DocumentTypeID = ko.observable(2); // This ID is constant for Insured Document
    self.UserID = ko.observable();
    self.DocumentName = ko.observable('');
    self.UploadPath = ko.observable('');
    self.IsEnable = ko.observable(false);
    self.SupplierDocumentID = ko.observable();
    self.InsuredDocumentUrl = ko.observable();
    self.UploadDate = ko.observable();
    self.urlpathInsured = ko.observable();
    self.UrlGetSupplierInsured = ko.observable();
    self.UrlRemoveFile = ko.observable();
    self.UrlUpdateSupplierInsurance = ko.observable();
    self.UrlAddSupplierInsurance = ko.observable();

    self.displayfileName = ko.observable();
    self.DocumentNameValidate = ko.observable(false);

    this.InitializeInsuredTab = function (data) {
        self.SupplierID(data.SupplierID);
    };

    this.GetActionUrlInsurance = function () {
        return self.urlpathInsured();
    }
    this.LoadTab = function () {
        self.GetAllService();
    };

    this.InitializeInsuranceUrls = function (values) {
        self.UrlGetSupplierInsured(values.UrlGetSupplierInsured);
        self.UrlRemoveFile(values.UrlRemoveFile);
        self.urlpathInsured(values.UrlAddSupplierInsurance);
        self.UrlAddSupplierInsurance(values.UrlAddSupplierInsurance);
        self.UrlUpdateSupplierInsurance(values.UrlUpdateSupplierInsurance);
    };

    this.ValidateInsuranceForm = function (FileUploderID) {
        var fileUpload = $("#" + FileUploderID).val();
        if (self.DocumentName() === null || (!self.DocumentName().length > 0)) {
            self.DocumentNameValidate(true);
            alert("Document Name is Required");
            return false;
        }

        else if (self.LevelOfCover() === null || (!self.LevelOfCover().length > 0)) {
            alert("LevelOfCover is Required");
            return false;
        }
        else if (self.RenewalDate() === null || (!self.RenewalDate().length > 0)) {
            alert("RenewalDate is Required");
            return false;
        }

        if (self.SupplierInsuredID() === 0) {
            return this.CheckInsuredFile(fileUpload);
        }
        else if (self.SupplierInsuredID() > 0) {
            self.urlpathInsured(self.UrlUpdateSupplierInsurance());
            if (fileUpload === null || fileUpload === '') {
                return true;
            }
            else {
                var answer = confirm("Are you sure to Replace old file")
                if (answer == true) {
                    return this.CheckInsuredFile(fileUpload);
                }
                else {
                    return true;
                }
            }
        }
    };

    this.CheckInsuredFile = function (file) {
        if (file === null || file === '') {
            alert("Document File is Required");
            return false;
        }
        else {
            var extensions = file.split('.');
            if (extensions[1].toUpperCase() === "PDF") {
                return true;
            }
            else {
                alert("Please select Pdf File Only ");
                return false;
            }
        }
    }

    this.GetAllService = function () {

        $("#divInsuredSpinner").spin(true);
        $.ajax({
            url: self.UrlGetSupplierInsured(),
            type: "POST",
            data: { supplierId: self.SupplierID(), documentTypeID: 2 },
            success: function (data) {
                if (data.length > 0) {
                    self.SupplierInsuredID(data[0].SupplierInsuredID);
                    self.LevelOfCover(data[0].LevelOfCover);
                    var value = parseJsonDateString(data[0].RenewalDate);
                    var renewalDateString = value.getDate() + "-" + (value.getMonth() + 1) + "-" + value.getFullYear();
                    self.RenewalDate(renewalDateString);
                    self.SupplierID(data[0].SupplierID);
                    self.DocumentName(data[0].DocumentName);
                    var UploadedDatevalue = parseJsonDateString(data[0].UploadDate);
                    var UploadedDateString = UploadedDatevalue.getDate() + "-" + (UploadedDatevalue.getMonth() + 1) + "-" + UploadedDatevalue.getFullYear();
                    self.displayfileName(data[0].DocumentName);
                    self.UploadDate(UploadedDateString);
                    //For IE
                    // $("#fileToUpload").replaceWith($("#fileToUpload").clone(true));
                    //For other browsers
                    $("#fileToUpload").val(null);

                    self.UploadPath(data[0].UploadPath);
                    self.SupplierDocumentID(data[0].SupplierDocumentID);
                    self.IsEnable(true);
                    var UrlInsuredDocumentpath = data[0].InsuredDocumentUrl;
                    self.InsuredDocumentUrl(UrlInsuredDocumentpath);
                }
                $("#divInsuredSpinner").spin(false);
            },
            error: function () {
                $("#divInsuredSpinner").spin(false);
            }
        });
    }

    this.RemoveFile = function (newsupplierId, newdocumentTypeID) {
       
        $.ajax({
            url: self.UrlRemoveFile(),
            type: "POST",
            contentType: 'application/json',
            data: ko.toJSON(this),
            success: function (Result) {
                alert(Result);
                self.UploadPath(null);
                $("#fileToUpload").replaceWith($("#fileToUpload").clone(true));
                //For other browsers
                $("#fileToUpload").val("");
                self.DocumentName(null);
                self.IsEnable(false);
                self.LevelOfCover(null);
                self.RenewalDate(null);
                self.SupplierInsuredID(null);
                self.urlpathInsured(self.UrlAddSupplierInsurance());
                self.SupplierInsuredID(0);
            }
        });
    };
};







