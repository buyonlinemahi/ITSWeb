function AcceptAndReferViewModel(model) {
    var self = this;

    self.Age = ko.observable();
    self.CaseID = ko.observable();
    self.SelectedSupplier = ko.observable();

    self.SelectedSupplierID = ko.observable();



    self.SelectedSupplierID.subscribe(function (value) {
        if ($("#ddlSupplierID").val() != '') {
            $.post("/Referral/SelectedSupplierDistanceRankPrice", {
                supplierID: value, treatmentCategoryID: self.CasePatientTreatment.TreatmentCategoryID(),
                postCode: self.CasePatientTreatment.PostCode()
            }, "json").done(function (response) {

                self.SelectedSupplier(response);

            });
        } else {
            self.SelectedSupplier(null);
        }

    });
    var mapping = {
        'BirthDate': {
            create: function (options) { if (options.data != null) return moment(options.data).format("DD/MM/YYYY"); }
        },
        'InjuryDate': {
            create: function (options) { if (options.data != null) return moment(options.data).format("DD/MM/YYYY"); }
        }
    };

    self.Init = function (model) {
        if (model != null) {
            ko.mapping.fromJS(model, mapping, self);
            $("#CaseIDForGeneratePatientReferrer").val(model.CasePatientTreatment.CaseID);
        }

        if (self.CasePatientTreatment.BirthDate != null) {
            var today = new Date();
            var birthDate = new Date(self.CasePatientTreatment.BirthDate);
            var dt = today.getFullYear() - birthDate.getFullYear();
            var m = today.getMonth() - birthDate.getMonth();
            if (m < 0 || (m === 0 && today.getDate() < birthDate.getDate())) {
                dt--;
            }
            self.Age(dt);
        }
    };

    self.UpdateAcceptAndReferFormSucess = function (item) {
        if ($("#ddlSupplierID").val() != '') {
            $("#loader-main-div").removeClass("hidden");
            //set timeout to accomodate download functionality of pdf letter.
            setTimeout(function () {
                alert($.parseJSON(item));
                window.location = '/Referral/Index';
            }, 2000)
        }
        else {
            alert('Please select Supplier');
            return false;
        }
    };

    self.SubmitGenerateLetterForm = function () {
     
        $("#loader-main-div").addClass("hidden");
        $("#frmGenerateLetters").submit();        
    }

    self.SaveButtonDisable = ko.observable(false);
    self.SaveValidatedClick = function ($form, event) {
      self.SaveButtonDisable(true);
    };
};