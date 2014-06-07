(function($){
	 $.fn.enableControl = function (data) {
        $.each(data, function (index, value) {
            $(value).attr("disabled", true);
        });
        $(this).on("click", function () {
            if ($(this).hasClass("active")) {
                $.each(data, function (index, value) {
                    $(value).attr("disabled", true);
                });
                $(this).removeClass("active").val("Sửa thông tin");
            } else {
                $.each(data, function (index, value) {
                    $(value).attr("disabled", false);
                });
                $(this).addClass("active").val("Khóa chỉnh sửa");
            }
        });
    }
}(jQuery));
(function($){
	$(document).ready(function(e) {
	   $(".showcat-button").showDropDown({"showDiv":".nav-category","eventX":"mouseover", "leaveToOut":true});
	   $(".tab-head").find("a").kyersTab();
	   $("#btn-change-pass").enableControl(["#txtUsername", "#txtPassWord","#txtNewPassWord", "#txtReNewPassWord"]);
    });
}(jQuery));