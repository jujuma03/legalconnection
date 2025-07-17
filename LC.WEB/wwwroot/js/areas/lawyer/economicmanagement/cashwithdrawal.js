var initapp = function () {

    var datatable = {
        withdrawalRequest: {
            object: null,
            options: {
                ajax: {
                    url: `/abogado/finanzas/get-solicitudes-retiro`,
                    type: "GET"
                },
                pageLength : 10,
                columns: [
                    {
                        data: "registerDate",
                        title: "Fec. Solicitud",
                        orderable: false
                    },
                    {
                        data: "amount",
                        title: "Monto Solicitado",
                        orderable: false
                    },
                    {
                        data: "status",
                        title: "Estado",
                        orderable: false
                    },
                    {
                        data: null,
                        title: "Opciones",
                        orderable: false,
                        render: function (row) {
                            var tpm = "";
                            tpm += `<button type='button' title='Detalles' class="ml-1 btn btn-primary btn-sm m-btn m-btn m-btn--icon"><span><i class="la la-eye"></i><span>Detalles</span></span></button>`;
                            return tpm;
                        }
                    }
                ]
            },
            init: function () {
                datatable.withdrawalRequest.object = $("#withdrawal_request_table").DataTable(datatable.withdrawalRequest.options);
            }
        },
        init: function () {
            this.withdrawalRequest.init();
        }
    };

    var balance = {
        inprocess: {
            get: function () {
                mApp.block(".div_proceess", {
                    message: "Cargando saldo..."
                });

                $.ajax({
                    url: "/abogado/finanzas/get-saldo-proceso",
                    type: "GET"
                })
                    .done(function (e) {
                        var result = e.toFixed(2);
                        $("#in_process_balance_span").html(`S/.${result}`);
                        mApp.unblock(".div_proceess");
                    })
                    .fail(function () {
                        toastr.error("No se pudo obtener el saldo disponible", "Error!");
                    });
            },
            init: function () {
                this.get();
            }
        },
        avaiable: {
            get: function () {
                mApp.block(".available_balance", {
                    message: "Cargando saldo..."
                });

                $.ajax({
                    url: "/abogado/finanzas/get-saldo-disponible",
                    type: "GET"
                })
                    .done(function (e) {
                        var result = e.toFixed(2);
                        $("#available_balance_span").html(`S/.${result}`);
                        mApp.unblock(".available_balance");
                    })
                    .fail(function () {
                        toastr.error("No se pudo obtener el saldo disponible", "Error!");
                    });
            },
            init: function () {
                this.get();
            }
        },
        possible: {
            get: function () {
                mApp.block(".div_detail_2", {
                    message: "Cargando saldo..."
                });

                $.ajax({
                    url: "/abogado/finanzas/get-saldos",
                    type: "GET"
                })
                    .done(function (result) {
                        $("#total_balance_span").html(`S/.${result.total.toFixed(2)}`);
                        $(".inprogress_span").html(`${result.inProgessCases}`);
                        $(".inprogress_balance_span").html(`S/.${result.inprogressBalanace.toFixed(2)}`);
                        $(".finished_span").html(`${result.finishedCases}`);
                        $(".finished_balance_span").html(`S/.${result.finishedBalance.toFixed(2)}`);
                        mApp.unblock(".div_detail_2");
                    })
                    .fail(function () {
                        toastr.error("No se pudo obtener el saldo disponible", "Error!");
                    });
            },
            init: function () {
                this.get();
            }
        },
        init: function () {
            this.avaiable.init();
            this.possible.init();
            this.inprocess.init();
        }
    };

    return {
        init: function () {
            balance.init();
            datatable.init();
        }
    };
}();

$(() => {
    initapp.init();
});

