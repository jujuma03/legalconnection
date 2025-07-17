var InitApp = function () {
    var datatable = {
        client: {
            object: null,
            options: {
                ajax: {
                    url: "/reportes/pagos/get",
                    type: "GET",
                    data: function (data) {
                        data.searchValue = $("#search").val();
                    }
                },
                columns: [
                    {
                        data: "name",
                        title: "Nombre",
                        orderable: false
                    },
                    {
                        data: "surnames",
                        title: "Apellidos",
                        orderable: false
                    },
                    {
                        data: "document",
                        title: "DNI",
                        orderable: false
                    },
                    {
                        data: "email",
                        title: "Correo",
                        orderable: false
                    },
                    {
                        data: "totalAmount",
                        title: "Monto Total",
                        orderable: false
                    },
                    {
                        data: "date",
                        title: "Fecha",
                        orderable: false
                    },
                    //{
                    //    data: null,
                    //    title: "Opciones",
                    //    orderable: false,
                    //    render: function (data) {
                    //        return `<a href="/admin/abogados/perfil/${data.id}" class="btn btn-sm btn-info"> <i class="la la-eye"></i> </button>`;
                    //    }
                    //}
                ]
            },
            reload: function () {
                this.object.ajax.reload();
            },
            init: function () {
                this.object = $("#tbl-data").DataTable(this.options);
            }
        },
        init: function () {
            this.client.init();
            $("#search").doneTyping(function () {
                datatable.client.reload();
            });
        }
    };

    return {
        init: function () {
            datatable.init();
        }
    };
}();

$(() => {
    InitApp.init();
});
