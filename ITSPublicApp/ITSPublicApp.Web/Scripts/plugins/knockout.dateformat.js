
/*
Latest version:1.0
Created by Robin singh
Created on 05/10/2013
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

//            var strDate = value.getMonth() + 1 + "/"
//                            + value.getDate() + "/"
//                            + value.getFullYear();

            var strDate = moment(value).format('MM/DD/YYYY');

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

ko.extenders.formatDate = function (target, formatString) {
    var result = ko.computed({
        read: function () {
            return target();
        },
        write: function (newValue) {
            var current = target();
            var valueToWrite;
            if (newValue != '') {
                valueToWrite = moment(newValue).format(formatString);
            }
            if (valueToWrite !== current) {
                target(valueToWrite);
            }
            else {
                if (newValue !== current) {
                    target.notifySubscribers(valueToWrite);
                }
            }
        }
    });

    //initialize with current value
    result(target());
    return result;
};

ko.extenders.parseJsonDate = function (target) {
    var result = ko.computed({
        read: target,
        write: function (newValue) {
            var valueToWrite;
            if (target && newValue != null) {
                if (newValue != '') {
                    var formattedDate = moment(newValue).format('DD/MM/YYYY');
                    valueToWrite = formattedDate;
                }
            } else {
                valueToWrite = '';
            }
            target(valueToWrite);
        }
    });
    result(target());
    return result;
};


