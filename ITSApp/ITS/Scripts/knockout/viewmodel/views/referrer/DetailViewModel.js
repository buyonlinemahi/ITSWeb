function ReferrerDetailViewModel(model) {
    var self = this;
    self.ReferrerID = ko.observable();
    self.EncryptedReferrerID = ko.observable();
    self.ReferrerName = ko.observable();
    self.ReferrerContactFirstName = ko.observable();
    self.ReferrerContactLastName = ko.observable();
    self.ReferrerMainContactEmail = ko.observable();
    self.ReferrerMainContactFax = ko.observable();
    self.ReferrerMainContactPhone = ko.observable();
    self.IsPolicyDetail = ko.observable();
    self.IsEmploymentDetail = ko.observable();
    self.IsPolicyDetailOpenOrDropdowns = ko.observable();
    self.IsNewPolicyTypes = ko.observable();
    self.DateAdded = ko.observableArray();
    $(".phoneMaskformat").mask("99999 999999");
    self.DisableUpdateButton = ko.observable(false);


   

    self.UpdateReferrerDetailFormBeforeSubmit = function (arr, $form, options) {
        if ($form.jqBootstrapValidation('hasErrors')) {
            return false;
        }
        self.DisableUpdateButton(true);
        return true;
    };

    ko.mapping.fromJS(model, {}, self);

    if (model.IsPolicyDetailOpenOrDropdowns == "Opens") {
        $("#IsPolicyDetailOpenOrDropdownsYes").attr("checked", true);        
    }
    else {
        $("#IsPolicyDetailOpenOrDropdownsNo").attr("checked", true);
       
    }
    //---------------- For IsPolicy Details-----------------------------------//
    if (model.IsPolicyDetail == true || model.IsNewPolicyTypes == false) {
        DisabledIsNewPolicyTypes();
    }
    $('.first').change(function () {
        DisabledIsNewPolicyTypes();
    });

    function EnablePolicyDetailDropDown() {
        $(".first").attr('disabled', false);
    }

    function DisabledPolicyDetailDropDown() {
        $(".first").attr('checked', false);
        
    }

//---------------------END----------------------------------------------------//



    //----------------For IsNewPolicyTypes --------------------------------------//

    if (model.IsNewPolicyTypes != null || model.IsNewPolicyTypes == "") {
        DisabledPolicyDetailDropDown();
    }


    if (model.IsNewPolicyTypes == "GIP") {       
        $("#IsNewPolicyTypesGIP").attr("checked", true);
    }
    else if (model.IsNewPolicyTypes == "IIP") {
        $("#IsNewPolicyTypesIIP").attr("checked", true);
    }
    else if (model.IsNewPolicyTypes == "TPD") {
        $("#IsNewPolicyTypesTPD").attr("checked", true);
    }
    else {
        $("#IsNewPolicyTypesELRehab").attr("checked", true);
    }

    $('.second').change(function () {
        DisabledPolicyDetailDropDown();
        $("#IsPolicyDetailOpenOrDropdownsDiv").hide();
    });
     
    function EnableIsNewPolicyTypes() {
        $(".second").attr('disabled', false);
    }

    function DisabledIsNewPolicyTypes() {
        $(".second").attr('checked', false);
       
    }
//---------------------END----------------------------------------------------//
}

