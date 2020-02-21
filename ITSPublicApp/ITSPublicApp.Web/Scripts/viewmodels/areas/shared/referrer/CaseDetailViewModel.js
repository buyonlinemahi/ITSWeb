
function CaseDetailViewModel() {
    var self = this;
    self.CasePatientTreatment = ko.observable();
    self.CaseAssessmentDetails = ko.observableArray();
    self.IsCustom = ko.observable();
    self.EncryptCaseID = ko.observable();
    self.NoRecordFound = ko.observable('No Record Found');
    self.init = function (model) {
        if (model != null) {
            ko.mapping.fromJS(model, mappingOptions, self);
            ko.mapping.fromJS(model.CasePatientTreatment.IsCustom, {}, self.IsCustom);
            ko.mapping.fromJS(model.CasePatientTreatment.EncryptCaseID, {}, self.EncryptCaseID);
        }

    };
    var mappingOptions = {
        'BirthDate': {
            create: function (options) {
                if (options.data != null) {
                    return moment(options.data).format("DD/MM/YYYY");
                }
            }
        },
        'InjuryDate': {
            create: function (options) {
                if (options.data != null) {
                    return moment(options.data).format("DD/MM/YYYY");
                }
            }
        },
        'AssessmentDate': {
            create: function (options) {
                if (options.data != null) {
                    return moment(options.data).format("DD/MM/YYYY");
                }
            }
        },
        'UploadDate': {
            create: function (options) {
                if (options.data != null) {
                    return moment(options.data).format("DD/MM/YYYY");
                }
            }
        }
    };
    self.CaseAssessmentReportClick = function (item) {
        $("#loader-main-div").removeClass("hidden");
        var actionUrl = null;
        if (item.AssessmentServiceID() > 0) {
            switch (item.AssessmentServiceID()) {
                case 1:
                    $.post("/Referrer/CaseSearch/GetFinalUploadByCaseID/", { caseID: item.EncryptCaseID(), assessmentServiceID: item.EncryptAssessmentServiceID() }, function (res) {
                        if (res != "") {
                            window.location = res;
                        }
                        else {
                            $.post('/PrintPopUp/PrintInitialAssessment/', { Caseid: item.EncryptCaseID(), CaseAssessmentDetailId: item.EncryptCaseAssessmentDetailID() }, function (resp) {
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
                    });
                    break;
                case 2:
                    $.post("/Referrer/CaseSearch/GetFinalUploadByCaseID/", { caseID: item.EncryptCaseID(), assessmentServiceID: item.EncryptAssessmentServiceID() }, function (res) {
                        if (res != "") {
                            window.location = res;
                        }
                        else {
                            $.post('/PrintPopUp/PrintReviewAssessment/', { Caseid: item.EncryptCaseID(), CaseAssessmentDetailId: item.EncryptCaseAssessmentDetailID() }, function (resp) {
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
                    });
                    break;
                case 3:
                    $.post("/Referrer/CaseSearch/GetFinalUploadByCaseID/", { caseID: item.EncryptCaseID(), assessmentServiceID: item.EncryptAssessmentServiceID() }, function (res) {
                        if (res != "") {
                            window.location = res;
                        }
                        else {
                            $.post('/PrintPopUp/PrintFinalAssessment/', { caseID: item.EncryptCaseID(), CaseAssessmentDetailId: item.EncryptCaseAssessmentDetailID() }, function (resp) {
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
                    });
                    break;
            }
        }
        $("#loader-main-div").addClass("hidden");
    };
}