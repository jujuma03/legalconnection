var InitApp = function () {
    var datatable = {
        types: {
            object: null,
            options: {
                processing: true,
                serverSide: true,
                ajax: {
                    url: "/admin/redes-sociales/get",
                    type: "GET",
                    dataType: "JSON",
                    data: function (data) {
                        delete data.columns;
                        data.type = $("#selectType-filter option:selected").val();
                        data.status = $("#selectStatus option:selected").val();
                    }
                },
                columns: [
                    {
                        title: "Imágen",
                        data: null,
                        render: function (data) {
                            return '<i class="la la-' + data.type.toLowerCase() + '"></i>';
                        }
                    },
                    {
                        title: "Tipo",
                        data: "type"
                    },
                    {
                        title: "Estado",
                        data: null,
                        render: function (data) {
                            //return '<label class="switch"><input type="checkbox" id="togBtn"><div class="slider round"><span class="on">ACTIVO</span><span class="off">OCULTO</span></div></label>';
                            if (data.status == 'Oculto')
                                return '<label class="switch"><input type="checkbox" id="togBtn" class="status-switch" data-id="' + data.id + '"><div class="slider round"><span class="on" style="color:black; font-weight:bold;" >ACTIVO</span><span class="off" style="color:black; font-weight:bold;">OCULTO</span></div></label>';
                            if (data.status == 'Activo')
                                return '<label class="switch"><input type="checkbox" checked id="togBtn" class="status-switch" data-id="' + data.id + '"><div class="slider round"><span class="on" style="color:black; font-weight:bold;">ACTIVO</span><span class="off" style="color:black; font-weight:bold;">OCULTO</span></div></label>';
                        }
                    },
                    {
                        title: "Opciones",
                        data: null,
                        render: function (data) {
                            return '<a data-id="' + data.id + '" data-toggle="modal" href="#update_modal" class="btn btn-light m-btn m-btn--icon m-btn--icon-only edit" title="Editar"><i class="la la-edit"></i></a>' +
                                ' <button data-id="' + data.id + '" class="btn btn-light m-btn m-btn--icon m-btn--icon-only delete" title="Delete"><i class="la la-trash"></i></button>';
                        }
                    }
                ],

            },
            init: function () {
                datatable.types.object = $("#data-table").DataTable(datatable.types.options);

                $('#data-table').on('click', '.edit', function (e) {
                    var id = $(this).data("id");
                    $("#update_items_form input").attr("disabled", true);
                    $("#update_items_form select").attr("disabled", true);
                    $("#update_items_form textarea").attr("disabled", true);
                    $(".btn-save").addLoader();
                    $.ajax({
                        url: `/admin/redes-sociales/get/${id}`,
                        type: "GET"
                    }).always(function () {
                        $("#update_items_form input").attr("disabled", false);
                        $("#update_items_form select").attr("disabled", false);
                        $("#update_items_form textarea").attr("disabled", false);
                        $(".btn-save").removeLoader();
                    }).done(function (result) {
                        $("#update_items_form #Id").val(result.id);
                        $("#update_items_form #Type").val(result.type).trigger("change");
                        $("#update_items_form #UrlDirection").val(result.urlDirection);
                        if (result.status) {
                            $("#update_items_form #Status").prop("checked", true);
                        } else {
                            $("#update_items_form #Status").prop("checked", false);
                        }
                    }).fail(function (e) {

                    });
                });

                $('#data-table').on('change', '.status-switch', function (e) {
                    var id = $(this).data("id");
                    var active = $(this).is(":checked");
                    mApp.blockPage();
                    $.ajax({
                        url: `/admin/redes-sociales/${id}/cambiar-estado/post`,
                        type: "POST",
                        data: {
                            status: active
                        }
                    }).always(function () {
                        mApp.unblockPage();
                        datatable.types.reload();
                    }).done(function () {
                        swal({
                            type: "success",
                            allowOutsideClick: false,
                            title: "Éxito",
                            text: "Estado cambiado satisfactoriamente.",
                            confirmButtonText: "Aceptar"
                        });
                    }).fail(function (e) {
                        swal({
                            type: "error",
                            title: "Error al cambiar estado.",
                            text: e.status === 502 ? "No hay respuesta del servidor" : e.responseText,
                            confirmButtonText: "Aceptar"
                        });
                    });
                });

                $('#data-table').on('click', '.delete', function (e) {
                    var id = $(this).data("id");
                    swal({
                        title: "¿Desea eliminar el elemento?",
                        text: "Una vez eliminado no podrá recuperarlo",
                        type: "warning",
                        showCancelButton: true,
                        confirmButtonText: "Si, eliminar",
                        confirmButtonClass: "btn btn-danger m-btn m-btn--custom",
                        cancelButtonText: "Cancelar"
                    }).then(function (result) {
                        if (result.value) {
                            mApp.blockPage();
                            $.ajax({
                                url: "/admin/redes-sociales/eliminar",
                                type: "POST",
                                data: {
                                    id: id
                                }
                            }).done(function () {
                                mApp.unblockPage();

                                swal({
                                    type: "success",
                                    allowOutsideClick: false,
                                    title: "Éxito",
                                    text: "Elemento eliminado satisfactoriamente.",
                                    confirmButtonText: "Aceptar"
                                }).then((result) => {
                                    datatable.types.reload();
                                });
                            }).fail(function (e) {
                                swal({
                                    type: "error",
                                    title: "Error al eliminar elemento.",
                                    text: e.status === 502 ? "No hay respuesta del servidor" : e.responseText,
                                    confirmButtonText: "Aceptar"
                                });
                            });

                        }
                    });
                });
            },
            reload: function () {
                datatable.types.object.ajax.reload();
            }
        },
        init: function () {
            datatable.types.init();
        }
    };
    var form = {
        submit: function (form) {
            var formData = new FormData($(form).get(0));
            $("#add_items_form input").attr("disabled", true);
            $("#add_items_form select").attr("disabled", true);
            $("#add_items_form textarea").attr("disabled", true);
            $(".btn-save").addLoader();
            $.ajax({
                data: formData,
                type: "POST",
                contentType: false,
                processData: false,
                url: $(form).attr("action")
            })
                .always(function () {
                    $("#add_items_form input").attr("disabled", false);
                    $("#add_items_form select").attr("disabled", false);
                    $("#add_items_form textarea").attr("disabled", false);
                    $(".btn-save").removeLoader();
                })
                .done(function (result) {
                    swal({
                        type: "success",
                        allowOutsideClick: false,
                        title: "Éxito",
                        text: "Pregunta guardada satisfactoriamente.",
                        confirmButtonText: "Aceptar"
                    }).then((result) => {
                        $("#add_modal").modal("hide");
                        datatable.types.reload();
                    });
                })
                .fail(function (e) {
                    swal({
                        type: "error",
                        title: "Error al guardar pregunta.",
                        text: e.status === 502 ? "No hay respuesta del servidor" : e.responseText,
                        confirmButtonText: "Aceptar"
                    });
                });
        },
        submitUpdate: function (form) {
            var formData = new FormData($(form).get(0));
            $("#update_items_form input").attr("disabled", true);
            $("#update_items_form select").attr("disabled", true);
            $("#update_items_form textarea").attr("disabled", true);
            $(".btn-save").addLoader();
            $.ajax({
                data: formData,
                type: "POST",
                contentType: false,
                processData: false,
                url: $(form).attr("action")
            })
                .always(function () {
                    $("#update_items_form input").attr("disabled", false);
                    $("#update_items_form select").attr("disabled", false);
                    $("#update_items_form textarea").attr("disabled", false);
                    $(".btn-save").removeLoader();
                })
                .done(function (result) {
                    swal({
                        type: "success",
                        allowOutsideClick: false,
                        title: "Éxito",
                        text: "Pregunta editada satisfactoriamente.",
                        confirmButtonText: "Aceptar"
                    }).then((result) => {
                        $("#update_modal").modal("hide");
                        datatable.types.reload();
                    });
                })
                .fail(function (e) {
                    swal({
                        type: "error",
                        title: "Error al editar pregunta.",
                        text: e.status === 502 ? "No hay respuesta del servidor" : e.responseText,
                        confirmButtonText: "Aceptar"
                    });
                });
        }
    };

    var validate = {
        init: function () {
            $("#add_items_form").validate({
                submitHandler: function (formElement, e) {
                    e.preventDefault();
                    form.submit(formElement);
                }
            });
            $("#update_items_form").validate({
                submitHandler: function (formElement, e) {
                    e.preventDefault();
                    form.submitUpdate(formElement);
                }
            });
            $("#add_modal").on('hidden.bs.modal', function (e) {
                $("#add_items_form").resetForm();
            });
            $("#update_modal").on('hidden.bs.modal', function (e) {
                $("#update_items_form").resetForm();
            });
        }
    };
    var select = {
        init: function () {
            $("#selectType-add, #selectType-edit, #selectStatus, #selectType-filter").select2();
        }
    };

    return {
        init: function () {
            datatable.types.init();
            select.init();
            //search.init();
            validate.init();
        }
    };
}();

$(function () {
    InitApp.init();
});