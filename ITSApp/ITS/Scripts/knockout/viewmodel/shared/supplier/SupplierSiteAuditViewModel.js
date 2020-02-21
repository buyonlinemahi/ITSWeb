function SupplierSiteAuditModel(data, user) {

    var self = this;
    self.SupplierSiteAuditID = ko.observable();
    self.AuditNotes = ko.revertableObservable();
    self.AuditPass = ko.revertableObservable();
    self.UserID = ko.revertableObservable();
    self.AuditDate = ko.revertableObservable().extend({ formatDate: 'DD/MM/YYYY' });
    self.SupplierID = ko.observable();
    self.SupplierDocumentID = ko.observable();
    self.UserName = ko.observable();
    self.DocumentName = ko.revertableObservable();
    self.UploadPath = ko.observable();
    self.DocumentUrl = ko.observable();
    self.UploadDate = ko.observable();
    self.Users = ko.observableArray(user);

    ko.mapping.fromJS(data, {}, self);
   
    ko.CommitChanges(self); 

    self.DocumentURLUnescaped = ko.computed(function () {
        return self.DocumentUrl() == null ? self.DocumentUrl() : self.DocumentUrl().replace(/&amp;/g, '&');
    });

    self.AuditPass.IsPass = ko.computed({
        read: function () {
            return self.AuditPass().toString();
        },
        write: function (newValue) {
            self.AuditPass(newValue === "true");
        },
        owner: this
    });

    
}


function SupplierSiteAuditGridViewModel() {

    var self = this;

    self.SupplierSiteAudits = ko.observableArray([]);
    self.SelectedSupplierSiteAudit = ko.observable();
    self.viewSupplierSiteAudit = function (item) {
        self.SelectedSupplierSiteAudit(item);
    };

    self.DisableUpdateButton = ko.observable(false);
    self.UpdateSiteAuditFormBeforeSubmit = function (arr, $form, options) {
        if ($form.jqBootstrapValidation('hasErrors')) return false;
        self.DisableUpdateButton(true);
        return true;
    };

    self.DeSelectCurrentSelectedSupplierSiteAudit = function () {
        self.SelectedSupplierSiteAudit(null);
        $('#divViewSiteAudit').modal('hide');
    };

    self.CloseClick = function (item) {
        ko.RevertChanges(item);
        self.DeSelectCurrentSelectedSupplierSiteAudit();
    };

    self.UpdateSelectedSupplierSiteAudit = function (item) {
        ko.mapping.fromJS(item, {}, self.SelectedSupplierSiteAudit);
        ko.CommitChanges(self.SelectedSupplierSiteAudit());
        self.DisableUpdateButton(false);
        alert("Updated successfully");
    };

    self.Init = function (model, user) {
        $.each(model, function (index, value) {
            self.SupplierSiteAudits.push(new SupplierSiteAuditModel(value, user));
        });
    };

    self.AddSupplierSiteAudit = function (responseText, user, $form) {
        self.SupplierSiteAudits.push(new SupplierSiteAuditModel($.parseJSON(responseText), user));
        alert("Added successfully");
    };

    self.deleteSupplierSiteAudit = function (itemToDelete) {
        var confirm = window.confirm("Are you sure to delete this item");
        if (confirm) {
            $.ajax({
                url: "/SupplierShared/DeleteSupplierSiteAudit",
                cache: false,
                type: "POST", dataType: 'json',
                contentType: 'application/json',
                data: ko.toJSON(this),
                success: function (data) {
                    self.SupplierSiteAudits.remove(function (item) {
                        return item.SupplierSiteAuditID == itemToDelete.SupplierSiteAuditID;
                    });
                    alert(data);
                },
                error: function (data) {
                    alert("Couldn't delete site audit - " + data);
                }
            });
        }
    };
}