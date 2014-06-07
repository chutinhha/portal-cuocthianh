(function ($) {
	$.fn.KyersShowBox = function(options){
		defaults = {
				"closeButton":"#kshowbox-btn-cancel",
				"clickOutToClose": false,
				"closeBox": false,
				"boxWidth": $(window).width()/1.5
			};
		var settings = $.extend(defaults,options||{});
		$(this).before('<div class="overlay-box"></div>');
		$(this).append('<div class="close-button"></div>');
		var $overlay = $(".overlay-box");
		var $showbox = $(this);
		var $windowheight = $(window).height();
		if(settings.closeBox == true)
		{
			CloseBox();
		}
		else
		{
			$overlay.fadeIn(function(){
				$showbox.fadeIn();
				$showbox.css({"width": settings.boxWidth});
				$showbox.css({"margin-left": $showbox.width()/4, "top": $(document).scrollTop(), "margin-top": $(window).innerHeight()*(0.125)});
				$overlay.css({"height": window.innerHeight, "width": window.innerWidth});
			});
		}
		
		$(window).bind("load resize", function(){
			$showbox.css({"margin-left": $showbox.width()/4, "top": $(document).scrollTop(), "margin-top": $(window).innerHeight()*(0.125)});
			$overlay.css({"height": window.innerHeight, "width": window.innerWidth});
			if($(window).width()<$showbox.width()+$showbox.offset().left)
			{
				$showbox.css({"margin-left": $(document).scrollLeft()*2});
				$showbox.css({"margin-left": $(document).scrollLeft()*2 + $showbox.offset().left});
			}
		});
		$(window).bind("scroll",function(){
			if($(window).scrollTop()>$(window).innerHeight())
			{
				$showbox.css({"top": $(document).scrollTop(), "margin-top": 0});
			}
			else
			{ 
				$showbox.css({"top": $(document).scrollTop(), "margin-top": $(window).innerHeight()*(0.125)});
			}
		});
		$(this).mouseover(function(e){
			if($(document).scrollTop() == 0)
			{
				$("body").css({"overflow": "hidden"});
				$("html").css({"overflow": "hidden"});
				$overlay.css({"overflow": "hidden"});
			}
		});
		$(this).mouseleave(function(e){
			$("html").css({"overflow": "auto"});
			$("html").css({"overflow": "auto"});
			$overlay.css({"overflow": "auto"});
		});
		$(settings.closeButton).click(function(){
			CloseBox();
		});
		
		if(settings.clickOutToClose==true)
		{
			$overlay.click(function(){
				CloseBox();
			});
		}
		$(".close-button").click(function(){
			CloseBox();
		});
		function CloseBox(){
			$showbox.fadeOut(function(){
				$overlay.fadeOut("fast", function(){
					$overlay.remove();
				});
			});
		};
	}
})(jQuery);