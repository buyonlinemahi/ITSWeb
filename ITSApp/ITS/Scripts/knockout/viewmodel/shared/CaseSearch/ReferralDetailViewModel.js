function AddReferralViewModel(modelReferrer) {
    ko.validation.init({ insertMessages: false });
    var self = this;

    //property patients   
    self.CaseID = ko.observable();
    self.InjuryType = ko.observable();
    self.Patient = ko.observable();
    self.PatientID = ko.observable();
    self.PatientTitle = ko.observable();
    self.PatientFirstName = ko.observable();//.extend({ required: { message: "Patient's First name is required." } });
    self.PatientLastName = ko.observable();//.extend({ required: { message: "Patient's Surname is required." } });
    self.PatientAddress = ko.observable();//.extend({ required: { message: "Patient's Address is required." } });
    self.PatientCity = ko.observable();//.extend({ required: { message: "Patient's City is required." } });
    self.PatientRegion = ko.observable();//.extend({ required: { message: "Patient's Region is required." } });
    self.PatientPostCode = ko.observable();//.extend({ required: { message: "Patient's Post Code is required." } });
    self.PatientInjuryDate = ko.observable();//.extend({ required: { message: "Patient's Date of Injury is required." } });
    self.PatientBirthDate = ko.observable();//.extend({ required: { message: "Patient's Date of Birth is required." } });
    self.PatientHomePhone = ko.observable();//.extend({ required: { message: "Patient's Main Phone is required." } });
    self.PatientWorkPhone = ko.observable();
    self.PatientMobilePhone = ko.observable();
    self.PatientGenderID = ko.observable();
    self.ReferrerDocumentTypes = ko.observableArray([]);
    self.SpecialInstructionNotes = ko.observable();
    self.EmploymentId = ko.observable();
    self.EmploymentTypes = ko.observableArray([]);
    self.HasInjuredPartyRepresentative = ko.observable();
    self.PolicieReferenceNo = ko.observable();
    self.TypeCoverId = ko.observable();
    self.PolicyCriteriaId = ko.observable();
    self.AdmittedId = ko.observable();
    self.BenefitDate = ko.observable();
    self.RehabProportionateBenefit = ko.observable();
    self.EndBenefitDate = ko.observable();
    self.MonthlyValue = ko.observable();
    self.WeeklyValue = ko.observable();
    self.FitForWorkId = ko.observable();
    self.PolicyWording = ko.observable();
    self.NameOfReinsurer = ko.observable();
    self.SelectedPolicyTypeID = ko.observable();
    self.PolicyID = ko.observable();
    self.ReInsuredId = ko.observable();
    self.EmploymentTypeId = ko.observable();
    self.CompanyName = ko.observable();
    self.JobRole = ko.observable();
    self.jobTitle = ko.observable();
    self.NationalINSNumber = ko.observable();
    self.LineManager = ko.observable();
    self.EmployeeNumber = ko.observable();
    self.EmploymentAddress = ko.observable();
    self.ContactName = ko.observable();
    self.PrimaryPhone = ko.observable();
    self.SelectedCaseReferrerProjectTreatmentID = ko.observable();
    self.InjuredRepresentativeOptionID = ko.observable();
    self.PolicyTypes = ko.observableArray([]);
    self.TypeCovers = ko.observableArray([]);
    self.PolicyCriterias = ko.observableArray([]);
    self.FitForWorks = ko.observableArray([]);
    self.Admitteds = ko.observableArray([]);
    self.Referrer = ko.observableArray([]);
    self.NameOfReinsurerID = ko.observable();
 
    // OpenPolicyDetail Section
    self.PolicyOpenDetailID = ko.observable();
    self.PolicyType = ko.observable();
    self.TypeCover = ko.observable();
    self.PolicyCriteria = ko.observable();
    self.RehabORProportionate = ko.observable();
    self.FitforWork = ko.observable();
    self.ReInsured = ko.observable();
    self.ReferenceNo = ko.observable();
    self.Admitted = ko.observable();
    self.OpenBenefitDate = ko.observable();
    self.OpenMonthlyValue = ko.observable();
    self.OpenEndBenefitDate = ko.observable();
    self.NameofReinsurer = ko.observable();
    self.OpenPolicyWording = ko.observable();

    self.SelectedReferrerProjectID = ko.observable();
    self.ReferrerDocuments = ko.observableArray([]);
    self.enableAddReferrer = ko.observable(true);
    self.PatientEmail = ko.observable().extend({
        email: {
            onlyIf: function () {
                return (self.PatientEmail !== undefined && self.PatientEmail !== null && self.PatientEmail != '');
            },
            message: "Patient's Email format is invalid."
        }
    });
    self.PatientHasLegalRep = ko.observable();
    //Injured Party DropDown
    self.InjuredID = ko.observable();
    self.InjuredPartyRepresentativeOptionsArray = ko.observableArray();
    self.InjuredPartyRepresentativeOptionsArray = ko.observableArray([self.InjuredPartyRepresentativeOptionsArray(0)]);
    self.selectedItem = ko.observable(0);
    //Parimary Condition DropDown
    self.PrimaryConditionID = ko.observable();
    self.PrimaryConditionArray = ko.observableArray();
    self.PrimaryConditionArray = ko.observableArray([self.PrimaryConditionArray(0)]);
    self.selectedPrimaryCondition = ko.observable(0);

    self.GipID = ko.observable();
    self.GipArray = ko.observableArray();
    self.GipArray = ko.observableArray([self.GipArray(0)]);
    self.selectedGip = ko.observable(0);

    self.TpdID = ko.observable();
    self.TpdArray = ko.observableArray();
    self.TpdArray = ko.observableArray([self.TpdArray(0)]);
    self.selectedTpd = ko.observable(0);

    self.IipID = ko.observable();
    self.IipArray = ko.observableArray();
    self.IipArray = ko.observableArray([self.IipArray(0)]);
    self.selectedIip = ko.observable(0);

    self.ElRehabID = ko.observable();
    self.ElRehabArray = ko.observableArray();
    self.ElRehabArray = ko.observableArray([self.ElRehabArray(0)]);
    self.selectedElRehab = ko.observable(0);


    self.CaseReferrerProjectTreatmentID = ko.observable();//.extend({ required: { message: "Please choose a Treatment Category." }, insertMessages: false });
    self.OfficeLocationID = ko.observable();
    self.ReferrerProjects = ko.observableArray();
    self.ReferrerAssignedUsers = ko.observableArray();
    self.AssignedUser = ko.observable();
    self.UserID = ko.observable();
    self.TreatmentCategories = ko.observableArray();
    self.TreatmentTypes = ko.observableArray();

    //-- Employment Detail Model
    self.UsualJobRoleTypeID = ko.observable();
    self.CurrentRoleTypeID = ko.observable();
    self.DateofFirstAbsence = ko.observable();
    self.AgileWorkerID = ko.observable();
    self.OfficeLocation = ko.observable();
    self.UsualHours = ko.observable();
    self.CurrentHours = ko.observable();
    self.CurrentlyAbsentFromWorkID = ko.observable();
    self.OfficeBasedID = ko.observable();
    self.TypeofIllness = ko.observable();
    self.PreRelatedAbsence = ko.observable();
    self.MedicationOrTreatment = ko.observable();
    self.EAP = ko.observable();
    self.TypeofIllness = ko.observable();
    self.IllnessEmpAbilityToPerform = ko.observable();
    self.FurtherQueries = ko.observable();
    self.AdditionalQuestion1 = ko.observable();
    self.AdditionalQuestion2 = ko.observable();
    self.FurtherQuestion1 = ko.observable();
    self.FurtherQuestion2 = ko.observable();
    self.AssignedUserList = ko.observableArray();    

    $(".phoneMaskformat").mask("99999 999999");
    self.EmploymentEmail = ko.observable().extend({
        email: {
            onlyIf: function () {
                return (self.PatientEmail !== undefined && self.PatientEmail !== null && self.PatientEmail != '');
            },
            message: "Employment's Email format is invalid."
        }
    });


    self.PatientAge = ko.computed(function () {
        if (self.PatientBirthDate() !== undefined && self.PatientBirthDate() !== '') {
            var birthDate1 = $('#Patient_BirthDate').val();
            var splitDate = birthDate1.split('/');
            var today = new Date().getFullYear();
            var birthyear = splitDate[2];
            var birthmonth = splitDate[1];
            var todayMonth = new Date().getMonth() + 1;
            if (todayMonth.length == 1) todayMonth = "0" + todayMonth;
            var birthday = splitDate[0];
            var todayDay = new Date().getDate();
            if (todayDay.length == 1) todayDay = "0" + todayDay;

            if (birthyear < today) {
                if (birthmonth > todayMonth) {
                    var age = today - birthyear - 1;
                    if (age < 18)
                        $(".injured-party-req").show();
                    else
                        $(".injured-party-req").hide();
                    return age;
                }
                else if (birthmonth == todayMonth) {
                    if (birthday < todayDay || birthday == todayDay) {
                        var age = today - birthyear;
                        if (age < 18)
                            $(".injured-party-req").show();
                        else
                            $(".injured-party-req").hide();
                        return age;
                    }
                    else {
                        var age = today - birthyear - 1;
                        if (age < 18)
                            $(".injured-party-req").show();
                        else
                            $(".injured-party-req").hide();
                        return age;
                    }
                }
                else {
                    var age = today - birthyear;
                    if (age < 18)
                        $(".injured-party-req").show();
                    else
                        $(".injured-party-req").hide();
                    return age;
                }
            } else {
                var age = 0;
                $(".injured-party-req").show();
                return age;
            }
        }
        return '';
    });

    var isPatientHaveSolicitor = function () {
        return self.PatientHasLegalRep() && self.PatientHasLegalRep() != "false";
    };

    self.IsStanding = ko.observable();
    self.IsWalking = ko.observable();
    self.IsWorkATHeightOrClimb = ko.observable();
    self.IsExtendedOrProlonged = ko.observable();
    self.IsVocationalDriving = ko.observable();
    self.IsDrivingLGVOrPCVs = ko.observable();
    self.IsDrivingForkliftTrucks = ko.observable();
    self.IsWorkWithChemials = ko.observable();
    self.IsWorkBiologicalOrChemical = ko.observable();
    self.IsWorkWithSkinIrritants = ko.observable();
    self.IsWorkWithDengerousMachinery = ko.observable();
    self.IsNightWork = ko.observable();
    self.IsShiftWork = ko.observable();
    self.IsWorkInConfinedSpaces = ko.observable();
    self.IsWorkWithDustOrFumes = ko.observable();
    self.IsLiftOrCarryHeavyItems = ko.observable();
    self.IsWorkWithComputerOrScreens = ko.observable();
    self.IsWorkTowardsTagetOrPressuredsituation = ko.observable();
    self.IsWorkWithAdultOrChildren = ko.observable();
    self.IsHealthCareWorker = ko.observable();
    self.IsOccasionalOverseasTravel = ko.observable();
    self.IsOutsideWork = ko.observable();
    self.IsNoisedHarzardArea = ko.observable();
    self.IsHandlingFood = ko.observable();
    self.FreeText = ko.observable();
    self.JobDemandID = ko.observable(0);
    
    
    //property solicitors
    self.SolicitorID = ko.observable();
    self.SolicitorCompanyName = ko.observable();
    self.SolicitorAddress = ko.observable();
    self.SolicitorCity = ko.observable();
    self.SolicitorRegion = ko.observable();
    self.SolicitorPhone = ko.observable();
    self.SolicitorEmail = ko.observable().extend({
        email: {
            onlyIf: function () {
                return (self.SolicitorEmail !== undefined && self.SolicitorEmail !== null && self.SolicitorEmail != '');
            },
            message: "\nSolicitor's Email format is invalid"
        }
    });
    self.SolicitorFirstName = ko.observable();
    self.SolicitorLastName = ko.observable();
    self.SolicitorPostCode = ko.observable();
    self.SolicitorFax = ko.observable();
    self.SolicitorReferenceNumber = ko.observable();
    self.IsReferralUnderJointInstruction = ko.observable();


    self.MedicalReportFileUpload = ko.observable();
    self.PatientConsentFileUpload = ko.observable();

    ////----Injured Party Representation-----------------///
    self.InjuredID = ko.observable();
    self.FirstName = ko.observable();
    self.LastName = ko.observable();
    self.ReferralID = ko.observable();
    self.Tel1 = ko.observable();
    self.Tel2 = ko.observable();
    self.Address = ko.observable();
    self.PostCode = ko.observable();
    self.Email = ko.observable();

    self.FullName = ko.observable();
    self.Relationship = ko.observable();

    function ValidateEmail(email) {
        var expr = /^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;
        return expr.test(email);
    };
    $('#Injured_Email').change(function () {
        if (!ValidateEmail($("#Injured_Email").val())) {
            alert("Rep Email format is not correct.");
        }

    });



    //property case
    self.ReferrerProjectID = ko.observable();//.extend({ required: { message: "Please choose a Project." } });
    self.TreatmentTypeID = ko.observable();//.extend({ required: { message: "Please choose a Treatment Type." } });
    self.CaseSpecialInstruction = ko.observable();
    self.CaseReferrerReferenceNumber = ko.observable();//.extend({ required: { message: "Referrer Reference Number is required." } });

    self.IsNewPolicyTypeID = ko.observable();
    self.NewPolicyReferenceNumber = ko.observable();

    self.CaseSendInvoiceTo = ko.observable();
    self.SendInvoiceTo = ko.observable();
    self.SendInvoiceName = ko.observable();
    self.ReferrerAssignedUser = ko.observable();


    //self.SendInvoiceEmail = ko.observable().extend({
    //    email: {
    //        onlyIf: function () {
    //            return (self.SendInvoiceEmail !== undefined && self.SendInvoiceEmail !== null && self.SendInvoiceEmail != '');
    //        },
    //        message: "\nSend Invoice Email format is invalid"
    //    }
    //});


    $("#objReferrerProjectDropDown").change(function () {
        setUIControls(self);
        if (self.ReferrerProjectID === '' || self.ReferrerProjectID === undefined) {
            return false;
        }
        $("#loader-main-div").removeClass("hidden");
        $.ajax({
            url: "/CaseSearch/GetTreatmentCategories",
            type: 'post',
            dataType: "json",
            data: { referrerProjectID: self.ReferrerProjectID },
            success: function (result) {
                self.TreatmentTypeID(null);
                self.CaseReferrerProjectTreatmentID(null);
                self.TreatmentCategories(result);
                $("#loader-main-div").addClass("hidden");
            },
            error: function () {
                $("#loader-main-div").addClass("hidden");
            }
        });
    });


    //self.TreatmentCategoryID = ko.observable();//.extend({ required: { message: "Please choose a Treatment Category." }, insertMessages: false }); 



    self.ReferrerLocationID = ko.observable().extend({
        required: { message: "Please choose an Office Location." }
    });


    //Gender
    self.Genders = ko.observableArray([
        new Gender(0, 'Choose Gender'),
        new Gender(1, 'Male'),
        new Gender(2, 'Female')
    ]);

    self.ReferrerLocations = ko.observableArray();

    //--CaseContacts
    self.UserContacts = ko.observableArray();
    self.selectedContact = ko.observable();

    $("#objReferrerAssignedUser").change(function () {
        var selectedText = $("#objReferrerAssignedUser").find("option:selected").text();

        if ($("#objReferrerAssignedUser").val() != "") {
            $.post("/User/GetUser", {
                UserId: $("#objReferrerAssignedUser").val()
            },
                    function (modelList) {
                        if (modelList != null) {
                            self.UserID(modelList.UserID);
                            self.Email(modelList.Email);
                            self.FullName(modelList.FirstName + " " + modelList.LastName);
                        }
                    });
        }
    });


    self.EmailCaseContacts = ko.observable();

    self.ReferrerLocationID.subscribe(function () {
        if (self.ReferrerLocationID() === '' || self.ReferrerLocationID() === undefined) {
            self.UserContacts.removeAll();
            //$('#ddlUserContacts').attr("disabled", "disabled");
            self.CaseContacts.removeAll();
            self.showAdd(false);
            return false;
        } else {
            self.showAdd(true);
            $('#ddlUserContacts').removeAttr('disabled');
        }

        $("#loader-main-div").removeClass("hidden");
        $.ajax({
            url: "/CaseSearch/GetUserContacts",
            type: 'post',
            dataType: 'json',
            data: { caseID: self.CaseID() },
            success: function (result) {
                var arrayContact = [];
                $.each(result, function (index, item) {
                    arrayContact.push(new Contact(item.UserID, item.FullName, item.Email));
                });

                arrayContact.push(new Contact(modelReferrer.UserID, 'Main Contact', modelReferrer.Email));
                self.UserContacts.removeAll();
                self.UserContacts(arrayContact);

                // binding user email list
                self.CaseContacts.removeAll();
                $.each(self.UserContacts(), function (index, item) {
                    $.each(self.EmailCaseContacts(), function (index1, item1) {
                        if (item.UserID == item1.UserID) {
                            self.CaseContacts.push(new CaseContact(ko.toJS(item)));
                        }
                    });
                });
                $('#ddlUserContacts').removeAttr('disabled');
                //
                $("#loader-main-div").addClass("hidden");
            },
            error: function () {
                $("#loader-main-div").addClass("hidden");
            }
        });
    });


    self.CaseContacts = ko.observableArray();//.extend({ minLength: { params: 1, message: "Case Contacts must have at least one" }, maxLength: { params: 5, message: "Case Contacts need only 5 or less" } });
    self.CaseContacts.subscribe(function () {
        var arrLength = self.CaseContacts().length;
        if (arrLength && arrLength > 0) {
            $('#GridEmailContactMessage').hide();
        } else {
            $('#GridEmailContactMessage').show();
        }

        if (arrLength > 4) {
            self.showAdd(false);
        } else {
            self.showAdd(true);
        }
    });

    self.showAdd = ko.observable(true);

    self.addContact = function () {
        if (self.ReferrerLocationID() === '') {
            alert('Please select Referrer Information : Office Location');
            return false;
        }
        if ($("#txtSelectedContact").val() == "") {
            alert('Please select an Email Contact');
            return false;
        }

        var validContact = true;
        $.each(self.CaseContacts(), function (index, value) {
            if (value.Contact.UserID === self.selectedContact().UserID) {
                alert('You have already added this contact. Please choose another.');
                return validContact = false;
            }
        });

        if (validContact) {
            self.CaseContacts.push(new CaseContact(ko.toJS(self.selectedContact())));
            //self.selectedContact().FullName=$("#objReferrerAssignedUser").find("option:selected").text();
            $('select#ddlUserContacts')[0].selectedIndex = 0;
            $('#txtSelectedContact').val('');
        }
    };

    self.deleteContact = function (item) {
        self.CaseContacts.remove(item);
    };
    //--CaseContacts

    //binds


    //forediting
    self.PatientHasLegalRep.ForEditing = ko.computed({
        read: function () {
            self.PatientHasLegalRep();
        },
        write: function (newValue) {
            self.PatientHasLegalRep(newValue);
        },
        owner: self
    });

    //subscribe change
    self.PatientHasLegalRep.subscribe(function () {
        if (self.PatientHasLegalRep() === "true") {
            $('#divSolicitor').show();
        } else {
            $('#divSolicitor').hide();
        }
    });
    self.SelectedReferrerProject = ko.observable();

    //self.ReferrerProjectID = ko.computed(function () {
    //    if (self.SelectedReferrerProject() !== undefined) {
    //        $("#objReferrerProjectID").val(self.SelectedReferrerProject().ReferrerProjectID);
    //        return self.SelectedReferrerProject().ReferrerProjectID;
    //    }
    //});
    self.ReferrerProjectIsTriage = ko.computed(function () {
        if (self.SelectedReferrerProject() !== undefined) {
            return self.SelectedReferrerProject().IsTriage;
        }
    });

    self.CaseReferrerProjectTreatmentID.subscribe(function () {
        setUIControls(self);
        if (self.CaseReferrerProjectTreatmentID() !== undefined && self.CaseReferrerProjectTreatmentID() !== '' && self.CaseReferrerProjectTreatmentID() !== null) {
            $("#loader-main-div").removeClass("hidden");
            $.ajax({
                url: "/CaseSearch/GetTreatmentTypes",
                type: 'post',
                dataType: "json",
                data: { referrerProjectTreatmentID: self.CaseReferrerProjectTreatmentID() },
                success: function (result) {
                    //document.getElementById('TreatmentTypes').selectedIndex = 0;
                    self.TreatmentTypes(result);
                    $("#loader-main-div").addClass("hidden");
                },
                error: function () {
                    $("#loader-main-div").addClass("hidden");
                }
            });
        }
    });
    self.errors = ko.validation.group(self);
    self.ConfirmDialogMessage = ko.observable();


    //submit events
    this.submit = function () {
        var ifFindError = "True";
        var errors = "Errors ";
        if (self.AssignedUserList().length == 0) {
            errors += "Please select an Assigned User.";
        }
        if (self.PatientFirstName.trim() == "") {
            errors += "Patient's First name is required.";
        }
        if (self.PatientLastName.trim() == "") {
            errors += "Patient's Last name is required.";
        }
        if (self.PatientAddress.trim() == "") {
            errors += "Patient's Address is required.";
        }
        if (self.PatientCity.trim() == "") {
            errors += "Patient's City is required.";
        }
        if (self.PatientRegion.trim() == "") {
            errors += "Patient's Region is required.";
        }
        if (self.PatientPostCode.trim() == "") {
            errors += "Patient's PostCode is required.";
        }
        if (self.PatientInjuryDate.trim() == "") {
            errors += "Patient's InjuryDate is required.";
        }
        if (self.PatientBirthDate.trim() == "") {
            errors += "Patient's BirthDate is required.";
        }
        if (self.PatientHomePhone.trim() == "") {
            errors += "Patient's HomePhone is required.";
        }
        else if ($("#ddlGender").val() == "") {            
            errors += "Please select a gender.";
        }
        if ($('#Case_ReferrerProjectTreatmentID').val() == '') {
            errors += "Please choose a Treatment Category.";
        }
        if ($('#objReferrerProjectDropDown').val() == '') {
            errors += "Please choose a Project.";
        }
        if (self.TreatmentTypeID == "") {
            errors += "Please choose a Treatment Type.";
        }        
        if (self.CaseContacts().length < 1) {
            errors += "Case Contacts must have at least one.";
        }
        else if (self.CaseContacts().length > 5) {
            errors += "Case Contacts  need only 5 or less.";
        }
       
        if ($("#sendInvoiceToID").prop("checked")) {
            if ($("#objSendInvoiceName").val() === "") {
                alert("Invoice Name is required.");
                ifFindError = "False";
            }
            else if ($("#objSendInvoiceEmail").val() === "") {
                alert("Invoice email is required.");
                ifFindError = "False";
            }
        }
        var drop = $('#objInjuredPartyRepresentative :selected').text();
        if (self.PatientAge() != "") {
            if (self.PatientAge() < 18 && drop == "Select One") {
                alert("Injured Party Representative Section is required.");
                ifFindError = "False";
            }
        }
        if (drop !== "Select One" && $("#PatientAge").val() < 18) {
            if ($("#Injured_FirstName").val() === "") {
                alert("Rep First Name is required.");
                ifFindError = "False";
            }
            else if ($("#Injured_LastName").val() === "") {
                alert("Rep Last Name is required.");
                ifFindError = "False";
            }
            else if ($("#Injured_Tel1").val() === "") {
                alert("Rep Phone is required.");
                ifFindError = "False";
            }

        }
        if (self.PatientHasLegalRep() === "true") {
            if ($("#Solicitor_CompanyName").val() === "") {
                alert("Solicitor Company Name is required.");
                ifFindError = "False";
            }
            else if ($("#Solicitor_Address").val() === "") {
                alert("Solicitor Address is required.");
                ifFindError = "False";
            }
            else if ($("#Solicitor_City").val() === "") {
                alert("Solicitor City is required.");
                ifFindError = "False";
            }
            else if ($("#Solicitor_PostCode").val() === "") {
                alert("Solicitor Post Code is required.");
                ifFindError = "False";
            }
            else if ($("#Solicitor_Phone").val() === "") {
                alert("Solicitor Phone is required.");
                ifFindError = "False";
            }
            else if ($("#Solicitor_ReferenceNumber").val() === "") {
                alert("Solicitor Reference is required.");
                ifFindError = "False";
            }
        }

        if (ifFindError === "True") {
            if (!self.isValid() || errors.trim() != 'Errors') {
                $.each(self.errors(), function (index, value) {
                    errors = errors + ' ' + value();
                });
                alert(errors);
                return false;
            }
            $("#loader-main-div").removeClass("hidden");


            if ($("#objInjuredPartyRepresentative").val() == 1) {
                self.FirstName("");
                self.LastName("");
                self.Tel1("");
                self.Tel2("");
                self.PostCode("");
                self.Email("");
                self.Relationship("");
                self.Address("");
            }

            if ($("#NoPatientHasLegalRep").is(':checked')) {
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
            }

            //self.enableAddReferrer(false);
            var options = {
                dataType: 'html',
                contentType: "application/html; charset=utf-8",
                success: function (result) {

                    $("#loader-main-div").addClass("hidden");
                    return false;
                }
            };
            window.onbeforeunload = null;
            $('#frmAddReferral').ajaxSubmit(options);
            $.ajax({
                url: "/CaseSearch/GetEmailContacts",
                type: 'post',
                dataType: 'json',
                data: { caseID: self.CaseID() },
                success: function (result) {
                    self.EmailCaseContacts(result);
                }
            });
            alert("Update Successfully");
            location.reload();
        }
        $("#loader-main-div").addClass("hidden");

    };

    //forediting
    self.CaseSendInvoiceTo.ForEditing = ko.computed({
        read: function () {
            self.CaseSendInvoiceTo();
        },
        write: function (newValue) {
            self.CaseSendInvoiceTo(newValue);
        },
        owner: self
    });
    self.CaseSendInvoiceTo.subscribe(function () {
        if (self.CaseSendInvoiceTo() === "Other") {
            $('#DivSendInvoiceTo').show('slow');
        } else {
            self.SendInvoiceName('');
            //self.SendInvoiceEmail('');
            $('#DivSendInvoiceTo').hide('slow');
        }
    });



    this.init = function () {
        $(".injured-party-req").hide();
        $('#objReferrerProjectDropDown').attr("disabled", true);
        $('#PatientAge').attr("disabled", "disabled");
        $('#divSolicitor').hide();
        $('#DivSendInvoiceTo').hide();
        //$('#Case_ReferrerProjectTreatmentID').attr("disabled", "disabled");
        $('#TreatmentTypes').attr("disabled", "disabled");
        $('#txtSelectedContact').attr("disabled", "disabled");
        $('#ddlUserContacts').attr("disabled", "disabled");
        $('#patientHasLegalRepID').attr("checked", "checked");
        $('#RefeerProjectUserID').attr("checked", "checked");
        $('#objPatientConsent').attr("disabled", true);

        if (self.Referrer != null) {
            if (referrer.IsPolicyDetail == true) {
                if (referrer.IsPolicyDetail == true && referrer.IsPolicyDetailOpenOrDropdowns == "Dropdowns") {
                    $('#PolicyOpenDetailDiv').hide();
                    $('#PolicyDetailDiv').show();
                }
                else {
                    $('#PolicyOpenDetailDiv').show();
                    $('#PolicyDetailDiv').hide();
                }
            }
            else {
                $('#PolicyOpenDetailDiv').hide();
                $('#PolicyDetailDiv').hide();
            }
            if (self.Referrer.IsEmploymentDetail == true) {
                $('#EmploymentDetailDiv').show();
            }
            else {
                $('#EmploymentDetailDiv').hide();
            }

            if (self.Referrer.IsNewPolicyTypes == "GIP") {
                $('#GIPDiv').show();
                $('#IIPDiv').hide();
                $('#TPDDiv').hide();
                $('#ELRehabDiv').hide();
               
            }
            else if (self.Referrer.IsNewPolicyTypes == "IIP") {
                $('#GIPDiv').hide();
                $('#IIPDiv').show();
                $('#TPDDiv').hide();
                $('#ELRehabDiv').hide();
               
            }
            else if (self.Referrer.IsNewPolicyTypes == "TPD") {
                $('#GIPDiv').hide();
                $('#IIPDiv').hide();
                $('#TPDDiv').show();
                $('#ELRehabDiv').hide();
              
            }
            else if (self.Referrer.IsNewPolicyTypes == "ELRehab")
            {
                $('#GIPDiv').hide();
                $('#IIPDiv').hide();
                $('#TPDDiv').hide();
                $('#ELRehabDiv').show();
            }
            else
            {
                $('#GIPDiv').hide();
                $('#IIPDiv').hide();
                $('#TPDDiv').hide();
                $('#ELRehabDiv').hide();
            }
        }

        $('#loadingImage').hide().ajaxStart(function () {
            $(this).show();
        }).ajaxStop(function () {
            $(this).hide();
        });
        self.showAdd(false);

        $.getJSON("/CaseSearch/GetAllInjuredRepresentativeOption", function (data) {
            self.InjuredPartyRepresentativeOptionsArray(data.slice());
            self.Check_InjuredPartyRepDropDownList();
        });

        $.getJSON("/CaseSearch/GetAllPrimaryCondition", function (data) {
            self.PrimaryConditionArray(data.slice());
        });

        $.getJSON("/CaseSearch/GetAllGips", function (data) {
            self.GipArray(data.slice());
        });

        $.getJSON("/CaseSearch/GetAllIips", function (data) {
            self.IipArray(data.slice());
        });

        $.getJSON("/CaseSearch/GetAllTpds", function (data) {
            self.TpdArray(data.slice());
        });

        $.getJSON("/CaseSearch/GetAllElRehabs", function (data) {
            self.ElRehabArray(data.slice());
        });

        $.ajax({
            url: "/CaseSearch/GetTreatmentCategories",
            type: 'post',
            dataType: "json",
            data: { referrerProjectID: self.ReferrerProjectID },
            success: function (result) {
                self.TreatmentTypeID(null);
                self.CaseReferrerProjectTreatmentID(self.SelectedCaseReferrerProjectTreatmentID());
                self.TreatmentCategories(result);
                $("#loader-main-div").addClass("hidden");
            },
            error: function () {
                $("#loader-main-div").addClass("hidden");
            }
        });
    };

    //self.HasInjuredPartyRepresentative.subscribe(function () {        
    //    self.Check_InjuredPartyRepDropDownList();
    //});

    var IsInjuredRepresentative = true;
    self.Check_InjuredPartyRepDropDownList = function () {
        var objInjuredID = $("#objInjuredPartyRepresentative").val();
        if (objInjuredID > 1) {
            $('#divInjuredPartyRepresentatives').show();
        }
        else {
            $('#divInjuredPartyRepresentatives').hide();
        }
    }

    self.InjuredPartyRepDropDownList = function () {
        self.Check_InjuredPartyRepDropDownList();
    };

    $('#txtmonthlyvalue').change(function () {
        var num = parseFloat($("#txtmonthlyvalue").val());
        var new_num = $("#txtmonthlyvalue").val(num.toFixed(2));
    });

    $('#txtWeeklyValue').change(function () {
        var num = parseFloat($("#txtWeeklyValue").val());
        var new_num = $("#txtWeeklyValue").val(num.toFixed(2));
    });
    self.btnAddAssignUserClick = function () {
        // have to apply a check if the value already exist
        if ($("#objReferrerAssignedUser option:selected").val() == "")
        {
            alert("Please select a user");
            return false;
        }

        for (var i = 0; i < self.AssignedUserList().length; i++) {
            if (self.AssignedUserList()[i].UserID == $("#objReferrerAssignedUser option:selected").val()) {
                alert("User already in the list");
                return false;
            }
        }

        var arr = { CaseReferrerAssignedUserID: null, UserID: $("#objReferrerAssignedUser option:selected").val(), CaseID: self.CaseID(), UserName: $("#objReferrerAssignedUser option:selected").text()};

        $.ajax({
            url: "/CaseSearch/AddCaseAssignedUser",
            type: "post",            
            data: JSON.stringify(arr),
            contentType: "application/json",
            success: function (res) {                
                if (res > 0) {
                    arr.CaseReferrerAssignedUserID = res;
                    self.AssignedUserList.push(arr);
                }
                else {
                    alert("Error Occured");
                }
            }
        });        
    }

    self.btnDeleteAssignUserClick = function (_CaseReferrerAssignedUserID) {
        if (confirm("Are you sure you want to remove?")) {
            $.ajax({
                url: "/CaseSearch/DeleteCaseAssignedUser",
                type: "post",
                dataType: "json",
                data: { _id: _CaseReferrerAssignedUserID },
                success: function (res) {
                    if (res != false) {
                        self.AssignedUserList.remove(function (user) {
                            return user.CaseReferrerAssignedUserID == _CaseReferrerAssignedUserID;
                        });
                    }
                    else {
                        alert("Error Occured");
                    }
                }
            });
        }
    }
}
// AddReferralViewModel ends ------
function setUIControls(self) {
    if ($('#objReferrerProjectDropDown').val() === '' || $('#objReferrerProjectDropDown').val() === undefined) {
        self.CaseReferrerProjectTreatmentID(undefined);
        self.TreatmentTypeID(undefined);
        $('#Case_ReferrerProjectTreatmentID').attr("disabled", "disabled");
        $('#TreatmentTypes').attr("disabled", "disabled");
    } else {
        $('#Case_ReferrerProjectTreatmentID').removeAttr('disabled');
        $('#TreatmentTypes').removeAttr('disabled');
    }

    if (self.CaseReferrerProjectTreatmentID() === '' || self.CaseReferrerProjectTreatmentID() === undefined) {
        self.TreatmentTypeID(undefined);
        $('#TreatmentTypes').attr("disabled", "disabled");
    } else {
        $('#TreatmentTypes').removeAttr('disabled');
    }
}



function calPatientAge() {
    if ($("#Patient_BirthDate").val() !== undefined && $("#Patient_BirthDate").val() !== '') {
        var birthDate1 = $('#Patient_BirthDate').val();
        var splitDate = birthDate1.split('/');
        var today = new Date().getFullYear();
        var birthyear = splitDate[2];
        var birthmonth = splitDate[1];
        var todayMonth = new Date().getMonth() + 1;
        if (todayMonth.length == 1) todayMonth = "0" + todayMonth;
        var birthday = splitDate[0];
        var todayDay = new Date().getDate();
        if (todayDay.length == 1) todayDay = "0" + todayDay;

        if (birthyear < today) {
            if (birthmonth > todayMonth) {
                var age = today - birthyear - 1;
                if (age < 18)
                    $(".injured-party-req").show();
                else
                    $(".injured-party-req").hide();
                return $("#PatientAge").val(age);
            }
            else if (birthmonth == todayMonth) {
                if (birthday < todayDay || birthday == todayDay) {
                    var age = today - birthyear;
                    if (age < 18)
                        $(".injured-party-req").show();
                    else
                        $(".injured-party-req").hide();
                    return $("#PatientAge").val(age);
                }
                else {
                    var age = today - birthyear - 1;
                    if (age < 18)
                        $(".injured-party-req").show();
                    else
                        $(".injured-party-req").hide();
                    return $("#PatientAge").val(age);
                }
            }
            else {
                var age = today - birthyear;
                if (age < 18)
                    $(".injured-party-req").show();
                else
                    $(".injured-party-req").hide();
                return $("#PatientAge").val(age);
            }
        } else {
            var age = 0;
            $(".injured-party-req").show();
            return $("#PatientAge").val(age);
        }
    }
    $("#PatientAge").val('');

}
function CaseContact(contact) {
    this.showRemove = ko.observable(true);
    this.Contact = contact;
}

function Contact(userID, fullName, email) {
    this.UserID = userID;
    this.FullName = fullName;
    this.Email = email;
    this.Details = fullName + ' - ' + email;
}

function Gender(genderID, genderName) {
    this.GenderID = genderID;
    this.GenderName = genderName;
}