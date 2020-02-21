
function ReferrerOfficeLocationDetailViewModel() {
    var self = this;

    self.ReferrerLocationID = ko.observable();
    self.Name = ko.observable();
    self.Address = ko.observable();
    self.City = ko.observable();
    self.Region = ko.observable();
    self.PostCode = ko.observable();
    self.IsMainOffice = ko.observable();
    self.ReferrerID = ko.observable();
    self.IsActive = ko.observable();



    this.Init = function (model) {
        if (model != null) {
            ko.mapping.fromJS(model, {}, self);
        }
    };
};