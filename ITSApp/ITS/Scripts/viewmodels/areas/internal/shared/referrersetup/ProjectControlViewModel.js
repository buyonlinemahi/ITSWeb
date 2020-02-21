/*
*  Latest Version  : 1.8


*  Modified by  : Robin Singh
*  Version      : 1.0
*  Date         : 15-Nov-2012
*  Description  : Added viewModel to Implement add project - referrer setup

*  Modified by  : Anuj Batra  
*  Version      : 1.1  
*  Date         : 15-Nov-2012   
*  Description  : Updated viewModel to Add and update the ViewModel.  

*  Modified by  : Robin Singh  
*  Version      : 1.2  
*  Date         : 16-Nov-2012  
*  Description  : Updated viewModel.  

*  Modified by  : Robin Singh  
*  Version      : 1.3  
*  Date         : 17-Nov-2012  
*  Description  : Added and Updated viewModel to SaveProjectTreatment and updated ProjectControlViewModel  for visible /invisible.

*  Modified by  : Harpreet Singh,Robin Singh 
*  Version      : 1.4 
*  Date         : 19-Nov-2012  
*  Description  : Modified ViewModal to Display referrer projects.

*  Modified by  : Robin Singh 
*  Version      : 1.5
*  Date         : 21-Nov-2012  
*  Description  : Added viewModel to save TreatmentCategory. 

*  Modified by  : Robin Singh, Harpreet Singh
*  Version      : 1.6
*  Date         : 22-Nov-2012  
*  Description  : Added Method SaveProjectTreatment to Save ReferrerProjectTreatments and Method GetPricingTypesID to get AllPricingTypesBycategoryId.

*  Modified by  : Robin Singh, Harpreet Singh
*  Version      : 1.7
*  Date         : 27-Nov-2012  
*  Description  : Modified Method GetPricingTypesID and GetPricingTypesName to Get PricingTypesName.

*  Modified by  : Robin Singh
*  Version      : 1.8
*  Date         : 14-Dec- 2012
*  Description  : Implement Validation for Checkbox Selection to add Project and enable/disable values.

*/

//!!!! ---- TODO: TRANSFER OTHER VIEWMODELS IN A SEPARATE JS FILE AFTER DURING CLEANUP/REFACTOR PHASE ----- !!!!!

function ProjectControlViewModel() {
};

function TreatmentCategory(name, val, enabled, referrerProjectID, isTriage) {
    var self = this;
    self.TreatmentCategoryName = ko.observable(name);
    self.TreatmentCategoryID = ko.observable(val);
    self.Enabled = ko.observable(enabled);
    self.ReferrerProjectTreatmentID = ko.observable();
    self.ReferrerProjectID = ko.observable(referrerProjectID);
    self.IsTriage = ko.observable(isTriage);
}

function ProjectControlDetailsViewModel() {
    //dont use protectedobservable for now. build for simplicity first before going to a complicated route.
    var self = this;
    self.ProjectName = ko.observable('').extend({ required: true });
    self.ReferrerID = ko.observable('').extend({ required: true });
    self.ReferrerProjectID = ko.observable();
    self.StatusID = ko.observable(2);
    self.FirstAppointmentOffered = ko.observable(false);
    self.message = ko.observable();
    self.TreatmentCategoryName = ko.observable();
    self.TreatmentCategoryID = ko.observable();
    self.visibleAmend = ko.observable(false);
    self.visibleSave = ko.observable(true);
    self.visibleUpdate = ko.observable(true);
    self.visiblecancel = ko.observable(false);
    self.enableAmmend = ko.observable(false);
    self.enableSave = ko.observable(true);
    self.enableprojectname = ko.observable(true);
    self.enableFirstAppointmentOffered = ko.observable(true);
    self.Enabled = ko.observable(false);
    self.IsEnabled = ko.observable();
    self.TabIsEnabled = ko.observable(false);
    self.EnableTreatmentCategory = ko.observable(true);
    self.Flag = ko.observable();
    self.Projects = ko.observableArray();
    self.Status = ko.observable();
    self.NoRecordFound = ko.observable(true);
    self.ReferrerSelectedTreatmentCategories = ko.observableArray([]);
    self.ProjectTreatmentCategories = ko.observableArray([]);
    self.TreatmentCategories = ko.observableArray([]);
    self.ViewEnabled = ko.observable(false);
    self.ViewConfimEnabled = ko.observable(true);
    self.ViewOReditFlag = ko.observable();
    self.TriageChkIsEnabled = ko.observable(true);
    self.TriageChkIsActive = ko.observable(true);
    self.IsTriage = ko.observable(false);
    self.CentralEmail = ko.observable();
    self.EmailSendingOptions = ko.observableArray([]);
    self.EmailSendingOptionID = ko.observable(0);
    self.EmailSendingOptions = [{ Option: 'Send All', OptionID: 1 }, { Option: 'CC All', OptionID: 2 }, { Option: 'None', OptionID: 3}];
    self.EnableEmailoptions = ko.observable(true);

    // function to initialise Project Control
    this.GetReferrerProjects = function (model) {

        if (model.length <= 0) {
            self.NoRecordFound(true);
            self.Projects.removeAll();
            self.Status("There are no projects currently associated with this referrer");
        }
        else if (model != null) {
            self.Projects(model);
            self.NoRecordFound(false);
        }
        else {
            self.NoRecordFound(false);
        }
    };


    this.viewProjectDetails = function (project) {

        self.ReferrerProjectID(project.ReferrerProjectID);
        self.BindTreatmentCategoryToEdit(project.ReferrerProjectID, false);
        $("#divProjectDetailContainer").dialog("open");
        self.ViewEnabled(true);
        enableDisablepricingMatrix(false);
        self.ViewConfimEnabled(false);
        self.ViewOReditFlag(false);
        self.disableEdit();

    };

    this.editProjectDetails = function (project) {
        self.ReferrerProjectID(project.ReferrerProjectID);
        self.BindTreatmentCategoryToEdit(project.ReferrerProjectID, true);
        $("#divProjectDetailContainer").dialog("open");
        self.ViewEnabled(false);
        enableDisablepricingMatrix(true);
        self.ViewOReditFlag(true);
        self.ViewConfimEnabled(true);
    };

    this.BindTreatmentCategoryToEdit = function (ReferrerProjectID, flag) {

        $.ajax({
            url: "/ReferrerSetup/GetReferrerProjectByReferrerProjectID/",
            type: 'post',
            data: { referrerProjectID: ReferrerProjectID },
            dataType: 'json',
            success: function (result) {

                self.LoadProjectDetails(result);
                if (flag)
                    self.MakeEditableproject();
                self.ReferrerProjectID(ReferrerProjectID);
                self.ReferrerSelectedTreatmentCategories([]);
                $.each(result.TreatmentCategories, function (index, value) {
                    if (value.Enabled != false) {
                        self.ReferrerSelectedTreatmentCategories.push(value);
                    }
                });
                self.ProjectTreatmentCategories(self.ReferrerSelectedTreatmentCategories());
            },
            error: function (result) {
            }
        });
    }

    this.LoadProjectDetails = function (project) {


        self.clearModelValues();
        self.StatusID(project.StatusID);
        self.Enabled(project.Enabled);
        self.EmailSendingOptionID(project.EmailSendingOptionID);
        self.CentralEmail(project.CentralEmail);
        self.ReferrerID(project.ReferrerID);
        self.ProjectName(project.ProjectName);
        self.IsTriage(project.IsTriage);
        self.ReferrerProjectID(project.ReferrerProjectID);
        self.FirstAppointmentOffered(project.FirstAppointmentOffered);

        ko.utils.arrayForEach(self.TreatmentCategories(), function (item) {
            item.ReferrerProjectID(self.ReferrerProjectID());
            item.IsTriage(self.IsTriage());

        });

        //loop thru each treatment category in projectDetailsViewModel.TreatmentCategories() and load the the values from the database.
        $.each(project.TreatmentCategories, function (index, dbValue) {
            var currentTreatmentCategory = ko.utils.arrayFirst(self.TreatmentCategories(), function (treatmentCategory) {
                return (treatmentCategory.TreatmentCategoryID() === dbValue.TreatmentCategoryID);

            });
            currentTreatmentCategory.Enabled(dbValue.Enabled);
            currentTreatmentCategory.ReferrerProjectTreatmentID(dbValue.ReferrerProjectTreatmentID);
        });



        document.getElementById('tabs-inner').style.display = 'block';
        document.getElementById('divCatagories').style.display = 'block';
    };

    this.MakeEditableproject = function () {

        self.visibleUpdate(false);
        self.visibleSave(false);
        self.visiblecancel(false);
        self.enableAmmend(true);
        self.enableSave(false);
        self.enableprojectname(false);
        self.enableFirstAppointmentOffered(false);
        self.TriageChkIsActive(false);
        self.EnableEmailoptions(false);
        self.Enabled(false);
        self.visibleAmend(true);
    }

    this.disableEdit = function () {

        self.visibleUpdate(false);
        self.visibleSave(false);
        self.visiblecancel(false);
        self.enableAmmend(false);
        self.enableSave(false);
        self.enableprojectname(false);
        self.enableFirstAppointmentOffered(false);
        self.EnableEmailoptions(false);
        self.Enabled(false);
        self.visibleAmend(false);
        self.TriageChkIsActive(false);
    };
    this.MakeupdateprojectDetails = function () {

        enablePricing(false);
        self.visibleUpdate(true);
        self.visibleSave(false);
        self.visiblecancel(true);
        self.enableAmmend(false);
        self.enableSave(false);
        self.visibleAmend(false);
        self.enableprojectname(true);
        self.enableFirstAppointmentOffered(true);
        self.EnableEmailoptions(true);
        self.EnableTreatmentCategory(false);
        self.Enabled(false);
        self.TriageChkIsActive(true);

    }
    this.CancelReferrerProjects = function () {

        enablePricing(true);
        self.visibleAmend(true);
        self.visibleUpdate(false);
        self.visibleSave(true);
        self.visiblecancel(false);
        self.enableAmmend(true);
        self.enableSave(false);
        self.enableprojectname(false);
        self.enableFirstAppointmentOffered(false);
        self.TriageChkIsActive(false);
        self.EnableEmailoptions(false);
        self.Enabled(false);
    }
    this.MakeAdableprojectDetails = function () {

        self.enableprojectname(true);
        self.visibleUpdate(false);
        self.visibleSave(true);
        self.enableFirstAppointmentOffered(true);
        self.TriageChkIsActive(true);
        self.EnableEmailoptions(true);
        self.visiblecancel(false);
        self.visibleAmend(false);
        self.enableAmmend(false);
        self.enableSave(true);
        self.ProjectTreatmentCategories([]);
        document.getElementById('tabs-inner').style.display = 'none';
        document.getElementById('divCatagories').style.display = 'none';

    }

    this.clearModelValues = function () {
        self.ReferrerID(null);
        self.ProjectName(null);
        self.ReferrerProjectID(null);
        self.FirstAppointmentOffered(false);
        self.EmailSendingOptionID(null);
        self.CentralEmail(null);
        self.IsTriage(false);
    };

    this.ReferrerProjects = function () {

        $.ajax({
            url: '/ReferrerSetup/GetReferrerProjectsbyReferrerID',
            cache: false,
            type: "POST", dataType: "json",
            data: { ReferrerID: self.ReferrerID() },
            success: function (data) {

                self.GetReferrerProjects(data);
            }
        });
    };


    this.IsDataPresent = function (newValue) {


        var IsPresent = false;
        $.each(self.ProjectTreatmentCategories(), function (index, value) {

            if (value.TreatmentCategoryID === newValue.TreatmentCategoryID() && newValue.Enabled() === false)
                IsPresent = true;
        });

        //NOTE:-Should be implemented the CheckProjectIsLinkedtoAnyCase on index page
        if (checkProjectIsLinkedtoAnyCase()) {

            //code implementation if  treatment category linked to any case
            confirm("Treatment Category cannot be unchecked. There are cases for this Project with the selected Treatment Category");
        }
        else if (IsPresent == true) {

            var confirmAnswer = confirm("If you uncheck the Treatment Category for this project, the data inserted in the Project Tabs will be deleted. Are you sure you want to uncheck this treatment category");
            if (!confirmAnswer)
                newValue.Enabled(true);
        }
    };

    this.BindTreatmentCategories = function () {

        $.ajax({
            url: "/ReferrerSetup/GetAllTreatmentCategories/",
            type: 'post',
            contentType: 'application/json',
            success: function (model) {

                self.TreatmentCategories([]);
                $.each(model, function (index, value) {

                    self.TreatmentCategories.push(new TreatmentCategory(value.TreatmentCategoryName, value.TreatmentCategoryID, false));
                });
            }
        });
    };
    this.addReferrerProject = function () {

        var pattern = /^([a-zA-Z0-9_.-])+@([a-zA-Z0-9_.-])+\.([a-zA-Z])+([a-zA-Z])+/;
        var errmsg = "";
        var hasError = false;
        var chks = document.getElementsByName('checkBoxListValidate');
        var hasChecked = false;
        for (var i = 0; i < chks.length; i++) {
            if (chks[i].checked) {
                hasChecked = true;
                break;
            }
        }
        if (hasChecked == false) {
            errmsg = "Please Checked at least One Treatment Category.";
            hasError = true;
        }

        else if (self.ProjectName() == null) {
            errmsg = "Project Name is required Field";
            hasError = true;
        }
        else if (self.EmailSendingOptionID() == null) {

            errmsg = "Select Email Sending Option";
            hasError = true;
        }

        else if (self.CentralEmail() == null) {

            errmsg = "Central Email is required Field";
            hasError = true;
        }
        else if (!pattern.test(self.CentralEmail())) {

            errmsg = "Central Email is not in correct format";
            hasError = true;

        }
        if (hasError) {
            alert(errmsg);
            return;
        }
        else {
            self.enableSave(false);
            $.ajax({
                url: "/ReferrerSetup/AddReferrerProject/",
                type: 'post',
                data: ko.toJSON(this),
                contentType: 'application/json',
                success: function (result) {
                    self.Flag(true);
                    $("#lblMsg").text("Project " + self.ProjectName() + " successfully Created")
                    $("#lblMsg").show();
                    setTimeout(function () { $("#lblMsg").hide(); }, 5000);
                    // check first if the function exists
                    if (InitProjectTreatments) {
                        InitProjectTreatments(result, { ProjectName: self.ProjectName(), ReferrerProjectID: result[0].ReferrerProjectID, StatusID: 2 });
                    }
                    self.clearModelValues();
                    self.MakeEditableproject();
                    self.BindTreatmentCategoryToEdit(result[0].ReferrerProjectID, true);
                    self.NoRecordFound(false);

                }
            });

        }

    };
  

    var selectedTreatmentCaregory;
    self.TreatmentCategoryID.subscribe(function (newSelectedProjectTreatment) {
        selectedTreatmentCaregory = newSelectedProjectTreatment;
        if (checkPricingTabForChanges()) {
            var answer = confirm("Changes made to project setup for current treatment category.  Do you wish to save changes?");
            if (answer == true) {
                savePricing();
                if (InitializeTabs) {
                    InitializeTabs(newSelectedProjectTreatment, self.IsTriage());
                }
            }
            else if (answer == false) {
                if (InitializeTabs) {
                    InitializeTabs(newSelectedProjectTreatment, self.IsTriage());
                }
                changePricingFlag();
            }
        }
        else if (!checkPricingTabForChanges()) {

            if (InitializeTabs) {
                InitializeTabs(newSelectedProjectTreatment, self.IsTriage());
            }
        }

    });

    this.MakeEditable = function () {
        self.clearModelValues();
    };

    this.UpdateReferrerProjects = function () {

        var pattern = /^([a-zA-Z0-9_.-])+@([a-zA-Z0-9_.-])+\.([a-zA-Z])+([a-zA-Z])+/;
        var errmsg = "";
        var hasError = false;

        var chks = document.getElementsByName('checkBoxListValidate');

        var hasChecked = false;
        for (var i = 0; i < chks.length; i++) {
            if (chks[i].checked) {
                hasChecked = true;
                break;
            }
        }
        if (hasChecked == false) {
            errmsg = "Please Checked at least One Treatment Category.";
            hasError = true;
        }
        else if (self.ProjectName() == null) {
            errmsg = "Project Name is required Field";
            hasError = true;
        }
        else if (self.EmailSendingOptionID() == null) {

            errmsg = "Select Email Sending Option";
            hasError = true;
        }
        else if (self.CentralEmail() == null) {

            errmsg = "Central Email is required Field";
            hasError = true;
        }
        else if (!pattern.test(self.CentralEmail())) {

            errmsg = "Central Email is not in correct format";
            hasError = true;

        }
        if (hasError) {
            alert(errmsg);
            return;
        }
        else {
            self.EnableTreatmentCategory(true);
            $.ajax({
                url: "/ReferrerSetup/UpdateReferrerProject/",
                type: 'post',
                data: ko.toJSON(this),
                contentType: 'application/json',
                success: function (result) {

                    $("#lblMsg").text("Project " + self.ProjectName() + " successfully Updated")
                    $("#lblMsg").show();
                    setTimeout(function () { $("#lblMsg").hide(); }, 5000);
                    enablePricing(true);
                    self.Flag(true);

                    if (InitProjectTreatments) {
                        InitProjectTreatments(result, null);
                    }
                    self.BindTreatmentCategoryToEdit(result[0].ReferrerProjectID, true);
                    self.MakeEditableproject();

                }
            });
            self.ReferrerProjects();
        }
    };

    this.CloseDialog = function () {
        $("#divProjectDetailContainer").dialog("close");
        self.Flag(false);
    };
    this.CloseConfirmDialog = function () {
        if (self.Flag() == true) {
            if (getPricingTabStatus()) {
                $("#TreatmentCategoryDialog").dialog("open");
            }
            else {
                $("#divProjectDetailContainer").dialog("close");
                self.Flag(false);
            }
        }
        else {
            $("#saveChangesModel").dialog("open");
        }
    };

    this.Init = function () {
        self.clearModelValues();
        ko.utils.arrayForEach(self.TreatmentCategories(), function (treatmentCategory) {
            treatmentCategory.Enabled(false);
        });
    };

    this.showModalReferrerProject = function () {
        clearAllTabs();
        var errmsg = "";
        var hasError = false;

        if (self.ReferrerID() == '') {
            hasError = true;
            errmsg = "Please Select Referrer First";
        }
        if (hasError) {
            alert(errmsg);
            return;
        }
        else {
            var New = false;
            if (InitProjectDetails) {
                InitProjectDetails(self.ReferrerID(), New);
                self.ViewEnabled(false);
                enableDisablepricingMatrix(true);
                self.ViewOReditFlag(true);
                self.ViewConfimEnabled(true);
                return true;
            }
        }
    };

    //Header Script

    $(function () {

        $("#divProjectDetailContainer").dialog({
            autoOpen: false,
            width: 1050,
            modal: true,
            buttons: {
                Cancel: function () {
                    $(this).dialog("close");
                }
            }
        });

        $("#treatmentCatagoryEditCantainer").dialog({
            autoOpen: false,
            width: 1050,
            modal: true,
            buttons: {
                Cancel: function () {
                    $(this).dialog("close");
                }
            }
        });

        $("#saveChangesModel").dialog({
            autoOpen: false,
            width: 400,
            modal: true,
            buttons: {
                Cancel: function () {
                    $(this).dialog("close");
                }
            }
        });

        $("#TreatmentCategoryDialog").dialog({
            autoOpen: false,
            width: 400,
            modal: true,
            buttons: {
                Cancel: function () {
                    $(this).dialog("close");
                }
            }
        });

        $("#dialogForPricingTabIsSaved").dialog({
            autoOpen: false,
            width: 400,
            modal: true,
            buttons: {
                "Delete all items": function () {
                    $(this).dialog("close");
                },
                "Cancel": function () {
                    $(this).dialog("close");
                }
            }
        });





        $("#CancelReferrerTreatmentWitoutSaving").button()
			.click(function () {
			    $("#TreatmentCategoryDialog").dialog("close");
			});



        $("#CloseReferrierProjectWithoutTreatmentSaving").button()
			.click(function () {
			    $("#divProjectDetailContainer").dialog("close");
			    $("#TreatmentCategoryDialog").dialog("close");
			});


        $("#btnsaveandexit").button()
			.click(function () {
			    if (projectDetailsViewModel.ReferrerProjectID() > 0)
			        projectDetailsViewModel.UpdateReferrerProjects();
			    else
			        projectDetailsViewModel.addReferrerProject();

			    $("#divProjectDetailContainer").dialog("close");
			    $("#saveChangesModel").dialog("close");

			});

        $("#btndiscardchanges").button()
			.click(function () {
			    $(this).closest('.ui-dialog-content').dialog('close');
			    $("#divProjectDetailContainer").dialog("close");
			});
        var isChecked = $('#chkSelect:checked').val() ? true : false;

    });

    $(function () {
        $("#tabs").tabs();
    });
    $(function () {
        $("#tabs-inner").tabs();
    });
};
