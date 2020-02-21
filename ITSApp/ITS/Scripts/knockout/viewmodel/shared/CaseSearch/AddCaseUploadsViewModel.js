function AddCaseUploadsViewModel(caseid) {
    var self = this;
    self.CaseID = ko.observable(caseid);

    function ClearTheUploadFIle() {
        $('#DocumentFileUpload').each(function () {
            $(this).after($(this).clone(true)).remove();
        });

        var control = $('#DocumentFileUpload');
        control.replaceWith(control.val('').clone(true));

        control.on({
            change: function () { console.log("Changed") },
            focus: function () { console.log("Focus") }
        });
    }
    self.CaseUploadFormBeforeSubmit = function (arr, $form, options) {
        UploadFileValidations();
        if ($form.jqBootstrapValidation('hasErrors')) {
            return false;
        }
       
    }
   
    self.AddCaseUploadsFormSuccess = function () {
        alert('File Upload Successfully');
        $("#CloseBTN").click();
    }

    function UploadFileValidations() {
        var extention = $("#DocumentFileUpload").val().split('.').pop().toLowerCase();

        if (extention == "jpg" || extention == "jpeg" || extention == "doc" || extention == "docx" || extention == "tiff" || extention == 'pdf' || extention == 'tif') {
            var iSize = ($("#DocumentFileUpload")[0].files[0].size / 1024);
            iSize = (Math.round(iSize * 100) / 100);
            if (iSize < 10241) {

                return true;

            } else {
                alert('Uploaded file exceed the limit of 10 Mb');
                ClearTheUploadFIle();
            }

        }
        else {
            alert("please select pdf,doc,docx,jpg,jpeg,tif or tiff file only");
            ClearTheUploadFIle();
            return false;
        }
    }
};