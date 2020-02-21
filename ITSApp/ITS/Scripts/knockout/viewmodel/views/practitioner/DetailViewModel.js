function PractitionerDetailViewModel(model) {
    var self = this;
    self.PractitionerID = ko.observable();
    self.PractitionerFirstName = ko.observable();
    self.PractitionerLastName = ko.observable();
    self.AreasofExpertiseIDs = ko.observableArray();
    self.AreasofExpertise = ko.observableArray();


    self.Init = function (data) {
        $.each(data, function (index, value) {
            self.AreasofExpertise.push(new AreasofExpertise(value, self.AreaofExpertiseIDs()));
        });
    };

    self.DisableUpdateButton = ko.observable(false);

    self.UpdatePractitionerDetailFormBeforeSubmit = function(arr, $form, options) {
        if ($form.jqBootstrapValidation('hasErrors')) {
            return false;   
        }
        self.DisableUpdateButton(true);
        return true;
    };
    
    ko.mapping.fromJS(model, {}, self);

}

function AreasofExpertise(data, selectedAreaofExpertiseIDs) {
    var self = this;
    self.AreasofExpertiseID = ko.observable(data.AreasofExpertiseID);
    self.AreasofExpertiseName = ko.observable(data.AreasofExpertiseName);
    self.IsSelected = ko.computed(function () {
        if ($.grep(selectedAreaofExpertiseIDs,
                    function (e) {
                        return e == data.AreasofExpertiseID;
                    })[0] !== undefined) {
            return true;
        } else {
            return false;
        }
    });
}