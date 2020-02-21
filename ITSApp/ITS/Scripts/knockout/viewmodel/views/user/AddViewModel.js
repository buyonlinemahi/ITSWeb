function AddViewModel() {
    var self = this;

    self.UserTypes = [{ UserType: "Internal", UserTypeID: 1 }, { UserType: "Referrer", UserTypeID: 2 }, { UserType: "Supplier", UserTypeID: 3 }];
    self.ReferrerTypes = [{ ReferrerType: "Referrer Admin", ReferrerTypeID: "Referrer Admin" }, { ReferrerType: "Referrer Project Admin", ReferrerTypeID: "Referrer Project Admin" }, { ReferrerType: "Referrer Project User", ReferrerTypeID: "Referrer Project User" }];
    self.SupplierTypes = [{ SupplierType: "Supplier Admin", SupplierTypeID: "Supplier Admin" }, { SupplierType: "Supplier User", SupplierTypeID: "Supplier User" }];
    self.SelectedUserTypeID = ko.observable();
    self.Referrers = ko.observableArray();
    self.Suppliers = ko.observableArray();
    self.ReferrerLocations = ko.observableArray();
    self.ReferrerProjects = ko.observableArray();
    self.UserProjects = ko.observableArray();
    self.ReferrerID = ko.observable();
    self.SupplierID = ko.observable();
    self.ReferrerTypeID = ko.observable();
    self.SupplierTypeID = ko.observable();
    self.ReferrerLocationID = ko.observable();
    self.userSelectedProject = ko.observable();
    $(".phoneMaskformat").mask("99999 999999");

    self.SelectedUserTypeID.subscribe(function (userTypeId) {
        switch (userTypeId) {
            case 2:
                var ajax = AjaxUtil.post('/UserShared/GetReferrers', 'json');
                ajax.done(function (resp) {
                    ko.mapping.fromJS(resp, {}, self.Referrers);
                });
                break;
            case 3:
                var ajax = AjaxUtil.post('/UserShared/GetSuppliers', 'json');
                ajax.done(function (resp) {
                    ko.mapping.fromJS(resp, {}, self.Suppliers);
                });
                break;
        }
    });



    $("#frmAddUser").submit(function () {
        // to check if username and password are same(validation)
        if ($('#txtUserName').val() == $('#Password').val()) {
            alert('UserName and Password could not be same!');
            $("#Password").focus();
            return false;
        }
        else {
            return true;
        }
    });


    self.ReferrerID.subscribe(function () {
        var ajax = AjaxUtil.post('/UserShared/GetLocationsByReferrerID', 'json', { referrerID: self.ReferrerID() });
        ajax.done(function (locations) {
            ko.mapping.fromJS(locations, {}, self.ReferrerLocations);
        });

    });

    self.ReferrerAssignmentChange = function (referrerID) {        
        if (referrerID != undefined)  {            
            $.post("/User/GetReferrerProjectsByReferrerID", { referrerID: referrerID }, function (data) {
                ko.mapping.fromJS(data, {}, self.ReferrerProjects);
            });
        }
    };
    
    self.ReferrerProjectChange = function (ReferrerProjectID)
    {
        if (self.ReferrerID() != undefined) {
            self.UserProjects([]);            
            self.ReferrerAssignmentChange(self.ReferrerID());
            if (self.ReferrerTypeID() != "Referrer Project Admin") 
            {
                $("#ddlReferrerProjects").removeClass("mrgleft15");
            }
        }
    }

    self.addProjectToList = function () {
        if (self.ReferrerProjects().length > 0) {           
            self.UserProjects.push(self.ReferrerProjects()[$("#ddlReferrerProjects option:selected").index()]);
            self.ReferrerProjects.remove(self.ReferrerProjects()[$("#ddlReferrerProjects option:selected").index()]);
            $("#tblUserProject").removeClass("hide");
            $("#ddlReferrerProjects").addClass("mrgleft15");
        }
    };

    self.removeProjectFromList = function (item) {
        if (confirm("Are you sure?")) {
            self.UserProjects.remove(item);
            self.ReferrerProjects.push(item);
            if (self.UserProjects().length == 0) {
                $("#tblUserProject").addClass("hide");
            }
        }
    };
};


