function Complaint(data, complaintStatus, complaintType) {
    var self = this;
    self.SupplierComplaintID = ko.observable();

    self.ComplaintDescription = ko.observable();
    self.ComplaintDate = ko.revertableObservable().extend({ formatDate: 'DD/MM/YYYY' });
    self.SupplierID = ko.observable();

    self.ComplaintTypeID = ko.revertableObservable();
    self.ComplaintStatusID = ko.revertableObservable();

    self.ComplaintStatusName = ko.revertableObservable();
    self.ComplaintTypeName = ko.revertableObservable();
   
    self.ComplaintStatuses = ko.observableArray(complaintStatus);
    self.ComplaintTypes = ko.observableArray(complaintType);

    self.ComplaintStatusID.subscribe(function (newVal) {
      
                self.ComplaintStatusName($.grep(self.ComplaintStatuses(),
                        function (e) {
                         return e.ComplaintStatusID == newVal; })[0].ComplaintStatusName);
    });

    self.ComplaintTypeID.subscribe(function (newVal) {
      
        self.ComplaintTypeName($.grep(self.ComplaintTypes(),
            function (e) { return e.ComplaintTypeID == newVal; })[0].ComplaintTypeName);
    });

    ko.mapping.fromJS(data, {}, self);
    ko.CommitChanges(self);

    
    
};

function SupplierComplaintGridViewModel() {

    var self = this;
    self.SupplierComplaintsArray = ko.observableArray([]);
    self.SelectedComplaint = ko.observable();
  
    var complaintStatus = [{ ComplaintStatusID: 1, ComplaintStatusName: "Upheld" }, { ComplaintStatusID: 2, ComplaintStatusName: "Rejected" }, { ComplaintStatusID: 3, ComplaintStatusName:"Investigating" }];
    var complaintType = [{ ComplaintTypeID: 1, ComplaintTypeName: "Operational" }, { ComplaintTypeID: 2, ComplaintTypeName: "Clinical" }, { ComplaintTypeID: 3, ComplaintTypeName: "Commercial" }];

    self.DisableUpdateButton = ko.observable(false);
    self.UpdateComplaintFormBeforeSubmit = function (arr, $form, options) {
        if ($form.jqBootstrapValidation('hasErrors')) return false;
        self.DisableUpdateButton(true);
        return true;
    }

    self.initializeModel = function (complaints) {
        if (complaints != null) {
            $.each(complaints, function (index, value) {
                var complaint = new Complaint(value, complaintStatus, complaintType);
                self.SupplierComplaintsArray.push(complaint);
            });
        }
    };

    self.AddComplaintSuccess = function (responseText, statusText, $form) {
        self.SupplierComplaintsArray.push(new Complaint($.parseJSON(responseText), complaintStatus, complaintType));
        $('#divAddComplaint').modal('hide');
        alert("Added successfully");

    };
    self.UpdateComplaintSuccess = function (item) {
        ko.mapping.fromJS(item, {}, self.SelectedComplaint);
        ko.CommitChanges(self.SelectedComplaint());
        self.DisableUpdateButton(false);
        alert("Updated successfully");
        self.SelectedComplaint(null);
        $('#divViewComplaint').modal('hide');
    };

    self.viewComplaint = function (item) {
        self.SelectedComplaint(item);
    };

    self.closeButtonClick = function (item) {
        ko.RevertChanges(item);
      
        self.SelectedComplaint(null);
        $('#divViewComplaint').modal('hide');
    };

    self.deleteComplaint = function (itemToDelete) {

        var confermation = confirm("Are You Sure to Delete.?");
        if (confermation) {
            $.ajax({
                url: "/SupplierShared/DeleteSupplierComplaint/",
                cache: false,
                type: "POST", dataType: "json",
                data: { supplierComplaintID: itemToDelete.SupplierComplaintID },
                success: function (data) {
                    self.SupplierComplaintsArray.remove(function (item) { return item.SupplierComplaintID == itemToDelete.SupplierComplaintID })
                    alert(data);
                },
                error: function (data) {
                    alert("Error while deleting a new Complaint - " + data);
                }
            });
        }
    };

};

