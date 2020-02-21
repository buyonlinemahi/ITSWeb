function DocumentSetupViewModel() {
    var self = this;
    self.ProjectTreatmentDocumentSetupResult = ko.observableArray();

    self.Init = function (model) {
        if (model != null) {
            BindData(model);
        }
    }
    function BindData(model) {
        ko.mapping.fromJS(model, {}, self.ProjectTreatmentDocumentSetupResult);
        var isCheckedInitial = self.ProjectTreatmentDocumentSetupResult()[0].DocumentSetupTypeID();
        var isCheckedReturn = self.ProjectTreatmentDocumentSetupResult()[1].DocumentSetupTypeID();
        var isCheckedFinal = self.ProjectTreatmentDocumentSetupResult()[2].DocumentSetupTypeID();
        if (isCheckedInitial == 2) {
            $("#0divAssessment").css({ display: "block" });
            $('#0AssesmentFile').attr("required", "required");
            $("#0download").attr("href", self.ProjectTreatmentDocumentSetupResult()[0].UploadedFilePath());
        }
        if (isCheckedReturn == 2) {
            $("#1divAssessment").css({ display: "block" });
            $('#1AssesmentFile').attr("required", "required");
            $("#1download").attr("href", self.ProjectTreatmentDocumentSetupResult()[1].UploadedFilePath());
        }
        if (isCheckedFinal == 2) {
            $("#2divAssessment").css({ display: "block" });
            $('#2AssesmentFile').attr("required", "required");
            $("#2download").attr("href", self.ProjectTreatmentDocumentSetupResult()[2].UploadedFilePath());
        }
    }

    $(document).ready(function () {
        $('#0AssesmentFile').click(function () {
            $('#0AssesmentFile').removeClass('required_bdr');
        });
        $('#1AssesmentFile').click(function () {
            $('#1AssesmentFile').removeClass('required_bdr');
        });
        $('#2AssesmentFile').click(function () {
            $('#2AssesmentFile').removeClass('required_bdr');
        });

        function NofileUpload() {
            alert("File not upload.");
            return false;
        }

        $('input[type="radio"]').click(function () {

            if ($(this).attr('id') == "0Default") {
                $("#0divAssessment").css({ display: "none" });
                $("#1divAssessment").css({ display: "none" });
                $("#2divAssessment").css({ display: "none" });
                $('#0AssesmentFile').removeAttr("required", "required");
                $('#1AssesmentFile').removeAttr("required", "required");
                $('#2AssesmentFile').removeAttr("required", "required");

                self.ProjectTreatmentDocumentSetupResult()[1].DocumentSetupTypeID('1');
                self.ProjectTreatmentDocumentSetupResult()[2].DocumentSetupTypeID('1');
            }

            if ($(this).attr('id') == "0Custom") {
                $("#0divAssessment").css({ display: "block" });
                $('#0AssesmentFile').attr("required", "required");
                $("#1divAssessment").css({ display: "block" });
                $('#1AssesmentFile').attr("required", "required");
                $("#2divAssessment").css({ display: "block" });
                $('#2AssesmentFile').attr("required", "required");
                self.ProjectTreatmentDocumentSetupResult()[1].DocumentSetupTypeID('2');
                self.ProjectTreatmentDocumentSetupResult()[2].DocumentSetupTypeID('2');

            }

            if ($(this).attr('id') == "1Default") {
                $("#0divAssessment").css({ display: "none" });
                $("#1divAssessment").css({ display: "none" });
                $("#2divAssessment").css({ display: "none" });
                $('#0AssesmentFile').removeAttr("required", "required");
                $('#1AssesmentFile').removeAttr("required", "required");
                $('#2AssesmentFile').removeAttr("required", "required");
                self.ProjectTreatmentDocumentSetupResult()[0].DocumentSetupTypeID('1');
                self.ProjectTreatmentDocumentSetupResult()[2].DocumentSetupTypeID('1');
            }

            if ($(this).attr('id') == "1Custom") {
                $("#0divAssessment").css({ display: "block" });
                $('#0AssesmentFile').attr("required", "required");
                $("#1divAssessment").css({ display: "block" });
                $('#1AssesmentFile').attr("required", "required");
                $("#2divAssessment").css({ display: "block" });
                $('#2AssesmentFile').attr("required", "required");
                self.ProjectTreatmentDocumentSetupResult()[0].DocumentSetupTypeID('2');
                self.ProjectTreatmentDocumentSetupResult()[2].DocumentSetupTypeID('2');
            }

            if ($(this).attr('id') == "2Default") {
                $("#0divAssessment").css({ display: "none" });
                $("#1divAssessment").css({ display: "none" });
                $("#2divAssessment").css({ display: "none" });
                $('#0AssesmentFile').removeAttr("required", "required");
                $('#1AssesmentFile').removeAttr("required", "required");
                $('#2AssesmentFile').removeAttr("required", "required");
                self.ProjectTreatmentDocumentSetupResult()[1].DocumentSetupTypeID('1');
                self.ProjectTreatmentDocumentSetupResult()[0].DocumentSetupTypeID('1');
            }

            if ($(this).attr('id') == "2Custom") {
                $("#0divAssessment").css({ display: "block" });
                $('#0AssesmentFile').attr("required", "required");
                $("#1divAssessment").css({ display: "block" });
                $('#1AssesmentFile').attr("required", "required");
                $("#2divAssessment").css({ display: "block" });
                $('#2AssesmentFile').attr("required", "required");
                self.ProjectTreatmentDocumentSetupResult()[1].DocumentSetupTypeID('2');
                self.ProjectTreatmentDocumentSetupResult()[0].DocumentSetupTypeID('2');
            }

        });
    });

    this.UpdateDocumentSetupFormBeforeSubmit = function (arr, $form, options) {
        var file = $("#0AssesmentFile").val();
        var file1 = $("#1AssesmentFile").val();
        var file2 = $("#2AssesmentFile").val();
        if ($('#0Custom').val() == "2" && $("#0Custom").prop("checked")) {
            if ($("#0AssesmentFile").val() == "")
                $('#0AssesmentFile').addClass('required_bdr');
            if ($("#1AssesmentFile").val() == "")
                $('#1AssesmentFile').addClass('required_bdr');
            if ($("#2AssesmentFile").val() == "")
                $('#2AssesmentFile').addClass('required_bdr');

            if ($("#0AssesmentFile").val() == "") {
                return false;
            }
            else {
                var ext = file.substr((file.lastIndexOf('.') + 1));
                ext = ext.toLowerCase();
                if (ext == "doc" || ext == "docx" || ext == 'pdf' || ext == 'tif' || ext == 'tiff') {
                } else {
                    alert('Invalid file type in Upload Document. Only PDF,DOC,DOCX,TIF,TIFF is accepted.');
                    return false;
                }
            }

            if ($("#1AssesmentFile").val() == "") {
                return false;
            }
            else {
                var ext1 = file1.substr((file1.lastIndexOf('.') + 1));
                ext1 = ext1.toLowerCase();
                if (ext1 == "doc" || ext1 == "docx" || ext1 == 'pdf' || ext == 'tif' || ext == 'tiff') {

                } else {
                    alert('Invalid file type in Upload Document. Only PDF,DOC,DOCX,TIF,TIFF is accepted.');
                    return false;
                }
            }

            if ($("#2AssesmentFile").val() == "") {
                return false;
            }
            else {
                var ext2 = file2.substr((file2.lastIndexOf('.') + 1));
                ext2 = ext2.toLowerCase();
                if (ext2 == "doc" || ext2 == "docx" || ext2 == 'pdf' || ext == 'tif' || ext == 'tiff') {

                } else {

                    alert('Invalid file type in Upload Document. Only PDF,DOC,DOCX,TIF,TIFF is accepted.');
                    return false;
                }
                return true;
            }
        }
        return true;
    };

    this.UpdateDocumentSetupFormSuccess = function (model) {

        alert("Updated Sucessfully");
        $("#0AssesmentFile").val('');
        $("#1AssesmentFile").val('');
        $("#2AssesmentFile").val('');
        BindData($.parseJSON(model));
        //var prj_treatmentid = self.ProjectTreatmentDocumentSetupResult()[1].ReferrerProjectTreatmentID();
        //window.location('/Referrer/ProjectTreatment/' + prj_treatmentid);
    } 
}