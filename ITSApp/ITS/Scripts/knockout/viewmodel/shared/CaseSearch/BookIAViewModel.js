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

ko.extenders.parseJsonTime = function (target) {
    var result = ko.computed({
        read: target,
        write: function (newValue) {
            var valueToWrite;
            if (target && newValue != null) {
                var formattedDate = moment(newValue).format('HH:mm');
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

function BookIAViewModel() {
    var self = this;
    self.CaseID = ko.observable();
    self.CaseIDPK = ko.observable();
    self.EnableIA = ko.observable(true);
    self.AppointmentDateTime = ko.observable();
    self.FirstAppointmentOfferedDate = ko.observable();
    self.CaseAppointmentDateAppointmentTime1 = ko.observable().extend({ parseJsonTime: null });
    self.CaseAppointmentDateAppointmentDate1 = ko.observable().extend({ parseJsonDate: null });
    self.CaseAppointmentDateAppointmentDate = ko.observable().extend({ required: { message: "Appointment Date is required."} });
    self.CaseAppointmentDateAppointmentTime = ko.observable().extend({ required: { message: "Appointment Time is required." } });
    self.CasePatientContactDates = ko.observableArray([]);
    self.CaseUnsuccessfullContactDates = ko.observableArray([]);
    self.AppointmentDateTime = ko.computed(function () {
        return self.CaseAppointmentDateAppointmentDate() + ' ' + self.CaseAppointmentDateAppointmentTime();
    });
    self.InitialFromBeforeSubmit = function (arr, $form, options) {
        if ($form.jqBootstrapValidation('hasErrors'))
            return false;
        else
            return true;
    };
    self.updateIAFormSuccess = function (responseText, statusText, xhr, $form) {
        alert("Successfully Updated");
        self.CaseID(responseText);
    };

    this.Init = function (model, CaseID, CasePatientContactDates, CaseUnsuccessfullContactDates) {
        if (model != null) {
         
            self.CaseAppointmentDateAppointmentDate1(model.AppointmentDateTime);
            self.CaseAppointmentDateAppointmentTime1(model.AppointmentDateTime);
            self.CaseAppointmentDateAppointmentDate(model.strAppointmentDate);
            self.CaseAppointmentDateAppointmentTime(model.strAppointmentTime);
            self.CaseID(model.CaseID);
            self.EnableIA(model.WorkflowID < 220);
            if (model.FirstAppointmentOfferedDate == null) {
                self.FirstAppointmentOfferedDate = ko.observable(null);
            }
            else
            {
                self.FirstAppointmentOfferedDate = ko.observable(moment(model.FirstAppointmentOfferedDate).format("DD/MM/YYYY"));
            }
        }

        if (CasePatientContactDates != null)
        {
            self.CasePatientContactDates(CasePatientContactDates);
        }
        if (CaseUnsuccessfullContactDates != null)
        {
            self.CaseUnsuccessfullContactDates(CaseUnsuccessfullContactDates);
        }
        self.CaseIDPK(CaseID);
    };
};



