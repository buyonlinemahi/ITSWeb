

function AddSupplierComplaintViewModel(SupplierID) {

    var self = this;
    self.ComplaintTypeID = ko.observable();
    self.ComplaintStatusID = ko.observable();
    self.SupplierID = ko.observable(SupplierID);
    self.ComplaintStatusName = ko.observable();
    self.ComplaintTypeName = ko.observable();
    self.ComplaintDate = ko.observable();

    self.ComplaintStatus = ko.observableArray([{ ComplaintStatusID: 1, ComplaintStatusName: "Upheld" }, { ComplaintStatusID: 2, ComplaintStatusName: "Rejected" }, { ComplaintStatusID: 3, ComplaintStatusName: "Investigating"}]);
    self.ComplaintTypes = ko.observableArray([{ ComplaintTypeID: 1, ComplaintTypeName: "Operational" }, { ComplaintTypeID: 2, ComplaintTypeName: "Clinical" }, { ComplaintTypeID: 3, ComplaintTypeName: "Commercial"}]);

    self.ComplaintStatusID.subscribe(function (newVal) {
        self.ComplaintStatusName($.grep(self.ComplaintStatus(),
                        function (e) { return e.ComplaintStatusID == newVal; })[0].ComplaintStatusName);
    });

    self.ComplaintTypeID.subscribe(function (newVal) {
        self.ComplaintTypeName($.grep(self.ComplaintTypes(),
            function (e) { return e.ComplaintTypeID == newVal; })[0].ComplaintTypeName);
    });

    self.ComplaintFormBeforeSubmit = function (arr, $form, options) {
        if ($form.jqBootstrapValidation('hasErrors')) {
            return false;
        }
        else {
            self.DisableAddButton(true);
            return true;
        }
    }

    self.DisableAddButton = ko.observable(false);

    this.ResetDisabledAddButton = function () {
        self.DisableAddButton(false);
    };
};