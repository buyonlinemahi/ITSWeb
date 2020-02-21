

function SupplierClinicalAudit(data, users) {

    var self = this;
    self.SupplierID = ko.observable();
    self.SupplierClinicalAuditID = ko.observable();
    self.PatientFirstName = ko.revertableObservable();
    self.PatientLastName = ko.revertableObservable();
    self.Users = ko.observableArray(users);
    self.SelectedAuditor = ko.observable();

    self.DocumentUpload = ko.observable();
    self.SupplierDocumentID = ko.observable();
    self.DocumentUrl = ko.observable();
    self.DocumentURLUnescaped = ko.computed(function () {
        return self.DocumentUrl() == null ? self.DocumentUrl() : self.DocumentUrl().replace(/&amp;/g, '&');
    });
    self.UploadPath = ko.observable();

    self.CaseNumber = ko.revertableObservable();
    self.AuditPass = ko.revertableObservable();
    self.UserID = ko.revertableObservable();
    self.AuditDate = ko.revertableObservable().extend({ formatDate: 'DD/MM/YYYY' });
    self.DocumentName = ko.revertableObservable();

    ko.mapping.fromJS(data, {}, self);
    ko.CommitChanges(self);

    self.SelectedCasePatient = ko.revertableObservable();

    self.CaseID = ko.computed(function () {
        if (self.SelectedCasePatient() !== undefined) {
            return self.SelectedCasePatient().CaseID;
        }
        else {
            return data.CaseID;
        }
    });

    self.GetPatientName = function () {
        $.ajax({
            url: "/SupplierShared/GetCasePatientLikeCaseNumber",
            cache: false,
            type: "POST", dataType: 'json',
            contentType: 'application/json',
            data: ko.toJSON({ casenumber: $("#txtItsReferenceNumber").val() }),
            success: function (data) {
                var t = data[0].PatientFirstName + ' ' + data[0].PatientLastName;
                $("#txtPatientName").val(t);
            },
            error: function (data) {
                alert("Couldn't find data - " + data);
            }
        });
    }

}

function SupplierClinicalAuditGridViewModel() {
    var self = this;
    self.ClinicalAudits = ko.observableArray([]);
    self.SelectedClinicalAudit = ko.observable();

    this.DisableUpdateButton = ko.observable(false);

    this.UpdateClinicalAuditFormBeforeSubmit = function (arr, $form, options) {
        if ($form.jqBootstrapValidation('hasErrors')) return false;
        self.DisableUpdateButton(true);
        return true;
    }

    this.Init = function (model, users) {
        self.loadClinicalAudits(model, users);
    };

    this.ClinicalCloseClick = function (item) {
        ko.RevertChanges(item);
        self.DeSelectCurrentSelectedSupplierClinicalAudit();
    };

    this.viewClinicalAudit = function (item) {
        self.SelectedClinicalAudit(item);
    };

    this.AddClinicalAudit = function (responseText, users, form) {
        var clinicalAudit = new SupplierClinicalAudit($.parseJSON(responseText), users);
        self.ClinicalAudits.push(clinicalAudit);
        alert("Added Sucessfully");
    };

    this.DeSelectCurrentSelectedSupplierClinicalAudit = function () {
        self.SelectedClinicalAudit(null);
        $('#divViewClinicalAudit').modal('hide');
    };

    this.UpdateSelectedSupplierClinicalAudit = function (item) {
        ko.mapping.fromJS(item, {}, self.SelectedClinicalAudit);
        ko.CommitChanges(self.SelectedClinicalAudit());
        this.DisableUpdateButton(false);
        alert("Updated Sucessfully");
    };

    this.DeleteClinicalAudit = function (itemToDelete) {
        var confirm = window.confirm("Are you sure to delete this item");
        if (confirm) {
            $.ajax({
                url: "/SupplierShared/DeleteClinicalAudit",
                cache: false,
                type: "POST",
                dataType: 'json',
                contentType: 'application/json',
                data: ko.toJSON(this),
                success: function (data) {
                    self.ClinicalAudits.remove(function (item) {
                        return item.SupplierClinicalAuditID == itemToDelete.SupplierClinicalAuditID;
                    });
                    alert("Deleted successfully.");
                },
                error: function (data) {
                    alert("Error while deleting a new Clinical Audit - " + data);
                }
            });
        }
    };

    this.loadClinicalAudits = function (model, users) {
        $.each(model, function (index, value) {
            self.ClinicalAudits.push(new SupplierClinicalAudit(value, users));
        });
    };

};