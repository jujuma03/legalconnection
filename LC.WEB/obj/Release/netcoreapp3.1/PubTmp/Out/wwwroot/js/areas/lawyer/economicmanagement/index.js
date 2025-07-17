var index = function () {

    var balance = {
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
        init: function () {
            this.avaiable.init();
        }
    };

    var legalcases = {
        finalized: {
            activePage: 1,
            recordsPerDraw: 10,
            object: $("#finished_cases_partial"),
            update : function () {
                $.ajax({
                    url: "/abogado/finanzas/get-casos-concluidos",
                    type: "GET",
                    dataType: "HTML",
                    data: {
                        page: legalcases.finalized.activePage,
                        rpdraw: legalcases.finalized.recordsPerDraw
                    }
                })
                    .done(function (e) {
                        legalcases.finalized.object.html(e);
                    });
            },
            events: {
                onChangePage: function () {
                    legalcases.finalized.object.on("click", ".previous-item", function () {
                        legalcases.finalized.activePage--;
                        legalcases.finalized.update();
                    });

                    legalcases.finalized.object.on("click", ".next-item", function () {
                        legalcases.finalized.activePage++;
                        legalcases.finalized.update();
                    });
                },
                init: function () {
                    this.onChangePage();
                }
            },
            init: function () {
                this.update();
                this.events.init();
            }
        },
        init: function () {
            this.finalized.init();
        }
    };

    return {
        init: function () {
            balance.init();
            legalcases.init();
        }
    };
}();

$(() => {
    index.init();
});