ko.extenders.parseJsonDate = function (target) {
    target.subscribe(function (newValue) {
        if (newValue !== null) {
            var formattedDate = moment(newValue).format('MM/DD/YYYY');
            target(formattedDate);
        }
    });
    return target;
};

ko.extenders.formatDate = function (target, formatString) {
    var result = ko.computed({
        read: function () {
            return target();
        },
        write: function (newValue) {
            var current = target();
            var valueToWrite = moment(newValue).format(formatString);
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

ko.bindingHandlers.dateString = {
    init: function (element, valueAccessor, allBindingsAccessor, viewModel) {
        try {

            var jsonDate = ko.utils.unwrapObservable(valueAccessor());
            var value = parseJsonDateString(jsonDate);         
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