function AddViewModel() {
    var self = this;
    self.SaveButtonDisable = ko.observable(false);
    $(".phoneMaskformat").mask("99999 999999");
    self.SaveValidatedClick = function ($form, event) {
        self.SaveButtonDisable(true);
    };
}