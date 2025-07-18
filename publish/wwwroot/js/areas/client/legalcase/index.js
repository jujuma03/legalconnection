var index = function () {

    var legalcases = {
        activePage: 1,
        recordPerDraw: 10,
        object: $("#cases_partial"),
        update: function () {
            mApp.block(legalcases.object, {
                message : "Cargando casos..."
            });

            $.ajax({
                url: "/mis-casos/get-casos",
                data: {
                    page: legalcases.activePage,
                    rpdraw: legalcases.recordPerDraw
                },
                dataType: "html",
                type : "GET"
            })
                .done(function (e) {
                    legalcases.object.html(e);
                })
                .always(function () {
                    mApp.unblock(legalcases.object.object);
                });
        },
        events: {
            onChangePage: function () {
                legalcases.object.on("click", ".previous-item", function () {
                    legalcases.activePage--;
                    legalcases.update();
                });

                legalcases.object.on("click", ".next-item", function () {
                    legalcases.activePage++;
                    legalcases.update();
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
    };

    var events = {
        onDetail: function () {
            $("#cases_partial").on("click", ".legal_case_portlet", function () {
                var id = $(this).data("id");
                window.location.href = `/mis-casos/${id}/detalle`;
            });
        },
        init: function () {
            this.onDetail();
        }
    };

    return {
        init: function () {
            events.init();
            legalcases.init();
        }
    };
}();

$(() => {
    index.init();
})