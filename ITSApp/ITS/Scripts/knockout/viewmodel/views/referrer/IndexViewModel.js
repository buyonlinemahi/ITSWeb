function ReferrerGridViewModel() {
    var self = this;
    self.Referrers = ko.observableArray();

    self.Init = function (data) {
        this.loadReferrers(data);
    };

    this.loadReferrers = function (data) {
        if (data != null) {
            ko.mapping.fromJS(data, mappingOptions, self.Referrers);
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
}

function SearchViewModel() {
    var self = this;
}