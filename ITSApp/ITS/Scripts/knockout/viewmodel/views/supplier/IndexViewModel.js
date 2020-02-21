function Supplier(model) {
    var self = this;
    self.SupplierID = ko.observable();
    self.SupplierName = ko.observable();
    self.Address = ko.observable();
    self.PostCode = ko.observable();
    self.DateAdded = ko.observable().extend({ formatDate: 'DD/MM/YYYY' });  //ko.observable().extend({ parseJsonDate: null });
    ko.mapping.fromJS(model, {}, self);
}

function SearchViewModel() {
    var self = this;
}