function CaseUploadViewModel(model) {
    var self = this;  
    self.CaseUploadArray = ko.observableArray([]);
    self.DocumentTypes = ko.observableArray();
    self.MedicalReportFileUpload = ko.observable();  

    if (model != null) {
        self.DocumentTypes(model.ReferrerDocumentTypes);
            $.each(model.CaseDocumentUsers, function (index, value) {
                self.CaseUploadArray.push(new CaseUploadModelUpdate(value));
            });
        }

    function CaseUploadModelUpdate(model) {      
            var self = this;
            self.ReferrerDocumentTypeDesc = ko.observable(model.ReferrerDocumentTypeDesc);
            self.DocumentName = ko.observable(model.DocumentName);
            self.DocumentDate = ko.observable(moment(model.UploadDate).format("DD/MM/YYYY"));
            self.CaseID = ko.observable(model.CaseID);
            self.DocumentTypeID = ko.observable(model.DocumentTypeID);
            self.UploadPath = ko.observable(model.UploadPath);
            self.EncryptedCaseID = ko.observable(model.EncryptedCaseID);
        };  


    self.ViewUploads = function (data) {
        var DoctypeID = data.DocumentTypeID();     
        if (DoctypeID == 7 || DoctypeID == 8 || DoctypeID == 9 || DoctypeID == 10) {
            window.location = '/file/ViewCaseUploads?UploadPath=' + data.UploadPath() + '&DocumentName=' + data.DocumentName();
        }
        else {
            window.location = '/Referrer/CaseSearch/ViewCaseUploads?UploadPath=' + data.UploadPath() + '&CaseId=' + data.EncryptedCaseID();
        }
       
    }
    function ClearTheUploadFIle() {
        $('#fileReferrerDocumentUpload').each(function () {
            $(this).after($(this).clone(true)).remove();
        });

        var control = $('#fileReferrerDocumentUpload');
        control.replaceWith(control.val('').clone(true));

        control.on({
            change: function () { console.log("Changed") },
            focus: function () { console.log("Focus") }
        });
    }

    self.SaveReferrerDocument = function () {
        if (self.MedicalReportFileUpload() == undefined || $("#txtUploadDocName").val() == '' || $("#txtDocumentDate").val() == '' || $("#fileReferrerDocumentUpload").val() == '') {
            alert('Please fill all the fields');
            return false;
        }
        else {
           
                var ext = self.MedicalReportFileUpload().split('.').pop().toLowerCase();
                if (ext == "doc" || ext == "docx" || ext == "pdf" || ext == "jpg" || ext == "jpeg") {

                    var iSize = ($("#fileReferrerDocumentUpload")[0].files[0].size / 1024);
                    iSize = (Math.round(iSize * 100) / 100);
                    if (iSize < 10241) {

                        $("#loader-main-div").removeClass("hidden");

                        var formData = new FormData();
                        var fileInput = document.getElementById('fileReferrerDocumentUpload');
                        for (i = 0; i < fileInput.files.length; i++) {
                            formData.append(fileInput.files[i].name, fileInput.files[i]);
                        }
                        $.ajax({
                            type: "POST",
                            url: "/Referrer/AddReferral/AddReferrerDocument?docName=" + $("#txtUploadDocName").val() + "&docDate=" + $("#txtDocumentDate").val() + "&refDocType=" + $("#ddlUploadDocType option:selected").html() + "&refDocTypeID=" + $("#ddlUploadDocType").val() + "&CaseID=" + $("#cid").val(),
                            contentType: false,
                            processData: false,
                            data: formData,
                            success: function (result) {
                                //self.CaseUploadArray.removeAll();
                                result.DocumentDate = (moment(result.DocumentDate).format("DD/MM/YYYY"));
                                self.CaseUploadArray.push(result);
                                document.getElementById('divUploadModal').style.display = "none";
                                $("#divUploadedDocuments").removeClass("hide");
                                $("#tblRefDocForm :input").val('');
                                $("#loader-main-div").addClass("hidden");
                            },
                            error: function () {
                                $("#loader-main-div").addClass("hidden");
                            }
                        });

                        self.ViewUploads = function (result) {
                            window.location = '/Referrer/CaseSearch/ViewCaseUploads?UploadPath=' + result.UploadPath + '&CaseId=' + result.EncryptedCaseID;
                        };


                    }
                    else {
                        alert('Uploaded file exceed the limit of 10 Mb');
                        ClearTheUploadFIle();
                        return false;
                    }
                }
                else {
                    alert("Invalid File Type - Valid file extensions (JPG, JPEG, PDF, DOC, DOCX)");
                    ClearTheUploadFIle();
                    return false;
                }
           
        }
    };
}