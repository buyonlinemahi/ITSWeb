/* 
     latest Version  1.0
*    Author       : Harpreet Singh
*   Date         : 31-Dec-2012
*   Purpose      : Populate Practitioner information in textboxes
*   Version      : 1.0 
*/

(function ($) {
    $.fn.autocompleteByPractitionerName = function (settings, callback) {
   
        //URL should come from the mvc view, this default is just for development purposes.
        var defaultSettings = $.extend({}, settings); //feel free to add more options
        this.autocomplete({
            source: function (request, response) {
                $.ajax({
                    url: practitionerViewModel.SeacrhUrl(),
                    cache: false,
                    type: "POST", 
                    dataType: "json",
                    data: { searchKey: request.term,supplierID:practitionerViewModel.SupplierID },
                    success: function (data) {
                        response($.map(data, function (item) {

                             practitionerViewModel.UpdateGrid(data);
                           //return { value: item.PractitionerName, label: item.PractitionerName, PractitionerID : item.PractitionerID };
                        }))
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
            },
        });
    };
})(jQuery);
