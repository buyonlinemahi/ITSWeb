

function CaseUpload(data) {
    var self = this;
    self.CaseID = ko.observable(data.CaseID);
    self.DocumentTypeID = ko.observable(data.DocumentTypeID);
    self.UploadDate = ko.observable(moment(data.UploadDate).format("DD/MM/YYYY"));
    self.DocumentName = ko.observable(data.DocumentName);
    var lastSpacePos = (data.DocumentName).lastIndexOf(".");
    //self.DocumentName1 = ((data.DocumentName).substring(0, lastSpacePos));
    self.ReferrerDocumentTypeDesc = ko.observable(data.ReferrerDocumentTypeDesc);

    //  if (lastSpacePos > 0) {
    // self.EducationFormatDatePrioritysfinal.push(new EducationFormatDatePrioritysfinalItem(self.EducationFormatDatePrioritys()[i].EducationFormatDatePriority1().substring(lastSpacePos + 1)));
    self.FirstName = ko.observable(data.FirstName);
    self.LastName = ko.observable(data.LastName);
    self.UploadPath = ko.observable(data.UploadPath);
    self.UserID = ko.observable(data.UserID);

    self.SupplierCheck = ko.observable(data.SupplierCheck);
    self.ReferrerCheck = ko.observable(data.ReferrerCheck);
    self.ReferrerDocumentID = ko.observable(data.ReferrerDocumentID);
    self.CaseDocumentID = ko.observable(data.CaseDocumentID);
    self.UploadeddBy = ko.computed(function () {
        return self.FirstName() + ' ' + self.LastName();
    });


}
function CaseUploadViewModel() {   
    var self = this;

    self.CaseDocumentUsers = ko.observableArray([]);
    self.CaseUploadArray = ko.observableArray([]);
    self.CaseID = ko.observable();
  
    var mappingOptions = {
        'UploadDate': {
            create: function (options) {
                if (options.data != null) {
                    return moment(options.data).format("DD/MM/YYYY");
                }
            }
        },

    };
    self.Init = function (data) {
        if (data != null) {
            ko.mapping.fromJS(data, mappingOptions, self.CaseDocumentUsers);           
        }
    };


    

    self.AddCaseUploads = function (responseText) {

        var CasesUpload = new CaseUpload($.parseJSON(responseText));
        self.CaseDocumentUsers.push(CasesUpload);
        alert("Added Sucessfully");
    };

    self.AddCaseSearchSharedSuccess = function (responseText) {
        alert("Save Successfully");
    }
    self.ViewUploads = function (data) {
        var DoctypeID = this.DocumentTypeID();
        if (DoctypeID == 7 || DoctypeID == 8 || DoctypeID == 9 || DoctypeID == 10) {
            window.location = '/file/ViewCaseUploads?UploadPath=' + this.UploadPath() + '&DocumentName=' + this.DocumentName();
        }
        else {
            window.location = '/CaseSearch/ViewCaseUploads?UploadPath=' + data.UploadPath() + '&CaseId=' + data.CaseID();
        }
    };
   
}