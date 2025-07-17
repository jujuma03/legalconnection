var InitApp = function () {
    var form = {
        object: $("#register").validate({
            rules: {
                Name: {
                    alpha: true
                },
                ConfirmPassword: {
                    equalTo: '[name="Password"]'
                },
                Surnames: {
                    alpha: true
                },
                PhoneNumber: {
                    digits: true,
                    exactlength: 9
                },
                Email: {
                    email: true
                }
            },
            submitHandler: function (formElement, e) {
                e.preventDefault();
                form.submit(formElement);
            }
        }),
        submit: function (formElement) {
            var $btn = $("#register").find("button[type='submit']").addLoader();
            var formData = new FormData(formElement);
            $.ajax({
                url: "/registrar",
                type :"POST",
                data: formData,
                contentType: false,
                processData: false
            })
                .done(function (e) {
                    window.location.href = "/cliente/registro-exitoso";

                    //swal({
                    //    type: "success",
                    //    allowOutsideClick: false,
                    //    title: "Registro Completado",
                    //    text: "Su registro ha sido satisfactorio. Se ha enviado un correo electrónico para validar su cuenta.",
                    //    confirmButtonText: "Aceptar"
                    //}).then((result) => {
                    //    if (result.value) {
                    //        window.location.href = "/login";
                    //    }
                    //});
                })
                .fail(function (e) {
                    swal({
                        type: "error",
                        title: "Error al completar el registro.",
                        text: e.responseText,
                        confirmButtonText: "Aceptar"
                    });
                })
                .always(function () {
                    $("#register").find("button[type='submit']").removeLoader();
                });
        }
    };

    var selects = {
        documentType: {
            init: function () {
                $("#DocumentType").select2();
            }
        },
        department: {
            init: function () {
                selects.department.load();
                selects.department.events();
            },
            load: function () {
                $.ajax({
                    url: `/departamentos/get`
                }).done(function (result) {
                    $("#DepartmentId").select2({
                        data: result.items
                    }).trigger("change");
                });
            },
            events: function () {
                $("#DepartmentId").on("change", function () {
                    selects.province.load($(this).val());
                });
            }
        },
        province: {
            init: function () {
                $("#ProvinceId").select2({
                    disabled: true,
                    placeholder : "Seleccionar provincia"
                }).trigger("change");
                selects.province.events();
            },
            load: function (did) {
                $("#ProvinceId").empty();
                $.ajax({
                    url: `/provincias/get/${did}`
                }).done(function (result) {
                    $("#ProvinceId").select2({
                        data: result.items,
                        disabled:false
                    }).trigger("change");
                });
            },
            events: function () {
                $("#ProvinceId").on("change", function () {
                    selects.district.load($(this).val());
                });
            }
        }, 
        district: {
            init: function () {
                $("#DistrictId").select2({
                    disabled: true,
                    placeholder: "Seleccionar distrito"
                });
            },
            load: function (pid) {
                $("#DistrictId").empty();
                $.ajax({
                    url: `/distritos/get/${pid}`
                }).done(function (result) {
                    $("#DistrictId").select2({
                        data: result.items,
                        disabled: false
                    });
                });
            },
        }, 
        init: function () {
            this.department.init();
            this.province.init();
            this.district.init();
            this.documentType.init();
        }
    };

    return {
        init: function () {
            selects.init();
        }
    };
}();

$(function () {
    InitApp.init();
});