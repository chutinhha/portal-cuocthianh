(function($){
	
}(jQuery));
(function($){
	$(document).ready(function(e) {
	   $(".belloSlideShow").belloSlideShow();
	   $(".saleSlide").belloSlideShow({"navControl": true ,"orientation":"horizontal", "randomTime": true});
	   $(".slideProduct").belloSlideSet();
    });
}(jQuery));