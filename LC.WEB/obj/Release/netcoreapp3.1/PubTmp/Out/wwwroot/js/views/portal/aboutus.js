var AboutUs = function () {
    var missionvision = {
        init: function() {
            $.ajax({
                url: `/get-mision-vision`,
                type: "Get"
            }).done(function (result) {
                $("#mission-vision").html(result);
            });
        }
    };
    var ourteam = {
        init: function () {
            $.ajax({
                url: `/get-nuestro-equipo`,
                type: "Get"
            }).done(function (result) {
                $("#our-team").html(result);
            });
        }
    };
    return {
        init: function () {
            missionvision.init();
            ourteam.init();
        }
    };
}();

$(function () {
    AboutUs.init();
});
