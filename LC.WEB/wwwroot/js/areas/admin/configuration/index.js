var index = function () {

    var form = {
        object: $("#main_form").validate({
            submitHandler: function (formElement, e) {
                e.preventDefault();
                var $btn = $(formElement).find("button[type='submit']");
                $btn.addClass("m-loader m-loader--right m-loader--light").attr("disabled", true);
                var formData = new FormData(formElement);
                $.ajax({
                    url: "/admin/configuracion/actualizar",
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
                            text: "Datos de configuración actualizados satisfactoriamente.",
                            confirmButtonText: "Aceptar"
                        });
                    })
                    .fail(function (e) {
                        swal({
                            type: "error",
                            title: "Error al postular.",
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
        withdrawalRequestDay: {
            load: function () {
                $("#WithdrawalRequestDay").select2();
            },
            init: function () {
                this.load();
            }
        },
        init: function () {
            this.withdrawalRequestDay.init();
        }
    };

    return {
        init: function () {
            select.init();
            $("#WorkScheduleStart, #WorkScheduleEnd").timepicker({
                timeFormat: 'h:mm p',
                interval: 60,
                minTime: '10',
                maxTime: '6:00pm',
                defaultTime: '11',
                startTime: '10:00',
                dynamic: false,
                dropdown: true,
                scrollbar: true
            });
        }
    };
}();

$(() => {
    index.init();
});