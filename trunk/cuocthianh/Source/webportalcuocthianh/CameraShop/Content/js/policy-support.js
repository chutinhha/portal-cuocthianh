(function($){
	$.fn.chooseCatFac=function(){
		$.each($(this), function(){
			if($(this).is(':checked')){
				$(this).parent("li").addClass("selected");
			}
			else{
				$(this).parent("li").removeClass("selected");
			}
		});
		
		$(this).on("change", function(){
			if($(this).is(':checked')){
				$(this).parent("li").addClass("selected");
			}
			else{
				$(this).parent("li").removeClass("selected");
			}
		});
	}
}(jQuery));
(function($){
	$(document).ready(function(e) {
	   $(".showcat-button").showDropDown({"showDiv":".nav-category","eventX":"mouseover", "leaveToOut":true});
	   $(".chk").chooseCatFac();
    });
}(jQuery));