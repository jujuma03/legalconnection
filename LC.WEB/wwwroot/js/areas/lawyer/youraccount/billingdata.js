var billingdata = function () {

    var form = {
        object: $("#form_billingdata").validate({
            submitHandler: function (formElement, e) {
                e.preventDefault();
                var $btn = $(formElement).find("button[type='submit']");
                $btn.addClass("m-loader m-loader--right m-loader--light").attr("disabled", true);
                var formData = new FormData(formElement);
                $.ajax({
                    url: "/abogado/tu-cuenta/actualizar-datos-facturacion",
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
                            text: "Datos de Facturación actualizados.",
                            confirmButtonText: "Aceptar"
                        }).then((result) => {
                            window.location.reload();
                        });
                    })
                    .fail(function (e) {
                        $btn.removeClass("m-loader m-loader--right m-loader--light").attr("disabled", false);
                        swal({
                            type: "error",
                            title: "Error al actualizar los datos de facturación.",
                            text: e.status === 502 ? "No hay respuesta del servidor" : e.responseText,
                            confirmButtonText: "Aceptar"
                        });
                    })
            }
        })
    };

    return {
        init: function () {

        }
    };
}();

$(() => {
    billingdata.init();
});

