var InitApp = function () {
    var configurationBtn = {
        init: function () {
            $('#btnRegresar').click(function () {
                window.location.href = `/admin/banners-inicio`;
            });
        }
    };
    var summernot = {
        init: function () {
            $(".mv_summernote").summernote({
                lang: "es-ES",
                airMode: false,
                height: 350,
                toolbar: [
                    ['style', ['style']],
                    ['font', ['bold', 'italic', 'underline', 'strikethrough', 'superscript', 'subscript', 'clear']],
                    ['fontname', ['fontname']],
                    ['fontsize', ['fontsize']],
                    ['color', ['color']],
                    ['para', ['ol', 'ul', 'paragraph', 'height']],
                    ['table', ['table']],
                    ['insert', ['link', 'video', 'hr']],
                    ['view', ['undo', 'redo', 'fullscreen', 'codeview']]
                ]
            })
        }
    };
    var selects = {
        init: function () {
            $("#selectOrder").select2();
            $("#selectType")
                .on("change", function () {
                    var idtype = $(this).val();
                    $.ajax({
                        type: "GET",
                        url: `/admin/como-funciona/get-ordenes/${idtype}`
                    }).done(function (result) {
                        $("#selectOrder").html("");
                        $("#selectOrder").select2({
                            data: result,
                            disabled: false
                        });
                    });
                })
                .select2();
        }
    };
    var check = {
        init: function () {
            var status = $("#tbStatus").val();
            if (status == "Activo")
                $("#checkStatus").prop('checked', true);
            else
                $("#checkStatus").prop('checked', false);
        }
    };
    var imageaction = {
        init: function () {
            $('#upload-demo').hide();
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
            $("#recommended-resolution").text("aprox 525px x 305px");
            $(".cr-image").attr("src", $("#oldImage").attr("src"));
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
    var form = {
        init: function () {
            $("#update-form").validate({
                submitHandler: function (formElement, e) {
                    e.preventDefault();
                    mApp.blockPage();
                    //e.submit();
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
                            text: "Datos guardados satisfactoriamente.",
                            confirmButtonText: "Aceptar"
                        }).then((result) => {
                            window.location.href = "/admin/como-funciona"
                        })
                    })
                        .fail(function (e) {
                            swal({
                                type: "error",
                                title: "Error al guardar datos.",
                                text: e.status === 502 ? "No hay respuesta del servidor" : e.responseText,
                                confirmButtonText: "Aceptar"
                            });
                        });
                }
            });
        }
    };

    return {
        init: function () {
            configurationBtn.init();
            selects.init();
            check.init();
            summernot.init();
            form.init();
            imageaction.init();
        }
    };
}();

$(function () {
    InitApp.init();
});