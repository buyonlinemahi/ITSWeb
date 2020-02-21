
function CollectionActionHistoryViewModel() {
    var self = this;
    self.InvoiceCollectionActionHistory = ko.observableArray();

    mappingOptions = {
        'FollowUpDate': {
            create: function (options) {
                if (options.data != null) {
                    return moment(options.data).format("DD/MM/YYYY");
                }
            }
        },
        'DateofAction': {
            create: function (options) {
                if (options.data != null) {
                    return moment(options.data).format("DD/MM/YYYY");
                }
            }
        }
    };
    self.Init = function (model) {
        ko.mapping.fromJS(model, mappingOptions, self.InvoiceCollectionActionHistory);

    };

    self.UpdateCoolectionActionGrid = function (responseText) {
        alert("Successfully saved");
        var response = $.parseJSON(responseText);
        response.FollowUpDate = moment(response.FollowUpDate).format("DD/MM/YYYY");
        response.DateofAction = moment(response.DateofAction).format("DD/MM/YYYY");
        self.InvoiceCollectionActionHistory.push(response);
    };
};