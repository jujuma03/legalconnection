var validated = function () {
    var datatable = {
        lawyer: {
            object: null,
            options: {
                ajax: {
                    url: "/admin/abogados/get",
                    type :"GET",
                    data: function (data) {
                        delete data.columns;
                        data.status = 5;
                        data.search = $("#search").val();
                    }
                },
                columns: [
                    {
                        data: "code",
                        title: "Código",
                        orderable: false
                    },
                    {
                        data: "fullName",
                        title: "Nombre",
                        orderable: false
                    },
                    {
                        data: "validationDate",
                        title: "Fec. Validación",
                        orderable: false
                    },
                    {
                        data: "legalCasesReceived",
                        title: "Casos recibidos",
                        orderable: false
                    },
                    {
                        data: "legalCasesRejected",
                        title: "Casos rechazados",
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
    var datatableplan = {
        lawyer: {
            object: null,
            options: {
                ajax: {
                    url: "/admin/abogados/get",
                    type: "GET",
                    data: function (data) {
                        delete data.columns;
                        data.status = 3;
                        data.search = $("#search").val();
                    }
                },
                columns: [
                    {
                        data: "code",
                        title: "Código",
                        orderable: false
                    },
                    {
                        data: "fullName",
                        title: "Nombre",
                        orderable: false
                    },
                    {
                        data: "surnames",
                        title: "Apellidos",
                        orderable: false
                    },
                    {
                        data: "ubigeo",
                        title: "Lugar",
                        orderable: false
                    },
                    {
                        data: "validationDate",
                        title: "Fec. Validación",
                        orderable: false
                    },
                    {
                        data: "paymentDate",
                        title: "Fec. pago",
                        orderable: false
                    },
                    {
                        data: "isPublic",
                        title: "Perfil",
                        orderable: false
                    },
                    {
                        data: "legalCasesReceived",
                        title: "Casos recibidos",
                        orderable: false
                    },
                    {
                        data: "legalCasesRejected",
                        title: "Casos rechazados",
                        orderable: false
                    },
                    {
                        data: "legalCasesApplied",
                        title: "Casos post.",
                        orderable: false
                    },
                    {
                        data: "specialties",
                        title: "Esp.",
                        orderable: false
                    },
                    {
                        data: "plan",
                        title: "Plan",
                        orderable: false
                    },
                    {
                        data: null,
                        title: "Opc.",
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
                this.object = $("#tbl-data2").DataTable(this.options);
            }
        },
        init: function () {
            this.lawyer.init();
        }
    };

    var events = {
        onSearch: function () {
            $("#search").doneTyping(function () {
                datatable.lawyer.reload();
            });
        },
        init: function () {
            this.onSearch();
        }
    };

    return {
        init: function () {
            datatable.init();
            datatableplan.init();
            events.init();
        }
    };
}();

$(() => {
    validated.init();
})
