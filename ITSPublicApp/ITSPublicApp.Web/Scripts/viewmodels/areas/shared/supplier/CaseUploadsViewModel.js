function CaseUploadViewModel(model) {
    var self = this;

    self.CaseUploadArray = ko.observableArray([]);
    var mappingOptions = {
        'UploadDate': {
            create: function (options) {
                if (options.data != null)
                    return moment(options.data).format("DD/MM/YYYY");
            }
        },
    }
   
    if (model != null) {
        self.CaseUploadArray.removeAll();
            ko.mapping.fromJS(model, mappingOptions, self.CaseUploadArray);
        }

    self.ViewUploads = function (data) {
            var DoctypeID = data.DocumentTypeID();
            if (DoctypeID == 7 || DoctypeID == 8 || DoctypeID == 9 || DoctypeID == 10) {
                window.location = '/file/ViewCaseUploads?UploadPath=' + data.UploadPath() + '&DocumentName=' + data.DocumentName();
            }
            else {
                window.location = '/Supplier/CaseSearch/ViewCaseUploads?UploadPath=' + data.UploadPath() + '&CaseId=' + data.EncryptedCaseID();
            }

        }
}