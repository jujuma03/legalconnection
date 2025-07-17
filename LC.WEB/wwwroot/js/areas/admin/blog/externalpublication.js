var index = function () {

    var datatable = {
        publications: {
            object: null,
            options: {
                ajax: {
                    url: "/admin/blog/publicaciones-externas-get",
                    type: "GET",
                    data: function (data) {
                        data.searchValue = $("#search").val();
                    }
                },
                columns: [
                    {
                        data: "title",
                        title: "Titulo"
                    },
                    {
                        data: "lawyerFullName",
                        title: "Abogado"
                    },
                    {
                        data: "publicationDate",
                        title: "Fec. Publicación"
                    },
                    {
                        data: null,
                        title: "Opciones",
                        render: function (row) {
                            var tpm = `<a href="/admin/blog/publicaciones-externas-detalle?id=${row.id}" class="btn btn-primary btn-sm m-btn  m-btn m-btn--icon"><span><i class="la la-eye"></i><span>Detalle</span></span></a>`;
                            tpm += `<button data-id=${row.id} class="ml-1 btn-delete btn btn-danger m-btn m-btn--icon btn-sm m-btn--icon-only"><i class="la la-trash"></i></button>`;
                            return tpm;
                        }
                    },
                ]
            },
            reload: function () {
                this.object.ajax.reload();
            },
            events: {
                onDelete: function () {
                    $("#tbl-data").on("click", ".btn-delete", function () {
                        var id = $(this).data("id");
                        swal({
                            title: '¿Está seguro?',
                            text: "El registro será elminado.",
                            type: 'warning',
                            showCancelButton: true,
                            confirmButtonText: 'Sí, eliminar',
                            cancelButtonText: 'Cancelar',
                            showLoaderOnConfirm: true,
                            allowOutsideClick: () => !swal.isLoading(),
                            preConfirm: () => {
                                return new Promise((resolve) => {
                                    $.ajax({
                                        url: `/admin/blog/publicacion-externa-eliminar?id=${id}`,
                                        type: "POST"
                                    })
                                        .done(function () {
                                            datatable.publications.reload();
                                            swal({
                                                type: "success",
                                                title: "Éxito.",
                                                text: "El registro ha sido eliminado satisfactoriamente.",
                                                confirmButtonText: "Aceptar"
                                            });
                                        }).
                                        fail(function (e) {
                                            swal({
                                                type: "error",
                                                title: "Error al eliminar el registro.",
                                                text: e.status === 502 ? "No hay respuesta del servidor" : e.responseText,
                                                confirmButtonText: "Aceptar"
                                            });
                                        });
                                });
                            }
                        });
                    })
                },
                init: function () {
                    this.onDelete();
                }
            },
            init: function () {
                this.object = $("#tbl-data").DataTable(this.options);
                this.events.init();
            }
        },
        init: function () {
            this.publications.init();
        }
    };

    var events = {
        onSearch: function () {
            $("#search").doneTyping(function () {
                datatable.publications.reload();
            })
        },
        init: function () {
            this.onSearch();
        }
    };

    return {
        init: function () {
            datatable.init();
            events.init();
        }
    };
}();

$(() => {
    index.init();
});