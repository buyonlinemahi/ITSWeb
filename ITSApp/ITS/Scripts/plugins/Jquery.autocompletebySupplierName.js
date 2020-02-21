/* 
latest Version  1.0
*   Author       : Harpreet Singh
*   Date         : 18-Dec-2012
*   Purpose      : Auto complete seacrh by Supplier name
*   Version      : 1.0
*/


(function ($) {


    $.fn.autocompletebySuppliername = function (settings) {

        //URL should come from the mvc view, this default is just for development purposes.
        var defaultSettings = $.extend({}, settings); //feel free to add more options

        this.autocomplete({
            source: function (request, response) {
                $('#divSpin').spin(true);
                $.ajax({
                    url: supplierViewModel.ActionUrl(),
                    cache: false,
                    type: "POST",
                    dataType: "json",
                    data: { searchKey: request.term },
                    success: function (data) {
                        if (data.length <= 0) {
                            $('#divSpin').spin(false);
                        }
                        response($.map(data, function (item) {
                            $('#divSpin').spin(false);
                            return { value: item.SupplierName + ' / ' + item.PostCode + ' / ' + item.TreatmentCategoryName, label: item.SupplierName + ' / ' + item.PostCode + ' / ' + item.TreatmentCategoryName, SupplierID: item.SupplierID };
                        }))
                    },
                    error: function () {
                        $('#divSpin').spin(false);
                    }
                });
            },
            minLength: 2,
            select: defaultSettings.selected,
            open: function () {
                $(this).removeClass("ui-corner-all").addClass("ui-corner-top");
            },
            close: function () {
                $(this).removeClass("ui-corner-top").addClass("ui-corner-all");
            }
        })
    };
})(jQuery);
