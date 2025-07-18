var details = function () {

    var form = {
        object: $("#main_form").validate({
            submitHandler: function (formElement, e) {
                e.preventDefault();
                var $btn = $(formElement).find("button[type='submit']");
                $btn.addClass("m-loader m-loader--right m-loader--light").attr("disabled", true);
                var formData = new FormData(formElement);
                $(formElement).find(":input").attr("disabled", true);
                $.ajax({
                    url: "/admin/solicitudes-retiro/actualizar",
                    type: "POST",
                    data: formData,
                    contentType: false,
                    processData: false
                })
                    .done(function (e) {
                        swal({
                            type: "success",
                            allowOutsideClick: false,
                            title: "Hecho!.",
                            text: "Solicitud actualizada exitosamente",
                            confirmButtonText: "Entendido"
                        }).then((result) => {
                            if (result.value) {
                                window.location.href = "/admin/solicitudes-retiro";
                            }
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
                        $(formElement).find(":input").attr("disabled", false);
                    });
            }
        })
    };

    var modal = {
        denied: {
            object: $("#modal_denied"),
            form: {
                object: $("#denied_form").validate({
                    submitHandler: function (formElement, e) {
                        e.preventDefault();
                        var $btn = $(formElement).find("button[type='submit']");
                        $btn.addClass("m-loader m-loader--right m-loader--light").attr("disabled", true);
                        var formData = new FormData(formElement);
                        $(formElement).find(":input").attr("disabled", true);
                        $.ajax({
                            url: "/admin/solicitudes-retiro/actualizar",
                            type: "POST",
                            data: formData,
                            contentType: false,
                            processData: false
                        })
                            .done(function (e) {
                                swal({
                                    type: "success",
                                    allowOutsideClick: false,
                                    title: "Hecho!.",
                                    text: "Solicitud actualizada exitosamente",
                                    confirmButtonText: "Entendido"
                                }).then((result) => {
                                    if (result.value) {
                                        window.location.href = "/admin/solicitudes-retiro";
                                    }
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
                                $(formElement).find(":input").attr("disabled", false);
                            });
                    }
                })
            }
        }
    };

    var events = {
        onChangeFile: function () {
            $("#DepositReceiptFile").on("change", function () {
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
            events.init();
        }
    };
}();

$(() => {
    details.init();
});