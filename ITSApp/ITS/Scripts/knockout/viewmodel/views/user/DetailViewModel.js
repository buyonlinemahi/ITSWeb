function UserDetailViewModel() {
    var self = this;

    self.UserTypes = [{ UserType: "Internal", UserTypeID: 1 }, { UserType: "Referrer", UserTypeID: 2 }, { UserType: "Supplier", UserTypeID: 3 }]
    self.SelectedUserTypeID = ko.observable();
    self.Referrers = ko.observableArray();
    self.Suppliers = ko.observableArray();
    self.ReferrerLocations = ko.observableArray();
    self.ReferrerID = ko.observable();
    self.SupplierID = ko.observable();
    self.ReferrerTypeID = ko.observable();
    self.SupplierTypeID = ko.observable();
    self.ReferrerLocationID = ko.observable();
    self.UserName = ko.observable();
    self.Password = ko.observable();
    self.FirstName = ko.observable();
    self.LastName = ko.observable();
    self.UserTypeID = ko.observable();
    self.Email = ko.observable();
    self.Fax = ko.observable();
    self.Telephone = ko.observable();
    self.UserID = ko.observable();
    IsPasswordDirty = ko.observable(false);
    self.ReferrerProjects = ko.observableArray();
    self.UserProjects = ko.observableArray();
    self.NewUserProjects = ko.observableArray();    
    self.userSelectedProject = ko.observable();
    $(".phoneMaskformat").mask("99999 999999");
        

    self.Init = function (model) {
        if (model.UserProjects != "" && model.User.ReferrerTypes == "Referrer Project Admin") {
            $("#tblUserProject").removeClass("hide");
            ko.mapping.fromJS(model.UserProjects, {}, self.UserProjects);
            ko.mapping.fromJS(model.UserProjects, {}, self.OldUserProjects);
        }
        else if (model.UserProjects != "" && model.User.ReferrerTypes == "Referrer Project User") {
            $("#tblUserProject").addClass("hide");
            $("#hdnOldUserProjectSingleValue").val(model.UserProjects[0].ReferrerProjectID);
            self.userSelectedProject(model.UserProjects[0].ReferrerProjectID);
        }

        if (model.Suppliers != null) {
            ko.mapping.fromJS(model.Suppliers, {}, self.Suppliers);
        }
        if (model.Referrers != null) {
            ko.mapping.fromJS(model.Referrers, {}, self.Referrers);
            ko.mapping.fromJS(model.ReferrerLocations, {}, self.ReferrerLocations);
        }

        ko.mapping.fromJS(model.ReferrerProjects, {}, self.ReferrerProjects);
                
        

        if (model.User != null) {
            ko.mapping.fromJS(model.User, {}, self);
            $('#ConfirmPassword').val(model.User.Password);
            self.ReferrerTypes = [{ ReferrerType: "Referrer Admin", ReferrerTypeID: "Referrer Admin" }, { ReferrerType: "Referrer Project Admin", ReferrerTypeID: "Referrer Project Admin" }, { ReferrerType: "Referrer Project User", ReferrerTypeID: "Referrer Project User" }];
            self.SupplierTypes = [{ SupplierType: "Supplier Admin", SupplierTypeID: "Supplier Admin" }, { SupplierType: "Supplier User", SupplierTypeID: "Supplier User" }];
            ko.mapping.fromJS(model.User.ReferrerTypes, {}, self.ReferrerTypeID);
            ko.mapping.fromJS(model.User.SupplierTypes, {}, self.SupplierTypeID);
        }
    };

    self.ReferrerID.subscribe(function () {
        if (self.ReferrerID() != null) {
            var ajax = AjaxUtil.post('/UserShared/GetLocationsByReferrerID', 'json', { referrerID: self.ReferrerID() });
            ajax.done(function (locations) {
                ko.mapping.fromJS(locations, {}, self.ReferrerLocations);
            });
        }
    });

    self.PasswordValueChanged = function () {

        self.IsPasswordDirty(true);

    };
    self.UpdateDetailSuccess = function () {
        self.IsPasswordDirty(false);
        alert("Updated Sucessfully");

    };

    self.UpdateUserDetailFormBeforeSubmit = function (arr, $form, options) {        
        if ($form.jqBootstrapValidation('hasErrors')) {
            return false;
        }
        self.NewUserProjects([]);
        return true;
    }

    self.GetReferrerProjectsByReferrerID = function (referrerID) {
        if (referrerID != undefined) {
            $.post("/User/GetReferrerProjectsByReferrerID", { referrerID: referrerID }, function (data) {
                ko.mapping.fromJS(data, {}, self.ReferrerProjects);
            });
        }
    };

    self.GetReferrerProjectAssignedToUser = function () {
        if (self.ReferrerID() != undefined) {
            $.post("/User/GetReferrerProjectAssignedToUser", { referrerID: self.ReferrerID(), userID: self.UserID() }, function (data) {
                ko.mapping.fromJS(data, {}, self.UserProjects);
                $("#tblUserProject").removeClass("hide");
            });
        }
    };
    self.ReferrerProjectChange = function () {
        if (self.ReferrerID() != undefined) {
            self.UserProjects([]);
            self.GetReferrerProjectsByReferrerID(self.ReferrerID());
            if (self.ReferrerTypeID() == "Referrer Project Admin") {
                $("#ddlReferrerProjects").addClass("mrgleft15");
                self.GetReferrerProjectAssignedToUser();
            }
            else {
                $("#ddlReferrerProjects").removeClass("mrgleft15");
            }
        }
    }

    self.addProjectToList = function () {
        if (self.ReferrerProjects().length > 0) {
            self.NewUserProjects.push(self.ReferrerProjects()[$("#ddlReferrerProjects option:selected").index()]);// adding to new project list for add these only
            self.UserProjects.push(self.ReferrerProjects()[$("#ddlReferrerProjects option:selected").index()]);
            self.ReferrerProjects.remove(self.ReferrerProjects()[$("#ddlReferrerProjects option:selected").index()]);
            $("#tblUserProject").removeClass("hide");
            $("#ddlReferrerProjects").addClass("mrgleft15");
        }
    };

    self.removeProjectFromList = function (item) {
        if (confirm("Are you sure?")) {
            $.post("/User/DeleteUserProject", { referrerProjectID: item.ReferrerProjectID(), userID: self.UserID }, function (data) {
                self.NewUserProjects.remove(item);
                self.UserProjects.remove(item);
                self.ReferrerProjects.push(item);
                if (self.UserProjects().length == 0) {
                    $("#tblUserProject").addClass("hide");
                }
            });
        }
    };

    self.SentMail = function (model) {
        alertify.confirm("Are you sure you want to reset the password?",
            function (e) {
                $("#loader-main-div").removeClass("hidden");
                if (e) {
                    $.ajax({
                        url: "/Home/ResetUserpassword",
                        cache: false,
                        type: "POST", dataType: 'json',
                        contentType: 'application/json',
                        data: ko.toJSON({ uid: $("#uid").val() }),
                        success: function (data) {
                            alertify.alert(data);
                            $("#loader-main-div").addClass("hidden");
                    }});
                }
                else
                {
                    $("#loader-main-div").addClass("hidden");
                    return false;
                }
            });
    }
};
