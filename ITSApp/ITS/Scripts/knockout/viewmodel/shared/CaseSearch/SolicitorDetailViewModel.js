function SolicitorDetailViewModel() {

    var self = this;
    self.Solicitor = ko.observable();
    self.Init = function (model) {
        self.Solicitor(model);
    };

    $(".phoneMaskformat").mask("99999 999999");
    self.SolicitorUpdateFormBeforeSubmit = function (arr, $form, options) {
        
        if ($form.jqBootstrapValidation('hasErrors'))
            return false;
        return true;
    };
   

};