function ReferralDetailViewModel() {

    this.init = function () {
        $(".injured-party-req").hide();
        $('#PatientAge').attr("disabled", "disabled");
        $('#divSolicitor').hide();
        $('#DivSendInvoiceTo').hide();
        $('#Case_ReferrerProjectTreatmentID').attr("disabled", "disabled");
        $('#TreatmentTypes').attr("disabled", "disabled");
        $('#txtSelectedContact').attr("disabled", "disabled");
        $('#ddlUserContacts').attr("disabled", "disabled");
        $('#patientHasLegalRepID').attr("checked", "checked");
        $('#RefeerProjectUserID').attr("checked", "checked");
        $('#objPatientConsent').attr("disabled", true);

        $('#loadingImage').hide().ajaxStart(function () {
            $(this).show();
        }).ajaxStop(function () {
            $(this).hide();
        });
        self.showAdd(false);
    };

    self.PatientHasLegalRep.subscribe(function () {
        if (self.PatientHasLegalRep() === "true") {
            $('#divSolicitor').show('slow');
            self.SolicitorCompanyName.extend({ required: { message: "Solicitor Company Name is required." } });
            self.SolicitorAddress.extend({ required: { message: "Solicitor Address is required." } });
            self.SolicitorCity.extend({ required: { message: "Solicitor City is required" } });
            self.SolicitorPhone.extend({ required: { message: "Solicitor Phone is required" } });
            self.SolicitorPostCode.extend({ required: { message: "Solicitor Post Code is required" } });
            self.SolicitorReferenceNumber.extend({ required: { message: "Solicitor Reference is required" } });
        } else {
            self.SolicitorCompanyName.extend({ required: false });
            self.SolicitorAddress.extend({ required: false });
            self.SolicitorCity.extend({ required: false });
            self.SolicitorPhone.extend({ required: false });
            self.SolicitorPostCode.extend({ required: false });
            self.SolicitorReferenceNumber.extend({ required: false });

            self.SolicitorCompanyName('');
            self.SolicitorAddress('');
            self.SolicitorCity('');
            self.SolicitorRegion('');
            self.SolicitorPhone('');
            self.SolicitorEmail('');
            self.SolicitorFirstName('');
            self.SolicitorLastName('');
            self.SolicitorPostCode('');
            self.SolicitorFax('');
            self.SolicitorReferenceNumber('');
            $('#divSolicitor').hide('slow');
        }
    });
}