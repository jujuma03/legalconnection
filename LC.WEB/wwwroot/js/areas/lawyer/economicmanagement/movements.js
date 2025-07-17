var initapp = function () {
    var movements = {
        activePage: 1,
        recordsPerDraw: 10,
        init: function () {
            mApp.block(".movements-partial", {
                message: "Cargando saldo..."
            });

            $.ajax({
                url: "/abogado/finanzas/get-solicitudes-retiro-partial",
                type: "GET",
                dataType: "HTML",
                data: {
                    page: movements.activePage,
                    rpdraw: movements.recordsPerDraw
                }
            })
                .done(function (e) {
                    $("#movements-partial").html(e);
                    mApp.unblock(".movements-partial");
                })
                .fail(function () {
                    toastr.error("Ocurrió un problema en el servidor", "Error!");
                });
        }
    };
  
    return {
        init: function () {
            movements.init();
        }
    };
}();

$(() => {
    initapp.init();
});

