
/*
 Latest version:1.0
 Created by Harpreet singh
 Created on 01/14/2013
 Purpose  To format date string 


*/
var jsonDateRE = /^\/Date\((-?\d+)(\+|-)?(\d+)?\)\/$/;
var parseJsonDateString = function (value) {
   
    var arr = value && jsonDateRE.exec(value);

    if (arr) {
        return new Date(parseInt(arr[1]));
    }

    return value;
};

//Use the Following Syntax to format date 
// <input id="id of element" style="font-size: 12px; color: #000;border:none;background:transparent" readonly="true" data-bind="dateString:your date obserable variable"/>

ko.bindingHandlers.dateString = {

    init: function (element, valueAccessor, allBindingsAccessor, viewModel) {
        try {

            var jsonDate = ko.utils.unwrapObservable(valueAccessor());
            var value = parseJsonDateString(jsonDate);

            var strDate = value.getMonth() + 1 + "/"
                            + value.getDate() + "/"
                            + value.getFullYear();
            element.setAttribute('value', strDate);
        }
        catch (exc) {
        }
        $(element).change(function () {
            var value = valueAccessor();
            value(element.getAttribute('value'));
        });
    },
    update: function (element, valueAccessor, allBindingsAccessor, viewModel) {
        var val = valueAccessor();
        val(element.getAttribute('value'));
    }
};






