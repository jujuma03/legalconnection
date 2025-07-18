var detail = function () {

    var LegalCaseId = $("#LegalCaseId").val();

    var lawyers = {
        applicants: {
            load: function () {
                $.ajax({
                    url: `/mis-casos/get-postulaciones?legalCaseId=${LegalCaseId}`,
                    dataType: "HTML",
                    type: "GET"
                })
                    .done(function (e) {
                        $("#partial_applicants").html(e);
                    });
            },
            init: function () {
                this.load();
            }
        },
        accepted: {
            load: function () {
                $.ajax({
                    url: `/mis-casos/get-abogados-aceptados?legalCaseId=${LegalCaseId}`,
                    dataType: "HTML",
                    type: "GET"
                })
                    .done(function (e) {
                        $("#partial_lawyers").html(e);
                    });
            },
            init: function () {
                this.load();
            }
        },
        init: function () {
            lawyers.applicants.init();
            lawyers.accepted.init();
        }
    };

    var events = {
        acceptLawyer: function () {
            $("#partial_applicants").on("click", ".accept_applicant ", function () {
                var $btn = $(this);
                var lawyerId = $(this).data("id");
                var formData = new FormData();
                formData.append("LawyerId", lawyerId);
                formData.append("LegalCaseId", LegalCaseId);
                $btn.addLoader();
                $.ajax({
                    url: `/mis-casos/seleccionar-abogado`,
                    data: formData,
                    type: "POST",
                    contentType: false,
                    processData: false
                })
                    .done(function (e) {
                        events.updateStatusStr(e);
                        lawyers.applicants.load();
                        lawyers.accepted.load();
                        swal({
                            type: "success",
                            allowOutsideClick: false,
                            //title: "Éxito!",
                            text: "AL CONFIRMAR LA SELECCIÓN ESTAS APERTURANDO EL CASO CON EL ABOGADO.",
                            confirmButtonText: "Entendido"
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
                    });
            });
        },
        removeLawyer: function () {
            $("#partial_lawyers").on("click", ".delete_lawyer", function () {
                var $btn = $(this);
                var lawyerId = $(this).data("id");
                var formData = new FormData();
                formData.append("LawyerId", lawyerId);
                formData.append("LegalCaseId", LegalCaseId);

                $btn.addLoader();
                $.ajax({
                    url: `/mis-casos/remover-abogado`,
                    data: formData,
                    type: "POST",
                    contentType: false,
                    processData: false
                })
                    .done(function (e) {
                        events.updateStatusStr(e);
                        lawyers.applicants.load();
                        lawyers.accepted.load();
                        swal({
                            type: "success",
                            allowOutsideClick: false,
                            title: "Éxito!",
                            text: "EL ABOGADO HA SIDO REMOVIDO DEL CASO.",
                            confirmButtonText: "Entendido"
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
                    });
            });
        },
        processPayment: function () {
            $("#partial_lawyers").on("click", ".proccess_payment", function (e) {
                var $btn = $(this);
                var lawyerId = $(this).data("id");
                $btn.addLoader();
                $.ajax({
                    url: `/get-precio-consulta?lawyerId=${lawyerId}&legalCaseId=${LegalCaseId}`,
                    type: "GET"
                })
                    .done(function (e) {
                        var url = `/mis-casos/procesar-pago`;
                        var formData = new FormData();
                        formData.append("LawyerId", lawyerId);
                        formData.append("LegalCaseId", LegalCaseId);
                        ProcessOnlinePayment(e, url, formData);
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
                    });
            });
            $("#partial_lawyers").on("click", ".proccess_lawyer", function (e) {
               var $btn = $(this);
                var lawyerId = $(this).data("id");
                $btn.addLoader();
                swal({
                    title: "¿Está seguro?",
                    text: "¿Desea seleccionar este abogado?",
                    type: "info",
                    showCancelButton: true,
                    confirmButtonText: "Si, seleccionar",
                    confirmButtonClass: "btn btn-danger m-btn m-btn--custom",
                    cancelButtonText: "Cancelar"
                }).then(function (result) {
                    var url = `/mis-casos/procesar-pago`;
                    var formData = new FormData();
                    formData.append("LawyerId", lawyerId);
                    formData.append("LegalCaseId", LegalCaseId);
                    formData.append("IsFreeFee", true);
                    $.ajax({
                        url: url,
                        data: formData,
                        type: "POST",
                        contentType: false,
                        processData: false
                    })
                        .done(function (e) {
                            swal({
                                type: "success",
                                allowOutsideClick: false,
                                title: "Éxito!",
                                text: "EL ABGADO HA SIDO SELECCIONADO CON ÉXITO.",
                                confirmButtonText: "Entendido"
                            }).then((result) => {
                                if (result) {
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
                        });
                });
            });
        },
        getRemainingTime: function () {
            $.ajax({
                url: `/mis-casos/get-segundos-restantes?legalCaseId=${LegalCaseId}`,
                type: "GET"
            })
                .done(function (e) {
                    if (e !== null && e !== undefined) {
                        simplyCountdown('#remaining_time', {
                            year: e.year, // required
                            month: e.month, // required
                            day: e.day, // required
                            hours: e.hour, // Default is 0 [0-23] integer
                            minutes: e.minutes, // Default is 0 [0-59] integer
                            seconds: e.seconds, // Default is 0 [0-59] integer
                            words: { //words displayed into the countdown
                                days: 'día',
                                hours: 'hora',
                                minutes: 'minuto',
                                seconds: 'segundo',
                                pluralLetter: 's'
                            },
                            plural: true, //use plurals
                            inline: true, //set to true to get an inline basic countdown like : 24 days, 4 hours, 2 minutes, 5 seconds
                            inlineClass: 'simply-countdown-inline', //inline css span class in case of inline = true
                            // in case of inline set to false
                            enableUtc: false, //Use UTC as default
                            onEnd: function () { return; }, //Callback on countdown end, put your own function here
                            refresh: 1000, // default refresh every 1s
                            sectionClass: 'simply-section', //section css class
                            amountClass: 'simply-amount', // amount css class
                            wordClass: 'simply-word', // word css class
                            zeroPad: true,
                            countUp: false,
                        });
                    }
                });
        },
        updateStatusStr: function (e) {
            $("#legal_case_status").text(e);
        },
        requestReview: function () {
            $("#request_review").on("click", function () {
                $(this).addLoader();
                $.ajax({
                    type: "POST",
                    url: `/mis-casos/solicitar-revision?legalCaseId=${LegalCaseId}`
                })
                    .done(function (e) {
                        window.location.reload();
                    });
            });
        },
        onEdit: function () {
            $("#edit_legalcase_btn").click(function () {
                modal.legalCaseEdit.show();
            });
        },
        onShowLawyerInfo: function () {
            $("#partial_lawyers").on("click", ".contact_lawyer", function () {
                $(this).addLoader();
                var lawyerId = $(this).data("id");
                modal.lawyerInfo.show(lawyerId);
            });
        },
        onDeleteLegalCase: function () {
            $("#delete_legalcase_btn").on("click", function () {
                var $btn = $(this);
                swal({
                    title: '¿Está seguro?',
                    text: "El caso será eliminado. Este proceso será irreversible.",
                    type: 'warning',
                    showCancelButton: true,
                    confirmButtonText: 'Sí, eliminar',
                    cancelButtonText: 'Cancelar',
                    showLoaderOnConfirm: true,
                    allowOutsideClick: () => !swal.isLoading(),
                    preConfirm: () => {
                        return new Promise((resolve) => {
                            $btn.addClass("m-loader m-loader--right m-loader--light").attr("disabled", true);

                            $.ajax({
                                url: `/mis-casos/eliminar-caso?legalCaseId=${LegalCaseId}`,
                                type: "POST",
                                contentType: false,
                                processData: false
                            })
                                .done(function (e) {
                                    swal({
                                        type: "success",
                                        allowOutsideClick: false,
                                        title: "Éxito",
                                        text: "Caso Eliminado.",
                                        confirmButtonText: "Aceptar"
                                    }).then((result) => {
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
                                    $btn.removeClass("m-loader m-loader--right m-loader--light").attr("disabled", false);
                                    modal.reject.object.find(":input").attr("disabled", false);
                                });
                        });
                    }
                });
            });
        },
        init: function () {
            this.acceptLawyer();
            this.removeLawyer();
            this.processPayment();
            this.getRemainingTime();
            this.requestReview();
            this.onEdit();
            this.onShowLawyerInfo();
            this.onDeleteLegalCase();
        }
    };

    var modal = {
        legalCaseEdit: {
            object: $("#edit_legalCase_modal"),
            show: function () {
                this.object.modal("show");
                this.object.find(":input").attr("disabled", true);
                $.ajax({
                    url: `/mis-casos/get-caso-detalles?legalCaseId=${LegalCaseId}`,
                    type: "GET"
                })
                    .done(function (e) {
                        modal.legalCaseEdit.object.find("[name='Description']").val(e.description);
                        select.speciality.setValue(e.speciality, e.specialityName);
                        select.specialityTheme.setValues(e.themes);
                        select.department.setValue(e.ubigeo.departmentId, e.ubigeo.department);
                        select.province.setValue(e.ubigeo.provinceId, e.ubigeo.province);
                        modal.legalCaseEdit.object.find(":input").attr("disabled", false);
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
                        modal.legalCaseEdit.object.find(":input").attr("disabled", true);
                        $.ajax({
                            url: "/mis-casos/editar-caso",
                            type: "POST",
                            data: formData,
                            contentType: false,
                            processData: false
                        })
                            .done(function (e) {
                                modal.legalCaseEdit.object.modal("hide");
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
                                modal.legalCaseEdit.object.find(":input").attr("disabled", false);
                            });
                    }
                })
            }
        },
        lawyerInfo: {
            object: $("#lawyerinfo_modal"),
            show: function (lawyerId) {
                $.ajax({
                    url: `/mis-casos/get-contacto-abogado?lawyerId=${lawyerId}&legalCaseId=${LegalCaseId}`,
                    type: "GET"
                })
                    .done(function (e) {
                        modal.lawyerInfo.object.modal("show");
                        modal.lawyerInfo.object.find(".name").text(e.name);
                        modal.lawyerInfo.object.find(".email").text(e.email);
                        modal.lawyerInfo.object.find(".phoneNumber").text(e.phoneNumber);
                        modal.lawyerInfo.object.find(".dni").text(e.dni);
                        modal.lawyerInfo.object.find(".houseNumber").text(e.houseNumber);
                    })
                    .fail(function (e) {
                        swal({
                            type: "error",
                            title: "Error",
                            text: e.status === 502 ? "No hay respuesta del servidor" : e.responseText,
                            confirmButtonText: "Aceptar"
                        });
                    })
                    .always(function () {
                        $("#partial_lawyers").find(".contact_lawyer").removeLoader();
                    });
            }
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
                    dropdownParent: modal.legalCaseEdit.object,
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
                    dropdownParent: modal.legalCaseEdit.object,
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
                    dropdownParent: modal.legalCaseEdit.object,
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
                    dropdownParent: modal.legalCaseEdit.object,
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
            lawyers.init();
            events.init();
            description.init();
        }
    };
}();

$(() => {
    detail.init();
});