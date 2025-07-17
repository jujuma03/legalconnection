var plans =  function() {

    var datatable = {
        plan: {
            object: null,
            options: {
                ajax: {
                    url: `/admin/planes/get`,
                    tpye: "POST",
                    data: function (data) {
                        data.searchValue = $("#search").val();
                    }
                },
                columns: [
                    {
                        data: "name",
                        title: "Nombre",
                        orderable : false
                    },
                    {
                        data: "interval",
                        orderable : false,
                        title: "Intervalo"
                    },
                    {
                        data: "amount",
                        orderable: false,
                        title: "Costo"
                    },
                    {
                        data: null,
                        title: "Opciones",
                        orderable: false,
                        render: function (row) {
                            var tpm = "";
                            tpm += `<a href="/admin/planes/detalles/${row.id}" class="btn btn-primary btn-sm m-btn m-btn m-btn--icon"><span><i class="la la-eye"></i><span>Detalles</span></span></a>`;
                            tpm += `<a href="/admin/planes/${row.id}/beneficios" class="ml-1 btn btn-secondary btn-sm m-btn m-btn m-btn--icon"><span><i class="la la-edit"></i><span>Editar</span></span></a>`;
                            tpm += `<button title='Eliminar'class="ml-1 btn btn-danger m-btn m-btn--icon btn-sm m-btn--icon-only btn-delete" data-id="${row.id}"><i class="la la-trash"></i></button>`;
                            return tpm;
                        }
                    }
                ]
            },
            reload: function () {
                this.object.ajax.reload();
            },
            events: {
                onDelete: function () {
                    datatable.plan.object.on("click", ".btn-delete", function () {
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
                                        url: `/admin/planes/eliminar-plan?id=${id}`,
                                        type: "POST",
                                    })
                                        .done(function (e) {
                                            swal({
                                                type: "success",
                                                title: "Hecho!",
                                                confirmButtonText: "Entendido",
                                                text: "El registro ha sido eliminado satisfactoriamente."
                                            });
                                            datatable.plan.reload();
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
                    this.onDelete();
                }
            },
            init: function () {
                datatable.plan.object = $("#tbl-data").DataTable(this.options);
                this.events.init();
            }
        },
        init: function () {
            this.plan.init();
        }
    };

    return {
        init: function () {
            datatable.init();
        }
    };
}();

$(() => {
    plans.init();
});
