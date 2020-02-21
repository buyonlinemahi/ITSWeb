/* 
   latest Version  1.0
*   Author       : Robin Singh
*   Date         : 07-March-2013
*   Purpose      : Populate Practitioners Setup information in textboxes
*   Version      : 1.0 
*/

(function ($) {
    $.fn.autocompletePractitionerSetupByName = function (settings, callback) {
   
        //URL should come from the mvc view, this default is just for development purposes.
        var defaultSettings = $.extend({}, settings); //feel free to add more options
        this.autocomplete({
            source: function (request, response) {
                $.ajax({
                    url: practitionerSetupViewModel.SeacrhUrl(),
                    cache: false,
                    type: "POST", 
                    dataType: "json",
                    data: { searchKey: request.term },
                    success: function (data) {
                        response($.map(data, function (item) {

                           return { value: item.PractitionerFirstName + ' ' +  item.PractitionerLastName , label: item.PractitionerFirstName + ' ' + item.PractitionerLastName, PractitionerID : item.PractitionerID };
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
