

function EmailSetupViewModel() {
    var self = this;

    self.ProjectTreatmentEmailSetup = ko.observableArray();

    self.Init = function (data) {
        if (data != null) {
            ko.mapping.fromJS(data, {}, self.ProjectTreatmentEmailSetup);
        }

        var UsertypeID = 0;

        ko.utils.arrayForEach(self.ProjectTreatmentEmailSetup(), function (item) {

            if (UsertypeID == item.UserTypeID()) {
                item.IsTextVisible(false);
            }
            else {
                item.IsTextVisible(true);
                UsertypeID = item.UserTypeID();
            }
        });
    };



    self.UpdateProjectTreatmentEmailSetupFormSuccess = function () {
        alert("Updated Sucessfully");
    };
};