/* 
latest Version  1.0
*   Author       : Harpreet Singh
*   Date         : 09-Nov-2012
*   Purpose      : Populate user information in textboxes
*   Version      : 1.0
*/

(function ($) {
    $.fn.autocompletebyReferrername = function (settings) {
        //URL should come from the mvc view, this default is just for development purposes.
        var defaultSettings = $.extend({}, settings); //feel free to add more options
        this.autocomplete({
            source: function (request, response) {
                $('#divSpin').spin(true);
                $.ajax({
                    url: defaultSettings.actionUrl,
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
                            return { value: item.ReferrerName, label: item.ReferrerName, ReferrerID: item.ReferrerID };
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
