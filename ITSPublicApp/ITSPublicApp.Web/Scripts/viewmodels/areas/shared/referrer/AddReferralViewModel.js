function AddReferralViewModel(modelReferrer) {
    ko.validation.init({ insertMessages: false });
    var self = this;
    //property patients
    self.PatientTitle = ko.observable();
    self.PatientFirstName = ko.observable().extend({ required: { message: " Patient's First name is required.\n " } });
    self.PatientLastName = ko.observable().extend({ required: { message: "Patient's Surname is required.\n " } });
    self.PatientAddress = ko.observable().extend({ required: { message: "Patient's Address is required.\n " } });
    self.PatientCity = ko.observable().extend({ required: { message: "Patient's City is required.\n " } });
    self.PatientRegion = ko.observable().extend({ required: { message: "Patient's Region is required.\n " } });   
    self.PatientInjuryDate = ko.observable().extend({ required: { message: "Patient's Date of Onset is required.\n " } });
    self.PatientBirthDate = ko.observable().extend({ required: { message: "Patient's Date of Birth is required.\n " } });
    self.PatientHomePhone = ko.observable().extend({ required: { message: "Patient's Main Phone is required.\n " } });
    self.PatientWorkPhone = ko.observable();
    self.PatientMobilePhone = ko.observable();
    self.PatientGenderID = ko.observable();
    self.ReferrerDocumentTypes = ko.observableArray([]);
    self.PatientPostCode = ko.observable();

    self.EmploymentTypes = ko.observableArray([]);
    self.PolicyTypes = ko.observableArray([]);
    self.AdditionalTests = ko.observableArray([]);
    self.ReasonForReferral = ko.observableArray([]);
    self.NetworkRailStandardApplicable = ko.observableArray([]);
    self.IsPolicyDetailOpenOrDropdowns = ko.observable();
    self.TypeCovers = ko.observableArray([]);
    self.PolicyCriterias = ko.observableArray([]);
    self.FitForWorks = ko.observableArray([]);
    self.Admitteds = ko.observableArray([]);
    self.Referrer = ko.observableArray([]);
    self.WorkTypes = ko.observableArray([]);
    self.RoleTypes = ko.observableArray([]);
    self.Reinsurers = ko.observableArray([]);
    self.GenderTypes = ko.observable([]);


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
    self.HasInjuredPartyRepresentative = ko.observable();

    self.InjuredID = ko.observable();
    self.InjuredPartyRepresentativeOptionsArray = ko.observableArray();
    self.InjuredPartyRepresentativeOptionsArray = ko.observableArray([self.InjuredPartyRepresentativeOptionsArray(0)]);
    self.selectedItem = ko.observable(0);

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


    self.EmploymentQueryID = ko.observable();
    self.EmploymentQueryArray = ko.observableArray();
    self.EmploymentQueryArray = ko.observableArray([self.EmploymentQueryArray(0)]);
    self.selectedEmploymentQuery = ko.observable(0);

    $(".phoneMaskformat").mask("99999 999999");
    self.EmploymentEmail = ko.observable().extend({
        email: {
            onlyIf: function () {
                return (self.PatientEmail !== undefined && self.PatientEmail !== null && self.PatientEmail != '');
            },
            message: "Employment's Email format is invalid."
        }
    });

    self.AssignedUserList = ko.observableArray();

    

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

    ///===================Employee Detail ==================//
    self.EmployeeDetailID = ko.observable();
    self.UsualJobRoleTypeID = ko.observable();
    self.UsualHours = ko.observable();
    self.CurrentRoleTypeID = ko.observable();
    self.CurrentHours = ko.observable();
    self.DateofFirstAbsence = ko.observable();
    self.OfficeLocation = ko.observable();
    self.TypeofIllness = ko.observable();
    self.PreRelatedAbsence = ko.observable();
    self.MedicationOrTreatment = ko.observable();
    self.EAP = ko.observable();
    self.IllnessEmpAbilityToPerform = ko.observable();
    self.FurtherQueries = ko.observable();
    self.CurrentlyAbsentFromWorkID = ko.observable();
    self.AgileWorkerID = ko.observable();
    self.OfficeBasedID = ko.observable();
    self.LineManager = ko.observable();
    self.CostCentreDivision = ko.observable();
    self.EmployeeNumber = ko.observable();
    self.AdditionalQuestion1 = ko.observable();
    self.AdditionalQuestion2 = ko.observable();
    self.FurtherQuestion1 = ko.observable();
    self.FurtherQuestion2 = ko.observable();
    self.NationalINSNumber = ko.observable();
    self.jobTitle = ko.observable();
    //==================================================//
    //property solicitors
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
            message: "\nSolicitor's Email format is invalid."
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
    self.Relationship = ko.observable();
    self.showAdd = ko.observable(true);

    function ValidateEmail(email) {
        var expr = /^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;
        return expr.test(email);
    };

    $('#Injured_Email').change(function () {
        if (!ValidateEmail($("#Injured_Email").val())) {
            $('#Injured_Email').val('');
            $('#Injured_Email').focus();
            alert("Rep Email format is not correct.");
        }
        self.Email($("#Injured_Email").val());
    });

   
    //property case
    self.ReferrerProjectID = ko.observable().extend({ required: { message: "Please choose a Project.\n " } });
     
    self.CaseSpecialInstruction = ko.observable();
    self.CaseReferrerReferenceNumber = ko.observable();

    self.IsNewPolicyTypeID = ko.observable();
    self.NewPolicyReferenceNumber = ko.observable();

    self.CaseSendInvoiceTo = ko.observable();
    self.SendInvoiceTo = ko.observable();
    self.SendInvoiceName = ko.observable();
    self.ReferrerAssignedUser = ko.observable();
    self.ReferrerProjectID = ko.observable();

    self.SendInvoiceEmail = ko.observable().extend({
        email: {
            onlyIf: function () {
                return (self.SendInvoiceEmail !== undefined && self.SendInvoiceEmail !== null && self.SendInvoiceEmail != '');
            },
            message: "\nSend Invoice Email format is invalid."
        }
    });

    self.CaseID = ko.observable();    
    self.ReferrerProjectID = ko.observable();
    self.OfficeLocationID = ko.observable();

    self.EncryptReferrerLocationID = ko.observable().extend({
        required: { message: "Please choose an Office Location." }
    });

    self.ReferrerLocations = ko.observableArray();
    self.EncryptReferrerLocationID = ko.observable();

    //  Case Contacts
    self.UserContacts = ko.observableArray();

    self.selectedContact = ko.observable();

    $('#ObjpatientConstentRaidoID').change(function () {
        if ($("#ObjpatientConstentRaidoID").prop("checked")) {
            $('#objPatientConsent').attr("disabled", false);
        }
    });

    $('#objPatientConsent').change(function () {
        var iSize = ($("#objPatientConsent")[0].files[0].size / 1024);
        iSize = (Math.round(iSize * 100) / 100);

        var ext = $('#objPatientConsent').val().split('.').pop().toLowerCase();
        if ($.inArray(ext, ['pdf', 'docx', 'xlsx', 'jpg', 'jpeg', 'png', 'bmp']) == -1) {
            alert('Please a select file in .pdf, .docx, .xlsx, .jpg, jpeg, .bmp and .png format.');
            ClearTheUploadFIle();
        }
        else if (iSize > 10240) {
            alert('Uploaded file exceed the limit of 10 Mb');
            ClearTheUploadFIle();
        }
        else {
            var formData = new FormData();
            var fileInput = document.getElementById('objPatientConsent');
            for (i = 0; i < fileInput.files.length; i++) {
                formData.append(fileInput.files[i].name, fileInput.files[i]);
            }
            
            if (ext == "docx" || ext == "pdf" || ext == "xlsx" || ext == "jpg" || ext == "jpeg" || ext == "png" || ext == 'bmp') {
                switch (ext) {
                    case "docx":
                        $.ajax({
                            type: "POST",
                            url: "/Referrer/AddReferral/IsDoc",
                            contentType: false,
                            processData: false,
                            data: formData,
                            success: function (_res) {
                                if (_res == 'True') {
                                    return true;
                                }
                                else {
                                    alert("Invalid or Corrupt file");
                                    ClearTheUploadFIle();
                                    return false;
                                }
                            },
                            error: function () {
                            }
                        });
                        break;

                    case "xlsx":
                        $.ajax({
                            type: "POST",
                            url: "/Referrer/AddReferral/IsXlsx",
                            contentType: false,
                            processData: false,
                            data: formData,
                            success: function (_res) {
                                if (_res == 'True') {
                                    return true;
                                }
                                else {
                                    alert("Invalid or Corrupt file");
                                    ClearTheUploadFIle();
                                    return false;
                                }
                            },
                            error: function () {
                            }
                        });
                        break;

                    case "pdf":
                        $.ajax({
                            type: "POST",
                            url: "/Referrer/AddReferral/IsPdf",
                            contentType: false,
                            processData: false,
                            data: formData,
                            success: function (_res) {
                                if (_res == 'True') {
                                    return true;
                                }
                                else {
                                    alert("Invalid or Corrupt file");
                                    ClearTheUploadFIle();
                                    return false;
                                }
                            },
                            error: function () {
                            }
                        });
                        break;
                    case "jpg":
                    case "jpeg":
                    case "png":
                    case "bmp":
                        $.ajax({
                            type: "POST",
                            url: "/Referrer/AddReferral/IsImage",
                            contentType: false,
                            processData: false,
                            data: formData,
                            success: function (_res) {
                                if (_res == 'True') {
                                    return true;
                                }
                                else {
                                    alert("Invalid or Corrupt file");
                                    ClearTheUploadFIle();
                                    return false;
                                }
                            },
                            error: function () {
                            }
                        });
                        break;
                }
            }
        }
    });

    function ClearTheUploadFIle() {
        $('#objPatientConsent').each(function () {
            $(this).after($(this).clone(true)).remove();
        });

        var control = $('#objPatientConsent');
        control.replaceWith(control.val('').clone(true));

        control.on({
            change: function () { console.log("Changed") },
            focus: function () { console.log("Focus") }
        });
    }

    function ClearTheUploadFIle1() {
        $('#fileReferrerDocumentUpload').each(function () {
            $(this).after($(this).clone(true)).remove();
        });

        var control = $('#objPatientConsent');
        control.replaceWith(control.val('').clone(true));

        control.on({
            change: function () { console.log("Changed") },
            focus: function () { console.log("Focus") }
        });
    }

    self.SaveReferrerDocument = function () {

        if (self.MedicalReportFileUpload() == undefined || $("#txtUploadDocName").val() == '' || $("#txtDocumentDate").val() == '' || $("#fileReferrerDocumentUpload").val() == '') {
            alert('Please fill all the fields');
            return false;
        }
        else {
            var formData = new FormData();
            var fileInput = document.getElementById('fileReferrerDocumentUpload');
            for (i = 0; i < fileInput.files.length; i++) {
                formData.append(fileInput.files[i].name, fileInput.files[i]);
            }
            var fileUpload = $("#fileReferrerDocumentUpload").val();
            var ext = fileUpload.split('.').pop().toLowerCase();
            
            if (ext == "docx" || ext == "pdf" || ext == "xlsx" || ext == "jpg" || ext == "jpeg" || ext == "png" || ext == "bmp") {
                switch (ext) {
                    case "docx":
                        $.ajax({
                            type: "POST",
                            url: "/Referrer/AddReferral/IsDoc",
                            contentType: false,
                            processData: false,
                            data: formData,
                            success: function (_res) {
                                if (_res == 'True') {
                                    return true;
                                }
                                else {
                                    alert("Invalid or Corrupt file");
                                    return false;
                                }
                            },
                            error: function () {
                            }
                        });
                        break;

                    case "xlsx":
                        $.ajax({
                            type: "POST",
                            url: "/Referrer/AddReferral/IsXlsx",
                            contentType: false,
                            processData: false,
                            data: formData,
                            success: function (_res) {
                                if (_res == 'True') {
                                    return true;
                                }
                                else {
                                    alert("Invalid or Corrupt file");
                                    return false;
                                }
                            },
                            error: function () {
                            }
                        });
                        break;

                    case "pdf":
                        $.ajax({
                            type: "POST",
                            url: "/Referrer/AddReferral/IsPdf",
                            contentType: false,
                            processData: false,
                            data: formData,
                            success: function (_res) {
                                if (_res == 'True') {
                                    return true;
                                }
                                else {
                                    alert("Invalid or Corrupt file");
                                    return false;
                                }
                            },
                            error: function () {
                            }
                        });
                        break;

                    case "jpg":
                    case "jpeg":
                    case "png":
                    case "bmp":
                        $.ajax({
                            type: "POST",
                            url: "/Referrer/AddReferral/IsImage",
                            contentType: false,
                            processData: false,
                            data: formData,
                            success: function (_res) {
                                if (_res == 'True') {
                                    return true;
                                }
                                else {                                    
                                    alert("Invalid or Corrupt file");
                                    return false;
                                }
                            },
                            error: function () {
                            }
                        });
                        break;
                }

                var iSize = ($("#fileReferrerDocumentUpload")[0].files[0].size / 1024);
                iSize = (Math.round(iSize * 100) / 100);
                if (iSize < 10241) {

                    $("#loader-main-div").removeClass("hidden");
                    var formData = new FormData();
                    var fileInput = document.getElementById('fileReferrerDocumentUpload');
                    for (i = 0; i < fileInput.files.length; i++) {
                        formData.append(fileInput.files[i].name, fileInput.files[i]);
                    }

                    $.ajax({
                        type: "POST",
                        url: "/Referrer/AddReferral/SaveReferrerDocument?docName=" + $("#txtUploadDocName").val() + "&docDate=" + $("#txtDocumentDate").val() + "&refDocType=" + $("#ddlUploadDocType option:selected").html() + "&refDocTypeID=" + $("#ddlUploadDocType").val(),
                        contentType: false,
                        processData: false,
                        data: formData,
                        success: function (result) {
                            self.ReferrerDocuments.push(result);
                            document.getElementById('divUploadModal').style.display = "none";
                            $("#divUploadedDocuments").removeClass("hide");
                            $("#tblRefDocForm :input").val('');
                            $("#loader-main-div").addClass("hidden");
                        },
                        error: function () {
                            $("#loader-main-div").addClass("hidden");
                        }
                    });
                }
                else {
                    alert('Uploaded file exceed the limit of 10 Mb.');
                    ClearTheUploadFIle1();
                    return false;
                }
            }
            else {
                alert("Please a select file in .pdf, .docx, .xlsx, .jpg, jpeg and .png format.");
                ClearTheUploadFIle1();
                return false;
            }

        }
    };

    self.RemoveReferrerDocument = function (refDoc) {
        if (!confirm("Are you sure?")) {
            return false;
        }

        $.post("/Referrer/AddReferral/RemoveReferrerDocument", {
            tempCaseID: refDoc.TempCaseID
        },
        function (res) {
            self.ReferrerDocuments.remove(refDoc);
            if (self.ReferrerDocuments().length == 0) {
                $("#divUploadedDocuments").addClass("hide");
            }
        }
      )
    };

    $("#objReferrerAssignedUser").change(function () {
        var selectedText = $("#objReferrerAssignedUser").find("option:selected").text();

        if ($("#objReferrerAssignedUser").val() != "") {
            $.post("/User/GetUserByID", {
                UserId: $("#objReferrerAssignedUser").val()
            },
                    function (modelList) {
                        if (modelList != null) {
                            self.UserID = modelList.UserID;
                            self.Email = modelList.Email;
                            self.FullName = modelList.FirstName + " " + modelList.LastName;
                        }
                    }
        )
        }
    });

    self.EncryptReferrerLocationID.subscribe(function () {
        if (self.EncryptReferrerLocationID() === '' || self.EncryptReferrerLocationID() === undefined) {
            self.UserContacts.removeAll();
            //$('#ddlUserContacts').attr("disabled", "disabled");
            self.CaseContacts.removeAll();
            self.showAdd(false);
            if (self.EncryptReferrerLocationID() != null)
                var _referrerLocationID = self.EncryptReferrerLocationID().EncryptReferrerLocationID;
            $("#EncryptReferrerLocationIDhidden").val(_referrerLocationID);
            return false;
        } else {
            self.showAdd(true);
            if (self.EncryptReferrerLocationID() != null)
                var _referrerLocationID = self.EncryptReferrerLocationID().EncryptReferrerLocationID;
                $("#EncryptReferrerLocationIDhidden").val(_referrerLocationID);

           // $('#ddlUserContacts').removeAttr('disabled');
        }
        $('#divOfficeLocationSpinner').spin(true);
        $.ajax({
            url: "/Referrer/AddReferral/GetUserContacts",
            type: 'post',
            dataType: 'json',
            //data: { referrerLocationID: self.EncryptReferrerLocationID() },
            success: function (result) {
                var arrayContact = [];
                $.each(result, function (index, item) {
                    arrayContact.push(new Contact(item.UserID, item.FullName, item.Email));
                });               
                arrayContact.push(new Contact(modelReferrer.UserID, 'Main Contact', modelReferrer.Email));
                self.UserContacts(arrayContact);
                $('#divOfficeLocationSpinner').spin(false);
            },
            error: function () {
                $('#divOfficeLocationSpinner').spin(false);
            }
        });
    });
    if (modelReferrer.EyptReferrerLocationID != null) {
        self.EncryptReferrerLocationID(modelReferrer.EyptReferrerLocationID);
       
        $('#divOfficeLocationSpinner').spin(true);
        $.ajax({
            url: "/Referrer/AddReferral/GetUserContacts",
            type: 'post',
            dataType: 'json',
            //data: { referrerLocationID: self.EncryptReferrerLocationID() },
            success: function (result) {
                self.showAdd(true);
                //$('#ddlUserContacts').prop("disabled", false);
                var arrayContact = [];
                $.each(result, function (index, item) {
                    arrayContact.push(new Contact(item.UserID, item.FullName, item.Email));
                });
                arrayContact.push(new Contact(modelReferrer.UserID, 'Main Contact', modelReferrer.Email));
                self.UserContacts(arrayContact);
                $('#divOfficeLocationSpinner').spin(false);
            },
            error: function () {
                $('#divOfficeLocationSpinner').spin(false);
            }
        });
    }
    else
        self.selectedRLItem('');

    self.CaseReferrerProjectTreatmentID = ko.observable().extend({ required: { message: "Please choose a Treatment Category.\n" }, insertMessages: false });

    self.CaseContacts = ko.observableArray().extend({ minLength: { params: 1, message: "Case Contacts must have at least one.\n" }, maxLength: { params: 5, message: "Case Contacts need only 5 or less.\n " } });

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

    

    self.addContact = function () {
       
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
            $('select#ddlUserContacts')[0].selectedIndex = 0;
            $('#txtSelectedContact').val('');
        }
    };

    self.deleteContact = function (item) {
        self.CaseContacts.remove(item);
    };
    //binds
    self.ReferrerProjects = ko.observableArray();
    self.ReferrerAssignedUsers = ko.observableArray();
    self.AssignedUser = ko.observable();
    self.USERID = ko.observable();
    self.IsActive = ko.observable();
    self.TreatmentCategories = ko.observableArray();
    self.TreatmentTypes = ko.observableArray();
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
            $('#divSolicitor').show('slow');

        } else {
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

    self.SelectedReferrerProject = ko.observable();

    self.ReferrerProjectID = ko.computed(function () {
        if (self.SelectedReferrerProject() !== undefined) {
            $("#objReferrerProjectID").val(self.SelectedReferrerProject().ReferrerProjectID);
            return self.SelectedReferrerProject().ReferrerProjectID;

        }

    });

    self.ReferrerProjectIsTriage = ko.computed(function () {
        if (self.SelectedReferrerProject() !== undefined) {
            return self.SelectedReferrerProject().IsTriage;
        }
    });

    self.ReferrerProjectID.subscribe(function () {
        setUIControls(self);
        if (self.ReferrerProjectID() === '' || self.ReferrerProjectID() === undefined) {
            return false;
        }

        $('#divGrdCategorySpinner').spin(true);

        $.ajax({
            url: "/Referrer/AddReferral/GetTreatmentCategories",
            type: 'post',
            dataType: "json",
            data: { referrerProjectID: self.ReferrerProjectID() },
            success: function (result) {
                //self.TreatmentTypeID(null);
                self.CaseReferrerProjectTreatmentID(null);
                self.TreatmentCategories(result);
                $('#divGrdCategorySpinner').spin(false);
            },
            error: function () {
                $('#divGrdCategorySpinner').spin(false);
            }
        });
    });

    self.CaseReferrerProjectTreatmentID.subscribe(function () {
        setUIControls(self);
        if (self.CaseReferrerProjectTreatmentID() !== undefined && self.CaseReferrerProjectTreatmentID() !== '' && self.CaseReferrerProjectTreatmentID() !== null) {
            $('#divGrdCategorySpinner').spin(true);
            $.ajax({
                url: "/Referrer/AddReferral/GetTreatmentTypes",
                type: 'post',
                dataType: "json",
                data: { referrerProjectTreatmentID: self.CaseReferrerProjectTreatmentID() },
                success: function (result) {
                    //document.getElementById('TreatmentTypes').selectedIndex = 0;
                    self.TreatmentTypes(result);
                    $('#divGrdCategorySpinner').spin(false);
                },
                error: function () {
                    $('#divGrdCategorySpinner').spin(false);
                }
            });
        }
    });

    self.errors = ko.validation.group(self);

    self.ConfirmDialogMessage = ko.observable();
    //submit events
    this.submit = function () {
        var ifFindError = "True";
        var errors = "Errors \n";

        if (self.AssignedUserList().length < 1) {
            alert("Please select an Assigned User.");
            return false;
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
        if (drop !== "Select One" && self.PatientAge() < 16 && self.Referrer.IsRepresentation == true) {
            if ($("#Injured_FirstName").val() === "") {
                alert("If patient DOB is under 16 then patient will require Legal representation.");                
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
        else if ($("#Patient_PostCode").val() === "") {
            alert("Patient PostCode is required.");
            ifFindError = "False";
        }
        else if ($('input[name="ObjpatientConstentRaidoID"]:checked').length == 0)
        {
            alert("Consent radio button is required. ");
            ifFindError = "False";
        }
        else if ($("#ddlGender").val() == "")
        {
            alert("Please select a Gender.");
            ifFindError = "False";
        }
        else if ($('#ReferrerLocationID :selected').text() == 'Please Select') {
            alert('Cost Centre / Organisation / Location is required.');
            ifFindError = "False";
        }

      
        
        if (ifFindError === "True") {
            if (!self.isValid()) {
                $.each(self.errors(), function (index, value) {
                    errors = errors + ' ' + value();
                });
                alert(errors);
                return false;
            }
            $("#loader-main-div").removeClass("hidden");
           
            self.enableAddReferrer(false);
            var options = {
                dataType: 'html',
                contentType: "application/html; charset=utf-8",
                success: function (result) {
                    var newDialog = $(result);
                    newDialog.dialog({
                        modal: true,
                        show: 'clip',
                        hide: 'clip',
                        resizable: false,
                        width: 'auto',
                        buttons: [
                                    {
                                        text: "Enter Another Referral", click: function () {
                                            window.location.href = "/Referrer/AddReferral";
                                            $(this).dialog("close");
                                        }
                                    },
                                    {
                                        text: "Done", click: function () {
                                            window.location.href = "/Referrer/Home";
                                            $(this).dialog("close");
                                        }
                                    }
                        ]
                    }).dialog("widget").find(".ui-dialog-titlebar").hide();
                    $("#loader-main-div").addClass("hidden");
                    return false;                                                                                                                                                                                                           
                }
            };
            window.onbeforeunload = null;
            $('#frmAddReferral').ajaxSubmit(options);
        }
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
            self.SendInvoiceEmail('');
            $('#DivSendInvoiceTo').hide('slow');
        }
    });

    this.init = function () {
        $(".injured-party-req").hide();
        $('#PatientAge').attr("disabled", "disabled");
        $('#divSolicitor').hide();
        $('#DivSendInvoiceTo').hide();
        $('#Case_ReferrerProjectTreatmentID').attr("disabled", "disabled");
        $('#TreatmentTypes').attr("disabled", "disabled");
        $('#txtSelectedContact').attr("disabled", "disabled");
       // $('#ddlUserContacts').attr("disabled", "disabled");
        $('#patientHasLegalRepID').attr("checked", "checked");
        $('#RefeerProjectUserID').attr("checked", "checked");
        $('#objPatientConsent').attr("disabled", true);

        if (self.Referrer != null) {            

            if (self.Referrer.IsDrugandAlcoholTest == true)
                $("#divDrugTest").show();
            else 
                $("#divDrugTest").hide();          
            $("#hdPolicyOpenOrDropdown").val(self.Referrer.IsPolicyDetailOpenOrDropdowns);
            if (self.Referrer.IsPolicyDetail == true) {
                if (self.Referrer.IsPolicyDetail == true && self.Referrer.IsPolicyDetailOpenOrDropdowns == "Dropdowns") {
                    $('#PolicyOpenDetailDiv').hide();
                    $('#PolicyDetailDiv').show();
                }
                else {
                    $('#PolicyOpenDetailDiv').show();
                    $('#PolicyDetailDiv').hide();
                }
            }
            else
            {
                $('#PolicyOpenDetailDiv').hide();
                $('#PolicyDetailDiv').hide();
            }

            if (self.Referrer.IsNewPolicyTypes == "GIP")
            {
                $('#GIPDiv').show();
                $('#IIPDiv').hide();
                $('#TPDDiv').hide();
                $('#ELRehabDiv').hide();
            }
            else if (self.Referrer.IsNewPolicyTypes == "IIP")
            {
                $('#GIPDiv').hide();
                $('#IIPDiv').show();
                $('#TPDDiv').hide();
                $('#ELRehabDiv').hide();
            }
            else if (self.Referrer.IsNewPolicyTypes == "TPD")
            {
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
          
            if (self.Referrer.IsEmploymentDetail == true)
                $('#EmploymentDetailDiv').show();
            else
                $('#EmploymentDetailDiv').hide();

            if (self.Referrer.IsEmploeeDetail == true)
                $('#EmploymentDetailSecondDiv').show();
            else
                $('#EmploymentDetailSecondDiv').hide();
            
            if (self.Referrer.IsRepresentation == true)
            {
                $('#InjuredPartyRepresentativeli').show();
                $('#objInjuredPartyRepresentative').show();
                $('#divLegalRepresentation').show();
            }

            if (self.Referrer.IsAdditionalQuestion == true)
                $('#divAdditionalQuestion').show();
            else
                $('#divAdditionalQuestion').hide();

            if (self.Referrer.IsJobDemand == true)
                $('#divJobDemand').show();
            else
                $('#divJobDemand').hide();
        }

        $('#loadingImage').hide().ajaxStart(function () {
            $(this).show();
        }).ajaxStop(function () {
            $(this).hide();
        });
        self.showAdd(false);

        $.getJSON("/Referrer/AddReferral/GetAllInjuredRepresentativeOption", function (data) {
            self.InjuredPartyRepresentativeOptionsArray(data.slice());
        });

        $.getJSON("/Referrer/AddReferral/GetAllPrimaryCondition", function (data) {
            self.PrimaryConditionArray(data.slice());
        });

        $.getJSON("/Referrer/AddReferral/GetAllGips", function (data) {
            self.GipArray(data.slice());
        });

        $.getJSON("/Referrer/AddReferral/GetAllIips", function (data) {
            self.IipArray(data.slice());
        });

        $.getJSON("/Referrer/AddReferral/GetAllTpds", function (data) {
            self.TpdArray(data.slice());
        });

        $.getJSON("/Referrer/AddReferral/GetAllElRehabs", function (data) {
            self.ElRehabArray(data.slice());
        });
    };

    function Check_InjuredPartyRepDropDownList(event) {
        if (event.originalEvent) { // User changed
            var objInjuredID = $("#objInjuredPartyRepresentative").val();
            if (objInjuredID > 1) {
                $('#divInjuredPartyRepresentatives').show('slow');
            }
            else {
                $('#divInjuredPartyRepresentatives').hide('slow');
                self.FirstName("");
                self.LastName("");
                self.Tel1("");
                self.Tel2("");
                self.PostCode("");
                self.Email("");
                self.Relationship("");
                self.Address("");
            }
        }
        else
            $('#divInjuredPartyRepresentatives').hide('slow');
    }

    self.InjuredPartyRepDropDownList = function (obj, event) {
        Check_InjuredPartyRepDropDownList(event);
    };

    $('#txtmonthlyvalue').change(function () {
        var num = parseFloat($("#txtmonthlyvalue").val());
        var new_num = $("#txtmonthlyvalue").val(num.toFixed(2));
    });
    // alert(modelReferrer.EyptReferrerLocationID);
    $('#txtweeklyvalue').change(function () {
        var num = parseFloat($("#txtweeklyvalue").val());
        var new_num = $("#txtweeklyvalue").val(num.toFixed(2));
    });
    $(document).ready(function () {
        if (modelReferrer.EncryptUserID != null) {
            $("#objReferrerAssignedUser").val(modelReferrer.EncryptUserID);



            var defaultContact = {
                Details: modelReferrer.FullName + ' - ' + modelReferrer.Email,
                UserID:modelReferrer.UserID
                };

            self.CaseContacts.push(new CaseContact(ko.toJS(defaultContact)));
        }
    });

    self.btnAddAssignUserClick = function () {
        if ($("#objReferrerAssignedUser option:selected").val() == "")
        {
            alert("Please select a user");
            return false;
        }
        for (var i = 0; i < self.AssignedUserList().length; i++)
        {
            if (self.AssignedUserList()[i].EncID == $("#objReferrerAssignedUser option:selected").val())
            {
                alert("User already in the list");
                return false;
            }
        }
        var arr = { UserName: $("#objReferrerAssignedUser option:selected").text(), EncID: $("#objReferrerAssignedUser option:selected").val() };
        self.AssignedUserList.push(arr);
    }

    self.btnDeleteAssignUserClick = function (_EncID) {        
        self.AssignedUserList.remove(function (user) {
            return user.EncID == _EncID;
        });
    }
}   

function setUIControls(self) {
    if (self.ReferrerProjectID() === '' || self.ReferrerProjectID() === undefined) {
        self.CaseReferrerProjectTreatmentID(undefined);
        //self.TreatmentTypeID(undefined);
        $('#Case_ReferrerProjectTreatmentID').attr("disabled", "disabled");
        $('#TreatmentTypes').attr("disabled", "disabled");
    } else {
        $('#Case_ReferrerProjectTreatmentID').removeAttr('disabled');
        $('#TreatmentTypes').removeAttr('disabled');
    }

    if (self.CaseReferrerProjectTreatmentID() === '' || self.CaseReferrerProjectTreatmentID() === undefined) {
        //self.TreatmentTypeID(undefined);
        $('#TreatmentTypes').attr("disabled", "disabled");
    } else {
        $('#TreatmentTypes').removeAttr('disabled');
    }
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