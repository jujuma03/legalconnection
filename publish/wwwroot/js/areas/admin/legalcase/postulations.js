var index = function () {
    var datatable = {
        legalCase: {
            object: null,
            options: {
                responsive: true,
                processing: true,
                serverSide: true,
                dom: 'Bfrtip',
                buttons: [
                    {
                        text: '<span style="font-size:17px;font-family:SourceSansPro;font-weight:bold">Descargar en Excel</span>',
                        className: 'btn btn-primary',
                        aling: 'right',
                        action: function () {
                            var button = $(this)[0].node;
                            $(button).addClass("m-loader m-loader--right m-loader--secondary").attr("disabled", true);
                            $.fileDownload(`/admin/casos-legales/get-casos/get-excel`,
                                {
                                    httpMethod: 'GET',
                                    data: {
                                        type: 1,
                                        date: $("#date_filter").val(),
                                        search: $("#search").val()
                                    },
                                    successCallback: function () {
                                        toastr.success("Archivo descargado satisfactoriamente", "Éxito");
                                    }
                                }
                            ).done(function () {
                                toastr.success("Archivo descargado satisfactoriamente", "Éxito");
                            })
                                .fail(function () {
                                    toastr.error("Error al descargar el archivo", "Error");
                                })
                                .always(function () {
                                    $(button).removeClass("m-loader m-loader--right m-loader--secondary").attr("disabled", false);
                                });
                        }
                    }
                ],
                ajax: {
                    dataType: "JSON",
                    url: "/admin/casos-legales/get-casos",
                    type: "GET",
                    data: function (data) {
                        data.type = 1;
                        data.dateStart = $("#date_start_filter").val();
                        data.dateEnd = $("#date_end_filter").val();
                        data.search = $("#search").val()
                    }
                },
                columns: [
                    {
                        title: "Código",
                        data: "code",
                    },
                    {
                        title: "Cliente",
                        data: "clienteFullname",
                    },
                    {
                        title: "Lugar",
                        data: null,
                        orderable: false,
                        render: function (row) {
                            return `${row.department} - ${row.province}`;
                        }
                    },
                    {
                        title: "Fec. Envio",
                        data: "createdAt",
                    },
                    {
                        title: "Fec. Validación",
                        data: "approvedAt",
                    },
                    {
                        title: "Especialidad",
                        data: "speciality",
                        orderable: false,
                    },
                    {
                        title: "Estado",
                        data: null,
                        render: function (row) {
                            return `<span class="m-badge m-badge--${row.statusColor} m-badge--wide">${row.statusName}</span>`;
                        }
                    },
                    {
                        title: "Opciones",
                        data: null,
                        orderable: false,
                        render: function (data) {
                            var tpm = "";
                            tpm += `<a href="/admin/casos-legales/${data.id}/detalles" class="ml-1 btn btn-primary btn-sm m-btn m-btn m-btn--icon"><span><i class="la la-eye"></i><span>Revisar</span></span></a>`;
                            return tpm;
                        }
                    }
                ]
            },
            reload: function () {
                this.object.ajax.reload();
            },
            init: function () {
                datatable.legalCase.object = $("#legal_cases_datatable").DataTable(datatable.legalCase.options);
            }
        },
        revision: {
            object: null,
            options: {
                ajax: {
                    dataType: "JSON",
                    url: "/admin/casos-legales/get-casos",
                    type: "GET",
                    data: function (data) {
                        data.status = 1;
                        data.type = 1;
                        data.dateStart = $("#date_start_filter").val();
                        data.dateEnd = $("#date_end_filter").val();
                        data.search = $("#search").val()
                    }
                },
                columns: [
                    {
                        title: "Código",
                        data: "code",
                    },
                    {
                        title: "Cliente",
                        data: "clienteFullname",
                    },
                    {
                        title: "Lugar",
                        data: null,
                        orderable: false,
                        render: function (row) {
                            return `${row.department} - ${row.province}`;
                        }
                    },
                    {
                        title: "Fec. Envio",
                        data: "createdAt",
                    },
                    {
                        title: "Especialidad",
                        data: "speciality",
                        orderable: false,
                    },
                    {
                        title: "Opciones",
                        data: null,
                        orderable: false,
                        render: function (data) {
                            var tpm = "";
                            tpm += `<a href="/admin/casos-legales/${data.id}/detalles" class="ml-1 btn btn-primary btn-sm m-btn m-btn m-btn--icon"><span><i class="la la-eye"></i><span>Revisar</span></span></a>`;
                            return tpm;
                        }
                    }
                ]
            },
            reload: function () {
                this.object.ajax.reload();
            },
            init: function () {
                datatable.revision.object = $("#revision_datatable").DataTable(datatable.revision.options);
            }
        },
        lookinglawyer: {
            object: null,
            options: {
                ajax: {
                    dataType: "JSON",
                    url: "/admin/casos-legales/get-casos",
                    type: "GET",
                    data: function (data) {
                        data.status = 5;
                        data.type = 1;
                        data.dateStart = $("#date_start_filter").val();
                        data.dateEnd = $("#date_end_filter").val();
                        data.search = $("#search").val()
                    }
                },
                columns: [
                    {
                        title: "Código",
                        data: "code",
                    },
                    {
                        title: "Cliente",
                        data: "clienteFullname",
                    },
                    {
                        title: "Lugar",
                        data: null,
                        orderable: false,
                        render: function (row) {
                            return `${row.department} - ${row.province}`;
                        }
                    },
                    {
                        title: "Fec. Envio",
                        data: "createdAt",
                    },
                    {
                        title: "Especialidad",
                        data: "speciality",
                        orderable: false,
                    },
                    {
                        title: "Opciones",
                        data: null,
                        orderable: false,
                        render: function (data) {
                            var tpm = "";
                            tpm += `<a href="/admin/casos-legales/${data.id}/detalles" class="ml-1 btn btn-primary btn-sm m-btn m-btn m-btn--icon"><span><i class="la la-eye"></i><span>Revisar</span></span></a>`;
                            return tpm;
                        }
                    }
                ]
            },
            reload: function () {
                this.object.ajax.reload();
            },
            init: function () {
                datatable.lookinglawyer.object = $("#looking_lawyer_datatable").DataTable(datatable.lookinglawyer.options);
            }
        },
        pendingpayment: {
            object: null,
            options: {
                ajax: {
                    dataType: "JSON",
                    url: "/admin/casos-legales/get-casos",
                    type: "GET",
                    data: function (data) {
                        data.status = 7;
                        data.type = 1;
                        data.dateStart = $("#date_start_filter").val();
                        data.dateEnd = $("#date_end_filter").val();
                        data.search = $("#search").val()
                    }
                },
                columns: [
                    {
                        title: "Código",
                        data: "code",
                    },
                    {
                        title: "Cliente",
                        data: "clienteFullname",
                    },
                    {
                        title: "Lugar",
                        data: null,
                        orderable: false,
                        render: function (row) {
                            return `${row.department} - ${row.province}`;
                        }
                    },
                    {
                        title: "Fec. Envio",
                        data: "createdAt",
                    },
                    {
                        title: "Especialidad",
                        data: "speciality",
                        orderable: false,
                    },
                    {
                        title: "Opciones",
                        data: null,
                        orderable: false,
                        render: function (data) {
                            var tpm = "";
                            tpm += `<a href="/admin/casos-legales/${data.id}/detalles" class="ml-1 btn btn-primary btn-sm m-btn m-btn m-btn--icon"><span><i class="la la-eye"></i><span>Revisar</span></span></a>`;
                            return tpm;
                        }
                    }
                ]
            },
            reload: function () {
                this.object.ajax.reload();
            },
            init: function () {
                datatable.pendingpayment.object = $("#pending_payment_datatable").DataTable(datatable.pendingpayment.options);
            }
        },
        rejected: {
            object: null,
            options: {
                ajax: {
                    dataType: "JSON",
                    url: "/admin/casos-legales/get-casos",
                    type: "GET",
                    data: function (data) {
                        data.status = 3;
                        data.type = 1;
                        data.dateStart = $("#date_start_filter").val();
                        data.dateEnd = $("#date_end_filter").val();
                        data.search = $("#search").val()
                    }
                },
                columns: [
                    {
                        title: "Código",
                        data: "code",
                    },
                    {
                        title: "Cliente",
                        data: "clienteFullname",
                    },
                    {
                        title: "Lugar",
                        data: null,
                        orderable: false,
                        render: function (row) {
                            return `${row.department} - ${row.province}`;
                        }
                    },
                    {
                        title: "Fec. Envio",
                        data: "createdAt",
                    },
                    {
                        title: "Fec. Rechazo",
                        data: "approvedAt",
                    },
                    {
                        title: "Especialidad",
                        data: "speciality",
                        orderable: false,
                    },
                    {
                        title: "Opciones",
                        data: null,
                        orderable: false,
                        render: function (data) {
                            var tpm = "";
                            tpm += `<a href="/admin/casos-legales/${data.id}/detalles" class="ml-1 btn btn-primary btn-sm m-btn m-btn m-btn--icon"><span><i class="la la-eye"></i><span>Revisar</span></span></a>`;
                            return tpm;
                        }
                    }
                ]
            },
            reload: function () {
                this.object.ajax.reload();
            },
            init: function () {
                datatable.rejected.object = $("#rejected_datatable").DataTable(datatable.rejected.options);
            }
        },
        validated: {
            object: null,
            options: {
                ajax: {
                    dataType: "JSON",
                    url: "/admin/casos-legales/get-casos",
                    type: "GET",
                    data: function (data) {
                        data.status = 2;
                        data.type = 1;
                        data.dateStart = $("#date_start_filter").val();
                        data.dateEnd = $("#date_end_filter").val();
                        data.search = $("#search").val()
                    }
                },
                columns: [
                    {
                        title: "Código",
                        data: "code",
                    },
                    {
                        title: "Cliente",
                        data: "clienteFullname",
                    },
                    {
                        title: "Lugar",
                        data: null,
                        orderable: false,
                        render: function (row) {
                            return `${row.department} - ${row.province}`;
                        }
                    },
                    {
                        title: "Fec. Envio",
                        data: "createdAt",
                    },
                    {
                        title: "Fec. Validación",
                        data: "approvedAt",
                    },
                    {
                        title: "Especialidad",
                        data: "speciality",
                        orderable: false,
                    },
                    {
                        title: "Opciones",
                        data: null,
                        orderable: false,
                        render: function (data) {
                            var tpm = "";
                            tpm += `<a href="/admin/casos-legales/${data.id}/detalles" class="ml-1 btn btn-primary btn-sm m-btn m-btn m-btn--icon"><span><i class="la la-eye"></i><span>Revisar</span></span></a>`;
                            return tpm;
                        }
                    }
                ]
            },
            reload: function () {
                this.object.ajax.reload();
            },
            init: function () {
                datatable.validated.object = $("#validated_datatable").DataTable(datatable.validated.options);
            }
        },
        finalized: {
            object: null,
            options: {
                ajax: {
                    dataType: "JSON",
                    url: "/admin/casos-legales/get-casos",
                    type: "GET",
                    data: function (data) {
                        data.status = 9;
                        data.type = 1;
                        data.dateStart = $("#date_start_filter").val();
                        data.dateEnd = $("#date_end_filter").val();
                        data.search = $("#search").val()
                    }
                },
                columns: [
                    {
                        title: "Código",
                        data: "code",
                    },
                    {
                        title: "Cliente",
                        data: "clienteFullname",
                    },
                    {
                        title: "Lugar",
                        data: null,
                        orderable: false,
                        render: function (row) {
                            return `${row.department} - ${row.province}`;
                        }
                    },
                    {
                        title: "Fec. Envio",
                        data: "createdAt",
                    },
                    {
                        title: "Fec. Validación",
                        data: "approvedAt",
                    },
                    {
                        title: "Especialidad",
                        data: "speciality",
                        orderable: false,
                    },
                    {
                        title: "Opciones",
                        data: null,
                        orderable: false,
                        render: function (data) {
                            var tpm = "";
                            tpm += `<a href="/admin/casos-legales/${data.id}/detalles" class="ml-1 btn btn-primary btn-sm m-btn m-btn m-btn--icon"><span><i class="la la-eye"></i><span>Revisar</span></span></a>`;
                            return tpm;
                        }
                    }
                ]
            },
            reload: function () {
                this.object.ajax.reload();
            },
            init: function () {
                datatable.finalized.object = $("#finalized_datatable").DataTable(datatable.finalized.options);
            }
        },
        init: function () {
            this.legalCase.init();
            this.rejected.init();
            this.revision.init();
            this.validated.init();
            this.lookinglawyer.init();
            this.finalized.init();
            this.pendingpayment.init();
        },
        reloadall: function () {
            var reload = $(".nav-link.m-tabs__link.active.show").attr("href");
            if (reload === "#all")
                this.legalCase.reload();
            if (reload === "#revision")
                this.rejected.reload();
            if (reload === "#rejected")
                this.revision.reload();
            if (reload === "#validated")
                this.validated.reload();

            if (reload === "#lookinglawyer")
                this.lookinglawyer.reload();
            if (reload === "#finalized")
                this.finalized.reload();
            if (reload === "#pendingpayment")
                this.pendingpayment.reload();
        }
    };
    var datepicker = {
        init: function () {
            var t;
            t = mUtil.isRTL() ? { leftArrow: '<i class="la la-angle-right"></i>', rightArrow: '<i class="la la-angle-left"></i>' }
                : { leftArrow: '<i class="la la-angle-left"></i>', rightArrow: '<i class="la la-angle-right"></i>' };

            $("#m_datepicker_5").datepicker({ rtl: mUtil.isRTL(), todayHighlight: !0, templates: t });

        }
    };
    var search = {
        init: function () {
            $("#search").doneTyping(function () {
                datatable.reloadall();
            });
        }
    };

    var event = {
        init: function () {
            $(document).on('change', '#m_datepicker_5', function () { // Change event for datepicker
                datatable.reloadall();
            });
        }
    };
    return {
        init: function () {
            datatable.init();
            datepicker.init();
            search.init();
            event.init();
        }
    };
}();

$(() => {
    index.init();
});