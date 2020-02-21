

function SearchReferrer(model) {
    var self = this;
    self.ReferrerID = ko.observable();
    self.EncryptedReferrerID = ko.observable();

    self.ReferrerName = ko.observable();
    self.ReferrerMainContactPhone = ko.observable();
    self.City = ko.observable();
    self.Name = ko.observable();
    ko.mapping.fromJS(model, {}, self);
}

function SearchViewModel() {
    var self = this;
    self.Referrers = ko.observableArray();
    self.SelectedReferrer = ko.observable();
    self.ReferrerID = ko.observable();
    self.EncryptedReferrerID = ko.observable();
    self.TotalItemCount = ko.observable();
    self.ReferrerSearch = ko.observable('');

    self.Init = function (Referrers, ReferrerSearch, totalCount) {
        
        self.ReferrerSearch(ReferrerSearch);
        self.TotalItemCount(totalCount);
        $.each(Referrers, function (index, value) {
            self.Referrers.push(new SearchReferrer(value));
        });

    };


    self.searchReferrerFormBeforeSubmit = function (arr, $form, options) {

        if ($form.jqBootstrapValidation('hasErrors')) return false;
        return true;
    };

    self.UpdateReferrerGrid = function (responseText) {
        var response = $.parseJSON(responseText);
      
        self.Referrers([]);
        $.each(response.Referrers, function (index, value) {
            self.Referrers.push(new SearchReferrer(value));
        });

        if (self.TotalItemCount() != response.TotalCount) {
            self.Pager().CurrentPage(1);
        }
        self.TotalItemCount(response.TotalCount);

    };

    self.GetReferrerSearchWithSkipAndTake = function (skip, take) {

        if (skip == undefined || take == undefined) {
            self.Skip(0);
            self.Take(pagingSettings.pageSize);
        }
        else {
            self.Skip(skip);
            self.Take(take);
        }
        $("#frmSearchReferrer").submit();

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
        self.GetReferrerSearchWithSkipAndTake(skip, take);

    });

}