var index = function () {

    var events = {
        onCancelSupscription: function () {
            $("#cancel_subscription").on("click", function () {
                swal({
                    title: '¿Está seguro?',
                    text: "Su plan será cancelado una vez termine su ciclo de facturación.",
                    type: 'warning',
                    showCancelButton: true,
                    confirmButtonText: 'Sí, cancelar',
                    cancelButtonText: 'Cancelar',
                    showLoaderOnConfirm: true,
                    allowOutsideClick: () => !swal.isLoading(),
                    preConfirm: () => {
                        return new Promise((resolve) => {
                            $.ajax({
                                url: `/abogado/tu-cuenta/cancelar-suscripcion`,
                                type: "POST",
                                contentType: false,
                                processData: false
                            })
                                .done(function (e) {
                                    swal({
                                        type: "success",
                                        allowOutsideClick: false,
                                        title: "Hecho",
                                        text: "Su suscripción ha sido cancelado.",
                                        confirmButtonText: "Aceptar"
                                    }).then((result) => {
                                        window.location.reload();
                                    });
                                })
                                .fail(function (e) {
                                    swal({
                                        type: "error",
                                        title: "Error al cancelar la suscripción.",
                                        text: e.status === 502 ? "No hay respuesta del servidor" : e.responseText,
                                        confirmButtonText: "Aceptar"
                                    });
                                });
                        });
                    }
                });
            });
        },
        init: function () {
            this.onCancelSupscription();
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