var lawyerpublication = function () {

    var datatable = {
        publications: {
            object: null,
            options: {
                ajax: {
                    url: "/admin/blog/publicaciones-abogado-get",
                    type: "GET",
                    data: function (data) {
                        data.searchValue = $("#search").val();
                    }
                },
                columns: [
                    {
                        data: "title",
                        title: "Titulo"
                    },
                    {
                        data: null,
                        title: "Abogado",
                        render: function (row) {
                            return `${row.name} ${row.surnames}`;
                        }
                    },
                    {
                        data: "publicationDate",
                        title: "Fec. Publicación"
                    },
                    {
                        data: null,
                        title: "Opciones",
                        render: function (row) {
                            var tpm = `<a href="/admin/blog/publicaciones-abogado-detalle?id=${row.id}" class="btn btn-primary btn-sm m-btn  m-btn m-btn--icon"><span><i class="la la-eye"></i><span>Detalle</span></span></a>`;
                            return tpm;
                        }
                    },
                ]
            },
            reload: function () {
                this.object.ajax.reload();
            },
            init: function () {
                this.object = $("#tbl-data").DataTable(this.options);
            }
        },
        init: function () {
            this.publications.init();
        }
    };

    var events = {
        onSearch: function () {
            $("#search").doneTyping(function () {
                datatable.publications.reload();
            });
        },
        init: function () {
            this.onSearch();
        }
    };

    return {
        init: function () {
            datatable.init();
            events.init();
        }
    };
}();

$(() => {
    lawyerpublication.init();
});