

function SearchUser(model) {
    var self = this;
    self.UserID = ko.observable();
    self.UserName = ko.observable();
    self.Email = ko.observable();
    self.UserType = ko.observable();
    self.FirstName = ko.observable();
    self.LastName = ko.observable();
    self.UserTypeName = ko.observable();
    ko.mapping.fromJS(model, {}, self);
}

function SearchViewModel() {
    var self = this;
    self.Users = ko.observableArray();
    self.SelectedUser = ko.observable();
    self.UserID = ko.observable();
    self.TotalItemCount = ko.observable();
    self.UserSearch = ko.observable('');

    self.Init = function (Users, UserSearch, totalCount) {

        self.UserSearch(UserSearch);
        self.TotalItemCount(totalCount);
        $.each(Users, function (index, value) {
            self.Users.push(new SearchUser(value));
        });

    };


    self.searchUserFormBeforeSubmit = function (arr, $form, options) {
        if ($form.jqBootstrapValidation('hasErrors')) return false;
        return true;
    };

    self.UpdateUserGrid = function (responseText) {
        var response = $.parseJSON(responseText);

        self.Users([]);
        $.each(response.Users, function (index, value) {
            self.Users.push(new SearchUser(value));
        });

        if (self.TotalItemCount() != response.TotalCount) {
            self.Pager().CurrentPage(1);
        }
        self.TotalItemCount(response.TotalCount);

    };

    self.GetUserSearchWithSkipAndTake = function (skip, take) {

        if (skip == undefined || take == undefined) {
            self.Skip(0);
            self.Take(pagingSettings.pageSize);
        }
        else {
            self.Skip(skip);
            self.Take(take);
        }
        $("#frmSearchUser").submit();

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
        self.GetUserSearchWithSkipAndTake(skip, take);

    });

}