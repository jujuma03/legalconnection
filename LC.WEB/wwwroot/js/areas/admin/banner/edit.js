var InitApp = function () {

    var configurationBtn = {
        init: function () {
            $('#btnRegresar').click(function () {
                window.location.href = `/admin/banners-inicio`;
            });
        }
    }
    var check = {
        init: function () {
            var status = $("#tbStatus").val();
            var statusDirec = $("#tbStatusDirection").val();
            var routeType = $("#tbRouteType").val();

            if (routeType == "Interna")
                $("#checkRouteType").prop('checked', true);
            else
                $("#checkRouteType").prop('checked', false);

            if (status == "Activo")
                $("#checkStatus").prop('checked', true);
            else
                $("#checkStatus").prop('checked', false);

            if (statusDirec == "Activo")
                $("#checkStatusDirection").prop('checked', true);
            else
                $("#checkStatusDirection").prop('checked', false);
        }
    }

    var order = {
        init: function () {

            if ($("#tbStatus").val() == "Oculto") {
                $("#listOrder").hide();
            }

            $("#checkStatus").change(function () {
                if (this.checked) {
                    $("#listOrder").show();
                }
                else
                    $("#listOrder").hide();
            })

        }
    }

    var form = {
        submit: function (form) {
            var formData = new FormData($(form).get(0));
            $("#editForm input").attr("disabled", true);
            $("#editForm select").attr("disabled", true);
            $(".btn-save").addLoader();
            $.ajax({
                data: formData,
                type: "POST",
                contentType: false,
                processData: false,
                url: $(form).attr("action")
            })
                .always(function () {
                    $("#editForm input").attr("disabled", false);
                    $("#editForm select").attr("disabled", false);
                    $(".btn-save").removeLoader();
                })
                .done(function (result) {
                    swal({
                        type: "success",
                        allowOutsideClick: false,
                        title: "Éxito",
                        text: "Banner guardado satisfactoriamente.",
                        confirmButtonText: "Aceptar"
                    }).then((result) => {
                        location.href = "/admin/banners-inicio";
                    });
                })
                .fail(function (e) {
                    swal({
                        type: "error",
                        title: "Error al editar banner.",
                        text: e.status === 502 ? "No hay respuesta del servidor" : e.responseText,
                        confirmButtonText: "Aceptar"
                    });
                });
        }
    };

    var validate = {
        init: function () {
            $("#editForm").validate({
                submitHandler: function (formElement, e) {
                    e.preventDefault();
                    form.submit(formElement);
                }
            });
        }
    };

    var upload = {
        init: function () {
            $("#upload-offi").hide();
            $uploadCrop = $('#upload-demo').croppie({
                enableExif: true,
                viewport: {
                    width: 1920,
                    height: 950,
                    type: 'square'
                },
                boundary: {
                    width: 1940,
                    height: 980
                }
            });

            var imgValue = $("#imgValue").val();
            $(".cr-image").css('transform', 'translate3d(10px, 15px, 0) scale(1)');
            $(".cr-image").attr("src", imgValue);
            $(".cr-slider-wrap").hide();

            $('#Image').on('change', function () {
                $("#upload-offi").hide();
                $('#upload-demo').show();
                var reader = new FileReader();
                reader.onload = function (event) {
                    $uploadCrop.croppie('bind', {
                        url: event.target.result
                    }).then(function () {
                        $('.upload-result').prop("disabled", false);
                        $(".cr-slider-wrap").show();
                    });
                }
                reader.readAsDataURL(this.files[0]);
                $(this).parent().find(".custom-file-label").text("Archivo seleccionado");
            });

            $('.upload-result').click(function (event) {
                $uploadCrop.croppie('result', {
                    type: 'canvas',
                    size: 'viewport'
                }).then(function (response) {

                    $("#upload-preview").attr("src", response);
                    $("#preview-modal").modal("show");

                });

            });

            $('#btnCropSave').click(function (event) {
                $uploadCrop.croppie('result', {
                    type: 'canvas',
                    size: 'viewport'
                }).then(function (response) {
                    $("#preview-modal").modal("hide");
                    $("#img-offi").attr("src", response);
                    $("#upload-demo").hide();
                    $("#upload-offi").show();
                    $('.upload-result').prop("disabled", true);
                    $("#urlCropImg").val(response);

                });
            });

        }
    };

    var select = {
        init: function () {
            $("#SequenceOrder").select2();
            $("#Status").select2();
        }
    };

    return {
        init: function () {
            select.init();
            upload.init();
            check.init();
            validate.init();
            configurationBtn.init();
            order.init();
            //$('#listOrder').hide();
        }
    }
}();

$(function () {
    InitApp.init();
});
