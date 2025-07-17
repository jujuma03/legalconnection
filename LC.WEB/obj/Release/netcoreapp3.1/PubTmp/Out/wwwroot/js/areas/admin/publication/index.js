var index = function () {
    var datatable = {
        publication: {
            object: null,
            options: {
                ajax: {
                    url: "/admin/publicaciones/get",
                    type: "GET",
                    data: function (data) {
                        data.search = $("#search").val();
                    }
                },
                columns: [
                    {
                        data: null,
                        title: "Abogado",
                        render: function (row) {
                            return `${row.name} ${row.surnames}`;
                        }
                    },
                    {
                        data: "title",
                        title: "Título"
                    },
                    {
                        data: null,
                        title: "Estado",
                        render: function (row) {
                            var badge = "";
                            if (row.status == 1)
                                badge = "badge-primary";
                            if (row.status == 2)
                                badge = "badge-success";
                            if (row.status == 3)
                                badge = "badge-danger";

                            return `<span class="badge ${badge}">${row.statusName}</span>`;
                        }
                    },
                    {
                        data: "publicationDate",
                        title: "Fec. Publicación"
                    },
                    {
                        data: null,
                        title: "Opciones",
                        render: function (data) {
                            return `<button data-id="${data.id}" data-status="${data.status}" class="btn btn-sm btn-info btn-detail"> <i class="la la-eye"></i> </button>`;
                        }
                    }
                ]
            },
            events: {
                onView: function () {
                    datatable.publication.object.on("click", ".btn-detail", function () {
                        var id = $(this).data("id");
                        var status = $(this).data("status");
                        if (status == 1) {
                            modal.publication.object.find(".reject_publication").show();
                            modal.publication.object.find(".validate_publication").show();
                        }
                        if (status == 2) {
                            modal.publication.object.find(".reject_publication").show();
                            modal.publication.object.find(".validate_publication").hide();
                        }
                        if (status == 3) {
                            modal.publication.object.find(".reject_publication").hide();
                            modal.publication.object.find(".validate_publication").show();
                        }
                        modal.publication.show(id);
                    });
                },
                init: function () {
                    this.onView();
                }
            },
            reload: function () {
                this.object.ajax.reload();
            },
            init: function () {
                this.object = $("#ajax_data").DataTable(this.options);
                this.events.init();
            }
        },
        init: function () {
            this.publication.init();
        }
    };
    var summernote = {
        defaultOptions: {
            lang: "es-ES",
            airMode: false,
            height: 250,
        },
    };
    var modal = {
        publication: {
            object: $("#publication_detail"),
            show: function (id) {
                modal.publication.object.find(":input").attr("disabled", true);
                modal.publication.object.modal("show");
                $.ajax({
                    url: `/admin/publicaciones/get-publicacion?id=${id}`,
                    type: "GET"
                })
                    .done(function (e) {
                        $('#Image').attr('src', '/documentos/' + e.image);
                        //modal.publication.object.find("[name='Image']").src= e.image;
                        modal.publication.object.find("[name='PublicationId']").val(e.id);
                        modal.publication.object.find("[name='Lawyer']").val(e.lawyer);
                        modal.publication.object.find("[name='Description']").val(e.description).summernote(summernote.defaultOptions).summernote('disable');
                        modal.publication.object.find("[name='Title']").val(e.title);
                        modal.publication.object.find("[name='Topic']").val(e.topic);
                        modal.publication.object.find("[name='PublicationDate']").val(e.publicationDate);

                        modal.publication.object.find(":input").attr("disabled", false);
                    });
            },
            events: {
                onApprove: function () {
                    modal.publication.object.on("click", ".validate_publication", function () {
                        var $btn = $(this);
                        $btn.addClass("m-loader m-loader--right m-loader--light").attr("disabled", true);
                        var id = modal.publication.object.find("[name='PublicationId']").val();
                        $.ajax({
                            url: `/admin/publicaciones/aprobar?id=${id}`,
                            type: "POST",
                        })
                            .done(function (e) {
                                modal.publication.object.modal("hide");
                                datatable.publication.reload();
                                swal({
                                    type: "success",
                                    allowOutsideClick: false,
                                    title: "Éxito",
                                    text: "Publicación actualizada satisfactoriamente.",
                                    confirmButtonText: "Aceptar"
                                });
                            })
                            .fail(function (e) {
                                swal({
                                    type: "error",
                                    title: "Error al guardar los datos.",
                                    text: e.status === 502 ? "No hay respuesta del servidor" : e.responseText,
                                    confirmButtonText: "Aceptar"
                                });
                            })
                            .always(function () {
                                $btn.removeClass("m-loader m-loader--right m-loader--light").attr("disabled", false);
                            });
                    });
                },
                onReject: function () {
                    modal.publication.object.on("click", ".reject_publication", function () {
                        var $btn = $(this);
                        $btn.addClass("m-loader m-loader--right m-loader--light").attr("disabled", true);
                        var id = modal.publication.object.find("[name='PublicationId']").val();
                        $.ajax({
                            url: `/admin/publicaciones/rechazar?id=${id}`,
                            type: "POST",
                        })
                            .done(function (e) {
                                modal.publication.object.modal("hide");
                                datatable.publication.reload();
                                swal({
                                    type: "success",
                                    allowOutsideClick: false,
                                    title: "Éxito",
                                    text: "Publicación rechazada satisfactoriamente.",
                                    confirmButtonText: "Aceptar"
                                });
                            })
                            .fail(function (e) {
                                swal({
                                    type: "error",
                                    title: "Error al guardar los datos.",
                                    text: e.status === 502 ? "No hay respuesta del servidor" : e.responseText,
                                    confirmButtonText: "Aceptar"
                                });
                            })
                            .always(function () {
                                $btn.removeClass("m-loader m-loader--right m-loader--light").attr("disabled", false);
                            });
                    });
                },
                init: function () {
                    this.onApprove();
                    this.onReject();
                }
            },
            init: function () {
                this.events.init();
            }
        },
        init: function () {
            this.publication.init();
        }
    };

    var events = {
        onSearch: function () {
            $("#search").doneTyping(function () {
                datatable.publication.reload();
            });
        },
        init: function () {
            this.onSearch();
        }
    };

    return {
        init: function () {
            datatable.init();
            modal.init();
            events.init();
        }
    };
}();

$(() => {
    index.init();
});