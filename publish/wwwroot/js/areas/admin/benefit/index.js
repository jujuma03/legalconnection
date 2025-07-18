var indexBenefits = function () {
    var datatable = {
        benefit: {
            object: null,
            options: {
                ajax: {
                    url: "/admin/beneficios/get",
                    type: "GET",
                    dataType: "JSON",
                    data: function (data) {
                        data.searchValue = $("#search").val();
                    }
                },
                columns: [
                    {
                        title: "Descripción",
                        data: "description",
                        orderable: false
                    },
                    {
                        title: "Opciones",
                        data: null,
                        orderable: false,
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
                    datatable.benefit.object.on("click", ".btn-edit", function () {
                        var id = $(this).data("id");
                        modal.benefit.edit.show(id);
                    });
                },
                onDelete: function () {
                    datatable.benefit.object.on("click", ".btn-delete", function () {
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
                                        url: `/admin/beneficios/eliminar?id=${id}`,
                                        type: "POST",
                                    })
                                        .done(function (e) {
                                            swal({
                                                type: "success",
                                                title: "Hecho!",
                                                confirmButtonText: "Entendido",
                                                text: "El registro ha sido eliminado satisfactoriamente."
                                            });
                                            datatable.benefit.reload();
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
                datatable.benefit.object = $("#tbl-data").DataTable(datatable.benefit.options);
                datatable.benefit.events.init();
            }
        },
        init: function () {
            datatable.benefit.init();
        }
    };

    var events = {
        onShowModal: function () {
            $("#add_benefit").on("click", function () {
                modal.benefit.add.show();
            });
        },
        onSeach: function () {
            $("#search").doneTyping(function () {
                datatable.benefit.reload();
            });
        },
        init: function () {
            this.onShowModal();
            this.onSeach();
        }
    };

    var modal = {
        benefit: {
            object: $("#benefit_modal"),
            form: {
                object: $("#benefit_form").validate({
                    submitHandler: function (formElement, e) {
                        e.preventDefault();
                        var $btn = $(formElement).find("button[type='submit']");
                        $btn.addClass("m-loader m-loader--right m-loader--light").attr("disabled", true);
                        var formData = new FormData(formElement);
                        modal.benefit.object.find(":input").attr("disabled", true);
                        $.ajax({
                            url: $(formElement).attr("action"),
                            type: "POST",
                            data: formData,
                            contentType: false,
                            processData: false
                        })
                            .done(function (e) {
                                modal.benefit.object.modal("hide");
                                datatable.benefit.reload();
                                swal({
                                    type: "success",
                                    allowOutsideClick: false,
                                    title: "Éxito",
                                    text: "Beneficio guardado satisfactoriamente.",
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
                                modal.benefit.object.find(":input").attr("disabled", false);
                            });
                    }
                })
            },
            add: {
                show: function () {
                    modal.benefit.object.find(".modal-title").text("Agregar Beneficio");
                    $("#benefit_form").attr("action", "/admin/beneficios/agregar");
                    modal.benefit.object.modal("show");
                }
            },
            edit: {
                show: function (id) {
                    modal.benefit.object.find(":input").attr("disabled", true);
                    modal.benefit.object.find(".modal-title").text("Editar especialidad");
                    $("#benefit_form").attr("action", "/admin/beneficios/editar");
                    modal.benefit.object.modal("show");

                    $.ajax({
                        url: "/admin/beneficios/get-beneficio?id=" + id,
                        type: "GET"
                    })
                        .done(function (e) {
                            modal.benefit.object.find("[name='Description']").val(e.description);
                            modal.benefit.object.find("[name='Id']").val(e.id);
                            modal.benefit.object.find(":input").attr("disabled", false);
                        });
                }
            },
            events: {
                onHidden: function () {
                    modal.benefit.object.on('hidden.bs.modal', function (e) {
                        modal.benefit.form.object.resetForm();
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
            this.benefit.init();
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
    indexBenefits.init();
});