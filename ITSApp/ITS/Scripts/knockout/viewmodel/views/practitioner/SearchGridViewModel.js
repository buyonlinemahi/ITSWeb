

function SearchPractitioner(model) {
    var self = this;
    self.PractitionerID = ko.observable();
    self.PractitionerFullName = ko.observable();
    self.TreatmentCategoryName = ko.observable();
    self.PractitionerLastName = ko.observable();
    self.PractitionerFirstName = ko.observable();
    self.RegistrationTypeName = ko.observable();
    ko.mapping.fromJS(model, {}, self);
}

function SearchGridViewModel() {
    var self = this;
    self.Practitioners = ko.observableArray();
    self.SelectedPractitioner = ko.observable();
    self.PractitionerID = ko.observable();
    self.TotalItemCount = ko.observable();
    self.PractitionerSearch = ko.observable('');

    this.Init = function (Practitioners, PractitionerSearch, totalCount) {

        self.PractitionerSearch(PractitionerSearch);
        self.TotalItemCount(totalCount);
        $.each(Practitioners, function (index, value) {
            self.Practitioners.push(new SearchPractitioner(value));
        });

    };


    self.searchPractitionerFormBeforeSubmit = function (arr, $form, options) {

        if ($form.jqBootstrapValidation('hasErrors')) return false;
        return true;
    };

    this.UpdatePractitionerGrid = function (responseText) {
        var response = $.parseJSON(responseText);
      
        self.Practitioners([]);
        $.each(response.Practitioners, function (index, value) {

            self.Practitioners.push(new SearchPractitioner(value));
        });
        if (self.TotalItemCount() != response.TotalCount) {
            self.Pager().CurrentPage(1);
        }
        self.TotalItemCount(response.TotalCount);

    };

    this.GetPractitionerSearchWithSkipAndTake = function (skip, take) {

        if (skip == undefined || take == undefined) {
            self.Skip(0);
            self.Take(pagingSettings.pageSize);
        }
        else {
            self.Skip(skip);
            self.Take(take);
        }
        $("#frmSearchPractitioner").submit();

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
        self.GetPractitionerSearchWithSkipAndTake(skip, take);

    });

}