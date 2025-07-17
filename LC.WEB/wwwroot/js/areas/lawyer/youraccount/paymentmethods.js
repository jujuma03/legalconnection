var paymentmethods = function () {

    var lawyerCard = {
        activePage: 1,
        recordsPerDraw: 5,
        object: $("#lawyer_cards"),
        events: {
            update: function () {
                mApp.block(lawyerCard.object, {
                    message: "Cargando tarjetas..."
                });

                $.ajax({
                    url: `/abogado/tu-cuenta/get-tarjetas`,
                    data: {
                        page: lawyerCard.activePage,
                        rpdraw: lawyerCard.recordsPerDraw
                    },
                    type: "GET",
                    dataType: "html"
                })
                    .done(function (e) {
                        lawyerCard.object.html(e);
                    })
                    .always(function () {
                        mApp.unblock(lawyerCard.object);
                    });
            },
            onChangePage: function () {
                lawyerCard.object.on("click", ".previous-item", function () {
                    lawyerCard.activePage--;
                    lawyerCard.events.update();
                });

                lawyerCard.object.on("click", ".next-item", function () {
                    lawyerCard.activePage++;
                    lawyerCard.events.update();
                });
            },
            onDeleteCard: function () {
                $("#lawyer_cards").on("click", ".btn_delete_card", function () {
                    var id = $(this).data("id");
                    swal({
                        title: '¿Está seguro?',
                        text: "Eliminará la tarjeta seleccionada.",
                        type: 'warning',
                        showCancelButton: true,
                        confirmButtonText: 'Sí, eliminar',
                        cancelButtonText: 'Cancelar',
                        showLoaderOnConfirm: true,
                        allowOutsideClick: () => !swal.isLoading(),
                        preConfirm: () => {
                            return new Promise((resolve) => {
                                $.ajax({
                                    url: `/abogado/tu-cuenta/eliminar-tarjeta?cardId=${id}`,
                                    type: "POST",
                                    contentType: false,
                                    processData: false
                                })
                                    .done(function (e) {
                                        lawyerCard.events.update();
                                        swal({
                                            type: "success",
                                            allowOutsideClick: false,
                                            title: "Hecho",
                                            text: "Tarjeta eliminada.",
                                            confirmButtonText: "Aceptar"
                                        });
                                    })
                                    .fail(function (e) {
                                        swal({
                                            type: "error",
                                            title: "Error al eliminar la tarjeta.",
                                            text: e.status === 502 ? "No hay respuesta del servidor" : e.responseText,
                                            confirmButtonText: "Aceptar"
                                        });
                                    });
                            });
                        }
                    });
                });
            },
            init: function () {
                this.update();
                this.onChangePage();
                this.onDeleteCard();
            }
        },
        init: function () {
            this.events.init();
        }
    };

    var modal = {
        newCard: {
            object: $("#new_card_modal"),
            events: {
                show: function () {
                    var $btn = $(this);

                    $("#new_card_btn").click(function () {
                        var $btn = $(this);
                        $btn.attr("disabled", true);

                        $.ajax({
                            url: "/abogado/tu-cuenta/validar-usuario-pago",
                            type : "POST"
                        })
                            .done(function () {
                                modal.newCard.object.modal("show");
                            })
                            .fail(function (e) {
                                swal({
                                    type: "error",
                                    title: "Error al actualizar los datos de facturación.",
                                    text: e.status === 502 ? "No hay respuesta del servidor" : e.responseText,
                                    confirmButtonText: "Aceptar"
                                });
                            })
                            .always(function () {
                                $btn.attr("disabled", false);
                            });
                    });
                },
                init: function () {
                    this.show();
                }
            },
            form: {
                object: $("#form_card").validate({
                    submitHandler: function (formElement, e) {
                        e.preventDefault();
                        var $btn = $(formElement).find("button[type='submit']");
                        $btn.addClass("m-loader m-loader--right m-loader--light").attr("disabled", true);
                        Culqi.createToken();
                        CurrentExecuteFormData = new FormData();
                    }
                }),
                events: {
                    submit: function () {
                        var $btn = $("#form_card").find("button[type='submit']");

                        $.ajax({
                            url: "/abogado/tu-cuenta/agregar-tarjeta",
                            type: "POST",
                            data: CurrentExecuteFormData,
                            contentType: false,
                            processData: false
                        })
                            .done(function (e) {
                                modal.newCard.object.modal("hide");
                                swal({
                                    type: "success",
                                    allowOutsideClick: false,
                                    title: "Éxito",
                                    text: "Tarjeta agregada satisfactoriamente.",
                                    confirmButtonText: "Aceptar"
                                }).then((result) => {
                                    lawyerCard.events.update();
                                });
                            })
                            .fail(function (e) {
                                swal({
                                    type: "error",
                                    title: "Error al actualizar los datos de facturación.",
                                    text: e.status === 502 ? "No hay respuesta del servidor" : e.responseText,
                                    confirmButtonText: "Aceptar"
                                });
                            })
                            .always(function () {
                                $btn.removeClass("m-loader m-loader--right m-loader--light").attr("disabled", false);
                            });
                    },
                    onHidden: function () {
                        modal.newCard.object.on('hidden.bs.modal', function (e) {
                            modal.newCard.form.object.resetForm();
                            $(".select2_month").val(null).trigger("change");
                        });
                    },
                    init: function () {
                        this.onHidden();
                    }
                },
                init: function () {
                    this.events.init();
                }
            },
            init: function () {
                modal.newCard.form.init();
                modal.newCard.events.init();
            }
        },
        init: function () {
            Culqi.init();
            modal.newCard.init();
        }
    };

    var select2 = {
        cardMonth: {
            init: function () {
                $(".select2_month").select2({
                    placeholder : "Seleccionar",
                    minimumResultsForSearch: -1
                });
            }
        },
        init: function () {
            this.cardMonth.init();
        }
    };

    var datepicker = {
        cardYear: {
            init: function () {
                $(".datepicker_year").datepicker({
                    format: "yyyy",
                    weekStart: 1,
                    orientation: "bottom",
                    language: "{{ app.request.locale }}",
                    keyboardNavigation: false,
                    viewMode: "years",
                    minViewMode: "years"
                });
            }
        },
        init: function () {
            this.cardYear.init();
        }
    };

    return {
        init: function () {
            lawyerCard.init();
            modal.init();
            select2.init();
            datepicker.init();
        },
        submitNewCard: function () {
            modal.newCard.form.events.submit();
        }
    };
}();

$(() => {
    paymentmethods.init();
});