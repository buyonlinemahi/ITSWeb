
function InvoiceViewModel() {
    var self = this;
    self.caseInvoicePatientReferrer = ko.observableArray([]);

    self.TotalCount = ko.observable();

    self.TotalItemCount = ko.computed(function () {
        return self.TotalCount();
    });


    self.Init = function (data) {

        if (data != null) {
            ko.mapping.fromJS(data, {}, self);
        }
     
    };

   


    self.GetInvoicesWithSkipTake = function (skip, take) {

        
        if (skip == undefined || take == undefined) {
            self.Skip(0);
            self.Take(pagingSettings.pageSize);
        }
        else {
            self.Skip(skip);
            self.Take(take);
        }
        $("#frmInvoiceGrid").submit();

    };

    var pagingSettings = {
        pageSize: 5,
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
        self.GetInvoicesWithSkipTake(skip, take);

    });

};