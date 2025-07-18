var index = function () {
    var datatable = {
        legalCase: {
            object: null,
            options: {
                ajax: {
                    dataType: "JSON",
                    url: "/admin/casos-legales/get-casos",
                    type: "GET",
                    data: function (data) {
                        data.type = 2;
                        data.dateStart = $("#date_start_filter").val();
                        data.dateEnd = $("#date_end_filter").val();
                        data.search = $("#search").val()
                    }
                },
                columns: [
                    {
                        title: "Código",
                        data: "code",
                        orderable: false
                    },
                    {
                        title: "Cliente",
                        data: "clienteFullname",
                        orderable: false
                    },
                    {
                        title: "Abogado",
                        data: "lawyer",
                        orderable: false
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
                        orderable: false
                    },
                    {
                        title: "Especialidad",
                        data: "speciality",
                        orderable: false
                    },
                    {
                        title: "Estado",
                        data: null,
                        orderable: false,
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
                        data.type = 2;
                        data.status = 1;
                        data.dateStart = $("#date_start_filter").val();
                        data.dateEnd = $("#date_end_filter").val();
                        data.search = $("#search").val()
                    }
                },
                columns: [
                    {
                        title: "Código",
                        data: "code",
                        orderable: false
                    },
                    {
                        title: "Cliente",
                        data: "clienteFullname",
                        orderable: false
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
                        orderable: false
                    },
                    {
                        title: "Especialidad",
                        data: "speciality",
                        orderable: false
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
        waiting: {
            object: null,
            options: {
                ajax: {
                    dataType: "JSON",
                    url: "/admin/casos-legales/get-casos",
                    type: "GET",
                    data: function (data) {
                        data.type = 2;
                        data.status = 10;
                        data.dateStart = $("#date_start_filter").val();
                        data.dateEnd = $("#date_end_filter").val();
                        data.search = $("#search").val()
                    }
                },
                columns: [
                    {
                        title: "Código",
                        data: "code",
                        orderable: false
                    },
                    {
                        title: "Cliente",
                        data: "clienteFullname",
                        orderable: false
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
                        orderable: false
                    },
                    {
                        title: "Fec. Aprobación",
                        data: "approvedAt",
                        orderable: false
                    },
                    {
                        title: "Especialidad",
                        data: "speciality",
                        orderable: false
                    },
                    {
                        title: "Abogado",
                        data: "lawyer",
                        orderable: false
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
                datatable.waiting.object = $("#waiting_datatable").DataTable(datatable.waiting.options);
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
                        data.type = 2;
                        data.status = 11;
                        data.dateStart = $("#date_start_filter").val();
                        data.dateEnd = $("#date_end_filter").val();
                        data.search = $("#search").val()
                    }
                },
                columns: [
                    {
                        title: "Código",
                        data: "code",
                        orderable: false
                    },
                    {
                        title: "Cliente",
                        data: "clienteFullname",
                        orderable: false
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
                        orderable: false
                    },
                    {
                        title: "Fec. Rechazo",
                        data: "approvedAt",
                        orderable: false
                    },
                    {
                        title: "Especialidad",
                        data: "speciality",
                        orderable: false
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
        pendingpayment: {
            object: null,
            options: {
                ajax: {
                    dataType: "JSON",
                    url: "/admin/casos-legales/get-casos",
                    type: "GET",
                    data: function (data) {
                        data.type = 2;
                        data.status = 7;
                        data.dateStart = $("#date_start_filter").val();
                        data.dateEnd = $("#date_end_filter").val();
                        data.search = $("#search").val()
                    }
                },
                columns: [
                    {
                        title: "Código",
                        data: "code",
                        orderable: false
                    },
                    {
                        title: "Cliente",
                        data: "clienteFullname",
                        orderable: false
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
                        orderable: false
                    },
                    {
                        title: "Fec. Aprobación",
                        data: "approvedAt",
                        orderable: false
                    },
                    {
                        title: "Especialidad",
                        data: "speciality",
                        orderable: false
                    },
                    {
                        title: "Abogado",
                        data: "lawyer",
                        orderable: false
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
                datatable.pendingpayment.object = $("#pending_datatable").DataTable(datatable.pendingpayment.options);
            }
        },
        inprogress: {
            object: null,
            options: {
                ajax: {
                    dataType: "JSON",
                    url: "/admin/casos-legales/get-casos",
                    type: "GET",
                    data: function (data) {
                        data.type = 2;
                        data.status = 8;
                        data.dateStart = $("#date_start_filter").val();
                        data.dateEnd = $("#date_end_filter").val();
                        data.search = $("#search").val()
                    }
                },
                columns: [
                    {
                        title: "Código",
                        data: "code",
                        orderable: false
                    },
                    {
                        title: "Cliente",
                        data: "clienteFullname",
                        orderable: false
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
                        orderable: false
                    },
                    {
                        title: "Fec. pago",
                        orderable: false,
                        data: "paymentdate"
                    },
                    {
                        title: "Especialidad",
                        orderable: false,
                        data: "speciality"
                    },
                    {
                        title: "Abogado",
                        data: "lawyer",
                        orderable: false
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
                datatable.inprogress.object = $("#inprogress_datatable").DataTable(datatable.inprogress.options);
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
                        data.type = 2;
                        data.status = 9;
                        data.dateStart = $("#date_start_filter").val();
                        data.dateEnd = $("#date_end_filter").val();
                        data.search = $("#search").val()
                    }
                },
                columns: [
                    {
                        title: "Código",
                        data: "code",
                        orderable: false
                    },
                    {
                        title: "Cliente",
                        data: "clienteFullname",
                        orderable: false
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
                        orderable: false
                    },
                    {
                        title: "Fec. Cierre",
                        data: "finishat",
                        orderable: false
                    },
                    {
                        title: "Especialidad",
                        data: "speciality",
                        orderable: false
                    },
                    {
                        title: "Abogado",
                        data: "lawyer",
                        orderable: false
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
            this.revision.init();
            this.waiting.init();
            this.rejected.init();
            this.pendingpayment.init();
            this.inprogress.init();
            this.finalized.init();
        },
        reloadall: function () {
            var reload = $(".nav-link.m-tabs__link.active.show").attr("href");
            if (reload === "#all")
                this.legalCase.reload();
            if (reload === "#revision")
                this.revision.reload();
            if (reload === "#waitingconfirmation")
                this.waiting.reload();
            if (reload === "#rejectedbylawyer")
                this.rejected.reload();
            if (reload === "#pendingpaymet")
                this.pendingpayment.reload();
            if (reload === "#inprogress")
                this.inprogress.reload();
            if (reload === "#finalized")
                this.finalized.reload();
        }
    };
    var datepicker = {
        init: function () {
            $("#date_start_filter").datepicker({
                endDate: '+0d',
                clearBtn: false,
                orientation: "bottom",
            }).on("change", function () {
                datatable.reloadall();
            });

            $("#date_end_filter").datepicker({
                endDate: '+0d',
                clearBtn: false,
                orientation: "bottom",
            }).on("change", function () {
                datatable.reloadall();
            });
        }
    };
    var search = {
        init: function () {
            $("#search").doneTyping(function () {
                datatable.reloadall();
            });
        }
    };
    return {
        init: function () {
            datatable.init();
            datepicker.init();
            search.init();
        }
    };
}();

$(() => {
    index.init();
});