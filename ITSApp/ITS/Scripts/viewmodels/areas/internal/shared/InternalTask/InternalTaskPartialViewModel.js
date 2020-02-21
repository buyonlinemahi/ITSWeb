
function InternalTaskMainScreenViewModel() {
    var animation = new AnimationInternalTask();
    var self = this;

    self.ReferralsCount = ko.observable();
    self.InitialAssessmentQACount = ko.observable();
    self.AuthorisationCount = ko.observable();
    self.ReviewAssessmentQACount = ko.observable();
    self.FinalAssessmentQACount = ko.observable();
    self.TriageAssessmentQACount = ko.observable();
    self.CaseStoppedCount = ko.observable();
    self.ReferralTasksDueTodayCount = ko.observable();
    self.ScreenSelected = ko.observable();

    self.RadioMenuOptions = [{ Option: 'All', ID: 5 },
    { Option: 'All Physical Therapy', ID: 1 }, { Option: 'All Psychological  ', ID: 2 },
    { Option: 'All Diagnostic  ', ID: 3 }, { Option: 'All Special Services  ', ID: 4}];
    self.ScreenDivArray = [{ Name: "ReferralsReceived" }, { Name: "divTriageAssessmentQA" }, { Name: "InitialAssessmentQA" }, { Name: "divAuthorisation" }, { Name: "divReviewAssessmentQA" }, { Name: "divFinalAssessment" }, { Name: "divCaseStopped" }, { Name: "divReferralTaskDue"}];

    self.RadioSelectedTreatmentTypeID = ko.observable();

    this.CallShowGrid = function () {

        if (self.ScreenSelected()) {
            self.ShowGrid(self.ScreenSelected());
        }

    };

    this.RadioSelectedTreatmentTypeID.subscribe(function () {
        $('#divCountGrid').spin();
        var all = 5;
        RadiobuttonSelectedID = self.RadioSelectedTreatmentTypeID();
        if (self.RadioSelectedTreatmentTypeID() == all) {
            self.BindAllTaskCount(function () {
                $('#divCountGrid').spin(false);
            });
        } else {
            self.BindTaskCountTreatmentCategoryID(self.RadioSelectedTreatmentTypeID(), function () {
                $('#divGridDisplaySpinner').spin(false);
                $('#divCountGrid').spin(false);
            });
        }

    });

    this.InitializInternalTaskeScreen = function () {
        animation.displayCaseCountAnimation();
    };

    this.BindAllTaskCount = function (callback) {
        $.ajax({
            url: "/InternalTask/GetCaseCounts",
            cache: false,
            type: "Post",
            dataType: "json",
            success: function (result) {
                self.BindCountValues(result);
                callback(true);
            }
        });
    };

    this.BindTaskCountTreatmentCategoryID = function (treatmentCategoryID, callback) {
        $.ajax({
            url: "/InternalTask/GetCaseCountByTreatmentCategoryID/",
            data: { treatmentCategoryID: treatmentCategoryID },
            cache: false,
            type: "Post",
            dataType: "json",
            success: function (result) {
                self.BindCountValues(result);
                callback(true);
            }
        });
    };

    this.BindCountValues = function (data) {
        $.each(data, function (index, value) {
            var condition = value.Ordinal;
            var count = value.CaseCount;
            switch (condition) {
                case 1:
                    self.ReferralsCount(count);
                    break;
                case 2:
                    self.TriageAssessmentQACount(count);
                    break;
                case 3:
                    self.InitialAssessmentQACount(count);
                    break;
                case 4:
                    self.AuthorisationCount(count);
                    break;
                case 5:
                    self.ReviewAssessmentQACount(count);
                    break;
                case 6:
                    self.FinalAssessmentQACount(count);
                    break;
                case 7:
                    self.CaseStoppedCount(count);
                    break;
                case 8:
                    self.ReferralTasksDueTodayCount(count);
                    break;
               
                default:
                    break;
            }
        });
    };

    self.ShowGrid = function (divName) {
        self.ScreenSelected(divName);
        var partialViewTorender;
        switch (divName) {

            case self.ScreenDivArray[0].Name:
                partialViewTorender = '/ReferralsReceived/Index';
                break;
            case self.ScreenDivArray[1].Name:
                partialViewTorender = '/TriageAssessmentQA/Index';
                break;
            case self.ScreenDivArray[2].Name:
                partialViewTorender = '/InitialAssessmentQA/Index';
                break;
            case self.ScreenDivArray[3].Name:
                partialViewTorender = '/Authorisation/Index';
                break;
            case self.ScreenDivArray[4].Name:
                partialViewTorender = '/ReviewAssessmentQA/Index';
                break;
            case self.ScreenDivArray[5].Name:
                partialViewTorender = '/FinalAssessment/Index';
                break;
            case self.ScreenDivArray[6].Name:
                partialViewTorender = '/CaseStopped/Index';
                break;

            case self.ScreenDivArray[7].Name:
                partialViewTorender = '/ReferralTasksDueToday/Index';
                break;

            
        }

        $('#divGridDisplaySpinner').spin();
        $.ajax({
            url: partialViewTorender,
            dataType: 'html',
            type: "POST",
            success: function (data) {
                self.BindPartialView(divName, data);
            }
        });
    };

    this.BindPartialView = function (divName, data) {
        var viewModel;
        var methodToCall;
        var selectedTreatmentCategoryId = self.RadioSelectedTreatmentTypeID();
        var all = 5;

        switch (divName) {
            case self.ScreenDivArray[0].Name:
                viewModel = new ReferralsReceivedViewModel();
                if (selectedTreatmentCategoryId == all) {
                    methodToCall = viewModel.GetReferralCases(function () {
                        $('#divGridDisplaySpinner').spin(false);
                    });
                } else {
                    methodToCall = viewModel.GetReferralCasesByTreatmentCategoryID(selectedTreatmentCategoryId, function () {
                        $('#divGridDisplaySpinner').spin(false);
                    });
                }
                break;
            case self.ScreenDivArray[1].Name:
                viewModel = new TriageAssessmentViewModel();
                if (selectedTreatmentCategoryId == all) {
                    methodToCall = viewModel.GetTriageAssessmentCases(function () {
                        $('#divGridDisplaySpinner').spin(false);
                    });
                } else {
                    methodToCall = viewModel.GetTriageAssessmentCasesByTreatmentCategoryID(selectedTreatmentCategoryId, function () {
                        $('#divGridDisplaySpinner').spin(false);
                    });
                }
                break;
            case self.ScreenDivArray[2].Name:
                viewModel = new InitialAssessmentViewModel();
                if (selectedTreatmentCategoryId == all) {
                    methodToCall = viewModel.GetAllInitialAssessmentProjects(function () {
                        $('#divGridDisplaySpinner').spin(false);
                    });
                } else {
                    methodToCall = viewModel.GetInitialAssessmentProjectsByTreatmentCategoryID(selectedTreatmentCategoryId, function () {
                        $('#divGridDisplaySpinner').spin(false);
                    });
                }
                break;
            case self.ScreenDivArray[3].Name:
                viewModel = new AuthorisationViewModel();
                if (selectedTreatmentCategoryId == all) {
                    methodToCall = viewModel.GetAuthorisationCases(function () {
                        $('#divGridDisplaySpinner').spin(false);
                    });
                } else {
                    methodToCall = viewModel.GetAuthorisationCasesByTreatmentCategoryID(selectedTreatmentCategoryId, function () {
                        $('#divGridDisplaySpinner').spin(false);
                    });
                }
                break;
            case self.ScreenDivArray[4].Name:
                viewModel = new ReviewAssessmentViewModel();
                if (selectedTreatmentCategoryId == all) {
                    methodToCall = viewModel.GetAllReviewAssessmentProjects(function () {
                        $('#divGridDisplaySpinner').spin(false);
                    });
                } else {
                    methodToCall = viewModel.GetReviewAssessmentProjectsByTreatmentCategoryID(selectedTreatmentCategoryId, function () {
                        $('#divGridDisplaySpinner').spin(false);
                    });
                }
                break;
            case self.ScreenDivArray[5].Name:
                viewModel = new FinalAssessmentQAViewModel();
                if (selectedTreatmentCategoryId == all) {
                    methodToCall = viewModel.GetFinalAssessmentCaseWorkflow(function () {
                        $('#divGridDisplaySpinner').spin(false);
                    });
                } else {
                    methodToCall = viewModel.GetFinalAssessmentCaseWorkflowByTreatmentID(selectedTreatmentCategoryId, function () {
                        $('#divGridDisplaySpinner').spin(false);
                    });
                }
                break;
            case self.ScreenDivArray[6].Name:
                viewModel = new CaseStoppedViewModel();
                if (selectedTreatmentCategoryId == all) {
                    methodToCall = viewModel.GetCaseStoppedCaseWorkflowPatientProjects(function () {
                        $('#divGridDisplaySpinner').spin(false);
                    });
                } else {
                    methodToCall = viewModel.GetCaseStoppedCaseWorkflowPatientProjectsByTreatmentCategoryID(selectedTreatmentCategoryId, function () {
                        $('#divGridDisplaySpinner').spin(false);
                    });
                }
                break;
            case self.ScreenDivArray[7].Name:
                $('#divGridDisplaySpinner').spin(false);
                break;

           
        }



        $('#divGridDisplay').html(data)
            .promise().done(function () {
                animation.displayGridAnimation();
                ko.applyBindings(viewModel, $('#divGridDisplay').get(0));
                methodToCall;
            });
    };

    this.ShowCountGridDisplay = function () {
        animation.displayCaseCountAnimation();
        self.ScreenSelected(null);
    };
};



function AnimationInternalTask() {
    this.displayGridAnimation = function () {
        $('#backDiv').css("display", "block");
        $('#mainDiv').hide('slide', { direction: 'right' }, 1000);
        $('#divGridDisplay').show('slide', { direction: 'left' }, 1000);
    };

    this.displayCaseCountAnimation = function () {
        $('#mainDiv').show('slide', { direction: 'right' }, 1000);
        $('#divGridDisplay').hide('slide', { direction: 'left' }, 1000);
        $('#backDiv').css("display", "none");
    };

    this.hideBackDiv = function () {
        $('#backDiv').css("display", "none");
    };
}

