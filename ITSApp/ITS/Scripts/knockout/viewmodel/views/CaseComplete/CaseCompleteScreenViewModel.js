/// <reference path="../../../../knockout-2.0.0.js" />

ko.extenders.changeToBool = function (target) {
    var result = ko.computed({
        read: target,
        write: function (newValue) {
            if (newValue != undefined) {
                if (newValue == "1") {
                    return target(true);
                } else {
                    return target(false);
                }
            }
        }
    });
    result(target());
    return result;
};

function CaseCompleteScreenViewModel() {
    var self = this;
    self.CaseID = ko.observable();
    self.CaseTreatmentPricingType = ko.observableArray([]);
    self.CaseBespokeServicePricingType = ko.observableArray([]);
    self.VatPercentage = ko.observable(0);
    self.NotifyReferrer = ko.observable();
    self.ReferrerAndSupplierVATPricing = ko.observable();
    self.IsNotifyEmailtoReferrer = ko.observable(false).extend({ changeToBool: null });
    self.TreatmentReferrerSupplierPricings = ko.observableArray([]);


    self.IsNotifyEmailtoReferrer.subscribe(function (newval) {
        self.NotifyReferrer(newval);
    });


    self.TreatmentTotal = ko.computed(function () {
        var totalCaseTreatmentPricing = 0;
        var totalBespokePricing = 0;
        ko.utils.arrayForEach(self.CaseTreatmentPricingType(), function (value) {
            totalCaseTreatmentPricing += parseFloat(ko.utils.unwrapObservable(value.ReferrerPrice()));

        });
        ko.utils.arrayForEach(self.CaseBespokeServicePricingType(), function (value) {
            totalBespokePricing += parseFloat(ko.utils.unwrapObservable(value.ReferrerPrice()));
        });

        var result;

        var beforeVat = parseFloat(totalCaseTreatmentPricing) + parseFloat(totalBespokePricing);
        if (self.VatPercentage() != 0 && self.VatPercentage() <= 100 && self.VatPercentage() > 0) {
            result = (beforeVat + ((beforeVat * parseFloat(self.VatPercentage())) / 100));
        }
        else {
            result = beforeVat;
        }
        return result.toFixed(2);
    });

    self.Init = function (data) {


        if (data.CaseTreatmentPricingType.length > 0) {
            ko.mapping.fromJS(data.CaseTreatmentPricingType, mappingOptions, self.CaseTreatmentPricingType);


        }

        if (data.CaseBespokeServicePricingType.length > 0) {
            ko.mapping.fromJS(data.CaseBespokeServicePricingType, mappingOptions, self.CaseBespokeServicePricingType);
        }

        if (data.TreatmentReferrerSupplierPricings.length > 0) {
            ko.mapping.fromJS(data.TreatmentReferrerSupplierPricings, mappingOptions, self.TreatmentReferrerSupplierPricings);
        }
        self.CaseID(data.CaseID);

        if (data.ReferrerAndSupplierVATPricing != null) {
            if (data.ReferrerAndSupplierVATPricing.ReferrerPrice != null) {
                self.VatPercentage(data.ReferrerAndSupplierVATPricing.ReferrerPrice);
            }
            self.ReferrerAndSupplierVATPricing(data.ReferrerAndSupplierVATPricing);

        }

    };

    mappingOptions = {
        'DateOfService': {
            create: function (options) {
                if (options.data != null) {
                    return moment(options.data).format("DD/MM/YYYY");
                }
            }
        }
    };

    this.ExitClick = function () {
        if (confirm("Data will not be saved, are you sure you want to exit?")) {
            window.location.assign("/CaseComplete/index/");
        }
    }

    this.UpdateCaseCompleteInvoicing = function (responseText, statusText, xhr, $form) {
        setTimeout(function () {
            alert(responseText);
            window.location.assign("/CaseComplete/index/");
        }, 2000);
    };

    this.GenerateInvoice = function () {
        $("#frmGenerateInvoice").submit();
    };

    self.selectedValue = function (val) {

        var selectedAssessment = ko.utils.arrayFirst(self.TreatmentReferrerSupplierPricings(), function (item) {
                       return item.PricingTypeName() == val.PricingTypeName();
        });
        this.ReferrerPricingID(selectedAssessment.ReferrerPricingID());
        this.SupplierPriceID(selectedAssessment.SupplierPriceID());
        this.SupplierPrice(selectedAssessment.SupplierPrice());
        this.ReferrerPrice(selectedAssessment.ReferrerPrice());
    };

    self.CaseCompleteFormBeforeSubmit = function (arr, $form, options) {

        var error = false;

        if ($form.jqBootstrapValidation('hasErrors')) {
            return false;
        }
        else {

            var InitailAssessmentCount = 0;
            var FinalAssessmentCount = 0;

            $.each(self.CaseTreatmentPricingType(), function (index, value) {
                var DuplicateDateCount = 0;

                if (value.PricingTypeName() == 'Initial Assessment') {
                    InitailAssessmentCount++;
                }
                if (value.PricingTypeName() == 'Final Assessment') {
                    FinalAssessmentCount++;
                }
                if (InitailAssessmentCount > 1) {
                    alert("Only one Initial Assessment allowed");
                    error = true;
                    return false;
                }
                if (FinalAssessmentCount > 1) {
                    alert("Only one Final Assessment allowed");
                    error = true;
                    return false;
                }
            });
            if (!error)
                $.each(self.CaseTreatmentPricingType(), function (index, value) {

                    var DuplicateDateCount = 0;
                    $.each(self.CaseTreatmentPricingType(), function (index1, existingItem) {
                        if (existingItem.DateOfService == value.DateOfService) {
                            DuplicateDateCount++;
                        }
                        if (DuplicateDateCount > 1) {
                            error = true;
                            return false;
                        }
                    });
                    if (DuplicateDateCount > 1) {
                        alert("Date of service can not be duplicate");
                        error = true;
                        return false;
                    }
                });
        }

        if (error)
            return false;
        return true;
    };


}

       