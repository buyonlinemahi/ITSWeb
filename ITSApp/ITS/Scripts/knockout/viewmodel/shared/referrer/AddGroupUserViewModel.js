function AddGroupUserViewModel(ReferrerID) {
    var self = this;

    self.UsersBindingByReferrerID = ko.observableArray([]);
    self.UserID = ko.observable();
    self.UserName = ko.observable();
    self.SelectedTreatmentCategory = ko.observable();
    self.GroupName = ko.observable();
    self.ReferrerID = ko.observable();
    self.MultipleUserID = ko.observable();
    self.EncryptedReferrerID = ko.observable();
    self.NewName = ko.observable();
    self.UpdateCheck = ko.observable();
    $("#objnewName").hide();

    self.Save = function () {
        var Check = $('#hdcheck').val();
        if (Check == 1) {
            if ($('#objnewName').val() == "") {
                alert("Please enter group Name.")
                return false;
            }
            else if ($('#drpUserID').val() == null) {
                alert("Please Select Aleast one user.")
                return false;
            }
            else {
                var _saveData =
               {
                   GroupName: $('#hdName').val(),
                   ReferrerID: ReferrerID,
                   NewName: $("#objnewName").val(),
                   MultipleUserID: $('#drpUserID').val(),
                   UpdateCheck: Check
               }
            }
        }
        else {
            if ($('#objName').val() == "") {
                alert("Please enter group Name.")
                return false;
            }
            else if ($('#drpUserID').val() == null) {
                alert("Please Select Aleast one user.")
                return false;
            }
            else {
                var _saveData = {
                    GroupName: $('#objName').val(),
                    MultipleUserID: $('#drpUserID').val(),
                    ReferrerID: ReferrerID
                }
            }
        }
        $.ajax({
            url: "/Referrer/AddGroup",
            type: 'post',
            dataType: 'json',
            contentType: 'application/json',
            data: JSON.stringify({ _referrerGroup: _saveData }),
            cache: false,
            success: function (_model) {
                if (_model.Msg == "Group Added" || _model.Msg == "Group update SuccessFully" || _model.Msg == "Group Name Already Exicted") {
                    alert(_model.Msg);
                    $('#hdcheck').val("");
                    $("#divAddReferrerGroup").modal("hide");
                    window.location = '/Referrer/Detail?referrerID=' + _model.EncryptedReferrerID;
                }
                else {
                    alert(_model);
                }
            }
        });
    }

    $(document).ready(function () {
        GetReferrerUsersByID(ReferrerID);
    });
    //============User Drop Down Binding==============//

    function GetReferrerUsersByID(val) {

        var _Id = val;
        $.ajax({
            url: "/Referrer/GetReferrerUsersByID",
            cache: false,
            type: "POST", dataType: 'json',
            contentType: 'application/json',
            data: JSON.stringify({ _referrerID: _Id }),
            success: function (model) {
                self.UsersBindingByReferrerID.removeAll();
                $.each(model, function (index, value) {
                    self.UsersBindingByReferrerID.push(new UserInsertByID(value));
                });
                $('#drpUserID').multiselect('destroy');
                $('#drpUserID').multiselect('refresh');

            },
            error: function (data) {
                alert("Error while updating a item - " + data);
            }
        });
    }

    function UserInsertByID(value) {
        var self = this;
        self.UserID = ko.observable(value.UserID);
        self.UserName = ko.observable(value.FirstName + value.LastName);
    }
    //=================================================//



};
