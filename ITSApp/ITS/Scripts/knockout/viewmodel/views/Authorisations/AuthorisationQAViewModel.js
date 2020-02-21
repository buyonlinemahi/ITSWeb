function CaseTreatmentPricingModel(data, pricingTypeName, caseid) {
    var self = this;
    self.PricingTypeName = ko.observable(pricingTypeName);
    self.CaseID = ko.observable(caseid);
    self.CaseTreatmentPricingID = ko.observable();
    self.DateOfService = ko.observable();
    self.IsComplete = ko.observable();
    self.PatientDidNotAttend = ko.observable();
    self.ReferrerPrice = ko.observable();
    self.ReferrerPricingID = ko.observable();
    self.SupplierPrice = ko.observable();
    self.SupplierPriceID = ko.observable();
    self.WasAbandoned = ko.observable();
    ko.mapping.fromJS(data, {}, self);
};

function CaseBespokeServicePricingModel(data, bespokeServiceName, referrerPrice, supplierPrice, caseid) {
    var self = this;
    self.CaseBespokeServiceID = ko.observable();
    self.TreatmentCategoryName = ko.observable();
    self.BespokeServiceName = ko.observable(bespokeServiceName);
    self.TreatmentCategoryBespokeServiceID = ko.observable();
    self.TreatmentCategoryID = ko.observable();
    self.BespokeServiceID = ko.observable();
    self.ReferrerPrice = ko.observable(referrerPrice);
    self.SupplierPrice = ko.observable(supplierPrice);
    self.CaseID = ko.observable(caseid);
    ko.mapping.fromJS(data, {}, self);

}

function AuthorisationViewModel() {
    var self = this;
    self.CaseTreatmentPricing = ko.observableArray();
    self.CaseBespokeServicePricing = ko.observableArray();
    self.SelectedReferrerTreatmentPricing = ko.observable();
    self.SelectedBespokeServicePricing = ko.observable();
    self.ReferrerPrice = ko.observable();
    self.SupplierPrice = ko.observable();
    self.CaseID = ko.observable();
    self.TreatmentTotal = ko.observable(0);
    self.TreatmentSession = ko.observable();
    self.AssessmentServiceName = ko.observable();

    this.Init = function (model) {
       
        ko.mapping.fromJS(model, {}, self);
        ko.mapping.fromJS(model.CaseAssessment.CaseID, {}, self.CaseID);

        if (model.ReferrerCaseAssessmentModification[0]!=null)
        {
        if (model.ReferrerCaseAssessmentModification[0].TreatmentSession != null)
        {
            $("#TreatmentSession").val(model.ReferrerCaseAssessmentModification[0].TreatmentSession);
        }
        if (model.ReferrerCaseAssessmentModification[0].AssessmentServiceName != "")
        {
            $("#AssessmentServiceName").val(model.ReferrerCaseAssessmentModification[0].AssessmentServiceName);
        }
        }
        if (model.CaseTreatmentPricings != null)
        {
            $.each(model.CaseTreatmentPricings, function (index, value) {
                var matchedRow = ko.utils.arrayFirst(model.TreatmentReferrerSupplierPricing, function (item) {
                    return (value.ReferrerPricingID === item.ReferrerPricingID);
                });
                if (matchedRow != null || undefined) {
                    self.CaseTreatmentPricing.push(new CaseTreatmentPricingModel(value, matchedRow.PricingTypeName));
                }

                });
        }

        if (model.CaseBespokeServicePricings != null)
        {
            $.each(model.CaseBespokeServicePricings, function (index, value) {
                var matchedRow = ko.utils.arrayFirst(model.TreatmentCategoriesBespokeServices, function (item) {
                    return (value.TreatmentCategoryBespokeServiceID === item.TreatmentCategoryBespokeServiceID);
                });
                if (matchedRow != null || undefined)
                {
                self.CaseBespokeServicePricing.push(new CaseBespokeServicePricingModel(value, matchedRow.BespokeServiceName));
            }});
        }
    };

    this.addCaseTreatmentPricing = function () {
        
        if ((self.SelectedReferrerTreatmentPricing() == "undefined") || (self.SelectedReferrerTreatmentPricing() == null))
            alert("Please Select Treatment Type.");
        else {
            self.CaseTreatmentPricing.push(new CaseTreatmentPricingModel(self.SelectedReferrerTreatmentPricing(), self.CaseID()));
            $("#divCreateCaseTreatmentGrid").modal('hide');
        }
    };

    this.addBespokeCasePricing = function () {
        
        if (self.ReferrerPrice() == null || self.SupplierPrice() == null || self.ReferrerPrice() == '' || self.SupplierPrice() == '') {
            alert("Referrer & Supplier price are required");
        }
        else {
            self.CaseBespokeServicePricing.push(new CaseBespokeServicePricingModel(self.SelectedBespokeServicePricing(), '', self.ReferrerPrice(), self.SupplierPrice(), self.CaseID()));
            self.ReferrerPrice(null); self.SupplierPrice(null);
            self.SelectedBespokeServicePricing(null);
            $("#divCreateBespokeGrid").modal('hide');
        }
    };

    this.deleteCasePricing = function (item) {
        if (confirm("Are you sure to delete this pricing")) {

            if (this.CaseTreatmentPricingID() === undefined) {
                self.CaseTreatmentPricing.remove(item);
            }
            else {
                var ajax = AjaxUtil.post('/AuthorisationsShared/DeleteCaseTreatmentPricingByCaseTreatmentPricingID', 'json', { caseTreatmentPricingID: this.CaseTreatmentPricingID });
                ajax.done(function (resp) {
                    alert(resp);
                    self.CaseTreatmentPricing.remove(item);

                });
            }
        }

    };

    this.deleteBespokeCasePricing = function (item) {
        if (confirm("Are you sure to delete this pricing")) {
            if (this.CaseBespokeServiceID() === undefined) {
                self.CaseBespokeServicePricing.remove(item);
            }
            else {
                var ajax = AjaxUtil.post('/AuthorisationsShared/DeleteCaseBespokeServiceByCaseBespokeServiceID', 'json', { caseBespokeServiceID: this.CaseBespokeServiceID });
                ajax.done(function (resp) {
                    alert(resp);
                    self.CaseBespokeServicePricing.remove(item);
                });
            }
        }
    };

    this.UpdateAuthorisationQASuccess = function (resp) {
        alert(resp);
        $("#loader-main-div").addClass("hidden");
        window.location = '/Authorisations';
    };

    self.UpdateAuthorisationQA = function (result) {
        alert("Successfully Send");        
        window.location = '/Authorisations/Index';
    };

    self.UpdateAuthorisationQAFormBeforeSubmit = function (arr, $form, options) {
        if ($form.jqBootstrapValidation('hasErrors')) return false;
        
        $("#loader-main-div").removeClass("hidden");
        return true;
    };


    self.TreatmentTotal = ko.computed(function () {
        var totalCaseTreatmentPricing = 0;
        var totalBespokePricing = 0;
        ko.utils.arrayForEach(self.CaseTreatmentPricing(), function (value) {
            totalCaseTreatmentPricing += parseFloat(ko.utils.unwrapObservable(value.ReferrerPrice()));
        });
        ko.utils.arrayForEach(self.CaseBespokeServicePricing(), function (value) {
            totalBespokePricing += parseFloat(ko.utils.unwrapObservable(value.ReferrerPrice()));
        });

        return parseFloat(totalCaseTreatmentPricing) + parseFloat(totalBespokePricing);
    });
    
    this.PrintAuthorisationQA = function () {
        var divElements = document.getElementById('content').innerHTML;
       
        document.body.innerHTML =
                "<html><head><title>Authorisation QA</title></br></br></head><body>" + divElements + "</body>";
        window.print();
        location.reload(true);
    }

};