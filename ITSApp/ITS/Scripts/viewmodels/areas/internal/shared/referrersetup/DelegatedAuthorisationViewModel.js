/*Latest Version : 1.5
Author : vishal
Dated : 12-08-2012
======================================
Modified by : Vishal 
Version     : 1.2
Description : Modify Model add amend Cancle ,Update Button functionality
Description : Modify Methods And Remove Alerts
Date        : 12-10-2012
===============================================
Modified by : Pardeep 
Version     : 1.3
Description : Changes done to make textbox enable/disable on the click of Ammend button
Date        : 12-Dec-2012
===============================================
Modified by : Vishal 
Version     : 1.4
Description : Changes done to make Save button functionality and remove add methods
Date        : 12-Dec-2012
===============================================
Modified by : Vishal 
Version     : 1.5
Description : Changes done to make Validation and Remove Unused Code
Date        : 14-Dec-2012


*/


function Delegations(delegatedAuthorisationID, deletegatedAuthorisationName, deletegatedAuthorisationTypeId, delegatedAuthorisationTypes, referrerProjectTreatmentID, selectedTreatmentCategoryID, amount, referrerProjectTreatmentAuthorisationID) {
    var self = this;
    self.DelegatedAuthorisationID = ko.observable(delegatedAuthorisationID);
    self.DeletegatedAuthorisationName = ko.observable(deletegatedAuthorisationName);
    self.DelegatedAuthorisationTypeID = ko.observable(deletegatedAuthorisationTypeId);
    self.DelegatedAuthorisationTypes = ko.observableArray(delegatedAuthorisationTypes);
    self.ReferrerProjectTreatmentID = ko.observable(referrerProjectTreatmentID);
    self.SelectedTreatmentCategoryID = ko.observable(selectedTreatmentCategoryID);
    self.Amount = ko.observable(amount).extend({ number: { params: true, message: 'Please enter a number in price matrix'} });
    self.ReferrerProjectTreatmentAuthorisationID = ko.observable(referrerProjectTreatmentAuthorisationID);
    self.errors = ko.validation.group(self);




    self.DelegatedAuthorisationTypeID.subscribe(function (newValue) {
        self.Amount(0);
    });

    self.IsDivVisible = ko.computed(function () {
        var computedResult;
        if (self.DelegatedAuthorisationID() == selectedTreatmentCategoryID) {
            computedResult = true;
        }
        else {
            computedResult = false;
        }
        return computedResult;
    }, self);

};

function DelegatedAuthorisationViewModel() {
    var self = this;
    self.Delegates = ko.observableArray([]);
    self.ReferrerProjectTreatmentID = ko.observable();
    self.SelectedTreatmentCategoryID = ko.observable();
    // self.DelegatedAuthorisationTypes = [{ TypeID: 1, DelegatedAuthorisationTypeName: "Sessions" }, { TypeID: 2, DelegatedAuthorisationTypeName: "Cost"}];
    self.DelegatedAuthorisationTypes = [{ TypeID: 2, DelegatedAuthorisationTypeName: "Cost"}];
    self.ViewEnabled = ko.observable();

    this.Initialize = function (referrerProjectTreatment) {
        self.SelectedTreatmentCategoryID(referrerProjectTreatment.TreatmentCategoryID);
        self.ReferrerProjectTreatmentID(referrerProjectTreatment.ReferrerProjectTreatmentID);
        self.Delegates.removeAll();
        this.GetAllService();
    };


    this.GetAllService = function (IsRadioEnabled) {
        $.ajax({
            url: "/ReferrerDelegateAuthorisation/GetAllDelegatedAuthorisation/",
            type: 'post',
            data: { referrerProjectTreatmentID: self.ReferrerProjectTreatmentID() },
            success: function (services) {
                $.each(services, function (index, value) {
                    self.Delegates.push(new Delegations(services[index].DelegatedAuthorisationID, services[index].DeletegatedAuthorisationName, services[index].DelegatedAuthorisationTypeID, self.DelegatedAuthorisationTypes, self.ReferrerProjectTreatmentID, self.SelectedTreatmentCategoryID(), services[index].Amount, services[index].ReferrerProjectTreatmentAuthorisationID));
                });
            }
        });
    };


    this.EnableDisable = function (flag) {

        self.ViewEnabled(flag);

    };
    this.ClearDelegateModel = function () {
        self.Delegates.removeAll();
    };
    self.UpdateReferrerProjectTreatmentAuthorisationClick =
     function () {
         $.ajax({
             url: "/ReferrerDelegateAuthorisation/UpdateReferrerProjectTreatmentAuthorisation/",
             type: 'post',
             data: ko.toJSON(self.Delegates()),
             contentType: 'application/json',
             success: function (result) {
                 alert(result);
             }
         });
     };

};
