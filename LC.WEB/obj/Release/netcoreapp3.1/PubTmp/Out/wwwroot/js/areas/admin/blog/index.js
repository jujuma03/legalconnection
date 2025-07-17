var index = function () {

    var datatable = {
        blog: {
            object: null,
            options: {
                ajax: {
                    url: "/admin/blog/blog-get",
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
                        data: "lawyer",
                        title: "Abogado"
                    },
                    {
                        data: "publicationDate",
                        title: "Fec. Publicación"
                    },
                    {
                        data: null,
                        title: "Opciones",
                        render: function (row) {
                            var tpm = `<a href="/admin/blog/blog-detalle?id=${row.id}" class="btn btn-primary btn-sm m-btn  m-btn m-btn--icon"><span><i class="la la-eye"></i><span>Detalle</span></span></a>`;
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
            this.blog.init();
        }
    };

    return {
        init: function () {
            datatable.init();
        }
    };
}();

$(() => {
    index.init();
});