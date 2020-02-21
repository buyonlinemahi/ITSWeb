

function IndexViewModel() {
    var self = this;

    self.Cases = ko.observableArray();
 
    self.TotalItemCount = ko.observable();
    self.CaseSearch = ko.observable();


    self.Init = function (Cases, CaseSearch, totalCount) {
        self.CaseSearch(CaseSearch);
        self.TotalItemCount(totalCount);
        ko.mapping.fromJS(Cases, {}, self.Cases);
        self.Pager().CurrentPage(1);
    };


    self.searchCaseFormBeforeSubmit = function (arr, $form, options) {

        if ($form.jqBootstrapValidation('hasErrors')) return false;
        return true;
    };

    self.UpdateCaseGrid = function (responseText) {
        var response = $.parseJSON(responseText);
        if (response != null) {
            if (self.TotalItemCount() != response.TotalCount) {
                self.Pager().CurrentPage(1);
            }
            self.Cases([]);
            ko.mapping.fromJS(response.Cases, {}, self.Cases);
            ko.mapping.fromJS(response.TotalCount, {}, self.TotalItemCount);
            ko.mapping.fromJS(response.CaseSearch, {}, self.CaseSearch);
        }

    };

    self.GetCaseSearchWithSkipAndTake = function (skip, take) {
        if (skip == undefined || take == undefined) {
            self.Skip(0);
            self.Take(pagingSettings.pageSize);
        }
        else {
            self.Skip(skip);
            self.Take(take);
        }
        $("#frmCaseSearch").submit();

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
        self.GetCaseSearchWithSkipAndTake(skip, take);

    });


}