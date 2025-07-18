var InitApp = function () {

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
                        url: $(formElement).attr("action")
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
                            window.location.href = "/admin/misionvision"
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

            $("#add-form").validate({
                submitHandler: function (formElement, e) {
                    mApp.blockPage();
                    //e.submit();
                    var formData = new FormData($(formElement).get(0));
                    $.ajax({
                        data: formData,
                        type: "POST",
                        contentType: false,
                        processData: false,
                        url: $(formElement).attr("action")
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
                            window.location.href = "/admin/misionvision"
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
    var imageaction = {
        indexUpload: -1,
        uploadCropMission: null,
        uploadCropVision: null,
        init: function () {
            $('#upload-demo-0').hide();
            $('#upload-demo-1').hide();
            imageaction.uploadCropMission = $('#upload-demo-0').croppie({
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
            imageaction.uploadCropVision = $('#upload-demo-1').croppie({
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
            $("#recommended-resolution-0").text("aprox 525px x 305px");
            $("#recommended-resolution-1").text("aprox 525px x 305px");
            $(".cr-image").attr("src", $("#oldImage-0").attr("src"));
            $(".cr-image").attr("src", $("#oldImage-1").attr("src"));
              $('#Image-0').on('change', function () {
                $("#upload-offi-0").hide();
                $('#upload-demo-0').show();
                var reader = new FileReader();
                reader.onload = function (event) {
                    imageaction.uploadCropMission.croppie('bind', {
                        url: event.target.result
                    }).then(function () {
                        $("#oldImage-0").hide()
                         $('#upload-demo-0').show();
                        $('.upload-result-0').prop("disabled", false);
                        $(".cr-slider-wrap").show();
                    });
                }
                  reader.readAsDataURL(this.files[0]);
                  $(this).parent().find(".custom-file-label").text("Archivo seleccionado");
            });
            $('#Image-1').on('change', function () {
                $("#upload-offi-1").hide();
                $('#upload-demo-1').show();
                var reader = new FileReader();
                reader.onload = function (event) {
                    imageaction.uploadCropVision.croppie('bind', {
                        url: event.target.result
                    }).then(function () {
                        $("#oldImage-1").hide()
                        $('#upload-demo-1').show();
                        $('.upload-result-1').prop("disabled", false);
                        $(".cr-slider-wrap").show();
                    });
                }
                reader.readAsDataURL(this.files[0]);
                $(this).parent().find(".custom-file-label").text("Archivo seleccionado");
            });
            $('.upload-result-0').click(function (event) {
                imageaction.indexUpload = 0
                imageaction.uploadCropMission.croppie('result', {
                    type: 'canvas',
                    size: 'viewport'
                }).then(function (response) {
                    $("#upload-preview").attr("src", response);
                    $("#preview-modal").modal("show");
                });
            });
            $('.upload-result-1').click(function (event) {
                imageaction.indexUpload = 1
                imageaction.uploadCropVision.croppie('result', {
                    type: 'canvas',
                    size: 'viewport'
                }).then(function (response) {
                    $("#upload-preview").attr("src", response);
                    $("#preview-modal").modal("show");
                });
            });
            $('#btnCropSave').click(function (event) {
                if (imageaction.indexUpload == 0) {
                    imageaction.uploadCropMission.croppie('result', {
                        type: 'canvas',
                        size: 'viewport'
                    }).then(function (response) {
                        $("#preview-modal").modal("hide");
                        $("#img-offi-" + imageaction.indexUpload).attr("src", response);
                        $("#upload-demo-" + imageaction.indexUpload).hide();
                        $("#upload-offi-" + imageaction.indexUpload).show();
                        $('.upload-result-' + imageaction.indexUpload).prop("disabled", true);
                        $("#UrlCropImg-" + imageaction.indexUpload).val(response);
                    });
                } else {
                    imageaction.uploadCropVision.croppie('result', {
                        type: 'canvas',
                        size: 'viewport'
                    }).then(function (response) {
                        $("#preview-modal").modal("hide");
                        $("#img-offi-" + imageaction.indexUpload).attr("src", response);
                        $("#upload-demo-" + imageaction.indexUpload).hide();
                        $("#upload-offi-" + imageaction.indexUpload).show();
                        $('.upload-result-' + imageaction.indexUpload).prop("disabled", true);
                        $("#UrlCropImg-" + imageaction.indexUpload).val(response);
                    });
                } 
            });
        }
    };
    return {
        init: function () {
            summernot.init();
            imageaction.init();
            form.init();
        }
    };
}();

$(function () {
    InitApp.init();
});