
/*
*  Latest Version  : 1.3

*  Modified by  : Pardeep Kumar
*  Version      : 1.1
*  Date         : 14-Nov-2012
*  Description  : Updated viewModel to implement the changes to add the user according to the selected userTypeId

*  Modified by  : Pardeep Kumar
*  Version      : 1.2
*  Date         : 19-Nov-2012
*  Description  : Updated viewModel to implement the changes to display referrer names and referrer location for the selected referrer
*               : Updated viewModel to bind the supplier dropdownlist (date:- 22-Nov-2012)

*  Modified by  : Freddie Tan
*  Version      : 1.3
*  Date         : 26-Nov-2012
*  Description  : Referrer variables/script clean up
*               : removed unsused functions

*/

function UserControlViewModel() {
    var self = this;
    self.FirstName = ko.protectedObservable();
    self.LastName = ko.protectedObservable();
    self.Email = ko.protectedObservable();
    self.UserName = ko.protectedObservable();
    self.UserID = ko.protectedObservable();
    self.Password = ko.protectedObservable();
    self.UserTypeID = ko.protectedObservable();
    self.SelectedUserTypeID = ko.observable();
    self.Fax = ko.protectedObservable();
    self.Telephone = ko.protectedObservable();
    self.ReferrerID = ko.protectedObservable();
    self.ReferrerLocationID = ko.protectedObservable();
    self.SupplierID = ko.protectedObservable();
    self.ShowSaveButton = ko.observable(false);
    self.ShowEditButton = ko.observable(false);

    self.EnableUserGroup = ko.observable(false);
    self.EnableUserType = ko.observable(false);


    self.bindReferrerLocation = ko.observable(); //variable not used
    self.getReferrerLocationID = ko.observable();
    self.selectedSupplierID = ko.observable();

    //referrer
    self.SelectedReferrerID = ko.observable();
    self.referrerName = ko.observableArray();
    self.referrerLocationNames = ko.observableArray();

    // SupplierName values are dummy for this time
    self.SupplierNames = ko.observableArray();

    // observable variables(model) for _InternalSecurityPartial (Checkboxes)
    self.internalTasksChecked = ko.observable(true);
    self.internalCaseSearchChecked = ko.observable(false);
    self.internalReferrerSetupChecked = ko.observable(false);
    self.internalSupplierSetupChecked = ko.observable(false);
    self.internalPractitionerChecked = ko.observable(false);
    self.internalFinanceChecked = ko.observable(false);
    self.internalReportingChecked = ko.observable(false);
    self.internalManagementChecked = ko.observable(false);
    self.internalResourcesChecked = ko.observable(false);

    self.internalReferrerAccReffChecked = ko.observable(false);
    self.internalResourcesChecked = ko.observable(false);
    self.internalIniAssessChecked = ko.observable(false);
    self.internalAuthorizeChecked = ko.observable(false);
    self.internalReviewChecked = ko.observable(false);
    self.internalFinalAssesChecked = ko.observable(false);

    self.internalPrintQueChecked = ko.observable(false);
    self.internalPrintChqChecked = ko.observable(false);
    self.internalAddPractChecked = ko.observable(false);
    self.internalCaseTaskChecked = ko.observable(false);

    self.internalHistoryChecked = ko.observable(false);
    self.internalReferralFormChecked = ko.observable(false);
    self.internalLogsChecked = ko.observable(false);
    self.internalPricingChecked = ko.observable(false);
    self.internalPayableChecked = ko.observable(false);
    self.internalAccountsChecked = ko.observable(false);
    self.internalCaseNotesChecked = ko.observable(false);
    self.internalUploadDocumentsChecked = ko.observable(false);


    // observable variables(model) for _ReferrerSecurityPartial (Checkboxes)
    self.referrerAddReffChecked = ko.observable(false);
    self.referrerViewReportChecked = ko.observable(false);
    self.referrerApproveRecomChecked = ko.observable(false);
    self.referrerModifyRecomChecked = ko.observable(false);
    self.referrerCloseCaseChecked = ko.observable(false);

    // observable variables(model) for _SupplierSecurityPartial (Checkboxes)
    self.supplierCaseChecked = ko.observable(false);
    self.supplierViewRefferalsMainChecked = ko.observable(false);
    self.supplierInitialAssessChecked = ko.observable(false);
    self.supplierInitialAssessReportChecked = ko.observable(false);
    self.supplierNextAssessReportChecked = ko.observable(false);
    self.supplierCaseStoppedChecked = ko.observable(false);

    self.supplierPatientManagementChecked = ko.observable(false);
    self.supplierViewRefferalsCaseScreenChecked = ko.observable(false);

    self.supplierPatientManagementDisabled = ko.observable('disabled');
    self.supplierViewRefferalsDisabled = ko.observable('disabled');

    // observable variables(model) to set the name of usertype
    self.selectedUserType = ko.observable('');
    self.UserTypes = ko.observableArray([
        { UserType: 'Internal', ID: 1 },
        { UserType: 'Referrer', ID: 2 },
        { UserType: 'Supplier', ID: 3 }
    ]);


    self.SelectedUserTypeID.subscribe(function (newUserTypeValue) {
        var selectedID = newUserTypeValue;
        if (selectedID == 1) {
            $('#ReferrerPartial').css("display", "none");
            $("#InternalPartial").css("display", "inline");
            $('#supplierPartial').css("display", "none");
            self.ReferrerID(null);
            self.ReferrerLocationID(null);
            self.SupplierID(null);
            self.selectedUserType("Internal");

        }
        if (selectedID == 2) {
            $('#ReferrerPartial').css("display", "inline");
            $("#InternalPartial").css("display", "none");
            $('#supplierPartial').css("display", "none");
            self.SupplierID(null);
            self.bindReferrerAll();
            self.selectedUserType("Referrer");
        }
        if (selectedID == 3) {

            $('#ReferrerPartial').css("display", "none");
            $("#InternalPartial").css("display", "none");
            $('#supplierPartial').css("display", "inline");
            self.ReferrerID(null);
            self.ReferrerLocationID(null);
            self.selectedUserType("Supplier");
            self.bindSupplierAll();
        }
    });

    self.SelectedReferrerID.subscribe(function (newReferrerIDValue) {
        var newSelectedReferrerID = newReferrerIDValue;
        self.referrerLocationNames.removeAll();
        if (newSelectedReferrerID != null) {
            $.ajax({
                url: "/Referrer/GetReferrerLocationsByReferrerID/",
                cache: false,
                type: "POST", dataType: "json",
                data: { referrerID: newSelectedReferrerID },
                success: function (data) {
                    self.referrerLocationNames(data);
                }
            });
        }
    });

    // method to bind Supplier All in Supplier Dropdownlist
    this.bindSupplierAll = (function () {
        //only load one time
        if (self.SupplierNames().length == 0) {
            $.ajax({
                url: "/SupplierShared/GetAllSupplier/",
                cache: false,
                type: "POST", dataType: "json",
                success: function (data) {
                    self.SupplierNames(data);
                }
            });
        }
    });

    // method to bind Referre All in Referrer Dropdownlist
    this.bindReferrerAll = (function () {
        //only load one time
        if (self.referrerName().length == 0) {
            $.ajax({
                async: false,
                url: "/Referrer/GetAllReferrer/",
                cache: false,
                type: "POST", dataType: "json",
                success: function (data) {
                    self.referrerName(data);
                }
            });
        }
    });

    // select event to pick SupllierId
    this.selectedSupplierID.subscribe(function (selectedSupplierId) {
        self.SupplierID = ko.protectedObservable(selectedSupplierId);
    });

    this.updateModel = function (model) {
        self.FirstName(model.FirstName);
        self.LastName(model.LastName);
        self.Email(model.Email);
        self.UserName(model.UserName);
        self.UserID(model.UserID);
        self.Password(model.Password);
        self.SelectedUserTypeID(model.UserTypeID);
        self.Fax(model.Fax);
        self.Telephone(model.Telephone);
        self.ReferrerID(model.ReferrerID);
        self.SelectedReferrerID(model.ReferrerID);
        self.ReferrerLocationID(model.ReferrerLocationID);
        self.SupplierID(model.SupplierID);
        self.commitModel();
        self.ShowEditButton(true);
        self.EnableUserGroup(false);
        self.EnableUserType(false);
    };

    this.add = function () {
        self.FirstName(null);
        self.LastName(null);
        self.Email(null);
        self.UserName(null);
        self.UserID(null);
        self.Password(null);
        self.UserTypeID(null);
        self.SelectedUserTypeID(null);
        self.Fax(null);
        self.Telephone(null);
        self.ReferrerID(null);
        self.ReferrerLocationID(null);
        self.SelectedReferrerID(null);
        self.referrerLocationNames.removeAll();
        self.commitModel();
        self.ShowSaveButton(true);
        self.ShowEditButton(false);
        self.EnableUserGroup(true);
        self.EnableUserType(true);
        $("#txtAutoSearch").val("");
        $("#save").removeAttr('disabled');
        $(".right-textbx input").each(function () {
            $(this).prop("disabled", false);
        })
        // call the function to populate all the dropdownlists
        this.bindReferrerAll();
        this.bindSupplierAll();
    };

    this.commitModel = function () {
        self.UserTypeID(self.SelectedUserTypeID());
        self.ReferrerID(self.SelectedReferrerID());

        self.UserTypeID.commit();
        self.FirstName.commit();
        self.LastName.commit();
        self.Email.commit();
        self.UserName.commit();
        self.UserID.commit();
        self.Password.commit();
        self.Fax.commit();
        self.Telephone.commit();
        self.ReferrerID.commit();
        self.ReferrerLocationID.commit();
        self.SupplierID.commit();
    };

    this.resetAll = function () {

        self.FirstName.reset();
        self.LastName.reset();
        self.Email.reset();
        self.UserName.reset();
        self.UserID.reset();
        self.Password.reset();
        self.Fax.reset();
        self.Telephone.reset();
        self.ReferrerID.reset();
        self.ReferrerLocationID.reset();
        self.SupplierID.reset();
        self.UserTypeID.reset();
        self.SelectedUserTypeID(self.UserTypeID());
        self.SelectedReferrerID(self.ReferrerID());
        self.EnableUserGroup(false);
        self.EnableUserType(false);
        $(".mgt-left input").prop("disabled", true);
        $("#txtAutoSearch").prop("disabled", false);
        $("#updateuser").css("display", "none");
        $("#editUser").css("display", "inline");
        $("#cancelUpdate").css("display", "none");
        $("#confermationMsg").css("display", "none");
    };

    this.UpdateUser = function () {
        self.commitModel();
        if (self.UserTypeID() == 1 || self.UserTypeID() == 3) {
            self.ReferrerID(null);
        }
        self.FirstName.commit();
        self.LastName.commit();
        self.Email.commit();
        self.UserName.commit();
        self.UserID.commit();
        self.Password.commit();
        self.Fax.commit();
        self.Telephone.commit();
        self.ReferrerID.commit();
        self.ReferrerLocationID.commit();
        self.SupplierID.commit();

        $.ajax({
            url: "/User/UpdateUser/",
            type: 'post',
            data: ko.toJSON(this),
            contentType: 'application/json',
            success: function (result) {

                $("#confermationMsg").text("• User '" + result.UserName + "' successfully updated under '"
                                + self.selectedUserType() + "' portal.");

                $("#confermationMsg").css("display", "inline");
            }
        });

        $("#updateuser").css("display", "none");
        $("#editUser").css("display", "inline");
        $("#cancelUpdate").css("display", "none");
        $(".mgt-left input").prop("disabled", true);
        $("#txtAutoSearch").prop("disabled", false);
        self.EnableUserType(false);
    };

    this.disableInputControls = function () {
        $(".mgt-left input").prop("disabled", true);
        $("#txtAutoSearch").prop("disabled", false);
    };



    this.saveNewUser = function () {
        self.commitModel();

        var emailReg = /^([\w-\.]+@([\w-]+\.)+[\w-]{2,4})?$/;
        var errmsg = "";
        var hasError = false;

        if (self.UserName() == null) {
            //$("#UserName").removeClass('right-textbx').addClass("right-errtextbx");

            hasError = true;
            errmsg = "User Login is required Field";
        }
        else if (self.Password() == null) {
            hasError = true;
            errmsg = "User Password is required Field";
        }
        else if (self.FirstName() == null) {
            hasError = true;
            errmsg = "User First Name is required Field";
        }
        else if (self.LastName() == null) {
            hasError = true;
            errmsg = "User Surname is required Field";
        }
        else if (self.Email() == null) {
            hasError = true;
            errmsg = "Email-Id is required Field";
        }
        else if (!emailReg.test(self.Email())) {
            hasError = true;
            errmsg = "Email-Id is not in correct format";
        }

        if (hasError) {
            alert(errmsg);
            return;
        }
        else {
            self.commitModel();
            $.ajax({
                url: "/User/AddUser/",
                type: 'post',
                data: ko.toJSON(this),
                contentType: 'application/json',
                success: function (result) {
                    $("#confermationMsg").text("• User '" + result.UserName + "' successfully added to '"
                                + self.selectedUserType() + "' portal.");

                    $("#confermationMsg").css("display", "inline");
                    self.ShowSaveButton(false);
                    self.ShowEditButton(true);
                }
            });
        }
    };

    this.editClick = function () {
        self.ShowSaveButton(false);
        $(".mgt-left input").prop("disabled", false);
        $("#txtAutoSearch").prop("disabled", false);
        $("#updateuser").css("display", "inline");
        $("#cancelUpdate").css("display", "inline");
        $("#editUser").css("display", "none");
        $("#confermationMsg").css("display", "none");

        self.EnableUserGroup(true);
        self.EnableUserType(true);
        //this.bindReferrerAll();
        //this.bindSupplierAll();

    };


    // event to selectall checkbox for _ReferrerSecutiryPartial
    this.referrerSelectAll = (function () {
        self.referrerAddReffChecked(true);
        self.referrerViewReportChecked(true);
        self.referrerApproveRecomChecked(true);
        self.referrerModifyRecomChecked(true);
        self.referrerCloseCaseChecked(true);
    });
    // event to deSelectall checkbox for _ReferrerSecutiryPartial
    this.referrerDeSelectAll = (function () {
        self.referrerAddReffChecked(false);
        self.referrerViewReportChecked(false);
        self.referrerApproveRecomChecked(false);
        self.referrerModifyRecomChecked(false);
        self.referrerCloseCaseChecked(false);
    });

    // event to selectall checkbox for _InternalSecutiryPartial
    this.internalSelectAll = (function () {
        self.internalTasksChecked(true);
        self.internalCaseSearchChecked(true);
        self.internalReferrerSetupChecked(true);
        self.internalSupplierSetupChecked(true);
        self.internalPractitionerChecked(true);
        self.internalFinanceChecked(true);
        self.internalReportingChecked(true);
        self.internalManagementChecked(true);
        self.internalResourcesChecked(true);
        self.internalReferrerAccReffChecked(true);


        self.internalResourcesChecked(true);
        self.internalIniAssessChecked(true);
        self.internalAuthorizeChecked(true);
        self.internalReviewChecked(true);
        self.internalFinalAssesChecked(true);

        self.internalPrintQueChecked(true);
        self.internalPrintChqChecked(true);
        self.internalAddPractChecked(true);
        self.internalCaseTaskChecked(true);

        self.internalHistoryChecked(true);
        self.internalReferralFormChecked(true);
        self.internalLogsChecked(true);
        self.internalPricingChecked(true);
        self.internalPayableChecked(true);
        self.internalAccountsChecked(true);
        self.internalCaseNotesChecked(true);
        self.internalUploadDocumentsChecked(true);
    });

    // event to deSelectall checkbox for _InternalSecutiryPartial
    this.internalDeSelectAll = (function () {
        self.internalTasksChecked(true);
        self.internalCaseSearchChecked(false);
        self.internalReferrerSetupChecked(false);
        self.internalSupplierSetupChecked(false);
        self.internalPractitionerChecked(false);
        self.internalFinanceChecked(false);
        self.internalReportingChecked(false);
        self.internalManagementChecked(false);
        self.internalResourcesChecked(false);
        self.internalReferrerAccReffChecked(false);

        self.internalResourcesChecked(false);
        self.internalIniAssessChecked(false);
        self.internalAuthorizeChecked(false);
        self.internalReviewChecked(false);
        self.internalFinalAssesChecked(false);

        self.internalPrintQueChecked(false);
        self.internalPrintChqChecked(false);
        self.internalAddPractChecked(false);
        self.internalCaseTaskChecked(false);

        self.internalHistoryChecked(false);
        self.internalReferralFormChecked(false);
        self.internalLogsChecked(false);
        self.internalPricingChecked(false);
        self.internalPayableChecked(false);
        self.internalAccountsChecked(false);
        self.internalCaseNotesChecked(false);
        self.internalUploadDocumentsChecked(false);
    });

    // event to selectall checkbox for _SupplierSecutiryPartial
    this.supplierSelectAll = (function () {
        self.supplierCaseChecked(true);
        self.supplierViewRefferalsMainChecked(true);
        self.supplierInitialAssessChecked(true);
        self.supplierInitialAssessReportChecked(true);
        self.supplierNextAssessReportChecked(true);
        self.supplierCaseStoppedChecked(true);

        self.supplierPatientManagementChecked(true);
        self.supplierViewRefferalsCaseScreenChecked(true);

        self.supplierPatientManagementDisabled('');
        self.supplierViewRefferalsDisabled('');
    });

    // event to deSelectall checkbox for _SupplierSecutiryPartial
    this.supplierDeSelectAll = (function () {
        self.supplierCaseChecked(false);
        self.supplierViewRefferalsMainChecked(false);
        self.supplierInitialAssessChecked(false);
        self.supplierInitialAssessReportChecked(false);
        self.supplierNextAssessReportChecked(false);
        self.supplierCaseStoppedChecked(false);

        self.supplierPatientManagementChecked(false);
        self.supplierViewRefferalsCaseScreenChecked(false);

        self.supplierPatientManagementDisabled('disabled');
        self.supplierViewRefferalsDisabled('disabled');
    });

    // this code is in progress
    this.supplierCaseCheckedChanged = (function () {
        if (self.supplierCaseChecked == true) {
            self.supplierPatientManagementDisabled('disabled');
            self.supplierViewRefferalsDisabled('disabled');
            self.supplierCaseChecked(false);
        }
        else {
            self.supplierPatientManagementDisabled(' ');
            self.supplierViewRefferalsDisabled(' ');
            self.supplierCaseChecked(true);
        }
    });
}


ko.protectedObservable = function (initialValue) {
    //private variables
    var _actualValue = ko.observable(initialValue),
        _tempValue = initialValue;

    //computed observable that we will return
    var result = ko.computed({
        //always return the actual value
        read: function () {
            return _actualValue();
        },
        //stored in a temporary spot until commit
        write: function (newValue) {
            _tempValue = newValue;
        }
    });

    //if different, commit temp value
    result.commit = function () {

        if (_tempValue !== _actualValue()) {
            _actualValue(_tempValue);
        }
    };

    //force subscribers to take original
    result.reset = function () {

        _actualValue.valueHasMutated();
        _tempValue = _actualValue();   //reset temp value
    };
    return result;
};

