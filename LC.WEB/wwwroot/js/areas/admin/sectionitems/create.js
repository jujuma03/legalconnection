var InitApp = function () {
    var form = {
        submit: function (form) {
            var formData = new FormData($(form).get(0));
            $("#add_items_form input").attr("disabled", true);
            $("#add_items_form select").attr("disabled", true);
            $("#add_items_form textarea").attr("disabled", true);
            $(".btn-save").addLoader();
            $.ajax({
                data: formData,
                type: "POST",
                contentType: false,
                processData: false,
                url: $(form).attr("action")
            })
                .always(function () {
                    $("#add_items_form input").attr("disabled", false);
                    $("#add_items_form select").attr("disabled", false);
                    $("#add_items_form textarea").attr("disabled", false);
                    $(".btn-save").removeLoader();
                })
                .done(function (result) {
                    swal({
                        type: "success",
                        allowOutsideClick: false,
                        title: "Éxito",
                        text: "Elemento guardado satisfactoriamente.",
                        confirmButtonText: "Aceptar"
                    }).then((result) => {
                        $("#add_modal").modal("hide");
                        window.location.href = "/admin/elementos-seccion"
                    });
                })
                .fail(function (e) {
                    swal({
                        type: "error",
                        title: "Error al guardar elemento.",
                        text: e.status === 502 ? "No hay respuesta del servidor" : e.responseText,
                        confirmButtonText: "Aceptar"
                    });
                });
        }
    };

    var validate = {
        init: function () {
            $('#upload-demo').hide();
            $("#upload-offi").hide();
            $('.btn-save').hide();
            $("#add_items_form").validate({
                submitHandler: function (formElement, e) {
                    e.preventDefault();
                    form.submit(formElement);
                }
            });
            $("#add_modal").on('hidden.bs.modal', function (e) {
                $("#add_items_form").resetForm();
                $("#add_items_form").find("[name='Image']").val(null);
                $("#add_items_form").find(".custom-file-label").text("Seleccione un archivo");
            });
        }
    };
    var select = {
        init: function () {
            $("#selectSection")
                .on("change", function () {
                    $("#upload-offi").hide();
                    $("#add_items_form").find("[name='Image']").val(null);
                    $("#add_items_form").find(".custom-file-label").text("Seleccione un archivo");
                    $('#Image').val(null);
                    $('#Image').text("");
                    if ($('#upload-demo').data('croppie')) {
                        $('#upload-demo').croppie('destroy')
                    }

                    $('#upload-demo').show();
                    //SERVICIOS
                    if ($(this).val() == 2 || $(this).val() == 8) {
                        $uploadCrop = $('#upload-demo').croppie({
                            enableExif: true,
                            viewport: {
                                width: 350,
                                height: 250,
                                type: 'square'
                            },
                            boundary: {
                                width: 355,
                                height: 255
                            }
                        });
                        $("#recommended-resolution").text("aprox 350px x 250px")
                        $(".cr-image").attr("src", "http://ipsumimage.appspot.com/350x250");
                        //PERFIL DE LA EMPRESA
                        //PERFILES DE LOS ABOGADOS
                        //NUESTRO EQUIPO 
                    } else if ($(this).val() == 3 || $(this).val() == 4 || $(this).val() == 5 || $(this).val() == 7 ) {
                        $uploadCrop = $('#upload-demo').croppie({
                            enableExif: true,
                            viewport: {
                                width: 300,
                                height: 300,
                                type: 'square'
                            },
                            boundary: {
                                width: 305,
                                height: 305
                            }
                        });
                        $("#recommended-resolution").text("aprox 300px x 300px")
                        $(".cr-image").attr("src", "http://ipsumimage.appspot.com/300x300");
                    } else if ($(this).val() == 6) {
                        $uploadCrop = $('#upload-demo').croppie({
                            enableExif: true,
                            viewport: {
                                width: 300,
                                height: 300,
                                type: 'square'
                            },
                            boundary: {
                                width: 305,
                                height: 305
                            }
                        });
                        $("#recommended-resolution").text("aprox 300px x 305px")
                        $(".cr-image").attr("src", "http://ipsumimage.appspot.com/305x305");
                    }
                    $(".cr-slider-wrap").hide();
                })
                .select2();
        }
    };
    var imageaction = {
        init: function () {
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
                    $(".btn-save").show();
                    $("#upload-demo").hide();
                    $("#upload-offi").show();
                    $('.upload-result').prop("disabled", true);
                    $("#UrlCropImg").val(response);
                });
            });
        }
    };
    return {
        init: function () {
            $('#btnRegresar').click(function () {
                window.location.href = `/admin/elementos-seccion`;
            });
            imageaction.init();
            select.init();
            validate.init();

            $('#upload-demo').show();
            $uploadCrop = $('#upload-demo').croppie({
                enableExif: true,
                viewport: {
                    width: 350,
                    height: 250,
                    type: 'square'
                },
                boundary: {
                    width: 355,
                    height: 255
                }
            });
            $("#recommended-resolution").text("aprox 350px x 250px")
            $(".cr-image").attr("src", "http://ipsumimage.appspot.com/350x250");
        }
    };
}();

$(function () {
    InitApp.init();
});