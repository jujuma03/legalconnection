var index = function () {
    var specialityId = $("#SpecialityId").val();
    var datatable = {
        themes: {
            object: null,
            options: {
                serverSide: true,
                processing: true,
                ajax: {
                    url: "/admin/especialidades/get-temas-datatable",
                    type: "GET",
                    dataType: "JSON",
                    data: function (data) {
                        data.specialityId = specialityId;
                        data.searchValue = $("#search").val();
                    }
                },
                columns: [
                    {
                        title: "Código",
                        data: "code",
                        orderable: false
                    },
                    {
                        title: "Nombre Oficial",
                        data: "officialName",
                        orderable : false
                    },
                    {
                        title: "Nombre Coloquial",
                        data: "colloquialName",
                        orderable: false
                    },
                    {
                        title: "Opciones",
                        orderable: false,
                        data: null,
                        render: function (data) {
                            var tpm = "";
                            tpm += `<button title='Editar' class="btn btn-primary m-btn m-btn--icon btn-sm m-btn--icon-only btn-edit" data-id="${data.id}"><i class="la la-edit"></i></button>`;
                            tpm += `<button title='Eliminar' class="ml-1 btn btn-danger m-btn m-btn--icon btn-sm m-btn--icon-only btn-delete" data-id="${data.id}"><i class="la la-trash"></i></button>`;
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
                    datatable.themes.object.on("click", ".btn-edit", function () {
                        var id = $(this).data("id");
                        modal.theme.edit.show(id);
                    });
                },
                onDelete: function () {
                    datatable.themes.object.on("click", ".btn-delete", function () {
                        var id = $(this).data("id");
                        swal({
                            title: "¿Está seguro?",
                            text: "El registro será eliminado permanentemente",
                            type: "warning",
                            showCancelButton: true,
                            confirmButtonText: "Sí, eliminarlo",
                            cancelButtonText: "Cancelar",
                            showLoaderOnConfirm: true,
                            preConfirm: () => {
                                return new Promise((resolve) => {
                                    $.ajax({
                                        url: `/admin/especialidades/eliminar-tema?id=${id}`,
                                        type: "POST"
                                    })
                                        .done(function (e) {
                                            datatable.themes.reload();
                                            swal({
                                                type: "success",
                                                title: "Hecho!",
                                                confirmButtonText: "Entendido",
                                                text: "El registro ha sido eliminado satisfactoriamente."
                                            });
                                        })
                                        .fail(function (e) {
                                            swal({
                                                type: "error",
                                                title: "Error",
                                                confirmButtonText: "Entendido",
                                                text: e.status === 502 ? "No hay respuesta del servidor" : e.responseText,
                                            });
                                        });
                                });
                            },
                            allowOutsideClick: () => !swal.isLoading()
                        });
                    });
                },
                init: function () {
                    this.onEdit();
                    this.onDelete();
                }
            },
            init: function () {
                datatable.themes.object = $("#tbl-data").DataTable(datatable.themes.options);
                datatable.themes.events.init();
            }
        },
        init: function () {
            datatable.themes.init();
        }
    };

    var events = {
        onShowModal: function () {
            $("#add_theme").on("click", function () {
                modal.theme.add.show();
            });
        },
        onSeach: function () {
            $("#search").doneTyping(function () {
                datatable.themes.reload();
            });
        },
        init: function () {
            this.onShowModal();
            this.onSeach();
        }
    };

    var modal = {
        theme: {
            object: $("#theme_modal"),
            form: {
                object: $("#theme_form").validate({
                    rules: {
                        OfficialName: {
                            maxlength: 60
                        },
                        ColloquialName: {
                            maxlength: 60
                        }
                    },
                    submitHandler: function (formElement, e) {
                        e.preventDefault();
                        var $btn = $(formElement).find("button[type='submit']");
                        $btn.addClass("m-loader m-loader--right m-loader--light").attr("disabled", true);
                        var formData = new FormData(formElement);
                        formData.append("SpecialityId", specialityId);
                        modal.theme.object.find(":input").attr("disabled", true);
                        $.ajax({
                            url: $(formElement).attr("action"),
                            type: "POST",
                            data: formData,
                            contentType: false,
                            processData: false
                        })
                            .done(function (e) {
                                modal.theme.object.modal("hide");
                                datatable.themes.reload();
                                swal({
                                    type: "success",
                                    allowOutsideClick: false,
                                    title: "Éxito",
                                    text: "Tema guardado satisfactoriamente.",
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
                                modal.theme.object.find(":input").attr("disabled", false);
                            });
                    }
                })
            },
            add: {
                show: function () {
                    modal.theme.object.find(".modal-title").text("Agregar tema");
                    $("#theme_form").attr("action", "/admin/especialidades/agregar-tema");
                    modal.theme.object.modal("show");
                }
            },
            edit: {
                show: function (id) {
                    modal.theme.object.find(":input").attr("disabled", true);
                    modal.theme.object.find(".modal-title").text("Editar tema");
                    $("#theme_form").attr("action", "/admin/especialidades/editar-tema");
                    modal.theme.object.modal("show");

                    $.ajax({
                        url: "/admin/especialidades/get-tema?id=" + id,
                        type: "GET"
                    })
                        .done(function (e) {
                            modal.theme.object.find("[name='Code']").val(e.code);
                            modal.theme.object.find("[name='ColloquialName']").val(e.colloquialName);
                            modal.theme.object.find("[name='OfficialName']").val(e.officialName);
                            modal.theme.object.find("[name='Id']").val(e.id);
                            modal.theme.object.find(":input").attr("disabled", false);
                        });
                }
            },
            events: {
                onHidden: function () {
                    modal.theme.object.on('hidden.bs.modal', function (e) {
                        modal.theme.form.object.resetForm();
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
            this.theme.init();
        }
    };

    return {
        init: function () {
            datatable.init();
            events.init();
            modal.init();
        }
    };
}();

$(() => {
    index.init();
});