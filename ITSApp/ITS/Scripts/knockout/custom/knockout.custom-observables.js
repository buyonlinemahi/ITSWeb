ko.revertableObservable = function (initialValue) {
    var prop = ko.observable(initialValue);
    prop.initValue = ko.observable(initialValue);
    prop.commit = function () {
        prop.initValue(prop());
    };
    prop.reset = function () {
        prop(prop.initValue()); //reset to init value
    };
    return prop;
};

ko.revertableObservableArray = function (initialArray) {
    var prop = ko.observableArray(initialArray);
    prop.InitValue = ko.observableArray(initialArray.slice(0));
    prop.commit = function () {
        prop.InitValue(prop());
    };
    prop.reset = function () {
        prop(prop.InitValue());
    };

    return prop;
};

ko.CommitChanges = function (viewModel) {
    for (var key in viewModel) {
        if (viewModel.hasOwnProperty(key) && ko.isObservable(viewModel[key]) && Object.prototype.toString.call(viewModel[key].commit) == '[object Function]') {
            viewModel[key].commit();
        }
    }
};

ko.RevertChanges = function (viewModel) {
    for (var key in viewModel) {
        if (viewModel.hasOwnProperty(key) && ko.isObservable(viewModel[key]) && Object.prototype.toString.call(viewModel[key].reset) == '[object Function]') {
            viewModel[key].reset();
        }
    }
};