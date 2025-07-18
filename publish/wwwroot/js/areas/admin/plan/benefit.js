var benefits = function () {

    var form = {
        object: $("#main_form").validate({
            submitHandler: function (formElement, e) {
                e.preventDefault();
                var $btn = $(formElement).find("button[type='submit']");
                $btn.addClass("m-loader m-loader--right m-loader--light").attr("disabled", true);
                var formData = new FormData(formElement);
                $.ajax({
                    url: "/admin/planes/beneficios-actualizar",
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
                            text: "Beneficios actualizados satisfactoriamente.",
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

    return {
        init: function () {
        }
    };
}();

$(() => {
    benefits.init();
});