/*
*  Latest Version  : 1.0

*  Auther       : Anuj Batra
*  Version      : 1.0
*  Date         : 19-Dec-2012
*  Description  : Create viewModel to implement supplier Complaint setup

*/

function Complaints(supplierComplaintID, complaintTypeID, complaintStatusID, complaintDescription, complaintDate, supplierID, complaintStatusName, complaintTypeName) {

    var self = this;
    self.SupplierComplaintID = ko.observable(supplierComplaintID);
    self.ComplaintTypeID = ko.observable(complaintTypeID);
    self.ComplaintStatusID = ko.observable(complaintStatusID);
    self.ComplaintDescription = ko.observable(complaintDescription);
    self.ComplaintDate = ko.observable(complaintDate);
    self.SupplierID = ko.observable(supplierID);
    self.ComplaintStatusName = ko.observable(complaintStatusName);
    self.ComplaintTypeName = ko.observable(complaintTypeName);

};

function SupplierComplaintViewModel() {
    var self = this;
    self.SupplierComplaintID = ko.observable();
    self.ComplaintTypeID = ko.observable();
    self.ComplaintStatusID = ko.observable();
    self.ComplaintDate = ko.observable();
    self.ComplaintDescription = ko.observable();
    self.SupplierID = ko.observable();
    self.SupplierComplaintsArray = ko.observableArray([]);
    self.ComplaintStatus = ko.observableArray([{ ComplaintStatusID: 1, ComplaintStatusName: "Upheld" }, { ComplaintStatusID: 2, ComplaintStatusName: "Rejected" }, { ComplaintStatusID: 3, ComplaintStatusName: "Investigating"}]);
    self.ComplaintTypes = ko.observableArray([{ ComplaintTypeID: 1, ComplaintTypeName: "Operational" }, { ComplaintTypeID: 2, ComplaintTypeName: "Clinical" }, { ComplaintTypeID: 3, ComplaintTypeName: "Commercial"}]);
    self.IsSaveVisible = ko.observable(true);
    self.IsUpdateVisible = ko.observable(false);

   this.editItem = function (item) {
        var gotDate = "";
        var preparedDate = "";
        gotDate = item.ComplaintDate().split('/');
        preparedDate = gotDate[2] + "-" + gotDate[0] + "-" + gotDate[1];
        self.ComplaintDate(preparedDate);
        self.ComplaintDescription(item.ComplaintDescription());
        self.ComplaintTypeID(item.ComplaintTypeID());
        self.ComplaintStatusID(item.ComplaintStatusID());
        self.SupplierComplaintID(item.SupplierComplaintID());
        self.IsSaveVisible(false);
        self.IsUpdateVisible(true);
        $("#modelPopComplain").dialog("open");
    };


    this.Update = function (item) {
        if (self.checkData()) {
            $.ajax({
                url: "/SupplierSetupComplaint/UpdateSupplierComplaintByComplaindID/",
                cache: false,
                type: "POST", contentType: 'application/json',
                data: ko.toJSON(item),
                success: function (data) {
                    alert(data);
                    self.getAll();

                }
            });
            self.clearModel();
            $("#modelPopComplain").dialog("close");
        }
    };

    this.checkData = function () {
    
        if (self.ComplaintDate() === null || self.ComplaintDate() == '') {
            alert(" Please enter Date ");
            return false;
        }
        else if (self.ComplaintDescription() == null || self.ComplaintDescription() == '') {
            alert("Please enter Description ");
            return false;
        }
        if (self.ComplaintDescription().length > 8000) {
            alert("Description can not be more then 8000 charachters.");
            return false;
        }
        return true;
    };

    this.clearModel = function () {
        self.IsSaveVisible(true);
        self.IsUpdateVisible(false);
        self.ComplaintDate(null);
        self.ComplaintDescription(null);
        self.ComplaintTypeID(1);
        self.ComplaintStatusID(1);
        self.SupplierComplaintID(null);
    };
    this.Cancel = function () {
        self.clearModel();
        $("#modelPopComplain").dialog("close");
    };


    this.Remove = function (item) {

        $.ajax({
            url: "/SupplierSetupComplaint/DeleteSupplierComplaintBySupplierComplaintID/",
            cache: false,
            type: "POST", dataType: "json",
            data: { supplierComplaintID: item.SupplierComplaintID },
            success: function (data) {
                alert(data);
                self.getAll();
            },
            error: function (data) {
                alert("Error while deleting a new Complaint - " + data);
            }
        });
    };
    
    this.AddNew = function (testing) {

        if (self.checkData()) {

            $.ajax({
                url: "/SupplierSetupComplaint/AddSupplierComplaint/",
                cache: false,
                type: "POST", contentType: 'application/json',
                data: ko.toJSON(testing),
                success: function (data) {
                    alert(data);
                    self.getAll();
                },
                error: function (data) {
                    alert("Error while Adding a new Complaint - " + data);
                }
            });

            self.clearModel();
            $("#modelPopComplain").dialog("close");
        }
    };

    this.showModal = function () {

        $("#modelPopComplain").dialog("open");

    };

    this.initializeModel = function (supplierId) {
        self.SupplierID(supplierId);

    };

    this.LoadTab = function () {
        self.getAll();
    };
    this.getAll = function () {
        self.SupplierComplaintsArray.destroyAll();
        $("#divComplaintSpinner").spin(true);
        $.ajax({
            url: "/SupplierSetupComplaint/GetSupplierComplaintsBySupplierID/",
            type: 'post',
            cache: false,
            datatype: 'application/json',
            data: { supplierId: self.SupplierID() },
            success: function (complaints) {

                $.each(complaints, function (index, value) {
                    self.SupplierComplaintsArray.push(new Complaints
                   (value.SupplierComplaintID, value.ComplaintTypeID, value.ComplaintStatusID, value.ComplaintDescription,
                   value.ComplaintDate, value.SupplierID, value.ComplaintStatusName, value.ComplaintTypeName));
                });
                $("#divComplaintSpinner").spin(false);
            },
            error: function () {
                $("#divComplaintSpinner").spin(false);
            }
        });
    }
};