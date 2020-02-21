
function CaseNote(data) {
    var self = this;
    self.CaseNoteID = ko.observable(data.CaseNoteID);
    self.Note = ko.observable(data.Note);
    self.CaseID = ko.observable(data.CaseID);
    self.UserID = ko.observable(data.UserID);
    self.FirstName = ko.observable(data.FirstName);
    self.LastName = ko.observable(data.LastName);
    self.DateAdded = ko.observable(moment(data.DateAdded).format("DD/MM/YYYY"));
    self.UserName = ko.observable(data.UserName);
    self.WorkflowDefination = ko.observable(data.WorkflowDefination);
    self.EnteredBy = ko.computed(function () {
        return self.FirstName() + ' ' + self.LastName();
    });
}

function CaseNotesGridViewModel() {
    var self = this;
    self.CaseNotes = ko.observableArray();
   
    this.Init = function (model) {
        if (model != null) {
            $.each(model, function (index, value) {
                self.CaseNotes.push(new CaseNote(value));
            });
        }
    };
    this.AddCaseNotes = function (responseText) {
       
        var CaseNote1 = new CaseNote($.parseJSON(responseText));
        self.CaseNotes.push(CaseNote1);
        alert("Added Sucessfully");
    };
}; 