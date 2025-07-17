var pending = function () {
    var datatable = {
        lawyer: {
            object: null,
            options: {
                ajax: {
                    url: "/admin/abogados/get",
                    type: "GET",
                    data: function (data) {
                        delete data.columns;
                        data.onlyNewLawyer = true;
                        data.search = $("#search").val();
                    }
                },
                columns: [
                    {
                        data: "fullName",
                        title: "Nombre",
                        orderable: false
                    },
                    {
                        data: "ubigeo",
                        title: "Lugar",
                        orderable: false
                    },
                    {
                        data: "registerDate",
                        title: "Fec. Registro",
                        orderable: false
                    },
                    {
                        data: "status",
                        title: "Estado",
                        orderable: false
                    },
                    {
                        data: "specialties",
                        title: "Especialidades",
                        orderable: false
                    },
                    {
                        data: null,
                        title: "Opciones",
                        orderable: false,
                        render: function (data) {
                            return `<a href="/admin/abogados/perfil/${data.id}" class="btn btn-sm btn-info"> <i class="la la-eye"></i> </button>`;
                        }
                    }
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
            this.lawyer.init();
        }
    };

    return {
        init: function () {
            datatable.init();
        }
    };
}();

$(() => {
    pending.init();
});
