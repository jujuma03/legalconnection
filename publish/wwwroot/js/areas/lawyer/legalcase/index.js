var legalcases = function () {
    var CurrentSearchType = null;

    var progress = {
        object: $("#progress_bar"),
        show: function () {
            this.object.removeClass("invisible");
        },
        hide: function () {
            this.object.addClass("invisible");
        }
    };

    var legalCases = {
        activePage: 1,
        recordsPerDraw: 5,
        object: $("#main_portlet_legal_cases"),
        update: function (type) {
            progress.show();
            $.ajax({
                url: `/abogado/casos/get-items?searchType=${type}`,
                type: "GET",
                data: {
                    page: legalCases.activePage,
                    rpdraw: legalCases.recordsPerDraw
                },
                dataType: "HTML"
            })
                .done(function (e) {
                    legalCases.object.html(e);
                    progress.hide();
                });
        },
        events: {
            onLoadBySearch: function () {
                $(".nav_item_legal_cases").on("click", function () {
                    $(".nav_item_legal_cases").removeClass("m-nav__item--active");
                    $(this).addClass("m-nav__item--active");
                    var id = $(this).data("id");
                    CurrentSearchType = id;
                    legalCases.update(id);
                });
            },
            onViewDetail: function () {
                legalCases.object.on("click",".item_legal_case", function () {
                    var id = $(this).data("id");
                    window.location.href = `/abogado/casos/${id}`;
                });
            },
            onChangePage: function () {
                legalCases.object.on("click", ".previous-item", function () {
                    legalCases.activePage--;
                    legalCases.update(CurrentSearchType);
                });

                legalCases.object.on("click", ".next-item", function () {
                    legalCases.activePage++;
                    legalCases.update(CurrentSearchType);
                });
            },
            init: function () {
                this.onLoadBySearch();
                this.onChangePage();
                this.onViewDetail();
            }
        },
        init: function () {
            this.events.init();
        }
    };


    var events = {
        setDefaultItem: function () {
            CurrentSearchType = 1;
            $(`.nav_item_legal_cases[data-id='${1}']`).click();
        },
        onDetail: function () {
            $("#main_portlet_legal_cases").on("click", ".legal_case_portlet", function () {
                var id = $(this).data("id");
                window.location.href = `/abogado/casos/${id}`;
            });
        },
        init: function () {
            this.setDefaultItem();
            this.onDetail();
        }
    };

    return {
        init: function () {
            legalCases.init();
            events.init();
        }
    };
}();

$(() => {
    legalcases.init();
});