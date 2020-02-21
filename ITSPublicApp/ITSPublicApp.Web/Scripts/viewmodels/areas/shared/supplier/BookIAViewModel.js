ko.extenders.changeNullTo = function (target, option) {
    var result = ko.computed({
        read: target,
        write: function (newValue) {
            var valueToWrite;
            if (newValue !== null) {
                valueToWrite = newValue;
            } else {
                valueToWrite = option;
            }
            target(valueToWrite);
        }
    });
    result(target());
    return result;
};

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

function BookIAViewModel() {
    var self = this;
    self.PatientContactAttempts = ko.observableArray();
    self.CaseID = ko.observable();
    self.PatientID = ko.observable();
    self.Title = ko.observable().extend({ changeNullTo: '--' });
    self.FirstName = ko.observable();
    self.LastName = ko.observable();
    self.Address = ko.observable();
    self.Region = ko.observable();
    self.PostCode = ko.observable();
    self.InjuryDate = ko.observable().extend({ parseJsonDate: null });
    self.BirthDate = ko.observable().extend({ changeNullTo: '--', parseJsonDate: null });
    self.ContactAttemptDate = ko.observable().extend({ ChangeNullTo: '--', parseJsonDate: null });
    self.CasePatientContactAttemptID = ko.observable();
    self.HomePhone = ko.observable().extend({ changeNullTo: '--' });
    self.WorkPhone = ko.observable().extend({ changeNullTo: '--' });
    self.MobilePhone = ko.observable().extend({ changeNullTo: '--' });
    self.Gender = ko.observable();
    self.ContactAttemptDate = ko.observable();
    self.IsFileExist = ko.observable();
    self.IsFirstAppointmentOffered = ko.observable();
    self.InnovateNote = ko.observable();
    self.PatientAge = ko.computed({
        read: function () {
            if (self.BirthDate() != '--') {
                var today = new Date();
                var birthDate = new Date(self.BirthDate());
                var age = today.getFullYear() - birthDate.getFullYear();
                var m = today.getMonth() - birthDate.getMonth();
                if (m < 0 || (m === 0 && today.getDate() < birthDate.getDate())) {
                    age--;
                }
                return age;
            } else {
                return '--';
            }
        },
        owner: this
    });

    self.Email = ko.observable().extend({ changeNullTo: '--' });

    self.CaseDateOfInquiry = ko.observable().extend({ changeNullTo: '--', parseJsonDate: null });
    self.CaseNumber = ko.observable().extend({ changeNullTo: '--' });

    self.CasePatientContactAttempts = ko.observableArray([]);
    self.CaseAppointmentDate = ko.observable();

    self.CasePatientContactAttemptAttemptDate = ko.observable();
    self.PatientContactDate = ko.observable().extend({ required: { message: "Patient Contact Date is required." } });

    self.CaseAppointmentDateAppointmentDate = ko.observable().extend({ required: { message: "Appointment Date is required." } });
    self.CaseAppointmentDateAppointmentTime = ko.observable().extend({ required: { message: "Appointment Time is required." } });
    self.CaseAppointmentDateFirstAppointmentOfferedDate = ko.observable().extend({ required: { message: "First Appointment Offered Date is required." } });
    self.CaseAppointmentDate = ko.computed(function () {
        return self.CaseAppointmentDateAppointmentDate() + ' ' + self.CaseAppointmentDateAppointmentTime();
    });
   
    self.ReferralDownloadPath = ko.observable();

    self.errors = ko.validation.group(self);

    self.SupplierAsignedUser = ko.observable();
    self.SupplierAsignedUserID = ko.observable();
    self.SupplierAsignedUsers = ko.observableArray();
    self.selectedItem = ko.observable(0);
    var mappingOptions = {
        'ContactAttemptDate': {
            create: function (options) { return moment(options.data).format("DD/MM/YYYY"); }
        }
    };
    this.Init = function (caseID) {
        $.post('/Supplier/BookIA/GetBookIA', { caseID: caseID },
            function (resp) {
               
                ko.mapping.fromJS(resp.CasePatient, {}, self);
                ko.mapping.fromJS(resp.CasePatientContactAttempts, mappingOptions, self.PatientContactAttempts);
                self.ReferralDownloadPath(resp.ReferralDownloadPath);
                self.SupplierAsignedUsers(resp.SuppliarAssignedUsers.slice());
                //self.selectedItem(resp.SupplierAssignedUserID);
                self.IsFileExist(resp.IsFileExist);
                self.IsFirstAppointmentOffered(resp.IsFirstAppointmentOffered);
                self.InnovateNote(resp.InnovateNote);
            });
    };
  
    this.SaveCasePatientContactAttempt = function (value) {
       
        if (self.CasePatientContactAttemptAttemptDate() != undefined && self.CasePatientContactAttemptAttemptDate() != '') {
            var record = new CasePatientContactAttempt(0, value.CaseID(), value.PatientID(), value.CasePatientContactAttemptAttemptDate());
            self.CasePatientContactAttempts.push(record);
            self.CasePatientContactAttemptAttemptDate('');
        } else {
            alert('Please add First Attempt / Unsuccessful Call Date.');
            return false;
        }
        
        $.ajax({
            type: "post",
            url: '/Supplier/BookIA/SaveUnsuccessfullDates',
            data: JSON.stringify({ CasePatientContactAttempts: record }),
            contentType: 'application/json',
            dataType: 'json',
            success: function (viewModel)
            {
                self.PatientContactAttempts.removeAll();
                ko.mapping.fromJS(viewModel.CasePatientContactAttempts, mappingOptions, self.PatientContactAttempts);
                
            }
        });
        
    };

    this.RemoveCasePatientContactAttempt = function (value) {
        var CaseContactAttemptID = value.CasePatientContactAttemptID();
         $.ajax({
            type: "post",
            url: '/Supplier/BookIA/DeleteUnsuccessfullDates',
            data: JSON.stringify({ _CaseContactAttemptID: value.CasePatientContactAttemptID(),_CaseID:value.CaseID()}),
            contentType: 'application/json',
            dataType: 'json',
            success: function (viewModel)
            {
                self.PatientContactAttempts.removeAll();
                ko.mapping.fromJS(viewModel.CasePatientContactAttempts, mappingOptions, self.PatientContactAttempts);

            }
            
        });
         
    };

    self.PatientContactAttempts.subscribe(function () {
        if (self.PatientContactAttempts() != null && self.PatientContactAttempts().length > 0) {
            $('#GridUnsuccessfulCallsMessage').hide();
        } else {
            $('#GridUnsuccessfulCallsMessage').show();
        }
    });

    this.Confirm = function () {
        $("#loader-main-div").removeClass("hidden");
        if (!self.isValid()) {
            var errors = "Errors ";
            $.each(self.errors(), function (index, value) {
                errors = errors + ' ' + value();
            });
            alert(errors);
            $("#loader-main-div").addClass("hidden");
            return false;
        }
        if ($("#ddlSupplierAsignedUserID").val() == 0)
        {
            alert("Please select Asigned User.");
            $("#loader-main-div").addClass("hidden");
            return false;
        }

        var caseAppointmentDate = new CaseAppointmentDate(self.CaseID(), self.CaseAppointmentDate(), self.CaseAppointmentDateFirstAppointmentOfferedDate());

        var json = JSON.stringify({
            CaseID: self.CaseID(),
            SuppliarAssignedUserID: $("#ddlSupplierAsignedUserID").val(),
            CasePatientContactAttempts: self.CasePatientContactAttempts(),
            CaseAppointmentDate: caseAppointmentDate,
            PatientContactDate: self.PatientContactDate(),
            InnovateNote: $("#txtInnovateNote").val()
        });

        $.ajax({
            type: "post",
            url: '/Supplier/BookIA/Confirm',
            data: json,
            contentType: 'application/json',
            dataType: 'json',
            success: function (data) {
                if (data == 1) {
                    alert('Successfully Booked');
                    $("#loader-main-div").addClass("hidden");
                    window.location.href = "/Supplier/Home";
                }
            },
            error: function () {
                alert("Some error occrured.");
                $("#loader-main-div").addClass("hidden");
            }
        });
    };

    this.Cancel = function () {
        if (confirm('Are you sure you want to cancel?')) {
            window.location.href = "/Supplier/NewPatients";
        }
    };

    
}

function CasePatientContactAttempt(CasePatientContactAttemptID,caseID, patientID, contactAttemptDate) {
    this.CasePatientContactAttemptID = ko.observable(CasePatientContactAttemptID);
    this.CaseID = caseID;
    this.PatientID = patientID;
    this.ContactAttemptDate = contactAttemptDate;
}

function CaseAppointmentDate(caseID, appointmentDateTime, firstAppointmentOfferedDate) {
    this.CaseID = caseID;
    this.AppointmentDateTime = appointmentDateTime;
    this.FirstAppointmentOfferedDate = firstAppointmentOfferedDate;
}