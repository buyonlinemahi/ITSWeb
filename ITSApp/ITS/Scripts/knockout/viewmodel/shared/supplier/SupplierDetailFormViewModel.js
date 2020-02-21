function SupplierDetailFormViewModel() {
    var self = this;
    self.SupplierName = ko.observable();
    self.SupplierID = ko.observable();
    self.Address = ko.observable();
    self.City = ko.observable();
    self.Region = ko.observable();
    self.PostCode = ko.observable();
    self.Phone = ko.observable();
    self.Fax = ko.observable();
    self.Email = ko.observable();
    self.Website = ko.observable();
    self.IsWheelChairAccessibility = ko.observable();
    self.IsWeekends = ko.observable();
    self.IsEvenings = ko.observable();
    self.IsParking = ko.observable();
    self.IsHomeVisit = ko.observable();
    self.IsTriage = ko.observable();
    self.Ranking = ko.observable('');
    self.StatusID = ko.observable();
    self.Notes = ko.observable();
    $(".phoneMaskformat").mask("99999 999999");
    self.DisableUpdateButton = ko.observable(false);

    self.UpdateSupplierDetailFormBeforeSubmit = function (arr, $form, options) {
        if ($form.jqBootstrapValidation('hasErrors')) return false;
        self.DisableUpdateButton(true);
        return true;
    }

    self.Init = function (model) {
        self.SupplierID(model.SupplierID);
        self.SupplierName(model.SupplierName);
        self.Address(model.Address);
        self.City(model.City);
        self.Region(model.Region);
        self.PostCode(model.PostCode);
        self.Phone(model.Phone);
        self.Fax(model.Fax);
        self.Email(model.Email);
        self.Website(model.Website);
        self.IsWheelChairAccessibility(model.IsWheelChairAccessibility);
        self.IsWeekends(model.IsWeekends);
        self.IsEvenings(model.IsEvenings);
        self.IsParking(model.IsParking);
        self.IsHomeVisit(model.IsHomeVisit);
        self.IsTriage(model.IsTriage);
        self.Ranking(model.Ranking);
        self.StatusID(model.StatusID);
        self.Notes(model.Notes);
    };

    self.UpdateDetailSucess = function (model) {
        self.StatusID(model.StatusID);
        self.DisableUpdateButton(false);
        if (model.Message != null) {
            alert("Email already exists");
        }
        else {
            alert("Supplier Updated Sucessfully");
        }
       
    };

    self.UpdateStatus = function () {
        var ajaxPost = AjaxUtil.post('/SupplierShared/UpdateSupplierStatus', 'json', { supplierID: self.SupplierID(), isTriage: self.IsTriage() });
        ajaxPost.done(function (resp) {
            self.StatusID(resp);
        });
    };

}