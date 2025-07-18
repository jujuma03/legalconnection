var login = function () {
    var form = {
        object: $("#login_form"),
        validate: function () {
            form.object.validate({
                highlight: function (element, errorClass, validClass) {
                    $(element).addClass("input-error-validate");
                },
                unhighlight: function (element, errorClass, validClass) {
                    $(element).removeClass("input-error-validate");
                },
                rules: {
                    UserName: "required",
                    Password : "required"
                },
                messages: {
                    UserName: {
                        required : "Es necesario ingresar el correo."
                    },
                    Password: {
                        required : "Es necesario ingresar la contraseña."
                    }
                },
                errorClass: 'form-control-feedback label-error-validate',
                submitHandler: function (formElement, e) {
                    var $btn = $("#login_form").find("button[type='submit']");
                    $btn.addClass('m-loader m-loader--right m-loader--light').attr('disabled', true);
                    var formData = new FormData(formElement);
                    e.preventDefault();
                    $.ajax({
                        url: $(formElement).attr("action"),
                        type: "POST",
                        data: formData,
                        contentType: false,
                        processData: false
                    })
                        .done(function (e) {
                            events.errorMessage.hide();
                            window.location.href = e;
                        })
                        .fail(function (e) {
                            $(formElement).find("[name='Password']").val("");
                            $btn.removeClass('m-loader m-loader--right m-loader--light').attr('disabled', false);
                            events.errorMessage.show(e.responseText);
                        });
                }
            });
        },
        init: function () {
            this.validate();
            $('textarea').keyup(function (e) {
                if (e.keyCode == 13) {
                    $("#login_form").submit();
                }
            });
        }
    };

    var events = {
        errorMessage: {
            show: function (message) {
                $("#response_login").text(message);
                $("#response_login").removeClass("d-none");
            },
            hide: function () {
                $("#response_login").text("");
                $("#response_login").addClass("d-none");
            }
        },
        onAsClient: function () {
            $("#asclient").click(function () {
                $(this).addClass("bg-dark");
                $(this).addClass("text-white");

                $("#aslawyer").removeClass("bg-dark");
                $("#aslawyer").removeClass("text-white");
                $("#aslawyer").addClass("btn-secondary");
                $("#external_div_logins").removeClass("d-none");
                $("#register_link").attr("href","/registrar");
            });
        },
        onAsLawyer: function () {
            $("#aslawyer").click(function () {
                $(this).addClass("bg-dark");
                $(this).addClass("text-white");

                $("#asclient").removeClass("bg-dark");
                $("#asclient").removeClass("text-white");
                $("#asclient").addClass("btn-secondary");
                $("#external_div_logins").addClass("d-none");
                $("#register_link").attr("href", "/registrar-abogado");
            });
        },
        init: function () {
            this.onAsClient();
            this.onAsLawyer();
        }
    };

    return {
        init: function () {
            form.init();
            events.init();
            $(".input-group-append").on("click", function () {
                $(this).find("i").toggleClass("la-eye la-eye-slash");
                var x = document.getElementById("Password");
                if (x.type === "password") {
                    x.type = "text";
                } else {
                    x.type = "password";
                }
            });
        }
    };
}();

$(() => {
    login.init();
});