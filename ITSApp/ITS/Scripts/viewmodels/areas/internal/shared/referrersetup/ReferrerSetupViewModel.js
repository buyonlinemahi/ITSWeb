/*
*  Latest Version  : 1.0


*  Modified by  : Harpreet Singh
*  Version      : 1.0
*  Date         : 14-Nov-2012
*  Description  : Updated viewModel to implement the changes to add the referrer and referrer location

*  Modified by  : Harpreet Singh
*  Version      : 1.0
*  Date         : 15-Nov-2012
*  Description  : Updated viewModel to implement and applyed validation's

*  Modified by  : Harpreet Singh
*  Version      : 1.0
*  Date         : 05-April-2013
*  Description  : Updated viewModel to implement and applyed validation's


*/

function ReferrerControlViewModel() {
    var self = this;
    self.ReferrerID = ko.protectedObservable('');
    self.ReferrerName = ko.protectedObservable('');
    self.ReferrerContactFirstName = ko.protectedObservable('');
    self.ReferrerContactLastName = ko.protectedObservable('');
    self.ReferrerMainContactEmail = ko.protectedObservable('');
    self.ReferrerLocationID = ko.protectedObservable('');
    self.Name = ko.protectedObservable('');
    self.Address = ko.protectedObservable('');
    self.City = ko.protectedObservable('');
    self.Region = ko.protectedObservable('');
    self.PostCode = ko.observable('');
    self.ReferrerMainContactPhone = ko.observable().extend({ number: { params: true, message: 'Please enter only a number in Main Contact Telephone'} });
    self.ReferrerMainContactFax = ko.observable('').extend({ number: { params: true, message: 'Please enter only a number in Main Contact Fax'} });
    self.IsMainOffice = ko.protectedObservable('');
    self.IsActive = ko.observable(true);
   
   
    self.EnableAddNew = ko.observable(true);
    self.EnableAmmend = ko.observable(false);
    self.EnableCancel = ko.observable(false);
    self.EnableSave = ko.observable(false);
    self.EnableUpdate = ko.observable(true);
    self.VisibleUpdate = ko.observable(false);
    self.VisibleCancel = ko.observable(false);
    self.VisibleSave = ko.observable(true);
    self.VisibleAmmend = ko.observable(true);   
    self.CancelEditReferrer = ko.observableArray([]);
    self.CancelEditReferrerLocation = ko.observableArray([]);
    

    self.errors = ko.validation.group(self);


    this.disableInputControls = function () {
        $(".referrer-cantainer input[type='text']").prop("disabled", true);
    };
    this.clearModelValues = function () {

        self.ReferrerID(null);
        self.ReferrerName(null);
        self.ReferrerContactFirstName(null);
        self.ReferrerContactLastName(null); 
        self.ReferrerMainContactEmail(null); 
        self.Name(null);
        self.Address(null);
        self.City(null);
        self.Region(null);
        self.PostCode(null);
        self.ReferrerMainContactPhone(null);
        self.ReferrerMainContactFax(null);
        self.IsMainOffice(null);
        self.commitModel();
    };

    this.updateModelReferrer = function (model) {
        if (model != null) {
            self.CancelEditReferrer(model);
            self.EnableAmmend(true);         
            self.VisibleCancel(false);
            self.VisibleSave(true);
            self.EnableAddNew(true);
            self.EnableSave(false);
            self.EnableAmmend(true);
            self.VisibleAmmend(true);
            self.VisibleUpdate(false);
            self.ReferrerID(model.ReferrerID);
            self.ReferrerLocationID(model.ReferrerLocationID);
            self.ReferrerName(model.ReferrerName);
            self.ReferrerContactFirstName(model.ReferrerContactFirstName);
            self.ReferrerContactLastName(model.ReferrerContactLastName);         
            self.ReferrerMainContactEmail(model.ReferrerMainContactEmail);
            self.ReferrerMainContactPhone(model.ReferrerMainContactPhone);
            self.ReferrerMainContactFax(model.ReferrerMainContactFax);
            self.ReferrerID.commit();
            self.ReferrerName.commit();
            self.ReferrerContactFirstName.commit();
            self.ReferrerContactLastName.commit();         
            self.ReferrerMainContactEmail.commit();
            //self.ReferrerMainContactPhone.commit();
            //self.ReferrerMainContactFax.commit();
        }
    };

    this.updateModelReferrerLocation = function (model) {
        if (model != null) {
            self.CancelEditReferrerLocation(model);
            self.Name(model.Name);
            self.Address(model.Address);
            self.ReferrerLocationID(model.ReferrerLocationID);
            self.City(model.City);
            self.Region(model.Region);
            self.PostCode(model.PostCode);
            self.IsMainOffice(model.IsMainOffice);
            self.Name.commit();
            self.Address.commit();
            self.City.commit();
            self.Region.commit();
            //self.PostCode.commit();
            self.IsMainOffice.commit();
            self.ReferrerLocationID.commit();
        }
    };

    this.commitModel = function () {
        self.ReferrerID.commit();
        self.ReferrerName.commit();
        self.ReferrerContactFirstName.commit();
        self.ReferrerContactLastName.commit();     
        self.ReferrerMainContactEmail.commit();
        self.Name.commit();
        self.Address.commit();
        self.City.commit();
        self.Region.commit();
        //self.PostCode.commit();
        //self.ReferrerMainContactPhone.commit();
        //self.ReferrerMainContactFax.commit();
        self.IsMainOffice.commit();
        self.ReferrerLocationID.commit();

    };
    this.makeAddable = function () {

        self.ReferrerID(null);
        self.ReferrerName(null);
        self.ReferrerContactFirstName(null);
        self.ReferrerContactLastName(null);  
        self.ReferrerMainContactEmail(null);
        self.Name(null);
        self.Address(null);
        self.City(null);
        self.Region(null);
        self.PostCode(null);
        self.ReferrerMainContactPhone(null);
        self.ReferrerMainContactFax(null);
        self.IsMainOffice(null);
        $(".referrer-cantainer input[type='text']").prop("disabled", false);
        $(".referrer-cantainer input[type='text']").val("");
        self.disableProjectLocationTabs();

        self.VisibleCancel(true);
        self.VisibleUpdate(false);
        self.EnableCancel(true);
        self.VisibleSave(true);
        self.EnableSave(true);
        self.EnableAddNew(false);
        self.VisibleAmmend(false);     

        clearProjectAndOfficeGrid();
        self.CancelEditReferrer(null);
        self.CancelEditReferrerLocation(null);
        self.commitModel();
    };
    this.resetAll = function () {
        self.ReferrerID.reset();
        self.ReferrerName.reset();
        self.ReferrerContactFirstName.reset();
        self.ReferrerContactLastName.reset();  
        self.ReferrerMainContactEmail.reset();
        self.Name.reset();
        self.Address.reset();
        self.City.reset();
        self.Region.reset();
        //self.PostCode.reset();
        //self.ReferrerMainContactPhone.reset();
        //self.ReferrerMainContactFax.reset();
        self.IsMainOffice.reset();

    };

    this.makeCancel = function () {
        self.enableProjectLocationTabs();
        self.updateModelReferrer(self.CancelEditReferrer());
        self.updateModelReferrerLocation(self.CancelEditReferrerLocation());  
        self.EnableAmmend(true);
        self.VisibleAmmend(true);
        self.VisibleCancel(false);
        self.VisibleUpdate(false);
        self.EnableAddNew(true);
        self.VisibleSave(true);
        self.resetAll();
        self.commitModel();
        self.EnableSave(false);
        if (self.Name() == null || self.Name() == "")
            self.EnableAmmend(false)
        else
            self.EnableAmmend(true);
        $(".referrer-cantainer input[type='text']").css("background", "#fff");
        $("#lblMessage").hide();
        $("#autoSeacrh").val('');
        // clearProjectAndOfficeGrid();
        $(".referrer-cantainer input[type='text']").prop("disabled", true);

        self.clearModelValues();


    }

    this.makeEditable = function () {
 
        self.EnableCancel(true);
        self.EnableAmmend(false);
        self.VisibleAmmend(false);
        self.VisibleCancel(true);
        self.VisibleUpdate(true);
        self.VisibleSave(false);
        self.EnableUpdate(true);
        $(".referrer-cantainer input[type='text']").prop("disabled", false);

    };
    this.UpdateReferrer = function () {
        self.commitModel();
        if (!self.validateModel()) {

        }
        else {
            $(".referrer-cantainer input[type='text']").prop("disabled", false);
            $("#btnSave").prop("disabled", false);
            $.ajax({
                url: "/Referrer/UpdateReferrer/",
                type: 'post',
                data: ko.toJSON(this),
                contentType: 'application/json',
                success: function (result) {
                    $(".referrer-cantainer input[type='text']").prop("disabled", true);
                    $("#lblMessage").text("Referrer successfully updated to Referrer Portal");
                    self.EnableUpdate(false);
                    self.VisibleCancel(false);
                    self.VisibleAmmend(true);
                    self.EnableAmmend(true);
                    self.EnableEmailoptions(false);
             
                    self.CancelEditReferrer(result.Referrer);
                    self.CancelEditReferrerLocation(result.ReferrerLocation);
                    $("#lblMessage").addClass("message");
                    setTimeout(function () {
                        $("#lblMessage").slideUp("slow");
                    }, 5000);
                }
            });
        }
    };

    this.disableProjectLocationTabs = function () {
        $('#tabs').tabs('disable', '#tabs-1');
        $('#tabs').tabs('disable', '#officeLocationTab');
        $('#refrerrerProjects').hide();
        $('#Divoffice').hide();
    };
    this.enableProjectLocationTabs = function () {
        $('#tabs').tabs('enable', '#tabs-1');
        $('#tabs').tabs('enable', '#officeLocationTab');
        $('#refrerrerProjects').show();
        $('#Divoffice').show();
    };

    this.validateModel = function () {
        $("#lblMessage").show();
        var emailReg = /^([\w-\.]+@([\w-]+\.)+[\w-]{2,4})?$/;
        var errmsg = "";

        var hasError = false;


        if (self.ReferrerName() == null || self.ReferrerName() == "") {
            hasError = true;
            errmsg = "Referrer Name is required Field";
            $("#referrerName").css("background", "pink");
        } else if (self.Address() == null || self.Address() == "") {
            hasError = true;
            errmsg = "Referrer Address is required Field";
            $("#referrerAddress").css("background", "pink");
        } else if (self.Region() == null || self.Region() == "") {
            hasError = true;
            errmsg = "Referrer Head Office Region is required Field";
            $("#referrerHeadOfficeRegion").css("background", "pink");
        } else if (self.ReferrerContactFirstName() == null || self.ReferrerContactFirstName() == null) {
            hasError = true;
            errmsg = "Referrer Contact First Name is required Field";
            $("#referrerContactFirstName").css("background", "pink");
        } else if (self.ReferrerContactLastName() == null || self.ReferrerContactLastName() == "") {
            hasError = true;
            errmsg = "Referrer Contact Surname is required Field";
            $("#referrerContactSurname").css("background", "pink");
        } else if (self.ReferrerMainContactPhone() == null || self.ReferrerMainContactPhone() == "") {
            hasError = true;
            errmsg = "Referrer Telephone is required Field";
            $("#referrerTelephone").css("background", "pink");
        } else if (self.Name() == null || self.Name() == "") {
            hasError = true;
            errmsg = "Referrer Location Name is required Field";
            $("#referrerLocationName").css("background", "pink");
        } else if (self.City() == null || self.City() == "") {
            hasError = true;
            errmsg = "Referrer City is required Field";
            $("#referrerCity").css("background", "pink");
        } else if (self.PostCode() == null || self.PostCode() == "") {
            hasError = true;
            errmsg = "Referrer Post Code is required Field";
            $("#referrerPostCode").css("background", "pink");
        } 

         else if (self.isValid() == false) {
            hasError = true;
            $.each(self.errors(), function (index, value) {
                errmsg = value();
                return false;
            });
        }

        if (hasError) {
            $("#lblMessage").text(errmsg);
            $("#lblMessage").addClass("lblErrorCls");
            return false;
        }
        return true;
    };

    //    this.ValidateNumeric = function () {
    //        var errors = "Errors ";
    //        if (!self.isValid()) {
    //            $.each(self.errors(), function (index, value) {
    //                errors = errors + ' ' + value();
    //            });
    //            alert(errors);
    //            return false;
    //        }
    //        else {
    //            return true;
    //        }
    //    }

    this.addReferrer = function () {
        self.commitModel();

        if (!self.validateModel()) {

        }
        else {
            self.EnableSave(false);
            $.ajax({
                url: "/Referrer/AddReferrer/",
                type: 'post',
                data: ko.toJSON(this),
                contentType: 'application/json',
                success: function (result) {
                    $("#lblMessage").text("Referrer successfully added to Referrer Portal");
                    $(".referrer-cantainer input[type='text']").val("");
                    self.makeCancel();
                    $("#lblMessage").addClass("message");
                    $("#lblMessage").show('', function () {
                        window.setTimeout(function () {
                            $("#lblMessage").hide('clip');
                        }, 3000);
                    });
                    updateOfficeLocationGrid(result.ReferrerID);
                    self.enableProjectLocationTabs();

                }
            });
        }
    };
};

