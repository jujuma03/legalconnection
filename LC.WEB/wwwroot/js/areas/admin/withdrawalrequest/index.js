var index = function () {

    var datatable = {
        withdrawalRequest : {
            object: null,
            options: {
                ajax: {
                    url: "/admin/solicitudes-retiro/get",
                    type: "GET",
                    data: function (data) {

                    }
                },
                columns: [
                    {
                        data: "registerDate",
                        title :"Fec. Registro"
                    },
                    {
                        data: "lawyerFullName",
                        title :"Abogado"
                    },
                    {
                        data: "amount",
                        title: "Monto"
                    },
                    {
                        data: "status",
                        title :"Estado"
                    },
                    {
                        data: null,
                        title: "Opciones",
                        render: function (data) {
                            var tpm = "";
                            tpm += `<a href="/admin/solicitudes-retiro/${data.id}/detalles" class="btn btn-primary btn-sm m-btn m-btn m-btn--icon"><span><i class="la la-eye"></i><span>Detalles</span></span></a>`;
                            return tpm;
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
            datatable.withdrawalRequest.init();
        }
    };

    return {
        init: function () {
            datatable.init();
        }
    };
}();

$(() => {
    index.init();
});