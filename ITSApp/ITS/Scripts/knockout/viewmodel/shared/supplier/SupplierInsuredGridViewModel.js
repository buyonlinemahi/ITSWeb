function SupplierInsurance(data) {
    var self = this;
    self.SupplierID = ko.observable();
    self.SupplierInsuredID = ko.observable();
    self.SupplierDocumentID = ko.observable();
    self.DocumentName = ko.revertableObservable();
    self.LevelOfCover = ko.revertableObservable();
    self.RenewalDate = ko.revertableObservable().extend({formatDate: 'DD/MM/YYYY' });
    self.UploadPath = ko.observable();
    self.DocumentUrl = ko.observable();
    ko.mapping.fromJS(data, {}, self);
    ko.CommitChanges(self);

    self.DocumentURLUnescaped = ko.computed(function () {
        return self.DocumentUrl() == null ? self.DocumentUrl() : self.DocumentUrl().replace(/&amp;/g, '&');
    });

   
}

function SupplierInsuredGridViewModel() {

    var self = this;
    self.Insurances = ko.observableArray();
    self.SelectedSupplierInsured = ko.observable();

    self.DisableUpdateButton = ko.observable(false);

    this.UpdateInsuredFormBeforeSubmit = function (arr, $form, options) {
        if ($form.jqBootstrapValidation('hasErrors')) return false;
        self.DisableUpdateButton(true);
        return true;
    }

    this.Init = function (model) {
        if (model != null) {
            $.each(model, function (index, value) {
                self.Insurances.push(new SupplierInsurance(value));
            });
        }
    };

    this.viewInsurance = function (item) {
        self.SelectedSupplierInsured(item);
    };

    this.UpdateInsuredGrid = function (responseText, $form) {
        self.Insurances.push(new SupplierInsurance($.parseJSON(responseText)));
        alert("Added successfully");
    };

    this.InsuranceCloseClick = function (item) {
        ko.RevertChanges(item);
        self.DeSelectCurrentSelectedSupplierInsured();
    };

    this.DeSelectCurrentSelectedSupplierInsured = function () {
        self.SelectedSupplierInsured(null);
        $('#divViewInsured').modal('hide');
    };

    this.UpdateAndRefreshGrid = function (responseText, $form) {
        self.SelectedSupplierInsured(null);
    };

    this.UpdateSelectedSupplierInsured = function (item) {
        ko.mapping.fromJS(item, {}, self.SelectedSupplierInsured);
        ko.CommitChanges(self.SelectedSupplierInsured);
        this.DisableUpdateButton(false);
        alert("Updated successfully");
    };

    this.deleteInsurance = function (itemToDelete) {
        var confirm = window.confirm("Are you sure to delete this item");
        if (confirm) {
            $.ajax({
                url: "/SupplierShared/DeleteInsurance",
                cache: false,
                type: "POST", dataType: 'json',
                contentType: 'application/json',
                data: ko.toJSON(this),
                success: function (data) {
                    self.Insurances.remove(function (item) { return item.SupplierInsuredID == itemToDelete.SupplierInsuredID })
                    alert(data);
                },
                error: function (data) {
                    alert("Error while deleting a new Insurance - " + data);
                }
            });
        }
    };

};