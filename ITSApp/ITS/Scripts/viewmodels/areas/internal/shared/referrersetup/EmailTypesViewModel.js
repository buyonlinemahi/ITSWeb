

function EmailTypesService(emailTypeID, emailValueTypeId, referrerProjectTreatmentID, emailTypeName, emailValueTypes) {
    var self = this;
    self.EmailTypeID = ko.observable(emailTypeID);
    self.EmailTypeValueID = ko.observable(emailValueTypeId);
    self.ReferrerProjectTreatmentID = ko.observable(referrerProjectTreatmentID);
    self.EmailTypeName = ko.observable(emailTypeName);
    self.EmailValueTypes = ko.observableArray(emailValueTypes);
}

function EmailType(typeID, emailType) {
    var self = this;
    self.TypeID = typeID;
    self.EmailType = emailType;
    self.IsEnabled = ko.computed(function () {
        return self.EmailType != 'Custom';
    });
}

function EmailTypesViewModel() {
    var self = this;
    self.ReferrerProjectTreatmentID = ko.observable('');
    self.EmailValueTypes = [new EmailType(1, "Default"), new EmailType(2, "Custom"), new EmailType(3, "None")];
    //self.Emails = ko.observableArray([]);
    self.ReferrerEmails = ko.observableArray([]);
    self.SupplierEmails = ko.observableArray([]);
    self.ViewEnabled = ko.observable();
    self.Emails = ko.computed(function () {
        return self.ReferrerEmails().concat(self.SupplierEmails());
    });
    self.notAvailable = ko.observable(false);

    this.InitializeEmailSetUp = function (referrerProjectTreatment) {

        self.ReferrerProjectTreatmentID(referrerProjectTreatment.ReferrerProjectTreatmentID);
        self.ReferrerEmails.removeAll();
        self.SupplierEmails.removeAll();
        //self.Emails.destroyAll();
        this.GetAllEmailService();
    };
    this.EnableDisable = function (flag) {
        self.ViewEnabled(flag);

    };
    this.GetAllEmailService = function () {

        $.ajax({
            url: "/ReferrerEmailSetup/GetAllReferrerProjectTreatmentEmailsByReferrerProjectTreatmentID/",
            type: 'post',
            cache: false,
            dataType: 'json',
            data: { referrerProjectTreatmentID: self.ReferrerProjectTreatmentID() },
            success: function (services) {
                $.each(services, function (index, value) {
                    if (services[index].UserTypeID == 2)
                        self.ReferrerEmails.push(new EmailTypesService(services[index].EmailTypeID, services[index].EmailTypeValueID, self.ReferrerProjectTreatmentID(), services[index].EmailTypeName, self.EmailValueTypes));
                    else
                        self.SupplierEmails.push(new EmailTypesService(services[index].EmailTypeID, services[index].EmailTypeValueID, self.ReferrerProjectTreatmentID(), services[index].EmailTypeName, self.EmailValueTypes));
                });

            }
        });
    };

    this.UpdateEmailstypes = function () {
        $.ajax({
            url: "/ReferrerEmailSetup/UpdateReferrerProjectTreatmentEmailSetUp/",
            type: 'post',
            data: ko.toJSON(self.Emails),
            contentType: 'application/json',
            success: function (result) {
                alert(result);
            }
        });
    };

};