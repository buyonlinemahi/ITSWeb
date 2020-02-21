function AddClinicalAuditViewModel(supplierID, users) {
    var self = this;
    self.SupplierID = ko.observable(supplierID);
    self.Users = ko.observableArray(users);
    self.CasePatients = ko.observableArray([new CasePatient('E219AE54-4249-4367-A6E9-F45C00A25316', 9, 'fname', 'pname'),
                                            new CasePatient('82D41BDE-FE95-446C-B2D8-B001E1966884', 10, 'fname1', 'pname2')]);

    self.SelectedCasePatient = ko.observable();

    self.CaseID = ko.computed(function () {
        if (self.SelectedCasePatient() !== undefined) {
            return self.SelectedCasePatient().CaseID;
        }
    });
    self.GetPatientName = function () {
        $.ajax({
            url: "/SupplierShared/GetCasePatientLikeCaseNumber",
            cache: false,
            type: "POST", dataType: 'json',
            contentType: 'application/json',
            data: ko.toJSON({ casenumber: $("#txtItsReferenceNumber").val() }),
            success: function (data) {
                var t = data[0].PatientFirstName + ' ' + data[0].PatientLastName;
                $("#txtPatientName").val(t);
            },
            error: function (data) {
                alert("Couldn't find data - " + data);
            }
        });
    }
    function CasePatient(caseNumber, caseID, patientFirstName, patientLastName) {
        this.CaseNumber = caseNumber;
        this.CaseID = caseID;
        this.PatientFirstName = patientFirstName;
        this.PatientLastName = patientLastName;
    }

    self.DisableAddButton = ko.observable(false);

    this.ResetDisabledAddButton = function () {
        self.DisableAddButton(false);
    };
      
     
  

    // validation

    self.ClinicalAuditFormBeforeSubmit = function (arr, $form, options) {
        if ($form.jqBootstrapValidation('hasErrors')) {
            return false;
        }
        else {
            var extention = $('input[name$="ClinicalAuditDocumentFileUpload"]').val().split('.').pop().toLowerCase();
            if (extention == "jpg" || extention == "jpeg" || extention == "doc" || extention == "docx" || extention == "tiff" || extention == 'pdf' || extention == 'tif') {
                self.DisableAddButton(true);
                return true;
            } else {
                alert("please select pdf,doc,docx,jpg,jpeg,tif or tiff file only");
              return false;
            }
        }

    }
} 