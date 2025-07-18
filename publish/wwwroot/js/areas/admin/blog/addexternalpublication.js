var addexternalpublication = function () {

    var summernote = {
        //defaultOptions: {
        //    height: 250,
        //    toolbar: [
        //        ['style', ['bold', 'italic', 'underline']],
        //        ['fontsize', ['fontsize']],
        //        ['color', ['color']],
        //        ['para', ['ul', 'ol', 'paragraph']],
        //        ['height', ['height']]
        //    ]
        //},
        //description: {
        //    object: null,
        //    getValue: function () {
        //        return this.object.summernote('code');
        //    },
        //    init: function () {
        //        this.object = $("#Description").summernote(summernote.defaultOptions);
        //    }
        //},
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
            //this.description.init();
        }
    };

    var datepicker = {
        init: function () {
            $(".datepicker_input").datepicker({
                endDate: '+0d',
                clearBtn: false,
                orientation: "bottom",
            });
        }
    };

    var form = {
        init: function () {
            $("#main_form").validate({
                submitHandler: function (formElement, e) {
                    e.preventDefault();
                    var $btn = $(formElement).find("button[type='submit']");
                    $btn.addClass("m-loader m-loader--right m-loader--light").attr("disabled", true);
                    var formData = new FormData(formElement);
                    //formData.append("Description", summernote.description.getValue());
                    $.ajax({
                        url: "/admin/blog/publicacion-external-add",
                        type: "POST",
                        data: formData,
                        contentType: false,
                        processData: false
                    })
                        .done(function (e) {
                            window.location.href = "/admin/blog/publicaciones-externas";
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
                        });
                }
            })
        }
    };

    var imageaction = {
        indexUpload: -1,
        uploadCropBlogImage: null,
        uploadCropLawyerPhoto: null,
        init: function () {
            $('#upload-demo-0').hide();
            $('#upload-demo-1').hide();
            imageaction.uploadCropBlogImage = $('#upload-demo-0').croppie({
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
            imageaction.uploadCropLawyerPhoto = $('#upload-demo-1').croppie({
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
            $('#Image-0').on('change', function () {
                $("#upload-offi-0").hide();
                $('#upload-demo-0').show();
                var reader = new FileReader();
                reader.onload = function (event) {
                    imageaction.uploadCropBlogImage.croppie('bind', {
                        url: event.target.result
                    }).then(function () {
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
                    imageaction.uploadCropLawyerPhoto.croppie('bind', {
                        url: event.target.result
                    }).then(function () {
                        $('.upload-result-1').prop("disabled", false);
                        $(".cr-slider-wrap").show();
                    });
                }
                reader.readAsDataURL(this.files[0]);
                $(this).parent().find(".custom-file-label").text("Archivo seleccionado");
            });
            $('.upload-result-0').click(function (event) {
                imageaction.indexUpload = 0
                imageaction.uploadCropBlogImage.croppie('result', {
                    type: 'canvas',
                    size: 'viewport'
                }).then(function (response) {
                    $("#upload-preview").attr("src", response);
                    $("#preview-modal").modal("show");
                });
            });
            $('.upload-result-1').click(function (event) {
                imageaction.indexUpload = 1
                imageaction.uploadCropLawyerPhoto.croppie('result', {
                    type: 'canvas',
                    size: 'viewport'
                }).then(function (response) {
                    $("#upload-preview").attr("src", response);
                    $("#preview-modal").modal("show");
                });
            });
            $('#btnCropSave').click(function (event) {
                if (imageaction.indexUpload == 0) {
                    imageaction.uploadCropBlogImage.croppie('result', {
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
                    imageaction.uploadCropLawyerPhoto.croppie('result', {
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
            summernote.init();
            datepicker.init();
            imageaction.init();
            form.init();
        }
    };
}();

$(() => {
    addexternalpublication.init();
});
