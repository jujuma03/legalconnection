var lawyerdirectory = function () {
    var lawyerscards = {
        first: true,
        activePage: 1,
        recordsPerDraw: 10,
        arrayStars: [],
        init: function () {
            $("#lawyers-cards").blockUI("Cargando...");
            $.ajax({
                url: '/directorio/get',
                data: {
                    //pagination
                    srch: $("#search").val(),
                    page: lawyerscards.activePage,
                    rpdraw: lawyerscards.recordsPerDraw,
                    //
                    min: $(".min").val(),
                    max: $(".max").val(),
                    sp: $(".specialties-select").val(),
                    d: $(".departaments-select").val(),
                    prv: $(".provinces-select").val(),
                    //t: $(".departaments-select").val(),
                    stars: lawyerscards.arrayStars.join(','),
                    first: lawyerscards.first
                },
                success: function (result) {
                    lawyerscards.first = false;
                    $("#lawyers-cards").html(result);
                    $("#lawyers-cards").unblockUI();
                    $('#lawyers-cards').find('.complete-card').find('.expandable').expander({
                        slicePoint: 200,
                        expandText: 'VER MÁS',
                        userCollapseText: '...OCULTAR'
                    });
                },
                error: function () {

                }
            });

        },
        events: function () {
            $("#lawyers-cards").on("click", ".previous-item", function () {
                lawyerscards.activePage--;
                lawyerscards.init();
            });
            $("#lawyers-cards").on("click", ".next-item", function () {
                lawyerscards.activePage++;
                lawyerscards.init();
            });
            $("#lawyers-cards").on("click", ".m-portlet__head-label", function () {
                var id = $(this).data("id");
                window.location.href = `/lc/abogado/perfil/${id}`
            });
        }
    };
    var selects = {
        provinces: {
            init: function () {
                $.ajax({
                    url: `/provincias/get/v3/null?first=false`
                }).done(function (result) {
                    $(".provinces-select").empty();
                    var newOption = new Option("Todas", "value", true, true);
                    $('.provinces-select').append(newOption).trigger('change');
                    $(".provinces-select").select2({
                        data: result.items,
                    });
                });
                selects.provinces.events();
            },
            load: function (dptoId) {
                $.ajax({
                    url: `/provincias/get/v3/${dptoId}`
                }).done(function (result) {
                    $(".provinces-select").empty();
                    var newOption = new Option("Todas", "value", true, true);
                    $('.provinces-select').append(newOption).trigger('change');
                    $(".provinces-select").select2({
                        data: result.items,
                        disabled: false
                    });
                });
            },
            events: function () {
                $(".provinces-select").on("change", function () {
                    lawyerscards.init();
                });
            }
        },
        departaments: {
            init: function () {
                selects.departaments.load();
                selects.departaments.events();
            },
            load: function () {
                $.ajax({
                    url: `/departamentos/get/v3`
                }).done(function (result) {
                    $(".departaments-select").select2({
                        data: result.items
                    });
                });
            },
            events: function () {
                $(".departaments-select").on("change", function () {
                    var dptoId = $(this).val();
                    lawyerscards.init();
                    selects.provinces.load(dptoId);
                });
            }
        },
        specialties: {
            init: function () {
                selects.specialties.load();
                selects.specialties.events();
            },
            load: function () {
                $.ajax({
                    url: `/especialidades/get/v2`
                }).done(function (result) {
                    $(".specialties-select").select2({
                        data: result.results
                    });
                });
            },
            events: function () {
                $(".specialties-select").on("change", function () {
                    lawyerscards.init();
                });
            }
        },
        types: {
            init: function () {
                $(".types-select").select2();
                //selects.types.load();
                //selects.types.events();
            },
            load: function () {
                $.ajax({
                    url: `/especialidades/get/v2`
                }).done(function (result) {
                    $(".types-select").select2({
                        data: result.items
                    });
                });
            },
            events: function () {
                $(".types-select").on("change", function () {
                    lawyerscards.init();
                });
            }
        },
        init: function () {
            this.specialties.init();
            this.departaments.init();
            this.provinces.init();
            this.types.init();
        }
    };
    var searchValue = {
        init: function () {
            $("#search").doneTyping(function () {
                //$("#lawyers-cards").blockUI("Cargando...");
                lawyerscards.init();
                //$("#lawyers-cards").unblockUI();
            });
        }
    };
    var range = {
        init: function () {
            $(".range-change").doneTypingRange(function () {
                //$("#lawyers-cards").blockUI("Cargando...");
                lawyerscards.init();
                //$("#lawyers-cards").unblockUI();
            });
        }
    };
    var stars = {
        init: function () {
            $('#customCheck1,#customCheck2,#customCheck3,#customCheck4,#customCheck5').click(function () {
                $("#lawyers-cards").blockUI("Cargando...");
                var valto = this.value;
                if (this.checked) {
                    lawyerscards.arrayStars.push(valto);
                } else {
                    lawyerscards.arrayStars = lawyerscards.arrayStars.filter(function (value, index, arr) { return value != valto; })
                }
                lawyerscards.init();
                $("#lawyers-cards").unblockUI();
            });
        }
    };

    return {
        init: function () {
            lawyerscards.init();
            lawyerscards.events();
            selects.init();
            range.init();
            searchValue.init();
            stars.init();
        }
    };
}();

$(() => {
    lawyerdirectory.init();
    $('[data-toggle="tooltip"]').tooltip();
});