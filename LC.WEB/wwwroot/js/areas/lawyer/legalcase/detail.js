var detail = function () {

    var LegalCaseId = $("#LegalCaseId").val();

    var legalCases = {
        object: $("#main_portlet_legal_cases")
    };

    var modal = {
        postulate: {
            object: $("#postulate_modal"),
            form: {
                object: $("#postulate_form").validate({
                    submitHandler: function (formElement, e) {
                        e.preventDefault();
                        var $btn = $(formElement).find(".postulate_legal_case");
                        $btn.addClass("m-loader m-loader--right m-loader--light").attr("disabled", true);
                        var formData = new FormData(formElement);
                        modal.postulate.object.find(":input").attr("disabled", true);
                        $.ajax({
                            url: "/abogado/casos/postular-caso",
                            type: "POST",
                            data: formData,
                            contentType: false,
                            processData: false
                        })
                            .done(function (e) {
                                modal.postulate.object.modal("hide");
                                window.location.reload();
                                //swal({
                                //    type: "success",
                                //    allowOutsideClick: false,
                                //    title: "Éxito",
                                //    text: "Postulación enviada satisfactoriamente.",
                                //    confirmButtonText: "Aceptar"
                                //});
                            })
                            .fail(function (e) {
                                swal({
                                    type: "error",
                                    title: "Error al postular.",
                                    text: e.status === 502 ? "No hay respuesta del servidor" : e.responseText,
                                    confirmButtonText: "Aceptar"
                                });
                            })
                            .always(function () {
                                $btn.removeClass("m-loader m-loader--right m-loader--light").attr("disabled", false);
                                modal.postulate.object.find(":input").attr("disabled", false);
                            });
                    }
                })
            },
            events: {
                show: function (legalCaseId) {
                    modal.postulate.object.modal("show");
                    modal.postulate.object.find("[name='LegalCaseId']").val(legalCaseId);
                },
                onHidden: function () {
                    modal.postulate.object.on('hidden.bs.modal', function (e) {
                        modal.postulate.form.object.resetForm();
                    });
                },
                init: function () {
                    this.onHidden();
                }
            },
            init: function () {
                this.events.init();
            }
        },
        acceptCase: {
            object: $("#accept_modal"),
            form: {
                object: $("#accept_form").validate({
                    submitHandler: function (formElement, e) {
                        e.preventDefault();
                        var $btn = $(formElement).find("button[type='submit']");
                        $btn.addClass("m-loader m-loader--right m-loader--light").attr("disabled", true);
                        var formData = new FormData(formElement);
                        modal.postulate.object.find(":input").attr("disabled", true);
                        $.ajax({
                            url: "/abogado/casos/aceptar-caso",
                            type: "POST",
                            data: formData,
                            contentType: false,
                            processData: false
                        })
                            .done(function (e) {
                                window.location.reload();
                            })
                            .fail(function (e) {
                                swal({
                                    type: "error",
                                    title: "Error al acecptar caso.",
                                    text: e.status === 502 ? "No hay respuesta del servidor" : e.responseText,
                                    confirmButtonText: "Aceptar"
                                });
                            })
                            .always(function () {
                                $btn.removeClass("m-loader m-loader--right m-loader--light").attr("disabled", false);
                                modal.acceptCase.object.find(":input").attr("disabled", false);
                            });
                    }
                })
            },
            events: {
                show: function (legalCaseId) {
                    modal.acceptCase.object.modal("show");
                    modal.acceptCase.object.find("[name='LegalCaseId']").val(legalCaseId);
                },
                onHidden: function () {
                    modal.acceptCase.object.on('hidden.bs.modal', function (e) {
                        modal.acceptCase.form.object.resetForm();
                    });
                },
                init: function () {
                    this.onHidden();
                }
            },
            init: function () {
                this.events.init();
            }
        },
        reportAbandonment: {
            object: $("#report_abandonment_modal"),
            form: {
                object: $("#report_abandonment_form").validate({
                    submitHandler: function (formElement, e) {
                        e.preventDefault();
                        var $btn = $(formElement).find("button[type='submit']");
                        $btn.addClass("m-loader m-loader--right m-loader--light").attr("disabled", true);
                        var formData = new FormData(formElement);
                        modal.reportAbandonment.object.find(":input").attr("disabled", true);
                        $.ajax({
                            url: "/abogado/casos/reportar-abandono",
                            type: "POST",
                            data: formData,
                            contentType: false,
                            processData: false
                        })
                            .done(function (e) {
                                modal.reportAbandonment.object.modal("hide");
                                swal({
                                    type: "success",
                                    allowOutsideClick: false,
                                    title: "Éxito",
                                    text: "Reporte Enviado. Los asesores revisarán el sustento lo más pronto posible.",
                                    confirmButtonText: "Aceptar"
                                }).then((result) => {
                                    window.location.reload();
                                });
                            })
                            .fail(function (e) {
                                swal({
                                    type: "error",
                                    title: "Error al enviar el reporte.",
                                    text: e.status === 502 ? "No hay respuesta del servidor" : e.responseText,
                                    confirmButtonText: "Aceptar"
                                });
                            })
                            .always(function () {
                                $btn.removeClass("m-loader m-loader--right m-loader--light").attr("disabled", false);
                                modal.reportAbandonment.object.find(":input").attr("disabled", false);
                            });
                    }
                })
            },
            events: {
                show: function (legalCaseId) {
                    modal.reportAbandonment.object.modal("show");
                    modal.reportAbandonment.object.find("[name='LegalCaseId']").val(legalCaseId);
                },
                onHidden: function () {
                    modal.reportAbandonment.object.on('hidden.bs.modal', function (e) {
                        modal.reportAbandonment.form.object.resetForm();
                    });
                },
                init: function () {
                    this.onHidden();
                }
            },
            init: function () {
                this.events.init();
            }
        },
        init: function () {
            modal.postulate.init();
            modal.acceptCase.init();
            modal.reportAbandonment.init();
        }
    };

    var events = {
        postulate: function () {
            legalCases.object.on("click", ".postulate_legal_case", function () {
                var id = $(this).data("id");
                modal.postulate.events.show(id);
            });
        },
        acceptCase: function () {
            legalCases.object.on("click", ".accept_case", function () {
                var id = $(this).data("id");
                modal.acceptCase.events.show(id);
            });
        },
        closeCase: function () {
            legalCases.object.on("click", ".close_legal_case", function () {
                var $btn = $(".close_legal_case");
                $btn.addClass("m-loader m-loader--right m-loader--light").attr("disabled", true);
                var id = $(this).data("id");
                var formData = new FormData();
                formData.append("LegalCaseId", id);
                swal({
                    title: "¿Está seguro?",
                    text: "¿Desea concluir el caso?",
                    type: "info",
                    showCancelButton: true,
                    confirmButtonText: "Si, concluir",
                    confirmButtonClass: "btn btn-danger m-btn m-btn--custom",
                    cancelButtonText: "Cancelar"
                }).then(function (result) {
                    $.ajax({
                        url: "/abogado/casos/cerrar-caso",
                        type: "POST",
                        data: formData,
                        contentType: false,
                        processData: false
                    })
                        .done(function (e) {
                            swal({
                                type: "success",
                                allowOutsideClick: false,
                                title: "Caso Concluido",
                                text: "Recuerda que para obtener mayor visibilidad, solicita a tu cliente que te califique en la plataforma.",
                                confirmButtonText: "Aceptar"
                            }).then((result) => {
                                window.location.reload();
                            });
                        })
                        .fail(function (e) {
                            swal({
                                type: "error",
                                title: "Error al finalizar el caso.",
                                text: e.status === 502 ? "No hay respuesta del servidor" : e.responseText,
                                confirmButtonText: "Aceptar"
                            });
                        })
                        .always(function () {
                            $btn.removeClass("m-loader m-loader--right m-loader--light").attr("disabled", false);
                        });
                });
                $btn.removeClass("m-loader m-loader--right m-loader--light").attr("disabled", false);
            });
        },
        filedCase: function () {
            legalCases.object.on("click", ".filed_legal_case", function () {
                var id = $(this).data("id");
                var $btn = $(this);
                $btn.addClass("m-loader m-loader--right m-loader--light").attr("disabled", true);
                $.ajax({
                    url: `/abogado/casos/archivar-caso?legalCaseId=${id}`,
                    type: "POST"
                })
                    .done(function (e) {
                        swal({
                            type: "success",
                            title: "Éxito.",
                            text: "Caso archivado.",
                            confirmButtonText: "Aceptar"
                        }).then((result) => {
                            window.location.reload();
                        });
                    })
                    .fail(function (e) {
                        swal({
                            type: "error",
                            title: "Error al archivar.",
                            text: e.status === 502 ? "No hay respuesta del servidor" : e.responseText,
                            confirmButtonText: "Aceptar"
                        });
                    })
                    .always(function () {
                        $btn.removeClass("m-loader m-loader--right m-loader--light").attr("disabled", false);
                    });

            });
        },
        reportAbandonment: function () {
            legalCases.object.on("click", ".report_abandonment", function () {
                var id = $(this).data("id");
                modal.reportAbandonment.events.show(id);
            });
        },
        rejectCase: function () {
            legalCases.object.on("click", ".reject_case", function () {
                var $btn = $(".close_legal_case");
                var id = $(this).data("id");

                swal({
                    title: '¿Está seguro?',
                    text: "Rechazará el caso. Este proceso es irreversible.",
                    type: 'warning',
                    showCancelButton: true,
                    confirmButtonText: 'Sí, rechazar',
                    cancelButtonText: 'Cancelar',
                    showLoaderOnConfirm: true,
                    allowOutsideClick: () => !swal.isLoading(),
                    preConfirm: () => {
                        return new Promise((resolve) => {
                            $btn.addClass("m-loader m-loader--right m-loader--light").attr("disabled", true);
                            $.ajax({
                                url: `/abogado/casos/rechazar-caso?legalCaseId=${id}`,
                                type: "POST",
                                contentType: false,
                                processData: false
                            })
                                .done(function (e) {
                                    swal({
                                        type: "success",
                                        allowOutsideClick: false,
                                        title: "Caso Rechazado",
                                        text: "El caso cambiará de estado.",
                                        confirmButtonText: "Aceptar"
                                    }).then((result) => {
                                        window.location.reload();
                                    });
                                })
                                .fail(function (e) {
                                    swal({
                                        type: "error",
                                        title: "Error al rechazar el caso.",
                                        text: e.status === 502 ? "No hay respuesta del servidor" : e.responseText,
                                        confirmButtonText: "Aceptar"
                                    });
                                })
                                .always(function () {
                                    $btn.removeClass("m-loader m-loader--right m-loader--light").attr("disabled", false);
                                });
                        });
                    }
                });
            });
        },
        init: function () {
            this.postulate();
            this.acceptCase();
            this.closeCase();
            this.filedCase();
            this.reportAbandonment();
            this.rejectCase();
        }
    };

    var select2 = {
        responseTime: function () {
            $("#ResponseTime").select2({
                placeholder: "Seleccionar tiempo",
                minimumResultsForSearch: -1
            });
        },
        init: function () {
            this.responseTime();
        }
    };

    return {
        init: function () {
            modal.init();
            events.init();
            select2.init();
        }
    };
}();

$(() => {
    detail.init();
});