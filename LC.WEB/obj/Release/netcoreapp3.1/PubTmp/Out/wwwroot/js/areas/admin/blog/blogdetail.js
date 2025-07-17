var blogdetail = function () {
    var summernote = {
        defaultOptions: {
            lang: "es-ES",
            airMode: false,
            height: 250,
        },
        init: function (){
            $("#Description").summernote(summernote.defaultOptions).summernote('disable');
        }
    };
    var events = {
        onDelete: function () {
            $("#delete_blog").on("click", function () {
                console.log("asdasd");
                var $btn = $(this);
                var id = $(this).data("id");
                $btn.addLoader();
                $.ajax({
                    url: `/admin/blog/blog-eliminar?id=${id}`,
                    type: "POST"
                })
                    .done(function () {
                        window.location.href = "/admin/blog";
                    }).
                    fail(function (e) {
                        swal({
                            type: "error",
                            title: "Error al eliminar del blog..",
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
            this.onDelete();
        }
    };

    return {
        init: function () {
            events.init();
            summernote.init();
        }
    };
}();

$(() => {
    blogdetail.init();
});