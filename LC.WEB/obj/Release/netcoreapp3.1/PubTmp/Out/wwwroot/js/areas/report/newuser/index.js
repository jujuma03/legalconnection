var InitApp = function () {
    var datatable = {
        user: {
            object: null,
            options: {
                responsive: true,
                processing: true,
                serverSide: true,
                ajax: {
                    url: "/reportes/usuarios-nuevos/get",
                    type: "GET",
                    data: function (data) {
                        delete data.columns;
                        data.searchValue = $("#search").val();
                    }
                },
                columns: [
                    {
                        data: "fullName",
                        title: "Nombre",
                    },
                    {
                        data: "email",
                        title: "Correo",
                    },
                    {
                        data: "phone",
                        title: "Celular",
                    },
                    {
                        data: "document",
                        title: "DNI",
                    },
                    {
                        data: "date",
                        title: "Fecha",
                    },
                    {
                        data: "type",
                        title: "Tipo",
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
            this.user.init();
            $("#search").doneTyping(function () {
                datatable.user.reload();
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
