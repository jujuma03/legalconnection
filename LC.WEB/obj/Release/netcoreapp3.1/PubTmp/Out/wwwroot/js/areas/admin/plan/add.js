var newplan = function () {

    var form = {
        object: $("#main_form").validate({
            rules: {
                IntervalCount: {
                    digits: true,
                    min :0
                },
                TrialDays: {
                    digits: true,
                    min : 0
                }
            },
            submitHandler: function (formElement, e) {
                e.preventDefault();
                var $btn = $(formElement).find("button[type='submit']");
                $btn.addClass("m-loader m-loader--right m-loader--light").attr("disabled", true);
                var formData = new FormData(formElement);
                $.ajax({
                    url: "/admin/planes/agregar-plan",
                    type: "POST",
                    data: formData,
                    contentType: false,
                    processData: false
                })
                    .done(function (e) {
                        swal({
                            type: "success",
                            allowOutsideClick: false,
                            title: "Éxito",
                            text: "Plan agregado satisfactoriamente.",
                            confirmButtonText: "Aceptar"
                        }).then((result) => {
                            window.location.href = "/admin/planes";
                        });
                    })
                    .fail(function (e) {
                        swal({
                            type: "error",
                            title: "Error.",
                            text: e.status === 502 ? "No hay respuesta del servidor" : e.responseText,
                            confirmButtonText: "Aceptar"
                        });
                    })
                    .always(function () {
                        $btn.removeClass("m-loader m-loader--right m-loader--light").attr("disabled", false);
                    });
            }
        })
    };

    var select = {
        interval: {
            load: function () {
                $("#Interval").select2({
                    placeholder: "Seleccionar intervalo",
                    minimumResultsForSearch: -1
                });
            },
            init: function () {
                this.load();
            }
        },
        init: function () {
            this.interval.init();
        }
    };

    return {
        init: function () {
            select.init();
        }
    };
}();

$(() => {
    newplan.init();
});