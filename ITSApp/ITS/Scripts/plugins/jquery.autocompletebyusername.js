/* 
     latest Version  1.1
*    Author       : Harpreet Singh,Pardeep kumar
*   Date         : 06-Nov-2012
*   Purpose      : Populate user information in textboxes
*   Version      : 1.1 
*/

(function ($) {
    $.fn.autocompletebyusername = function (settings, callback) {
        //URL should come from the mvc view, this default is just for development purposes.
        var defaultSettings = $.extend({}, settings); //feel free to add more options
        this.autocomplete({
            source: function (request, response) {
                $.ajax({
                    url: defaultSettings.actionUrl,
                    cache: false,
                    type: "POST", 
                    dataType: "json",
                    data: { searchKey: request.term },
                    success: function (data) {
                        response($.map(data, function (item) {
                            return { value: item.UserName, label: item.UserName, UserID : item.UserID };
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
