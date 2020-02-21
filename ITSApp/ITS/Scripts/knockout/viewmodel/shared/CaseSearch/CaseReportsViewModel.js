function CaseReport(value) {
    var self = this;
    self.CaseID = ko.observable(value.CaseID);
    self.DocumentName = ko.observable(value.DocumentName);
    self.EventDate = ko.observable(moment(value.EventDate).format("DD/MM/YYYY"));
    self.EventDescription = ko.observable(value.EventDescription);
    self.FirstName = ko.observable(value.FirstName);
    self.LastName = ko.observable(value.LastName);
    self.CaseAssessmentDetailID= ko.observable(value.CaseAssessmentDetailID);
    self.AssessmentServiceID=ko.observable(value.AssessmentServiceID);
    self.CompletedBy = ko.computed(function () {
        return self.FirstName() + ' ' + self.LastName();
    });


};

function CaseReportsGridViewModel() {
    var self = this;
    self.CaseReports = ko.observableArray([]);
    self.EventDescription = ko.observable();
    self.IsNotifyEmailtoReferrer = ko.observable(false);
    self.IsInvoiceSent = ko.observable(false);
    self.Flagisshow = ko.observable(false);
    this.Init = function (model, model1) {
        if (model != null) {
              $.each(model, function (index, value) {
                self.CaseReports.push(new CaseReport(value));
                if (value.EventDescription == 'Invoice Sent') {
                    self.IsInvoiceSent(true);
                }
            });
        }
        if (model1 != null) {
            self.Flagisshow(true)
            
        }
    };
    //self.ViewReport = function (item) {
    //    if (item.EventDescription() == "Final Assessment Report Submitted to Innovate" || item.EventDescription() == "Final Assessment Report Submitted to Referrer") {
    //        var actionUrl = null;
    //        if (item.AssessmentServiceID() > 0) {
    //            if (item.EventDescription() == "Final Assessment Report Submitted to Innovate") {
    //                actionUrl = '/PrintPopUp/PrintFinalAssessmentQA';
    //            }
    //            else {
    //                actionUrl = '/PrintPopUp/PrintFinalAssessment';
    //            }

    //        }
    //        $.post(actionUrl, { Caseid: item.CaseID(), CaseAssessmentDetailId: item.CaseAssessmentDetailID() }, function (resp) {
    //            var mywindow;
    //            if (navigator.appName == 'Microsoft Internet Explorer') {
    //                mywindow = window.open('', '', '');
    //                mywindow.document.write(resp);
    //                mywindow.document.close();
    //                mywindow.focus();

    //            } else {
    //                mywindow = window.open('', '', '');
    //                mywindow.document.write(resp);
    //            }
    //            return false;
    //        });
    //    }
    //    else {
    //        self.EventDescription(item.EventDescription());
    //        $("#frmAssessmentDownloadReport").submit();
    //    }
    //};

    self.ViewReport = function (item) {

            self.EventDescription(item.EventDescription());
            $("#frmAssessmentDownloadReport").submit();
      
    };


    self.InvoiceEmailSend = function () {
        if ($("#inputEmailSentTo").val() != '') {
            if (isValidEmailAddress($("#inputEmailSentTo").val())) {
                self.EventDescription("InvoiceEmailSend");
                return true;
            }
            else {
                alert('email address is incorrect.');
                $("#inputEmailSentTo").focus();
                return false;
            }
        }
        else {
            alert('Sent to email address is required.');
            $("#inputEmailSentTo").focus();
            return false;
        }

    }

    function isValidEmailAddress(emailAddress) {
        var pattern = new RegExp(/^[+a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/i);
        return pattern.test(emailAddress);
    };

};