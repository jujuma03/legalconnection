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
            $("#add_items_form").validate({
                submitHandler: function (formElement, e) {
                    e.preventDefault();
                    form.submit(formElement);
                }
            });
        }
    };
    var select = {
        init: function () {
            $("#selectSection")
                .on("change", function () {
                    $("#upload-offi").hide();
                    $("#editForm").find("[name='Image']").val(null);
                    $("#editForm").find(".custom-file-label").text("Seleccione un archivo");
                    $('#Image').val(null);
                    $('#Image').text("");
                    if ($('#upload-demo').data('croppie')) {
                        $('#upload-demo').croppie('destroy')
                    }

                    $('#upload-demo').show();
                    //SERVICIOS
                    if ($(this).val() == 2) {
                        $uploadCrop = $('#upload-demo').croppie({
                            enableExif: true,
                            viewport: {
                                width: 525,
                                height: 305,
                                type: 'square'
                            },
                            boundary: {
                                width: 530,
                                height: 320
                            }
                        });
                        $("#recommended-resolution").text("aprox 525px x 305px")
                        $(".cr-image").attr("src", "http://ipsumimage.appspot.com/525x305");
                        //PERFIL DE LA EMPRESA
                        //PERFILES DE LOS ABOGADOS
                        //NUESTRO EQUIPO 
                    } else if ($(this).val() == 3 || $(this).val() == 4 || $(this).val() == 5) {
                        $uploadCrop = $('#upload-demo').croppie({
                            enableExif: true,
                            viewport: {
                                width: 130,
                                height: 130,
                                type: 'circle'
                            },
                            boundary: {
                                width: 135,
                                height: 135
                            }
                        });
                        $("#recommended-resolution").text("aprox 130px x 130px")
                        $(".cr-image").attr("src", "http://ipsumimage.appspot.com/130x130");
                    } else if ($(this).val() == 6) {
                        $uploadCrop = $('#upload-demo').croppie({
                            enableExif: true,
                            viewport: {
                                width: 200,
                                height: 200,
                                type: 'circle'
                            },
                            boundary: {
                                width: 205,
                                height: 205
                            }
                        });
                        $("#recommended-resolution").text("aprox 200px x 200px")
                        $(".cr-image").attr("src", "http://ipsumimage.appspot.com/200x200");
                    }
                    $(".cr-slider-wrap").hide();
                })
                .select2();
        }
    };
    var imageaction = {
        init: function () {
            $('#upload-demo').hide();
            var typeSetion = $("#selectSection").val();
            if (typeSetion == 2 || typeSetion==8 ) {
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
            } else if (typeSetion == 3 || typeSetion == 4 || typeSetion == 5 || typeSetion == 7) {
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
            } else if (typeSetion == 6) {
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
            $('#Image').on('change', function () {
                $("#upload-offi").hide();
                $('#upload-demo').show();
                var reader = new FileReader();
                reader.onload = function (event) {
                    $uploadCrop.croppie('bind', {
                        url: event.target.result
                    }).then(function () {
                        $("#oldImage").hide()
                        $('#upload-demo').show();
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
            select.init();
            validate.init();
            imageaction.init();
        }
    };
}();

$(function () {
    InitApp.init();
});