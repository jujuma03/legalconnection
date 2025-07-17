var add = function () {

    var select2 = {
        department: {
            load: function () {
                $("#DepartmentId").select2({
                    ajax: {
                        url: "/departamentos/get/v2",
                        delay: 500,
                        data: function (params) {
                            var query = {
                                page: params.page || 1,
                                term: params.term
                            };
                            return query;
                        }
                    },
                    minimumInputLength: 0,
                    placeholder: 'Seleccione departamento',
                    allowClear: false
                });

                if ($("#DepartmentId_value").val() !== "" && $("#DepartmentId_value").val() !== null) {
                    this.setValue($("#DepartmentId_value").val(), $("#Department_value").val());
                }
            },
            setValue: function (id, text) {
                if ($('#DepartmentId').find("option[value='" + id + "']").length) {
                    $('#DepartmentId').val(id).trigger('change');
                } else {
                    var newOption = new Option(text, id, true, true);
                    $('#DepartmentId').append(newOption).trigger('change');
                }
            },
            events: {
                onChange: function () {
                    $("#DepartmentId").on("change", function () {
                        select2.province.clear();
                    });
                },
                init: function () {
                    this.onChange();
                }
            },
            init: function () {
                this.load();
                this.events.init();
            }
        },
        province: {
            load: function () {
                $("#ProvinceId").select2({
                    ajax: {
                        url: "/provincias/get/v2",
                        delay: 500,
                        data: function (params) {
                            var query = {
                                departmentId: $("#DepartmentId").val(),
                                page: params.page || 1,
                                term: params.term
                            };
                            return query;
                        }
                    },
                    minimumInputLength: 0,
                    placeholder: 'Seleccione provincia',
                    allowClear: true
                });


                if ($("#ProvinceId_value").val() !== "" && $("#ProvinceId_value").val() !== null) {
                    this.setValue($("#ProvinceId_value").val(), $("#Province_value").val());
                }
            },
            setValue: function (id, text) {
                if ($('#ProvinceId').find("option[value='" + id + "']").length) {
                    $('#ProvinceId').val(id).trigger('change');
                } else {
                    var newOption = new Option(text, id, true, true);
                    $('#ProvinceId').append(newOption).trigger('change');
                }
            },
            clear: function () {
                $("#ProvinceId").val(null).trigger("change");
            },
            init: function () {
                this.load();
            }
        },
        specialityTheme: {
            load: function () {
                $("#SpecialityThemeId").select2({
                    placeholder: 'Seleccione tema de especialidad',
                });

                $("#SpecialityThemeId").val(null).trigger("change");
            },
            init: function () {
                this.load();
            }
        },
        init: function () {
            this.department.init();
            this.province.init();
            this.specialityTheme.init();
        }
    };

    var form = {
        object: $("#form_add_legal_case").validate({
            rules: {
                Description: {
                    maxlength: 240
                }
            },
            submitHandler: function (formElement, e) {
                e.preventDefault();
                var $btn = $(formElement).find("button[type='submit']");
                $btn.addClass("m-loader m-loader--right m-loader--light").attr("disabled", true);
                var formData = new FormData(formElement);
                $(formElement).find(":input").attr("disabled", true);
                $.ajax({
                    url: "/mis-casos/agregar-digirido",
                    type: "POST",
                    data: formData,
                    contentType: false,
                    processData: false
                })
                    .done(function (e) {

                        window.location.href = "/mis-casos/registro-satisfactorio";

                        //swal({
                        //    type: "success",
                        //    allowOutsideClick: false,
                        //    title: "Tu caso ha sido registado.",
                        //    text: "En breve un asesor se contactará contigo para brindarte mayor alcance sobre nuestro servicio. ",
                        //    confirmButtonText: "Entendido"
                        //}).then((result) => {
                        //    if (result.value) {
                        //        window.location.href = "/mis-casos";
                        //    }
                        //});
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
                        $(formElement).find(":input").attr("disabled", false);
                    });
            }
        })
    };

    var description = {
        init: function () {
            $('#Description').maxlength({
                alwaysShow: true,
                threshold: 5,
                warningClass: "m-badge m-badge--primary m-badge--rounded m-badge--wide",
                limitReachedClass: "m-badge m-badge--brand m-badge--rounded m-badge--wide",
            });
        }
    };

    var events = {
        getCurrentPosition: function () {
            if ("geolocation" in navigator) {
                navigator.geolocation.getCurrentPosition(function (position) {
                    console.log(position);
                });
            }
        },
        init: function () {
            this.getCurrentPosition();
        }
    };

    return {
        init: function () {
            select2.init();
            description.init();
            events.init();
        }
    };
}();

$(() => {
    add.init();
});
