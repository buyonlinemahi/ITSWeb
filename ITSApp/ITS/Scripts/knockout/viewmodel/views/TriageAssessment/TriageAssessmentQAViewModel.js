function TriageAssessmentQAViewModel() {
    var self = this;

    self.Init = function (data) {
        if (data != null) {
            ko.mapping.fromJS(data.CasePatientTreatment, {}, self);
        }
    };

    function ClearTheUploadFIle() {
        $('#TriageAssessmentUploadFile').each(function () {
            $(this).after($(this).clone(true)).remove();
        });

        var control = $('#TriageAssessmentUploadFile');
        control.replaceWith(control.val('').clone(true));

        control.on({
            change: function () { console.log("Changed") },
            focus: function () { console.log("Focus") }
        });
    }

    $('#TriageAssessmentUploadFile').change(function () {
        var ext = $('#TriageAssessmentUploadFile').val().split('.').pop().toLowerCase();
        if ($.inArray(ext, ['pdf', 'PDF', 'doc', 'DOC', 'docx', 'DOCX', 'png', 'PNG', 'jpeg', 'JPEG']) == -1) {
            alert('Upload pdf,doc,png,jpeg file only');
            ClearTheUploadFIle()
        }
        var iSize = ($("#TriageAssessmentUploadFile")[0].files[0].size / 1024);
        iSize = (Math.round(iSize * 100) / 100);
        if (iSize < 10241) {
          
        }
        else {
            alert('Uploaded file exceed the limit of 10 Mb');
            ClearTheUploadFIle();
        }
    });
    self.UpdateTriageAssesmentQAFormBeforeSubmit = function (arr, $form, options) {
        if ($form.jqBootstrapValidation('hasErrors')) return false;
        return true;
    };

    self.UpdateTriageAssessmentQA = function (responseText, statusText, xhr, $form) {
      alert($.parseJSON(responseText));
        window.location = '/TriageAssessment/Index';
    };
};