var question =function() {
    var LegalCaseId = $("#Id").val();

    var form = {
        object: $("#question_form").validate({
            submitHandler: function (formElement, e) {
                e.preventDefault();
                var $btn = $(formElement).find("button[type='submit']");
                $btn.addClass("m-loader m-loader--right m-loader--light").attr("disabled", true);
                var formData = new FormData(formElement);
                $(formElement).find(":input").attr("disabled", true);
                $.ajax({
                    method : "POST",
                    url: "/admin/casos-legales/preguntas/guardar",
                    data: formData,
                    contentType: false,
                    processData: false
                })
                    .done(function (e) {
                        swal({
                            type: "success",
                            allowOutsideClick: false,
                            title: "Éxito",
                            text: "Preguntas guardas con éxito.",
                            confirmButtonText: "Aceptar"
                        }).then((result) => {
                            window.location.href = `/admin/casos-legales/${LegalCaseId}/detalles`;
                        });
                    })
                    .fail(function (e) {
                        swal({
                            type: "error",
                            title: "Error al guardar los datos.",
                            text: e.status === 502 ? "No hay respuesta del servidor" : e.responseText,
                            confirmButtonText: "Aceptar"
                        });
                    })
                    .always(function () {
                        $btn.removeClass("m-loader m-loader--right m-loader--light").attr("disabled", false);
                        $(formElement).find(":input").attr("disabled", true);
                    });
            }
        })
    };

    var events = {
        onAcceptLegalCase: function () {
            $("#accept_legal_case").on("click", function () {
                swal({
                    title: '¿Está seguro?',
                    text: "El caso será confirmado y una vez derivado habilitado para recibir ofertas.",
                    type: 'warning',
                    showCancelButton: true,
                    confirmButtonText: 'Sí, confirmar',
                    cancelButtonText: 'Cancelar',
                    showLoaderOnConfirm: true,
                    allowOutsideClick: () => !swal.isLoading(),
                    preConfirm: () => {
                        return new Promise((resolve) => {
                            $.ajax({
                                url: `/admin/casos-legales/${LegalCaseId}/aceptar`,
                                type: "POST"
                            })
                                .done(function () {
                                    window.location.href = `/admin/casos-legales/${LegalCaseId}/detalles`;
                                });
                        });
                    }
                });
            });
        },
        init: function () {
            this.onAcceptLegalCase();
        }
    };

    return {
        init: function () {
            events.init();
        }
    };
}();

$(() => {
    question.init();
});