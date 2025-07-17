var index = function () {

    var events = {
        onAcceptTermAndConditions: function () {
            $("#accept_terms_conditions").click(function () {
                var $btn = $(this);
                $btn.addClass("m-loader m-loader--right m-loader--light").attr("disabled", true);
                $.ajax({
                    url: "/inicio/aceptar-terminos-condiciones",
                    type: "POST"
                })
                    .done(function () {
                        swal({
                            type: "success",
                            allowOutsideClick: false,
                            title: "Hecho.",
                            text: "Gracias por aceptar nuestros términos y condiciones. ¡Muchos Éxitos!",
                            confirmButtonText: "Entendido"
                        }).then((result) => {
                            if (result.value) {
                                window.location.href = "/abogado/perfil";
                            }
                        });
                    })
                    .fail(function (e) {
                        $btn.removeClass("m-loader m-loader--right m-loader--light").attr("disabled", true);
                        swal({
                            type: "error",
                            title: "Error al guardar los datos.",
                            text: e.status === 502 ? "No hay respuesta del servidor" : e.responseText,
                            confirmButtonText: "Aceptar"
                        });
                    });
            });
        },
        init: function () {
            this.onAcceptTermAndConditions();
        }
    };

    return {
        init: function () {
            events.init();
        }
    };
}();

$(() => {
    index.init();
});