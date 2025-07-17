var detail = function () {

    var events = {
        onAddBlog: function () {
            $("#add_blog").on("click", function () {
                var $btn = $(this);
                var id = $(this).data("id");
                $btn.addLoader();
                $.ajax({
                    url: `/admin/blog/agregar-blog-publicacion-abogado?id=${id}`,
                    type: "POST"
                })
                    .done(function () {
                        window.location.href = "/admin/blog";
                    }).
                    fail(function (e) {
                        swal({
                            type: "error",
                            title: "Error al agregar al blog..",
                            text: e.status === 502 ? "No hay respuesta del servidor" : e.responseText,
                            confirmButtonText: "Aceptar"
                        });
                    }).
                    always(function () {
                        $btn.removeLoader();
                    });
            });
        },
        init: function () {
            this.onAddBlog();
        }
    };

    return {
        init: function () {
            events.init();
        }
    };
}();

$(() => {
    detail.init();
});

