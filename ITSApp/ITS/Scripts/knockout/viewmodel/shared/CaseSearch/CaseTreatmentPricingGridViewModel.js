function CaseTreatmentPricingModel(_DateOfService, _PricingTypeName, _ReferrerPrice, _SupplierPrice, _caseID, _CaseTreatmentPricingID, _IsComplete, _ReferrerPricingID, _SupplierPriceID, _AuthorizationStatus, _WasAbandoned, _PatientDidNotAttend) {
    var self = this;
    self.PricingTypeName = ko.observable(_PricingTypeName);
    self.CaseID = ko.observable(_caseID);
    self.CaseTreatmentPricingID = ko.observable(_CaseTreatmentPricingID);
    if (_DateOfService == "" || _DateOfService == null) {
        self.DateOfService = ko.observable(_DateOfService);
    }
    else {
        self.DateOfService = ko.observable(moment(_DateOfService).format("DD/MM/YYYY"));
    }
    self.IsComplete = ko.observable(_IsComplete);
    self.ReferrerPrice1 = ko.observable(_ReferrerPrice);
    self.ReferrerPrice = ko.observable(_ReferrerPrice);
    self.ReferrerPricingID = ko.observable(_ReferrerPricingID);
    self.SupplierPrice1 = ko.observable(_SupplierPrice);
    self.SupplierPrice = ko.observable(_SupplierPrice);
    self.SupplierPriceID = ko.observable(_SupplierPriceID);
    self.PatientDidNotAttend = ko.observable(_PatientDidNotAttend);
    self.WasAbandoned = ko.observable(_WasAbandoned);
    self.AuthorizationStatus = ko.observable(_AuthorizationStatus);
    if (_AuthorizationStatus == 'true') {
        if (_PatientDidNotAttend == 'true') {
            self.AuthorizationStatusText = ko.observable('Did Not Attend');
        }
        else if (_WasAbandoned == 'true') {
            self.AuthorizationStatusText = ko.observable('Not Required');
        }
        else {
            self.AuthorizationStatusText = ko.observable('Treatment Authorized');
        }
    }
    else if (_AuthorizationStatus == 'false') {
        if (_PatientDidNotAttend == 'true') {
            self.AuthorizationStatusText = ko.observable('Did Not Attend');
        }
        else if (_WasAbandoned == 'true') {
            self.AuthorizationStatusText = ko.observable('Not Required');
        }
        else {
            self.AuthorizationStatusText = ko.observable('Treatment Not Authorized');
        }
    }
    else {
        if (_PatientDidNotAttend == 'true') {
            self.AuthorizationStatusText = ko.observable('Did Not Attend');
        }
        else if (_WasAbandoned == 'true' ) {
            self.AuthorizationStatusText = ko.observable('Not Required');
        }
        else {
            self.AuthorizationStatusText = ko.observable('');
        }
    }
};
function CaseTreatmentPricingModelUpdate(_DateOfService, _PricingTypeName, _ReferrerPrice, _SupplierPrice, _caseID, _CaseTreatmentPricingID, _IsComplete, _ReferrerPricingID, _SupplierPriceID, _AuthorizationStatus) {
    var self = this;
    self.PricingTypeName = ko.observable(_PricingTypeName);
    self.CaseID = ko.observable(_caseID);
    self.CaseTreatmentPricingID = ko.observable(_CaseTreatmentPricingID);
    if (_DateOfService == "" || _DateOfService == null) {
        self.DateOfService = ko.observable(_DateOfService);
    }
    else {
        self.DateOfService = ko.observable(_DateOfService);
    }
    self.IsComplete = ko.observable(_IsComplete);
    self.ReferrerPrice1 = ko.observable(_ReferrerPrice);
    self.ReferrerPrice = ko.observable(_ReferrerPrice);
    self.ReferrerPricingID = ko.observable(_ReferrerPricingID);
    self.SupplierPrice1 = ko.observable(_SupplierPrice);
    self.SupplierPrice = ko.observable(_SupplierPrice);
    self.SupplierPriceID = ko.observable(_SupplierPriceID);
    self.AuthorizationStatus = ko.observable(_AuthorizationStatus);
    
        if (_AuthorizationStatus == 'true') {
            self.AuthorizationStatusText = ko.observable('Treatment Authorized');
        }
        else if (_AuthorizationStatus == 'false') {
            self.AuthorizationStatusText = ko.observable('Treatment Not Authorized');
        }
        else { self.AuthorizationStatusText = ko.observable(''); }
    

};

function CaseTreatmentPricingModel1(model) {
    var self = this;
    self.PricingTypeName = ko.observable(model.PricingTypeName);
    self.PricingTypeID = ko.observable(0);
    self.CaseID = ko.observable(model.CaseID);
    self.PatientDidNotAttendDate = ko.observable();
    self.CaseTreatmentPricingID = ko.observable(model.CaseTreatmentPricingID);
    if (model.DateOfService == null) {
        if (model.PatientDidNotAttend == true) {
            self.DateOfService = (moment(model.PatientDidNotAttendDate).format("DD/MM/YYYY"));
            }
        else {
            self.DateOfService = ko.observable(model.DateOfService);
        }
    }
    else {
         if (model.PatientDidNotAttend == true) {
             self.DateOfService = ko.observable(moment(model.PatientDidNotAttendDate).format("DD/MM/YYYY"));
        }
        else {
        self.DateOfService = ko.observable(moment(model.DateOfService).format("DD/MM/YYYY"));
         }
        }
    self.IsComplete = ko.observable(model.IsComplete);
    self.PatientDidNotAttend = ko.observable(model.PatientDidNotAttend);
    self.WasAbandoned = ko.observable(model.WasAbandoned);
    self.ReferrerPrice1 = ko.observable(model.ReferrerPrice);
    self.ReferrerPrice = ko.observable(model.ReferrerPrice);
    self.ReferrerPricingID = ko.observable(model.ReferrerPricingID);
    self.SupplierPrice1 = ko.observable(model.SupplierPrice);
    self.SupplierPrice = ko.observable(model.SupplierPrice);
    self.SupplierPriceID = ko.observable(model.SupplierPriceID);
    if (model.AuthorizationStatus == true) {
                if (model.PatientDidNotAttend == true) {
                    self.AuthorizationStatusText = ko.observable('Did Not Attend');
                }
                else if(model.WasAbandoned == true)
                {
                    self.AuthorizationStatusText = ko.observable('Not Required');
                }
                else {
                    self.AuthorizationStatusText = ko.observable('Treatment Authorized');
                }
    }
    else if (model.AuthorizationStatus == false && model.AuthorizationStatus != null) {
                if (model.PatientDidNotAttend == true) {
                    self.AuthorizationStatusText = ko.observable('Did Not Attend');
                }
                else if (model.WasAbandoned == true) {
                    self.AuthorizationStatusText = ko.observable('Not Required');
                }
                else {
                    self.AuthorizationStatusText = ko.observable('Treatment Not Authorized');
                }
    }
    else {
                if (model.PatientDidNotAttend == true) {
                    self.AuthorizationStatusText = ko.observable('Did Not Attend');
                }
                else if (model.WasAbandoned == true) {
                    self.AuthorizationStatusText = ko.observable('Not Required');
                }
                else {
                    self.AuthorizationStatusText = ko.observable('');
                }
    }
    self.AuthorizationStatus = ko.observable(model.AuthorizationStatus);
    self.AuthorizationStatusDDL = ko.observableArray([]);
};


function CaseTreatmentPricingGridViewModel() {
    var self = this;
    self.CaseTreatmentPricings = ko.observableArray([]);
    self.SelectedReferrerTreatmentPricing = ko.observableArray([]);
    self.AuthorizationStatusDDL = ko.observableArray([
    { name: "Treatment Authorized", value: "true" },
    { name: "Treatment Not Authorized", value: "false" }
    ]);

    self.AccountPayable = ko.observable(0);
    self.AccountReceivable = ko.observable(0);
    self.PricingTypeName = ko.observable();
    self.PricingTypeID = ko.observable(0);
    self.CaseID = ko.observable();
    self.CaseTreatmentPricingID = ko.observable();
    self.DateOfService = ko.observable();
    self.IsComplete = ko.observable();
    self.PatientDidNotAttend = ko.observable();
    self.PatientDidNotAttendDate = ko.observable();
    self.PriceString = ko.observable();
    self.ReferrerPrice1 = ko.observable();
    self.ReferrerPrice = ko.observable();
    self.ReferrerPricingID = ko.observable();
    self.SupplierPrice1 = ko.observable();
    self.SupplierPrice = ko.observable();
    self.SupplierPriceID = ko.observable();
    self.WasAbandoned = ko.observable();
    self.selectedPricingTypeName = ko.observable();
    self.WorkFlowID = ko.observable();
    self._caseId = ko.observable();
    self.AuthorizationStatus = ko.observable();
    self.AuthorizationStatusText = ko.observable();
    var index = 0;
    self.Init = function (data) {
        if (data.CaseTreatmentPricingTypes.length > 0) {
            $.each(data.CaseTreatmentPricingTypes, function (index, value) {
                  self.CaseTreatmentPricings.push(new CaseTreatmentPricingModel1(value));
                self.AccountPayable(parseFloat(self.AccountPayable()) + parseFloat(value.SupplierPrice));
                self.AccountReceivable(parseFloat(self.AccountReceivable()) + parseFloat(value.ReferrerPrice));
            });
            ko.mapping.fromJS(data.TreatmentReferrerSupplierPricing, {}, self.SelectedReferrerTreatmentPricing);
            ko.mapping.fromJS(data.CaseAppointmentDate.WorkflowID, {}, self.WorkFlowID);
            ko.mapping.fromJS(data.CaseAppointmentDate.CaseID, {}, self._caseId);
        }
    };

    self.removeCaseTreatmentPricing = function (item) {
        if (item.CaseTreatmentPricingID() != undefined) {
            var _conf = confirm("Are you sure you want to delete this record");
            if (_conf == true) {
                $.post("/CaseSearch/DeleteCaseTreatmentPricingByCaseTreatmentPricingID", { caseTreatmentPricingID: this.CaseTreatmentPricingID, WorkFlowID: self.WorkFlowID(), caseID: self._caseId() }, function (data) {
                    self.AccountPayable(parseFloat(data.SupplierPriceTotal));
                    self.AccountReceivable(parseFloat(data.ReferrerPriceTotal));
                });
                self.CaseTreatmentPricings.remove(item);
            }

        }

    }

    self.editCaseTreatmentPricing = function (model) {        
        resetControl();
        index = self.CaseTreatmentPricings.indexOf(model);
        self.CaseID(model.CaseID());
        self.CaseTreatmentPricingID(model.CaseTreatmentPricingID());
        self.DateOfService(model.DateOfService());
        self.IsComplete(false);
        self.ReferrerPrice1(model.ReferrerPrice());
        self.ReferrerPrice(model.ReferrerPrice());
        self.ReferrerPricingID(model.ReferrerPricingID());
        self.SupplierPrice1(model.SupplierPrice());
        self.SupplierPrice(model.SupplierPrice());
        self.SupplierPriceID(model.SupplierPriceID());
        self.PricingTypeName(model.PricingTypeName());
        self.selectedPricingTypeName(model.PricingTypeName());
        if (model.AuthorizationStatus() != null) {
            $('#AuthorizationStatus').val(model.AuthorizationStatus().toString());
        }
        else { $('#AuthorizationStatus').val("true"); }
        $('#PricingTypeName').val(model.PricingTypeName());
        $("#divModifyCaseTreatmentGridCaseSearch").modal('show');
    }


    self.closeCaseTreatmentPricing = function () {
        resetControl();
    };

    function resetControl() {
        self.CaseTreatmentPricingID(null);
        $('#PricingTypeName').val('');
        self.DateOfService('');
        self.ReferrerPrice1(null);
        self.SupplierPrice1(null);
        self.ReferrerPrice(null);
        self.SupplierPrice(null);
        self.SupplierPriceID(null);
        self.ReferrerPricingID(null);
        $('#AuthorizationStatus').val('true');
        $('#divCreateCaseTreatmentGridCaseSearch input[type=text],select').removeClass('required_bdr');
    }

    function btnUpdateTreatmentPricingCaseSearch(model) {
        $.ajax({
            url: "/CaseSearch/UpdateCaseSearchTreatmentPricing",
            cache: false,
            type: "POST", dataType: 'json',
            contentType: 'application/json',
            data: ko.toJSON({ caseTreatmentPricingType: model, selectedTreatmentPricing: self.SelectedReferrerTreatmentPricing(), caseID: self._caseId() }),
            success: function (data) {
                self.AccountPayable(parseFloat(data.SupplierPriceTotal));
                self.AccountReceivable(parseFloat(data.ReferrerPriceTotal));
                if (data.IsChanged == 0) {
                    self.CaseTreatmentPricings.push(new CaseTreatmentPricingModel(data.DateOfService, model.PricingTypeName, data.ReferrerPrice1, data.SupplierPrice1, model.CaseID, data.CaseTreatmentPricingID, data.IsComplete, data.ReferrerPricingID, data.SupplierPriceID, model.AuthorizationStatus, model.WasAbandoned, model.PatientDidNotAttend));
                }
            },
            error: function (data) {
                alert("Data Not Found");
            }
        });
    }

    self.AddNewTreatmentCaseSearch = function () {

        if ($("#divCreateCaseTreatmentGridCaseSearch").jqBootstrapValidation('hasErrors')) {

            var _PricingTypeName = $('#PricingTypeName').val();
            var _ReferrerPrice = $('#ReferrerPrice').val();
            var _SupplierPrice = $('#SupplierPrice').val();

            if (_PricingTypeName == "" || _PricingTypeName == "Select Treatment" || _PricingTypeName == null) {
                $('#PricingTypeName').addClass('required_bdr');
            }

            if (_ReferrerPrice == "") {
                $('#ReferrerPrice').addClass('required_bdr');
            }

            if (_SupplierPrice == "") {
                $('#SupplierPrice').addClass('required_bdr');
            }

            return false;
        }
        else {

            var myViewModel = {
                DateOfService: self.DateOfService(),
                PricingTypeName: self.selectedPricingTypeName(),
                ReferrerPrice1: self.ReferrerPrice1(),
                SupplierPrice1: self.SupplierPrice1(),
                CaseID: self.CaseID(),
                CaseTreatmentPricingID: self.CaseTreatmentPricingID(),
                IsComplete: self.IsComplete(),
                ReferrerPricingID: self.ReferrerPricingID(),
                SupplierPriceID: self.SupplierPriceID(),
                AuthorizationStatus: self.AuthorizationStatus()
            };

            btnUpdateTreatmentPricingCaseSearch(myViewModel);
            resetControl();
            myViewModel = null;
            $("#divCreateCaseTreatmentGridCaseSearch").modal('hide');
            return true;
        }

    };


    self.ModifyTreatmentCaseSearch = function () {

        if ($("#divModifyCaseTreatmentGridCaseSearch").jqBootstrapValidation('hasErrors')) {

            var _PricingTypeName = $('#PricingTypeName').val();
            var _ReferrerPrice = $('#ReferrerPrice').val();
            var _SupplierPrice = $('#SupplierPrice').val();

            if (_PricingTypeName == "" || _PricingTypeName == "Select Treatment" || _PricingTypeName == null) {
                $('#PricingTypeName').addClass('required_bdr');
            }

            if (_ReferrerPrice == "") {
                $('#ReferrerPrice').addClass('required_bdr');
            }

            if (_SupplierPrice == "") {
                $('#SupplierPrice').addClass('required_bdr');
            }

            return false;
        }
        else {
            self.CaseTreatmentPricings.splice(index, 1);
            var PricingTypeName = $('#PricingTypeName :selected').val()
            var AuthorizationStatus = $('#AuthorizationStatus :selected').val();
            self.CaseTreatmentPricings.splice(index, 0, new CaseTreatmentPricingModelUpdate(self.DateOfService(), PricingTypeName, self.ReferrerPrice1(), self.SupplierPrice1(), self.CaseID(), self.CaseTreatmentPricingID(), self.IsComplete(), self.ReferrerPricingID(), self.SupplierPriceID(), AuthorizationStatus));
            var dos = self.DateOfService();
            var myViewModel = {
                DateOfService: self.DateOfService(),
                PricingTypeName: PricingTypeName,
                ReferrerPrice1: self.ReferrerPrice1(),
                SupplierPrice1: self.SupplierPrice1(),
                PatientDidNotAttend: self.PatientDidNotAttend(),
                CaseID: self.CaseID(),
                CaseTreatmentPricingID: self.CaseTreatmentPricingID(),
                IsComplete: self.IsComplete(),
                ReferrerPricingID: self.ReferrerPricingID(),
                SupplierPriceID: self.SupplierPriceID(),
                AuthorizationStatus: AuthorizationStatus
            };

            btnUpdateTreatmentPricingCaseSearch(myViewModel);
            myViewModel = null;
            resetControl();
            $("#divModifyCaseTreatmentGridCaseSearch").modal('hide');
            return true;
        }
    };



    mappingOptions = {
        'DateOfService': {
            create: function (options) {
                if (options.data != null) {
                    return moment(options.data).format("DD/MM/YYYY");
                }
            }
        },
        'ReferrerPrice': {
            create: function (options) {
                if (options.data != null) {

                    self.AccountReceivable(parseFloat(self.AccountReceivable()) + parseFloat(options.data));
                    return options.data;
                }
            }
        },
        'SupplierPrice': {
            create: function (options) {
                if (options.data != null) {

                    self.AccountPayable(parseFloat(self.AccountPayable()) + parseFloat(options.data));
                    return options.data;
                }
            }
        }
    };


    $(document).ready(function () {
        $('#PricingTypeName').change(function (model) {
            var assmentID = $('#PricingTypeName :selected').val();
            self.selectedPricingTypeName(assmentID);
        });

        $('#divCreateCaseTreatmentGridCaseSearch input[type=text],select').click(function () {
            $(this).removeClass('required_bdr');
        })

        if (self.WorkFlowID() < 220) {
            $('#_AddCaseTreatementGrid').show();
        }
        else {
            $('#_AddCaseTreatementGrid').hide();
        }
    });
};