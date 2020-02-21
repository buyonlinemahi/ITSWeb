

function SearchSupplier(model) {
    var self = this;
    self.SupplierID = ko.observable();
    self.SupplierName = ko.observable();
    self.TreatmentCategoryName = ko.observable();
    self.PostCode = ko.observable();

    ko.mapping.fromJS(model, {}, self);
}

function SupplierSearchGridViewModel() {
    var self = this;
    self.Suppliers = ko.observableArray();
    self.SelectedSupplier = ko.observable();
    self.SupplierID = ko.observable();
    self.TotalItemCount = ko.observable();
    self.SupplierSearch = ko.observable('');

    self.searchFormBeforeSubmit = function (arr, $form, options) {
       
        if ($form.jqBootstrapValidation('hasErrors')) return false;
        return true;
    }

    this.Init = function (suppliers, supplierSearch, totalCount) {

        self.SupplierSearch(supplierSearch);
        self.TotalItemCount(totalCount);
        $.each(suppliers, function (index, value) {
            self.Suppliers.push(new SearchSupplier(value));
        });
       
    };

    this.UpdateSupplierGrid = function (responseText, statusText, xhr, $form) {
      
        var response = $.parseJSON(responseText);
        self.Suppliers([]);
        $.each(response.Suppliers, function (index, value) {

            self.Suppliers.push(new SearchSupplier(value));
        });
        if (self.TotalItemCount() != response.TotalCount) {
            self.Pager().CurrentPage(1);
        }
        self.TotalItemCount(response.TotalCount);

    };

    this.GetSupplierSearchWithSkipAndTake = function (skip, take) {

        if (skip == undefined || take == undefined) {
            self.Skip(0);
            self.Take(pagingSettings.pageSize);
        }
        else {
            self.Skip(skip);
            self.Take(take);
        }
        $("#frmfSearchSupplier").submit();

    };


    var pagingSettings = {
        pageSize: 10,
        pageSlide: 2
    };
    self.Skip = ko.observable(0);
    self.Take = ko.observable(pagingSettings.pageSize);

    self.Pager = ko.pager(self.TotalItemCount);
    self.Pager().PageSize(pagingSettings.pageSize);
    self.Pager().PageSlide(pagingSettings.pageSlide);
    self.Pager().CurrentPage(1);
    self.Pager().CurrentPage.subscribe(function () {

        var skip = pagingSettings.pageSize * (self.Pager().CurrentPage() - 1);
        var take = pagingSettings.pageSize;
      
        self.GetSupplierSearchWithSkipAndTake(skip, take);

    });

}