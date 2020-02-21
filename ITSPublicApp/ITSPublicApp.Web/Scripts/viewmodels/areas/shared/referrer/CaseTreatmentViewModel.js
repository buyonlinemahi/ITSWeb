function CaseTreatmentViewModel(model) {
    var self = this;

    self.CaseTreatmentViewModelArray = ko.observableArray([]);
    var mappingOptions = {
        'DateOfService': {
            create: function (options) {
                if (options.data != null)
                    return moment(options.data).format("DD/MM/YYYY");
            }
        },
    }

    if (model != null) {
        ko.mapping.fromJS(model, mappingOptions, self.CaseTreatmentViewModelArray);
    }
   
}