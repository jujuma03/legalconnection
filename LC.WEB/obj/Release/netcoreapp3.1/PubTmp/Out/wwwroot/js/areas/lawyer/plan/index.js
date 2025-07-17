var planDetail = function () {

    var events = {
        onChangePlan: function () {
            $(".btn_select_change_plan").on("click", function () {
                swal({
                    title: '¿Está seguro?',
                    text: "Su plan será actualizado.",
                    type: 'warning',
                    showCancelButton: true,
                    confirmButtonText: 'Sí, cambiar',
                    cancelButtonText: 'Cancelar',
                    showLoaderOnConfirm: true,
                    allowOutsideClick: () => !swal.isLoading(),
                    preConfirm: () => {
                        return new Promise((resolve) => {
                            var planId = $(this).data("id");
                            var formData = new FormData();
                            formData.append("PlanId", planId);

                            $.ajax({
                                url: `/abogado/planes/cambiar-plan`,
                                type: "POST",
                                data: formData,
                                contentType: false,
                                processData: false
                            })
                                .done(function (e) {
                                    swal({
                                        type: "success",
                                        allowOutsideClick: false,
                                        title: "Hecho",
                                        text: "Plan actualizado con éxito.",
                                        confirmButtonText: "Aceptar"
                                    })
                                        .then((result) => {
                                            window.location.href = "/abogado/tu-cuenta";
                                        });

                                })
                                .fail(function (e) {
                                    swal({
                                        type: "error",
                                        title: "Error al actualizar su plan.",
                                        text: e.status === 502 ? "No hay respuesta del servidor" : e.responseText,
                                        confirmButtonText: "Aceptar"
                                    })
                                        .then((result) => {
                                            window.location.href = "/abogado/tu-cuenta/metodos-pago";
                                        });
                                });
                        });
                    }
                });
            });
        },
        init: function () {
            this.onChangePlan();

            $('[data-toggle="popover"]').popover();
            $('.popover-dismiss').popover({
                trigger: 'focus'
            })
        }
    };

    return {
        init: function () {
            events.init();
        }
    };
}();

$(() => {
    planDetail.init();
});