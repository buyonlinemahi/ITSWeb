
self.NewPatientCount = ko.observable();
self.ExistingPatientsInitialAssessmentCount = ko.observable();
self.ExistingPatientsNextAssessmentCount = ko.observable();
self.AuthorisationCount = ko.observable();
self.StoppedCaseCount = ko.observable();

ko.bindingHandlers.ajaxForm = {
    init: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
        var value = ko.utils.unwrapObservable(valueAccessor());
        $(element).ajaxForm(value);


        $.ajax({
            url: "/Supplier/CaseSearch/GetNotificationBubbleCountBySupplierID",
            type: 'post',
            dataType: 'json',
            success: function (result) {
                self.NewPatientCount(result.NewPatientCount);
                self.ExistingPatientsInitialAssessmentCount(result.ExistingPatientsInitialAssessmentCount);
                self.ExistingPatientsNextAssessmentCount(result.ExistingPatientsNextAssessmentCount);
                self.AuthorisationCount(result.AuthorisationCount);
                self.StoppedCaseCount(result.StoppedCaseCount);

                if (result.NewPatientCount == 0)
                    $("#NewPatientCount").css("opacity", "0");
                if (result.ExistingPatientsInitialAssessmentCount == 0)
                    $("#ExistingPatientsInitialAssessmentCount").css("opacity", "0");
                if (result.ExistingPatientsNextAssessmentCount == 0)
                    $("#ExistingPatientsNextAssessmentCount").css("opacity", "0");
                if (result.AuthorisationCount == 0)
                    $("#AuthorisationCount").css("opacity", "0");
                if (result.StoppedCaseCount == 0)
                    $("#StoppedCaseCount").css("opacity", "0");
            }
        });
    }
};

ko.validation.configure({
    insertMessages: false,
    grouping: { deep: false }
});

function CaseSearch(caseID, fullName, caseNumber, postCode, mobilePhone, EncryptCaseID) {
    var self = this;
    self.CaseID = ko.observable(caseID);
    self.CaseNumber = ko.observable(caseNumber);
    self.PatientFullName = ko.observable(fullName);
    self.PostCode = ko.observable(postCode);
    self.MobilePhone = ko.observable(mobilePhone);
    self.EncryptCaseID = ko.observable(EncryptCaseID);
};

function CaseSearchViewModel() {
    var self = this;

    self.SearchCriteriaID = ko.observable().extend({ required: { message: '\nSearch Criteria is required.' } });

    self.SearchCritarias = [{ SearchByName: 'Patient Name', SearchCategoryID: 1, Selected: true }, { SearchByName: 'Case Number ', SearchCategoryID: 2, Selected: false }];


    self.SearchText = ko.observable().extend({ required: { message: '\nSearch Text is required.' } });
    self.CaseSearchArray = ko.observableArray([]);
    self.NoRecordFound = ko.observable('');


    self.BindResult = function (model) {
        self.CaseSearchArray([]);
        if (model.Cases.length > 0) {
            $.each(model.Cases, function (index, value) {
                self.CaseSearchArray.push(new CaseSearch(value.CaseID, value.FirstName + ' ' + value.LastName, value.CaseNumber, value.PostCode, value.MobilePhone, value.EncryptCaseID));
            });
            self.NoRecordFound('');
            self.TotalItemCount(model.TotalCount);
            self.SearchText(model.CaseSearchCriteria.SearchText);
        }
        else {
            self.CaseSearchArray([]);
            self.NoRecordFound('No Record Found');
            self.TotalItemCount(0);
        }
        $('#divGridDisplaySpinner').spin(false);
    }

    self.errors = ko.validation.group(self);

    self.Skip = ko.observable(0);
    self.Take = ko.observable(10);


    self.AfterSuccessCaseSearch = function (responseText, statusText, xhr, $form) {
        self.BindResult(responseText);
    };

    self.SubmitCaseSearchform = function () {
        if (self.ValidatePage()) {
            self.Pager().CurrentPage(1);
            self.Skip(0);
            self.Take(10);
            $('#divGridDisplaySpinner').spin();
            $('#frmCaseSearch').submit();
        }
    };

    self.GetPagedSearchRecords = function (_skip, _take) {
        if (self.ValidatePage()) {
            self.Skip(_skip);
            self.Take(_take);
            $('#frmCaseSearch').submit();
        }
    };



    self.ValidatePage = function () {
        var errors = "Errors ";
        if (!self.isValid()) {
            $.each(self.errors(), function (index, value) {
                errors = errors + ' ' + value();
            });
            alert(errors);
            return false;
        }
        else {
            return true;
        }
    }

    var pagingSettings = {
        pageSize: 10,
        pageSlide: 2
    };

    // page variables and methods used for pagging
    self.TotalItemCount = ko.observable();
    self.Pager = ko.pager(self.TotalItemCount);
    self.Pager().PageSize(pagingSettings.pageSize);
    self.Pager().PageSlide(pagingSettings.pageSlide);
    self.Pager().CurrentPage(1);
    self.Pager().CurrentPage.subscribe(function () {
        var skip = pagingSettings.pageSize * (self.Pager().CurrentPage() - 1);
        var take = pagingSettings.pageSize;
        self.GetPagedSearchRecords(skip, take);
    });
};