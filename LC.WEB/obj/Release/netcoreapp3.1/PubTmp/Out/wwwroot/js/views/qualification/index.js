var qualification = function () {

    var form = {
        object: $("#main_form").validate({
            submitHandler: function (formElement, e) {
                e.preventDefault();
 
                var formData = new FormData(formElement);
                var qualification = $(".fa_star_selected");
                if (qualification.length <= 0) {
                    swal({
                        title :"Información",
                        type: "info",
                        text: "Debe seleccionar un puntaje para el abogado."
                    });
                    return;
                }

                var $btn = $(formElement).find("button[type='submit']");
                $btn.addClass("m-loader m-loader--right m-loader--light").attr("disabled", true);
                var maxValue = 0;
                $.each(qualification, function (i, v) {
                    var value = qualification.parent().data("id");
                    if (value > maxValue)
                        maxValue = value;
                });

                formData.append("Qualification", maxValue);

                $.ajax({
                    url: `/calificaciones/enviar`,
                    data: formData,
                    type: "POST",
                    contentType: false,
                    processData: false
                })
                    .done(function () {
                        swal({
                            type: "success",
                            allowOutsideClick: false,
                            title: "Éxito",
                            text: "Califación enviada con éxito. Gracias por ayudarnos a mejorar.",
                            confirmButtonText: "Aceptar"
                        }).then(function () {
                            window.location.href = "/";
                        });
                    })
                    .fail(function (e) {
                        $btn.removeLoader();
                        swal({
                            type: "error",
                            title: "Error al enviar la calificación.",
                            text: e.status === 502 ? "No hay respuesta del servidor" : e.responseText,
                            confirmButtonText: "Aceptar"
                        });
                    });
            }
        })
    };

    var starts = {
        onHover: function () {
            $(".btn_fa_start").hover(function () {
                var id = $(this).data("id");
                for (var i = 1; i <= 5; i++) {
                    var btn = $(`button[data-id='${i}']`);
                    var fa = btn.find(".fa-star");

                    if (i <= id) {
                        fa.addClass("fa_star_selected");
                    } else {
                        fa.removeClass("fa_star_selected");
                    }
               
                }
            });
        },
        init: function () {
            this.onHover();
        }
    };

    var commentary = {
        init: function () {
            $('#Commentary').maxlength({
                alwaysShow: true,
                threshold: 5,
                warningClass: "m-badge m-badge--primary m-badge--rounded m-badge--wide",
                limitReachedClass: "m-badge m-badge--brand m-badge--rounded m-badge--wide",
            });
        }
    };

    return {
        init: function () {
            starts.init();
            commentary.init();
        }
    };
}();

$(() => {
    qualification.init();
});

