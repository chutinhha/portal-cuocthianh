$.fn.belloSlideShow=function(options){
	$.each($(this), function(){
		var slideOuter= $(this);
		//2 biến defaults và settings dùng để custom
		var defaults = {"eventX":"mouseover", 
			"eventNot":"mouseleave",
			"slideItem": "slide-item", 
			"timmer":6000, 
			"easing": 400,
			"autoPlay": true,
			"navControl": false,
			"orientation":"verticle",
			"randomTime": false};
			
		var settings = $.extend(defaults, options ||{});
		
		//Biến dùng để lưu lại object html của ảnh chính
		var mainItem = $(this).find(".slide-block-image").find("li");
		
		var index=0;
		//Add index cho ảnh chính
		$.each(mainItem, function(){
			$(this).attr("index", index).addClass(settings.slideItem);
			index++;
		});
		
		//Kiểm tra xem có thanh Nav Thumbnail hay không, nếu không có thì add
		//Chủ yếu nếu xài thì không sao, còn không xài cũng dùng để xét giá trị next, prev để autoPlay
		if($(this).find(".slide-block-nav").length===0){
			$(this).append($("<div/>", {
				"class":"slide-block-nav"
			}));
			$(this).find(".slide-block-nav").append($("<ul/>", {
				
			}));
			$.each(mainItem, function(){
				slideOuter.find(".slide-block-nav").find("ul").append("<li/>",{});
			});
		}
		//Biến dùng để lưu lại object html của thanh điều hướng
		var navItem = $(this).find(".slide-block-nav").find("li");
		//Trả về 0 để add Index cho Navbar
		index=0;
		$.each(navItem, function(){
			$(this).attr("index", index);
			index++;
		});
		//Init first class active
					
		mainItem.first().addClass("active");
		navItem.first().addClass("active");
		
		//Thêm nav Control ! cái này khác với Nav Thumbnail, cái này chỉ gồm 2 button Next và Preview
		if(settings.navControl===true){
			$(this).prepend($("<div/>", {
				"class":"slide-nav-control"
			}));
			$(this).find(".slide-nav-control").prepend($("<div/>", {
				"class":"btn btn-next-slide"
			}));
			$(this).find(".slide-nav-control").append($("<div/>", {
				"class":"btn btn-prev-slide"
			}));
			var navControl = $(this).find(".slide-nav-control");
			navControl.on(settings.eventX, function(){
				slideOuter.addClass("pause");
			});

			var position=0;
			///Click vào nav Control [--btnNext & btnPrev--] để thay đổi Slide
			var navButton = slideOuter.find(".slide-nav-control").find(".btn");
			navButton.on("click", function(){
				var thisNav = slideOuter.find(".slide-block-nav").find("li.active");
				var indexNav = slideOuter.find(".slide-block-image").find("li.active").attr("index");
				
				if($(this).attr("class").indexOf("btn-next-slide")>0){
					position=0;
				}
				if($(this).attr("class").indexOf("btn-prev-slide")>0){
					position=1;
				}
				if(position===0 && thisNav.next().length>0){
					thisNav.removeClass("active");
					thisNav.next().addClass("active");
					indexNav = slideOuter.find(".slide-block-nav").find("li.active").attr("index");
					mainItem.css({"z-index": 0});
					var preSlide=slideOuter.find("."+settings.slideItem+"[index="+indexNav+"]");
					preSlide.addClass("active");
					if(settings.orientation==="verticle"){
						preSlide.css({"bottom": slideHeight, "top":"auto","z-index": 2});
						preSlide.animate({"bottom": 0}, settings.easing, function(){
							preSlide.siblings().removeClass("active");
						});
					}
					else{
						preSlide.css({"right": slideHeight, "left":"auto","z-index": 2});
						preSlide.animate({"right": 0}, settings.easing, function(){
							preSlide.siblings().removeClass("active");
						});
					}
				}
				else if(position===1 && thisNav.prev().length>0){						
					thisNav.removeClass("active");
					thisNav.prev().addClass("active");
					indexNav = slideOuter.find(".slide-block-nav").find("li.active").attr("index");
					mainItem.css({"z-index": 0});
					var preSlide=slideOuter.find("."+settings.slideItem+"[index="+indexNav+"]");
					preSlide.addClass("active");
					if(settings.orientation==="verticle"){
						preSlide.css({"top": slideHeight, "bottom":"auto","z-index": 2});
						preSlide.animate({"top": 0}, settings.easing, function(){
							preSlide.siblings().removeClass("active");
						});
					}
					else{
						preSlide.css({"left": slideHeight, "right":"auto","z-index": 2});
						preSlide.animate({"left": 0}, settings.easing, function(){
							preSlide.siblings().removeClass("active");
						});
					}
				}
			});
		}
		///Click vào nav Thumbnail để thay đổi Slide
		var slideHeight= mainItem.height() * -0.6;
		navItem.on(settings.eventX, function(){
			slideOuter.addClass("pause");
			//Current Index
			var indexNav = $(this).attr("index");
			var beforeIndex=slideOuter.find(".slide-block-nav").find("li.active").attr("index");
			if(indexNav > beforeIndex && $(this).hasClass("active")===false)
			{
				navItem.removeClass("active");
				$(this).addClass("active");
				mainItem.css({"z-index": 0});
				var preSlide=slideOuter.find("."+settings.slideItem+"[index="+indexNav+"]");
				preSlide.addClass("active");
				if(settings.orientation==="verticle"){
					preSlide.css({"bottom": slideHeight, "top":"auto", "z-index": 2});
					preSlide.animate({"bottom": 0}, settings.easing);
				}
				else{
					preSlide.css({"right": slideHeight, "left":"auto", "z-index": 2});
					preSlide.animate({"right": 0}, settings.easing);
				}
			}
			else if($(this).hasClass("active")===false){
				navItem.removeClass("active");
				$(this).addClass("active");
				mainItem.css({"z-index": 0});
				var preSlide=slideOuter.find("."+settings.slideItem+"[index="+indexNav+"]");
				preSlide.addClass("active");
				if(settings.orientation==="verticle"){
					preSlide.css({"top": slideHeight, "bottom":"auto", "z-index": 2});
					preSlide.animate({"top": 0}, settings.easing);	
				}
				else{
					preSlide.css({"left": slideHeight, "right":"auto", "z-index": 2});
					preSlide.animate({"left": 0}, settings.easing);	
				}	
			}
		});
		
		navItem.add(navControl).on(settings.eventNot, function(){
			slideOuter.removeClass("pause");
		});
		//setInterVal để Slide chuyển tự động sau [--settings.timmer--] thời gian
		var position=0;
		$.interVal=function(options){
			if(settings.randomTime===true){
				settings.timmer=(Math.random()+0.5) * settings.timmer;
			}
			var df={"interVal":true};
			var st= $.extend(df, options ||{});
			if(st.interVal===true && slideOuter.hasClass("pause")===false){
				setInterval(function(){
					
					if(st.interVal===true && slideOuter.hasClass("pause")===false){
						var thisNav = slideOuter.find(".slide-block-nav").find("li.active");
						var indexNav = slideOuter.find(".slide-block-image").find("li.active").attr("index");
						thisNav.removeClass("active");
						if(indexNav<=slideOuter.find(".slide-block-image").find("li").first().attr("index")){
							position=0;
						}
						if(indexNav>=slideOuter.find(".slide-block-image").find("li").last().attr("index"))
						{
							position=1;
						}
						if(position===0){
							thisNav.next().addClass("active");
							indexNav = slideOuter.find(".slide-block-nav").find("li.active").attr("index");
							mainItem.css({"z-index": 0});
							var preSlide=slideOuter.find("."+settings.slideItem+"[index="+indexNav+"]");
							preSlide.addClass("active");
							if(settings.orientation==="verticle"){
								preSlide.css({"bottom": slideHeight, "top":"auto","z-index": 2});
								preSlide.animate({"bottom": 0}, settings.easing, function(){
									preSlide.siblings().removeClass("active");
								});
							}
							else{
								preSlide.css({"right": slideHeight, "left":"auto","z-index": 2});
								preSlide.animate({"right": 0}, settings.easing, function(){
									preSlide.siblings().removeClass("active");
								});
							}
						}
						else if(position===1){						
							thisNav.prev().addClass("active");
							indexNav = slideOuter.find(".slide-block-nav").find("li.active").attr("index");
							mainItem.css({"z-index": 0});
							var preSlide=slideOuter.find("."+settings.slideItem+"[index="+indexNav+"]");
							preSlide.addClass("active");
							if(settings.orientation==="verticle"){
								preSlide.css({"top": slideHeight, "bottom":"auto", "z-index": 2});
								preSlide.animate({"top": 0}, settings.easing, function(){
									preSlide.siblings().removeClass("active");
								});
							}
							else{
								preSlide.css({"left": slideHeight, "right":"auto", "z-index": 2});
								preSlide.animate({"left": 0}, settings.easing, function(){
									preSlide.siblings().removeClass("active");
								});
							}
						}
					}
				}, settings.timmer);
			}
		}
		if(settings.autoPlay===true){
			$.interVal();	
		}
	});
}

$.fn.belloSlideSet = function (options) {
    $.each($(this), function () {
        var slideOuter = $(this);
        //2 biến defaults và settings dùng để custom
        var defaults = { "eventX": "mouseover",
            "eventNot": "mouseleave",
            "slideItem": "slide-item",
            "timmer": 6000,
            "easing": 400,
            "autoPlay": true,
            "navControl": true,
            "orientation": "horizontal",
            "randomTime": false,
            "numItem": 4
        };
        var settings = $.extend(defaults, options || {});
        //Biến dùng để lưu lại object html của ảnh chính
        var mainItem = $(this).find(".slide-item");
        //Thêm nav Control ! cái này khác với Nav Thumbnail, cái này chỉ gồm 2 button Next và Preview
        if (settings.navControl === true) {
            $(this).append($("<div/>", {
                "class": "slide-nav-control"
            }));
            $(this).find(".slide-nav-control").prepend($("<div/>", {
                "class": "btn btn-next-slide"
            }));
            $(this).find(".slide-nav-control").append($("<div/>", {
                "class": "btn btn-prev-slide"
            }));
            var navControl = $(this).find(".slide-nav-control");
            navControl.on(settings.eventX, function () {
                slideOuter.addClass("pause");
            });

            var position = 0;
            ///Click vào nav Control [--btnNext & btnPrev--] để thay đổi Slide
            slideOuterWidth = slideOuter.width();
            var slideInner = slideOuter.find(".inner-module");
            var navButton = slideOuter.find(".slide-nav-control").find(".btn");
            var startItem = 0;
            mainItem.removeClass("active").fadeOut(0);
            mainItem.slice(startItem, startItem + settings.numItem).fadeIn(0, function () {
                $(this).addClass("active");
            });
            navButton.on("click", function () {
                if ($(this).attr("class").indexOf("btn-next-slide") > 0) {
                    position = 0;
                }
                if ($(this).attr("class").indexOf("btn-prev-slide") > 0) {
                    position = 1;
                }

                if (position === 0 && mainItem.last().hasClass("active") === false && mainItem.length > settings.numItem -1) {
                    startItem = startItem + settings.numItem;
                    slideInner.css({ "left": slideOuterWidth / settings.numItem, "right": "auto" });
                    mainItem.removeClass("active").fadeOut(300);
                    slideInner.animate({ "left": 0, "right": "auto" });
                    mainItem.slice(startItem, startItem + settings.numItem).fadeIn(0, function () {
                        $(this).addClass("active");

                    });
                }
                else if (mainItem.hasClass("active") && position === 1 && mainItem.first().hasClass("active") === false && mainItem.length > settings.numItem -1) {
                    startItem = startItem - settings.numItem;
                    slideInner.css({ "right": slideOuterWidth / 4, "left": "auto" });
                    mainItem.removeClass("active").fadeOut(300);
                    slideInner.animate({ "right": 0, "left": "auto" });
                    mainItem.slice(startItem, startItem + settings.numItem).fadeIn(0, function () {
                        $(this).addClass("active");

                    });
                }
            });
        }
    });
}