var index = function () {

    var datatable = {
        specialities: {
            object: null,
            options: {
                serverSide: true,
                processing: true,
                ajax: {
                    url: "/admin/especialidades/get-datatable",
                    type: "GET",
                    dataType: "JSON",
                    data: function (data) {
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
                        data: null,
                        orderable: false,
                        render: function (data) {
                            var tpm = "";
                            tpm += `<button title='Editar' class="btn btn-primary m-btn m-btn--icon btn-sm m-btn--icon-only btn-edit" data-id="${data.id}"><i class="la la-edit"></i></button>`;
                            tpm += `<a href="/admin/especialidades/${data.id}/temas" class="ml-1 btn btn-primary btn-sm m-btn m-btn m-btn--icon"><span><i class="la la-archive"></i><span>Temas</span></span></a>`;
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
                    datatable.specialities.object.on("click", ".btn-edit", function () {
                        var id = $(this).data("id");
                        modal.speacility.edit.show(id);
                    });
                },
                onDelete: function () {
                    datatable.specialities.object.on("click", ".btn-delete", function () {
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
                                        url: `/admin/especialidades/eliminar-especialidad?id=${id}`,
                                        type: "POST",
                                    })
                                        .done(function (e) {
                                            swal({
                                                type: "success",
                                                title: "Hecho!",
                                                confirmButtonText: "Entendido",
                                                text: "El registro ha sido eliminado satisfactoriamente."
                                            });
                                            datatable.specialities.reload();
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
                datatable.specialities.object = $("#tbl-data").DataTable(datatable.specialities.options);
                datatable.specialities.events.init();
            }
        },
        init: function () {
            datatable.specialities.init();
        }
    };

    var events = {
        onShowModal: function () {
            $("#add_speciality").on("click", function () {
                modal.speacility.add.show();
            });
        },
        onSeach: function () {
            $("#search").doneTyping(function () {
                datatable.specialities.reload();
            });
        },
        init: function () {
            this.onShowModal();
            this.onSeach();
        }
    };

    var modal = {
        speacility: {
            object: $("#speciality_modal"),
            form: {
                object: $("#speciality_form").validate({
                    rules: {
                        OfficialName: {
                            maxlength: 50
                        },
                        ColloquialName: {
                            maxlength: 50
                        }
                    },
                    submitHandler: function (formElement, e) {
                        e.preventDefault();
                        var $btn = $(formElement).find("button[type='submit']");
                        $btn.addClass("m-loader m-loader--right m-loader--light").attr("disabled", true);
                        var formData = new FormData(formElement);
                        modal.speacility.object.find(":input").attr("disabled", true);
                        $.ajax({
                            url: $(formElement).attr("action"),
                            type: "POST",
                            data: formData,
                            contentType: false,
                            processData: false
                        })
                            .done(function (e) {
                                modal.speacility.object.modal("hide");
                                datatable.specialities.reload();
                                swal({
                                    type: "success",
                                    allowOutsideClick: false,
                                    title: "Éxito",
                                    text: "Especialidad guardada satisfactoriamente.",
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
                                modal.speacility.object.find(":input").attr("disabled", false);
                            });
                    }
                })
            },
            add: {
                show: function () {
                    modal.speacility.object.find(".modal-title").text("Agregar especialidad");
                    $("#speciality_form").attr("action", "/admin/especialidades/agregar")
                    modal.speacility.object.modal("show");
                }
            },
            edit: {
                show: function (id) {
                    modal.speacility.object.find(":input").attr("disabled", true);
                    modal.speacility.object.find(".modal-title").text("Editar especialidad");
                    $("#speciality_form").attr("action", "/admin/especialidades/editar");
                    modal.speacility.object.modal("show");

                    $.ajax({
                        url: "/admin/especialidades/get-specaility?id=" + id,
                        type: "GET"
                    })
                        .done(function (e) {
                            modal.speacility.object.find("[name='Code']").val(e.code);
                            modal.speacility.object.find("[name='ColloquialName']").val(e.colloquialName);
                            modal.speacility.object.find("[name='OfficialName']").val(e.officialName);
                            modal.speacility.object.find("[name='Id']").val(e.id);
                            modal.speacility.object.find(":input").attr("disabled", false);
                        });
                }
            },
            events: {
                onHidden: function () {
                    modal.speacility.object.on('hidden.bs.modal', function (e) {
                        modal.speacility.form.object.resetForm();
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
            this.speacility.init();
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