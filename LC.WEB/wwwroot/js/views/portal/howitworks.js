var AboutUs = function () {
    var howitworks = {
        init: function() {
            $.ajax({
                url: `/get-como-funciona-cliente`,
                type: "Get"
            }).done(function (result) {
                $("#how-it-works-client-steps").html(result);
            });
        }
    };
    var howitworksSummary = {
        init: function () {
            $.ajax({
                url: `/get-como-funciona-resumen-cliente`,
                type: "Get"
            }).done(function (result) {
                $("#how-it-works-summary-client-steps").html(result);
            });
        }
    };
    var frequentquestion = {
        init: function () {
            $.ajax({
                url: `/get-preguntas-frecuentes-clientes`,
                type: "Get"
            }).done(function (result) {
                $("#frequent-questions").html(result);
            });
        }
    };
    return {
        init: function () {
            howitworks.init();
            frequentquestion.init();
            howitworksSummary.init();
        }
    };
}();

$(function () {
    AboutUs.init();
});
