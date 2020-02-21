/* 
    latest Version  1.0
*   Author       : Pardeep Kumar
*   Date         : 05-Jan-2013
*   Purpose      : Populate Clinical Audit information in textboxes
*   Version      : 1.0 
*/

(function ($) {
    $.fn.autocompleteCaseNumberForClinicalAudit = function (settings, callback) {

        var defaultSettings = $.extend({}, settings); 
        this.autocomplete({
            source: function (request, response) {
                $.ajax({
                    url: defaultSettings.actionUrl,
                    cache: false,
                    type: "POST", 
                    dataType: "json",
                    data: { caseNumber: request.term },
                    success: function (data) {
                        response($.map(data, function (item) {

                           return { value: item.CaseNumber, label: item.CaseNumber, PatientID : item.PatientID,CaseID:item.CaseID,ReferrerID:item.ReferrerID };
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
