
/*
*  Latest Version  : 1.0

*  Auther       : Robin Singh
*  Version      : 1.0
*  Date         : 07-March-2013
*  Description  : Created viewModel to implement Practitioner setup

*/
function AreaofExpertise(areaofExpertiseID, areaofExpertiseName, isChecked, practitinerId) {
    var self = this;
    self.AreaofExpertiseID = ko.observable(areaofExpertiseID);
    self.AreasofExpertiseName = ko.observable(areaofExpertiseName);
    self.IsChecked = ko.observable(isChecked);
    self.PractitionerID = ko.observable(practitinerId);
}
//PRACTITIONER REGISTRATION MODEL
function PractitionerRegistration(practitionerID, practitionerRegistrationID, treatmentCategoryID, treatmentCategoryName, registrationTypeID, registrationTypeName, registrationNumber, qualification, qualificationDate, expiryDate, yearsQualified, isActive) {

    var self = this;
    self.PractitionerID = ko.observable(practitionerID);
    self.PractitionerRegistrationID = ko.observable(practitionerRegistrationID);
    self.TreatmentCategoryID = ko.observable(treatmentCategoryID);
    self.TreatmentCategoryName = ko.observable(treatmentCategoryName);
    self.RegistrationTypeID = ko.observable(registrationTypeID);
    self.RegistrationTypeName = ko.observable(registrationTypeName);
    self.RegistrationNumber = ko.observable(registrationNumber);
    self.Qualification = ko.observable(qualification);
    self.QualificationDate = ko.observable(qualificationDate);
    self.ExpiryDate = ko.observable(expiryDate);
    self.YearsQualified = ko.observable(yearsQualified);
    self.IsActive = ko.observable(isActive);
}

function PractitionerSetupViewModel() {
    var self = this;
    self.Enabled = ko.observable(false);
    self.VisibleUpdate = ko.observable(false);
    self.EnabledSave = ko.observable(true);
    self.VisibleSave = ko.observable(true);
    self.TreatmentCategoryID = ko.observable();
    self.TreatmentCategoryName = ko.observable();
    self.PractitionerRegistrationID = ko.observable();
    self.selectedItem = ko.observable();
    self.PractitionerID = ko.observable();
    self.PractitionerFirstName = ko.observable();
    self.PractitionerLastName = ko.observable();
    self.Registrations = ko.observableArray([]);
    self.PractitonerTreatmentCategories = ko.observableArray([]);
    self.RegistrationTypes = ko.observableArray([]);
    self.ExpiryDate = ko.observable();
    self.YearsQualified = ko.observable();
    self.IsActive = ko.observable();
    self.practitionerRegistrations = ko.observableArray();
    self.selectedPractitionetrExperties = ko.observableArray();
    self.SeacrhUrl = ko.observable('/PractitionerSetup/PractitionerAutoCompleteByName');
    self.AreaOfExpertiseArray = ko.observableArray([]);
    self.RegistrationTypeID = ko.observable();
    self.RegistrationTypeName = ko.observable();
    self.RegistrationNumber = ko.observable();
    self.Qualification = ko.observable();
    self.QualificationDate = ko.observable();

    self.enableExpertise = ko.observable(false);
    self.Enabledpractitioner = ko.observable(false);
    self.EnabledAddRegistration = ko.observable(false);

    self.ExpiryDateEnable = "readonly";
    self.QualificationDateEnable = "readonly";

    self.QualificationDate.subscribe(function () {
        var today = new Date();
        var picQualificationDate = new Date(self.QualificationDate());
        var yearqualifiedDiff = today.getFullYear() - picQualificationDate.getFullYear();
        var month = today.getMonth() - picQualificationDate.getMonth();
        if (month < 0 || (month === 0 && today.getDate() < picQualificationDate.getDate())) {
            yearqualifiedDiff--;
        }
        self.YearsQualified(yearqualifiedDiff);
    });

    //InitializPractitionerSetupTab
    this.InitializPractitionerSetupTab = function (model) {
        ko.mapping.fromJS(model, {}, self);
        self.getPractitionerRegistration(model.PractitionerID);
        self.PractitionerFirstName(model.PractitionerFirstName);
        self.PractitionerLastName(model.PractitionerLastName);
        self.getPractitionerAreaOfExpertise(model.PractitionerID);
        self.VisibleSave(false);
        self.VisibleUpdate(true);
        self.Enabled(true);
        self.EnabledSave(true);
        self.GetTreatmentCategories();
    };
    this.LoadTab = function () {
        self.GetAreaOfExperties();
        self.EnabledSave(false);
    };
    //Method To Get Practitioner Registrations
    this.getPractitionerRegistration = function (practitionerID) {
        $.ajax({
            url: "/PractitionerSetup/GetPractitionerRegistrationByPractitionerId/",
            type: "POST",
            dataType: "json",
            data: { practitionerID: practitionerID },
            success: function (model) {
                self.Registrations([]);
                $.each(model, function (index, value) {
                    var qualificationDate = parseJsonDateString(value.QualificationDate);
                    var formatedqualificationDate = qualificationDate.getDate() + "-" + (qualificationDate.getMonth() + 1) + "-" + qualificationDate.getFullYear();

                    var expiryDate = parseJsonDateString(value.ExpiryDate);
                    var formatedExpiryDate = expiryDate.getDate() + "-" + (expiryDate.getMonth() + 1) + "-" + expiryDate.getFullYear();
                    self.Registrations.push(new PractitionerRegistration(value.PractitionerID, value.PractitionerRegistrationID, value.TreatmentCategoryID, value.TreatmentCategoryName, value.RegistrationTypeID, value.RegistrationTypeName, value.RegistrationNumber, value.Qualification, formatedqualificationDate, formatedExpiryDate, value.YearsQualified, value.IsActive));
                });
            },
            error: function () {

            }
        });
    };
    //Method To Get Practitioner AreaOfExperties By PractitionerID
    this.getPractitionerAreaOfExpertise = function () {
        $.ajax({
            url: "/PractitionerSetup/GetPractitionerAreaOfExperties/",
            cache: false,
            type: "POST", dataType: "json",
            data: { PractitionerID: self.PractitionerID },
            success: function (model) {
                ko.utils.arrayForEach(self.AreaOfExpertiseArray(), function (item) {
                    item.IsChecked(false);
                });
                $.each(model, function (index, value) {
                    ko.utils.arrayForEach(self.AreaOfExpertiseArray(), function (item) {
                        if (value.AreaofExpertiseID == item.AreaofExpertiseID()) {
                            item.IsChecked(true);
                        }
                    });
                });
            },
            error: function () {
            }
        });
    };

    //Method To Get All AreaOfExperties
    this.GetAreaOfExperties = function () {
        $.ajax({
            url: "/PractitionerSetup/GetAreaOfExperties/",
            cache: false,
            type: 'post',
            contentType: 'application/json',
            success: function (model) {
                self.AreaOfExpertiseArray([]);
                $.each(model, function (index, value) {
                    self.AreaOfExpertiseArray.push(new AreaofExpertise(value.AreasofExpertiseID, value.AreasofExpertiseName, false, 0));
                });
            },
            error: function () {
            }
        });
    };
    //Method To Get All TreatmentCategories
    this.GetTreatmentCategories = function () {
        $.ajax({
            url: "/PractitionerSetup/GetAllTreatmentCategories/",
            type: 'post',
            contentType: 'application/json',
            success: function (model) {
                self.PractitonerTreatmentCategories(model);
            }
        });
    };

    this.AddNewClick = function () {
        self.ClearModel();
        self.Enabled(true);
        self.VisibleSave(true);
        self.VisibleUpdate(false);
        self.EnabledSave(true);
        self.PractitionerFirstName(null);
        self.PractitionerLastName(null);
        self.GetTreatmentCategories();
        self.Registrations([]);
        self.GetAreaOfExperties();
        self.enableExpertise(true);
        self.Enabledpractitioner(true);
        self.EnabledAddRegistration(true);
    };

    this.ClearModel = function () {
        self.RegistrationNumber(null);
        self.Qualification(null);
        self.QualificationDate(null);
        self.ExpiryDate(null);
        self.YearsQualified(null);
    };
    //Treatment Category Subscribe Event
    self.TreatmentCategoryID.subscribe(function (newValue) {
        if (newValue != null)
            self.GetRegistrationTypesByTreatmentCatagoryId(newValue);

        ko.utils.arrayForEach(self.PractitonerTreatmentCategories(), function (item) {
            if (item.TreatmentCategoryID == newValue) {
                self.TreatmentCategoryName(item.TreatmentCategoryName);
            }
        });
    });
    //Registration Type Subscribe Event
    self.RegistrationTypeID.subscribe(function (newValue) {
        ko.utils.arrayForEach(self.RegistrationTypes(), function (item) {
            if (item.RegistrationTypeID == newValue) {
                self.RegistrationTypeName(item.RegistrationTypeName);
            }
        });
    });


    // This code is commented against task 3325...
    this.GetRegistrationTypesByTreatmentCatagoryId = function (id) {
        //$.ajax({
        //    url: "/PractitionerSetup/GetRegistrationTypes/",
        //    cache: false,
        //    type: "POST", dataType: "json",
        //    data: { treatmentCategoryID: self.TreatmentCategoryID() },
        //    success: function (model) {
        //        self.RegistrationTypes(model);
        //    },
        //    error: function () {
        //    }
        //});
    };

    //Add Practitioner Registration in a Temprory Grid 
    this.PractitionerRegistrationGrid = function () {
        var errmsg = "";
        var hasError = false;
        if (self.RegistrationNumber() == null) {
            errmsg = "Please Enter Registration Number";
            hasError = true;
        }
        else if (self.Qualification() == null) {
            errmsg = "Please Enter Qualification";
            hasError = true;
        }
        else if (self.ExpiryDate() == null) {
            errmsg = "Please Enter Expiry Date";
            hasError = true;
        }
        else if (self.QualificationDate() == null) {
            errmsg = "Please Enter Qualification Date";
            hasError = true;
        }
        if (hasError == true) {
            alert(errmsg);
        }
        else {
            self.Registrations.push(new PractitionerRegistration(self.PractitionerID, self.PractitionerRegistrationID, self.TreatmentCategoryID(), self.TreatmentCategoryName(), self.RegistrationTypeID(), self.RegistrationTypeName(), self.RegistrationNumber(), self.Qualification(), self.QualificationDate(), self.ExpiryDate(), self.YearsQualified(), self.IsActive()));
            self.ClearModel();
            self.GetTreatmentCategories();
        }
    }

    self.templateToUse = function (item) {
        return self.selectedItem() === item ? 'editpractitionerRegistrationTemplate' : 'practitionerRegistration';
    };
    this.editPractitionerRegistreationInfo = function (item) {

        self.selectedItem(item);
        self.PractitionerID(item.PractitionerID());
        self.PractitionerRegistrationID(item.PractitionerRegistrationID());
        self.RegistrationTypeID(item.RegistrationTypeID());
        self.RegistrationTypeName(item.RegistrationTypeName());
        self.RegistrationNumber(item.RegistrationNumber());
        self.Qualification(item.Qualification());
        self.QualificationDate(item.QualificationDate());
        self.ExpiryDate(item.ExpiryDate());
        self.YearsQualified(item.YearsQualified());
        self.practitionerRegistrations(self.Registrations());
        self.TreatmentCategoryID(item.TreatmentCategoryID());
        self.TreatmentCategoryName(item.TreatmentCategoryName());
        self.GetTreatmentCategories();
        bindCalenderToEditTemplate();
        self.Enabled(false);
        self.EnabledSave(false);
        self.EnabledAddRegistration(false);
    };

    this.CancelpractitionerRegistrationGrid = function (item) {
        self.selectedItem(null);
        self.EnabledSave(true);
        self.ClearModel();
        self.Enabled(true);
        self.EnabledAddRegistration(true);
        self.GetTreatmentCategories();
    };

    this.UpdatepractitionerRegistrations = function (item) {
        var selectedIndex = self.Registrations.indexOf(self.selectedItem());
        self.Registrations()[selectedIndex].TreatmentCategoryID(self.TreatmentCategoryID());
        self.Registrations()[selectedIndex].TreatmentCategoryName(self.TreatmentCategoryName());
        self.Registrations()[selectedIndex].RegistrationTypeID(self.RegistrationTypeID());
        self.Registrations()[selectedIndex].RegistrationTypeName(self.RegistrationTypeName());
        self.selectedItem(null);
        self.EnabledSave(true);
        self.Enabled(true);
        self.EnabledAddRegistration(true);
        self.ClearModel();

    };

    this.CompareTreatmentRegistration = function () {
        if (self.Registrations().length > 1) {
            for (var i = 0; i < self.Registrations().length; i++) {
                for (var j = i + 1; j < self.Registrations().length; j++) {
                    if (self.Registrations()[i].TreatmentCategoryID() === self.Registrations()[j].TreatmentCategoryID()) {
                        if (self.Registrations()[i].RegistrationTypeID() === self.Registrations()[j].RegistrationTypeID()) {
                            alert("Practitioner Registration Type already exists, please select different Registration Type or Treatment Category");
                            return false;
                        }
                        else { return true; }
                    }
                    else { return true; }
                }
            }
        }
        else {
            return true;
        }
    }
    this.AddPractitioner = function () {
        var errmsg = "";
        var hasError = false;
        if (self.PractitionerFirstName() == null || self.PractitionerFirstName() == '') {
            errmsg = "Please Enter Practitioner FirstName";
            hasError = true;
        }
        if (self.PractitionerLastName() == null || self.PractitionerLastName() == '') {
            errmsg = "Please Enter Practitioner LastName";
            hasError = true;
        }
        if (self.Registrations() == 0) {
            errmsg = "Please Add Practitioner Registration Information";
            hasError = true;
        }
        var validateAreaOFExpertise = false;
        $.each(self.AreaOfExpertiseArray(), function (index, value) {
            if (value.IsChecked() == true)
                validateAreaOFExpertise = true;
        });
        if (validateAreaOFExpertise == false) {
            alert("Please Check at least One Area of Experties Category.");
            return false;
        }
        if (hasError == true) {
            alert(errmsg);
        }
        else {
            if (self.CompareTreatmentRegistration()) {
                self.practitionerRegistrations(self.Registrations());
                ko.utils.arrayForEach(self.AreaOfExpertiseArray(), function (item) {
                    var value = item.IsChecked();
                    if (value) {
                        self.selectedPractitionetrExperties.push(new AreaofExpertise(item.AreaofExpertiseID(), item.AreasofExpertiseName, true, item.PractitionerID));
                    }
                });

                $.ajax({
                    url: "/PractitionerSetup/AddPratictioner/",
                    type: "POST",
                    data: ko.toJSON(this),
                    contentType: 'application/json',
                    success: function (model) {
                        self.PractitionerID(model);
                        self.selectedPractitionetrExperties([]);
                        self.Registrations([]);
                        self.PractitionerFirstName(null);
                        self.PractitionerLastName(null);
                        $("#lblParctitoner").text("Practitioner Successfully Added");
                        $("#lblParctitoner").show();
                        setInterval(function () { $("#lblParctitoner").hide(); }, 5000);
                        self.ClearModel();
                        self.GetAreaOfExperties();
                        self.Enabled(false);
                        self.enableExpertise(false);
                        self.EnabledAddRegistration(true);
                    },
                    error: function () {

                    }
                });
            }
        }
    };

    this.UpdatePractitioner = function () {
        var validateAreaOFExpertise = false;
        $.each(self.AreaOfExpertiseArray(), function (index, value) {
            if (value.IsChecked() == true)
                validateAreaOFExpertise = true;
        });
        if (validateAreaOFExpertise == false) {
            alert("Please Check at least One Area of Experties Category.");
            return false;
        }
        else {
            if (self.CompareTreatmentRegistration()) {
                self.practitionerRegistrations(self.Registrations());
                ko.utils.arrayForEach(self.AreaOfExpertiseArray(), function (item) {
                    var value = item.IsChecked();
                    if (value) {
                        self.selectedPractitionetrExperties.push(new AreaofExpertise(item.AreaofExpertiseID(), item.AreasofExpertiseName, true, self.PractitionerID));
                    }
                });
                $.ajax({
                    url: "/PractitionerSetup/UpdatePratictioner/",
                    type: "POST",
                    data: ko.toJSON(this),
                    contentType: 'application/json',
                    success: function (model) {
                        $("#lblParctitoner").text("Practitioner Successfully Updated");
                        $("#lblParctitoner").addClass("message");
                        $("#lblParctitoner").slideUp(5000);
                    },
                    error: function () {
                    }
                });
            }
        }
    };
};

