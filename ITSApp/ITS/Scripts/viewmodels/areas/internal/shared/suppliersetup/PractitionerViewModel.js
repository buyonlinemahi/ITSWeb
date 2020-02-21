

//AREA OF EXPERTIES MODEL
function AreaOfExperties(areaOfExpetiesId, areaOfExpertiseName) {
    var self = this;
    self.AreaofExpertiseID = ko.observable(areaOfExpetiesId);
    self.AreaofExpertiseName = ko.observable(areaOfExpertiseName);
    self.IsChecked = ko.observable();
};

//REGISTRATION MODEL
function PractitionerRegistration(practitionerID, practitionerRegistrationID, treatmentCategoryID, treatmentCategoryName, registrationTypeID, registrationNumber, registrationType, qualification, qualificationDate, expiryDate, yearsQualified, isActive) {
    var self = this;
    self.PractitionerID = ko.observable(practitionerID);
    self.PractitionerRegistrationID = ko.observable(practitionerRegistrationID);
    self.TreatmentCategoryID = ko.observable(treatmentCategoryID);
    self.TreatmentCategoryName = ko.observable(treatmentCategoryName);
    self.RegistrationTypeID = ko.observable(registrationTypeID);
    self.RegistrationNumber = ko.observable(registrationNumber);
    self.RegistrationTypeName = ko.observable(registrationType);
    self.Qualification = ko.observable(qualification);
    self.QualificationDate = ko.observable(qualificationDate);
    self.ExpiryDate = ko.observable(expiryDate);
    self.YearsQualified = ko.observable(yearsQualified);
    self.IsActive = ko.observable(isActive);
}
//Search Supplier Practitioner
function SupplierPractitioner(practitionerID, practitionerFiesrname, practitionerLastName, registrationNumber, registrationTypeName, treatmentcategoryname, isActive, supplierID, supplierPractitionerID, enable, supplierRegistrationId) {
    var self = this;
    self.SupplierID = ko.observable(supplierID);
    self.PractitionerRegistrationID = ko.observable(supplierRegistrationId);
    self.SupplierPractitionerID = ko.observable(supplierPractitionerID);
    self.IsActive = ko.observable(isActive);
    self.PractitionerFirstName = ko.observable(practitionerFiesrname);
    self.PractitionerLastName = ko.observable(practitionerLastName);
    self.PractitionerFullName = ko.observable();
    self.PractitionerID = ko.observable(practitionerID);
    self.RegistrationNumber = ko.observable(registrationNumber);
    self.RegistrationTypeName = ko.observable(registrationTypeName);
    self.TreatmentCategoryName = ko.observable(treatmentcategoryname);
    self.Enabled = ko.observable(enable);
    self.PractitionerFullName = ko.computed(function () {
        return self.PractitionerFirstName() + " " + self.PractitionerLastName();
    });
};

//Attached Practitioner
function AttachedPractitioner(practitionerID, practitionerFiesrname, practitionerLastName, registrationNumber, registrationTypeName, treatmentcategoryname, isActive, supplierID, supplierRegistrationId) {
    var self = this;
    self.SupplierID = ko.observable(supplierID);
    self.PractitionerRegistrationID = ko.observable(supplierRegistrationId);
    self.IsActive = ko.observable(isActive);
    self.PractitionerFirstName = ko.observable(practitionerFiesrname);
    self.PractitionerLastName = ko.observable(practitionerLastName);
    self.PractitionerFullName = ko.observable();
    self.PractitionerID = ko.observable(practitionerID);
    self.RegistrationNumber = ko.observable(registrationNumber);
    self.RegistrationTypeName = ko.observable(registrationTypeName);
    self.TreatmentCategoryName = ko.observable(treatmentcategoryname);
    self.PractitionerFullName = ko.computed(function () {
        return self.PractitionerFirstName() + " " + self.PractitionerLastName();
    });

};

function SupplierPractitionerViewModel() {
    var self = this;

    //Practitioner Table Fields
    self.PractitionerID = ko.observable();
    self.PractitionerFirstName = ko.observable();
    self.PractitionerLastName = ko.observable();

    //Practitioner Table Registration Fields
    self.PractitionerRegistrationID = ko.observable();
    self.TreatmentCategoryID = ko.observable();
    self.TreatmentCategoryName = ko.observable();
    self.RegistrationTypeID = ko.observable();
    self.RegistrationNumber = ko.observable();
    self.RegistrationTypeName = ko.observable();
    self.Qualification = ko.observable();
    self.QualificationDate = ko.observable();
    self.ExpiryDate = ko.observable();
    self.YearsQualified = ko.observable();
    self.IsActive = ko.observable();
    self.selectedItem = ko.observable();

    //Search Text
    self.PractitionerSearchText = ko.observable();
    self.Registrations = ko.observableArray([]);
    self.SelectedCritaria = ko.observable(true);
    self.CheckedSelectedUrl = ko.observable(true);
    self.SeacrhUrl = ko.observable('/SupplierPractitioner/PractitionerAutoCompleteByName');
    self.EnableSave = ko.observable(true);
    self.SupplierID = ko.observable();
    self.PractitionerArray = ko.observableArray();
    self.PractitonerTreatmentCategories = ko.observableArray([]);
    self.RegistrationTypes = ko.observableArray([]);
    self.SupplierAttachedPractitioners = ko.observableArray([]);
    self.Practitioners = ko.observableArray([]);
    //Visible Attach Link
    self.VisibleAttach = ko.observable();
    //Area of Expertise Array
    self.AreaOfExpertiesArray = ko.observableArray([]);
    self.practitionerAreaOFExperties = ko.observableArray([]);

    //Confirm box to leave the supplier practitioner tab
    this.ConfirmBox = function (data, event) {
        if (!confirm("This will take you to the Practitioner Setup Screen. If you leave, your data will not save. Do you wish to continue")) {
            event.stopImmediatePropagation();
            return false;
        }
        else {
            window.open("PractitionerSetup", "_self");
        }
    };

    //Fill the Selected Area of Experise
    this.SelectedAreaOFExperties = ko.computed(function () {
        ko.utils.arrayForEach(self.AreaOfExpertiesArray(), function (item) {
            self.practitionerAreaOFExperties.remove(item);
            if (item.IsChecked() == true)
                self.practitionerAreaOFExperties.push(item);
            else
                self.practitionerAreaOFExperties.remove(item);
        });

    });

    //Clear Registration Model
    this.ClearModel = function () {
        self.RegistrationNumber(null);
        self.Qualification(null);
        self.QualificationDate(null);
        self.ExpiryDate(null);
        self.YearsQualified(null);
    };

    //Initilized Practitioner tab
    this.InitializPractitionerTab = function (model) {
        self.SupplierID(model.SupplierID);
    };

    //load Practitioner Tab
    this.LoadTab = function () {
        self.GetTreatmentCategories();
        self.GetSupplierPractitioner();
    };

    //Update practitioner grid 
    this.UpdateGrid = function (model) {
        self.PractitionerArray([]);
        $.each(model, function (index, value) {
            self.PractitionerArray.push(new SupplierPractitioner(value.PractitionerID, value.PractitionerName, value.PractitionerRegistration, value.PractitionerRegistrationType,
            value.TreatmentCategoryID, value.IsActive, self.SupplierID(), value.TreatmentCategoryName));
        });
    }

    //METHOD FOR TO CALCULATE THE QUALIFIED YEAR
    self.QualificationDate.subscribe(function () {
        var today = new Date();
        var pQDt = new Date(self.QualificationDate());
        var dt = today.getFullYear() - pQDt.getFullYear();
        var m = today.getMonth() - pQDt.getMonth();
        if (m < 0 || (m === 0 && today.getDate() < pQDt.getDate())) {
            dt--;
        }
        self.YearsQualified(dt);
    });

    //Treatment Category Subscribe Event
    self.TreatmentCategoryID.subscribe(function (newvalue) {
        self.GetRegistrationTypes(newvalue);
        ko.utils.arrayForEach(self.PractitonerTreatmentCategories(), function (item) {
            if (item.TreatmentCategoryID == newvalue) {
                self.TreatmentCategoryName(item.TreatmentCategoryName);
            }
        });
    });
    //Registration Type ID  Subscribe Event
    self.RegistrationTypeID.subscribe(function (newvalue) {
        ko.utils.arrayForEach(self.RegistrationTypes(), function (item) {
            if (item.RegistrationTypeID == newvalue) {
                self.RegistrationTypeName(item.RegistrationTypeName);
            }
        });
    });

    //Template  method
    self.TemplateToUse = function (item) {
        return self.selectedItem() === item ? 'EditPractitionerRegistrationTemplate' : 'practitionerRegistration';
    };

    //Compare with the practitioner ragistration have treatment id and regostration type id allready exist
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

    //Edit the practitioner registration
    this.EditPractitionerRegistreationInfo = function (item) {

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
        self.TreatmentCategoryID(item.TreatmentCategoryID());
        self.TreatmentCategoryName(item.TreatmentCategoryName());
        self.GetTreatmentCategories();
        EditPactitionerCalender();
        self.EnableSave(false);
    };

    //cancel registration edit mode
    this.CancelpractitionerRegistrationEdit = function () {
        self.selectedItem(null);
        self.EnableSave(true);
        self.ClearModel();
    }

    //update practitioner grid after edit 
    this.UpdatePractitionerRegistration = function (value) {
        var selectedIndex = self.Registrations.indexOf(self.selectedItem());
        self.Registrations()[selectedIndex].TreatmentCategoryID(self.TreatmentCategoryID());
        self.Registrations()[selectedIndex].TreatmentCategoryName(self.TreatmentCategoryName());
        self.Registrations()[selectedIndex].RegistrationTypeID(self.RegistrationTypeID());
        self.Registrations()[selectedIndex].RegistrationTypeName(self.RegistrationTypeName());
        self.selectedItem(null);
        self.EnableSave(true);
        self.ClearModel();

    };
    //search categories.
    self.SearchCritaria = [{ SearchByName: 'Name', SearchCategoryID: 1, IsSearchBy: self.SelectedCritaria() }, { SearchByName: 'Treatment', SearchCategoryID: 2, IsSearchBy: false}];
    this.showModal = function () {
        $("#pratictionerPOPUP").dialog("open");
        self.GetAreaOfExperties();
        self.ClearModel();
    };

    //Change search url after the checkbox changed by name or treatment
    self.CheckedSelectedUrl.subscribe(function (newValue) {
        if (newValue == 1) {
            self.SeacrhUrl('/SupplierPractitioner/PractitionerAutoCompleteByName');
        }
        else if (newValue == 2) {
            self.SeacrhUrl('/SupplierPractitioner/PractitionerAutoCompleteByTreatmentCategoryName');
        }
    });

    //Add Practitioner Registration Temprory
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
            self.Registrations.push(new PractitionerRegistration(0, 0, self.TreatmentCategoryID(), self.TreatmentCategoryName(),
        self.RegistrationTypeID(), self.RegistrationNumber(), self.RegistrationTypeName(), self.Qualification(),
        self.QualificationDate(), self.ExpiryDate(), self.YearsQualified(), self.IsActive()));
            self.ClearModel();
        }
    }

    //Add ne Practitioner and its registration and area of expertise
    this.AddPractitioner = function () {
        var errmsg = "";
        var hasError = false;
        if (self.PractitionerFirstName() == null) {
            errmsg = "Please Enter First Name";
            hasError = true;
        }
        else if (self.PractitionerLastName() == null) {
            errmsg = "Please Enter Last Name";
            hasError = true;
        }
        else if (self.Registrations().length == 0) {
            errmsg = "Please add practitioner registration Info";
            hasError = true;
        }
        var validateAreaOFExpertise = false;
        $.each(self.AreaOfExpertiesArray(), function (index, value) {
            if (value.IsChecked() == true)
                validateAreaOFExpertise = true;
        });
        if (validateAreaOFExpertise == false) {
            alert("Please Checked at least One Area of Experties Category.");
            return false;
        }
        if (hasError == true) {
            alert(errmsg);
        }
        else {
            if (self.CompareTreatmentRegistration()) {
                $.ajax({
                    url: "/SupplierPractitioner/AddPratictioner/",
                    type: "POST",
                    data: ko.toJSON(this),
                    contentType: 'application/json',
                    success: function (model) {
                        self.PractitionerID(model);
                        self.PractitionerFirstName(null);
                        self.PractitionerLastName(null);
                        self.Registrations([]);
                        self.successMessaage();
                        self.ClearModel();
                    },
                    error: function () {
                        alert("Error occur");
                    }
                });
            }
        }
    };

    this.successMessaage = function () {
        $("#AddNewpracitioner").show();
        setTimeout(function () { $("#AddNewpracitioner").hide(); }, 5000);
    };
    //cancel to add new practitioner  confirmbox
    this.Cancel = function () {
        if (self.Registrations().length > 0 || self.PractitionerFirstName() != null || self.PractitionerLastName() != null)
            $("#CloseWithoutSaveConfirmBox").dialog("open");
        else
            $("#pratictionerPOPUP").dialog("close");
    }

    //close the add new practitioner popup  
    this.CloseWithoutSave = function () {
        self.PractitionerFirstName(null);
        self.PractitionerLastName(null);
        self.Registrations([]);
        self.ClearModel();
        $("#CloseWithoutSaveConfirmBox").dialog("close");
        $("#pratictionerPOPUP").dialog("close");
    };
    //Confirmbox cancel close without save
    this.NotCloseWithoutSave = function () {
        $("#CloseWithoutSaveConfirmBox").dialog("close");
    };

    //Remove attached practitoner
    this.RemovePractitioner = function (newvalue) {
        $.ajax({
            url: "/SupplierPractitioner/DeleteSupplierPractitionerBySupplierPractitionerID/",
            cache: false,
            type: "POST", dataType: "json",
            data: { supplierPractitionerID: newvalue.SupplierPractitionerID },
            success: function (model) {
                self.GetSupplierPractitioner();
            },
            error: function () {

            }
        });
    };

    //make enable and disable of attach button
    this.DisableEnableAttachmentOfPractitioner = ko.computed(function () {
        ko.utils.arrayForEach(self.Practitioners(), function (item) {
            item.Enabled(true);
            ko.utils.arrayForEach(self.SupplierAttachedPractitioners(), function (value) {
                if (item.PractitionerRegistrationID() == value.PractitionerRegistrationID()) {
                    item.Enabled(false);
                }

            });
        });

    });

    //search for practitioner and fill grid
    this.SearchforPractitioners = function () {
        var enabled = true;
        if (self.PractitionerSearchText() == null)
            alert("Please enter the text to search");
        else
            $("#divPractitionerSpinner").spin(true);
            $.ajax({
                url: self.SeacrhUrl(),
                cache: false,
                type: "POST", dataType: "json",
                data: { searchKey: self.PractitionerSearchText() },
                success: function (model) {
                    self.Practitioners([]);
                    $.each(model, function (index, value) {

                        self.Practitioners.push(new SupplierPractitioner(value.PractitionerID, value.PractitionerFirstName, value.PractitionerLastName, value.RegistrationNumber, value.RegistrationTypeName, value.TreatmentCategoryName
                    , value.IsActive, self.SupplierID(), 0, true, value.PractitionerRegistrationID));

                    });
                    $("#divPractitionerSpinner").spin(false);
                },
                error: function () {
                    alert("Error occure");
                    $("#divPractitionerSpinner").spin(false);
                }
            });
    };

    //attach practioner to supplier
    this.AttachPractitioner = function () {
        $.ajax({
            url: "/SupplierPractitioner/AddSupplierPractitioner/",
            type: "POST",
            data: ko.toJSON(this),
            contentType: 'application/json',
            success: function (model) {
                self.GetSupplierPractitioner();
            },
            error: function () {
            }
        });
    };

    //get all attached practitioners to supplier
    this.GetSupplierPractitioner = function () {

        $.ajax({
            url: "/SupplierPractitioner/GetSupplierPractitionerTreatmentRegistrationsBySupplierID/",
            cache: false,
            type: "POST", dataType: "json",
            data: { supplierID: self.SupplierID },
            success: function (model) {
                self.SupplierAttachedPractitioners([]);
                $.each(model, function (index, value) {
                    self.SupplierAttachedPractitioners.push(new SupplierPractitioner(value.PractitionerID, value.PractitionerFirstName,
                   value.PractitionerLastName, value.RegistrationNumber, value.RegistrationTypeName, value.TreatmentCategoryName
                    , value.IsActive, self.SupplierID(), value.SupplierPractitionerID, true, value.PractitionerRegistrationID));
                });

            },
            error: function () {

            }
        });

    };

    //get all treatment categories
    this.GetTreatmentCategories = function () {

        $.ajax({
            url: "/SupplierPractitioner/GetAllTreatmentCategories/",
            type: 'post',
            contentType: 'application/json',
            success: function (model) {
                self.PractitonerTreatmentCategories(model);
            }
        });
    };

    //get all area of experties
    this.GetAreaOfExperties = function () {
        $.ajax({
            url: "/SupplierPractitioner/GetAllAreaOfExperties/",
            type: 'post',
            contentType: 'application/json',
            success: function (model) {
                self.AreaOfExpertiesArray([]);
                $.each(model, function (index, value) {
                    self.AreaOfExpertiesArray.push(new AreaOfExperties(value.AreasofExpertiseID, value.AreasofExpertiseName));
                });

            }
        });

    };

    //get area of experties by practitioner id
    this.GetAreaOfExpertiesByPractionerID = function () {
        $.ajax({
            url: "/SupplierPractitioner/GetPractitionerAreaOfExperties/",
            cache: false,
            type: "POST", dataType: "json",
            data: { practitionerID: self.PractitionerID },
            success: function (model) {
                self.PractitionerAreaOfExperties([]);
                $.each(model, function (index, value) {
                    self.PractitionerAreaOfExperties.push(new PractitionerExperties(value.PractitionerExpertiseID, value.PractitionerID, value.AreaofExpertiseID, value.AreaofExpertiseName));
                });

            },
            error: function () {

            }
        });

    };


    //Get all registration types
    this.GetRegistrationTypes = function () {
        $.ajax({
            url: "/SupplierPractitioner/GetRegistrationTypes/",
            cache: false,
            type: "POST", dataType: "json",
            data: { treatmentCategoryID: self.TreatmentCategoryID },
            success: function (model) {
                self.RegistrationTypes(model);
            },
            error: function () {

            }
        });
    };

};

//Practitioner Header Script
var pickerOpts = {
    dateFormat: $.datepicker.ATOM,
    changeYear: true
};
$(function () {
    $("#datepicker").datepicker(pickerOpts);
    $("#datepickerExpirydate").datepicker(pickerOpts);
});

function EditPactitionerCalender() {
    $("#editExpiryDate").datepicker(pickerOpts);
};

$(document).ready(function () {
    $(function () {
        $("#pratictionerPOPUP").dialog({
            autoOpen: false,
            width: 976,
            modal: true,
            buttons: {
                Cancel: function () {
                    $(this).dialog("close");
                }
            }
        });
        $("#CloseWithoutSaveConfirmBox").dialog({
            autoOpen: false,
            width: 500,
            modal: true,
            buttons: {
                Cancel: function () {
                    $(this).dialog("close");
                }
            }
        });

    });

});