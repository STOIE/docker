$(document).ready(function () {

    $.validator.addMethod("folderpath", function (value, element) {
        return this.optional(element) || /^(\\(\\[^\s\\]+)+|([A-Za-z]:(\\)?|[A-z]:(\\[^\s\\]+)+))(\\)?$/.test(value);
    }, "Incorect folder path");

    var jqueryValidationRules = {};

    jqueryValidationRules.UTIL =
    {
        setupFormValidation: function (formToValidate) {
            $(formToValidate).validate({
                rules: {

                },
                messages: {

                },
                showErrors: function (errorMap, errorList) {
                    errorList.forEach(function (error) {
                        $(error.element).css("border", "2px solid #f0a2a5");
                    });
                },
                onfocusout: function (element) {
                    if ($(element).valid()) {
                        $(element).css("border", "none");
                    }
                },
                ignore: [],
                onkeyup: false
            });
        }
    };
});