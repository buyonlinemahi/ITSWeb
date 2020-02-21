function SupplierGridViewModel() {
    var self = this;
    self.Suppliers = ko.observableArray();
    self.SelectedSupplier = ko.observable();

    this.Init = function (model) {
       
            $.each(model, function (index, value) {
                self.Suppliers.push(new Supplier(value));
            });
        
    };

    

}