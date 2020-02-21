function AddViewModel() {
    var self = this;
    
    self.IsPolicyDetail = ko.observable();
    self.IsEmploymentDetail = ko.observable();
    self.IsPolicyDetailOpenOrDropdowns = ko.observable();
    self.IsEmploeeDetail = ko.observable();
    self.IsDrugandAlcoholTest = ko.observable();
    self.IsAdditionalQuestion = ko.observable();
    self.IsRepresentation = ko.observable();
    self.IsJobDemand = ko.observable();
    self.SaveButtonDisable = ko.observable(false);
    self.IsNewPolicyTypes = ko.observable();
    $(".phoneMaskformat").mask("99999 999999");
    self.SaveValidatedClick = function ($form, event) {
        self.SaveButtonDisable(true);
    };


//---------------------------For IsPolicy Details-----------------------------------//
    $('.first').change(function () {
        DisabledIsNewPolicyTypes();
    });

    function EnablePolicyDetailDropDown() {
        $(".first").attr('disabled', false);
    }

    function DisabledPolicyDetailDropDown() {
        $(".first").attr('checked', false);
       
    }

//-----------------------------END----------------------------------------------------//

//-----------------------For IsNewPolicyTypes-----------------------------------------//
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
//--------------------------------------END--------------------------------------------//
}