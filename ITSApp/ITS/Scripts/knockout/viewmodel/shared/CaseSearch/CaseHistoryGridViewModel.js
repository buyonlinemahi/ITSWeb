function CaseHisory(value) {
    var self = this;
    self.CaseID = ko.observable(value.CaseID);
    self.EventDate = ko.observable(moment(value.EventDate).format("DD/MM/YYYY"));    
    self.UserID = ko.observable(value.UserID);
    self.EventDescription = ko.observable(value.EventDescription);
    self.EventTypeID = ko.observable(value.EventTypeID);
    self.TimeCompleted = ko.observable(moment(value.EventDate).format("hh:mm a"));

    self.FirstName = ko.observable(value.FirstName);
    self.LastName = ko.observable(value.LastName);


    self.CompletedBy = ko.computed(function () {
        return self.FirstName() + ' ' + self.LastName();
    });


};

function CaseHistoryGridViewModel() {    
    var self = this;
    self.CaseHistories = ko.observableArray([]);
    self.FirstAppointmentOfferedDate = ko.observable();
    this.Init = function (model, appDate) {
        if (appDate != null) {
            if (appDate.FirstAppointmentOfferedDate != null) {
                self.FirstAppointmentOfferedDate(moment(appDate.FirstAppointmentOfferedDate).format("DD/MM/YYYY"));
            }
        }
        if (model != null) {
            $.each(model, function (index, value) {
                self.CaseHistories.push(new CaseHisory(value));
            });
        }
    };
};