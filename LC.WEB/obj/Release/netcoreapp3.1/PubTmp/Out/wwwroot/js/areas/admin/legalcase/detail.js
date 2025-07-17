var detail = function () {
    var LegalCaseId = $("#Id").val();
    var events = {
        onAcceptLegalCase: function () {
            $("#accept_legal_case").on("click", function () {
                swal({
                    title: '¿Está seguro?',
                    text: "El caso será confirmado y una vez derivado habilitado para recibir ofertas.",
                    type: 'warning',
                    showCancelButton: true,
                    confirmButtonText: 'Sí, confirmar',
                    cancelButtonText: 'Cancelar',
                    showLoaderOnConfirm: true,
                    allowOutsideClick: () => !swal.isLoading(),
                    preConfirm: () => {
                        return new Promise((resolve) => {
                            $.ajax({
                                url: `/admin/casos-legales/${LegalCaseId}/aceptar`,
                                type: "POST"
                            })
                                .done(function () {
                                    window.location.reload();
                                });
                        });
                    }
                });
            });
        },
        onDeriveLegalCase: function () {
            $("#derive_legal_case").on("click", function () {
                swal({
                    title: '¿Está seguro?',
                    text: "El caso será enviado a todos los abogados relacionados.",
                    type: 'warning',
                    showCancelButton: true,
                    confirmButtonText: 'Sí, enviar',
                    cancelButtonText: 'Cancelar',
                    showLoaderOnConfirm: true,
                    allowOutsideClick: () => !swal.isLoading(),
                    preConfirm: () => {
                        return new Promise((resolve) => {
                            $.ajax({
                                url: `/admin/casos-legales/${LegalCaseId}/derivar`,
                                type: "POST"
                            })
                                .done(function () {
                                    window.location.href = `/admin/casos-legales/${LegalCaseId}/detalles`;
                                });
                        });
                    }
                });
            });
        },
        init: function () {
            this.onAcceptLegalCase();
            this.onDeriveLegalCase();
        }
    };

    var modal = {
        reject: {
            object: $("#reject_modal"),
            form: {
                object: $("#reject_form").validate({
                    submitHandler: function (formElement, e) {
                        e.preventDefault();
                        var $btn = $(formElement).find("button[type='submit']");
                        $btn.addClass("m-loader m-loader--right m-loader--light").attr("disabled", true);
                        var formData = new FormData(formElement);
                        formData.append("LegalCaseId", LegalCaseId);
                        modal.reject.object.find(":input").attr("disabled", true);
                        $.ajax({
                            url: "/admin/casos-legales/rechazar",
                            type: "POST",
                            data: formData,
                            contentType: false,
                            processData: false
                        })
                            .done(function (e) {
                                modal.reject.object.modal("hide");
                                window.location.reload();
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
                                modal.reject.object.find(":input").attr("disabled", false);
                            });
                    }
                })
            }
        },
        observation: {
            object: $("#observation_modal"),
            events: {
                onShow: function () {
                    console.log("asdasd");
                    $("#view_observations").click(function () {
                        console.log("asdasd");
                        modal.observation.object.modal("show");
                        modal.observation.object.find(":input").attr("disabled", true);
                        $.ajax({
                            url: `/admin/casos-legales/get-observacion?legalCaseId=${LegalCaseId}`,
                            type: "GET"
                        })
                            .done(function (e) {
                                modal.observation.object.find("[name='Observation']").val(e);
                                modal.observation.object.find(":input").attr("disabled", false);
                            });
                    });
                },
                init: function () {
                    this.onShow();
                }
            },
            init: function () {
                this.events.init();
            }
        },
        edit: {
            object: $("#edit_legalCase_modal"),
            events: {
                onShow: function () {

                    $(".btn-edit").click(function () {

                        modal.edit.object.modal("show");
                        modal.edit.object.find(":input").attr("disabled", true);
                        $.ajax({
                            url: `/mis-casos/get-caso-detalles?legalCaseId=${LegalCaseId}`,
                            type: "GET"
                        })
                            .done(function (e) {
                                modal.edit.object.find("[name='Description']").val(e.description);
                                modal.edit.object.find("[name='Description']").attr('maxlength', e.descriptionMaxLength);
                                select.speciality.setValue(e.speciality, e.specialityName);
                                select.specialityTheme.setValues(e.themes);
                                select.department.setValue(e.ubigeo.departmentId, e.ubigeo.department);
                                select.province.setValue(e.ubigeo.provinceId, e.ubigeo.province);
                                modal.edit.object.find(":input").attr("disabled", false);
                            });
                    });
                },
                form: {
                    object: $("#edit_legalCase_form").validate({
                        //rules: {
                        //    Description: {
                        //        maxlength: 240
                        //    }
                        //},
                        submitHandler: function (formElement, e) {
                            e.preventDefault();
                            var $btn = $(formElement).find("button[type='submit']");
                            $btn.addClass("m-loader m-loader--right m-loader--light").attr("disabled", true);
                            var formData = new FormData(formElement);
                            formData.append("Id", LegalCaseId);
                            modal.edit.object.find(":input").attr("disabled", true);
                            $.ajax({
                                url: "/mis-casos/editar-caso",
                                type: "POST",
                                data: formData,
                                contentType: false,
                                processData: false
                            })
                                .done(function (e) {
                                    modal.edit.object.modal("hide");
                                    swal({
                                        type: "success",
                                        title: "Completado",
                                        text: "Caso Editado Satisfactoriamente.",
                                        confirmButtonText: "Aceptar",
                                        allowOutsideClick: false
                                    }).then(function (result) {
                                        if (result.value) {
                                            window.location.reload();
                                        }
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
                                    modal.edit.object.find(":input").attr("disabled", false);
                                });
                        }
                    })
                },
                init: function () {
                    this.onShow();
                }
            },
            init: function () {
                this.events.init();
            }
        },
        init: function () {
            this.edit.init();
        }
    };

    var select = {
        speciality: {
            load: function () {
                $("#SpecialitySelected").select2({
                    ajax: {
                        url: "/especialidades/get/v2",
                        delay: 500,
                        data: function (params) {
                            var query = {
                                page: params.page || 1,
                                term: params.term,
                                colloquialName: true
                            };
                            return query;
                        }
                    },
                    dropdownParent: modal.edit.object,
                    minimumInputLength: 0,
                    placeholder: 'Seleccione tema de especialidad',
                    allowClear: true
                });
                $("#SpecialitySelected").on("change", function () {
                    $('#SpecialityThemeId').select2('data', { id: null, text: null });
                    $('#SpecialityThemeId').val(null).trigger("change");
                });
            },
            setValue: function (id, text) {
                if ($('#SpecialitySelected').find("option[value='" + id + "']").length) {
                    $('#SpecialitySelected').val(id).trigger('change');
                } else {
                    var newOption = new Option(text, id, true, true);
                    $('#SpecialitySelected').append(newOption).trigger('change');
                }
            },
            init: function () {
                this.load();
            }
        },
        specialityTheme: {
            load: function () {
                $("#SpecialityThemeId").select2({
                    ajax: {
                        url: "/especialidades-temas/get/v2",
                        delay: 500,
                        data: function (params) {
                            var query = {
                                page: params.page || 1,
                                term: params.term,
                                colloquialName: true,
                                specialityId: $("#SpecialitySelected").val()
                            };
                            return query;
                        }
                    },
                    dropdownParent: modal.edit.object,
                    minimumInputLength: 0,
                    placeholder: 'Seleccione tema de especialidad',
                    allowClear: true
                });
            },
            setValues: function (object) {
                var selected = [];
                $.each(object, function (i, v) {
                    if ($('#SpecialityThemeId').find("option[value='" + v.id + "']").length) {
                        selected.push(v.id);
                    } else {
                        var newOption = new Option(v.text, v.id, true, true);
                        $('#SpecialityThemeId').append(newOption);
                        selected.push(v.id);
                    }
                });
                $('#SpecialityThemeId').val(selected).trigger("change");
            },
            init: function () {
                this.load();
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
                    minimumInputLength: 0,
                    dropdownParent: modal.edit.object,
                    placeholder: 'Seleccione departamento',
                    allowClear: false
                });
            },
            setValue: function (id, text) {
                if ($('#DepartmentId').find("option[value='" + id + "']").length) {
                    $('#DepartmentId').val(id).trigger('change');
                } else {
                    var newOption = new Option(text, id, true, true);
                    $('#DepartmentId').append(newOption).trigger('change');
                }
            },
            init: function () {
                this.load();
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
                    dropdownParent: modal.edit.object,
                    placeholder: 'Seleccione provincia',
                    allowClear: true
                });
            },
            setValue: function (id, text) {
                if ($('#ProvinceId').find("option[value='" + id + "']").length) {
                    $('#ProvinceId').val(id).trigger('change');
                } else {
                    var newOption = new Option(text, id, true, true);
                    $('#ProvinceId').append(newOption).trigger('change');
                }
            },
            init: function () {
                this.load();
            }
        },
        init: function () {
            this.speciality.init();
            this.specialityTheme.init();
            this.department.init();
            this.province.init();
        }
    };
    var description = {
        init: function () {
            $('#Description').maxlength({
                alwaysShow: true,
                threshold: 5,
                appendToParent: true,
                warningClass: "m-badge m-badge--primary m-badge--rounded m-badge--wide",
                limitReachedClass: "m-badge m-badge--brand m-badge--rounded m-badge--wide",
            });
        }
    };
    return {
        init: function () {
            select.init();
            events.init();
            modal.init();
            description.init();
        }
    };
}();

$(() => {
    detail.init();
});