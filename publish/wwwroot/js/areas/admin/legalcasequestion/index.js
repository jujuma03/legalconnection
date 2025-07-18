var initApp = function () {
    var datatable = {
        questions: {
            object: null,
            options: {
                ajax: {
                    url: "/admin/preguntas-casos/get-preguntas",
                    type: "GET",
                    data: function (data) {
                        data.searchValue = $("#search").val();
                    }
                },
                columns: [
                    {
                        title: "Descripción",
                        data: "description",
                        orderable : false
                    },
                    {
                        title: "Opciones",
                        data: null,
                        orderable: false,
                        render: function (row) {
                            var tpm = "";
                            tpm += `<button data-id='${row.id}' class="btn btn-primary btn-edit m-btn m-btn--icon btn-sm m-btn--icon-only"><i class="la la-edit"></i></button>`;
                            tpm += `<button data-id='${row.id}' class="ml-1 btn btn-danger btn-delete m-btn m-btn--icon btn-sm m-btn--icon-only"><i class="la la-trash"></i></button>`;
                            return tpm;
                        }
                    }
                ]
            },
            reload: function () {
                this.object.ajax.reload();
            },
            events: {
                onEdit: function () {
                    datatable.questions.object.on("click", ".btn-edit", function () {
                        var id = $(this).data("id");
                        modal.edit.show(id);
                    });
                },
                onDelete: function () {
                    datatable.questions.object.on("click", ".btn-delete", function () {
                        var id = $(this).data("id");
                        swal({
                            title: '¿Está seguro?',
                            text: "El registro será eliminado.",
                            type: 'warning',
                            showCancelButton: true,
                            confirmButtonText: 'Sí, eliminar',
                            cancelButtonText: 'Cancelar',
                            showLoaderOnConfirm: true,
                            allowOutsideClick: () => !swal.isLoading(),
                            preConfirm: () => {
                                return new Promise((resolve) => {
                                    $.ajax({
                                        url: `/admin/preguntas-casos/eliminar?id=${id}`,
                                        type: "POST"
                                    })
                                        .done(function () {
                                            datatable.questions.reload();
                                            swal({
                                                type: "success",
                                                title: "Éxito.",
                                                text: "Registro eliminado satisfactoriamente.",
                                                confirmButtonText: "Aceptar"
                                            });
                                        }).
                                        fail(function (e) {
                                            swal({
                                                type: "error",
                                                title: "Error al guardar los datos.",
                                                text: e.status === 502 ? "No hay respuesta del servidor" : e.responseText,
                                                confirmButtonText: "Aceptar"
                                            });
                                        });
                                });
                            }
                        });
                    });
                },
                init: function () {
                    this.onDelete();
                    this.onEdit();
                }
            },
            init: function () {
                this.object = $("#legal_case_questions_datatable").DataTable(this.options);
                this.events.init();
            }
        },
        init: function () {
            this.questions.init();
        }
    };

    var events = {
        onAdd: function () {
            $("#add_question").click(function () {
                modal.add.show();
            });
        },
        onSearch: function () {
            $("#search").doneTyping(function () {
                datatable.questions.reload();
            });
        },
        init: function () {
            this.onAdd();
        }
    };

    var modal = {
        object: $("#legal_case_question_modal"),
        form: {
            object: $("#legal_case_question_form").validate({
                submitHandler: function (formElement, e) {
                    var $btn = $(formElement).find("button[type='submit']");
                    $btn.addClass("m-loader m-loader--right m-loader--light").attr("disabled", true);
                    var formData = new FormData(formElement);
                    modal.object.find(":input").attr("disabled", true);

                    $.ajax({
                        url: $(formElement).attr("action"),
                        type: "POST",
                        data: formData,
                        contentType: false,
                        processData: false
                    })
                        .done(function (e) {
                            modal.object.modal("hide");
                            datatable.questions.reload();
                            swal({
                                type: "success",
                                allowOutsideClick: false,
                                title: "Éxito",
                                text: "Datos guardados satisfactoriamente.",
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
                            modal.object.find(":input").attr("disabled", false);
                        });
                }
            })
        },
        add: {
            show: function () {
                modal.object.modal("show");
                $("#legal_case_question_form").attr("action", "/admin/preguntas-casos/agregar");
            }
        },
        edit: {
            show: function (id) {
                modal.object.modal("show");
                $("#legal_case_question_form").attr("action", "/admin/preguntas-casos/actualizar");
                modal.object.find(":input").attr("disabled", true);
                $.ajax({
                    url: `/admin/preguntas-casos/get?id=${id}`,
                    type: "GET"
                })
                    .done(function (e) {
                        modal.object.find("[name='Description']").val(e.description);
                        modal.object.find("[name='Id']").val(e.id);
                        modal.object.find(":input").attr("disabled", false);
                    });
            }
        },
        events: {
            onHidden: function () {
                modal.object.on('hidden.bs.modal', function (e) {
                    modal.form.object.resetForm();
                });
            },
            init: function () {
                this.onHidden();
            }
        },
        init: function () {
            this.events.init();
        }
    };

    return {
        init: function () {
            datatable.init();
            modal.init();
            events.init();
        }
    };
}();

$(() => {
    initApp.init();
});