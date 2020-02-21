
function GroupDetailViewModel() {
    
    var self = this;
    var Name;
    self.ReferrerGroupDetail = ko.observableArray();  
    self.GroupName = ko.observable();
    self.GroupID = ko.observable();
    self.ReferrerID = ko.observable();    
    self.Name = ko.observable();
    self.UserID = ko.observable();
    self.UserName = ko.observable();

    self.Init = function (model) {
        if (model != null) {
            $.each(model, function (index, value) {
                self.ReferrerGroupDetail.push(new ReferrerGroup(value));                
            });
        }       
    };

    function ReferrerGroup(data) {
        var self = this;       
        self.GroupName = ko.observable(data.GroupName);
        self.ReferrerID = ko.observable(data.ReferrerID);
        self.ReferrerData = ko.mapping.fromJS([]);
        self.ReferrerUserDetail = ko.observableArray();
        if (data.ReferrerData.length > 0) {
            ko.mapping.fromJS(data.ReferrerData, {}, self.ReferrerUserDetail);                            
        }        
       

        ko.CommitChanges(self);
    };
  
    
    $("#btnGName").click(function () {
        Name = $("#objName").val();     
        $("#divAddReferrerGroup").modal("hide");       
        $("#multiDropDown").show();
        $("#SaveButton").show();
    });

    self.DeleteUser = function (item) {
        if (confirm("Are you sure to delete this User")) {
            var post = AjaxUtil.post('/Referrer/DeleteGroupUser', 'json', { groupIID: item.GroupID() });
            post.done(function (resp) {
                if (item.GroupID() === resp) {
                    self.ReferrerUserDetail.remove(function (e) {
                        return e.GroupID() == resp;
                    });
                }
            });
        }
    };
   
};

self.UserIDArray = ko.observableArray();
self.UpdateCheck = ko.observable();


self.viewGroupName = function (item, event)
{
  
    $('option', $('#drpUserID')).each(function (element) {
    $(this).prop('selected', false);
    });
    $("#drpUserID").multiselect("refresh");

    var context = ko.contextFor(event.target);
    var index = context.$index();
    var value = $("#Name_" + index).val();
    $.ajax({
        url: "/Referrer/GetUserDetail",
        type: 'post',
        dataType: 'json',
        contentType: 'application/json',
        data: JSON.stringify({ _id: $("#objReferrerID").val(), _name: value }),
        cache: false,
        success: function (_model) {
            var IDArray = [];
            for (var i = 0; i < _model.ReferrerDetailData.length; i++) {
                var _Id = _model.ReferrerDetailData[i].UserID;
                IDArray.push(_Id);
            }
            i = 0, size = IDArray.length;           
            for (i; i < size; i++) {
                $("option[value='" + IDArray[i] + "']", $('#drpUserID')).prop('selected', true);
                $("#drpUserID").multiselect("refresh");

            }            
        }
    });
    $("#hdcheck").val(1);
    $("#objName").hide();
    $("#objnewName").show();
    $("#hdName").val(value);
    $("#objnewName").val(value);    
    $("#divSaveGroupName").hide();    
    $("#multiDropDown").show();
    $("#divAddReferrerGroup").modal("show");
 
}






