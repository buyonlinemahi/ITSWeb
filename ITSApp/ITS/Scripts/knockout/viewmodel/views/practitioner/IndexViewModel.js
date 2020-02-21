function PractitionerModel(data) {
    var self = this;
    self.PractitionerFirstName = ko.observable();
    self.PractitionerLastName = ko.observable();
    self.AreaOfExperties = ko.observableArray();
    ko.mapping.fromJS(data, {}, self);
}

function PractitionerGridViewModel() {
    var self = this;
    self.Practitioners = ko.observableArray();
    self.SelectedPractitioner = ko.observableArray();
    self.Init = function (data) {
        this.loadPractitioners(data);
    };
    self.DeletePractitioner = function (item) {
        if (confirm("Are you sure to delete this Practitioner")) {
            var post = AjaxUtil.post('/PractitionerShared/DeletePractitioner', 'json', { practitionerID: item.PractitionerID() });
            post.done(function (resp) {
                if (item.PractitionerID() === resp) {
                    self.Practitioners.remove(function (e) {
                        return e.PractitionerID() == resp;
                    });
                }
            });
        }
    };
    this.loadPractitioners = function (data) {
        $.each(data, function (index, value) {
            self.Practitioners.push(new PractitionerModel(value));
        });
    };

}
function SearchViewModel() {
    var self = this;
}