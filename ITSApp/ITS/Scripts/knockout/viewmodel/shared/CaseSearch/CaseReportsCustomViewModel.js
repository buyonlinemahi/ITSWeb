function CaseReportCustom() {
    var self = this;
    self.CaseReportsDetailsCustom = ko.observableArray([]);
    self.Filepath = ko.observable();
    self.Init = function (data, ReportFileDownloadPath) {
        if (data.length > 0) {
            ko.mapping.fromJS(data, mappingOptions, self.CaseReportsDetailsCustom);
            self.Filepath(ReportFileDownloadPath);
         
               }
    };

    mappingOptions = {
        'UploadDate': {
            create: function (options) {
                if (options.data != null) {
                    return moment(options.data).format("DD/MM/YYYY");
                }
            }
        }

    };

}

