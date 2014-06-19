$.fn.kyersTab=function(options){
		var defaults = {"tabItem":".tab-item"};
		var settings = $.extend(defaults, options ||{});
		//Thêm index cho tab head
		var sIndex=0;
		$(this).parent("li").siblings("li").first().addClass("active");
		$.each($(this).parent("li").siblings("li"), function(){
			sIndex +=1;
			$(this).attr("index", sIndex);
		});
		//Thêm Index cho tab Item
		sIndex=0;
		$(settings.tabItem).first().addClass("active");
		$.each($(settings.tabItem), function(){
			sIndex +=1;
			$(this).attr("index", sIndex);
		});
		$(this).on("click", function(e){
			e.preventDefault()?e.preventDefault():e.retuenValue;
			$(this).parent("li").siblings("li").removeClass("active");
			$(this).parent("li").addClass("active");
			var currentIndex=$(this).parent("li").attr("index");
			$(settings.tabItem).removeClass("active");
			$(settings.tabItem + "[index=" + currentIndex + "]").addClass("active");

		});
	}