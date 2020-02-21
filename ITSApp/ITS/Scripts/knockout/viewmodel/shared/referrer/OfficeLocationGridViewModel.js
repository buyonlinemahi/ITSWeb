
function ReferrerLocation(model) {
    var self = this;

    self.ReferrerLocationID = ko.observable();
    self.Name = ko.revertableObservable();
    self.Address = ko.revertableObservable();
    self.City = ko.revertableObservable();
    self.Region = ko.revertableObservable();
    self.PostCode = ko.revertableObservable();
    self.IsMainOffice = ko.observable();
    self.ReferrerID = ko.observable();
    self.IsActive = ko.observable();

    ko.mapping.fromJS(model, {}, self);
    ko.CommitChanges(self);

    this.UpdateOfficeLocationFormBeforeSubmit = function (arr, $form, options) {
        if ($form.jqBootstrapValidation('hasErrors')) return false;
        return true;
    };
};

function OfficeLocationGridViewModel() {
    var self = this;

    self.ReferrerLocations = ko.observableArray();
    self.SelectedOfficeLocation = ko.observable();

    self.Init = function (data) {
        if (data != null) {
            $.each(data, function (index, value) {
                self.ReferrerLocations.push(new ReferrerLocation(value));
            });
        }
    };
    this.OfficeLocationCloseClick = function (item) {
        ko.RevertChanges(item);
        self.DeSelectCurrentSelectedRferrerOfficeLocation();
    };

    this.DeSelectCurrentSelectedRferrerOfficeLocation = function () {
        self.SelectedOfficeLocation(null);
        $('#divViewOfficeLocation').modal('hide');
    };

    this.UpdateOfficeLocationGridAfterSubmit = function (item) {

        alert("Update succssfully");
        if (item.IsMainOffice == true && self.SelectedOfficeLocation().IsMainOffice() == false);
        {
            var selectedItem = ko.utils.arrayFirst(self.ReferrerLocations(), function (item) {
                if (item.IsMainOffice())
                    return item;
            });
            selectedItem.IsMainOffice(false);
        }
        ko.mapping.fromJS(item, {}, self.SelectedOfficeLocation);
        ko.CommitChanges(self.SelectedOfficeLocation);
        self.DeSelectCurrentSelectedRferrerOfficeLocation();
    };

    this.viewOfficeLocation = function (item) {


        self.SelectedOfficeLocation(item);

    };
    this.DeleteOfficeLocation = function (item) {
        if (confirm("Are you  sure to delete office location")) {
            var response = AjaxUtil.post('/ReferrerShared/DeleteByReferrerLocationID', 'json', { ReferrerLocationID: item.ReferrerLocationID });
            response.done(function (resp) {
                self.ReferrerLocations.remove(function (e) {
                    return e.ReferrerLocationID == item.ReferrerLocationID
                });
            });
        }

    };

    this.UpdateOfficeLocationGrid = function (responseText, $form) {

        if (responseText.IsMainOffice);
        {
            var selectedItem = ko.utils.arrayFirst(self.ReferrerLocations(), function (item) {
                return item.IsMainOffice() == true;
            });
            selectedItem.IsMainOffice(false);
        }
        self.ReferrerLocations.push(new ReferrerLocation($.parseJSON(responseText)));
        alert("Added successfully");
    };
};