var blogs = function () {
    var publications = {
        activePage: 1,
        recordsPerDraw: 6,
        init: function () {
            $.ajax({
                url: `/get-blogs`,
                data: {
                    //pagination
                    srch: $("#search").val(),
                    page: publications.activePage,
                    rpdraw: publications.recordsPerDraw,
                    //
                },
                type: "Get"
            }).done(function (result) {
                $("#blogs-view").html(result);

                var maxLength = 497;
                $(".show-read-more").each(function (index, value) {
                    var id = $($(".m-portlet__body")[index]).data("id");
                    var myStr = $(this).text();
                    if ($.trim(myStr).length > maxLength) {
                        var newStr = myStr.substring(0, maxLength);
                        var removedStr = myStr.substring(maxLength, $.trim(myStr).length);
                        $(this).empty().html(newStr);
                        $(this).append(` <a href="/blog-detalle?id=${id}" class="read-more">Ver más...</a>`);
                        //$(this).append('<span class="more-text">' + removedStr + '</span>');
                    }
                });
                //$(".read-more").click(function () {
                //    $(this).siblings(".more-text").contents().unwrap();
                //    $(this).remove();
                //});
            });
        },
        events: function () {
            $("#blogs-view").on("click", ".previous-item", function () {
                publications.activePage--;
                publications.init();
            });
            $("#blogs-view").on("click", ".next-item", function () {
                publications.activePage++;
                publications.init();
            });
        }
    };
    return {
        init: function () {
            publications.init();
            publications.events();
        }
    };
}();

$(function () {
    blogs.init();
});
