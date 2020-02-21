
function EmailCommunicationViewModel() {
    var self = this;
    self.CaseCommunicationHistoryUsers = ko.observableArray([]);
    self.SelectedItem = ko.observable();

    self.Init = function (data) {
        if (data != null) {
            ko.mapping.fromJS(data, mappingOptions, self.CaseCommunicationHistoryUsers);
        }
    };

    mappingOptions = {
        'DateAdded': {
            create: function (options) {
                if (options.data != null) {
                    return moment(options.data).format("DD/MM/YYYY");
                }
            }
        }
    };

    self.ResendClick = function (newVal) {
      
        self.SelectedItem(newVal);
       
    };

    self.ResendEmailFormBeforeSubmit = function (arr, $form, options) {
        if ($form.jqBootstrapValidation('hasErrors')) {
            return false;
        }
        else {
            return true;
        }
    }

    self.ResendEmailFormSuccess = function (arr, $form, options) {
        alert("Email successfully sent");
        arr = $.parseJSON(arr);
        arr.DateAdded = moment(arr.DateAdded).format("DD/MM/YYYY");
        self.CaseCommunicationHistoryUsers.push(ko.mapping.fromJS(arr));
        $('#divResendEmail').modal('hide');
    };

    self.ViewEmailCommunicationUploads = function (item) {
        window.open('/file/ViewCaseUploads?UploadPath=' + item.UploadPath(), '_blank')
    };
};


