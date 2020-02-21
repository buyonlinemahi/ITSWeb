function UserGridViewModel() {
    var self = this;
    self.Users = ko.observableArray();

    self.Init = function (data) {
        this.loadUsers(data);
    };

    this.loadUsers = function (data) {
        if (data != null) {
            ko.mapping.fromJS(data, mappingOptions, self.Users);
        }
    };

    mappingOptions = {
        'DateAdded': {
            create: function (options) {
                if (options.data != null) {
                    return options.data.substring(0, 10).replace('-', '/').replace('-', '/');
                }
            }
        }
    };
};

function SearchViewModel() {
    var self = this;
}