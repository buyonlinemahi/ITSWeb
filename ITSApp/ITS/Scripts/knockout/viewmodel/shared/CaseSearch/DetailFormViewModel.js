ko.extenders.parseJsonDate = function (target) {
    var result = ko.computed({
        read: target,
        write: function (newValue) {
            var valueToWrite;
            if (target && newValue != null) {
                var formattedDate = moment(newValue).format('DD/MM/YYYY');
                valueToWrite = formattedDate;
            } else {
                valueToWrite = '--';
            }
            target(valueToWrite);
        }
    });
    result(target());
    return result;
};
function DetailFormViewModel(model) {

    
    var self = this;

   
    
    self.CasePatientReferrerSupplierWorkflow = ko.observableArray();
    self.Age = ko.observable();
    self.InjuryType = ko.observable(model.CasePatientTreatment.InjuryType);
    self.ReferralFileDownloadPath = ko.observable();
    self.GenderOptions = [{ "GenderID": 1, "Gender": "Male" }, { "GenderID": 2, "Gender": "Female"}]
    this.Init = function (model, ReferralFileDownloadPath) {
        if (model != null) {
            ko.mapping.fromJS(model, mappingOptions, self.CasePatientReferrerSupplierWorkflow);
        }

        self.ReferralFileDownloadPath(ReferralFileDownloadPath);
       
        if (self.CasePatientReferrerSupplierWorkflow().BirthDate != null) {
            self.calculateAge(self.CasePatientReferrerSupplierWorkflow().BirthDate);
        }
       
    };
    $(".phoneMaskformat").mask("99999 999999");
    self.InjuryDate = ko.observable().extend({ required: { message: "InjuryDate is required." } });
    self.UpdateCasePatientFormSuccess = function (responseText, statusText, xhr, $form) {
        alert(responseText);
       

    };
    self.CasePatientUpdateFormBeforeSubmit = function (arr, $form, options) {
        if ($("#frmUpdateCasePatient").jqBootstrapValidation('hasErrors')) {
            return false;
        }       
        $("#hd").val($("<div>").text($("#EditorCaseSearch").val()).html());//encoding
        return true;
    };

    self.dobChange = function (item) {
        self.calculateAge(item.BirthDate);
    };

    self.calculateAge = function (passedDate) {
               var today = new Date();
        var birthDate = (passedDate);
        var splitedDate = birthDate.split("/");
        birthDate = splitedDate[2] + "/" + splitedDate[1] + "/" + splitedDate[0];
        var CalculatedBirthdate = new Date(birthDate);
        var age = today.getFullYear() - CalculatedBirthdate.getFullYear();
        var month = today.getMonth() - CalculatedBirthdate.getMonth();
        if (month < 0 || (month === 0 && today.getDate() < CalculatedBirthdate.getDate())) {
            age--;
        }
             self.Age(age);

         }
             ///---------------------------------Stopped Case---------------------------------Mahi
    $("#btnCaseStopped").click(function () {
        var caseID = $("#hCaseID").val();
        if (caseID != null) {
            var r = confirm("Do you want to stop this Case");
            if (r == true) {
                $.getJSON("/CaseStop/CaseStoppedCaseScreen", { caseID: caseID}, function (data) {
                    alert("Case Stopped Successfully");
                }
            )}
            }

        }
    );
    var mappingOptions = {

        'BirthDate': {
            create: function (options) {
                if (options.data != null) {
                    return moment(options.data).format("DD/MM/YYYY");
                }
            }
            
        },
        'workflowID':{
            create : function(options){
                     return options.workflowID
                     }
               
        },
        'InjuryDate': {
            create: function (options) {
                if (options.data != null) {
                    return moment(options.data).format("DD/MM/YYYY");
                }
            }
        }
    };
    if ((model.workflowID >= 50) || (model.workflowID >= 52))
        $("#DivCADate").show();
    else
        $("#DivCADate").hide();
};

function submitform() {
    $("#hd").val($("<div>").text($("#EditorCaseSearch").val()).html());//encoding
}

