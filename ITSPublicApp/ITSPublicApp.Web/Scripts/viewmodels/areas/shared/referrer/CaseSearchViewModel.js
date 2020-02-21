

ko.bindingHandlers.ajaxForm = {
    init: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
        var value = ko.utils.unwrapObservable(valueAccessor());
        $(element).ajaxForm(value);
    }
};

ko.validation.configure({
    insertMessages: false,
    grouping: { deep: false }
});

function CaseSearch(firstName, lastName, caseID, caseReferrerReferenceNumber, workflowID, status, isFileExists, referralDownloadPath, InitialAssessmentDate, FinalAssessmentDate, InitialCaseAssessmentDetailID, FinalCaseAssessmentDetailID, InitialAssessmentServiceID, FinalAssessmentServiceID, EncryptCaseID, EncryptInitialCaseAssessmentDetailID, EncryptFinalCaseAssessmentDetailID, UserFullName) {
    var self = this;
    self.EncryptCaseID = ko.observable(EncryptCaseID);
    self.EncryptInitialCaseAssessmentDetailID = ko.observable(EncryptInitialCaseAssessmentDetailID);
    self.EncryptFinalCaseAssessmentDetailID = ko.observable(EncryptFinalCaseAssessmentDetailID);
    self.FirstName = ko.observable(firstName);
    self.LastName = ko.observable(lastName);
    self.CaseID = ko.observable(caseID);
    self.PatientFullName = ko.observable();
    self.WorkflowID = ko.observable(workflowID);
    self.Status = ko.observable(status);
    self.IsFileExist = ko.observable(isFileExists);
    self.ReferralDownloadPath = ko.observable(referralDownloadPath);
    self.CaseReferrerReferenceNumber = ko.observable(caseReferrerReferenceNumber);
    self.InitialAssessmentDate = ko.observable(InitialAssessmentDate);
    self.FinalAssessmentDate = ko.observable(FinalAssessmentDate);
    self.InitialCaseAssessmentDetailID = ko.observable(InitialCaseAssessmentDetailID);
    self.FinalCaseAssessmentDetailID = ko.observable(FinalCaseAssessmentDetailID);
    self.InitialAssessmentServiceID = ko.observable(InitialAssessmentServiceID);
    self.FinalAssessmentServiceID = ko.observable(FinalAssessmentServiceID);
    self.UserFullName = ko.observable(UserFullName);
    if (UserFullName != null)
    {
        $(".test").attr("hidden", false);
    }
    self.PatientFullName = ko.computed(function () {
        return self.FirstName() + " " + self.LastName();
    });
};


function CaseSearchViewModel() {

    var self = this;
    $("#ReferrerCaseDiv").show();
    self.GroupName = ko.observable();
    self.GroupArray = ko.observableArray();
    self.GroupArray = ko.observableArray([self.GroupArray(0)]);
    self.selectedGroupArray = ko.observable(0);
    self.SearchText = ko.observable();
    $( document ).ready(function() {         
        if ($('#hdMessageVal').val() == 'Referrer Admin') {                   
            self.SearchCriteriaID = ko.observable().extend({ required: { message: '\nSearch Criteria is required.' } });
            self.SearchCritarias = [{ SearchByName: 'Name', SearchCategoryID: 1 }, { SearchByName: 'Referrer Referernce Number', SearchCategoryID: 2 }, { SearchByName: 'Group Name', SearchCategoryID: 3 }];
            $.getJSON("/Referrer/CaseSearch/GetReferrerGroupsByReferrerID", function (data) {
                self.GroupArray(data.slice());
            });
        }
        else
        {            
            self.SearchCriteriaID = ko.observable().extend({ required: { message: '\nSearch Criteria is required.' } });
            self.SearchCritarias = [{ SearchByName: 'Name', SearchCategoryID: 1 }, { SearchByName: 'Referrer Referernce Number', SearchCategoryID: 2 }];
        }
    });
    
    self.CaseSearchArray = ko.observableArray([]);
    self.GroupUserSearchArray = ko.observableArray([]);
    self.NoRecordFound = ko.observable('');

    self.CaseSearchFormBeforeSubmit = function (arr, $form, options) {       
        if ($("#ddGroups").val() == null)
        {          
            $('#divGridDisplaySpinner').spin(false);
            return false;
        }
    };


    this.showhide = function () {            
        if ($("input[name='CaseSearchCriteria.SearchCriteria']:checked").val() == 3) {
            $(".radio-set").hide();
            $("#ddGroups").attr("hidden", false);            
            $("#lblG").attr("hidden", false);
            $("#ReferrerCaseDiv").hide();
            $("#SearchText").val("");
            self.SearchText = ko.observable();
            self.CaseSearchArray.removeAll();
            self.TotalItemCount(0);
            $("#CaseData").attr("hidden", true);
            $("#GroupData").attr("hidden", false);            
        }
        else
        {
            $("#CaseData").attr("hidden", false);
            $("#GroupData").attr("hidden", true);
            $("#SearchText").val("");
            self.SearchText = ko.observable().extend({ required: { message: '\nSearch Text is required.' } });
            $("#ddGroups").attr("hidden", true);
            $("#lblG").attr("hidden", true);
            $(".radio-set").show();
            $("#ReferrerCaseDiv").show();
            self.CaseSearchArray.removeAll();
            self.TotalItemCount(0);
        }
        return true;
    }
    
    self.BindResult = function (model) {
        self.CaseSearchArray([]);
        if (model.Cases.length > 0) {
            $.each(model.Cases, function (index, value) {
                if (value.InitialAssessmentDate != null)
                    var Intialdate = moment(value.InitialAssessmentDate).format("DD/MM/YYYY");
                if (value.FinalAssessmentDate != null)
                    var Finaldate = moment(value.FinalAssessmentDate).format("DD/MM/YYYY");
                self.CaseSearchArray.push(new CaseSearch(value.FirstName, value.LastName, value.CaseID, value.CaseReferrerReferenceNumber, value.WorkflowID, value.Status, value.IsFileExist, value.ReferralDownloadPath, Intialdate, Finaldate, value.InitialCaseAssessmentDetailID, value.FinalCaseAssessmentDetailID, value.InitialAssessmentServiceID, value.FinalAssessmentServiceID, value.EncryptCaseID, value.EncryptInitialCaseAssessmentDetailID, value.EncryptFinalCaseAssessmentDetailID, value.UserFullName));
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
    self.Take = ko.observable(20);


    self.ReferrerCaseSearchFormSuccess = function (responseText) {
        self.BindResult(responseText);
    };

    self.Submitform = function () {
        if (self.ValidatePage()) {
            self.Pager().CurrentPage(1);
            self.Skip(0);
            self.Take(20);
            $('#divGridDisplaySpinner').spin();
            $('#ReferrerCaseSearchForm').submit();
        }
    };
    self.CaseDetailClick = function (item) {
        $.ajax({
            url: '/Referrer/CaseSearch/CaseDetail',
            type: "post",
            dataType: "json",
            data: { id: item.EncryptCaseID() },
            success: function (result) {
                if (result != null) {
                    caseDetails(result);
                }
            },
            error: function () {
                alert("Error");
            }
        });
    };
   
    self.InitialCaseAssessmentReportClick = function (item) {
        var actionUrl = null;
        if (item.InitialAssessmentServiceID() > 0) {
            if (item.InitialAssessmentServiceID() == 1) {
                actionUrl = '/PrintPopUp/PrintInitialAssessment';
            }
            $.post(actionUrl, { caseID: item.EncryptCaseID(), caseAssessmentDetailID: item.EncryptInitialCaseAssessmentDetailID() }, function (resp) {
                var mywindow;
                if (navigator.appName == 'Microsoft Internet Explorer') {
                    mywindow = window.open('', '', '');
                    mywindow.document.write(resp);
                    mywindow.document.close();
                    mywindow.focus();

                } else {
                    mywindow = window.open('', '', '');
                    mywindow.document.write(resp);
                }
                return false;
            });
        }
    };

    self.FinalCaseAssessmentReportClick = function (item) {
        var actionUrl = null;
        if (item.FinalAssessmentServiceID() > 0) {
            if (item.FinalAssessmentServiceID() == 3) {
                actionUrl = '/PrintPopUp/PrintFinalAssessment';
            }
        }
        $.post(actionUrl, { caseID: item.EncryptCaseID(), caseAssessmentDetailID: item.EncryptFinalCaseAssessmentDetailID() }, function (resp) {
                var mywindow;
                if (navigator.appName == 'Microsoft Internet Explorer') {
                    mywindow = window.open('', '', '');
                    mywindow.document.write(resp);
                    mywindow.document.close();
                    mywindow.focus();

                } else {
                    mywindow = window.open('', '', '');
                    mywindow.document.write(resp);
                }
                return false;
            });
    };

    self.GetPagedSearchRecords = function (_skip, _take) {
        if (self.ValidatePage()) {
            self.Skip(_skip);
            self.Take(_take);
            $('#ReferrerCaseSearchForm').submit();
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
        pageSize: 20,
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



