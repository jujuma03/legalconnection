var InitApp = function () {
    var configurationBtn = {
        init: function () {
            $('#btnRegresar').click(function () {
                window.location.href = `/admin/banners-inicio`;
            });
            $("#order").hide();
        }
    }
    var select = {
        init: function () {
            $("#Status").select2();
            $("#SequenceOrder").select2();
        }
    };
    var form = {
        init: function () {
            $("#createForm").validate({
                submitHandler: function (formElement, e) {
                    e.preventDefault();
                    $(".m-alert").addClass("m--hide");
                    mApp.blockPage();
                    var formData = new FormData($(formElement).get(0));
                    $.ajax({
                        data: formData,
                        type: "POST",
                        contentType: false,
                        processData: false,
                        url: $(form).attr("action")
                    }).always(function () {
                        mApp.unblockPage();
                    }).done(function () {
                        swal({
                            type: "success",
                            allowOutsideClick: false,
                            title: "Éxito",
                            text: "Banner creado satisfactoriamente.",
                            confirmButtonText: "Aceptar"
                        }).then((result) => {
                            window.location.href = "/admin/banners-inicio"
                        })
                    })
                        .fail(function (e) {
                            swal({
                                type: "error",
                                title: "Error al crear banner.",
                                text: e.status === 502 ? "No hay respuesta del servidor" : e.responseText,
                                confirmButtonText: "Aceptar"
                            });
                        });
                    //e.submit();
                }
            });
        }
    };

    var order = {
        init: function () {
            $("input[type='checkbox']").change(function () {
                if (this.checked) {
                    $("#order").show();
                }
                else
                    $("#order").hide();
            })
        }
    }

    var upload = {
        init: function () {
            $("#btnSave").hide();
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

            $(".cr-image").attr("src", "http://ipsumimage.appspot.com/1920x980");
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
                    $("#btnSave").show();
                    $("#upload-demo").hide();
                    $("#upload-offi").show();
                    $('.upload-result').prop("disabled", true);
                    $("#urlCropImg").val(response);

                });
            });
        }
    };

    return {
        init: function () {
            form.init();
            select.init();
            upload.init();
            configurationBtn.init();
            order.init();
        }
    };
}();
$(function () {
    InitApp.init();
});