var index = function () {

    var form = {
        object: $("#main_form").validate({
            submitHandler: function (formElement, e) {
                e.preventDefault();
                var $btn = $(formElement).find("button[type='submit']");
                $btn.addClass("m-loader m-loader--right m-loader--light").attr("disabled", true);
                var formData = new FormData(formElement);
                $.ajax({
                    url: "/abogado/finanzas/ingresar-solicitud-retiro",
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
                            text: "Solicitud enviada exitosamente.",
                            confirmButtonText: "Aceptar"
                        }).then((result) => {
                            window.location.href = "/abogado/finanzas/retiro-efectivo";
                        });
                    })
                    .fail(function (e) {
                        swal({
                            type: "error",
                            title: "Error al enviar la solicitud.",
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
        financialEntity: {
            load: function () {
                $("#FinancialEntity").select2();
            },
            init: function () {
                this.load();
            }
        },
        init: function () {
            this.financialEntity.init();
        }
    };

    var events = {
        onChangeFile: function () {
            $("#ReceiptFileForFees").on("change", function () {
                var file = $(this).val();
                $(".custom-file-label").text(file);
            });
        },
        init: function () {
            this.onChangeFile();
        }
    };

    return {
        init: function () {
            select.init();
            events.init();
        }
    };
}();

$(() => {
    index.init();
});
