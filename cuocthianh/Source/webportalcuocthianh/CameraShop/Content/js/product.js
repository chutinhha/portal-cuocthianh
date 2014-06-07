(function ($) {
    $.fn.changeImgZoom = function (options) {
        var defaults = { "numThumb": 5 };
        var settings = $.extend(defaults, options || {});
        var navTopArrow = $("<div/>", {
            "class": "znav-top-arrow"
        });
        var zNavInner = $(".z-nav-inner");
        var navBottomArrow = $("<div/>", {
            "class": "znav-bottom-arrow"
        });

        $(this).prepend(navTopArrow);
        $(this).append(navBottomArrow);
        $(this).find("a").on("mouseover", function () {
            $("#imgZoomMain").attr("src", $(this).find("img").attr("src"));
            $("#imgZoomMain").elevateZoom({ tint: true, scrollZoom: true });
        });
        var navItemHeight = parseFloat(zNavInner.find("li").height());
        var navHeight = 0;
        $(navBottomArrow).on("click", function () {
            if (navHeight <= zNavInner.find("li").length * zNavInner.find("li").height()) {
                navHeight += zNavInner.height() - (zNavInner.outerHeight() - zNavInner.innerHeight());
            }
            else {
                navHeight = 0;
            }
            zNavInner.find("ul").animate({ "margin-top": navHeight * -1 });
        });
        //navHeight=0;
        $(navTopArrow).on("click", function () {
            if (navHeight > zNavInner.find("li").length * zNavInner.find("li").height()) {
                navHeight -= zNavInner.height() - (zNavInner.outerHeight() - zNavInner.innerHeight());
            }
            else {
                navHeight = 0;
            }
            zNavInner.find("ul").animate({ "margin-top": navHeight * -1 });
        });
    }
    //Key up to update cart price
    $.fn.updateCartPrice = function () {
        $(this).on("keyup", function (e) {
            var singlePrice = parseFloat($(".single-price").text().replace(/[.]/gi, ""));
            var amountPrice = $(".total-price");
            var priceFormat = formatPrice(parseFloat(singlePrice * parseFloat($(this).val())), ".");
            amountPrice.text(priceFormat + " ₫");
            //$(this).text(amountPrice);
            //Update Qty Button Pay
            if ($(this).val() !== "0" && $(this).val() !== "") {
                $(".btn-detail-pay").attr("href", "/ShoppingCart/BuyNow?ProductId=11&amp;Qty=" + $(this).val() + "");
                $(".btn-detail-add-cart").attr("qty", $(this).val());
            }
        });
    }
    //Hàm format giá
    function formatPrice(str, insertChar) {
        if (isFinite(str) === false) {
            str = 0;
        }
        str = str.toString();
        for (var i = str.length - 1; i > 0; i--) {
            if (i % 3 === 0) {
                str = str.insertAt(str.length - i, insertChar);
            }
        }
        return str;
    }
} (jQuery));
(function ($) {
    $(document).ready(function (e) {
        $(".showcat-button").showDropDown({ "showDiv": ".nav-category", "eventX": "mouseover", "leaveToOut": true });
        $("#imgZoomMain").elevateZoom({ tint: true, scrollZoom: true });
        $(".zoom-nav").changeImgZoom();
        $(".tab-head").find("a").kyersTab();
        $(".slideProduct").belloSlideSet({ "numItem": 3 });
        $(".txt-count").updateCartPrice();
    });
} (jQuery));