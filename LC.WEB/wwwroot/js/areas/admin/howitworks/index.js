var InitApp = function () {
    var datatable = {
        types: {
            object: null,
            options: {
                processing: true,
                serverSide: true,
                ajax: {
                    url: "/admin/como-funciona/get",
                    type: "GET",
                    dataType: "JSON",
                    data: function (data) {
                        delete data.columns;
                        data.Headline = $("#tbHeadline").val();
                        data.Status = $("#selectStatus option:selected").val();
                    }
                },
                columns: [
                    {
                        title: "",
                        data: "type"
                    },
                    {
                        title: "",
                        data: "intType"
                    },
                    {
                        title: "Titular",
                        data: "headline"
                    },
                    {
                        title: "Imágen",
                        data: null,
                        render: function (data) {
                            return '<img class="img-responsive" style="max-width:100%;max-height: 100px;" src="/documentos/'+ data.urlImage + '" />';
                        }
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
                        title: "Orden",
                        data: null,
                        render: function (data) {

                            //$(".select2-sequence").val(data.sequenceOrderId).trigger("change");
                            return '<select data-sequenceOrderId="' + data.sequenceOrderId + '" data-id="' + data.id + '" class="form-control select2-sequence"> <option value="' + data.sequenceOrderId + '">' + data.sequenceOrder + '</option></select>';
                            //select.sequence.init(data.sequenceOrderId);
                        }
                    },
                    {
                        title: "Opciones",
                        data: null,
                        render: function (data) {
                            return '<button data-id="' + data.id + '" class="btn btn-light m-btn m-btn--icon m-btn--icon-only edit" title="Editar"><i class="la la-edit"></i></button>' + ' <button data-id="' + data.id + '" class="btn btn-light m-btn m-btn--icon m-btn--icon-only delete" title="Delete"><i class="la la-trash"></i></button>';
                        }
                    }
                ],
                columnDefs: [
                    {
                        visible: false,
                        targets: [0,1]
                    }
                ],
                rowCallback: function (row, data) {
                    $.ajax({
                        url: (`/admin/como-funciona/seccionOrden/${data.intType}`)
                    }).done(function (result) {
                        $(".select2-sequence").select2({
                            minimumInputLength: 0,
                            data: result.items,
                        });
                    });
                },
                drawCallback: function (settings) {
                    var api = this.api();
                    var rows = api.rows({ page: 'current' }).nodes();
                    var last = null;

                    api.column(0, { page: 'current' }).data().each(function (group, i) {
                        if (last !== group) {
                            $(rows).eq(i).before(
                                '<tr class="group text-left"><td colspan="5">Pasos ' + group + '</td></tr>'
                            );

                            last = group;
                        }
                    });
                }
            },
            init: function () {
                datatable.types.object = $("#data-table").DataTable(datatable.types.options);

                $('#data-table').on('click', '.edit', function (e) {
                    var id = $(this).data("id");

                    window.location.href = `/admin/como-funciona/editar/${id}`;
                });

                $('#data-table').on('change', '.status-switch', function (e) {
                    var id = $(this).data("id");
                    var active = $(this).is(":checked");
                    mApp.blockPage();
                    $.ajax({
                        url: `/admin/como-funciona/${id}/cambiar-estado/post`,
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
                        title: "¿Desea eliminar este paso?",
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
                                url: "/admin/como-funciona/eliminar",
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
                                    text: "Paso eliminado satisfactoriamente.",
                                    confirmButtonText: "Aceptar"
                                }).then((result) => {
                                    datatable.types.reload();
                                });
                            }).fail(function (e) {
                                swal({
                                    type: "error",
                                    title: "Error al eliminar el paso.",
                                    text: e.status === 502 ? "No hay respuesta del servidor" : e.responseText,
                                    confirmButtonText: "Aceptar"
                                });
                            });

                        }
                    });
                });

                $('#data-table').on('change','.select2-sequence', function () {
                    var sequenceOrderId = $(this)[0].value;
                    var id = $(this).data("id");

                    mApp.blockPage();

                    $.ajax({
                        url: `/admin/como-funciona/${id}/cambiar-orden/${sequenceOrderId}/post`,
                        type: "POST",
                    }).always(function () {
                        mApp.unblockPage();
                    }).done(function () {
                        swal({
                            type: "success",
                            allowOutsideClick: false,
                            title: "Éxito",
                            text: "Orden cambiado satisfactoriamente.",
                            confirmButtonText: "Aceptar"
                        }).then((result) => {
                            datatable.types.reload();
                        });
                    }).fail(function (e) {
                        swal({
                            type: "error",
                            title: "Error al cambiar orden",
                            text: e.status === 502 ? "No hay respuesta del servidor" : e.responseText,
                            confirmButtonText: "Aceptar"
                        });
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

    var validationOrde = {
        init: function () {
            $('#btnCreateSlide').click(function () {
                window.location.href = `/admin/como-funciona/registrar`;
            });
        }
    }

    var select = {
        init: function () {
            $("#selectStatus")
                .on("change", function () {
                    datatable.types.object.ajax.reload();
                }).select2({
                });
        }
    };

    var search = {
        init: function () {
            $("#tbHeadline").doneTyping(function () {
                datatable.types.object.ajax.reload();
            });
        }
    }

    return {
        init: function () {
            datatable.types.init();
            select.init();
            search.init();
            $('#btnCreateSlide').click(function () {
                window.location.href = `/admin/como-funciona/registrar`;
            });
            validationOrde.init();
        }
    };
}();

$(function () {
    InitApp.init();
});