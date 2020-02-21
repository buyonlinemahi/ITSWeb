function AddSupplierSiteAuditViewModel() {
    var self = this;
    self.SupplierSiteAuditID = ko.observable();
    self.AuditNotes = ko.observable();
    self.AuditPass = ko.observable();
    self.UserID = ko.observable();
    self.AuditDate = ko.observable();
    self.SupplierID = ko.observable();
    self.SupplierDocumentID = ko.observable();
    self.UserName = ko.observable();
    self.DocumentName = ko.observable();
    self.UploadPath = ko.observable();
    self.DocumentUrl = ko.observable();
    self.UploadDate = ko.observable();
    self.Users = ko.observableArray([]);

    self.AuditPass.IsPass = ko.computed({
        read: function () {
            return this.AuditPass() == null ? "false" : this.AuditPass().toString();
        },
        write: function (newValue) {
            this.AuditPass(newValue === "true");
        },
        owner: this
    });

    self.Init = function (supplierID, users) {
        self.SupplierID(supplierID);
        if (users != null) {
            self.Users(users);
        }
    }

    self.DisableAddButton = ko.observable(false);

    this.ResetDisabledAddButton = function () {
        self.DisableAddButton(false);
    };

    self.SiteAuditFormBeforeSubmit = function (arr, $form, options) {
        if ($form.jqBootstrapValidation('hasErrors'))
            return false;
        else
            var extention = $('input[name$="FileUploadSiteAudit"]').val().split('.').pop().toLowerCase();

        if (extention == "jpg" || extention == "jpeg" || extention == "doc" || extention == "docx" || extention == "tiff" || extention == 'pdf' || extention == 'tif') {
            self.DisableAddButton(true);
            return true;
        } else {
            alert("please select pdf,doc,docx,jpg,jpeg,tif or tiff file only");

            return false;

        }
       

    }
}
