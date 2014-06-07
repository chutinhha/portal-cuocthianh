(function ($) {
    $.topAlertPopup = function (options) {
        var defaults = { "text": "Thông báo", "typeAlert": "success" };
        var settings = $.extend(defaults, options || {});
        //clearInterval($interval);
        $("#TopAlertPopup").remove();
        var $interval;
        if (settings.typeAlert == "error") {
            $("body").append('<div id="TopAlertPopup"><span class="text"></span></div>');
            $("#TopAlertPopup").find(".text").text(settings.text);
            $("#TopAlertPopup").addClass(settings.typeAlert).slideDown("slow");
            $interval = setInterval(function () {
                $("#TopAlertPopup").slideUp("slow", function () {
                    $("#TopAlertPopup").remove();
                });
            }, 3000);
        }
        if (settings.typeAlert == "success") {
            $("body").append('<div id="TopAlertPopup"><span class="text"></span></div>');
            $("#TopAlertPopup").find(".text").text(settings.text);
            $("#TopAlertPopup").addClass(settings.typeAlert).slideDown("slow");
            $interval = setInterval(function () {
                $("#TopAlertPopup").slideUp("slow", function () {
                    $("#TopAlertPopup").remove();
                });
            }, 3000);
        }
    };
})(jQuery);