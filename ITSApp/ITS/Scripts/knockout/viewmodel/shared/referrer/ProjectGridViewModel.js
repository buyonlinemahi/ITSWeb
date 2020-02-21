
function ReferrerProject(data) {
    var self = this;

    self.ReferrerProjectID = ko.observable();
    self.ProjectName = ko.observable();
    self.ReferrerID = ko.observable();
    self.StatusID = ko.observable();
    self.FirstAppointmentOffered = ko.observable();
    self.Enabled = ko.observable();
    self.IsTriage = ko.observable();
    self.EmailSendingOptionID = ko.observable();
    self.CentralEmail = ko.observable();
    self.IsActive = ko.observable();
    self.TreatmentCategories = ko.mapping.fromJS([]); 
    self.SelectedTreatmentCategory = ko.observable();

    var mapping = {
        'ignore': ["TreatmentCategories"]
    }

    if (data != null) {
        ko.mapping.fromJS(data, mapping, self);
    }

    if (data.TreatmentCategories != null) {
        var dataMappingOpt = {
            create: function (options) {
                return new TreatmentCategoryModel(options.data.TreatmentCategoryID, options.data.TreatmentCategoryName, options.data.ReferrerProjectTreatmentID, options.data.Pricing);
            }
        };
        ko.mapping.fromJS(data.TreatmentCategories, dataMappingOpt, self.TreatmentCategories);
    }
    ko.CommitChanges(self);

    self.UpdateReferrerProjectFormBeforeSubmit = function (arr, $form, options) {
        if ($form.jqBootstrapValidation('hasErrors')) return false;
        return true;
    };
    
    self.FilterTreatmentCategoryPricing = function (treatmentCategoryID, isTriage, pricings) {
  
        return ko.computed(function () {
            return ko.utils.arrayFilter(pricings(), function (p) {
                if (isTriage) {
                    return true;
                }
                if (!isTriage) {
                    return p.PricingTypeID() != 15;
                }
//                if (treatmentCategoryID != 1 || treatmentCategoryID != 2) {
//                    return true;
//                }
            });
        }, self)();
    };

    self.viewTreatmentCategory = function (item) {
        self.SelectedTreatmentCategory(item);
    };
};

function ReferrerProjectGridViewModel() {
    var self = this;
    self.ReferrerProjects = ko.observableArray();
    self.SelectedReferrerProject = ko.observable();

    self.Init = function (data) {
        if (data != null) {
            $.each(data, function (index, value) {
                self.ReferrerProjects.push(new ReferrerProject(value));
            });
        }
    };

    self.viewReferrerProject = function (item) {
        self.SelectedReferrerProject(item);
    };

    self.AddReferrerProjectTreatmentCategory = function(item) {
        InitAddReferrerProjectTreatmentModal(item.ReferrerProjectID(), item.IsTriage());
    };

    self.UpdateSelectedReferrerProject = function (item) {
        ko.mapping.fromJS(item, {}, self.SelectedReferrerProject);
        ko.CommitChanges(self.SelectedReferrerProject());
        alert("Updated Sucessfully");
        self.DeSelectCurrentSelectedReferrerProject();
    };

    self.DeSelectCurrentSelectedReferrerProject = function () {
        $("#divViewReferrerProject" + self.SelectedReferrerProject().ReferrerProjectID()).modal('hide');
        self.SelectedReferrerProject(null);
    };

    self.UpdateReferrerProjectGrid = function (responseText, $form) {
        self.ReferrerProjects.push(new ReferrerProject($.parseJSON(responseText)));
        alert("Added successfully");
    };

    self.AddTreatmentCategory = function (item) {
        var selectedReferrerProject = ko.utils.arrayFirst(self.ReferrerProjects(), function (referrerProject) {
            return item.ReferrerProjectID === referrerProject.ReferrerProjectID();
        });
        
        selectedReferrerProject.TreatmentCategories.push(new TreatmentCategoryModel(item.TreatmentCategoryID, item.TreatmentCategoryName, item.ReferrerProjectTreatmentID, item.Pricing));
        alert("Added successfully");
    };

    self.UpdateSelectedReferrerProjectStatus = function () {
        var ajax = AjaxUtil.post('/ReferrerProjectShared/UpdateReferrerProjectStatus', 'json', { referrerProjectID: self.SelectedReferrerProject().ReferrerProjectID(), isTriage: self.SelectedReferrerProject().IsTriage() });
        ajax.done(function (resp) {
            self.SelectedReferrerProject().StatusID(resp);
            ko.CommitChanges(self.SelectedReferrerProject());
        });
    };

    self.SetSelectedReferrerProject = function (item) {
        self.SelectedReferrerProject(item);
    };
};
