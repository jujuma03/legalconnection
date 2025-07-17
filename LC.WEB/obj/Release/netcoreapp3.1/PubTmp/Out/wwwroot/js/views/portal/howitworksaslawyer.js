var AboutUs = function () {
    var howitworks = {
        init: function() {
            $.ajax({
                url: `/get-como-funciona-abogado`,
                type: "Get"
            }).done(function (result) {
                $("#how-it-works-lawyer-steps").html(result);
            });
        }
    };
    var howitworksSummary = {
        init: function () {
            $.ajax({
                url: `/get-como-funciona-resumen-abogado`,
                type: "Get"
            }).done(function (result) {
                $("#how-it-works-summary-lawyer-steps").html(result);
            });
        }
    };
    var frequentquestion = {
        init: function () {
            $.ajax({
                url: `/get-preguntas-frecuentes-abogados`,
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
