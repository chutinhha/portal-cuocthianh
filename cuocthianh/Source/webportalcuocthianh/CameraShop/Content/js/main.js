(function ($) {
    $.fn.showDropDown = function (options) {
        var thisClick = $(this);
        var virtualArrow = $('<div/>', {
            "class": "top-arrow"
        });
        var defaults = { "showDiv": ".drop-down-profile", "setTopArrow": true, "eventX": "click", "eventNot": "mouseleave", "leaveToOut": false, "timmer": 300 };
        var settings = $.extend(defaults, options || {});
        if (settings.setTopArrow === true) {
            $(settings.showDiv).prepend(virtualArrow);
        }
        $(this).parent().find(settings.showDiv).slideUp(0);
        $(this).addClass("isuse");
        $(this).on(settings.eventX, function (e) {
            e.preventDefault() ? e.preventDefault() : e.returnValue;
            if ($(this).hasClass("active") === false) {
                $(settings.showDiv).slideUp(settings.timmer, function () {
                    thisClick.removeClass("active");
                });
                $(this).addClass("active");
                if ($(this).parent().find(settings.showDiv).length > 0) {
                    $(this).parent().find(settings.showDiv).slideDown(settings.timmer);
                }
                else {
                    $(this).siblings(settings.showDiv).slideDown(settings.timmer);
                }
            }
        });
        if (settings.leaveToOut === false) {
            $(document).on("click", function (e) {
                if (!$(e.target).closest(thisClick).length) {
                    $(settings.showDiv).slideUp(settings.timmer, function () {
                        thisClick.removeClass("active");
                    });
                }
            });
        }
        else {
            $(this).parent().on(settings.eventNot, function (e) {
                $(this).find(settings.showDiv).slideUp(settings.timmer, function () {
                    thisClick.removeClass("active");
                });
            });
        }
    }
    $.fn.customSelect = function () {
        $(this).each(function (index, element) {
            var mainSelect = $(this);
            var virtualSelect = $('<div/>', {
                "class": "virtual-select"
            });
            var virtualLabel = $('<span/>', {
                "class": "virtual-label",
                "text": $(this).find("option:selected").text()
            });
            var virtualArrow = $('<span/>', {
                "class": "virtual-arrow"
            });
            var ulList = $('<ul/>', {
                "class": "ul-listSelect"
            });
            $(this).wrap(function () {
                return virtualSelect;
            });
            $(this).before(virtualLabel);
            $(this).before(virtualArrow);
            $(this).parent(virtualSelect).append(ulList);
            $.each($(this).find("option"), function (index, element) {
                var text = $(element).text();
                var value = $(element).val();
                $(this).parent().parent().find(ulList).append("<li class='vir-select-option' virvalue=" + value + ">" + text + "</li>");
            });
            $(this).parents(virtualSelect).find(virtualLabel).showDropDown({ "showDiv": ".ul-listSelect", "setTopArrow": false, "timmer": 0 });
            $(this).parents(virtualSelect).find(".vir-select-option").on("click", function () {
                mainSelect.find("option:selected").text($(this).text()).val($(this).attr("virvalue"));
                $(virtualLabel).text($(this).text());
            });
        });
    }
    $.fn.dockLeftPanel = function (options) {
        var panel = $(this);
        panel.animate({ "right": panel.width() * -1 }, 2000);
        $(this).prepend('<a class="btn btn-blue btn-viewed" href="#"></a>');
        $(this).on("click", function (e) {
            e.preventDefault() ? e.preventDefault() : e.returnValue;
            if ($(this).hasClass("active") === false) {
                $(this).addClass("active");
                panel.animate({ "right": 0 });
            }
        });
        $(document).on("click", function (e) {
            if (!$(e.target).closest(panel).length) {
                panel.animate({ "right": panel.width() * -1 });
                panel.removeClass("active");
            }
        });
    }
    $.fn.AddToCart = function () {
        $(this).on("click", function () {
            var ProductId = $(this).attr("ProductId");
            var Qty = $(this).attr("Qty");
            if ($("#txt-cout-product").hasClass("txt-cout-product")) {
                Qty = $("#txt-cout-product").val();
            }
            $.ajax({
                type: "POST",
                url: '/ShoppingCart/AddToCart/',
                data: {
                    ProductId: ProductId,
                    Qty: Qty
                },
                traditional: true,
                dataType: 'json',
                success: function (e) {
                    $.topAlertPopup({ "text": "Thêm vào giỏ hàng thành công", "typeAlert": "success", "delay": "900" });
                    RefreshHeader();
                    RefreshCart();
                }
            });
        });
    }
    function RefreshCart() {
        $.get("/ShoppingCart/_ListCart", function (data) {
            $(".cart-view").html(data);
        });
    }
    function RefreshHeader() {
        $.get("/ShoppingCart/_CartHeader", function (data) {
            $(".nav-cart-box").html(data);
        });
    }
} (jQuery));
(function($){
	$(document).ready(function(e) {
       $(".link-account-infor").showDropDown({"showDiv":".account-dropdown", "timmer": 0});
	   $("#selectSearch").customSelect();
	   $("#txtNavSearch").kyersAutoComplete();
       $(".btn-add-cart").AddToCart();
       $(".viewed-product").dockLeftPanel();
       $("#txtNavSearch").kyersAutoComplete({"url":"/Search/SearchAll"});
    });
}(jQuery));