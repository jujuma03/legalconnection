var profile = function () {
    var cards = {
        personalInformation: {
            object: $("#portlet_personal_information"),
            update: function (formElement) {
                var name = $(formElement).find("[name='Name']").val();
                var surnames = $(formElement).find("[name='Surnames']").val();
                cards.personalInformation.object.find(".name_surnames").text(`${name} ${surnames}`);
                cards.personalInformation.object.find(".pi_houseNumber").text($(formElement).find("[name='HouseNumber']").val());
                cards.personalInformation.object.find(".pi_phoneNumber").text($(formElement).find("[name='PhoneNumber']").val());
                cards.personalInformation.object.find(".pi_dni").text($(formElement).find("[name='DNI']").val());
                cards.personalInformation.object.find(".pi_birthDate").text($(formElement).find("[name='BirthDate']").val());
                cards.personalInformation.object.find(".pi_sex").text($(formElement).find("[name='Sex'] option:selected").text());
                cards.personalInformation.object.find(".pi_department").text($(formElement).find("[name='DepartmentId'] option:selected").text());
                cards.personalInformation.object.find(".pi_province").text($(formElement).find("[name='ProvinceId'] option:selected").text());
                cards.personalInformation.object.find(".pi_district").text($(formElement).find("[name='DistrictId'] option:selected").text());
            },
            modal: {
                object: $("#personal_information_modal"),
                form: {
                    object: $("#personal_information_form").validate({
                        submitHandler: function (formElement, e) {
                            e.preventDefault();
                            var $btn = $(formElement).find("button[type='submit']");
                            $btn.addClass("m-loader m-loader--right m-loader--light").attr("disabled", true);
                            var formData = new FormData(formElement);
                            cards.personalInformation.modal.object.find(":input").attr("disabled", true);
                            $.ajax({
                                url: "/perfil/actualizar-informacion-personal",
                                type: "POST",
                                data: formData,
                                contentType: false,
                                processData: false
                            })
                                .done(function (e) {
                                    cards.personalInformation.update(formElement);
                                    cards.personalInformation.modal.object.modal("hide");
                                    swal({
                                        type: "success",
                                        allowOutsideClick: false,
                                        title: "Éxito",
                                        text: "Datos actualizados satisfactoriamente.",
                                        confirmButtonText: "Aceptar"
                                    });
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
                                    cards.personalInformation.modal.object.find(":input").attr("disabled", false);
                                });
                        }
                    })
                },
                load: function () {
                    var $page = cards.personalInformation.modal.object;
                    $page.find(":input").attr("disabled", true);
                    $.ajax({
                        url: "/perfil/get-informacion-personal",
                        type: "GET"
                    })
                        .done(function (e) {
                            cards.personalInformation.modal.object.find("[name='Name']").val(e.name);
                            cards.personalInformation.modal.object.find("[name='Surnames']").val(e.surnames);
                            cards.personalInformation.modal.object.find("[name='HouseNumber']").val(e.houseNumber);
                            cards.personalInformation.modal.object.find("[name='PhoneNumber']").val(e.phoneNumber);
                            cards.personalInformation.modal.object.find("[name='DNI']").val(e.dni);
                            cards.personalInformation.modal.object.find("[name='BirthDate']").val(e.birthDate);
                            cards.personalInformation.modal.object.find("[name='Sex']").val(e.sex).trigger("change");
                            select2.department.setValue(e.departmentId, e.department);
                            select2.province.setValue(e.provinceId, e.province);
                            select2.district.setValue(e.districtId, e.district);
                            $page.find(":input").attr("disabled", false);
                        });
                },
                onShow: function () {
                    cards.personalInformation.modal.object.on('show.bs.modal', function (e) {
                        cards.personalInformation.modal.load();
                    });
                },
                init: function () {
                    this.onShow();
                }
            },
            init: function () {
                this.modal.init();
            }
        },
        init: function () {
            this.personalInformation.init();
        }
    };

    var select2 = {
        sex: {
            init: function () {
                $("#Sex").select2({
                    dropdownParent: cards.personalInformation.modal.object,
                    minimumResultsForSearch: -1
                });
            }
        },
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
                    dropdownParent: cards.personalInformation.modal.object,
                    minimumInputLength: 0,
                    placeholder: 'Seleccione departamento',
                    allowClear: false
                });
            },
            setValue: function (value, text) {
                if ($('#DepartmentId').find("option[value='" + value + "']").length) {
                    $('#DepartmentId').val(value).trigger('change');
                } else {
                    var newOption = new Option(text, value, true, true);
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
                    dropdownParent: cards.personalInformation.modal.object,
                    minimumInputLength: 0,
                    placeholder: 'Seleccione provincia',
                    allowClear: true
                });
            },
            setValue: function (value, text) {
                if ($('#ProvinceId').find("option[value='" + value + "']").length) {
                    $('#ProvinceId').val(value).trigger('change');
                } else {
                    var newOption = new Option(text, value, true, true);
                    $('#ProvinceId').append(newOption).trigger('change');
                }
            },
            clear: function () {
                $("#ProvinceId").val(null).trigger("change");
            },
            events: {
                onChange: function () {
                    $("#ProvinceId").on("change", function () {
                        select2.district.clear();
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
        district: {
            load: function (provinceId) {
                $("#DistrictId").select2({
                    ajax: {
                        url: "/distritos/get/v2",
                        delay: 500,
                        data: function (params) {
                            var query = {
                                provinceId: $("#ProvinceId").val(),
                                page: params.page || 1,
                                term: params.term
                            };
                            return query;
                        }
                    },
                    dropdownParent: cards.personalInformation.modal.object,
                    minimumInputLength: 0,
                    placeholder: 'Seleccione distrito',
                    allowClear: true
                });
            },
            setValue: function (value, text) {
                if ($('#DistrictId').find("option[value='" + value + "']").length) {
                    $('#DistrictId').val(value).trigger('change');
                } else {
                    var newOption = new Option(text, value, true, true);
                    $('#DistrictId').append(newOption).trigger('change');
                }
            },
            clear: function () {
                $("#DistrictId").val(null).trigger("change");
            },
            init: function () {
                this.load();
            }
        },
        init: function () {
            select2.sex.init();
            select2.department.init();
            select2.province.init();
            select2.district.init();
        }
    };

    var datepicker = {
        init: function () {
            var currentDate = new Date("1990-01-01");  
            $(".datepicker_input").datepicker({
                endDate: '+0d',
                clearBtn: false,
                orientation: "bottom",
                setDate: currentDate
            });
        }
    };
    var changepasswordform = {
        form: {
            object: $("#change_password_form").validate({
                rules: {
                    ConfirmPassword: {
                        equalTo: '[name="NewPassword"]'
                    }
                },
                submitHandler: function (formElement, e) {
                    e.preventDefault();
                    var $btn = $(formElement).find("button[type='submit']");
                    $btn.addLoader();
                    var formData = new FormData(formElement);
                    $("#change_password_form").find(":input").attr("disabled", true);
                    $.ajax({
                        url: "/perfil/cambiar-password",
                        type: "POST",
                        data: formData,
                        contentType: false,
                        processData: false
                    })
                        .done(function (e) {
                            swal({
                                type: "success",
                                allowOutsideClick: false,
                                title: "Éxito",
                                text: "Contraseña actualizada satisfactoriamente.",
                                confirmButtonText: "Aceptar"
                            },
                                function () {
                                    window.location.reload();
                                });
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
                            $btn.removeLoader();
                            $("#change_password_form").find(":input").attr("disabled", false);
                        });
                }
            })
        },
    }
    return {
        init: function () {
            select2.init();
            cards.init();
            datepicker.init();
        }
    };
}();

$(() => {
    profile.init();
});